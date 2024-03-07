/***********************************************************************************************************************            
* Object:      usp_update_aion_remove_future_user_schedules         
*                 
*		@csvRecIdTxt VARCHAR(max) is csv with recid ;
*		@inputDate VARCHAR(25) is the cutoff date, so everything after this date will be removed
*		@ReturnValue INT OUTPUT = number of row affected
* Returns:      Number of rows affected.            
* Comments:                
* Version:      1.0            
* Created by:   jlindsay            
* Created:      10/14/2021          
************************************************************************************************************************            
* Change History: Date, Name, Description            
*  10/14/2021   jlindsay initial 
* 11/4/2021 jlindsay sub cycle integration
* 11/4/2021 jlindsay LES-1958 - include future proactive schedules
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_update_aion_remove_future_user_schedules] @csvRecIdTxt VARCHAR(max),
	@inputDate VARCHAR(25),
	@ReturnValue INT OUTPUT
AS
BEGIN
	--DECLARE @csvRecIdTxt VARCHAR(max) = 'REC21-00000-00004,REC21-00000-000GH,REC21-00000-000HQ,REC21-00000-000FG,REC21-00000-000FH,REC21-00000-000FK,';
	--DECLARE @inputDate VARCHAR(25) = '2021-10-14 05:00:00';
	--'Scheduled'      
	DECLARE @ScheduledApptResponse INT = 0;

	SELECT @ScheduledApptResponse = APPT_RESPONSE_STATUS_REF_ID
	FROM AION.APPOINTMENT_RESPONSE_STATUS_REF
	WHERE ENUM_MAPPING_VAL_NBR = 5;

	-- get the project ids, exclude preliminary projects
	-- get the plan review schedule rows
	-- get the fifo plan review rows
	--delete the user schedule rows for the future (GT @todayDate)
	WITH recIdTxts
	AS (
		SELECT cast(ltrim(rtrim([value])) AS VARCHAR) AS [rec_id_txt]
		FROM STRING_SPLIT(@csvRecIdTxt, ',')
		),
	projectIds
	AS (
		SELECT project_id
		FROM aion.Project p
		WHERE p.preliminary_ind = 0 --preliminary projects are excluded
			AND p.project_typ_ref_id != 1 --express projects are excluded
			AND p.rec_id_txt IN (
				SELECT rec_id_txt
				FROM recIdTxts
				)
		),
	userSchedules
	AS (
		SELECT us.user_schedule_id
		FROM user_schedule us
		INNER JOIN project_schedule ps ON us.project_schedule_id = ps.project_schedule_id
			AND (
				project_schedule_typ_desc = 'PR'
				OR project_schedule_typ_desc = 'FIFO'
				)
		INNER JOIN [AION].plan_review_schedule pr ON ps.appt_id = pr.plan_review_schedule_id
		INNER JOIN [AION].[PROJECT_CYCLE] PC ON pr.[PROJECT_CYCLE_ID] = pr.[PROJECT_CYCLE_ID]
		INNER JOIN projectIds i ON pc.project_id = i.project_id
		WHERE pr.APPT_RESPONSE_STATUS_REF_ID = @ScheduledApptResponse
			AND cast(us.START_DTTM AS DATE) > cast(@inputDate AS DATE)
		)
	DELETE
	FROM AION.USER_SCHEDULE
	WHERE user_schedule_id IN (
			SELECT user_schedule_id
			FROM userSchedules us
			);

	SELECT @ReturnValue = @@ROWCOUNT;

	RETURN;
END;
