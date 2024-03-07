/****** Object:  StoredProcedure [AION].[usp_select_aion_facilitator_meeting_appointment_get_by_project_id]    Script Date: 10/9/2020 10:23:22 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:       usp_select_aion_facilitator_meeting_appointment_get_by_project_id
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
* 10/7/2020    AION_user     Auto-generated
* 04/10/2021   jallen      add proposed dates
* 04/29/2021   jallen		add additional fields
* 07/29/2021   jallen      add new field for EXTERNAL_ATTENDEES_CNT
***********************************************************************************************************************/
CREATE PROCEDURE [AION].[usp_select_aion_facilitator_meeting_appointment_get_by_project_id] @projectId INT
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
		  , PROPOSED_1_DT
		  , PROPOSED_2_DT
		  , PROPOSED_3_DT
		  , APPT_CANCELLATION_REF_ID
		  , CANCEL_AFTER_DT
          , EXTERNAL_ATTENDEES_CNT
FROM FACILITATOR_MEETING_APPOINTMENT
WHERE
	-- @TODO:  Correct the following as necessary
	PROJECT_ID = @projectId

RETURN