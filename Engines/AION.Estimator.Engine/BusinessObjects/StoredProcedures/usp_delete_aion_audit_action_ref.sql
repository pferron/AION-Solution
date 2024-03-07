
/***********************************************************************************************************************
* Object:       usp_delete_aion_audit_action_ref
* Description:  Deletes AuditActionRef record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      2/27/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 2/27/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_delete_aion_audit_action_ref]

    @identity                                                    int

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM AUDIT_ACTION_REF
       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          AUDIT_ACTION_REF_ID = @identity
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting AuditActionRef record.', 18,1)

RETURN

GO