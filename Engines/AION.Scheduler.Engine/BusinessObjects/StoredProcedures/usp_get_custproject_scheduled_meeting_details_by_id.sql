﻿/***********************************************************************************************************************        
* Object:       [usp_get_custproject_scheduled_meeting_details_by_id]      
* Description:  Retrieves User list for given parameter(s).        
* Parameters:           
*                @projectid varchar(255)    
*        
* Returns:      Recordset.        
* Comments:         
* Version:      1.0        
* Created by:         
* Created:             
************************************************************************************************************************        
* Change History: Date, Name, Description        
* 01/20/2021    jlindsay add comment section, remove user_schedule table    
* 03/30/2021 jlindsay remove cancelled status filter, add project type desc filter  
* 04/10/2021 jallen add cancellation reason  
* 11/15/2021    jallen  response due should be two dates following the create date. 
* 2/15/2022	jlindsay add attendees csv
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_get_custproject_scheduled_meeting_details_by_id] @projectId VARCHAR(255)
AS
BEGIN
	WITH allmeetings
	AS (
		SELECT DISTINCT PMA.PRELIMINARY_MEETING_APPT_ID AS [MEETING_ID],
			7 AS [MEETING_TYPE], -- enum mapping value for PMA  
			PMA.FROM_DT,
			PMA.TO_DT,
			PMA.APPENDIX_AGENDA_DUE_DT AS AGENDA_DUE,
			DATEADD(day, 2, PMA.CREATED_DTTM) AS RESPONSE_DUE,
			ASR.ENUM_MAPPING_VAL_NBR AS APPT_RESPONSE_STATUS_REF_ID,
			ACR.ENUM_MAPPING_VAL_NBR AS APPT_CANCELLATION_REF_ID
		FROM [AION].[PRELIMINARY_MEETING_APPOINTMENT] PMA
		INNER JOIN [AION].[PROJECT_SCHEDULE] PS ON PS.[APPT_ID] = PMA.[PRELIMINARY_MEETING_APPT_ID]
			AND PS.[PROJECT_SCHEDULE_TYP_DESC] = 'PMA'
		INNER JOIN [AION].[PROJECT] P ON P.[PROJECT_ID] = PMA.PROJECT_ID
		INNER JOIN [AION].[PROJECT_STATUS_REF] PSR ON PSR.PROJECT_STATUS_REF_ID = P.PROJECT_STATUS_REF_ID
		INNER JOIN [AION].[APPOINTMENT_RESPONSE_STATUS_REF] ASR ON ASR.APPT_RESPONSE_STATUS_REF_ID = PMA.APPT_RESPONSE_STATUS_REF_ID
		LEFT JOIN [AION].[APPOINTMENT_CANCELLATION_REF] ACR ON ACR.APPT_CANCELLATION_REF_ID = PMA.[APPT_CANCELLATION_REF_ID]
		WHERE P.SRC_SYSTEM_VAL_TXT = @projectId
		
		UNION
		
		SELECT DISTINCT FMA.FACILITATOR_MEETING_APPT_IDENTIFIER AS [MEETING_ID],
			MTR.ENUM_MAPPING_VAL_NBR AS [MEETING_TYPE],
			FMA.FROM_DT,
			FMA.TO_DT,
			NULL AS AGENDA_DUE,
			DATEADD(day, 2, FMA.CREATED_DTTM) AS RESPONSE_DUE,
			ASR.ENUM_MAPPING_VAL_NBR AS APPT_RESPONSE_STATUS_REF_ID,
			ACR.ENUM_MAPPING_VAL_NBR AS APPT_CANCELLATION_REF_ID
		FROM [AION].[FACILITATOR_MEETING_APPOINTMENT] FMA
		INNER JOIN [AION].[PROJECT_SCHEDULE] PS ON PS.[APPT_ID] = FMA.[FACILITATOR_MEETING_APPT_IDENTIFIER]
			AND PS.[PROJECT_SCHEDULE_TYP_DESC] = 'FMA'
		INNER JOIN [AION].[PROJECT] P ON P.[PROJECT_ID] = FMA.PROJECT_ID
		INNER JOIN [AION].[PROJECT_STATUS_REF] PSR ON PSR.PROJECT_STATUS_REF_ID = P.PROJECT_STATUS_REF_ID
		INNER JOIN [AION].[MEETING_TYPE_REF] MTR ON MTR.MEETING_TYP_REF_ID = FMA.MEETING_TYP_REF_ID
		INNER JOIN [AION].[APPOINTMENT_RESPONSE_STATUS_REF] ASR ON ASR.APPT_RESPONSE_STATUS_REF_ID = FMA.APPT_RESPONSE_STATUS_REF_ID
		LEFT JOIN [AION].[APPOINTMENT_CANCELLATION_REF] ACR ON ACR.APPT_CANCELLATION_REF_ID = FMA.[APPT_CANCELLATION_REF_ID]
		WHERE P.SRC_SYSTEM_VAL_TXT = @projectId
		),
	projectschedules
	AS (
		SELECT DISTINCT PMA.PRELIMINARY_MEETING_APPT_ID AS [MEETING_ID],
			PS.PROJECT_SCHEDULE_ID
		FROM [AION].[PRELIMINARY_MEETING_APPOINTMENT] PMA
		INNER JOIN [AION].[PROJECT_SCHEDULE] PS ON PS.[APPT_ID] = PMA.[PRELIMINARY_MEETING_APPT_ID]
			AND PS.[PROJECT_SCHEDULE_TYP_DESC] = 'PMA'
		INNER JOIN [AION].[PROJECT] P ON P.[PROJECT_ID] = PMA.PROJECT_ID
		INNER JOIN [AION].[PROJECT_STATUS_REF] PSR ON PSR.PROJECT_STATUS_REF_ID = P.PROJECT_STATUS_REF_ID
		INNER JOIN [AION].[APPOINTMENT_RESPONSE_STATUS_REF] ASR ON ASR.APPT_RESPONSE_STATUS_REF_ID = PMA.APPT_RESPONSE_STATUS_REF_ID
		LEFT JOIN [AION].[APPOINTMENT_CANCELLATION_REF] ACR ON ACR.APPT_CANCELLATION_REF_ID = PMA.[APPT_CANCELLATION_REF_ID]
		WHERE P.SRC_SYSTEM_VAL_TXT = @projectId
		
		UNION
		
		SELECT DISTINCT FMA.FACILITATOR_MEETING_APPT_IDENTIFIER AS [MEETING_ID],
			PS.PROJECT_SCHEDULE_ID
		FROM [AION].[FACILITATOR_MEETING_APPOINTMENT] FMA
		INNER JOIN [AION].[PROJECT_SCHEDULE] PS ON PS.[APPT_ID] = FMA.[FACILITATOR_MEETING_APPT_IDENTIFIER]
			AND PS.[PROJECT_SCHEDULE_TYP_DESC] = 'FMA'
		INNER JOIN [AION].[PROJECT] P ON P.[PROJECT_ID] = FMA.PROJECT_ID
		INNER JOIN [AION].[PROJECT_STATUS_REF] PSR ON PSR.PROJECT_STATUS_REF_ID = P.PROJECT_STATUS_REF_ID
		INNER JOIN [AION].[MEETING_TYPE_REF] MTR ON MTR.MEETING_TYP_REF_ID = FMA.MEETING_TYP_REF_ID
		INNER JOIN [AION].[APPOINTMENT_RESPONSE_STATUS_REF] ASR ON ASR.APPT_RESPONSE_STATUS_REF_ID = FMA.APPT_RESPONSE_STATUS_REF_ID
		LEFT JOIN [AION].[APPOINTMENT_CANCELLATION_REF] ACR ON ACR.APPT_CANCELLATION_REF_ID = FMA.[APPT_CANCELLATION_REF_ID]
		WHERE P.SRC_SYSTEM_VAL_TXT = @projectId
		),
	attendees
	AS (
		SELECT am.[MEETING_ID],
			us.[USER_ID],
			us.[BUSINESS_REF_ID]
		FROM AION.USER_SCHEDULE us
		INNER JOIN projectschedules am ON us.PROJECT_SCHEDULE_ID = am.PROJECT_SCHEDULE_ID
		),
	attendeesCsv
	AS (
		SELECT us.[MEETING_ID],
			STRING_AGG(cast(us.[USER_ID] AS VARCHAR) + ',' + cast(us.[BUSINESS_REF_ID] AS VARCHAR) + ',' + u.first_nm + ',' + u.last_nm, ';') AS attendees
		FROM attendees us
		INNER JOIN AION.[USER] u ON us.[USER_ID] = u.[USER_ID]
		GROUP BY us.[MEETING_ID]
		)
	SELECT [MEETING_ID],
		[MEETING_TYPE],
		FROM_DT,
		TO_DT,
		AGENDA_DUE,
		RESPONSE_DUE,
		APPT_RESPONSE_STATUS_REF_ID,
		APPT_CANCELLATION_REF_ID,
		ATTENDEES = (
			SELECT attendees
			FROM attendeesCsv
			WHERE attendeesCsv.[MEETING_ID] = am.[MEETING_ID]
			)
	FROM allmeetings am

	RETURN;
END