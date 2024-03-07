
/***********************************************************************************************************************
* Object:       usp_update_aion_plan_review_schedule_detail
* Description:  Updates PlanReviewScheduleDetail record using supplied parameters.
* Parameters:   
*               @PLAN_REVIEW_SCHEDULE_DETAIL_ID                              int
*               @PLAN_REVIEW_SCHEDULE_ID                                     int
*               @BUSINESS_REF_ID                                             int
*               @START_DT                                                    datetime
*               @END_DT                                                      datetime
*               @POOL_REQUEST_IND                                            bit
*               @SAME_BUILD_CONTR_IND                                        bit
*               @MANUAL_ASSIGNMENT_IND                                       bit
*               @ASSIGNED_HOURS_NBR                                          decimal
*               @ASSIGNED_PLAN_REVIEWER_ID                                   int
*               @UPDATED_DTTM                                                datetime
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      10/12/2021
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/12/2021    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_update_aion_plan_review_schedule_detail]

    @PLAN_REVIEW_SCHEDULE_DETAIL_ID                              int
  , @PLAN_REVIEW_SCHEDULE_ID                                     int
  , @BUSINESS_REF_ID                                             int
  , @START_DT                                                    datetime
  , @END_DT                                                      datetime
  , @POOL_REQUEST_IND                                            bit
  , @SAME_BUILD_CONTR_IND                                        bit
  , @MANUAL_ASSIGNMENT_IND                                       bit
  , @ASSIGNED_HOURS_NBR                                          decimal(9,2)
  , @ASSIGNED_PLAN_REVIEWER_ID                                   int
  , @UPDATED_DTTM                                                datetime
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE PLAN_REVIEW_SCHEDULE_DETAIL
       SET
            PLAN_REVIEW_SCHEDULE_ID                                      = @PLAN_REVIEW_SCHEDULE_ID
          , BUSINESS_REF_ID                                              = @BUSINESS_REF_ID
          , START_DT                                                     = @START_DT
          , END_DT                                                       = @END_DT
          , POOL_REQUEST_IND                                             = @POOL_REQUEST_IND
          , SAME_BUILD_CONTR_IND                                         = @SAME_BUILD_CONTR_IND
          , MANUAL_ASSIGNMENT_IND                                        = @MANUAL_ASSIGNMENT_IND
          , ASSIGNED_HOURS_NBR                                           = @ASSIGNED_HOURS_NBR
          , ASSIGNED_PLAN_REVIEWER_ID                                    = @ASSIGNED_PLAN_REVIEWER_ID
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()

       WHERE
          PLAN_REVIEW_SCHEDULE_DETAIL_ID                                 = @PLAN_REVIEW_SCHEDULE_DETAIL_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating PlanReviewScheduleDetail record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO