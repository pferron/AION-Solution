
/***********************************************************************************************************************      
* Object:       usp_select_aion_schedule_capacity_search_list      
* Description:  Gets a schedule list by person      
* Parameters:         
*               @startdate    DATETIME,       
*               @enddate      DATETIME,       
*               @reviewerscsv VARCHAR(8000)      
*      
* Returns:      list      
* Comments:           
* Version:      1.0      
* Created by:   jlindsay      
* Created:      7/29/2020      
************************************************************************************************************************      
* Change History: Date, Name, Description      
* 7/29/2020    jlindsay     initial      
* 01/27/2021    jlindsay    add DISTINCT to user list      
* 02/23/2021    jlindsay    remove plan reviewer role check  
* 02/24/2021    jlindsay    increase csv field lenght to 8000
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_select_aion_schedule_capacity_search_list] @startdate    DATETIME, 
                                                                        @enddate      DATETIME, 
                                                                        @reviewerscsv VARCHAR(8000)
AS
    BEGIN
        DECLARE @selecteddates TABLE
        (ID           INT IDENTITY(1, 1) NOT NULL PRIMARY KEY, 
         selecteddate DATETIME
        );
        INSERT INTO @selecteddates
               SELECT DATEADD(DAY, nbr - 1, @StartDate)
               FROM
               (
                   SELECT ROW_NUMBER() OVER(
                          ORDER BY c.object_id) AS Nbr
                   FROM sys.columns c
               ) nbrs
               WHERE nbr - 1 <= DATEDIFF(DAY, @StartDate, @EndDate);      
        --      
        --      
        DECLARE @reltable TABLE
        (ID INT, 
         nm VARCHAR(20)
        );
        WITH SELUSERS
             AS (SELECT DISTINCT 
                        U.[USER_ID], 
                        U.FIRST_NM, 
                        U.LAST_NM
                 FROM [AION].[USER] U),
             CSVUSERS
             AS (SELECT [value] AS [user_id]
                 FROM STRING_SPLIT(@reviewerscsv, ','))
             SELECT U.[USER_ID], 
                    U.FIRST_NM, 
                    U.LAST_NM, 
                    d.selecteddate AS SCHEDULE_DATE
             FROM SELUSERS u
                  CROSS JOIN @selecteddates d
                  INNER JOIN CSVUSERS v ON u.[user_id] = v.[user_id];
    END;