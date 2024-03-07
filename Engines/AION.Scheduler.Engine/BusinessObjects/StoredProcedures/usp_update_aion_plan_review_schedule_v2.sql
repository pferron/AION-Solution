/****** Object:  StoredProcedure [AION].[usp_update_aion_plan_review_schedule]    Script Date: 9/13/2021 9:18:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:       usp_update_aion_plan_review_schedule
* Description:  Updates PlanReviewSchedule record using supplied parameters.
* Parameters:   
*    @PLAN_REVIEW_SCHEDULE_ID                                     int
*  , @PROJECT_ID                                                  int
*  , @BUSINESS_REF_ID                                             int
*  , @START_DT                                                    datetime
*  , @END_DT                                                      datetime
*  , @POOL_REQUEST_IND                                            bit
*  , @FIFO_REQUEST_IND                                            bit
*  , @APPT_RESPONSE_STATUS_REF_ID                                 int
*  , @UPDATED_DTTM                                                datetime
*  , @WKR_ID_TXT                                                  varchar(100)
*  , @PLAN_REVIEW_PROJECT_DETAILS_ID                              int
*  , @CYCLE_NBR                                                   int
*  , @PROD_DT													 datetime
*  , @IS_FUTURE_CYCLE_IND                                         bit
*  , @SCHEDULE_AFTER_DT                                           datetime
*  , @IS_RESCHEDULE_IND                                           bit
*  , @GATE_DT                                                     datetime
*  , @IS_CURRENT_CYCLE_IND                                        bit
*  , @REREVIEW_HOURS_NBR                                          decimal(15,7)
*  , @PROPOSED_HOURS_NBR                                          decimal(15,7)
*  , @PROPOSED_PLAN_REVIEWER_ID							          int = null
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      8/12/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 8/12/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_update_aion_plan_review_schedule]

    @PLAN_REVIEW_SCHEDULE_ID                                     int
  , @PROJECT_ID                                                  int
  , @BUSINESS_REF_ID                                             int
  , @START_DT                                                    datetime
  , @END_DT                                                      datetime
  , @POOL_REQUEST_IND                                            bit
  , @FIFO_REQUEST_IND                                            bit
  , @APPT_RESPONSE_STATUS_REF_ID                                 int
  , @UPDATED_DTTM                                                datetime
  , @WKR_ID_TXT                                                  varchar(100)
  , @PLAN_REVIEW_PROJECT_DETAILS_ID                              int
  , @CYCLE_NBR                                                   int
  , @PROD_DT													 datetime
  , @IS_FUTURE_CYCLE_IND                                         bit
  , @SCHEDULE_AFTER_DT                                           datetime
  , @IS_RESCHEDULE_IND                                           bit
  , @GATE_DT                                                     datetime
  , @IS_CURRENT_CYCLE_IND                                        bit
  , @REREVIEW_HOURS_NBR                                          decimal(15,7)
  , @PROPOSED_HOURS_NBR                                          decimal(15,7)
  , @PROPOSED_PLAN_REVIEWER_ID							          int = null

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE PLAN_REVIEW_SCHEDULE
       SET
            PROJECT_ID                                                   = @PROJECT_ID
          , BUSINESS_REF_ID                                              = @BUSINESS_REF_ID
          , START_DT                                                     = @START_DT
          , END_DT                                                       = @END_DT
          , POOL_REQUEST_IND                                             = @POOL_REQUEST_IND
          , FIFO_REQUEST_IND                                             = @FIFO_REQUEST_IND
          , APPT_RESPONSE_STATUS_REF_ID                                  = @APPT_RESPONSE_STATUS_REF_ID
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()
		  , PLAN_REVIEW_PROJECT_DETAILS_ID                               = @PLAN_REVIEW_PROJECT_DETAILS_ID
		  , CYCLE_NBR                                                    = @CYCLE_NBR
		  , PLANS_READY_ON_DT											 = @PROD_DT
		  , IS_FUTURE_CYCLE_IND                                          = @IS_FUTURE_CYCLE_IND
		  , SCHEDULE_AFTER_DT                                            = @SCHEDULE_AFTER_DT
		  , IS_RESCHEDULE_IND                                            = @IS_RESCHEDULE_IND
		  , GATE_DT                                                      = @GATE_DT
          , IS_CURRENT_CYCLE_IND                                         = @IS_CURRENT_CYCLE_IND
          , REREVIEW_HOURS_NBR                                           = @REREVIEW_HOURS_NBR
          , PROPOSED_HOURS_NBR                                           = @PROPOSED_HOURS_NBR
          , PROPOSED_PLAN_REVIEWER_ID                                    = @PROPOSED_PLAN_REVIEWER_ID
                                                       
		  
       WHERE
          PLAN_REVIEW_SCHEDULE_ID                                        = @PLAN_REVIEW_SCHEDULE_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating PlanReviewSchedule record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
