
/***********************************************************************************************************************
* Object:       usp_delete_aion_project_audit
* Description:  Deletes ProjectAudit record for given key field(s).
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

CREATE PROCEDURE [usp_delete_aion_project_audit]

    @identity                                                    int

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM PROJECT_AUDIT
       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          PROJECT_AUDIT_ID = @identity
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting ProjectAudit record.', 18,1)

RETURN

GO