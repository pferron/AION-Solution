/****** Object:  StoredProcedure [AION].[usp_select_aion_notes_get_by_id]    Script Date: 1/31/2020 3:33:49 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/***********************************************************************************************************************
* Object:       usp_select_aion_notes_get_by_id
* Description:  Retrieves Notes record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   AION_user
* Created:      10/2/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/2/2019    AION_user     Auto-generated
* 1/31/2020     jeanine lindsay add parent notes id, business ref id
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_select_aion_notes_get_by_id] @identity INT
AS
SELECT [NOTES_ID]
	,[NOTES_COMMENT]
	,[WKR_ID_CREATED_TXT]
	,[CREATED_DTTM]
	,[WKR_ID_UPDATED_TXT]
	,[UPDATED_DTTM]
	,[PROJECT_ID]
	,[NOTES_TYP_REF_ID]
	,[PARENT_NOTES_ID]
	,[BUSINESS_REF_ID]
FROM NOTES
WHERE
	-- @TODO:  Correct the following as necessary
	NOTES_ID = @identity

RETURN
GO

