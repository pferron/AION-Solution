﻿/***********************************************************************************************************************      
* Object:       usp_select_aion_fma_internalmeetings_byprojectid      
* Description:  Retrieves Scheduled Meeting list for given parameter(s).      
* Parameters:         
*      
* Returns:      Recordset.      
* Comments:     Returns any scheduled meeting for existing db projects even if no users have been added      
*                     
*                     
* Version:      1.0      
* Created by:   jallen      
* Created:      12/02/2020      
************************************************************************************************************************      
* Change History: Date, Name, Description      
* 12/02/2020    jallen     initial      
* 03/30/2021    jlindsay    add project schedule type desc filter      
* 11/03/2021    jallen      add appt id      
* 2/15/2022 jlindsay add attendees csv    
* 4/22/2022	jlindsay change where we get meetings users for front end UI display
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_select_aion_fma_internalmeetings_byprojectid_v2] @projectid INT
AS
BEGIN
	/*      
      
    public enum MeetingTypeEnum      
    {      
        Exit = 1,      
        Project_Challenges = 2,      
        Code_Admin = 3,      
        Legal_Easement = 4,      
        Phasing = 5,      
        Prepermitting = 6,      
        Preliminary = 7,      
        Express = 8,      
        NA = -1      
    }      
    */
	--      
	WITH allmeetings
	AS (
		SELECT DISTINCT PS.[RECURRING_APPT_DT] AS [MEETING_DATE_TIME],
			FMA.FROM_DT,
			FMA.TO_DT,
			MTR.ENUM_MAPPING_VAL_NBR AS [MEETING_TYPE],
			P.[PROJECT_TYP_REF_ID] AS [PROJECT_TYPE_ID], --DO NOT HAVE ENUM MAP AND ENUMS ARE MAPPED DIRECTLY TO PK.         
			P.[SRC_SYSTEM_VAL_TXT] AS [PROJECT_EXTREF_ID],
			P.[PROJECT_NM] AS [PROJECT_NM],
			CASE 
				WHEN PSR.ENUM_MAPPING_VAL_NBR IN (
						12,
						13,
						14,
						15,
						16,
						17
						)
					THEN PSR.ENUM_MAPPING_VAL_NBR
				ELSE 14 -- IF ANY OTHER STATUS THEN CONSIDER IT AS NOT SCHEDULED. ANYTHING OTHER NEED TO BE ADDED TO ANOTHER CASE HERE AS 3RD STEP.        
				END AS [PROJECT_STATUS],
			'' AS [AGENDA_DUE],
			PS.[RECURRING_APPT_DT] AS [MINUTES_DUE],
			P.[PROJECT_ID] AS [PROJECT_ID],
			0 AS [USER_ID],
			FMA.[WKR_ID_CREATED_TXT] AS [WKR_ID_CREATED_TXT],
			FMA.[CREATED_DTTM] AS [CREATED_DTTM],
			FMA.[WKR_ID_UPDATED_TXT] AS [WKR_ID_UPDATED_TXT],
			FMA.[UPDATED_DTTM] AS [UPDATED_DTTM],
			FMA.MEETING_ROOM_REF_ID,
			ps.PROJECT_SCHEDULE_ID,
			ARSR.ENUM_MAPPING_VAL_NBR AS APPT_RESPONSE_STATUS_REF_ID,
			PS.APPT_ID
		FROM [AION].[PROJECT_SCHEDULE] PS
		INNER JOIN [AION].[FACILITATOR_MEETING_APPOINTMENT] FMA ON PS.[APPT_ID] = FMA.[FACILITATOR_MEETING_APPT_IDENTIFIER]
			AND PS.[PROJECT_SCHEDULE_TYP_DESC] = 'FMA'
		INNER JOIN [AION].[PROJECT] P ON P.[PROJECT_ID] = FMA.PROJECT_ID
		INNER JOIN [AION].[PROJECT_STATUS_REF] PSR ON PSR.PROJECT_STATUS_REF_ID = P.PROJECT_STATUS_REF_ID
		INNER JOIN [AION].[MEETING_TYPE_REF] MTR ON FMA.MEETING_TYP_REF_ID = MTR.MEETING_TYP_REF_ID
		INNER JOIN [AION].APPOINTMENT_RESPONSE_STATUS_REF ARSR ON FMA.APPT_RESPONSE_STATUS_REF_ID = ARSR.APPT_RESPONSE_STATUS_REF_ID
		WHERE P.[PROJECT_ID] = @projectid
			AND PS.[RECURRING_APPT_DT] IS NOT NULL
		),
	attendees
	AS (
		SELECT am.PROJECT_SCHEDULE_ID,
			us.ASSIGNED_PLAN_REVIEWER_ID AS [USER_ID],
			us.[BUSINESS_REF_ID]
		FROM AION.FACILITATOR_MEETING_APPOINTMENT_DETAIL us
		INNER JOIN allMeetings am ON us.FACILITATOR_MEETING_APPT_IDENTIFIER = am.APPT_ID
		),
	attendeesCsv
	AS (
		SELECT us.PROJECT_SCHEDULE_ID,
			STRING_AGG(cast(us.[USER_ID] AS VARCHAR) + ',' + cast(us.[BUSINESS_REF_ID] AS VARCHAR) + ',' + u.first_nm + ',' + u.last_nm, ';') AS attendees
		FROM attendees us
		INNER JOIN AION.[USER] u ON us.[USER_ID] = u.[USER_ID]
		GROUP BY us.PROJECT_SCHEDULE_ID
		)
	SELECT [MEETING_DATE_TIME],
		FROM_DT,
		TO_DT,
		[MEETING_TYPE],
		[PROJECT_TYPE_ID],
		[PROJECT_EXTREF_ID],
		[PROJECT_NM],
		[PROJECT_STATUS],
		[AGENDA_DUE],
		[MINUTES_DUE],
		[PROJECT_ID],
		[USER_ID],
		[WKR_ID_CREATED_TXT],
		[CREATED_DTTM],
		[WKR_ID_UPDATED_TXT],
		[UPDATED_DTTM],
		MEETING_ROOM_REF_ID,
		PROJECT_SCHEDULE_ID,
		APPT_RESPONSE_STATUS_REF_ID,
		APPT_ID,
		ATTENDEES = (
			SELECT attendees
			FROM attendeesCsv
			WHERE attendeesCsv.PROJECT_SCHEDULE_ID = am.PROJECT_SCHEDULE_ID
			)
	FROM allmeetings am

	RETURN;
END;