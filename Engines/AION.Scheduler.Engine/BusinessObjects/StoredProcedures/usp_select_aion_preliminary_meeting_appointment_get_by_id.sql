/***********************************************************************************************************************          
* Object:       usp_select_aion_preliminary_meeting_appointment_get_by_id          
* Description:  preliminary meeting by id          
* Parameters:             
*               @identity                                            int       
*          
* Returns:      preliminary meeting by id       
* Comments:             
* Version:      1.0          
* Created by:             
* Created:                
************************************************************************************************************************          
* Change History: Date, Name, Description 
* 8/6/2020    AION_user     Auto-generated
* 01/06/2022    jlindsay    add reschedule      
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_select_aion_preliminary_meeting_appointment_get_by_id] @identity INT
AS
BEGIN
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
	FROM AION.PRELIMINARY_MEETING_APPOINTMENT
	WHERE PRELIMINARY_MEETING_APPT_ID = @identity;

	RETURN
END
