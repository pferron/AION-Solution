
/***********************************************************************************************************************
* Object:       usp_delete_aion_user_project_type_ref
* Description:  Deletes UserProjectTypeRef record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      5/4/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 5/4/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_delete_aion_user_project_type_ref]

    @identity                                                    int

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM USER_PROJECT_TYPE_XREF
       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          USER_PROJECT_TYP_CROSS_REF_ID = @identity
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting UserProjectTypeRef record.', 18,1)

RETURN

GO