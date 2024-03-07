
/***********************************************************************************************************************
* Object:       usp_delete_aion_occupancy_type_ref
* Description:  Deletes OccupancyTypeRef record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      10/28/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/28/2019    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_delete_aion_occupancy_type_ref]

    @identity                                                    int

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM OCCUPANCY_TYPE_REF
       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          OCCUPANCY_TYP_REF_ID = @identity
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting OccupancyTypeRef record.', 18,1)

RETURN

GO