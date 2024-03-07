CREATE PROCEDURE [AION].[usp_select_aion_fma_internalmeetings_byprojectid] @projectid INT, 
                                                                          @scheduled BIT = 1
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
    AppointmentResponseStatusEnum
    [Description("Scheduled")]
        Scheduled = 5

    */

        --if scheduled is requested use the scheduledstatus
        --else no filter
        --get the appointment response status ref if
        DECLARE @scheduledstatus INT;
        SELECT @scheduledstatus = APPT_RESPONSE_STATUS_REF_ID
        FROM [AION].[APPOINTMENT_RESPONSE_STATUS_REF]
        WHERE ENUM_MAPPING_VAL_NBR = 5;
        --
        SELECT DISTINCT 
               PS.[RECURRING_APPT_DT] AS [MEETING_DATE_TIME], 
               FMA.FROM_DT,
               FMA.TO_DT,
               MTR.ENUM_MAPPING_VAL_NBR AS [MEETING_TYPE],
               P.[PROJECT_TYP_REF_ID] AS [PROJECT_TYPE_ID], --DO NOT HAVE ENUM MAP AND ENUMS ARE MAPPED DIRECTLY TO PK.   
               P.[SRC_SYSTEM_VAL_TXT] AS [PROJECT_EXTREF_ID], 
               P.[PROJECT_NM] AS [PROJECT_NM], 
               FMA.APPT_RESPONSE_STATUS_REF_ID AS [APPOINTMENT_RESPONSE_STATUS],
               CASE
                   WHEN PSR.ENUM_MAPPING_VAL_NBR IN(12, 13, 14, 15, 16, 17)
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
               FMA.APPT_RESPONSE_STATUS_REF_ID
        FROM [AION].[PROJECT_SCHEDULE] PS
             INNER JOIN [AION].[FACILITATOR_MEETING_APPOINTMENT] FMA ON PS.[APPT_ID] = FMA.[FACILITATOR_MEETING_APPT_IDENTIFIER]
             INNER JOIN [AION].[PROJECT] P ON P.[PROJECT_ID] = FMA.PROJECT_ID
             INNER JOIN [AION].[PROJECT_STATUS_REF] PSR ON PSR.PROJECT_STATUS_REF_ID = P.PROJECT_STATUS_REF_ID
			 INNER JOIN [AION].[MEETING_TYPE_REF] MTR ON FMA.MEETING_TYP_REF_ID = MTR.MEETING_TYP_REF_ID
        WHERE P.[PROJECT_ID] = @projectid
              AND (@scheduled = 0
                   OR FMA.APPT_RESPONSE_STATUS_REF_ID = @scheduledstatus)
              AND PS.[RECURRING_APPT_DT] IS NOT NULL;

        RETURN;
    END;