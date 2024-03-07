
/***********************************************************************************************************************
* Object:	usp_insert_aion_plan_review_schedule_detail
* Description:	Inserts PlanReviewScheduleDetail record.
* Parameters:
*		@PLAN_REVIEW_SCHEDULE_ID                                     int
*		@BUSINESS_REF_ID                                             int
*		@START_DT                                                    datetime
*		@END_DT                                                      datetime
*		@POOL_REQUEST_IND                                            bit
*		@SAME_BUILD_CONTR_IND                                        bit
*		@MANUAL_ASSIGNMENT_IND                                       bit
*		@ASSIGNED_HOURS_NBR                                          decimal
*		@ASSIGNED_PLAN_REVIEWER_ID                                   int
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      10/12/2021
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/12/2021    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_insert_aion_plan_review_schedule_detail]
    @PLAN_REVIEW_SCHEDULE_ID                                     int
  , @BUSINESS_REF_ID                                             int
  , @START_DT                                                    datetime
  , @END_DT                                                      datetime
  , @POOL_REQUEST_IND                                            bit
  , @SAME_BUILD_CONTR_IND                                        bit
  , @MANUAL_ASSIGNMENT_IND                                       bit
  , @ASSIGNED_HOURS_NBR                                          decimal(9,2)
  , @ASSIGNED_PLAN_REVIEWER_ID                                   int
  , @WKR_ID_TXT                                                  varchar(100)
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO PLAN_REVIEW_SCHEDULE_DETAIL
          (
            PLAN_REVIEW_SCHEDULE_ID
          , BUSINESS_REF_ID
          , START_DT
          , END_DT
          , POOL_REQUEST_IND
          , SAME_BUILD_CONTR_IND
          , MANUAL_ASSIGNMENT_IND
          , ASSIGNED_HOURS_NBR
          , ASSIGNED_PLAN_REVIEWER_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          )
     VALUES
          (
            @PLAN_REVIEW_SCHEDULE_ID
          , @BUSINESS_REF_ID
          , @START_DT
          , @END_DT
          , @POOL_REQUEST_IND
          , @SAME_BUILD_CONTR_IND
          , @MANUAL_ASSIGNMENT_IND
          , @ASSIGNED_HOURS_NBR
          , @ASSIGNED_PLAN_REVIEWER_ID
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding PlanReviewScheduleDetail record.', 18,1)

RETURN
GO