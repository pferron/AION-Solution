
/***********************************************************************************************************************
* Object:       usp_delete_aion_aion_user_schedule_stage
* Description:  Deletes UserScheduleStage record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      9/25/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 9/25/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_delete_aion_user_schedule_stage]

    @project_id                                                    int

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM USER_SCHEDULE_STAGE
       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          [PROJECT_ID] = @project_id
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting UserScheduleStage record.', 18,1)

RETURN

GO