
/***********************************************************************************************************************
* Object:	usp_insert_aion_excluded_plan_reviewers
* Description:	Inserts ExcludedPlanReviewers record.
* Parameters:
*		@PLAN_REVIEWER_ID                                            int
*		@PROJECT_BUSINESS_RELATIONSHIP_ID                            int
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      10/10/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/10/2019    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_insert_aion_excluded_plan_reviewers]
    @PLAN_REVIEWER_ID                                            int
  , @PROJECT_BUSINESS_RELATIONSHIP_ID                            int
  , @WKR_ID_TXT                                                  varchar(100)
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO EXCLUDED_PLAN_REVIEWERS
          (
            PLAN_REVIEWER_ID
          , PROJECT_BUSINESS_RELATIONSHIP_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          )
     VALUES
          (
            @PLAN_REVIEWER_ID
          , @PROJECT_BUSINESS_RELATIONSHIP_ID
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding ExcludedPlanReviewers record.', 18,1)

RETURN
GO