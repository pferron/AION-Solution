
/***********************************************************************************************************************
* Object:       usp_delete_aion_standard_note
* Description:  Deletes StandardNote record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      2/21/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 2/21/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_delete_aion_standard_note]

    @identity                                                    int

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM STANDARD_NOTE
       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          STANDARD_NOTE_ID = @identity
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting StandardNote record.', 18,1)

RETURN

GO