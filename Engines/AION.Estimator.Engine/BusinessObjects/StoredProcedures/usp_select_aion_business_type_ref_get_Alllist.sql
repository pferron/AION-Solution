
/***********************************************************************************************************************
* Object:       usp_select_aion_business_type_ref_get_list
* Description:  Retrieves BusinessTypeRef list for given parameter(s).
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

CREATE PROCEDURE [usp_select_aion_business_type_ref_get_Alllist]

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

RETURN

GO