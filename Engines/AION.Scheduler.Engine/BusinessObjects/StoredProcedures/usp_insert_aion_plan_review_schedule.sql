/***********************************************************************************************************************  
* Object: usp_insert_aion_plan_review_schedule 
* Description: Inserts PlanReviewSchedule record.  
* Parameters:  
*  @PROJECT_CYCLE_ID                                            int  
*  @PROJECT_SCHEDULE_TYP_DESC                                   varchar(100)  
*  @IS_RESCHEDULE_IND                                           bit  
*  @APPT_RESPONSE_STATUS_REF_ID                                 int  
*  @APPT_CANCELLATION_REF_ID                                    int  
*  @VIRTUAL_MEETING_IND                                         bit  
*  @PROPOSED_1_DT                                               datetime  
*  @PROPOSED_2_DT                                               datetime  
*  @PROPOSED_3_DT                                               datetime  
*  @CANCEL_AFTER_DT                                             datetime  
*  @MEETING_ROOM_REF_ID                                         int  
*  @WKR_ID_TXT                                                  varchar(100)  
*  
* Returns:      Identity column of new record.  
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.  
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.  
* Version:      1.0  
* Created by:   AION_user  
* Created:      10/28/2021  
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 10/28/2021    AION_user     Auto-generated  
* 11/3/2021 jlindsay remove jba notation  
***********************************************************************************************************************/
CREATE PROCEDURE [usp_insert_aion_plan_review_schedule] @PROJECT_CYCLE_ID INT,
	@PROJECT_SCHEDULE_TYP_DESC VARCHAR(100),
	@IS_RESCHEDULE_IND BIT,
	@APPT_RESPONSE_STATUS_REF_ID INT,
	@APPT_CANCELLATION_REF_ID INT,
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

INSERT INTO PLAN_REVIEW_SCHEDULE (
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
	)
VALUES (
	@PROJECT_CYCLE_ID,
	@PROJECT_SCHEDULE_TYP_DESC,
	@IS_RESCHEDULE_IND,
	@APPT_RESPONSE_STATUS_REF_ID,
	@APPT_CANCELLATION_REF_ID,
	@WKR_ID_TXT,
	GETDATE(),
	@WKR_ID_TXT,
	GETDATE(),
	@VIRTUAL_MEETING_IND,
	@PROPOSED_1_DT,
	@PROPOSED_2_DT,
	@PROPOSED_3_DT,
	@CANCEL_AFTER_DT,
	@MEETING_ROOM_REF_ID
	)

SELECT @error = @@ERROR,
	@ReturnValue = SCOPE_IDENTITY()

IF @error != 0
	RAISERROR (
			'Error adding PlanReviewSchedule record.',
			18,
			1
			)

RETURN
