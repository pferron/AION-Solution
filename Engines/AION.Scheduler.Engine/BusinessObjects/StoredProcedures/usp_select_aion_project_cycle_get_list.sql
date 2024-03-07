
/***********************************************************************************************************************
* Object:       usp_select_aion_project_cycle_get_list
* Description:  Retrieves ProjectCycle list for given parameter(s).
* Parameters:   
*               @identity                                                   int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      10/11/2021
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/11/2021    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_project_cycle_get_list]

    @identity                                                   int

AS

       SELECT 
            PROJECT_CYCLE_ID
          , PROJECT_ID
          , CURRENT_CYCLE_IND
          , FUTURE_CYCLE_IND
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , CYCLE_NBR
          , PLANS_READY_ON_DT
          , IS_COMPLETE_IND
          , GATE_DT
          , SCHEDULE_AFTER_DT
          , RESPONDER_USER_ID
          , IS_APRV_IND
          , RESPONSE_DT
          , INCREMENT_ON_PLANS_RECEIVED_IND

       FROM PROJECT_CYCLE

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          PROJECT_CYCLE_ID = @identity
          

RETURN

GO