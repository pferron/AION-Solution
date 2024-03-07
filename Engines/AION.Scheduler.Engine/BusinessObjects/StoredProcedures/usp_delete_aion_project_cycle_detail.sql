
/***********************************************************************************************************************
* Object:       usp_delete_aion_project_cycle_detail
* Description:  Deletes ProjectCycleDetail record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      10/14/2021
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/14/2021    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_delete_aion_project_cycle_detail]

    @identity                                                    int

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM PROJECT_CYCLE_DETAIL
       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          PROJECT_CYCLE_DETAIL_ID = @identity
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting ProjectCycleDetail record.', 18,1)

RETURN

GO