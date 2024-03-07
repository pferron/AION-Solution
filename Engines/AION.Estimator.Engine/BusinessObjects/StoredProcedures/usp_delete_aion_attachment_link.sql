
/***********************************************************************************************************************
* Object:       usp_delete_aion_attachment_link
* Description:  Deletes AttachmentLink record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      10/10/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/10/2019    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_delete_aion_attachment_link]

    @identity                                                    int

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM ATTACHMENT_LINK
       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          ATTACHMENT_LINK_ID = @identity
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting AttachmentLink record.', 18,1)

RETURN

GO