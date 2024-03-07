
/***********************************************************************************************************************
* Object:       usp_select_aion_facilitator_meeting_appointment_get_by_id
* Description:  Retrieves FacilitatorMeetingAppointment record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   AION_user
* Created:      10/7/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/07/2020   AION_user    Auto-generated
* 04/10/2021   jallen      add proposed dates
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_facilitator_meeting_appointment_get_by_id]

    @identity                                                    int

AS

       SELECT 
            FACILITATOR_MEETING_APPT_IDENTIFIER
          , PROJECT_ID
          , MEETING_ROOM_REF_ID
          , APPT_RESPONSE_STATUS_REF_ID
          , FROM_DT
          , TO_DT
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , VIRTUAL_MEETING_IND
          , MEETING_TYP_REF_ID
		  , APPT_CANCELLATION_REF_ID
		  , CANCEL_AFTER_DT
          , PROPOSED_1_DT
		  , PROPOSED_2_DT
		  , PROPOSED_3_DT
          , EXTERNAL_ATTENDEES_CNT

       FROM FACILITATOR_MEETING_APPOINTMENT

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          FACILITATOR_MEETING_APPT_IDENTIFIER = @identity
          

RETURN

GO