
/***********************************************************************************************************************
* Object:       usp_delete_aion_plan_reviewer_available_hours
* Description:  Deletes PlanReviewerAvailableHours record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      3/18/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 3/18/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [AION].[usp_delete_aion_plan_reviewer_available_hours]

    @identity                                                    int

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM PLAN_REVIEWER_AVAILABLE_HOURS
       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          PLAN_REVIEWER_AVAILABLE_HOURS_ID = @identity
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting PlanReviewerAvailableHours record.', 18,1)

RETURN

GO