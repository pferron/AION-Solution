
/***********************************************************************************************************************
* Object:	usp_insert_aion_notification_email_list
* Description:	Inserts NotificationEmailList record.
* Parameters:
*		@PROJECT_EMAIL_NOTIFICATION_ID                               int
*		@USER_ID                                                     int
*		@EMAIL_ADDR_TXT                                              varchar(100)
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

CREATE PROCEDURE [usp_insert_aion_notification_email_list] @PROJECT_EMAIL_NOTIFICATION_ID INT, 
                                                           @USER_ID                       INT, 
                                                           @EMAIL_ADDR_TXT                VARCHAR(100), 
                                                           @WKR_ID_TXT                    VARCHAR(100), 
                                                           @ReturnValue                   INT OUTPUT
AS
     DECLARE @error INT;
     INSERT INTO NOTIFICATION_EMAIL_LIST
     (PROJECT_EMAIL_NOTIFICATION_ID, 
      USER_ID, 
      EMAIL_ADDR_TXT, 
      WKR_ID_CREATED_TXT, 
      CREATED_DTTM, 
      WKR_ID_UPDATED_TXT, 
      UPDATED_DTTM
     )
     VALUES
     (@PROJECT_EMAIL_NOTIFICATION_ID, 
      @USER_ID, 
      @EMAIL_ADDR_TXT, 
      @WKR_ID_TXT, 
      GETDATE(), 
      @WKR_ID_TXT, 
      GETDATE()
     );
     SELECT @error = @@ERROR, 
            @ReturnValue = SCOPE_IDENTITY();
     IF @error != 0
         RAISERROR('Error adding NotificationEmailList record.', 18, 1);
     RETURN;
GO