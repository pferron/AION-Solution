
/***********************************************************************************************************************      
* Object:       usp_select_assigned_facilitator      
* Description:  Retrieves User list for given parameter(s).      
* Parameters:         
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
* 11/20/2019    Shijo/Gayathri         Adding new colum to keep user active or inactive instead of deletion.        
* 03/19/2020    J Lindsay   Format, Change UNION ALL to UNION to get distinct, Change Left Join to get p table since null row    
*                           was coming back - break fix    
*1/26/2021   G Nadimpalli Get Least assigned Facilitator    
* 02/09/2021    jlindsay    exclude Closed and Cancelled projects  
* 02/12/2021    jlindsay    LES-2948 - picking users that aren't facilitators  
* 02/16/2021    jlindsay    sort by last name, first name
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_select_assigned_facilitator]
AS
    BEGIN

        -- closed       
        DECLARE @closedstatus INT;
        SELECT @closedstatus = PROJECT_STATUS_REF_ID
        FROM [AION].[PROJECT_STATUS_REF]
        WHERE ENUM_MAPPING_VAL_NBR = 21;    
        -- cancelled  
        DECLARE @cancelledstatus INT;
        SELECT @cancelledstatus = PROJECT_STATUS_REF_ID
        FROM [AION].[PROJECT_STATUS_REF]
        WHERE ENUM_MAPPING_VAL_NBR = 25;    
        --  
        WITH cteuser
             AS (SELECT [USER_ID], 
                        FIRST_NM, 
                        LAST_NM, 
                        EXTERNAL_SYSTEM_REF_ID, 
                        SRC_SYSTEM_VAL_TXT, 
                        ACTIVE_IND
                 FROM [AION].[USER] AS u
                 WHERE [USER_ID] IN
                 (
                     SELECT [USER_ID]
                     FROM [AION].[USER_SYSTEM_ROLE_RELATIONSHIP] AS usr
                     WHERE SYSTEM_ROLE_ID =
                     (
                         SELECT SYSTEM_ROLE_ID
                         FROM [AION].[SYSTEM_ROLE] AS sr
                         WHERE SYSTEM_ROLE_NM = 'Facilitator'
                     )
                 )
                       AND u.ACTIVE_IND = 1),
             cteprojectuser
             AS (SELECT p.ASSIGNED_FACILITATOR_ID AS [USER_ID], 
                        u.FIRST_NM, 
                        u.LAST_NM, 
                        u.EXTERNAL_SYSTEM_REF_ID, 
                        u.SRC_SYSTEM_VAL_TXT, 
                        u.ACTIVE_IND
                 FROM cteuser u
                      INNER JOIN AION.PROJECT p ON p.ASSIGNED_FACILITATOR_ID = u.[USER_ID]
                 WHERE p.PROJECT_STATUS_REF_ID NOT IN(@cancelledstatus, @closedstatus)
                      AND p.ASSIGNED_FACILITATOR_ID <> -1)  
             --  
             SELECT TOP 1 [USER_ID]
             FROM cteprojectuser
             GROUP BY [USER_ID], 
                      FIRST_NM, 
                      LAST_NM, 
                      EXTERNAL_SYSTEM_REF_ID, 
                      SRC_SYSTEM_VAL_TXT, 
                      ACTIVE_IND
             ORDER BY COUNT([USER_ID]) ASC, 
                      LAST_NM ASC, 
                      FIRST_NM ASC;
    END;