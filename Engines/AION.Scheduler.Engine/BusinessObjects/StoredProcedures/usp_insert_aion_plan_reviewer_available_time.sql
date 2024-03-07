
/***********************************************************************************************************************
* Object:	usp_insert_aion_plan_reviewer_available_time
* Description:	Inserts PlanReviewerAvailableTime record.
* Parameters:
*		@AVAILABLE_START_TM                                          datetime
*		@AVAILABLE_END_TM                                            datetime
*		@PROJECT_TYP_DESC                                            varchar(100)
*		@PROJECT_TYP_REF_ID                                          int
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      8/28/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 8/28/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_insert_aion_plan_reviewer_available_time]
    @AVAILABLE_START_TM                                          datetime
  , @AVAILABLE_END_TM                                            datetime
  , @PROJECT_TYP_DESC                                            varchar(100)
  , @PROJECT_TYP_REF_ID                                          int
  , @WKR_ID_TXT                                                  varchar(100)
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO PLAN_REVIEWER_AVAILABLE_TIME
          (
            AVAILABLE_START_TM
          , AVAILABLE_END_TM
          , PROJECT_TYP_DESC
          , PROJECT_TYP_REF_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          )
     VALUES
          (
            @AVAILABLE_START_TM
          , @AVAILABLE_END_TM
          , @PROJECT_TYP_DESC
          , @PROJECT_TYP_REF_ID
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding PlanReviewerAvailableTime record.', 18,1)

RETURN
GO