
/***********************************************************************************************************************
* Object:       usp_select_aion_project_email_notification_get_by_id
* Description:  Retrieves ProjectEmailNotification record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   AION_user
* Created:      11/12/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 11/12/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

ALTER PROCEDURE [usp_select_aion_project_email_notification_get_by_id] @identity INT
AS
     SELECT PROJECT_EMAIL_NOTIFICATION_ID, 
            PROJECT_ID, 
            EMAIL_TYP_DESC, 
            EMAIL_SUBJECT_TXT, 
            EMAIL_BODY_TXT, 
            EMAIL_SENT_DT, 
            SENDER_USER_ID, 
            WKR_ID_CREATED_TXT, 
            CREATED_DTTM, 
            WKR_ID_UPDATED_TXT, 
            UPDATED_DTTM
     FROM PROJECT_EMAIL_NOTIFICATION
     WHERE PROJECT_EMAIL_NOTIFICATION_ID = @identity;
     RETURN;
GO