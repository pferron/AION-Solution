/****** Object:  StoredProcedure [AION].[usp_select_aion_calendar_event_queue_get_list]    Script Date: 4/14/2021 2:54:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:       usp_select_aion_calendar_event_queue_get_list
* Description:  Retrieves CalendarEventQueue list for given parameter(s).
* Parameters:   
*               @identity                                                   int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      4/13/2021
************************************************************************************************************************
* Change History: Date, Name, Description
* 4/13/2021    AION_user     Auto-generated
* 4/14/2021    jallen	     Created for getting records by their process status
***********************************************************************************************************************/

CREATE PROCEDURE [AION].[usp_select_aion_calendar_event_queue_get_list_for_processing]

    @PROCESSED_IND bit,
	@IN_PROCESS_IND bit

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
        
		PROCESSED_IND = @PROCESSED_IND
		AND  IN_PROCESS_IND = @IN_PROCESS_IND 
        AND  RETRY_CNT <= 3

		ORDER BY CREATED_DTTM
          

RETURN


