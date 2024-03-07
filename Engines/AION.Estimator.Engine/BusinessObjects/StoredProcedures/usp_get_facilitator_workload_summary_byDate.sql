
/***********************************************************************************************************************  
* Object:       usp_get_facilitator_workload_summary  
* Description:  Retrieves list of facilitators and total number of projects assigned to each of them  
* Returns:      Recordset.  
* Version:      1.0  
* Created by:   Gayatri  
* Created:      1/23/2020  
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 1/23/2020 - Gayatri - Retrieves list of facilitators and total number of projects assigned to each of them  
* 03-10-2021    jlindsay    remove cancelled and closed status, update users list, add 1 to end date, change ordering
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_get_facilitator_workload_summary_byDate] @StartDate DATE = NULL, 
                                                                     @EndDate   DATE = NULL
AS
    BEGIN
        --don't get these status projects
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
        DECLARE @SDate DATETIME;
        DECLARE @EDate DATETIME;
        SET @SDate = ISNULL(@StartDate, DATEADD(DAY, -30, GETDATE()));
        SET @EDate = ISNULL(@EndDate, GETDATE());

        -- always add 1 day to end date to get all for the end date
        SELECT @EDate = @EDate + 1;
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
             AS (SELECT p.PROJECT_ID, 
                        p.ASSIGNED_FACILITATOR_ID AS [USER_ID], 
                        u.FIRST_NM, 
                        u.LAST_NM, 
                        u.EXTERNAL_SYSTEM_REF_ID, 
                        u.SRC_SYSTEM_VAL_TXT, 
                        ACTIVE_IND, 
                        p.CREATED_DTTM
                 FROM [AION].[USER] u
                      RIGHT JOIN AION.PROJECT p ON p.ASSIGNED_FACILITATOR_ID = u.[USER_ID]
                 WHERE p.PROJECT_STATUS_REF_ID NOT IN(@cancelledstatus, @closedstatus)
                      AND p.ASSIGNED_FACILITATOR_ID <> -1)
             SELECT p.[USER_ID], 
                    u.FIRST_NM, 
                    u.LAST_NM, 
                    COUNT(p.PROJECT_ID) AS TotalNoOfProjects
             FROM cteprojectuser p
                  RIGHT JOIN cteuser u ON p.[USER_ID] = u.[USER_ID]
             WHERE p.CREATED_DTTM BETWEEN @SDate AND @EDate
                   AND p.[USER_ID] IS NOT NULL
             GROUP BY p.[USER_ID], 
                      u.FIRST_NM, 
                      u.LAST_NM
             ORDER BY u.FIRST_NM, 
                      u.LAST_NM;
    END;