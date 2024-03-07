
/***********************************************************************************************************************
* Object:       usp_select_aion_external_system_ref_get_by_id
* Description:  Retrieves ExternalSystemRef record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   AION_user
* Created:      10/10/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/10/2019    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_external_system_ref_get_by_id]

    @identity                                                    int

AS

       SELECT 
            EXTERNAL_SYSTEM_REF_ID
          , EXTERNAL_SYSTEM_NM
          , EXTERNAL_SYSTEM_DESC
          , ADDL_INFORMATION_TXT
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , ENUM_MAPPING_VAL_NBR

       FROM EXTERNAL_SYSTEM_REF

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          EXTERNAL_SYSTEM_REF_ID = @identity
          

RETURN

GO