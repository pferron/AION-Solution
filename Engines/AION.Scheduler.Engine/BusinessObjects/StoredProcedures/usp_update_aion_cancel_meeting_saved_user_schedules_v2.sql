﻿ALTER PROCEDURE [AION].[usp_update_aion_cancel_meeting_saved_user_schedules_v2] 
	@ReturnValue INT OUTPUT
AS
    BEGIN  
        --'Not Scheduled'  
        DECLARE @APPT_RESPONSE_STATUS_REF_ID INT= 0;
        SELECT @APPT_RESPONSE_STATUS_REF_ID = APPT_RESPONSE_STATUS_REF_ID
        FROM AION.APPOINTMENT_RESPONSE_STATUS_REF
        WHERE ENUM_MAPPING_VAL_NBR = 4;  

        DELETE FROM USER_SCHEDULE
		WHERE PROJECT_SCHEDULE_ID IN 
		(SELECT PROJECT_SCHEDULE_ID
		FROM PROJECT_SCHEDULE PS
		INNER JOIN FACILITATOR_MEETING_APPOINTMENT FMA ON PS.APPT_ID = FMA.FACILITATOR_MEETING_APPT_IDENTIFIER
        AND PS.PROJECT_SCHEDULE_TYP_DESC = 'FMA'
		AND FMA.APPT_RESPONSE_STATUS_REF_ID = @APPT_RESPONSE_STATUS_REF_ID)
		AND GETDATE() >= DATEADD(HOUR, 2, UPDATED_DTTM); 
		
        SELECT @ReturnValue = @@ROWCOUNT;
        RETURN;
    END;