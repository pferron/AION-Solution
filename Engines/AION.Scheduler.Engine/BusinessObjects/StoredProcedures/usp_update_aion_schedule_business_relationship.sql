
/***********************************************************************************************************************
* Object:       usp_update_aion_schedule_business_relationship
* Description:  Updates ScheduleBusinessRelationship record using supplied parameters.
* Parameters:   
*               @SCHEDULE_BUSINESS_RELATIONSHIP_ID                           int
*               @PLAN_REVIEW_SCHEDULE_ID                                     int
*               @BUSINESS_REF_ID                                             int
*               @PROJECT_ID                                                  int
*               @REREVIEW_HOURS_NBR                                          int
*               @CYCLE_NBR                                                   int
*               @UPDATED_DTTM                                                datetime
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      9/14/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 9/14/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_update_aion_schedule_business_relationship]

    @SCHEDULE_BUSINESS_RELATIONSHIP_ID                           int
  , @PLAN_REVIEW_SCHEDULE_ID                                     int
  , @BUSINESS_REF_ID                                             int
  , @PROJECT_ID                                                  int
  , @REREVIEW_HOURS_NBR                                          int
  , @CYCLE_NBR                                                   int
  , @UPDATED_DTTM                                                datetime
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE SCHEDULE_BUSINESS_RELATIONSHIP
       SET
            PLAN_REVIEW_SCHEDULE_ID                                      = @PLAN_REVIEW_SCHEDULE_ID
          , BUSINESS_REF_ID                                              = @BUSINESS_REF_ID
          , PROJECT_ID                                                   = @PROJECT_ID
          , REREVIEW_HOURS_NBR                                           = @REREVIEW_HOURS_NBR
          , CYCLE_NBR                                                    = @CYCLE_NBR
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()

       WHERE
          SCHEDULE_BUSINESS_RELATIONSHIP_ID                              = @SCHEDULE_BUSINESS_RELATIONSHIP_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating ScheduleBusinessRelationship record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO