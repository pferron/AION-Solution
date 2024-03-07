
/***********************************************************************************************************************
* Object:	usp_insert_aion_project_cycle
* Description:	Inserts ProjectCycle record.
* Parameters:
*		@PROJECT_ID                                                  int
*		@CURRENT_CYCLE_IND                                           bit
*		@FUTURE_CYCLE_IND                                            bit
*		@CYCLE_NBR                                                   int
*		@PLANS_READY_ON_DT                                           datetime
*		@IS_COMPLETE_IND                                             bit
*		@GATE_DT                                                     datetime
*		@SCHEDULE_AFTER_DT                                           datetime
*		@RESPONDER_USER_ID                                           int
*		@IS_APRV_IND                                                 bit
*		@RESPONSE_DT                                                 datetime
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      10/11/2021
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/11/2021    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_insert_aion_project_cycle]
    @PROJECT_ID                                                  int
  , @CURRENT_CYCLE_IND                                           bit
  , @FUTURE_CYCLE_IND                                            bit
  , @CYCLE_NBR                                                   int
  , @PLANS_READY_ON_DT                                           datetime
  , @IS_COMPLETE_IND                                             bit
  , @GATE_DT                                                     datetime
  , @SCHEDULE_AFTER_DT                                           datetime
  , @RESPONDER_USER_ID                                           int
  , @IS_APRV_IND                                                 bit
  , @RESPONSE_DT                                                 datetime
  , @WKR_ID_TXT                                                  varchar(100)
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO PROJECT_CYCLE
          (
            PROJECT_ID
          , CURRENT_CYCLE_IND
          , FUTURE_CYCLE_IND
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , CYCLE_NBR
          , PLANS_READY_ON_DT
          , IS_COMPLETE_IND
          , GATE_DT
          , SCHEDULE_AFTER_DT
          , RESPONDER_USER_ID
          , IS_APRV_IND
          , RESPONSE_DT
          )
     VALUES
          (
            @PROJECT_ID
          , @CURRENT_CYCLE_IND
          , @FUTURE_CYCLE_IND
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          , @CYCLE_NBR
          , @PLANS_READY_ON_DT
          , @IS_COMPLETE_IND
          , @GATE_DT
          , @SCHEDULE_AFTER_DT
          , @RESPONDER_USER_ID
          , @IS_APRV_IND
          , @RESPONSE_DT
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding ProjectCycle record.', 18,1)

RETURN
GO