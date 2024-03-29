﻿/***********************************************************************************************************************    
* Object:       usp_select_aion_user_used_schedules_extras_by_user_id    
* Description:  Retrieves UserSchedule record for given key field(s).    
* Parameters:       
*               @identity                                                    int    
*    
* Returns:      Recordset.    
* Comments:     Developer may need to manually join to other tables, such as code tables,    
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.    
* Version:      1.0    
* Created by:   AION_user    
* Created:      3/19/2020    
************************************************************************************************************************    
* Change History: Date, Name, Description    
* 3/19/2020    AION_user     Auto-generated    
* 06/20/2021   jallen      Add FIFO appointments   
* 06/23/2021 jlindsay cast datetime to date  
* 11/03/2021   jallen      Updates for sub cycle integration  
* 12/7/2021 jlindsay change category for npas to time allocation type ref id
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_select_aion_user_used_schedules_extras_by_user_id_v2] @IDENTITY INT = NULL,
	@STDT DATETIME = NULL,
	@ENTDT DATETIME = NULL
AS
BEGIN
	DECLARE @DATESFROMTODAY INT = 0;

	IF (
			@STDT IS NULL
			AND @ENTDT IS NULL
			)
	BEGIN
		SELECT @DATESFROMTODAY = 1;
	END

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

	SELECT [USER_SCHEDULE_ID],
		[START_DTTM],
		[END_DTTM],
		[USER_ID],
		[PROJECT_SCHEDULE_ID],
		[PROJECT_SCHEDULE_TYP_DESC],
		PROJECT_ID,
		[BUSINESS_REF_ID],
		[PROJ_CATEGORY]
	FROM (
		SELECT U.[USER_SCHEDULE_ID] AS [USER_SCHEDULE_ID],
			U.[START_DTTM] AS [START_DTTM],
			U.[END_DTTM] AS [END_DTTM],
			U.[USER_ID] AS [USER_ID],
			PS.[APPT_ID] AS [PROJECT_SCHEDULE_ID],
			PS.[PROJECT_SCHEDULE_TYP_DESC] AS [PROJECT_SCHEDULE_TYP_DESC],
			P.PROJECT_ID AS PROJECT_ID,
			U.[BUSINESS_REF_ID] AS [BUSINESS_REF_ID],
			NULL AS [PROJ_CATEGORY]
		FROM [AION].[USER_SCHEDULE] U
		JOIN [AION].[PROJECT_SCHEDULE] PS ON PS.PROJECT_SCHEDULE_ID = U.PROJECT_SCHEDULE_ID
		JOIN [AION].[PLAN_REVIEW_SCHEDULE] PR ON PS.APPT_ID = PR.PLAN_REVIEW_SCHEDULE_ID
		JOIN [AION].[PROJECT_CYCLE] PC ON PR.PROJECT_CYCLE_ID = PC.PROJECT_CYCLE_ID
		JOIN [AION].PROJECT P ON PC.PROJECT_ID = P.PROJECT_ID
		WHERE PS.[PROJECT_SCHEDULE_TYP_DESC] = 'PR'
			AND (
				U.[USER_ID] = @identity
				OR @identity IS NULL
				)
			AND PR.APPT_RESPONSE_STATUS_REF_ID IN (
				@scheduledstatus,
				@tentativelyscheduled,
				@notscheduled
				)
		
		UNION
		
		SELECT U.[USER_SCHEDULE_ID] AS [USER_SCHEDULE_ID],
			U.[START_DTTM] AS [START_DTTM],
			U.[END_DTTM] AS [END_DTTM],
			U.[USER_ID] AS [USER_ID],
			PS.[APPT_ID] AS [PROJECT_SCHEDULE_ID],
			PS.[PROJECT_SCHEDULE_TYP_DESC] AS [PROJECT_SCHEDULE_TYP_DESC],
			P.PROJECT_ID AS PROJECT_ID,
			U.[BUSINESS_REF_ID] AS [BUSINESS_REF_ID],
			NULL AS [PROJ_CATEGORY]
		FROM [AION].[USER_SCHEDULE] U
		JOIN [AION].[PROJECT_SCHEDULE] PS ON PS.PROJECT_SCHEDULE_ID = U.PROJECT_SCHEDULE_ID
		JOIN [AION].[PLAN_REVIEW_SCHEDULE] EMA ON PS.APPT_ID = EMA.PLAN_REVIEW_SCHEDULE_ID
		JOIN [AION].[PROJECT_CYCLE] PC ON EMA.PROJECT_CYCLE_ID = PC.PROJECT_CYCLE_ID
		JOIN [AION].PROJECT P ON PC.PROJECT_ID = P.PROJECT_ID
		WHERE PS.[PROJECT_SCHEDULE_TYP_DESC] = 'EMA'
			AND (
				U.[USER_ID] = @identity
				OR @identity IS NULL
				)
			AND EMA.APPT_RESPONSE_STATUS_REF_ID IN (
				@scheduledstatus,
				@tentativelyscheduled,
				@notscheduled
				)
		
		UNION
		
		SELECT U.[USER_SCHEDULE_ID] AS [USER_SCHEDULE_ID],
			U.[START_DTTM] AS [START_DTTM],
			U.[END_DTTM] AS [END_DTTM],
			U.[USER_ID] AS [USER_ID],
			PS.[APPT_ID] AS [PROJECT_SCHEDULE_ID],
			PS.[PROJECT_SCHEDULE_TYP_DESC] AS [PROJECT_SCHEDULE_TYP_DESC],
			0 AS PROJECT_ID,
			U.[BUSINESS_REF_ID] AS [BUSINESS_REF_ID],
			NULL AS [PROJ_CATEGORY]
		FROM [AION].[USER_SCHEDULE] U
		JOIN [AION].[PROJECT_SCHEDULE] PS ON PS.PROJECT_SCHEDULE_ID = U.PROJECT_SCHEDULE_ID
		JOIN [AION].[RESERVE_EXPRESS_RESERVATION] EXP ON PS.APPT_ID = EXP.RESERVE_EXPRESS_RESERVATION_ID
		WHERE PS.[PROJECT_SCHEDULE_TYP_DESC] = 'EXP'
			AND (
				U.[USER_ID] = @identity
				OR @identity IS NULL
				)
			AND EXP.APPT_RESPONSE_STATUS_REF_ID IN (
				@scheduledstatus,
				@tentativelyscheduled,
				@notscheduled
				)
		
		UNION
		
		SELECT U.[USER_SCHEDULE_ID] AS [USER_SCHEDULE_ID],
			U.[START_DTTM] AS [START_DTTM],
			U.[END_DTTM] AS [END_DTTM],
			U.[USER_ID] AS [USER_ID],
			PS.[APPT_ID] AS [PROJECT_SCHEDULE_ID],
			PS.[PROJECT_SCHEDULE_TYP_DESC] AS [PROJECT_SCHEDULE_TYP_DESC],
			P.PROJECT_ID AS PROJECT_ID,
			U.[BUSINESS_REF_ID] AS [BUSINESS_REF_ID],
			NULL AS [PROJ_CATEGORY]
		FROM [AION].[USER_SCHEDULE] U
		JOIN [AION].[PROJECT_SCHEDULE] PS ON PS.PROJECT_SCHEDULE_ID = U.PROJECT_SCHEDULE_ID
		JOIN [AION].[FACILITATOR_MEETING_APPOINTMENT] FMA ON PS.APPT_ID = FMA.FACILITATOR_MEETING_APPT_IDENTIFIER
		JOIN [AION].PROJECT P ON FMA.PROJECT_ID = P.PROJECT_ID
		WHERE PS.[PROJECT_SCHEDULE_TYP_DESC] = 'FMA'
			AND (
				U.[USER_ID] = @identity
				OR @identity IS NULL
				)
			AND FMA.APPT_RESPONSE_STATUS_REF_ID IN (
				@scheduledstatus,
				@tentativelyscheduled,
				@notscheduled
				)
		
		UNION
		
		SELECT U.[USER_SCHEDULE_ID] AS [USER_SCHEDULE_ID],
			U.[START_DTTM] AS [START_DTTM],
			U.[END_DTTM] AS [END_DTTM],
			U.[USER_ID] AS [USER_ID],
			PS.[APPT_ID] AS [PROJECT_SCHEDULE_ID],
			PS.[PROJECT_SCHEDULE_TYP_DESC] AS [PROJECT_SCHEDULE_TYP_DESC],
			P.PROJECT_ID AS PROJECT_ID,
			U.[BUSINESS_REF_ID] AS [BUSINESS_REF_ID],
			NULL AS [PROJ_CATEGORY]
		FROM [AION].[USER_SCHEDULE] U
		JOIN [AION].[PROJECT_SCHEDULE] PS ON PS.PROJECT_SCHEDULE_ID = U.PROJECT_SCHEDULE_ID
		JOIN [AION].[PRELIMINARY_MEETING_APPOINTMENT] PMA ON PS.APPT_ID = PMA.PRELIMINARY_MEETING_APPT_ID
		JOIN [AION].PROJECT P ON PMA.PROJECT_ID = P.PROJECT_ID
		WHERE PS.[PROJECT_SCHEDULE_TYP_DESC] = 'PMA'
			AND (
				U.[USER_ID] = @identity
				OR @identity IS NULL
				)
			AND PMA.APPT_RESPONSE_STATUS_REF_ID IN (
				@scheduledstatus,
				@tentativelyscheduled,
				@notscheduled
				)
		
		UNION
		
		SELECT U.[USER_SCHEDULE_ID] AS [USER_SCHEDULE_ID],
			U.[START_DTTM] AS [START_DTTM],
			U.[END_DTTM] AS [END_DTTM],
			U.[USER_ID] AS [USER_ID],
			PS.[APPT_ID] AS [PROJECT_SCHEDULE_ID],
			PS.[PROJECT_SCHEDULE_TYP_DESC] AS [PROJECT_SCHEDULE_TYP_DESC],
			P.PROJECT_ID AS PROJECT_ID,
			U.[BUSINESS_REF_ID] AS [BUSINESS_REF_ID],
			NULL AS [PROJ_CATEGORY]
		FROM [AION].[USER_SCHEDULE] U
		JOIN [AION].[PROJECT_SCHEDULE] PS ON PS.PROJECT_SCHEDULE_ID = U.PROJECT_SCHEDULE_ID
		JOIN [AION].[PLAN_REVIEW_SCHEDULE] FS ON PS.APPT_ID = FS.PLAN_REVIEW_SCHEDULE_ID
		JOIN [AION].[PROJECT_CYCLE] PC ON FS.PROJECT_CYCLE_ID = PC.PROJECT_CYCLE_ID
		JOIN [AION].PROJECT P ON PC.PROJECT_ID = P.PROJECT_ID
		WHERE PS.[PROJECT_SCHEDULE_TYP_DESC] = 'FIFO'
			AND (
				U.[USER_ID] = @identity
				OR @identity IS NULL
				)
			AND FS.APPT_RESPONSE_STATUS_REF_ID IN (
				@scheduledstatus,
				@tentativelyscheduled,
				@notscheduled
				)
		
		UNION
		
		SELECT U.[USER_SCHEDULE_ID] AS [USER_SCHEDULE_ID],
			U.[START_DTTM] AS [START_DTTM],
			U.[END_DTTM] AS [END_DTTM],
			U.[USER_ID] AS [USER_ID],
			PS.[APPT_ID] AS [PROJECT_SCHEDULE_ID],
			PS.[PROJECT_SCHEDULE_TYP_DESC] AS [PROJECT_SCHEDULE_TYP_DESC],
			NPA.[NON_PROJECT_APPT_ID] AS PROJECT_ID,
			U.[BUSINESS_REF_ID] AS [BUSINESS_REF_ID],
			NPAT.TIME_ALLOCATION_TYP_REF_ID AS [PROJ_CATEGORY]
		FROM [AION].[USER_SCHEDULE] U
		JOIN [AION].[PROJECT_SCHEDULE] PS ON PS.PROJECT_SCHEDULE_ID = U.PROJECT_SCHEDULE_ID
		LEFT OUTER JOIN [AION].[NON_PROJECT_APPOINTMENT] NPA ON PS.[APPT_ID] = NPA.[NON_PROJECT_APPT_ID]
		LEFT OUTER JOIN [AION].[NON_PROJECT_APPOINTMENT_TYPE_REF] NPAT ON NPAT.[NON_PROJECT_APPT_TYP_REF_ID] = NPA.[NON_PROJECT_APPT_TYP_REF_ID]
		WHERE PS.[PROJECT_SCHEDULE_TYP_DESC] = 'NPA'
			AND (
				U.[USER_ID] = @identity
				OR @identity IS NULL
				)
		) T
	WHERE (
			@DATESFROMTODAY = 1
			AND START_DTTM IS NOT NULL
			AND END_DTTM IS NOT NULL
			AND CAST(END_DTTM AS DATE) > CAST(DATEADD(DD, - 1, GETDATE()) AS DATE)
			)
		OR (
			@DATESFROMTODAY = 0
			AND START_DTTM IS NOT NULL
			AND END_DTTM IS NOT NULL
			AND CAST(START_DTTM AS DATE) >= CAST(@STDT AS DATE)
			AND CAST(END_DTTM AS DATE) <= CAST(@ENTDT AS DATE)
			)
	ORDER BY [START_DTTM],
		[END_DTTM],
		[PROJECT_SCHEDULE_TYP_DESC]

	RETURN
END
