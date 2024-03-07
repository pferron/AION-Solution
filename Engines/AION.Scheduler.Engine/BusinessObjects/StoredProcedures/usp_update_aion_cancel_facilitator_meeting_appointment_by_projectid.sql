
/***********************************************************************************************************************        
* Object:      [usp_update_aion_cancel_facilitator_meeting_appointment_by_projectid]      
*             
* Returns:      Number of rows affected.        
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.        
* Version:      1.0        
* Created by:   AION_user        
* Created:      01/12/2021        
************************************************************************************************************************        
* Change History: Date, Name, Description        
*  01/12/2021    aburnam      
*  05/19/2021    jallen     Updated to include setting cancellation reason equal to Accela
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_update_aion_cancel_facilitator_meeting_appointment_by_projectid] 
@projectID INT,
@ReturnValue INT OUTPUT
AS
    BEGIN  
        --'Cancelled'  
        DECLARE @CANCELLED_APPT_RESPONSE_STATUS_REF_ID INT= 0;
        SELECT @CANCELLED_APPT_RESPONSE_STATUS_REF_ID = APPT_RESPONSE_STATUS_REF_ID
        FROM AION.APPOINTMENT_RESPONSE_STATUS_REF
        WHERE ENUM_MAPPING_VAL_NBR = 7;   

		-- 'Cancellation Reason' equal to 'Accela'
		DECLARE @CANCELLED_APPT_CANCELLATION_REF_ID INT= 0;
        SELECT @CANCELLED_APPT_CANCELLATION_REF_ID = APPT_CANCELLATION_REF_ID
        FROM AION.APPOINTMENT_CANCELLATION_REF
        WHERE ENUM_MAPPING_VAL_NBR = 2;   

        --  Update the status of the express meeting appointment
        UPDATE AION.FACILITATOR_MEETING_APPOINTMENT
          SET APPT_RESPONSE_STATUS_REF_ID = @CANCELLED_APPT_RESPONSE_STATUS_REF_ID,    
		  APPT_CANCELLATION_REF_ID = @CANCELLED_APPT_CANCELLATION_REF_ID
        WHERE PROJECT_ID = @projectID
	     
        SELECT @ReturnValue = @@ROWCOUNT;
        RETURN;
    END;
