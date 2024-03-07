
/***********************************************************************************************************************
* Object:       usp_select_aion_user_schedule_get_by_id
* Description:  Retrieves UserSchedule record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   AION_user
* Created:      3/19/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 3/19/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_select_aion_user_used_schedules_by_user_id]
 
    @identity                                                    int

AS
        SELECT MIN([USER_SCHEDULE_ID]) AS [USER_SCHEDULE_ID]
        ,MIN([START_DTTM]) AS [START_DTTM]
        ,MAX([END_DTTM]) AS [END_DTTM]
        ,MIN([USER_ID]) AS [USER_ID]
	    ,MIN([PROJECT_SCHEDULE_ID]) AS [PROJECT_SCHEDULE_ID]

        FROM [AION].[USER_SCHEDULE]

        GROUP BY [START_DTTM],[END_DTTM],[USER_ID]

        HAVING [USER_ID] = @identity

        ORDER BY [START_DTTM],[END_DTTM]
   
RETURN

GO