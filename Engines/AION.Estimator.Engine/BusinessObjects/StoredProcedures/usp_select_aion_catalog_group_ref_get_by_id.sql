/****** Object:  StoredProcedure [AION].[usp_select_aion_catalog_group_ref_get_by_id]    Script Date: 12/11/2019 8:52:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:       usp_select_aion_catalog_group_ref_get_by_id
* Description:  Retrieves CatalogGroupRef record for given key field(s).
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

ALTER PROCEDURE [AION].[usp_select_aion_catalog_group_ref_get_by_id]

    @identity                                                    int

AS

       SELECT 
            CATALOG_GRP_REF_ID
          , CATALOG_GRP_NM
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM

       FROM CATALOG_GROUP_REF

       WHERE

		CATALOG_GRP_REF_ID = @identity
                  

RETURN

