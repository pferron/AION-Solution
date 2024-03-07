/***********************************************************************************************************************
* Object:       usp_select_aion_facilitator_meeting_appointment_get_list
* Description:  Retrieves FacilitatorMeetingAppointment list for given parameter(s).
* Parameters:   
*               @identity                                                   int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      10/7/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/7/2020    AION_user     Auto-generated
* 04/10/2021   jallen      add proposed dates
* 07/29/2021   jallen      add new field for EXTERNAL_ATTENDEES_CNT
***********************************************************************************************************************/
ALTER PROCEDURE [usp_select_aion_facilitator_meeting_appointment_get_list_by_project_and_meeting_type] @SRC_SYSTEM_VAL_TEXT VARCHAR(255)
	,@MEETING_TYP_DESC VARCHAR(100)
AS
SELECT FMA.FACILITATOR_MEETING_APPT_IDENTIFIER
	,FMA.PROJECT_ID
	,FMA.MEETING_ROOM_REF_ID
	,FMA.APPT_RESPONSE_STATUS_REF_ID
	,FMA.FROM_DT
	,FMA.TO_DT
	,FMA.WKR_ID_CREATED_TXT
	,FMA.CREATED_DTTM
	,FMA.WKR_ID_UPDATED_TXT
	,FMA.UPDATED_DTTM
	,FMA.VIRTUAL_MEETING_IND
	,FMA.MEETING_TYP_REF_ID
	,FMA.APPT_CANCELLATION_REF_ID
	,FMA.CANCEL_AFTER_DT
	,FMA.PROPOSED_1_DT
	,FMA.PROPOSED_2_DT
	,FMA.PROPOSED_3_DT
	,FMA.EXTERNAL_ATTENDEES_CNT
FROM FACILITATOR_MEETING_APPOINTMENT FMA
INNER JOIN PROJECT P ON FMA.PROJECT_ID = P.PROJECT_ID
INNER JOIN MEETING_TYPE_REF MTR ON FMA.MEETING_TYP_REF_ID = MTR.MEETING_TYP_REF_ID
WHERE P.SRC_SYSTEM_VAL_TXT = @SRC_SYSTEM_VAL_TEXT
	AND MTR.MEETING_TYP_DESC = @MEETING_TYP_DESC

RETURN
GO

