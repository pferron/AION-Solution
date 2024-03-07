/***********************************************************************************************************************  
* Object:       usp_select_aion_plan_review_schedule_get_list_by_dates  
* Description:  Retrieves PlanReviewSchedule list for given parameter(s).  
* Parameters:     
*               @START_DT datetime,  
*               @END_DT   datetime,  
*               @PROJECT_SCHEDULE_TYP_DESC varchar(100)  
*  
* Returns:      Recordset.  
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.  
*               This proc expects id_person and/or id_file to generate list; modify as necessary.  
*               Include ORDER BY clause as necessary.  
* Version:      1.0  
* Created by:   AION_user  
* Created:      11/01/2021  
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 11/01/2021    AION_user     Auto-generated  
* 11/3/2021 jlindsay removed jba notation 
* 01/20/2022 jallen  Get only scheduled statuses
* 08/08/2022 jallen  Update enum to check for tentative scheduled status
* 08/10/2022 jallen  Order results by start date
***********************************************************************************************************************/
ALTER PROCEDURE [usp_select_aion_plan_review_schedule_get_list_by_dates] @START_DT DATETIME,
	@END_DT DATETIME,
	@PROJECT_SCHEDULE_TYP_DESC VARCHAR(100)
AS

DECLARE @scheduledstatus INT
DECLARE @tentativestatus INT

SELECT @scheduledstatus = APPT_RESPONSE_STATUS_REF_ID
        FROM [AION].[APPOINTMENT_RESPONSE_STATUS_REF]
        WHERE ENUM_MAPPING_VAL_NBR = 5;

SELECT @tentativestatus = APPT_RESPONSE_STATUS_REF_ID
        FROM [AION].[APPOINTMENT_RESPONSE_STATUS_REF]
        WHERE ENUM_MAPPING_VAL_NBR = 9;

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
INNER JOIN PLAN_REVIEW_SCHEDULE_DETAIL B ON A.PLAN_REVIEW_SCHEDULE_ID = B.PLAN_REVIEW_SCHEDULE_ID
WHERE
	-- @TODO:  Correct the following as necessary  
	A.PROJECT_SCHEDULE_TYP_DESC = @PROJECT_SCHEDULE_TYP_DESC
	AND CAST(B.START_DT AS DATE) >= CAST(@START_DT AS DATE)
	AND CAST(B.END_DT AS DATE) <= CAST(@END_DT AS DATE)
	AND A.APPT_RESPONSE_STATUS_REF_ID IN (@scheduledstatus,@tentativestatus)

GROUP BY A.PLAN_REVIEW_SCHEDULE_ID,
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
	A.MEETING_ROOM_REF_ID,
	B.START_DT

ORDER BY B.START_DT

RETURN
