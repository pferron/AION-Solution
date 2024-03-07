
/***********************************************************************************************************************
* Object:       usp_select_aion_module_get_by_id
* Description:  Retrieves Module record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   AION_user
* Created:      5/11/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 5/11/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_module_get_by_id]

    @identity                                                    int

AS

       SELECT 
            MODULE_REF_ID
          , MODULE_NM
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , ENUM_MAPPING_VAL_NBR

       FROM MODULE_REF

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          MODULE_REF_ID = @identity
          

RETURN

GO