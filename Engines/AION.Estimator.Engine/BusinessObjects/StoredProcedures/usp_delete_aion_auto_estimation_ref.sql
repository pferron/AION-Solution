
/***********************************************************************************************************************
* Object:       usp_delete_aion_auto_estimation_ref
* Description:  Deletes AutoEstimationRef record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      2/13/2023
************************************************************************************************************************
* Change History: Date, Name, Description
* 2/13/2023    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_delete_aion_auto_estimation_ref]

    @identity                                                    int

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM AUTO_ESTIMATION_REF
       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          AUTO_ESTIMATION_REF_ID = @identity
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting AutoEstimationRef record.', 18,1)

RETURN

GO