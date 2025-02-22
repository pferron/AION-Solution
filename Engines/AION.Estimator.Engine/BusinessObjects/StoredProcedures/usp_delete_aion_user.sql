
/***********************************************************************************************************************
* Object:       usp_delete_aion_user
* Description:  Deletes User record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      10/3/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/3/2019    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_delete_aion_user]

    @identity                                                    int

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM [USER]
       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          [USER_ID] = @identity
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting User record.', 18,1)

RETURN

GO