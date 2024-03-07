
/***********************************************************************************************************************
* Object:	usp_insert_aion_project_email_notification
* Description:	Inserts ProjectEmailNotification record.
* Parameters:
*		@PROJECT_ID                                                  int
*		@EMAIL_TYP_DESC                                              varchar(100)
*		@EMAIL_SUBJECT_TXT                                           varchar(255)
*		@EMAIL_BODY_TXT                                              varchar(0)
*		@EMAIL_SENT_DT                                               datetime
*		@SENDER_USER_ID                                              int
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      11/12/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 11/12/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_insert_aion_project_email_notification] @PROJECT_ID        INT, 
                                                              @EMAIL_TYP_DESC    VARCHAR(100), 
                                                              @EMAIL_SUBJECT_TXT VARCHAR(255), 
                                                              @EMAIL_BODY_TXT    VARCHAR(MAX), 
                                                              @EMAIL_SENT_DT     DATETIME, 
                                                              @SENDER_USER_ID    INT, 
                                                              @WKR_ID_TXT        VARCHAR(100), 
                                                              @ReturnValue       INT OUTPUT
AS
     DECLARE @error INT;
     INSERT INTO PROJECT_EMAIL_NOTIFICATION
     (PROJECT_ID, 
      EMAIL_TYP_DESC, 
      EMAIL_SUBJECT_TXT, 
      EMAIL_BODY_TXT, 
      EMAIL_SENT_DT, 
      SENDER_USER_ID, 
      WKR_ID_CREATED_TXT, 
      CREATED_DTTM, 
      WKR_ID_UPDATED_TXT, 
      UPDATED_DTTM
     )
     VALUES
     (@PROJECT_ID, 
      @EMAIL_TYP_DESC, 
      @EMAIL_SUBJECT_TXT, 
      @EMAIL_BODY_TXT, 
      @EMAIL_SENT_DT, 
      @SENDER_USER_ID, 
      @WKR_ID_TXT, 
      GETDATE(), 
      @WKR_ID_TXT, 
      GETDATE()
     );
     SELECT @error = @@ERROR, 
            @ReturnValue = SCOPE_IDENTITY();
     IF @error != 0
         RAISERROR('Error adding ProjectEmailNotification record.', 18, 1);
     RETURN;
GO