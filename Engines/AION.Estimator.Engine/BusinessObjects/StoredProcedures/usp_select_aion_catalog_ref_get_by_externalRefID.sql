/****** Object:  StoredProcedure [AION].[usp_select_aion_catalog_ref_get_by_externalRefID]    Script Date: 12/11/2019 9:06:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:       usp_select_aion_catalog_group_ref_get_by_id
* Description:  Retrieves CatalogGroupRef record for given CATALOG_KEY_TXT field(s).
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

ALTER PROCEDURE [AION].[usp_select_aion_catalog_ref_get_by_externalRefID]

    @externalRefID varchar(500)

AS

SELECT 
           CT.CATALOG_GRP_REF_ID,
		   CT.[CATALOG_REF_ID], 
		   CT.[CATALOG_VAL_TXT],
		   CT.[CATALOG_KEY_TXT],
		   CT.[CATALOG_SUBKEY_TXT],
		   CT.[WKR_ID_CREATED_TXT],
		   CT.[CREATED_DTTM],
		   CT.[WKR_ID_UPDATED_TXT],
		   CT.[UPDATED_DTTM]
       FROM AION.CATALOG_GROUP_REF CG JOIN AION.CATALOG_REF CT ON CG.[CATALOG_GRP_REF_ID] = CT.[CATALOG_GRP_REF_ID]
       
	   WHERE CATALOG_GRP_NM =  @externalRefID;
         

RETURN

