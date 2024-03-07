
/***********************************************************************************************************************
* Object:       usp_select_aion_attachment_link_get_by_id
* Description:  Retrieves AttachmentLink record for given key field(s).
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

CREATE PROCEDURE [usp_select_aion_attachment_link_get_by_id]

    @identity                                                    int

AS

       SELECT 
            ATTACHMENT_LINK_ID
          , LINK_TXT
          , NOTES_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , TAG_CREATED_ID_TXT
          , TAG_CREATED_DTTM
          , TAG_UPDATED_DTTM
          , TAG_UPDATED_ID_TXT
          , ATTACHMENT_TYP_CD

       FROM ATTACHMENT_LINK

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          ATTACHMENT_LINK_ID = @identity
          

RETURN

GO