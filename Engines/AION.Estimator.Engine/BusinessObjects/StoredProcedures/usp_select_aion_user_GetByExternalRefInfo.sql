/****** Object:  StoredProcedure [AION].[usp_select_aion_user_GetByExternalRefInfo]    Script Date: 4/13/2021 12:05:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************    
* Object:       usp_select_aion_user_GetByExternalRefInfo    
* Description:  Retrieves User record for given key field(s).    
* Parameters:       
*               @identity                                                    int    
*    
* Returns:      Recordset.    
* Comments:     Developer may need to manually join to other tables, such as code tables,    
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.    
* Version:      1.0    
* Created by:   AION_user    
* Created:      10/3/2019    
************************************************************************************************************************    
* Change History: Date, Name, Description    
* 10/3/2019    AION_user     Auto-generated    
* 10/11/2019 JL  Correct Columns    
* 12/10/2019 jeanine  add ui setting field      
* 04/23/2020    jlindsay add IsExpressSched    
* 04/29/2020    gnadimpalli add IsSchedulable  
* 02/19/2021    jlindsay removed externalsystemid filter
* 04/13/2021    jallen add UserPrincipalName and CalendarId
* 05/10/2021    jallen add CityInd
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_select_aion_user_GetByExternalRefInfo] @externalRefInfo  VARCHAR(255), 
                                                                    @externalSystemID INT
AS
    BEGIN
        --
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
        WHERE SRC_SYSTEM_VAL_TXT = @externalRefInfo;
        RETURN;
        --
    END;
