
/***********************************************************************************************************************
* Object:       usp_delete_aion_project_cycle
* Description:  Deletes ProjectCycle record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      10/11/2021
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/11/2021    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_delete_aion_project_cycle]

    @identity                                                    int

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM PROJECT_CYCLE
       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          PROJECT_CYCLE_ID = @identity
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting ProjectCycle record.', 18,1)

RETURN

GO