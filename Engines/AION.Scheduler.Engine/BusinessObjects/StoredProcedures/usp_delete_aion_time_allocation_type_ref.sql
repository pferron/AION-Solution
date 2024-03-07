
/***********************************************************************************************************************
* Object:       usp_delete_aion_time_allocation_type_ref
* Description:  Deletes TimeAllocationTypeRef record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      12/7/2021
************************************************************************************************************************
* Change History: Date, Name, Description
* 12/7/2021    AION_user     Auto-generated
* 
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_delete_aion_time_allocation_type_ref]

    @identity                                                    int

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM [AION].TIME_ALLOCATION_TYPE_REF
       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          TIME_ALLOCATION_TYP_REF_ID = @identity
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting TimeAllocationTypeRef record.', 18,1)

RETURN

GO