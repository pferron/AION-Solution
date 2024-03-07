/****** Object:  StoredProcedure [AION].[usp_select_aion_catalog_ref_get_by_id]    Script Date: 12/11/2019 8:29:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:       usp_select_aion_catalog_ref_get_by_id
* Description:  Retrieves CatalogRef record for given CATALOG_KEY_TXT field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   AION_user
* Created:      12/4/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 12/4/2019    AION_user     Auto-generated
* 
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_select_aion_catalog_ref_get_by_id]

    @identity                                                    int

AS

       SELECT 
            CATALOG_REF_ID
          , [CATALOG_VAL_TXT]
          , [CATALOG_KEY_TXT]
          , CATALOG_SUBKEY_TXT
          , CATALOG_GRP_REF_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM

       FROM CATALOG_REF

       WHERE @identity = CATALOG_REF_ID;
        

RETURN

