
/***********************************************************************************************************************
* Object:       usp_delete_aion_project_occupancy_typ_map_ref
* Description:  Deletes ProjectOccupancyTypMapRef record for given key field(s).
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

CREATE PROCEDURE [usp_delete_aion_project_occupancy_typ_map_ref]

    @identity                                                    int

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM PROJECT_OCCUPANCY_TYPE_MAP_REF
       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          PROJECT_OCCUPANCY_TYP_MAP_REF_ID = @identity
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting ProjectOccupancyTypMapRef record.', 18,1)

RETURN

GO