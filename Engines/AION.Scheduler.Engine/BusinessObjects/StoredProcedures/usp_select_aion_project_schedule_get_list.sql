
/***********************************************************************************************************************
* Object:       usp_select_aion_project_schedule_get_list
* Description:  Retrieves ProjectSchedule list for given parameter(s).
* Parameters:   
*               @identity                                                   int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      3/19/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 3/19/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_select_aion_project_schedule_get_list]

   

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

     
          

RETURN

GO