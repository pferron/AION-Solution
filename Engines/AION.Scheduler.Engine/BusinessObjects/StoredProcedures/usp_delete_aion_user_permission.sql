
/***********************************************************************************************************************
* Object:       usp_delete_aion_user_permission
* Description:  Deletes UserPermission record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      5/8/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 5/8/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_delete_aion_user_permission]

    @identity                                                    int

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM USER_PERMISSION_XREF
       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          USER_PERMISSION_CROSS_REF_ID = @identity
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting UserPermission record.', 18,1)

RETURN

GO