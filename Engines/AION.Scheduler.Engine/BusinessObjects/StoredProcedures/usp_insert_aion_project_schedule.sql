
/***********************************************************************************************************************
* Object:	usp_insert_aion_project_schedule
* Description:	Inserts ProjectSchedule record.
* Parameters:
*		@PROJECT_SCHEDULE_TYP_DESC                                   varchar(100)
*		@APPT_ID                                                     int
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      3/19/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 3/19/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_insert_aion_project_schedule]
    @PROJECT_SCHEDULE_TYP_DESC                                   varchar(100)
  , @APPT_ID                                                     int
  , @WKR_ID_TXT                                                  varchar(100)
  , @RECURRING_APPT_DT                                           datetime
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO PROJECT_SCHEDULE
          (
            PROJECT_SCHEDULE_TYP_DESC
          , APPT_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
		  , RECURRING_APPT_DT
          )
     VALUES
          (
            @PROJECT_SCHEDULE_TYP_DESC
          , @APPT_ID
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
		  , @RECURRING_APPT_DT
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding ProjectSchedule record.', 18,1)

RETURN
GO