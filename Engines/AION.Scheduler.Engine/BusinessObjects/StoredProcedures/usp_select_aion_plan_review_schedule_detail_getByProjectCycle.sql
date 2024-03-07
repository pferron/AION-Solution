/***********************************************************************************************************************  
* Object:       usp_select_aion_plan_review_schedule_detail_getByProjectCycle 
* Description:  Gets assigned hours for BEMP for project cycle 
* Parameters:     
*               @projectid                                                   int ,
*               @cyclenbr                                                   int 
*  
* Returns:      Recordset.  
* Comments:     
* Version:      1.0  
* Created by:   jlindsay 
* Created:      01/20/2022 
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 01/20/2022   jlindsay     initial
*   
***********************************************************************************************************************/
ALTER PROCEDURE [usp_select_aion_plan_review_schedule_detail_getByProjectCycle] @projectid INT,
	@cyclenbr INT
AS
BEGIN
	SELECT dtl.PLAN_REVIEW_SCHEDULE_DETAIL_ID,
		dtl.PLAN_REVIEW_SCHEDULE_ID,
		dtl.BUSINESS_REF_ID,
		dtl.START_DT,
		dtl.END_DT,
		dtl.POOL_REQUEST_IND,
		dtl.SAME_BUILD_CONTR_IND,
		dtl.MANUAL_ASSIGNMENT_IND,
		dtl.ASSIGNED_HOURS_NBR,
		dtl.ASSIGNED_PLAN_REVIEWER_ID,
		dtl.WKR_ID_CREATED_TXT,
		dtl.CREATED_DTTM,
		dtl.WKR_ID_UPDATED_TXT,
		dtl.UPDATED_DTTM
	FROM PLAN_REVIEW_SCHEDULE_DETAIL dtl
	INNER JOIN PLAN_REVIEW_SCHEDULE sch ON dtl.PLAN_REVIEW_SCHEDULE_ID = sch.PLAN_REVIEW_SCHEDULE_ID
	INNER JOIN PROJECT_CYCLE pc ON sch.PROJECT_CYCLE_ID = pc.PROJECT_CYCLE_ID
	INNER JOIN BUSINESS_REF br ON dtl.BUSINESS_REF_ID = br.BUSINESS_REF_ID
	WHERE pc.PROJECT_ID = @projectid
		AND pc.CYCLE_NBR = @cyclenbr
		AND br.ENUM_MAPPING_VAL_NBR IN (
			1,
			2,
			3,
			4
			)
		AND dtl.ASSIGNED_HOURS_NBR > 0;
END
