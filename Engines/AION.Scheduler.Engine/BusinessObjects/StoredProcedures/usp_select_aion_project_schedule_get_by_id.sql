
/***********************************************************************************************************************
* Object:       usp_select_aion_project_schedule_get_by_id
* Description:  Retrieves ProjectSchedule record for given key field(s).
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

ALTER PROCEDURE [AION].[usp_select_aion_project_schedule_get_by_id]

    @identity                                                    int

AS

       SELECT 
            PROJECT_SCHEDULE_ID
          , PROJECT_SCHEDULE_TYP_DESC
          , APPT_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
		  , RECURRING_APPT_DT

       FROM PROJECT_SCHEDULE

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          PROJECT_SCHEDULE_ID = @identity
          

RETURN

GO