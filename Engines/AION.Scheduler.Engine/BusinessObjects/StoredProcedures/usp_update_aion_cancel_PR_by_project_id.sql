/***********************************************************************************************************************          
* Object:      [usp_update_aion_cancel_PR_by_project_id]        
*               @PROJECT_ID int
*				@USER_ID int
* Returns:      list of PLAN_REVIEW_SCHEDULE_ID affected          
* Comments:             
* Version:      1.0          
* Created by:   jlindsay          
* Created:      11/17/2021          
************************************************************************************************************************          
* Change History: Date, Name, Description          
*  11/17/2021    jlindsay        
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_update_aion_cancel_PR_by_project_id] @PROJECT_ID INT,
	@USER_ID INT
AS
BEGIN

	--    
	--'Cancelled'    
	DECLARE @CANCELLED_APPT_RESPONSE_STATUS_REF_ID INT = 0;

	SELECT @CANCELLED_APPT_RESPONSE_STATUS_REF_ID = APPT_RESPONSE_STATUS_REF_ID
	FROM AION.APPOINTMENT_RESPONSE_STATUS_REF
	WHERE ENUM_MAPPING_VAL_NBR = 7;

	--'Cancellation Reason'    
	DECLARE @CANCELLED_APPT_REASON_REF_ID INT = 0;

	SELECT @CANCELLED_APPT_REASON_REF_ID = APPT_CANCELLATION_REF_ID
	FROM AION.APPOINTMENT_CANCELLATION_REF
	WHERE ENUM_MAPPING_VAL_NBR = 1;

	DECLARE @planReviewSchedules TABLE (PLAN_REVIEW_SCHEDULE_ID INT)

	INSERT INTO @planReviewSchedules
	SELECT PLAN_REVIEW_SCHEDULE_ID
	FROM AION.PLAN_REVIEW_SCHEDULE prs
	WHERE PROJECT_SCHEDULE_TYP_DESC = 'PR'
		AND PROJECT_CYCLE_ID IN (
			SELECT PROJECT_CYCLE_ID
			FROM PROJECT_CYCLE
			WHERE PROJECT_ID = @PROJECT_ID
			)

	UPDATE AION.PLAN_REVIEW_SCHEDULE
	SET APPT_RESPONSE_STATUS_REF_ID = @CANCELLED_APPT_RESPONSE_STATUS_REF_ID,
		APPT_CANCELLATION_REF_ID = @CANCELLED_APPT_REASON_REF_ID,
		UPDATED_DTTM = GETDATE(),
		WKR_ID_UPDATED_TXT = @USER_ID
	WHERE PLAN_REVIEW_SCHEDULE_ID IN (
			SELECT PLAN_REVIEW_SCHEDULE_ID
			FROM @planReviewSchedules
			)

	SELECT PLAN_REVIEW_SCHEDULE_ID
	FROM @planReviewSchedules

	RETURN;
END;
