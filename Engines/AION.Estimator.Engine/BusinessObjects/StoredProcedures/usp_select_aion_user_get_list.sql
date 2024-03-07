/****** Object:  StoredProcedure [AION].[usp_select_aion_user_get_list]    Script Date: 4/13/2021 12:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************      
* Object:       usp_select_aion_user_get_list      
* Description:  Retrieves User list for given parameter(s).      
* Parameters:         
*                  
*      
* Returns:      Recordset.      
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.      
*               This proc expects id_person and/or id_file to generate list; modify as necessary.      
*               Include ORDER BY clause as necessary.      
* Version:      1.0      
* Created by:   AION_user      
* Created:      10/10/2019      
************************************************************************************************************************      
* Change History: Date, Name, Description      
* 10/10/2019    AION_user     Auto-generated      
* 12/10/2019 jeanine  add ui setting field          
* 04/23/2020    jlindsay add IsExpressSched      
* 04/29/2020    gnadimpalli add IsSchedulableInd    
* 01/19/2021    jlindsay ordering  
* 03/17/2021    jlindsay    remove filter for WHERE ACTIVE_IND = 1 AND USER_ID NOT IN(-1, 1)
* 04/13/2021    jallen add UserPrincipalName and CalendarId  
* 05/04/2021    jallen    change column name USER_PRINCIPAL_NM
* 05/10/2021    jallen add CityInd
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_select_aion_user_get_list]
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
     ORDER BY FIRST_NM, 
              LAST_NM;
     RETURN;
