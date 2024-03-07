/***********************************************************************************************************************          
* Object:       usp_select_aion_schedule_capacity_search_plan_reviews         
* Description:  Gets a schedule list by person          
* Parameters:             
*               @startdate    DATETIME,           
*               @enddate      DATETIME,           
*               @reviewerscsv VARCHAR(2000)          
*          
* Returns:      list          
* Comments:               
* Version:      1.0          
* Created by:   jlindsay          
* Created:      7/29/2020          
************************************************************************************************************************          
* Change History: Date, Name, Description          
* 7/29/2020    jlindsay     initial          
* 9/16/2020     jlindsay    add express meeting appointments          
* 11/30/2020    jlindsay    add Plan review scheduled      
* 12/02/2020    jlindsay    add tentatively scheduled      
* 01/05/2021    jlindsay    add saved status, add FMA    
* 11/01/2021    jallen      changes for sub cycle integration   
* 11/3/2021 jlindsay remove jba notation
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_select_aion_schedule_capacity_search_plan_reviews] @startdate DATETIME,
	@enddate DATETIME,
	@reviewerscsv VARCHAR(2000)
AS
BEGIN
	--          
	-- Scheduled         
	DECLARE @scheduledstatus INT;

	SELECT @scheduledstatus = APPT_RESPONSE_STATUS_REF_ID
	FROM [AION].[APPOINTMENT_RESPONSE_STATUS_REF]
	WHERE ENUM_MAPPING_VAL_NBR = 5;

	-- tentatively scheduled    
	DECLARE @tentativelyscheduled INT;

	SELECT @tentativelyscheduled = APPT_RESPONSE_STATUS_REF_ID
	FROM [AION].[APPOINTMENT_RESPONSE_STATUS_REF]
	WHERE ENUM_MAPPING_VAL_NBR = 9;

	-- not scheduled (this is saved and has to be considered)    
	DECLARE @notscheduled INT;

	SELECT @notscheduled = APPT_RESPONSE_STATUS_REF_ID
	FROM [AION].[APPOINTMENT_RESPONSE_STATUS_REF]
	WHERE ENUM_MAPPING_VAL_NBR = 4;

	--          
	DECLARE @reltable TABLE (
		ID INT,
		nm VARCHAR(20)
		);

	WITH PR
	AS (
		SELECT DISTINCT U.[USER_ID],
			EXPR.[PLAN_REVIEW_SCHEDULE_ID] AS APPT_ID,
			PS.[PROJECT_SCHEDULE_TYP_DESC] AS [MEETING_TYPE],
			'' AS APPT_NM,
			US.START_DTTM AS START_DTTM,
			US.END_DTTM AS END_DTTM
		FROM [AION].[USER] U
		INNER JOIN [AION].[USER_SCHEDULE] US ON U.[USER_Id] = US.[USER_ID]
		INNER JOIN [AION].[PROJECT_SCHEDULE] PS ON US.[PROJECT_SCHEDULE_ID] = PS.[PROJECT_SCHEDULE_ID]
		INNER JOIN [AION].[PLAN_REVIEW_SCHEDULE] EXPR ON PS.[APPT_ID] = EXPR.[PLAN_REVIEW_SCHEDULE_ID]
			AND PS.[PROJECT_SCHEDULE_TYP_DESC] IN ('PR')
		WHERE EXPR.APPT_RESPONSE_STATUS_REF_ID IN (
				@scheduledstatus,
				@tentativelyscheduled,
				@notscheduled
				)
			AND CAST(US.START_DTTM AS DATE) <= CAST(@enddate AS DATE)
			AND CAST(US.END_DTTM AS DATE) >= CAST(@startdate AS DATE)
		),
	CSVUSERS
	AS (
		SELECT [value] AS [user_id]
		FROM STRING_SPLIT(@reviewerscsv, ',')
		)
	SELECT p.[USER_ID],
		[APPT_ID],
		MEETING_TYPE,
		APPT_NM,
		START_DTTM,
		END_DTTM
	FROM PR p
	INNER JOIN CSVUSERS v ON p.[user_id] = v.[user_id]
END;
