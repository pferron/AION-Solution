/***********************************************************************************************************************  
* Object:       usp_select_aion_plan_review_schedule_get_list_by_project_id  
* Description:  Retrieves PlanReviewSchedule list for given parameter(s).  
* Parameters:     
*               @identity                                                   int  
*  
* Returns:      Recordset.  
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.  
*               This proc expects id_person and/or id_file to generate list; modify as necessary.  
*               Include ORDER BY clause as necessary.  
* Version:      1.0  
* Created by:   AION_user  
* Created:      10/6/2021  
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 10/6/2021    AION_user     Auto-generated  
* 11/3/2021 jlindsay remove jba notation  
***********************************************************************************************************************/
CREATE PROCEDURE [usp_select_aion_plan_review_schedule_get_list_by_project_id] @projectId INT
AS
SELECT A.PLAN_REVIEW_SCHEDULE_ID,
	A.PROJECT_CYCLE_ID,
	A.PROJECT_SCHEDULE_TYP_DESC,
	A.IS_RESCHEDULE_IND,
	A.APPT_RESPONSE_STATUS_REF_ID,
	A.APPT_CANCELLATION_REF_ID,
	A.WKR_ID_CREATED_TXT,
	A.CREATED_DTTM,
	A.WKR_ID_UPDATED_TXT,
	A.UPDATED_DTTM,
	A.VIRTUAL_MEETING_IND,
	A.PROPOSED_1_DT,
	A.PROPOSED_2_DT,
	A.PROPOSED_3_DT,
	A.CANCEL_AFTER_DT,
	A.MEETING_ROOM_REF_ID
FROM PLAN_REVIEW_SCHEDULE A
INNER JOIN PROJECT_CYCLE B ON A.PROJECT_CYCLE_ID = B.PROJECT_CYCLE_ID
WHERE B.PROJECT_ID = @projectId
	AND B.CURRENT_CYCLE_IND = 1

RETURN
