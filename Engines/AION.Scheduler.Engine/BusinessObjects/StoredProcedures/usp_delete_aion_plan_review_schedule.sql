/***********************************************************************************************************************  
* Object:       usp_delete_aion_plan_review_schedule  
* Description:  Deletes PlanReviewSchedule record for given key field(s).  
* Parameters:     
*               @identity                                                    int  
*  
* Returns:      Number of rows affected.  
* Version:      1.0  
* Created by:   AION_user  
* Created:      10/28/2021  
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 10/28/2021    AION_user     Auto-generated  
* 11/3/2021 jlindsay remove jba notation  
***********************************************************************************************************************/
CREATE PROCEDURE [usp_delete_aion_plan_review_schedule] @identity INT,
	@ReturnValue INT OUTPUT
AS
DECLARE @error INT

DELETE
FROM PLAN_REVIEW_SCHEDULE
WHERE
	-- @TODO:  Correct the following as necessary  
	PLAN_REVIEW_SCHEDULE_ID = @identity

SELECT @error = @@ERROR,
	@ReturnValue = @@ROWCOUNT

IF @error != 0
	RAISERROR (
			'Error deleting PlanReviewSchedule record.',
			18,
			1
			)

RETURN
