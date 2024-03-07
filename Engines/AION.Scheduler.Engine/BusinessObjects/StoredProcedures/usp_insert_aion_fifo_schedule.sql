/****** Object:  StoredProcedure [AION].[usp_insert_aion_fifo_schedule]    Script Date: 6/1/2021 7:57:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:	usp_insert_aion_plan_review_schedule
* Description:	Inserts PlanReviewSchedule record.
* Parameters:
*		@PROJECT_ID                                                  int
*		@BUSINESS_REF_ID                                             int
*		@START_DT                                                    datetime
*		@END_DT                                                      datetime
*		@POOL_REQUEST_IND                                            bit
*		@FIFO_REQUEST_IND                                            bit
*		@APPT_RESPONSE_STATUS_REF_ID                                 int
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      8/12/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 8/12/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_insert_aion_fifo_schedule]
    @PROJECT_ID                                                  int
  , @BUSINESS_REF_ID                                             int
  , @APPT_RESPONSE_STATUS_REF_ID                                 int
  , @CYCLE_NBR                                                   int
  , @FIFO_SAME_BUILD_CONTR_IND                                   bit
  , @FIFO_MANUAL_ASSIGNMENT_IND                                  bit
  , @PLAN_REVIEW_START_DT                                        datetime
  , @START_DT                                                    datetime
  , @END_DT                                                      datetime
  , @PLANS_READY_ON_DT                                           datetime
  , @GATE_DT                                                     datetime
  , @ASSIGNED_PLAN_REVIEWER_ID                                   int
  , @ASSIGNED_HOURS_NBR                                          decimal(9,2)
  , @APPT_CANCELLATION_REF_ID                                    int
  , @WKR_ID_TXT                                                  varchar(100)
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO FIFO_SCHEDULE
          (
            PROJECT_ID
          , BUSINESS_REF_ID
          , APPT_RESPONSE_STATUS_REF_ID
          , CYCLE_NBR
          , FIFO_SAME_BUILD_CONTR_IND
          , FIFO_MANUAL_ASSIGNMENT_IND
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , PLAN_REVIEW_START_DT
          , START_DT
          , END_DT
          , PLANS_READY_ON_DT
          , GATE_DT
          , ASSIGNED_PLAN_REVIEWER_ID
          , ASSIGNED_HOURS_NBR
          , APPT_CANCELLATION_REF_ID
          )
     VALUES
          (
            @PROJECT_ID
          , @BUSINESS_REF_ID
          , @APPT_RESPONSE_STATUS_REF_ID
          , @CYCLE_NBR
          , @FIFO_SAME_BUILD_CONTR_IND
          , @FIFO_MANUAL_ASSIGNMENT_IND
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          , @PLAN_REVIEW_START_DT
          , @START_DT
          , @END_DT
          , @PLANS_READY_ON_DT
          , @GATE_DT
          , @ASSIGNED_PLAN_REVIEWER_ID
          , @ASSIGNED_HOURS_NBR
          , @APPT_CANCELLATION_REF_ID
          )


     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding  record.', 18,1)

RETURN
