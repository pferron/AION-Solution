
/***********************************************************************************************************************
* Object:	usp_insert_aion_plan_review_project_details
* Description:	Inserts PlanReviewProjectDetails record.
* Parameters:
*		@PROJECT_ID                                                  int
*		@RESPONDER_USER_IDENTIFIER                                   int
*		@IS_APRV_IND                                                 bit
*		@RESPONSE_DT                                                 datetime
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      9/3/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 9/3/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_insert_aion_plan_review_project_details]
    @PROJECT_ID                                                  int
  , @RESPONDER_USER_IDENTIFIER                                   int
  , @IS_APRV_IND                                                 bit
  , @RESPONSE_DT                                                 datetime
  , @WKR_ID_TXT                                                  varchar(100)
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO PLAN_REVIEW_PROJECT_DETAILS
          (
            PROJECT_ID
          , RESPONDER_USER_IDENTIFIER
          , IS_APRV_IND
          , RESPONSE_DT
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          )
     VALUES
          (
            @PROJECT_ID
          , @RESPONDER_USER_IDENTIFIER
          , @IS_APRV_IND
          , @RESPONSE_DT
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding PlanReviewProjectDetails record.', 18,1)

RETURN
GO