
/***********************************************************************************************************************
* Object:       usp_select_aion_attachment_link_get_list
* Description:  Retrieves AttachmentLink list for given parameter(s).
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

CREATE PROCEDURE [usp_select_aion_attachment_link_get_list]

    @identity                                                   int

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