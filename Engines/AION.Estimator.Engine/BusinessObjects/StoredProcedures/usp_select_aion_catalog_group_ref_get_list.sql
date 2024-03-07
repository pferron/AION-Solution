/****** Object:  StoredProcedure [AION].[usp_select_aion_catalog_group_ref_get_list]    Script Date: 12/11/2019 8:52:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:       usp_select_aion_catalog_group_ref_get_list
* Description:  Retrieves CatalogGroupRef list for given parameter(s).
* Parameters:   
*               @identity                                                   int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      12/4/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 12/4/2019    AION_user     Auto-generated
* 
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_select_aion_catalog_group_ref_get_list]

    @identity                                                   int

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
	   
        @identity = CATALOG_GRP_REF_ID
       -- @TODO:  Correct the following as necessary
        
          

RETURN

