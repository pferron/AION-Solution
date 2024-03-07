
/***********************************************************************************************************************
* Object:       usp_select_aion_project_email_notification_get_list
* Description:  Retrieves ProjectEmailNotification list for given parameter(s).
* Parameters:   
*               @identity                                                   int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      11/12/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 11/12/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_project_email_notification_get_list] @identity INT
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
     WHERE PROJECT_ID = @identity;
     RETURN;
GO