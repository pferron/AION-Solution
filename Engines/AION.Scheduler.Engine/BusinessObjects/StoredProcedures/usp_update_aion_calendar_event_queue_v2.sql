
/***********************************************************************************************************************
* Object:       usp_update_aion_calendar_event_queue_v2
* Description:  Updates CalendarEventQueue record using supplied parameters.
* Parameters:   
*               @CALENDAR_EVENT_QUEUE_ID                                     int
*               @JSON_OBJECT_TXT                                             varchar(MAX)
*               @ACTION_DESC                                                 varchar(50)
*               @PROCESSED_IND                                               bit
*               @PROCESSED_DTTM                                              datetime
*               @IN_PROCESS_IND                                              bit
*               @IN_PROCESS_DTTM                                             datetime
*               @UPDATED_DTTM                                                datetime
*               @WKR_ID_TXT                                                  varchar(100)
*               @RETRY_CNT                                                   int
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      4/13/2021
************************************************************************************************************************
* Change History: Date, Name, Description
* 4/13/2021    AION_user     Auto-generated
* 12/09/2021   jallen        add retry count
***********************************************************************************************************************/

CREATE PROCEDURE [usp_update_aion_calendar_event_queue_v2]

    @CALENDAR_EVENT_QUEUE_ID                                     int
  , @JSON_OBJECT_TXT                                             varchar(MAX)
  , @ACTION_DESC                                                 varchar(50)
  , @PROCESSED_IND                                               bit
  , @PROCESSED_DTTM                                              datetime
  , @IN_PROCESS_IND                                              bit
  , @IN_PROCESS_DTTM                                             datetime
  , @UPDATED_DTTM                                                datetime
  , @WKR_ID_TXT                                                  varchar(100)
  , @RETRY_CNT                                                   int


  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE CALENDAR_EVENT_QUEUE
       SET
            JSON_OBJECT_TXT                                              = @JSON_OBJECT_TXT
          , ACTION_DESC                                                  = @ACTION_DESC
          , PROCESSED_IND                                                = @PROCESSED_IND
          , PROCESSED_DTTM                                               = @PROCESSED_DTTM
          , IN_PROCESS_IND                                               = @IN_PROCESS_IND
          , IN_PROCESS_DTTM                                              = @IN_PROCESS_DTTM
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()
          , RETRY_CNT                                                    = @RETRY_CNT

       WHERE
          CALENDAR_EVENT_QUEUE_ID                                        = @CALENDAR_EVENT_QUEUE_ID       
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating CalendarEventQueue record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO