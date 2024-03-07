
/***********************************************************************************************************************
* Object:	usp_insert_aion_plan_reviewer_available_hours
* Description:	Inserts PlanReviewerAvailableHours record.
* Parameters:
*		@AVAILABLE_HOURS_NBR                                         int
*		@ENUM_MAPPING_VAL_NBR                                        int
*		@PLAN_REVIEWER_TYP_DESC                                      varchar(100)
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      3/18/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 3/18/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [AION].[usp_insert_aion_plan_reviewer_available_hours]
    @AVAILABLE_HOURS_NBR                                         int
  , @ENUM_MAPPING_VAL_NBR                                        int
  , @PLAN_REVIEWER_TYP_DESC                                      varchar(100)
  , @WKR_ID_TXT                                                  varchar(100)
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO PLAN_REVIEWER_AVAILABLE_HOURS
          (
            AVAILABLE_HOURS_NBR
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , ENUM_MAPPING_VAL_NBR
          , PLAN_REVIEWER_TYP_DESC
          )
     VALUES
          (
            @AVAILABLE_HOURS_NBR
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          , @ENUM_MAPPING_VAL_NBR
          , @PLAN_REVIEWER_TYP_DESC
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding PlanReviewerAvailableHours record.', 18,1)

RETURN
GO