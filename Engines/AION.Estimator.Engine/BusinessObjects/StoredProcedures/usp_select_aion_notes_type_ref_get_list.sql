
/***********************************************************************************************************************
* Object:       usp_select_aion_notes_type_ref_get_list
* Description:  Retrieves NotesTypeRef list for given parameter(s).
* Parameters:   
*               @identity                                                   int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      10/10/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/10/2019    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_notes_type_ref_get_list]

    @identity                                                   int

AS

       SELECT 
            NOTES_TYP_REF_ID
          , NOTES_TYP_REF_NM
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , EXTERNAL_SYSTEM_REF_ID
          , SRC_SYSTEM_VAL_TXT
          , ENUM_MAPPING_VAL_NBR

       FROM NOTES_TYPE_REF

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          NOTES_TYP_REF_ID = @identity
          

RETURN

GO