
/***********************************************************************************************************************    
* Object:       [usp_select_aion_user_admin_search_list]    
* Description:  Retrieves User list for given parameter(s).    
* Parameters:       
*                
*    
* Returns:      Recordset.    
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.    
*               This proc expects id_person and/or id_file to generate list; modify as necessary.    
*               Include ORDER BY clause as necessary.    
* Version:      1.0    
* Created by:  smithtc   
* Created:      10/10/2019    
************************************************************************************************************************    
* Change History: Date, Name, Description    
* 12/16/2020   smithtc  
* 01/19/2021    jlindsay    add ordering
* 04/16/2021    jallen      add USER_PRINCIPAL_NAME and CALENDAR_ID
* 05/04/2021    jallen    change column name USER_PRINCIPAL_NM
* 05/10/2021    jallen    add CityInd
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_select_aion_user_admin_search_list] @name VARCHAR(100)
AS
     SELECT USER_ID, 
            FIRST_NM, 
            LAST_NM, 
            EXTERNAL_SYSTEM_REF_ID, 
            SRC_SYSTEM_VAL_TXT, 
            WKR_ID_CREATED_TXT, 
            CREATED_DTTM, 
            WKR_ID_UPDATED_TXT, 
            UPDATED_DTTM, 
            ACTIVE_IND, 
            USER_INTERFACE_SETTING_TXT, 
            IS_EXPRESS_SCHEDULED_IND, 
            USER_NM, 
            LAN_ID_TXT, 
            PHONE_NUM, 
            EMAIL_ADDR_TXT, 
            NOTES_TXT, 
            IS_SCHEDULABLE_IND, 
            PLAN_REVIEW_OVERRIDE_HOURS_NBR, 
            HOURS_ESTIMATED_DESC, 
            JURISDICTION_TYP_ID, 
            SCHEDULABLE_LVL_DESC, 
            IS_PRELIM_MEETING_ALLOWED_IND,
            USER_PRINCIPAL_NM,
            CALENDAR_ID,
            CITY_IND
     FROM [AION].[USER]
     WHERE LAST_NM LIKE concat(replace(@name, ' ', '%'), '%')
           AND USER_ID NOT IN(-1, 1)
     ORDER BY FIRST_NM, 
              LAST_NM;
     RETURN;