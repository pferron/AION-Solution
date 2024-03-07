/****** Object:  StoredProcedure [AION].[usp_update_aion_fifo_schedule]    Script Date: 6/1/2021 8:03:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:       usp_update_aion_fifo_schedule
* Description:  Updates FIFOSchedule record using supplied parameters.
* Parameters:   
*               @FIFO_SCHEDULE_ID                                     int
  , @PROJECT_ID                                                  int
  , @BUSINESS_REF_ID                                             int
  , @DUE_DT                                                      datetime 
  , @APPT_RESPONSE_STATUS_REF_ID                                 int
  , @CYCLE_NBR                                                   int
   ,@FIFO_SAME_BUILDING_CONTRACTOR_IND                           bit
  , @FIFO_MANUAL_ASSIGNMENT_IND                                  bit
  , @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      8/12/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 8/12/2020    AION_user     Auto-generated
* 7/7/2021     jallen        Account for incoming nullable dates
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_update_aion_fifo_schedule]
    @FIFO_SCHEDULE_ID                                            int
  , @PROJECT_ID                                                  int
  , @BUSINESS_REF_ID                                             int
  , @APPT_RESPONSE_STATUS_REF_ID                                 int
  , @CYCLE_NBR                                                   int
  , @FIFO_SAME_BUILD_CONTR_IND                                   bit
  , @FIFO_MANUAL_ASSIGNMENT_IND                                  bit
  , @UPDATED_DTTM                                                datetime
  , @PLAN_REVIEW_START_DT                                        datetime = null
  , @START_DT                                                    datetime
  , @END_DT                                                      datetime
  , @PLANS_READY_ON_DT                                           datetime = null
  , @GATE_DT                                                     datetime = null
  , @ASSIGNED_PLAN_REVIEWER_ID                                   int
  , @ASSIGNED_HOURS_NBR                                          decimal(9,2)
  , @APPT_CANCELLATION_REF_ID                                    int = null
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE FIFO_SCHEDULE
       SET
            PROJECT_ID                                                   = @PROJECT_ID
          , BUSINESS_REF_ID                                              = @BUSINESS_REF_ID
          , APPT_RESPONSE_STATUS_REF_ID                                  = @APPT_RESPONSE_STATUS_REF_ID
          , CYCLE_NBR                                                    = @CYCLE_NBR
          , FIFO_SAME_BUILD_CONTR_IND                                    = @FIFO_SAME_BUILD_CONTR_IND
          , FIFO_MANUAL_ASSIGNMENT_IND                                   = @FIFO_MANUAL_ASSIGNMENT_IND
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()
          , PLAN_REVIEW_START_DT                                         = ISNULL(@PLAN_REVIEW_START_DT,@START_DT)
          , START_DT                                                     = @START_DT
          , END_DT                                                       = @END_DT
          , PLANS_READY_ON_DT                                            = ISNULL(@PLANS_READY_ON_DT,PLANS_READY_ON_DT)
          , GATE_DT                                                      = ISNULL(@GATE_DT,GATE_DT)
          , ASSIGNED_PLAN_REVIEWER_ID                                    = @ASSIGNED_PLAN_REVIEWER_ID
          , ASSIGNED_HOURS_NBR                                           = @ASSIGNED_HOURS_NBR
          , APPT_CANCELLATION_REF_ID                                     = ISNULL(@APPT_CANCELLATION_REF_ID,APPT_CANCELLATION_REF_ID)

       WHERE
          FIFO_SCHEDULE_ID                                               = @FIFO_SCHEDULE_ID               

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating  record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN

