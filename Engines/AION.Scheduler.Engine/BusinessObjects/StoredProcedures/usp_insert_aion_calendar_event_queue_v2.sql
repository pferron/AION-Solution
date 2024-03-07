
/***********************************************************************************************************************
* Object:	usp_insert_aion_calendar_event_queue_v2
* Description:	Inserts CalendarEventQueue record.
* Parameters:
*		@JSON_OBJECT_TXT                                             varchar(0)
*		@ACTION_DESC                                                 varchar(50)
*		@PROCESSED_IND                                               bit
*		@PROCESSED_DTTM                                              datetime
*		@IN_PROCESS_IND                                              bit
*		@IN_PROCESS_DTTM                                             datetime
*		@WKR_ID_TXT                                                  varchar(100)
*		@RETRY_CNT                                                   int
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      4/13/2021
************************************************************************************************************************
* Change History: Date, Name, Description
* 4/13/2021    AION_user     Auto-generated
* 12/09/2021   jallen        add retry count
***********************************************************************************************************************/

CREATE PROCEDURE [usp_insert_aion_calendar_event_queue_v2]
    @JSON_OBJECT_TXT                                             varchar(MAX)
  , @ACTION_DESC                                                 varchar(50)
  , @PROCESSED_IND                                               bit
  , @PROCESSED_DTTM                                              datetime
  , @IN_PROCESS_IND                                              bit
  , @IN_PROCESS_DTTM                                             datetime
  , @WKR_ID_TXT                                                  varchar(100)
  , @RETRY_CNT                                                   int
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO CALENDAR_EVENT_QUEUE
          (
            JSON_OBJECT_TXT
          , ACTION_DESC
          , PROCESSED_IND
          , PROCESSED_DTTM
          , IN_PROCESS_IND
          , IN_PROCESS_DTTM
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , RETRY_CNT
          )
     VALUES
          (
            @JSON_OBJECT_TXT
          , @ACTION_DESC
          , @PROCESSED_IND
          , @PROCESSED_DTTM
          , @IN_PROCESS_IND
          , @IN_PROCESS_DTTM
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          , @RETRY_CNT
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding CalendarEventQueue record.', 18,1)

RETURN
GO