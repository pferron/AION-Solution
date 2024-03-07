
/***********************************************************************************************************************
* Object:       usp_select_aion_plan_reviewer_available_time_get_by_id
* Description:  Retrieves PlanReviewerAvailableTime record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   AION_user
* Created:      8/28/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 8/28/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_plan_reviewer_available_time_get_by_id]

    @identity                                                    int

AS

       SELECT 
            PLAN_REVIEWER_AVAILABLE_TM_ID
          , AVAILABLE_START_TM
          , AVAILABLE_END_TM
          , PROJECT_TYP_DESC
          , PROJECT_TYP_REF_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM

       FROM PLAN_REVIEWER_AVAILABLE_TIME

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          PLAN_REVIEWER_AVAILABLE_TM_ID = @identity
          

RETURN

GO