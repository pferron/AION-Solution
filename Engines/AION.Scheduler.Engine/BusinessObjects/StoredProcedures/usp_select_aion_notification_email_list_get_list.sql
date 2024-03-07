
/***********************************************************************************************************************
* Object:       usp_select_aion_notification_email_list_get_list
* Description:  Retrieves NotificationEmailList list for given parameter(s).
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

CREATE PROCEDURE [usp_select_aion_notification_email_list_get_list] @identity INT
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
     WHERE
     PROJECT_EMAIL_NOTIFICATION_ID = @identity;
     RETURN;
GO