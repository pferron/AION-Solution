
/***********************************************************************************************************************
* Object:       usp_select_aion_plan_review_project_details_get_list
* Description:  Retrieves PlanReviewProjectDetails list for given parameter(s).
* Parameters:   
*               @identity                                                   int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      9/3/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 9/3/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_plan_review_project_details_get_list]

    @identity                                                   int

AS

       SELECT 
            PLAN_REVIEW_PROJECT_DETAILS_ID
          , PROJECT_ID
          , RESPONDER_USER_IDENTIFIER
          , IS_APRV_IND
          , RESPONSE_DT
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM

       FROM PLAN_REVIEW_PROJECT_DETAILS

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          PLAN_REVIEW_PROJECT_DETAILS_ID = @identity
          

RETURN

GO