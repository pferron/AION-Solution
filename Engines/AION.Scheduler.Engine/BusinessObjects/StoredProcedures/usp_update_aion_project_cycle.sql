
/***********************************************************************************************************************
* Object:       usp_update_aion_project_cycle
* Description:  Updates ProjectCycle record using supplied parameters.
* Parameters:   
*               @PROJECT_CYCLE_ID                                            int
*               @PROJECT_ID                                                  int
*               @CURRENT_CYCLE_IND                                           bit
*               @FUTURE_CYCLE_IND                                            bit
*               @UPDATED_DTTM                                                datetime
*               @CYCLE_NBR                                                   int
*               @PLANS_READY_ON_DT                                           datetime
*               @IS_COMPLETE_IND                                             bit
*               @GATE_DT                                                     datetime
*               @SCHEDULE_AFTER_DT                                           datetime
*               @RESPONDER_USER_ID                                           int
*               @IS_APRV_IND                                                 bit
*               @RESPONSE_DT                                                 datetime
*               @WKR_ID_TXT                                                  varchar(100)
*               @INCREMENT_ON_PLANS_RECEIVED_IND                             bit
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      10/11/2021
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/11/2021    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_update_aion_project_cycle]

    @PROJECT_CYCLE_ID                                            int
  , @PROJECT_ID                                                  int
  , @CURRENT_CYCLE_IND                                           bit
  , @FUTURE_CYCLE_IND                                            bit
  , @UPDATED_DTTM                                                datetime
  , @CYCLE_NBR                                                   int
  , @PLANS_READY_ON_DT                                           datetime
  , @IS_COMPLETE_IND                                             bit
  , @GATE_DT                                                     datetime
  , @SCHEDULE_AFTER_DT                                           datetime
  , @RESPONDER_USER_ID                                           int
  , @IS_APRV_IND                                                 bit
  , @RESPONSE_DT                                                 datetime
  , @WKR_ID_TXT                                                  varchar(100)
  , @INCREMENT_ON_PLANS_RECEIVED_IND                             bit

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE PROJECT_CYCLE
       SET
            PROJECT_ID                                                   = @PROJECT_ID
          , CURRENT_CYCLE_IND                                            = @CURRENT_CYCLE_IND
          , FUTURE_CYCLE_IND                                             = @FUTURE_CYCLE_IND
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()
          , CYCLE_NBR                                                    = @CYCLE_NBR
          , PLANS_READY_ON_DT                                            = @PLANS_READY_ON_DT
          , IS_COMPLETE_IND                                              = @IS_COMPLETE_IND
          , GATE_DT                                                      = @GATE_DT
          , SCHEDULE_AFTER_DT                                            = @SCHEDULE_AFTER_DT
          , RESPONDER_USER_ID                                            = @RESPONDER_USER_ID
          , IS_APRV_IND                                                  = @IS_APRV_IND
          , RESPONSE_DT                                                  = @RESPONSE_DT
          , INCREMENT_ON_PLANS_RECEIVED_IND                              = @INCREMENT_ON_PLANS_RECEIVED_IND

       WHERE
          PROJECT_CYCLE_ID                                               = @PROJECT_CYCLE_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating ProjectCycle record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO