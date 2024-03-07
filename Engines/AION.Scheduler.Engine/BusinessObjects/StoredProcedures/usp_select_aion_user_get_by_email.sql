/****** Object:  StoredProcedure [AION].[usp_select_aion_user_get_by_email]    Script Date: 4/13/2021 12:53:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************  
* Object:       usp_select_aion_user_get_by_email
* Description:  Retrieves User record for given key field(s).  
* Parameters:     
*               @@email
*  
* Returns:      Recordset.  
* Comments:     Developer may need to manually join to other tables, such as code tables,  
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.  
* Version:      1.0  
* Created by:   AION_user  
* Created:      02/18/2021
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 02/18/2021    AION_user     Auto-generated  
* 04/13/2021    jallen add UserPrincipalName and CalendarId  
* 05/04/2021    jallen    change column name USER_PRINCIPAL_NM
* 05/10/2021    jallen add CityInd
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_select_aion_user_get_by_email] @email  VARCHAR(255)

AS
    SELECT 
            USER_ID
          , FIRST_NM
          , LAST_NM
          , EXTERNAL_SYSTEM_REF_ID
          , SRC_SYSTEM_VAL_TXT
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , ACTIVE_IND
          , USER_INTERFACE_SETTING_TXT
          , IS_EXPRESS_SCHEDULED_IND
          , USER_NM
          , LAN_ID_TXT
          , PHONE_NUM
          , EMAIL_ADDR_TXT
          , NOTES_TXT
          , IS_SCHEDULABLE_IND
          , PLAN_REVIEW_OVERRIDE_HOURS_NBR
          , HOURS_ESTIMATED_DESC
          , JURISDICTION_TYP_ID
          , SCHEDULABLE_LVL_DESC
          , IS_PRELIM_MEETING_ALLOWED_IND
		  , USER_PRINCIPAL_NM
		  , CALENDAR_ID
          , CITY_IND

     FROM [AION].[USER]
     WHERE

     SRC_SYSTEM_VAL_TXT = @email
     RETURN;