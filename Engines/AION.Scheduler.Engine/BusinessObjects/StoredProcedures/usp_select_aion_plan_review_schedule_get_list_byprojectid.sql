/****** Object:  StoredProcedure [AION].[usp_select_aion_plan_review_schedule_get_list_byprojectid]    Script Date: 9/13/2021 9:15:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:       usp_select_aion_plan_review_schedule_get_list_byprojectid
* Description:  Retrieves PlanReviewSchedule list for given parameter(s).
* Parameters:   
*               @identity                                                   int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      8/12/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 8/12/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_select_aion_plan_review_schedule_get_list_byprojectid]

    @project_id                                                   int

AS

       SELECT 
            PLAN_REVIEW_SCHEDULE_ID
          , PROJECT_ID
          , BUSINESS_REF_ID
          , START_DT
          , END_DT
          , POOL_REQUEST_IND
          , FIFO_REQUEST_IND
          , APPT_RESPONSE_STATUS_REF_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
		  , PLAN_REVIEW_PROJECT_DETAILS_ID
		  , CYCLE_NBR
		  , PLANS_READY_ON_DT
		  , REQUEST_EXPRESS_NEXT_CYCLE_IND
		  , IS_FUTURE_CYCLE_IND
		  , SCHEDULE_AFTER_DT
		  , IS_RESCHEDULE_IND
		  , GATE_DT

       FROM PLAN_REVIEW_SCHEDULE

       WHERE
        
       
          PROJECT_ID = @project_id
          

RETURN

