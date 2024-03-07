/****** Object:  StoredProcedure [AION].[usp_select_aion_user_bysystemroleid]    Script Date: 4/14/2021 8:22:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************      
* Object:       usp_select_aion_user_bysystemroleid      
* Description:  Retrieves users for system role id  
* Parameters:         
*               @systemroleid                                                    int      
* Returns:      Recordset.      
* Comments:     Developer may need to manually join to other tables, such as code tables,      
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.      
* Version:      1.0      
* Created by:   jlindsay     
* Created:      12/10/2020    
************************************************************************************************************************      
* Change History: Date, Name, Description      
* 12/10/2020    jlindsay     initial     
* 01/19/2021    jlindsay    add ordering
* 04/14/2021    jallen    add UserPrincipalName and CalendarId
* 05/04/2021    jallen    change column name USER_PRINCIPAL_NM
* 05/10/2021    jallen    add CityInd
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_select_aion_user_bysystemroleid] @systemroleid INT
AS
    BEGIN

        --    
        WITH systemroles
             AS (SELECT *
                 FROM system_role
                 WHERE system_role_id = @systemroleid
                 UNION ALL
                 SELECT *
                 FROM system_role
                 WHERE parent_system_role_id = @systemroleid)
             SELECT u.[USER_ID], 
                    u.[FIRST_NM], 
                    u.[LAST_NM], 
                    u.[EXTERNAL_SYSTEM_REF_ID], 
                    u.[SRC_SYSTEM_VAL_TXT], 
                    u.[WKR_ID_CREATED_TXT], 
                    u.[CREATED_DTTM], 
                    u.[WKR_ID_UPDATED_TXT], 
                    u.[UPDATED_DTTM], 
                    u.[ACTIVE_IND], 
                    u.[USER_INTERFACE_SETTING_TXT], 
                    u.[IS_EXPRESS_SCHEDULED_IND], 
                    u.[USER_NM], 
                    u.[LAN_ID_TXT], 
                    u.[PHONE_NUM], 
                    u.[EMAIL_ADDR_TXT], 
                    u.[NOTES_TXT], 
                    u.[IS_SCHEDULABLE_IND], 
                    u.[PLAN_REVIEW_OVERRIDE_HOURS_NBR], 
                    u.[HOURS_ESTIMATED_DESC], 
                    u.[SCHEDULABLE_LVL_DESC], 
                    u.[JURISDICTION_TYP_ID], 
                    u.[IS_PRELIM_MEETING_ALLOWED_IND],
					u.[USER_PRINCIPAL_NM],
					u.[CALENDAR_ID],
                    u.[CITY_IND]
             FROM [user] u
                  INNER JOIN user_system_role_relationship r ON u.user_id = r.user_id
                  INNER JOIN systemroles s ON r.system_role_id = s.system_role_id
             WHERE u.ACTIVE_IND = 1
             ORDER BY u.FIRST_NM, 
                      u.LAST_NM;
    END;
