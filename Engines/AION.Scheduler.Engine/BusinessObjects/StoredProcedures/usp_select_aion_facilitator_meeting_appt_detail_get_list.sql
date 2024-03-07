
/***********************************************************************************************************************
* Object:       usp_select_aion_facilitator_meeting_appt_detail_get_list
* Description:  Retrieves FacilitatorMeetingApptDetail list for given parameter(s).
* Parameters:   
*               @identity                                                   int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      4/21/2022
************************************************************************************************************************
* Change History: Date, Name, Description
* 4/21/2022    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_facilitator_meeting_appt_detail_get_list]

    @identity                                                   int

AS

       SELECT 
            FACILITATOR_MEETING_APPT_DETAIL_ID
          , FACILITATOR_MEETING_APPT_IDENTIFIER
          , BUSINESS_REF_ID
          , ASSIGNED_PLAN_REVIEWER_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM

       FROM FACILITATOR_MEETING_APPOINTMENT_DETAIL

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          FACILITATOR_MEETING_APPT_DETAIL_ID = @identity
          

RETURN

GO