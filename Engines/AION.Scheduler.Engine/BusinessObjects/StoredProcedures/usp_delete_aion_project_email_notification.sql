
/***********************************************************************************************************************
* Object:       usp_delete_aion_project_email_notification
* Description:  Deletes ProjectEmailNotification record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      11/12/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 11/12/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_delete_aion_project_email_notification] @identity    INT, 
                                                              @ReturnValue INT OUTPUT
AS
     DECLARE @error INT;
     DELETE FROM PROJECT_EMAIL_NOTIFICATION
     WHERE

     -- @TODO:  Correct the following as necessary

     PROJECT_EMAIL_NOTIFICATION_ID = @identity;
     SELECT @error = @@ERROR, 
            @ReturnValue = @@ROWCOUNT;
     IF @error != 0
         RAISERROR('Error deleting ProjectEmailNotification record.', 18, 1);
     RETURN;
GO