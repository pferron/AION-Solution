/***********************************************************************************************************************  
* Object:       usp_select_aion_excluded_plan_reviewers_get_list  
* Description:  Retrieves ExcludedPlanReviewers list for given parameter(s).  
* Parameters:     
*               @identity                                                   int  
*  
* Returns:      Recordset.  
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.  
*               This proc expects id_person and/or id_file to generate list; modify as necessary.  
*               Include ORDER BY clause as necessary.  
* Version:      1.0  
* Created by:   AION_user  
* Created:      10/2/2019  
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 10/2/2019    AION_user     Auto-generated  
* 4/20/2021	jlindsay		get the business ref id
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_select_aion_excluded_plan_reviewers_get_list_by_PrjDeptID] @prjdeptID INT
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
	WHERE r.PROJECT_BUSINESS_RELATIONSHIP_ID = @prjdeptID

	RETURN
END
