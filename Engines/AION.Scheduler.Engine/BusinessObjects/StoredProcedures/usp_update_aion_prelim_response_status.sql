/***********************************************************************************************************************    
* Object:       usp_update_aion_prelim_response_status   
* Description:  Updates proposed date in prelim meeting appt    
* Parameters:    @PRELIMINARY_MEETING_APPT_ID int  
*     @APPT_RESPONSE_STATUS int  
* Returns:      Number of rows affected.    
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.    
* Version:      1.0    
* Created by:   AION_user    
* Created:      07/11/2020    
************************************************************************************************************************    
* Change History: Date, Name, Description    
*  07/11/2020    AION.PRELIMINARY_MEETING_APPOINTMENT      
*  04/22/2021    Get the correct DB statuses for updating the appointment and project  
*  01/06/2022 jcl update project status if accepted/scheduled to Scheduled
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_update_aion_prelim_response_status] @PRELIMINARY_MEETING_APPT_ID INT,
	@APPT_RESPONSE_STATUS INT,
	@ReturnValue INT OUTPUT
AS
DECLARE @error INT;
DECLARE @status INT;
DECLARE @rejectEnum INT;
DECLARE @apptRespStatus INT;
DECLARE @rejectRespStatus INT;
DECLARE @cancelStatus INT;
DECLARE @notScheduledStatus INT;
DECLARE @acceptEnum INT;
DECLARE @scheduledStatus INT;
DECLARE @scheduledEnum INT;

SELECT @scheduledEnum = [ENUM_MAPPING_VAL_NBR]
FROM AION.APPOINTMENT_RESPONSE_STATUS_REF
WHERE APPT_RESPONSE_STATUS_DESC = 'Scheduled';

SELECT @acceptEnum = [ENUM_MAPPING_VAL_NBR]
FROM AION.APPOINTMENT_RESPONSE_STATUS_REF
WHERE APPT_RESPONSE_STATUS_DESC = 'Accept';

SELECT @rejectEnum = [ENUM_MAPPING_VAL_NBR]
FROM AION.APPOINTMENT_RESPONSE_STATUS_REF
WHERE APPT_RESPONSE_STATUS_DESC = 'Reject';

SELECT @apptRespStatus = [APPT_RESPONSE_STATUS_REF_ID]
FROM AION.APPOINTMENT_RESPONSE_STATUS_REF
WHERE [ENUM_MAPPING_VAL_NBR] = @APPT_RESPONSE_STATUS;

SELECT @rejectRespStatus = [APPT_RESPONSE_STATUS_REF_ID]
FROM AION.APPOINTMENT_RESPONSE_STATUS_REF
WHERE APPT_RESPONSE_STATUS_DESC = 'Reject';

SELECT @cancelStatus = [APPT_CANCELLATION_REF_ID]
FROM AION.APPOINTMENT_CANCELLATION_REF
WHERE CANCELLATION_DESC = 'Reject';

SELECT @notScheduledStatus = PROJECT_STATUS_REF_ID
FROM AION.PROJECT_STATUS_REF
WHERE PROJECT_STATUS_REF_DESC = 'Not Scheduled';

SELECT @scheduledStatus = PROJECT_STATUS_REF_ID
FROM AION.PROJECT_STATUS_REF
WHERE PROJECT_STATUS_REF_DESC = 'Scheduled';

IF (@APPT_RESPONSE_STATUS = @rejectEnum)
BEGIN
	UPDATE [AION].[PRELIMINARY_MEETING_APPOINTMENT]
	SET APPT_RESPONSE_STATUS_REF_ID = @rejectRespStatus,
		APPT_CANCELLATION_REF_ID = @cancelStatus
	WHERE PRELIMINARY_MEETING_APPT_ID = @PRELIMINARY_MEETING_APPT_ID;

	UPDATE [AION].[PROJECT]
	SET PROJECT_STATUS_REF_ID = @notScheduledStatus
	WHERE PROJECT_ID = (
			SELECT PROJECT_ID
			FROM AION.PRELIMINARY_MEETING_APPOINTMENT
			WHERE PRELIMINARY_MEETING_APPT_ID = @PRELIMINARY_MEETING_APPT_ID
			);
END
ELSE
BEGIN
	UPDATE [AION].[PRELIMINARY_MEETING_APPOINTMENT]
	SET APPT_RESPONSE_STATUS_REF_ID = @apptRespStatus,
	APPT_CANCELLATION_REF_ID = null
	WHERE PRELIMINARY_MEETING_APPT_ID = @PRELIMINARY_MEETING_APPT_ID;

	IF (
			@APPT_RESPONSE_STATUS = @acceptEnum
			OR @APPT_RESPONSE_STATUS = @scheduledEnum
			)
	BEGIN
		UPDATE [AION].[PROJECT]
		SET PROJECT_STATUS_REF_ID = @scheduledStatus
		WHERE PROJECT_ID = (
				SELECT PROJECT_ID
				FROM AION.PRELIMINARY_MEETING_APPOINTMENT
				WHERE PRELIMINARY_MEETING_APPT_ID = @PRELIMINARY_MEETING_APPT_ID
				);
	END
END

SELECT @error = @@ERROR,
	@ReturnValue = @@ROWCOUNT

IF @error != 0
	RAISERROR (
			'Error deleting Notes record.',
			18,
			1
			)

RETURN
