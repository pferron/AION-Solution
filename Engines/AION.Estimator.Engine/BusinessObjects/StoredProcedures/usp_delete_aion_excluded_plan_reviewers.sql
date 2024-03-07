
/***********************************************************************************************************************
* Object:       usp_delete_aion_excluded_plan_reviewers
* Description:  Deletes ExcludedPlanReviewers record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      10/10/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/10/2019    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_delete_aion_excluded_plan_reviewers]

    @identity                                                    int

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM EXCLUDED_PLAN_REVIEWERS
       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          EXCLUDED_PLAN_REVIEWERS_ID = @identity
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting ExcludedPlanReviewers record.', 18,1)

RETURN

GO