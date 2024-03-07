
/***********************************************************************************************************************
* Object:       usp_select_aion_notification_email_list_get_by_id
* Description:  Retrieves NotificationEmailList record for given key field(s).
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

ALTER PROCEDURE [usp_select_aion_notification_email_list_get_by_id] @identity INT
AS
     SELECT NOTIFICATION_EMAIL_LIST_ID, 
            PROJECT_EMAIL_NOTIFICATION_ID, 
            USER_ID, 
            EMAIL_ADDR_TXT, 
            WKR_ID_CREATED_TXT, 
            CREATED_DTTM, 
            WKR_ID_UPDATED_TXT, 
            UPDATED_DTTM
     FROM NOTIFICATION_EMAIL_LIST
     WHERE NOTIFICATION_EMAIL_LIST_ID = @identity;
     RETURN;
GO