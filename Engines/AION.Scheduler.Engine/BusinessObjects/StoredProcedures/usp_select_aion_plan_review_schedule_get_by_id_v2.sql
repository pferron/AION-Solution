/****** Object:  StoredProcedure [AION].[usp_select_aion_plan_review_schedule_get_by_id]    Script Date: 9/13/2021 8:57:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
/***********************************************************************************************************************  
* Object:       usp_select_aion_plan_review_schedule_get_by_id  
* Description:  Retrieves PlanReviewSchedule record for given key field(s).  
* Parameters:     
*               @identity                                                    int  
*  
* Returns:      Recordset.  
* Comments:     Developer may need to manually join to other tables, such as code tables,  
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.  
* Version:      1.0  
* Created by:   AION_user  
* Created:      8/12/2020  
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 8/12/2020    AION_user     Auto-generated  
* 9/21/2020 jlindsay    add CYCLE_NBR  
***********************************************************************************************************************/  
  
ALTER PROCEDURE [AION].[usp_select_aion_plan_review_schedule_get_by_id_v2]  
  
    @identity                                                    int  
  
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
          , IS_CURRENT_CYCLE_IND
          , REREVIEW_HOURS_NBR
          , PROPOSED_PLAN_REVIEWER_ID
          , PROPOSED_HOURS_NBR
  
       FROM PLAN_REVIEW_SCHEDULE  
  
       WHERE  
          
       -- @TODO:  Correct the following as necessary  
          
          PLAN_REVIEW_SCHEDULE_ID = @identity  
            
  
RETURN  
  