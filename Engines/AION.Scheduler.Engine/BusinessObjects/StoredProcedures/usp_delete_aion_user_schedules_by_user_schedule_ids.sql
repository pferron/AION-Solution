SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:       usp_delete_aion_user_schedules_by_user_schedule_ids
* Description:  Deletes UserSchedule records for a given set of ids.
* Parameters:   
*               @userScheduleIds                                                    string
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   jallen
* Created:      08/24/2021
************************************************************************************************************************
* Change History: Date, Name, Description
* 08/24/2021    jallen     Created
***********************************************************************************************************************/

CREATE PROCEDURE [AION].[usp_delete_aion_user_schedules_by_user_schedule_ids]
    @userScheduleIds       varchar(MAX)
  , @ReturnValue           int OUTPUT


AS

	  CREATE TABLE #UserScheduleIds (
			UserScheduleId varchar(255)
		);

	  INSERT INTO #UserScheduleIds select value from String_Split(@userScheduleIds, ',')

      DELETE FROM USER_SCHEDULE 

      WHERE USER_SCHEDULE_ID in (select cast(UserScheduleId as int) from #UserScheduleIds)
      
	  SELECT @ReturnValue = @@ROWCOUNT

RETURN

