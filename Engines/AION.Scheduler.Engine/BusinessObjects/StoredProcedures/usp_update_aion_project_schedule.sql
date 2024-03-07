
/***********************************************************************************************************************
* Object:       usp_update_aion_project_schedule
* Description:  Updates ProjectSchedule record using supplied parameters.
* Parameters:   
*               @PROJECT_SCHEDULE_ID                                         int
*               @PROJECT_SCHEDULE_TYP_DESC                                   varchar(100)
*               @APPT_ID                                                     int
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

ALTER PROCEDURE [AION].[usp_update_aion_project_schedule]

    @PROJECT_SCHEDULE_ID                                         int
  , @PROJECT_SCHEDULE_TYP_DESC                                   varchar(100)
  , @APPT_ID                                                     int
  , @UPDATED_DTTM                                                datetime
  , @WKR_ID_TXT                                                  varchar(100)
  , @RECURRING_APPT_DT                                           datetime

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE PROJECT_SCHEDULE
       SET
            PROJECT_SCHEDULE_TYP_DESC                                    = @PROJECT_SCHEDULE_TYP_DESC
          , APPT_ID                                                      = @APPT_ID
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()
		  , RECURRING_APPT_DT											 = @RECURRING_APPT_DT

       WHERE
          PROJECT_SCHEDULE_ID                                            = @PROJECT_SCHEDULE_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating ProjectSchedule record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO