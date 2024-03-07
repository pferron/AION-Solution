
/***********************************************************************************************************************
* Object:       usp_delete_aion_plan_reviewer_available_time
* Description:  Deletes PlanReviewerAvailableTime record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      8/28/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 8/28/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_delete_aion_plan_reviewer_available_time]

    @identity                                                    int

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM PLAN_REVIEWER_AVAILABLE_TIME
       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          PLAN_REVIEWER_AVAILABLE_TM_ID = @identity
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting PlanReviewerAvailableTime record.', 18,1)

RETURN

GO