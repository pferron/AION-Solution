
/***********************************************************************************************************************
* Object:       usp_select_aion_standard_note_get_by_id
* Description:  Retrieves StandardNote record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   AION_user
* Created:      2/21/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 2/21/2020    AION_user     Auto-generated
* 2/24/2020     jlindsay    add STANDARD_NOTE_TITLE_TXT
***********************************************************************************************************************/

ALTER PROCEDURE [usp_select_aion_standard_note_get_by_id]

    @identity                                                    int

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

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          STANDARD_NOTE_ID = @identity
          

RETURN

GO