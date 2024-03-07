/****** Object:  StoredProcedure [AION].[usp_update_aion_cancel_express_meeting]    Script Date: 11/1/2021 12:05:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************        
* Object:      [usp_update_aion_cancel_express_meeting]      
*             
* Returns:      Number of rows affected.        
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.        
* Version:      1.0        
* Created by:   AION_user        
* Created:      07/11/2020        
************************************************************************************************************************        
* Change History: Date, Name, Description        
*  09/08/2020    jlindsay      
*  09/28/2020  jlindsay change update to remove subquerys  
*  10/29/2020   jlindsay update from and to dt to null
*  11/01/2021   jallen update to pull from plan review tables
*  03/23/2023   jallen update to select EMA-only plan review schedules and update today's date to EST
***********************************************************************************************************************/

CREATE PROCEDURE [AION].[usp_update_aion_cancel_express_meeting] 
	
AS
    BEGIN  
        --'Tentatively Scheduled'  
        DECLARE @TENTATIVE_APPT_RESPONSE_STATUS_REF_ID INT= 0;
        SELECT @TENTATIVE_APPT_RESPONSE_STATUS_REF_ID = APPT_RESPONSE_STATUS_REF_ID
        FROM AION.APPOINTMENT_RESPONSE_STATUS_REF
        WHERE ENUM_MAPPING_VAL_NBR = 9;  
        --  
        --'Cancelled'  
        DECLARE @CANCELLED_APPT_RESPONSE_STATUS_REF_ID INT= 0;
        SELECT @CANCELLED_APPT_RESPONSE_STATUS_REF_ID = APPT_RESPONSE_STATUS_REF_ID
        FROM AION.APPOINTMENT_RESPONSE_STATUS_REF
        WHERE ENUM_MAPPING_VAL_NBR = 7;  

        --'Cancellation Reason'  
        DECLARE @CANCELLED_APPT_REASON_REF_ID INT= 0;
        SELECT @CANCELLED_APPT_REASON_REF_ID = APPT_CANCELLATION_REF_ID
        FROM AION.APPOINTMENT_CANCELLATION_REF
        WHERE ENUM_MAPPING_VAL_NBR = 1;  


    DECLARE @NowEST datetime

    SET @NowEST = CONVERT(datetime, SWITCHOFFSET(GETDATE(), DATEPART(TZOFFSET,
    GETDATE() AT TIME ZONE 'Eastern Standard Time')))

    DECLARE @Appts TABLE
    (
	    EXPRESS_MEETING_APPT_ID INT
    )

    INSERT INTO @Appts  
    SELECT PLAN_REVIEW_SCHEDULE_ID 
    FROM AION.PLAN_REVIEW_SCHEDULE  
    WHERE PROJECT_SCHEDULE_TYP_DESC = 'EMA'
    AND APPT_RESPONSE_STATUS_REF_ID IN  
    (SELECT APPT_RESPONSE_STATUS_REF_ID FROM AION.APPOINTMENT_RESPONSE_STATUS_REF WHERE APPT_RESPONSE_STATUS_REF_ID=@TENTATIVE_APPT_RESPONSE_STATUS_REF_ID)  
    AND @NowEST >= CANCEL_AFTER_DT 

    UPDATE AION.PLAN_REVIEW_SCHEDULE
	SET 
        APPT_RESPONSE_STATUS_REF_ID = @CANCELLED_APPT_RESPONSE_STATUS_REF_ID,
        APPT_CANCELLATION_REF_ID = @CANCELLED_APPT_REASON_REF_ID
	WHERE PLAN_REVIEW_SCHEDULE_ID IN
	(SELECT EXPRESS_MEETING_APPT_ID FROM @Appts)

	SELECT EXPRESS_MEETING_APPT_ID FROM @Appts
	RETURN;
END;
