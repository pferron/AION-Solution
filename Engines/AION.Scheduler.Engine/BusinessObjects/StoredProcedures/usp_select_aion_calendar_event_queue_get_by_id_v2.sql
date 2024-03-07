
/***********************************************************************************************************************
* Object:       usp_select_aion_calendar_event_queue_get_by_id_v2
* Description:  Retrieves CalendarEventQueue record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   AION_user
* Created:      4/13/2021
************************************************************************************************************************
* Change History: Date, Name, Description
* 4/13/2021    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_calendar_event_queue_get_by_id_v2]

    @identity                                                    int

AS

       SELECT 
            CALENDAR_EVENT_QUEUE_ID
          , JSON_OBJECT_TXT
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

       FROM CALENDAR_EVENT_QUEUE

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          CALENDAR_EVENT_QUEUE_ID = @identity
          

RETURN

GO