/****** Object:  StoredProcedure [AION].[usp_select_aion_default_estimation_hours_get_AllList]    Script Date: 2/19/2020 10:46:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:       usp_select_aion_default_estimation_hours_get_list
* Description:  Retrieves DefaultEstimationHours list for given parameter(s).
* Parameters:   
*               @identity                                                   int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      10/2/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/2/2019    AION_user     Auto-generated
* 
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_select_aion_default_estimation_hours_get_AllList]

	@PROJECT_TYP_REF_ID					int = NULL

AS

SELECT [DEFAULT_ESTIMATION_HOURS_ID]
	,[DEFAULT_HOURS_NBR]
	,[WKR_ID_CREATED_TXT]
	,[WKR_ID_UPDATED_TXT]
	,[CREATED_DTTM]
	,[UPDATED_DTTM]
	,[BUSINESS_REF_ID]
	,[PROJECT_TYP_REF_ID]
	,[ENABLED_IND]
    ,[ESTIMATION_HOURS_TXT]
FROM DEFAULT_ESTIMATION_HOURS
WHERE (@PROJECT_TYP_REF_ID IS NULL OR PROJECT_TYP_REF_ID = @PROJECT_TYP_REF_ID )

RETURN
