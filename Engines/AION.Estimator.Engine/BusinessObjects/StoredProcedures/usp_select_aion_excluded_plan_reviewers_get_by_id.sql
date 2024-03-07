/***********************************************************************************************************************  
* Object:       usp_select_aion_excluded_plan_reviewers_get_by_id  
* Description:  Retrieves ExcludedPlanReviewers record for projectid 
* Parameters:     
*               @identity                                                    int  
*  
* Returns:      Recordset.  
* Comments:     Developer may need to manually join to other tables, such as code tables,  
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.  
* Version:      1.0  
* Created by:   AION_user  
* Created:      10/2/2019  
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 10/2/2019    AION_user     Auto-generated  
* 4/20/2021	jlindsay		get the list by the project id  
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_select_aion_excluded_plan_reviewers_get_by_id] @identity INT
AS
BEGIN
	SELECT r.[EXCLUDED_PLAN_REVIEWERS_ID],
		r.[PLAN_REVIEWER_ID],
		r.[PROJECT_BUSINESS_RELATIONSHIP_ID],
		r.[WKR_ID_CREATED_TXT],
		r.[CREATED_DTTM],
		r.[WKR_ID_UPDATED_TXT],
		r.[UPDATED_DTTM],
		p.[BUSINESS_REF_ID]
	FROM EXCLUDED_PLAN_REVIEWERS r
	INNER JOIN PROJECT_BUSINESS_RELATIONSHIP p ON r.[PROJECT_BUSINESS_RELATIONSHIP_ID] = p.[PROJECT_BUSINESS_RELATIONSHIP_ID]
	WHERE p.[PROJECT_ID] = @identity

	RETURN
END
