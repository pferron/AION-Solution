/****** Object:  StoredProcedure [AION].[usp_select_aion_user_byparentsystemrole]    Script Date: 4/14/2021 8:19:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************      
* Object:       usp_select_aion_user_byparentsystemrole      
* Description:  Retrieves users for system role enum (parent role id)    
* Parameters:         
*               @parentsystemroleenum                                                    int      
*               @getall bit -- send 0 to only get active users    
* Returns:      Recordset.      
* Comments:     Developer may need to manually join to other tables, such as code tables,      
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.      
* Version:      1.0      
* Created by:   AION_user      
* Created:      10/15/2020      
************************************************************************************************************************      
* Change History: Date, Name, Description      
* 10/15/2020    jlindsay     initial    
* 01/19/2021    jlindsay    add ordering  
* 03/18/2021    jlindsay    fix user id duplication
* 04/14/2021    jallen    add UserPrincipalName and CalendarId
* 05/04/2021    jallen    change column name USER_PRINCIPAL_NM
* 05/10/2021    jallen    add CityInd
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_select_aion_user_byparentsystemrole] @parentsystemroleenum INT, 
                                                        @getall               BIT = 1
AS
    BEGIN

        --get all from system role    
        DECLARE @systemroleid INT= 0;
        SELECT @systemroleid = system_role_id
        FROM system_role
        WHERE enum_mapping_val_nbr = @parentsystemroleenum;    
        --    
        WITH systemroles
             AS (SELECT system_role_id
                 FROM system_role
                 WHERE system_role_id = @systemroleid
                 UNION ALL
                 SELECT system_role_id
                 FROM system_role
                 WHERE parent_system_role_id = @systemroleid),
             userids
             AS (SELECT DISTINCT 
                        u.[USER_ID]
                 FROM [user] u
                      INNER JOIN user_system_role_relationship r ON u.[USER_ID] = r.[USER_ID]
                      INNER JOIN systemroles s ON r.system_role_id = s.system_role_id
                 WHERE @getall = 1
                       OR u.ACTIVE_IND = 1)
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
                  INNER JOIN userids r ON u.[USER_ID] = r.[USER_ID]
             ORDER BY u.FIRST_NM, 
                      u.LAST_NM;
    END;