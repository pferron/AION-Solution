/***********************************************************************************************************************  
* Object:       usp_update_aion_plan_review_schedule
* Description:  Updates PlanReviewSchedule record using supplied parameters.  
* Parameters:     
*               @PLAN_REVIEW_SCHEDULE_ID                                     int  
*               @PROJECT_CYCLE_ID                                            int  
*               @PROJECT_SCHEDULE_TYP_DESC                                   varchar(100)  
*               @IS_RESCHEDULE_IND                                           bit  
*               @APPT_RESPONSE_STATUS_REF_ID                                 int  
*               @APPT_CANCELLATION_REF_ID                                    int  
*               @UPDATED_DTTM                                                datetime  
*               @VIRTUAL_MEETING_IND                                         bit  
*               @PROPOSED_1_DT                                               datetime  
*               @PROPOSED_2_DT                                               datetime  
*               @PROPOSED_3_DT                                               datetime  
*               @CANCEL_AFTER_DT                                             datetime  
*               @MEETING_ROOM_REF_ID                                         int  
*               @WKR_ID_TXT                                                  varchar(100)  
*  
* Returns:      Number of rows affected.  
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.  
* Version:      1.0  
* Created by:   AION_user  
* Created:      10/28/2021  
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 10/28/2021    AION_user     Auto-generated  
* 11/3/2021	jlindsay remove jba notation  
***********************************************************************************************************************/
CREATE PROCEDURE [usp_update_aion_plan_review_schedule] @PLAN_REVIEW_SCHEDULE_ID INT,
	@PROJECT_CYCLE_ID INT,
	@PROJECT_SCHEDULE_TYP_DESC VARCHAR(100),
	@IS_RESCHEDULE_IND BIT,
	@APPT_RESPONSE_STATUS_REF_ID INT,
	@APPT_CANCELLATION_REF_ID INT,
	@UPDATED_DTTM DATETIME,
	@VIRTUAL_MEETING_IND BIT,
	@PROPOSED_1_DT DATETIME,
	@PROPOSED_2_DT DATETIME,
	@PROPOSED_3_DT DATETIME,
	@CANCEL_AFTER_DT DATETIME,
	@MEETING_ROOM_REF_ID INT,
	@WKR_ID_TXT VARCHAR(100),
	@ReturnValue INT OUTPUT
AS
DECLARE @error INT

UPDATE PLAN_REVIEW_SCHEDULE
SET PROJECT_CYCLE_ID = @PROJECT_CYCLE_ID,
	PROJECT_SCHEDULE_TYP_DESC = @PROJECT_SCHEDULE_TYP_DESC,
	IS_RESCHEDULE_IND = @IS_RESCHEDULE_IND,
	APPT_RESPONSE_STATUS_REF_ID = @APPT_RESPONSE_STATUS_REF_ID,
	APPT_CANCELLATION_REF_ID = @APPT_CANCELLATION_REF_ID,
	WKR_ID_UPDATED_TXT = @WKR_ID_TXT,
	UPDATED_DTTM = GETDATE(),
	VIRTUAL_MEETING_IND = @VIRTUAL_MEETING_IND,
	PROPOSED_1_DT = @PROPOSED_1_DT,
	PROPOSED_2_DT = @PROPOSED_2_DT,
	PROPOSED_3_DT = @PROPOSED_3_DT,
	CANCEL_AFTER_DT = @CANCEL_AFTER_DT,
	MEETING_ROOM_REF_ID = @MEETING_ROOM_REF_ID
WHERE PLAN_REVIEW_SCHEDULE_ID = @PLAN_REVIEW_SCHEDULE_ID
	AND ISNULL(CONVERT(VARCHAR(19), UPDATED_DTTM, 120), '') = ISNULL(CONVERT(VARCHAR(19), @UPDATED_DTTM, 120), '')

SELECT @error = @@ERROR,
	@ReturnValue = @@ROWCOUNT

IF @error != 0
	RAISERROR (
			'Error updating PlanReviewSchedule record.',
			18,
			1
			)

IF @ReturnValue = 0
	RAISERROR (
			'Data was changed/deleted prior to update.',
			18,
			100
			)

RETURN
