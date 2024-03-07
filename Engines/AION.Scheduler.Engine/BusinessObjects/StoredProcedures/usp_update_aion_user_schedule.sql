
/***********************************************************************************************************************
* Object:       usp_update_aion_user_schedule
* Description:  Updates UserSchedule record using supplied parameters.
* Parameters:   
*               @USER_SCHEDULE_ID                                            int
*               @START_DTTM                                                  datetime
*               @END_DTTM                                                    datetime
*               @PROJECT_SCHEDULE_ID                                         int
*               @USER_BUSINESS_RELATIONSHIP_ID                               int
*               @USER_ID                                                     int
*               @UPDATED_DTTM                                                datetime
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      3/19/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 3/19/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [AION].[usp_update_aion_user_schedule]

    @USER_SCHEDULE_ID                                            int
  , @START_DTTM                                                  datetime
  , @END_DTTM                                                    datetime
  , @PROJECT_SCHEDULE_ID                                         int
  , @USER_BUSINESS_RELATIONSHIP_ID                               int
  , @USER_ID                                                     int
  , @UPDATED_DTTM                                                datetime
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE USER_SCHEDULE
       SET
            START_DTTM                                                   = @START_DTTM
          , END_DTTM                                                     = @END_DTTM
          , PROJECT_SCHEDULE_ID                                          = @PROJECT_SCHEDULE_ID
          , USER_BUSINESS_RELATIONSHIP_ID                                = @USER_BUSINESS_RELATIONSHIP_ID
          , USER_ID                                                      = @USER_ID
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()

       WHERE
          USER_SCHEDULE_ID                                               = @USER_SCHEDULE_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating UserSchedule record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO