
/***********************************************************************************************************************
* Object:       usp_delete_aion_permission_system_role
* Description:  Deletes PermissionSystemRole record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      5/11/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 5/11/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_delete_aion_permission_system_role]

    @identity                                                    int

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM PERMISSION_SYSTEM_ROLE_XREF
       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          PERMISSION_SYSTEM_ROLE_CROSS_REF_ID = @identity
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting PermissionSystemRole record.', 18,1)

RETURN

GO