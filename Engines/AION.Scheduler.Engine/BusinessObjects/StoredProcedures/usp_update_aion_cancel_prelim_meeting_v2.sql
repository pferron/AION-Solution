﻿/***********************************************************************************************************************    
* Object:      [usp_update_aion_cancel_prelim_meeting]  
*         
* Returns:      Number of rows affected.    
* Comments:         
* Version:      1.0    
* Created by:   AION_user    
* Created:      07/11/2020    
************************************************************************************************************************    
* Change History: Date, Name, Description    
*  07/11/2020    AION.PRELIMINARY_MEETING_APPOINTMENT    
* 09/17/2021 jlindsay format, add update date, update project to not scheduled
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_update_aion_cancel_prelim_meeting_v2]
AS
BEGIN
	SET NOCOUNT ON;

	--'Tentatively Scheduled'    
	DECLARE @TENTATIVE_APPT_RESPONSE_STATUS_REF_ID INT = 0;

	SELECT @TENTATIVE_APPT_RESPONSE_STATUS_REF_ID = APPT_RESPONSE_STATUS_REF_ID
	FROM AION.APPOINTMENT_RESPONSE_STATUS_REF
	WHERE ENUM_MAPPING_VAL_NBR = 9;

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

	-- Project Not Scheduled status
	DECLARE @PROJECT_STATUS_REF_NOT_SCHEDULED INT = 0;

	SELECT @PROJECT_STATUS_REF_NOT_SCHEDULED = PROJECT_STATUS_REF_ID
	FROM AION.PROJECT_STATUS_REF
	WHERE ENUM_MAPPING_VAL_NBR = 6;

	DECLARE @Appts TABLE (
		PRELIMINARY_MEETING_APPT_ID INT,
		PROJECT_ID INT
		);

	INSERT INTO @Appts
	SELECT PRELIMINARY_MEETING_APPT_ID,
		PROJECT_ID
	FROM AION.PRELIMINARY_MEETING_APPOINTMENT PMA
	WHERE APPT_RESPONSE_STATUS_REF_ID IN (
			SELECT APPT_RESPONSE_STATUS_REF_ID
			FROM AION.APPOINTMENT_RESPONSE_STATUS_REF
			WHERE APPT_RESPONSE_STATUS_REF_ID = @TENTATIVE_APPT_RESPONSE_STATUS_REF_ID
			)
		AND DATEADD(HOUR, - 5, GETDATE()) >= PMA.CANCEL_AFTER_DT;-- adjust to EST  

	UPDATE AION.PRELIMINARY_MEETING_APPOINTMENT
	SET APPT_RESPONSE_STATUS_REF_ID = @CANCELLED_APPT_RESPONSE_STATUS_REF_ID,
		APPT_CANCELLATION_REF_ID = @CANCELLED_APPT_REASON_REF_ID,
		UPDATED_DTTM = GETDATE(),
		WKR_ID_UPDATED_TXT = '1' --system user
	WHERE PRELIMINARY_MEETING_APPT_ID IN (
			SELECT PRELIMINARY_MEETING_APPT_ID
			FROM @Appts
			);

	UPDATE AION.PROJECT
	SET PROJECT_STATUS_REF_ID = @PROJECT_STATUS_REF_NOT_SCHEDULED,
		UPDATED_DTTM = GETDATE(),
		WKR_ID_UPDATED_TXT = '1' --system user
	WHERE PROJECT_ID IN (
			SELECT PROJECT_ID
			FROM @Appts
			);

	SELECT PRELIMINARY_MEETING_APPT_ID
	FROM @Appts;

	RETURN
END
