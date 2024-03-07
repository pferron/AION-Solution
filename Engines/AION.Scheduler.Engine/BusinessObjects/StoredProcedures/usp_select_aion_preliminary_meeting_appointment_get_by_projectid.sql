/***********************************************************************************************************************    
* Object:       usp_select_aion_preliminary_meeting_appointment_get_by_projectid    
* Description:  Retrieves PreliminaryMeetingAppointment record for given key field(s).    
* Parameters:       
*               @identity                                                    int    
*    
* Returns:      Recordset.    
* Comments:     Developer may need to manually join to other tables, such as code tables,    
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.    
* Version:      1.0    
* Created by:   jlindsay    
* Created:      7/8/2020   
************************************************************************************************************************    
* Change History: Date, Name, Description    
* 7/8/2020    jlindsay     Auto-generated    
* 01/06/2022    jlindsay    add reschedule    
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_select_aion_preliminary_meeting_appointment_get_by_projectid] @projectid INT
AS
SELECT PRELIMINARY_MEETING_APPT_ID,
	FROM_DT,
	TO_DT,
	MEETING_ROOM_REF_ID,
	APPT_RESPONSE_STATUS_REF_ID,
	WKR_ID_CREATED_TXT,
	CREATED_DTTM,
	WKR_ID_UPDATED_TXT,
	UPDATED_DTTM,
	PROJECT_ID,
	APPENDIX_AGENDA_DUE_DT,
	PROPOSED_1_DT,
	PROPOSED_2_DT,
	PROPOSED_3_DT,
	APPT_CANCELLATION_REF_ID,
	CANCEL_AFTER_DT,
	RESCHEDULE_IND
FROM [AION].PRELIMINARY_MEETING_APPOINTMENT
WHERE PROJECT_ID = @projectid;

RETURN;
