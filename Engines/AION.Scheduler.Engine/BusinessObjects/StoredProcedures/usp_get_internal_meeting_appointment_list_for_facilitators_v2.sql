/***********************************************************************************************************************              
* Object:       usp_get_internal_meeting_appointment_list_for_facilitators              
* Description:  Gets all the meetings.              
* Parameters:                 
*               @identity                                            int              
*              
* Returns:      list of meetings             
* Comments:                 
* Version:      1.0              
* Created by:   AION_user              
* Created:      7/24/2020              
************************************************************************************************************************              
* Change History: Date, Name, Description              
* 7/24/2020    jlindsay     initial              
* 12/15/2020    jlindsay    add express (EMA) meeting type             
* 03/18/2021    jlindsay    add APPT_CANCELLATION_REF_ID          
* 7/8/2021  jlindsay add missing fields        
* 7/13/2021  jallen  add team score TEAM_GRADE_TXT        
* 10/19/2021 jlindsay change meeting type enum for express meetings     
* 10/19/2021 jlindsay filter for past meetings - remove from list the day after the meeting date    
* 10/28/2021 jallen  add express from plan review schedule table    
* 11/3/2021 jlindsay remove jba notation  
*	12/9/2021 jlindsay update SQL joins
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_get_internal_meeting_appointment_list_for_facilitators_v2] @identity INT
AS
BEGIN
	DECLARE @closedapptstatus INT;
	DECLARE @cancelledapptstatus INT;
	DECLARE @CancelledProjectStatus INT;
	DECLARE @todaysDate DATETIME = getdate();

	SET @todaysDate = cast(@todaysDate AS DATE);

	--** get the appt status of closed and cancelled            
	--(@closedapptstatus,@cancelledapptstatus)            
	SELECT @closedapptstatus = APPT_RESPONSE_STATUS_REF_ID
	FROM [AION].[APPOINTMENT_RESPONSE_STATUS_REF]
	WHERE ENUM_MAPPING_VAL_NBR = 8;

	--cancelled sts            
	SELECT @cancelledapptstatus = APPT_RESPONSE_STATUS_REF_ID
	FROM [AION].[APPOINTMENT_RESPONSE_STATUS_REF]
	WHERE ENUM_MAPPING_VAL_NBR = 7;

	--** get the project status of cancelled            
	SELECT @CancelledProjectStatus = PROJECT_STATUS_REF_ID
	FROM [AION].[PROJECT_STATUS_REF]
	WHERE SRC_SYSTEM_VAL_TXT = 'Cancelled';

	WITH activeProjectsWEMA
	AS (
		SELECT p.PROJECT_ID,
			pc.PROJECT_CYCLE_ID,
			prs.PLAN_REVIEW_SCHEDULE_ID AS [APPT_ID],
			p.[PROJECT_TYP_REF_ID] AS [PROJECT_TYPE_ID],
			p.[SRC_SYSTEM_VAL_TXT] AS [PROJECT_EXTREF_ID],
			p.[PROJECT_NM],
			ars.ENUM_MAPPING_VAL_NBR AS [APPOINTMENT_RESPONSE_STATUS],
			CASE 
				WHEN ars.ENUM_MAPPING_VAL_NBR IN (
						12,
						13,
						14,
						15,
						16,
						17
						)
					THEN ars.ENUM_MAPPING_VAL_NBR
				ELSE 14 -- IF ANY OTHER STATUS THEN CONSIDER IT AS NOT SCHEDULED. ANYTHING OTHER NEED TO BE ADDED TO ANOTHER CASE HERE AS 3RD STEP.                
				END AS [PROJECT_STATUS],
			'' AS [AGENDA_DUE],
			prs.[WKR_ID_CREATED_TXT] AS [WKR_ID_CREATED_TXT],
			prs.[CREATED_DTTM] AS [CREATED_DTTM],
			prs.[WKR_ID_UPDATED_TXT] AS [WKR_ID_UPDATED_TXT],
			prs.[UPDATED_DTTM] AS [UPDATED_DTTM],
			prs.APPT_CANCELLATION_REF_ID,
			P.[RTAP_IND],
			P.[ASSIGNED_FACILITATOR_ID],
			P.[PROJECT_MANAGER_ID],
			P.[REC_ID_TXT],
			P.[TEAM_GRADE_TXT],
			8 AS [MEETING_TYPE],
			p.[BUILD_CODE_VERSION_DESC]
		FROM [AION].[PROJECT] p
		INNER JOIN [AION].[PROJECT_CYCLE] pc ON p.PROJECT_ID = pc.PROJECT_ID
		INNER JOIN [AION].[PLAN_REVIEW_SCHEDULE] prs ON prs.PROJECT_CYCLE_ID = pc.PROJECT_CYCLE_ID
			AND [PROJECT_SCHEDULE_TYP_DESC] = 'EMA'
		INNER JOIN [AION].APPOINTMENT_RESPONSE_STATUS_REF ars ON ars.APPT_RESPONSE_STATUS_REF_ID = prs.APPT_RESPONSE_STATUS_REF_ID
		WHERE p.PROJECT_STATUS_REF_ID <> @CancelledProjectStatus
			AND prs.APPT_RESPONSE_STATUS_REF_ID NOT IN (
				@closedapptstatus,
				@cancelledapptstatus
				)
		),
	activeProjectsWPMA
	AS (
		SELECT p.PROJECT_ID,
			pc.PROJECT_CYCLE_ID,
			PRELIMINARY_MEETING_APPT_ID AS [APPT_ID],
			p.[PROJECT_TYP_REF_ID] AS [PROJECT_TYPE_ID],
			p.[SRC_SYSTEM_VAL_TXT] AS [PROJECT_EXTREF_ID],
			p.[PROJECT_NM],
			ars.ENUM_MAPPING_VAL_NBR AS [APPOINTMENT_RESPONSE_STATUS],
			CASE 
				WHEN ars.ENUM_MAPPING_VAL_NBR IN (
						12,
						13,
						14,
						15,
						16,
						17
						)
					THEN ars.ENUM_MAPPING_VAL_NBR
				ELSE 14 -- IF ANY OTHER STATUS THEN CONSIDER IT AS NOT SCHEDULED. ANYTHING OTHER NEED TO BE ADDED TO ANOTHER CASE HERE AS 3RD STEP.                
				END AS [PROJECT_STATUS],
			'' AS [AGENDA_DUE],
			prs.[WKR_ID_CREATED_TXT] AS [WKR_ID_CREATED_TXT],
			prs.[CREATED_DTTM] AS [CREATED_DTTM],
			prs.[WKR_ID_UPDATED_TXT] AS [WKR_ID_UPDATED_TXT],
			prs.[UPDATED_DTTM] AS [UPDATED_DTTM],
			prs.APPT_CANCELLATION_REF_ID,
			P.[RTAP_IND],
			P.[ASSIGNED_FACILITATOR_ID],
			P.[PROJECT_MANAGER_ID],
			P.[REC_ID_TXT],
			P.[TEAM_GRADE_TXT],
			7 AS [MEETING_TYPE],
			p.[BUILD_CODE_VERSION_DESC]
		FROM [AION].[PROJECT] p
		INNER JOIN [AION].[PROJECT_CYCLE] pc ON p.PROJECT_ID = pc.PROJECT_ID
		INNER JOIN [AION].[PRELIMINARY_MEETING_APPOINTMENT] prs ON prs.PROJECT_ID = p.PROJECT_ID
		INNER JOIN [AION].APPOINTMENT_RESPONSE_STATUS_REF ars ON ars.APPT_RESPONSE_STATUS_REF_ID = prs.APPT_RESPONSE_STATUS_REF_ID
		WHERE p.PROJECT_STATUS_REF_ID <> @CancelledProjectStatus
			AND prs.APPT_RESPONSE_STATUS_REF_ID NOT IN (
				@closedapptstatus,
				@cancelledapptstatus
				)
		),
	activeProjectsWFMA
	AS (
		SELECT p.PROJECT_ID,
			pc.PROJECT_CYCLE_ID,
			[FACILITATOR_MEETING_APPT_IDENTIFIER] AS [APPT_ID],
			p.[PROJECT_TYP_REF_ID] AS [PROJECT_TYPE_ID],
			p.[SRC_SYSTEM_VAL_TXT] AS [PROJECT_EXTREF_ID],
			p.[PROJECT_NM],
			ars.ENUM_MAPPING_VAL_NBR AS [APPOINTMENT_RESPONSE_STATUS],
			CASE 
				WHEN ars.ENUM_MAPPING_VAL_NBR IN (
						12,
						13,
						14,
						15,
						16,
						17
						)
					THEN ars.ENUM_MAPPING_VAL_NBR
				ELSE 14 -- IF ANY OTHER STATUS THEN CONSIDER IT AS NOT SCHEDULED. ANYTHING OTHER NEED TO BE ADDED TO ANOTHER CASE HERE AS 3RD STEP.                
				END AS [PROJECT_STATUS],
			'' AS [AGENDA_DUE],
			prs.[WKR_ID_CREATED_TXT] AS [WKR_ID_CREATED_TXT],
			prs.[CREATED_DTTM] AS [CREATED_DTTM],
			prs.[WKR_ID_UPDATED_TXT] AS [WKR_ID_UPDATED_TXT],
			prs.[UPDATED_DTTM] AS [UPDATED_DTTM],
			prs.APPT_CANCELLATION_REF_ID,
			P.[RTAP_IND],
			P.[ASSIGNED_FACILITATOR_ID],
			P.[PROJECT_MANAGER_ID],
			P.[REC_ID_TXT],
			P.[TEAM_GRADE_TXT],
			m.ENUM_MAPPING_VAL_NBR AS [MEETING_TYPE],
			p.[BUILD_CODE_VERSION_DESC]
		FROM [AION].[PROJECT] p
		INNER JOIN [AION].[PROJECT_CYCLE] pc ON p.PROJECT_ID = pc.PROJECT_ID
		INNER JOIN [AION].[FACILITATOR_MEETING_APPOINTMENT] prs ON prs.PROJECT_ID = p.PROJECT_ID
		INNER JOIN [AION].APPOINTMENT_RESPONSE_STATUS_REF ars ON ars.APPT_RESPONSE_STATUS_REF_ID = prs.APPT_RESPONSE_STATUS_REF_ID
		INNER JOIN [AION].[MEETING_TYPE_REF] m ON prs.MEETING_TYP_REF_ID = m.MEETING_TYP_REF_ID
		WHERE p.PROJECT_STATUS_REF_ID <> @CancelledProjectStatus
			AND prs.APPT_RESPONSE_STATUS_REF_ID NOT IN (
				@closedapptstatus,
				@cancelledapptstatus
				)
		),
	activeEMAs
	AS (
		SELECT PS.[APPT_ID],
			PS.[RECURRING_APPT_DT] AS [MEETING_DATE_TIME],
			PS.[RECURRING_APPT_DT] AS [MINUTES_DUE],
			0 AS [USER_ID]
		FROM [AION].[USER_SCHEDULE] US
		INNER JOIN [AION].[PROJECT_SCHEDULE] PS ON US.[PROJECT_SCHEDULE_ID] = PS.[PROJECT_SCHEDULE_ID]
			AND PS.[PROJECT_SCHEDULE_TYP_DESC] = 'EMA'
		WHERE PS.[RECURRING_APPT_DT] IS NOT NULL
			AND cast(PS.[RECURRING_APPT_DT] AS DATE) >= @todaysDate
		GROUP BY PS.[APPT_ID],
			PS.[RECURRING_APPT_DT]
		),
	activeFMAs
	AS (
		SELECT PS.[APPT_ID],
			PS.[RECURRING_APPT_DT] AS [MEETING_DATE_TIME],
			PS.[RECURRING_APPT_DT] AS [MINUTES_DUE],
			0 AS [USER_ID]
		FROM [AION].[USER_SCHEDULE] US
		INNER JOIN [AION].[PROJECT_SCHEDULE] PS ON US.[PROJECT_SCHEDULE_ID] = PS.[PROJECT_SCHEDULE_ID]
			AND PS.[PROJECT_SCHEDULE_TYP_DESC] = 'FMA'
		WHERE PS.[RECURRING_APPT_DT] IS NOT NULL
			AND cast(PS.[RECURRING_APPT_DT] AS DATE) >= @todaysDate
		GROUP BY PS.[APPT_ID],
			PS.[RECURRING_APPT_DT]
		),
	activePMAs
	AS (
		SELECT PS.[APPT_ID],
			PS.[RECURRING_APPT_DT] AS [MEETING_DATE_TIME],
			PS.[RECURRING_APPT_DT] AS [MINUTES_DUE],
			0 AS [USER_ID]
		FROM [AION].[USER_SCHEDULE] US
		INNER JOIN [AION].[PROJECT_SCHEDULE] PS ON US.[PROJECT_SCHEDULE_ID] = PS.[PROJECT_SCHEDULE_ID]
			AND PS.[PROJECT_SCHEDULE_TYP_DESC] = 'PMA'
		WHERE PS.[RECURRING_APPT_DT] IS NOT NULL
			AND cast(PS.[RECURRING_APPT_DT] AS DATE) >= @todaysDate
		GROUP BY PS.[APPT_ID],
			PS.[RECURRING_APPT_DT]
		)
	SELECT [MEETING_DATE_TIME],
		[MEETING_TYPE],
		[PROJECT_TYPE_ID],
		[PROJECT_EXTREF_ID],
		[PROJECT_NM],
		[APPOINTMENT_RESPONSE_STATUS],
		[PROJECT_STATUS],
		[AGENDA_DUE],
		[MINUTES_DUE],
		[PROJECT_ID],
		[USER_ID],
		[WKR_ID_CREATED_TXT],
		[CREATED_DTTM],
		[WKR_ID_UPDATED_TXT],
		[UPDATED_DTTM],
		[APPT_CANCELLATION_REF_ID],
		[RTAP_IND],
		[ASSIGNED_FACILITATOR_ID],
		[PROJECT_MANAGER_ID],
		[REC_ID_TXT],
		[TEAM_GRADE_TXT],
		[BUILD_CODE_VERSION_DESC]
	FROM activeEMAs e
	INNER JOIN activeProjectsWEMA p ON e.[APPT_ID] = p.[APPT_ID]
	
	UNION
	
	SELECT [MEETING_DATE_TIME],
		[MEETING_TYPE],
		[PROJECT_TYPE_ID],
		[PROJECT_EXTREF_ID],
		[PROJECT_NM],
		[APPOINTMENT_RESPONSE_STATUS],
		[PROJECT_STATUS],
		[AGENDA_DUE],
		[MINUTES_DUE],
		[PROJECT_ID],
		[USER_ID],
		[WKR_ID_CREATED_TXT],
		[CREATED_DTTM],
		[WKR_ID_UPDATED_TXT],
		[UPDATED_DTTM],
		[APPT_CANCELLATION_REF_ID],
		[RTAP_IND],
		[ASSIGNED_FACILITATOR_ID],
		[PROJECT_MANAGER_ID],
		[REC_ID_TXT],
		[TEAM_GRADE_TXT],
		[BUILD_CODE_VERSION_DESC]
	FROM activePMAs e
	INNER JOIN activeProjectsWPMA p ON e.[APPT_ID] = p.[APPT_ID]
	
	UNION
	
	SELECT [MEETING_DATE_TIME],
		[MEETING_TYPE],
		[PROJECT_TYPE_ID],
		[PROJECT_EXTREF_ID],
		[PROJECT_NM],
		[APPOINTMENT_RESPONSE_STATUS],
		[PROJECT_STATUS],
		[AGENDA_DUE],
		[MINUTES_DUE],
		[PROJECT_ID],
		[USER_ID],
		[WKR_ID_CREATED_TXT],
		[CREATED_DTTM],
		[WKR_ID_UPDATED_TXT],
		[UPDATED_DTTM],
		[APPT_CANCELLATION_REF_ID],
		[RTAP_IND],
		[ASSIGNED_FACILITATOR_ID],
		[PROJECT_MANAGER_ID],
		[REC_ID_TXT],
		[TEAM_GRADE_TXT],
		[BUILD_CODE_VERSION_DESC]
	FROM activeFMAs e
	INNER JOIN activeProjectsWFMA p ON e.[APPT_ID] = p.[APPT_ID];

	RETURN;
END;
