
/***********************************************************************************************************************
* Object:       usp_update_aion_schedule_business_relationship_stage
* Description:  Updates ScheduleBusinessRelationshipStage record using supplied parameters.
* Parameters:   
*               @SCHEDULE_BUSINESS_RELATIONSHIP_STAGE_ID                     int
*               @HOURS_NBR                                                   decimal
*               @CYCLE_NBR                                                   int
*               @BUSINESS_REF_ID                                             int
*               @PROJECT_ID                                                  int
*               @UPDATED_DTTM                                                datetime
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      12/17/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 12/17/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_update_aion_schedule_business_relationship_stage]

    @SCHEDULE_BUSINESS_RELATIONSHIP_STAGE_ID                     int
  , @HOURS_NBR                                                   decimal
  , @CYCLE_NBR                                                   int
  , @BUSINESS_REF_ID                                             int
  , @PROJECT_ID                                                  int
  , @UPDATED_DTTM                                                datetime
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE SCHEDULE_BUSINESS_RELATIONSHIP_STAGE
       SET
            HOURS_NBR                                                    = @HOURS_NBR
          , CYCLE_NBR                                                    = @CYCLE_NBR
          , BUSINESS_REF_ID                                              = @BUSINESS_REF_ID
          , PROJECT_ID                                                   = @PROJECT_ID
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()

       WHERE
          SCHEDULE_BUSINESS_RELATIONSHIP_STAGE_ID                        = @SCHEDULE_BUSINESS_RELATIONSHIP_STAGE_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating ScheduleBusinessRelationshipStage record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO