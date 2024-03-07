
/***********************************************************************************************************************
* Object:       usp_update_aion_excluded_plan_reviewers
* Description:  Updates ExcludedPlanReviewers record using supplied parameters.
* Parameters:   
*               @EXCLUDED_PLAN_REVIEWERS_ID                                  int
*               @PLAN_REVIEWER_ID                                            int
*               @PROJECT_BUSINESS_RELATIONSHIP_ID                            int
*               @UPDATED_DTTM                                                datetime
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      10/10/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/10/2019    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_update_aion_excluded_plan_reviewers]

    @EXCLUDED_PLAN_REVIEWERS_ID                                  int
  , @PLAN_REVIEWER_ID                                            int
  , @PROJECT_BUSINESS_RELATIONSHIP_ID                            int
  , @UPDATED_DTTM                                                datetime
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE EXCLUDED_PLAN_REVIEWERS
       SET
            PLAN_REVIEWER_ID                                             = @PLAN_REVIEWER_ID
          , PROJECT_BUSINESS_RELATIONSHIP_ID                             = @PROJECT_BUSINESS_RELATIONSHIP_ID
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()

       WHERE
          EXCLUDED_PLAN_REVIEWERS_ID                                     = @EXCLUDED_PLAN_REVIEWERS_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating ExcludedPlanReviewers record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO