
/***********************************************************************************************************************
* Object:       usp_delete_aion_plan_review_project_details
* Description:  Deletes PlanReviewProjectDetails record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      9/3/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 9/3/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_delete_aion_plan_review_project_details]

    @identity                                                    int

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM PLAN_REVIEW_PROJECT_DETAILS
       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          PLAN_REVIEW_PROJECT_DETAILS_ID = @identity
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting PlanReviewProjectDetails record.', 18,1)

RETURN

GO