
/***********************************************************************************************************************
* Object:       usp_select_aion_business_type_ref_get_by_id
* Description:  Retrieves BusinessTypeRef record for given key field(s).
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

CREATE PROCEDURE [usp_select_aion_business_type_ref_get_by_id]

    @identity                                                    int

AS

       SELECT 
            BUSINESS_TYP_REF_ID
          , BUSINESS_REF_TYP_SHORT_DESC
          , BUSINESS_REF_DISPLAY_NM
          , EXTERNAL_SYSTEM_REF_ID
          , SRC_SYSTEM_VAL_TXT
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , ENUM_MAPPING_VAL_NBR

       FROM BUSINESS_TYPE_REF

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          BUSINESS_TYP_REF_ID = @identity
          

RETURN

GO