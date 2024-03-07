
/***********************************************************************************************************************
* Object:       usp_select_aion_business_division_ref_get_by_id
* Description:  Retrieves BusinessDivisionRef record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   AION_user
* Created:      3/21/2022
************************************************************************************************************************
* Change History: Date, Name, Description
* 3/21/2022    AION_user     Auto-generated
* 
***********************************************************************************************************************/

ALTER PROCEDURE AION.[usp_select_aion_business_division_ref_get_by_id]

    @identity                                                    int

AS

       SELECT 
            BUSINESS_DIVISION_REF_ID
          , BUSINESS_DIVISION_NM
          , BUSINESS_DIVISION_DESC
          , ENUM_MAPPING_VAL_NBR
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM

       FROM AION.BUSINESS_DIVISION_REF

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          BUSINESS_DIVISION_REF_ID = @identity
          

RETURN

GO