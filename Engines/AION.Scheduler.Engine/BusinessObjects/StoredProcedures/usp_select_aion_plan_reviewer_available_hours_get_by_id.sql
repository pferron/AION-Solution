
/***********************************************************************************************************************
* Object:       usp_select_aion_plan_reviewer_available_hours_get_by_id
* Description:  Retrieves PlanReviewerAvailableHours record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   AION_user
* Created:      3/18/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 3/18/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [AION].[usp_select_aion_plan_reviewer_available_hours_get_by_id]

    @identity                                                    int

AS

       SELECT 
            PLAN_REVIEWER_AVAILABLE_HOURS_ID
          , AVAILABLE_HOURS_NBR
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , ENUM_MAPPING_VAL_NBR
          , PLAN_REVIEWER_TYP_DESC

       FROM PLAN_REVIEWER_AVAILABLE_HOURS

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          PLAN_REVIEWER_AVAILABLE_HOURS_ID = @identity
          

RETURN

GO