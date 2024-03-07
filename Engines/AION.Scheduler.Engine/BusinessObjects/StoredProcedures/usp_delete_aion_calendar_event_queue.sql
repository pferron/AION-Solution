
/***********************************************************************************************************************
* Object:       usp_delete_aion_calendar_event_queue
* Description:  Deletes CalendarEventQueue record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      4/13/2021
************************************************************************************************************************
* Change History: Date, Name, Description
* 4/13/2021    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_delete_aion_calendar_event_queue]

    @identity                                                    int

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM CALENDAR_EVENT_QUEUE
       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          CALENDAR_EVENT_QUEUE_ID = @identity
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting CalendarEventQueue record.', 18,1)

RETURN

GO