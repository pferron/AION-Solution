CREATE PROCEDURE [AION].[usp_delete_aion_reserve_express_plan_reviewer_all]

  @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM RESERVE_EXPRESS_PLAN_REVIEWER
 
     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting ReserveExpressPlanReviewer records.', 18,1)

RETURN

