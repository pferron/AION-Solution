
/***********************************************************************************************************************
* Object:       usp_delete_aion_business_division_ref
* Description:  Deletes BusinessDivisionRef record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      3/21/2022
************************************************************************************************************************
* Change History: Date, Name, Description
* 3/21/2022    AION_user     Auto-generated
* 
***********************************************************************************************************************/

ALTER PROCEDURE AION.[usp_delete_aion_business_division_ref]

    @identity                                                    int

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM AION.BUSINESS_DIVISION_REF
       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          BUSINESS_DIVISION_REF_ID = @identity
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting BusinessDivisionRef record.', 18,1)

RETURN

GO