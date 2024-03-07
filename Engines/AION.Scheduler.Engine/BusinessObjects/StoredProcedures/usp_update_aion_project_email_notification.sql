
/***********************************************************************************************************************
* Object:       usp_update_aion_project_email_notification
* Description:  Updates ProjectEmailNotification record using supplied parameters.
* Parameters:   
*               @PROJECT_EMAIL_NOTIFICATION_ID                               int
*               @PROJECT_ID                                                  int
*               @EMAIL_TYP_DESC                                              varchar(100)
*               @EMAIL_SUBJECT_TXT                                           varchar(255)
*               @EMAIL_BODY_TXT                                              varchar(MAX)
*               @EMAIL_SENT_DT                                               datetime
*               @SENDER_USER_ID                                              int
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

CREATE PROCEDURE [usp_update_aion_project_email_notification] @PROJECT_EMAIL_NOTIFICATION_ID INT, 
                                                              @PROJECT_ID                    INT, 
                                                              @EMAIL_TYP_DESC                VARCHAR(100), 
                                                              @EMAIL_SUBJECT_TXT             VARCHAR(255), 
                                                              @EMAIL_BODY_TXT                VARCHAR(MAX), 
                                                              @EMAIL_SENT_DT                 DATETIME, 
                                                              @SENDER_USER_ID                INT, 
                                                              @UPDATED_DTTM                  DATETIME, 
                                                              @WKR_ID_TXT                    VARCHAR(100), 
                                                              @ReturnValue                   INT OUTPUT
AS
     DECLARE @error INT;
     UPDATE PROJECT_EMAIL_NOTIFICATION
       SET 
           PROJECT_ID = @PROJECT_ID, 
           EMAIL_TYP_DESC = @EMAIL_TYP_DESC, 
           EMAIL_SUBJECT_TXT = @EMAIL_SUBJECT_TXT, 
           EMAIL_BODY_TXT = @EMAIL_BODY_TXT, 
           EMAIL_SENT_DT = @EMAIL_SENT_DT, 
           SENDER_USER_ID = @SENDER_USER_ID, 
           WKR_ID_UPDATED_TXT = @WKR_ID_TXT, 
           UPDATED_DTTM = GETDATE()
     WHERE PROJECT_EMAIL_NOTIFICATION_ID = @PROJECT_EMAIL_NOTIFICATION_ID
           AND ISNULL(CONVERT(VARCHAR(19), UPDATED_DTTM, 120), '') = ISNULL(CONVERT(VARCHAR(19), @UPDATED_DTTM, 120), '');
     SELECT @error = @@ERROR, 
            @ReturnValue = @@ROWCOUNT;
     IF @error != 0
         RAISERROR('Error updating ProjectEmailNotification record.', 18, 1);
     IF @ReturnValue = 0
         RAISERROR('Data was changed/deleted prior to update.', 18, 100);
     RETURN;
GO