/****** Object:  StoredProcedure [AION].[usp_select_aion_sync_failure_notification_get_list]    Script Date: 11/14/2023 6:40:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:       usp_select_aion_sync_failure_notification_get_list
* Description:  Retrieves SyncFailureNotification list for given parameter(s).
* Parameters:   
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      11/13/2023
************************************************************************************************************************
* Change History: Date, Name, Description
* 11/13/2023    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [AION].[usp_select_aion_sync_failure_notification_get_list]

AS
	   declare @currentDate as datetime = GETDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'Eastern Standard Time';

       SELECT LAST_FAILURE_NOTIFICATION_DT

       FROM SYNC_FAILURE_NOTIFICATION

       WHERE LAST_FAILURE_NOTIFICATION_DT >= DATEADD(HOUR, -1, @currentDate)
         

RETURN

