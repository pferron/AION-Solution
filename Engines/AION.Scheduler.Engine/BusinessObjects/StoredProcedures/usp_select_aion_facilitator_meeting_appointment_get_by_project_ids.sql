/****** Object:  StoredProcedure [AION].[usp_select_aion_facilitator_meeting_appointment_get_by_project_id]    Script Date: 4/20/2021 8:57:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:       usp_select_aion_facilitator_meeting_appointment_get_by_project_ids
* Description:  Retrieves FacilitatorMeetingAppointment record for given set of project ids.
* Parameters:   
*               @projectIds                                                    string
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   jallen
* Created:      04/20/2021 
************************************************************************************************************************
* Change History: Date, Name, Description
* 04/20/2021    jallen     Created
* 07/29/2021   jallen      add new field for EXTERNAL_ATTENDEES_CNT
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_select_aion_facilitator_meeting_appointment_get_by_project_ids]

    @projectIds       varchar(MAX)

AS

	  CREATE TABLE #ProjectIds (
			ProjectId varchar(255)
		);


	  INSERT INTO #ProjectIds select value from String_Split(@projectIds, ',')

       SELECT 
            FMA.FACILITATOR_MEETING_APPT_IDENTIFIER
          , FMA.PROJECT_ID
          , FMA.MEETING_ROOM_REF_ID
          , FMA.APPT_RESPONSE_STATUS_REF_ID
          , FMA.FROM_DT
          , FMA.TO_DT
          , FMA.WKR_ID_CREATED_TXT
          , FMA.CREATED_DTTM
          , FMA.WKR_ID_UPDATED_TXT
          , FMA.UPDATED_DTTM
          , FMA.VIRTUAL_MEETING_IND
          , FMA.MEETING_TYP_REF_ID
		  , FMA.APPT_CANCELLATION_REF_ID
		  , FMA.CANCEL_AFTER_DT
		  , FMA.PROPOSED_1_DT
		  , FMA.PROPOSED_2_DT
		  , FMA.PROPOSED_3_DT
          , FMA.EXTERNAL_ATTENDEES_CNT

       FROM FACILITATOR_MEETING_APPOINTMENT FMA
	   INNER JOIN PROJECT P ON FMA.PROJECT_ID = P.PROJECT_ID

       WHERE
        
       P.SRC_SYSTEM_VAL_TXT in (select ProjectId from #ProjectIds)
          

RETURN

