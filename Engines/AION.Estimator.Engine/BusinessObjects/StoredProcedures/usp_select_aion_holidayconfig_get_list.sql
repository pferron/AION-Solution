/****** Object:  StoredProcedure [AION].[usp_select_aion_holidayconfig_get_list]    Script Date: 4/30/2021 3:22:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:       usp_select_aion_holidayconfig_get_list
* Description:  Retrieves Holiday list for given parameter(s).
* Parameters:   
*               @identity                                                   int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   Gayatri
* Created:      02/18/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/10/2019    Gayatri     Auto-generated
* 04/30/2021    Jallen      Pull only active holidays
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_select_aion_holidayconfig_get_list]
   
AS
  SELECT 
       HOLIDAY_CONFIG_ID
      ,HOLIDAY_NM
      ,HOLIDAY_DT
      ,HOLIDAY_ANNUAL_RECUR_IND
      ,ACTIVE_IND
  FROM AION.HOLIDAY_CONFIGURATION

  WHERE ACTIVE_IND = 1
     
RETURN

