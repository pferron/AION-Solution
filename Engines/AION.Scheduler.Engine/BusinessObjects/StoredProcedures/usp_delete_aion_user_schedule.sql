
/***********************************************************************************************************************
* Object:       usp_delete_aion_user_schedule
* Description:  Deletes UserSchedule record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      3/19/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 3/19/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [AION].[usp_delete_aion_user_schedule]

    @identity                                                    int

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM USER_SCHEDULE
       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          USER_SCHEDULE_ID = @identity
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting UserSchedule record.', 18,1)

RETURN

GO