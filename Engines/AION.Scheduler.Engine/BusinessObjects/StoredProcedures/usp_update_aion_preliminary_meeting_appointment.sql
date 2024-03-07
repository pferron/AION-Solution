/***********************************************************************************************************************  
* Object:       usp_update_aion_preliminary_meeting_appointment  
* Description:  Updates PreliminaryMeetingAppointment record using supplied parameters.  
* Parameters:     
*               @PRELIMINARY_MEETING_APPT_ID                                 int  
*               @FROM_DT                                                     datetime  
*               @TO_DT                                                       datetime  
*               @MEETING_ROOM_REF_ID                                         int  
*               @APPT_RESPONSE_STATUS_REF_ID                                 int  
*               @UPDATED_DTTM                                                datetime  
*               @PROJECT_ID                                                  int  
*               @APPENDIX_AGENDA_DUE_DT                                      datetime  
*               @WKR_ID_TXT                                                  varchar(100)  
*				@RESCHEDULE_IND													bit
*  
* Returns:      Number of rows affected.  
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.  
* Version:      1.0  
* Created by:   AION_user  
* Created:      6/24/2020  
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 6/24/2020    AION_user     Auto-generated  
*  01/06/2022    jlindsay    add reschedule   
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_update_aion_preliminary_meeting_appointment] @PRELIMINARY_MEETING_APPT_ID INT,
	@FROM_DT DATETIME,
	@TO_DT DATETIME,
	@MEETING_ROOM_REF_ID INT,
	@APPT_RESPONSE_STATUS_REF_ID INT,
	@APPT_CANCELLATION_REF_ID INT = NULL,
	@UPDATED_DTTM DATETIME,
	@PROJECT_ID INT,
	@APPENDIX_AGENDA_DUE_DT DATETIME,
	@WKR_ID_TXT VARCHAR(100),
	@CANCEL_AFTER_DT DATETIME = NULL,
	@RESCHEDULE_IND BIT = 0,
	@ReturnValue INT OUTPUT
AS
DECLARE @error INT

UPDATE PRELIMINARY_MEETING_APPOINTMENT
SET FROM_DT = @FROM_DT,
	TO_DT = @TO_DT,
	MEETING_ROOM_REF_ID = @MEETING_ROOM_REF_ID,
	APPT_RESPONSE_STATUS_REF_ID = @APPT_RESPONSE_STATUS_REF_ID,
	APPT_CANCELLATION_REF_ID = isnull(@APPT_CANCELLATION_REF_ID, APPT_CANCELLATION_REF_ID),
	WKR_ID_UPDATED_TXT = @WKR_ID_TXT,
	UPDATED_DTTM = GETDATE(),
	PROJECT_ID = @PROJECT_ID,
	APPENDIX_AGENDA_DUE_DT = @APPENDIX_AGENDA_DUE_DT,
	CANCEL_AFTER_DT = isnull(@CANCEL_AFTER_DT, CANCEL_AFTER_DT),
	RESCHEDULE_IND = @RESCHEDULE_IND
WHERE PRELIMINARY_MEETING_APPT_ID = @PRELIMINARY_MEETING_APPT_ID
	AND ISNULL(CONVERT(VARCHAR(19), UPDATED_DTTM, 120), '') = ISNULL(CONVERT(VARCHAR(19), @UPDATED_DTTM, 120), '')

SELECT @error = @@ERROR,
	@ReturnValue = @@ROWCOUNT

IF @error != 0
	RAISERROR (
			'Error updating PreliminaryMeetingAppointment record.',
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
