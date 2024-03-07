
/***********************************************************************************************************************
* Object:       usp_select_aion_aion_user_schedule_stage_get_list
* Description:  Retrieves UserScheduleStage list for given parameter(s).
* Parameters:   
*               @identity                                                   int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      9/25/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 9/25/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

ALTER PROCEDURE [usp_select_aion_user_schedule_stage_get_list]

    @project_id                                                   int

AS

       SELECT 
            USER_SCHEDULE_STAGE_IDENTIFIER
          , START_DTTM
          , END_DTTM
          , USER_ID
          , BUSINESS_REF_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , PROJECT_ID

       FROM USER_SCHEDULE_STAGE

       WHERE
        
          PROJECT_ID = @project_id
          

RETURN

GO