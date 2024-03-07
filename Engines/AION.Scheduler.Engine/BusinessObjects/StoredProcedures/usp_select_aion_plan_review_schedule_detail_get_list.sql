
/***********************************************************************************************************************
* Object:       usp_select_aion_plan_review_schedule_detail_get_list
* Description:  Retrieves PlanReviewScheduleDetail list for given parameter(s).
* Parameters:   
*               @identity                                                   int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      10/12/2021
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/12/2021    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_plan_review_schedule_detail_get_list]

    @identity                                                   int

AS

       SELECT 
            PLAN_REVIEW_SCHEDULE_DETAIL_ID
          , PLAN_REVIEW_SCHEDULE_ID
          , BUSINESS_REF_ID
          , START_DT
          , END_DT
          , POOL_REQUEST_IND
          , SAME_BUILD_CONTR_IND
          , MANUAL_ASSIGNMENT_IND
          , ASSIGNED_HOURS_NBR
          , ASSIGNED_PLAN_REVIEWER_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM

       FROM PLAN_REVIEW_SCHEDULE_DETAIL

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          PLAN_REVIEW_SCHEDULE_DETAIL_ID = @identity
          

RETURN

GO