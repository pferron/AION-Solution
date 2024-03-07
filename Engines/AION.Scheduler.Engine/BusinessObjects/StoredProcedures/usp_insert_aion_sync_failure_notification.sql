
/***********************************************************************************************************************
* Object:	usp_insert_aion_sync_failure_notification
* Description:	Inserts SyncFailureNotification record.
* Parameters:
*		@LAST_FAILURE_NOTIFICATION_DT                                datetime
*
* Returns:      N/A
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      11/13/2023
************************************************************************************************************************
* Change History: Date, Name, Description
* 11/13/2023    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_insert_aion_sync_failure_notification]
    @LAST_FAILURE_NOTIFICATION_DT                                datetime
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO SYNC_FAILURE_NOTIFICATION
          (
            LAST_FAILURE_NOTIFICATION_DT
          )
     VALUES
          (
            @LAST_FAILURE_NOTIFICATION_DT
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()
     IF @error != 0
          RAISERROR('Error adding SyncFailureNotification record.', 18,1)

RETURN
GO