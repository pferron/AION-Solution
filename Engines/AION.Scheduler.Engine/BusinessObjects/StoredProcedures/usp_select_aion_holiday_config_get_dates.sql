/***********************************************************************************************************************
* Object:       usp_select_aion_holiday_config_get_dates
* Description:  Retrieves Holiday dates.
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   jallen
* Created:      06/28/2022
************************************************************************************************************************
* Change History: Date, Name, Description
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_select_aion_holiday_config_get_dates]
   
AS
  
WITH CTE_ALL_HOLIDAYS
AS
(
	SELECT
	HOLIDAY_DT 
	FROM HOLIDAY_CONFIGURATION
	WHERE ACTIVE_IND = 1
),
CTE_RECURRENCES AS
(
	SELECT
	DATEADD(YEAR, (YEAR(GETDATE()) - YEAR(HOLIDAY_DT)), HOLIDAY_DT) as HOLIDAY_DT
	FROM HOLIDAY_CONFIGURATION
	WHERE ACTIVE_IND = 1
	AND HOLIDAY_ANNUAL_RECUR_IND = 1
),
CTE_COMBINED AS
(
	SELECT HOLIDAY_DT
	FROM CTE_ALL_HOLIDAYS
	UNION
	SELECT HOLIDAY_DT
	FROM CTE_RECURRENCES
)
SELECT HOLIDAY_DT
FROM CTE_COMBINED
ORDER BY HOLIDAY_DT

