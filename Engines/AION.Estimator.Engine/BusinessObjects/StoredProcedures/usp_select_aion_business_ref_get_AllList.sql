
/***********************************************************************************************************************
* Object:       usp_select_aion_business_ref_get_list
* Description:  Retrieves BusinessRef list for given parameter(s).
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

CREATE PROCEDURE [usp_select_aion_business_ref_get_AllList]

AS

       SELECT 
            BUSINESS_NM
          , BUSINESS_REF_ID
          , BUSINESS_SHORT_DESC
          , BUSINESS_TYP_REF_ID
          , CREATED_DTTM
          , DIVISION_REF_ID
          , ENUM_MAPPING_VAL_NBR
          , EXTERNAL_SYSTEM_REF_ID
          , REGION_REF_ID
          , SRC_SYSTEM_VAL_TXT
          , UPDATED_DTTM
          , WKR_ID_CREATED_TXT
          , WKR_ID_UPDATED_TXT

       FROM BUSINESS_REF

RETURN

GO