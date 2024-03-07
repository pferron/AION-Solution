
/***********************************************************************************************************************
* Object:	usp_insert_aion_user_schedule_stage_userschedulestage
* Description:	Inserts UserScheduleStage record.
* Parameters:
*		@START_DTTM                                                  datetime
*		@END_DTTM                                                    datetime
*		@PROJECT_SCHEDULE_ID                                         int
*		@USER_ID                                                     int
*		@BUSINESS_REF_ID                                             int
*		@PROJECT_ID                                                  int
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      9/25/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 9/25/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_insert_aion_user_schedule_stage]
    @START_DTTM                                                  datetime
  , @END_DTTM                                                    datetime
  , @PROJECT_SCHEDULE_ID                                         int
  , @USER_ID                                                     int
  , @BUSINESS_REF_ID                                             int
  , @PROJECT_ID                                                  int
  , @WKR_ID_TXT                                                  varchar(100)
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO USER_SCHEDULE_STAGE
          (
            START_DTTM
          , END_DTTM
          , PROJECT_SCHEDULE_ID
          , USER_ID
          , BUSINESS_REF_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , PROJECT_ID
          )
     VALUES
          (
            @START_DTTM
          , @END_DTTM
          , @PROJECT_SCHEDULE_ID
          , @USER_ID
          , @BUSINESS_REF_ID
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          , @PROJECT_ID
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding UserScheduleStage record.', 18,1)

RETURN
GO