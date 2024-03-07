
/***********************************************************************************************************************
* Object:       usp_delete_aion_square_footage_ref
* Description:  Deletes SquareFootageRef record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      4/30/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 4/30/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_delete_aion_square_footage_ref]

    @identity                                                    int

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM SQUARE_FOOTAGE_REF
       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          SQUARE_FOOTAGE_REF_ID = @identity
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting SquareFootageRef record.', 18,1)

RETURN

GO