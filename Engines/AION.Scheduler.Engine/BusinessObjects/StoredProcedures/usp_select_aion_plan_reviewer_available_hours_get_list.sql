/***********************************************************************************************************************  
* Object:       usp_select_aion_plan_reviewer_available_hours_get_list  
* Description:  Retrieves PlanReviewerAvailableHours list for given parameter(s).  
* Parameters:     
*               @identity                                                   int  
*  
* Returns:      Recordset.  
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.  
*               This proc expects id_person and/or id_file to generate list; modify as necessary.  
*               Include ORDER BY clause as necessary.  
* Version:      1.0  
* Created by:   AION_user  
* Created:      3/18/2020  
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 3/18/2020    AION_user     Auto-generated  
* 12/14/2022    jlindsay    update to use the project_type_ref table
*                           select only values that have the hours enum mapping value
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_select_aion_plan_reviewer_available_hours_get_list]
AS
BEGIN
	SELECT PROJECT_TYP_REF_ID,
		AVAILABLE_HOURS_NBR,
		WKR_ID_CREATED_TXT,
		CREATED_DTTM,
		WKR_ID_UPDATED_TXT,
		UPDATED_DTTM,
		AVAILABLE_HOURS_ENUM_MAPPING_VAL_NBR,
		AVAILABLE_HOURS_PLAN_REVIEWER_TYP_DESC
	FROM AION.PROJECT_TYPE_REF
	WHERE ISNULL(AVAILABLE_HOURS_ENUM_MAPPING_VAL_NBR, '') != ''

	RETURN
END
