/***********************************************************************************************************************                
* Object:       usp_select_aion_schedule_capacity_search_meetings_list            
* Description:  Gets a schedule list by person                
* Parameters:                   
*               @startdate    DATETIME,                 
*               @enddate      DATETIME,                 
*               @reviewerscsv VARCHAR(8000)                
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
* 01/14/2021    jlindsay    add filter for express reservations - get only scheduled        
* 02/24/2021    jlindsay    increase csv field lenght to 8000      
* 06/22/2021    jallen      change NPA start and end dates to the project schedule recurring date      
* 06/22/2021 jlindsay add times to NPA's    
* 11/01/2021    jallen      changes for sub cycle integration  
11/3/2021 jlindsay remove jba notation
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_select_aion_schedule_capacity_search_meetings_list] @startdate DATETIME,
	@enddate DATETIME,
	@reviewerscsv VARCHAR(8000)
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

	WITH PMA
	AS (
		SELECT U.[USER_ID],
			PMA.[PRELIMINARY_MEETING_APPT_ID] AS APPT_ID,
			PS.[PROJECT_SCHEDULE_TYP_DESC] AS [MEETING_TYPE],
			PS.[PROJECT_SCHEDULE_TYP_DESC] AS APPT_NM,
			US.START_DTTM,
			US.END_DTTM
		FROM [AION].[USER] U
		INNER JOIN [AION].[USER_SCHEDULE] US ON U.[USER_Id] = US.[USER_ID]
		INNER JOIN [AION].[PROJECT_SCHEDULE] PS ON US.[PROJECT_SCHEDULE_ID] = PS.[PROJECT_SCHEDULE_ID]
		INNER JOIN [AION].[PRELIMINARY_MEETING_APPOINTMENT] PMA ON PS.[APPT_ID] = PMA.[PRELIMINARY_MEETING_APPT_ID]
			AND PS.[PROJECT_SCHEDULE_TYP_DESC] IN ('PMA')
		INNER JOIN [AION].[PROJECT] P ON P.[PROJECT_ID] = PMA.PROJECT_ID
		INNER JOIN [AION].[PROJECT_STATUS_REF] PSR ON PSR.PROJECT_STATUS_REF_ID = P.PROJECT_STATUS_REF_ID
		WHERE PMA.APPT_RESPONSE_STATUS_REF_ID IN (
				@scheduledstatus,
				@tentativelyscheduled,
				@notscheduled
				)
			AND CAST(US.START_DTTM AS DATE) <= CAST(@enddate AS DATE)
			AND CAST(US.END_DTTM AS DATE) >= CAST(@startdate AS DATE)
		),
	NPA
	AS (
		SELECT DISTINCT U.[USER_ID],
			NPA.[NON_PROJECT_APPT_ID] AS APPT_ID,
			PS.[PROJECT_SCHEDULE_TYP_DESC] AS [MEETING_TYPE],
			NPA.APPT_NM,
			cast(cast(PS.RECURRING_APPT_DT AS DATE) AS VARCHAR) + ' ' + CONVERT(VARCHAR(10), cast(US.START_DTTM AS TIME), 0) AS START_DTTM,
			cast(cast(PS.RECURRING_APPT_DT AS DATE) AS VARCHAR) + ' ' + CONVERT(VARCHAR(10), cast(US.END_DTTM AS TIME), 0) AS END_DTTM
		FROM [AION].[USER] U
		INNER JOIN [AION].[USER_SCHEDULE] US ON U.[USER_Id] = US.[USER_ID]
		INNER JOIN [AION].[PROJECT_SCHEDULE] PS ON US.[PROJECT_SCHEDULE_ID] = PS.[PROJECT_SCHEDULE_ID]
		INNER JOIN [AION].[NON_PROJECT_APPOINTMENT] NPA ON PS.[APPT_ID] = NPA.[NON_PROJECT_APPT_ID]
			AND PS.[PROJECT_SCHEDULE_TYP_DESC] IN ('NPA')
		WHERE CAST(US.START_DTTM AS DATE) <= CAST(@enddate AS DATE)
			AND CAST(US.END_DTTM AS DATE) >= CAST(@startdate AS DATE)
		),
	EXPRESS
	AS (
		SELECT DISTINCT U.[USER_ID],
			EXPR.[RESERVE_EXPRESS_RESERVATION_ID] AS APPT_ID,
			PS.[PROJECT_SCHEDULE_TYP_DESC] AS [MEETING_TYPE],
			'' AS APPT_NM,
			US.START_DTTM,
			US.END_DTTM
		FROM [AION].[USER] U
		INNER JOIN [AION].[USER_SCHEDULE] US ON U.[USER_Id] = US.[USER_ID]
		INNER JOIN [AION].[PROJECT_SCHEDULE] PS ON US.[PROJECT_SCHEDULE_ID] = PS.[PROJECT_SCHEDULE_ID]
		INNER JOIN [AION].[RESERVE_EXPRESS_RESERVATION] EXPR ON PS.[APPT_ID] = EXPR.[RESERVE_EXPRESS_RESERVATION_ID]
			AND PS.[PROJECT_SCHEDULE_TYP_DESC] IN ('EXP')
		WHERE EXPR.APPT_RESPONSE_STATUS_REF_ID IN (@scheduledstatus)
			AND CAST(US.START_DTTM AS DATE) <= CAST(@enddate AS DATE)
			AND CAST(US.END_DTTM AS DATE) >= CAST(@startdate AS DATE)
		),
	EMA
	AS (
		SELECT DISTINCT U.[USER_ID],
			EXPR.[PLAN_REVIEW_SCHEDULE_ID] AS APPT_ID,
			PS.[PROJECT_SCHEDULE_TYP_DESC] AS [MEETING_TYPE],
			'' AS APPT_NM,
			US.START_DTTM,
			US.END_DTTM
		FROM [AION].[USER] U
		INNER JOIN [AION].[USER_SCHEDULE] US ON U.[USER_Id] = US.[USER_ID]
		INNER JOIN [AION].[PROJECT_SCHEDULE] PS ON US.[PROJECT_SCHEDULE_ID] = PS.[PROJECT_SCHEDULE_ID]
		INNER JOIN [AION].[PLAN_REVIEW_SCHEDULE] EXPR ON PS.[APPT_ID] = EXPR.[PLAN_REVIEW_SCHEDULE_ID]
			AND PS.[PROJECT_SCHEDULE_TYP_DESC] IN ('EMA')
		WHERE EXPR.APPT_RESPONSE_STATUS_REF_ID IN (
				@scheduledstatus,
				@tentativelyscheduled,
				@notscheduled
				)
			AND CAST(US.START_DTTM AS DATE) <= CAST(@enddate AS DATE)
			AND CAST(US.END_DTTM AS DATE) >= CAST(@startdate AS DATE)
		),
	PR
	AS (
		SELECT DISTINCT U.[USER_ID],
			EXPR.[PLAN_REVIEW_SCHEDULE_ID] AS APPT_ID,
			PS.[PROJECT_SCHEDULE_TYP_DESC] AS [MEETING_TYPE],
			'' AS APPT_NM,
			US.START_DTTM,
			US.END_DTTM
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
	FMA
	AS (
		SELECT DISTINCT U.[USER_ID],
			EXPR.[FACILITATOR_MEETING_APPT_IDENTIFIER] AS APPT_ID,
			PS.[PROJECT_SCHEDULE_TYP_DESC] AS [MEETING_TYPE],
			CAST(MT.ENUM_MAPPING_VAL_NBR AS VARCHAR) AS APPT_NM,
			US.START_DTTM,
			US.END_DTTM
		FROM [AION].[USER] U
		INNER JOIN [AION].[USER_SCHEDULE] US ON U.[USER_Id] = US.[USER_ID]
		INNER JOIN [AION].[PROJECT_SCHEDULE] PS ON US.[PROJECT_SCHEDULE_ID] = PS.[PROJECT_SCHEDULE_ID]
		INNER JOIN [AION].[FACILITATOR_MEETING_APPOINTMENT] EXPR ON PS.[APPT_ID] = EXPR.[FACILITATOR_MEETING_APPT_IDENTIFIER]
			AND PS.[PROJECT_SCHEDULE_TYP_DESC] IN ('FMA')
		INNER JOIN [AION].MEETING_TYPE_REF MT ON EXPR.MEETING_TYP_REF_ID = MT.MEETING_TYP_REF_ID
		WHERE EXPR.APPT_RESPONSE_STATUS_REF_ID IN (
				@scheduledstatus,
				@tentativelyscheduled,
				@notscheduled
				)
			AND CAST(US.START_DTTM AS DATE) <= CAST(@enddate AS DATE)
			AND CAST(US.END_DTTM AS DATE) >= CAST(@startdate AS DATE)
		),
	FIFO
	AS (
		SELECT DISTINCT U.[USER_ID],
			EXPR.[PLAN_REVIEW_SCHEDULE_ID] AS APPT_ID,
			PS.[PROJECT_SCHEDULE_TYP_DESC] AS [MEETING_TYPE],
			'' AS APPT_NM,
			US.START_DTTM,
			US.END_DTTM
		FROM [AION].[USER] U
		INNER JOIN [AION].[USER_SCHEDULE] US ON U.[USER_Id] = US.[USER_ID]
		INNER JOIN [AION].[PROJECT_SCHEDULE] PS ON US.[PROJECT_SCHEDULE_ID] = PS.[PROJECT_SCHEDULE_ID]
		INNER JOIN [AION].[PLAN_REVIEW_SCHEDULE] EXPR ON PS.[APPT_ID] = EXPR.[PLAN_REVIEW_SCHEDULE_ID]
			AND PS.[PROJECT_SCHEDULE_TYP_DESC] IN ('FIFO')
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
	FROM PMA p
	INNER JOIN CSVUSERS v ON p.[user_id] = v.[user_id]
	
	UNION
	
	SELECT p.[USER_ID],
		[APPT_ID],
		MEETING_TYPE,
		APPT_NM,
		START_DTTM,
		END_DTTM
	FROM NPA p
	INNER JOIN CSVUSERS v ON p.[user_id] = v.[user_id]
	
	UNION
	
	SELECT p.[USER_ID],
		[APPT_ID],
		MEETING_TYPE,
		APPT_NM,
		START_DTTM,
		END_DTTM
	FROM EXPRESS p
	INNER JOIN CSVUSERS v ON p.[user_id] = v.[user_id]
	
	UNION
	
	SELECT p.[USER_ID],
		[APPT_ID],
		MEETING_TYPE,
		APPT_NM,
		START_DTTM,
		END_DTTM
	FROM EMA p
	INNER JOIN CSVUSERS v ON p.[user_id] = v.[user_id]
	
	UNION
	
	SELECT p.[USER_ID],
		[APPT_ID],
		MEETING_TYPE,
		APPT_NM,
		START_DTTM,
		END_DTTM
	FROM PR p
	INNER JOIN CSVUSERS v ON p.[user_id] = v.[user_id]
	
	UNION
	
	SELECT p.[USER_ID],
		[APPT_ID],
		MEETING_TYPE,
		APPT_NM,
		START_DTTM,
		END_DTTM
	FROM FMA p
	INNER JOIN CSVUSERS v ON p.[user_id] = v.[user_id]
	
	UNION
	
	SELECT p.[USER_ID],
		[APPT_ID],
		MEETING_TYPE,
		APPT_NM,
		START_DTTM,
		END_DTTM
	FROM FIFO p
	INNER JOIN CSVUSERS v ON p.[user_id] = v.[user_id];
END;
