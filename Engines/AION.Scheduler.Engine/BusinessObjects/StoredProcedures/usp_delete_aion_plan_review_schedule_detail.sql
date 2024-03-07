
/***********************************************************************************************************************
* Object:       usp_delete_aion_plan_review_schedule_detail
* Description:  Deletes PlanReviewScheduleDetail record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      10/12/2021
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/12/2021    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_delete_aion_plan_review_schedule_detail]

    @identity                                                    int

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM PLAN_REVIEW_SCHEDULE_DETAIL
       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          PLAN_REVIEW_SCHEDULE_DETAIL_ID = @identity
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting PlanReviewScheduleDetail record.', 18,1)

RETURN

GO