
/***********************************************************************************************************************
* Object:       usp_select_aion_aion_user_schedule_stage_get_by_id
* Description:  Retrieves UserScheduleStage record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   AION_user
* Created:      9/25/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 9/25/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_user_schedule_stage_get_by_id]

    @identity                                                    int

AS

       SELECT 
            USER_SCHEDULE_STAGE_IDENTIFIER
          , START_DTTM
          , END_DTTM
          , PROJECT_SCHEDULE_ID
          , USER_ID
          , BUSINESS_REF_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , PROJECT_ID

       FROM USER_SCHEDULE_STAGE

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          USER_SCHEDULE_STAGE_IDENTIFIER = @identity
          

RETURN

GO