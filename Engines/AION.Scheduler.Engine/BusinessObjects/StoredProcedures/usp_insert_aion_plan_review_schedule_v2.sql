SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:	usp_insert_aion_plan_review_schedule
* Description:	Inserts PlanReviewSchedule record.
* Parameters:
*    @PROJECT_ID                                                  int
*  , @BUSINESS_REF_ID                                             int
*  , @START_DT                                                    datetime
*  , @END_DT                                                      datetime
*  , @POOL_REQUEST_IND                                            bit
*  , @FIFO_REQUEST_IND                                            bit
*  , @APPT_RESPONSE_STATUS_REF_ID                                 int
*  , @WKR_ID_TXT                                                  varchar(100)
*  , @PLAN_REVIEW_PROJECT_DETAILS_ID                              int
*  , @CYCLE_NBR                                                   int
*  , @PLANS_READY_ON_DT                                           datetime
*  , @REQUEST_EXPRESS_NEXT_CYCLE_IND                              bit
*  , @IS_FUTURE_CYCLE_IND                                         bit
*  , @SCHEDULE_AFTER_DT                                           datetime
*  , @IS_RESCHEDULE_IND                                           bit
*  , @GATE_DT                                                     datetime
*  , @IS_CURRENT_CYCLE_IND                                        bit
*  , @REREVIEW_HOURS_NBR                                          decimal(15,7)
*  , @PROPOSED_HOURS_NBR                                          decimal(15,7)
*  , @PROPOSED_PLAN_REVIEWER_ID							          int = null
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      8/12/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 9/13/2021    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [AION].[usp_insert_aion_plan_review_schedule_v2]
    @PROJECT_ID                                                  int
  , @BUSINESS_REF_ID                                             int
  , @START_DT                                                    datetime
  , @END_DT                                                      datetime
  , @POOL_REQUEST_IND                                            bit
  , @FIFO_REQUEST_IND                                            bit
  , @APPT_RESPONSE_STATUS_REF_ID                                 int
  , @WKR_ID_TXT                                                  varchar(100)
  , @PLAN_REVIEW_PROJECT_DETAILS_ID                              int
  , @CYCLE_NBR                                                   int
  , @PLANS_READY_ON_DT                                           datetime
  , @REQUEST_EXPRESS_NEXT_CYCLE_IND                              bit
  , @IS_FUTURE_CYCLE_IND                                         bit
  , @SCHEDULE_AFTER_DT                                           datetime
  , @IS_RESCHEDULE_IND                                           bit
  , @GATE_DT                                                     datetime
  , @IS_CURRENT_CYCLE_IND                                        bit
  , @REREVIEW_HOURS_NBR                                          decimal(15,7)
  , @PROPOSED_HOURS_NBR                                          decimal(15,7)
  , @PROPOSED_PLAN_REVIEWER_ID							         int = null
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO PLAN_REVIEW_SCHEDULE
          (
            PROJECT_ID
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
		  , PROPOSED_HOURS_NBR
 		  , PROPOSED_PLAN_REVIEWER_ID
          )
     VALUES
          (
            @PROJECT_ID
          , @BUSINESS_REF_ID
          , @START_DT
          , @END_DT
          , @POOL_REQUEST_IND
          , @FIFO_REQUEST_IND
          , @APPT_RESPONSE_STATUS_REF_ID
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
		  , @PLAN_REVIEW_PROJECT_DETAILS_ID
		  , @CYCLE_NBR
		  , @PLANS_READY_ON_DT
		  , @REQUEST_EXPRESS_NEXT_CYCLE_IND
		  , @IS_FUTURE_CYCLE_IND
		  , @SCHEDULE_AFTER_DT
		  , @IS_RESCHEDULE_IND
		  , @GATE_DT
          , @IS_CURRENT_CYCLE_IND
          , @REREVIEW_HOURS_NBR
		  , @PROPOSED_HOURS_NBR
 		  , @PROPOSED_PLAN_REVIEWER_ID

          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding PlanReviewSchedule record.', 18,1)

RETURN