/***********************************************************************************************************************  
* Object:       usp_select_aion_plan_review_schedule_get_list  
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
* Created:      10/28/2021  
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 10/28/2021    AION_user     Auto-generated  
*  11/3/2021 jlindsay remove jba notation 
***********************************************************************************************************************/
CREATE PROCEDURE [usp_select_aion_plan_review_schedule_get_list] @identity INT
AS
SELECT PLAN_REVIEW_SCHEDULE_ID,
	PROJECT_CYCLE_ID,
	PROJECT_SCHEDULE_TYP_DESC,
	IS_RESCHEDULE_IND,
	APPT_RESPONSE_STATUS_REF_ID,
	APPT_CANCELLATION_REF_ID,
	WKR_ID_CREATED_TXT,
	CREATED_DTTM,
	WKR_ID_UPDATED_TXT,
	UPDATED_DTTM,
	VIRTUAL_MEETING_IND,
	PROPOSED_1_DT,
	PROPOSED_2_DT,
	PROPOSED_3_DT,
	CANCEL_AFTER_DT,
	MEETING_ROOM_REF_ID
FROM PLAN_REVIEW_SCHEDULE
WHERE
	-- @TODO:  Correct the following as necessary  
	PLAN_REVIEW_SCHEDULE_ID = @identity

RETURN
