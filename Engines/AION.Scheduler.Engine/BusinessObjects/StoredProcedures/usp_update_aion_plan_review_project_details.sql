
/***********************************************************************************************************************
* Object:       usp_update_aion_plan_review_project_details
* Description:  Updates PlanReviewProjectDetails record using supplied parameters.
* Parameters:   
*               @PLAN_REVIEW_PROJECT_DETAILS_ID                              int
*               @PROJECT_ID                                                  int
*               @RESPONDER_USER_IDENTIFIER                                   int
*               @IS_APRV_IND                                                 bit
*               @RESPONSE_DT                                                 datetime
*               @UPDATED_DTTM                                                datetime
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      9/3/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 9/3/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_update_aion_plan_review_project_details]

    @PLAN_REVIEW_PROJECT_DETAILS_ID                              int
  , @PROJECT_ID                                                  int
  , @RESPONDER_USER_IDENTIFIER                                   int
  , @IS_APRV_IND                                                 bit
  , @RESPONSE_DT                                                 datetime
  , @UPDATED_DTTM                                                datetime
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE PLAN_REVIEW_PROJECT_DETAILS
       SET
            PROJECT_ID                                                   = @PROJECT_ID
          , RESPONDER_USER_IDENTIFIER                                    = @RESPONDER_USER_IDENTIFIER
          , IS_APRV_IND                                                  = @IS_APRV_IND
          , RESPONSE_DT                                                  = @RESPONSE_DT
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()

       WHERE
          PLAN_REVIEW_PROJECT_DETAILS_ID                                 = @PLAN_REVIEW_PROJECT_DETAILS_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating PlanReviewProjectDetails record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO