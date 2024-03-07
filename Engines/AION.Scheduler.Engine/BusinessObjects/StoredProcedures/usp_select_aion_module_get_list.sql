
/***********************************************************************************************************************
* Object:       usp_select_aion_module_get_list
* Description:  Retrieves Module list for given parameter(s).
* Parameters:   
*               @identity                                                   int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      5/11/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 5/11/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_module_get_list]

    @identity                                                   int

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