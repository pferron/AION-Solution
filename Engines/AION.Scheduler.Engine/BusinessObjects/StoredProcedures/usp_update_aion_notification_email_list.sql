
/***********************************************************************************************************************
* Object:       usp_update_aion_notification_email_list
* Description:  Updates NotificationEmailList record using supplied parameters.
* Parameters:   
*               @NOTIFICATION_EMAIL_LIST_ID                                  int
*               @PROJECT_EMAIL_NOTIFICATION_ID                               int
*               @USER_ID                                                     int
*               @EMAIL_ADDR_TXT                                              varchar(100)
*               @UPDATED_DTTM                                                datetime
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      11/12/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 11/12/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_update_aion_notification_email_list] @NOTIFICATION_EMAIL_LIST_ID    INT, 
                                                           @PROJECT_EMAIL_NOTIFICATION_ID INT, 
                                                           @USER_ID                       INT, 
                                                           @EMAIL_ADDR_TXT                VARCHAR(100), 
                                                           @UPDATED_DTTM                  DATETIME, 
                                                           @WKR_ID_TXT                    VARCHAR(100), 
                                                           @ReturnValue                   INT OUTPUT
AS
     DECLARE @error INT;
     UPDATE NOTIFICATION_EMAIL_LIST
       SET 
           PROJECT_EMAIL_NOTIFICATION_ID = @PROJECT_EMAIL_NOTIFICATION_ID, 
           USER_ID = @USER_ID, 
           EMAIL_ADDR_TXT = @EMAIL_ADDR_TXT, 
           WKR_ID_UPDATED_TXT = @WKR_ID_TXT, 
           UPDATED_DTTM = GETDATE()
     WHERE NOTIFICATION_EMAIL_LIST_ID = @NOTIFICATION_EMAIL_LIST_ID
           AND ISNULL(CONVERT(VARCHAR(19), UPDATED_DTTM, 120), '') = ISNULL(CONVERT(VARCHAR(19), @UPDATED_DTTM, 120), '');
     SELECT @error = @@ERROR, 
            @ReturnValue = @@ROWCOUNT;
     IF @error != 0
         RAISERROR('Error updating NotificationEmailList record.', 18, 1);
     IF @ReturnValue = 0
         RAISERROR('Data was changed/deleted prior to update.', 18, 100);
     RETURN;
GO