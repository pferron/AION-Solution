
/***********************************************************************************************************************
* Object:       usp_delete_aion_notification_email_list
* Description:  Deletes NotificationEmailList record for given key field(s).
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

CREATE PROCEDURE [usp_delete_aion_notification_email_list] @identity    INT, 
                                                           @ReturnValue INT OUTPUT
AS
     DECLARE @error INT;
     DELETE FROM NOTIFICATION_EMAIL_LIST
     WHERE

     -- @TODO:  Correct the following as necessary

     NOTIFICATION_EMAIL_LIST_ID = @identity;
     SELECT @error = @@ERROR, 
            @ReturnValue = @@ROWCOUNT;
     IF @error != 0
         RAISERROR('Error deleting NotificationEmailList record.', 18, 1);
     RETURN;
GO