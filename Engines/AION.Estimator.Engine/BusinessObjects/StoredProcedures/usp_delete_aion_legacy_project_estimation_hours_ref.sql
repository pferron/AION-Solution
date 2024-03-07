
/***********************************************************************************************************************
* Object:       usp_delete_aion_legacy_project_estimation_hours_ref
* Description:  Deletes LegacyProjectEstimationHoursRef record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      8/31/2023
************************************************************************************************************************
* Change History: Date, Name, Description
* 8/31/2023    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_delete_aion_legacy_project_estimation_hours_ref]

    @identity                                                    int

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM LEGACY_PROJECT_ESTIMATION_HOURS_REF
       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          LEGACY_PROJECT_ESTIMATION_HOURS_REF_ID = @identity
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting LegacyProjectEstimationHoursRef record.', 18,1)

RETURN

GO