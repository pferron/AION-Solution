/****** Object:  StoredProcedure [AION].[[usp_update_aion_cancel_meeting_saved_user_schedules]]    Script Date: 11/25/2020 2:32:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************        
* Object:      [[usp_update_aion_cancel_meeting_saved_user_schedules]]      
*             
* Returns:      Number of rows affected.        
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.        
* Version:      1.0        
* Created by:   AION_user        
* Created:      11/25/2020       
************************************************************************************************************************        
* Change History: Date, Name, Description        
*  11/25/2020    jallen      
*  03/23/2020    jallen  Make sure to join on project schedule type description for FMA. 
***********************************************************************************************************************/

CREATE PROCEDURE [AION].[usp_update_aion_cancel_meeting_saved_user_schedules] @ReturnValue INT OUTPUT
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