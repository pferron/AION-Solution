
/***********************************************************************************************************************
* Object:       usp_select_aion_standard_note_get_list
* Description:  Retrieves StandardNote list for given parameter(s).
* Parameters:   
*               @identity                                                   int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      2/21/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 2/21/2020    AION_user     Auto-generated
* 2/24/2020     jlindsay    add STANDARD_NOTE_TITLE_TXT
***********************************************************************************************************************/

ALTER PROCEDURE [usp_select_aion_standard_note_get_list]


AS

       SELECT 
            STANDARD_NOTE_ID
          , STANDARD_NOTE_GRP_NM
          , STANDARD_NOTE_TYP_REF_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , STANDARD_NOTE_TXT
          , STANDARD_NOTE_TITLE_TXT
       FROM STANDARD_NOTE

         

RETURN

GO