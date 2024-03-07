/***********************************************************************************************************************  
* Object:       usp_select_aion_plan_review_schedule_detail_get_by_plan_review_schedule_id  
* Description:  Retrieves PlanReviewScheduleDetail record for given key field(s).  
* Parameters:     
*               @identity                                                    int  
*  
* Returns:      Recordset.  
* Comments:     Developer may need to manually join to other tables, such as code tables,  
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.  
* Version:      1.0  
* Created by:   AION_user  
* Created:      10/6/2021  
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 10/6/2021    AION_user     Auto-generated  
* 2/14/2022	jlindsay adding assigned reviewer name for view model  
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_select_aion_plan_review_schedule_detail_get_by_plan_review_schedule_id] @planReviewScheduleId INT
AS
BEGIN
	IF (
			(
				SELECT count('s')
				FROM AION.PLAN_REVIEW_SCHEDULE_DETAIL
				WHERE PLAN_REVIEW_SCHEDULE_ID = @planReviewScheduleId
					AND ISNULL(ASSIGNED_PLAN_REVIEWER_ID, 0) > 0
				) > 0
			)
	BEGIN
		SELECT detail.PLAN_REVIEW_SCHEDULE_DETAIL_ID,
			detail.PLAN_REVIEW_SCHEDULE_ID,
			detail.BUSINESS_REF_ID,
			detail.START_DT,
			detail.END_DT,
			detail.POOL_REQUEST_IND,
			detail.SAME_BUILD_CONTR_IND,
			detail.MANUAL_ASSIGNMENT_IND,
			detail.ASSIGNED_HOURS_NBR,
			detail.ASSIGNED_PLAN_REVIEWER_ID,
			detail.WKR_ID_CREATED_TXT,
			detail.CREATED_DTTM,
			detail.WKR_ID_UPDATED_TXT,
			detail.UPDATED_DTTM,
			usr.LAST_NM,
			usr.FIRST_NM
		FROM AION.PLAN_REVIEW_SCHEDULE_DETAIL detail
		LEFT JOIN AION.[USER] usr ON detail.ASSIGNED_PLAN_REVIEWER_ID = usr.[USER_ID]
		WHERE PLAN_REVIEW_SCHEDULE_ID = @planReviewScheduleId
	END
	ELSE
	BEGIN
		SELECT detail.PLAN_REVIEW_SCHEDULE_DETAIL_ID,
			detail.PLAN_REVIEW_SCHEDULE_ID,
			detail.BUSINESS_REF_ID,
			detail.START_DT,
			detail.END_DT,
			detail.POOL_REQUEST_IND,
			detail.SAME_BUILD_CONTR_IND,
			detail.MANUAL_ASSIGNMENT_IND,
			detail.ASSIGNED_HOURS_NBR,
			detail.ASSIGNED_PLAN_REVIEWER_ID,
			detail.WKR_ID_CREATED_TXT,
			detail.CREATED_DTTM,
			detail.WKR_ID_UPDATED_TXT,
			detail.UPDATED_DTTM,
			'' AS LAST_NM,
			'' AS FIRST_NM
		FROM AION.PLAN_REVIEW_SCHEDULE_DETAIL detail
		WHERE PLAN_REVIEW_SCHEDULE_ID = @planReviewScheduleId
	END

	RETURN
END
