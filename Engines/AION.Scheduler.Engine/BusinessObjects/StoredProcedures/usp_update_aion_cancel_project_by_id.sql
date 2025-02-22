/****** Object:  StoredProcedure [AION].[usp_update_aion_cancel_project_by_id]    Script Date: 12/1/2020 2:20:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************        
* Object:      [usp_update_aion_cancel_project_by_id]      
*             
* Returns:      Number of rows affected.        
* Comments:     This stored proc cancels a project and all related items.        
* Version:      1.0        
* Created by:   AION_user        
* Created:      12/1/2020       
************************************************************************************************************************        
* Change History: Date, Name, Description        
*  12/1/2020    aburnam 
*  08/23/2021   jallen   Add FMAs to appointments requiring cancellation
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_update_aion_cancel_project_by_id] 

@PROJECT_ID INT,
@USER_ID INT,
@RETURN_VALUE INT OUTPUT

AS
    BEGIN  

		--'Cancelled Appointment'  
        DECLARE @CANCELLED_APPT_RESPONSE_STATUS_REF_ID INT= 0;
        SELECT @CANCELLED_APPT_RESPONSE_STATUS_REF_ID = APPT_RESPONSE_STATUS_REF_ID
        FROM AION.APPOINTMENT_RESPONSE_STATUS_REF
        WHERE ENUM_MAPPING_VAL_NBR = 7;  

		--'Cancelled Project'
		DECLARE @CANCELLED_PROJECT_RESPONSE_STATUS_REF_ID INT = 0;
		SELECT @CANCELLED_PROJECT_RESPONSE_STATUS_REF_ID = PROJECT_STATUS_REF_ID
		FROM AION.PROJECT_STATUS_REF
		WHERE ENUM_MAPPING_VAL_NBR = 25

		UPDATE [AION].[PRELIMINARY_MEETING_APPOINTMENT]
		SET APPT_RESPONSE_STATUS_REF_ID = @CANCELLED_APPT_RESPONSE_STATUS_REF_ID,
		WKR_ID_UPDATED_TXT = @USER_ID,
		UPDATED_DTTM = GETDATE()
		WHERE PROJECT_ID = @PROJECT_ID

		UPDATE [AION].[FACILITATOR_MEETING_APPOINTMENT]
		SET APPT_RESPONSE_STATUS_REF_ID = @CANCELLED_APPT_RESPONSE_STATUS_REF_ID,
		WKR_ID_UPDATED_TXT = @USER_ID,
		UPDATED_DTTM = GETDATE()
		WHERE PROJECT_ID = @PROJECT_ID

		UPDATE [AION].[PLAN_REVIEW_SCHEDULE]
		SET APPT_RESPONSE_STATUS_REF_ID = @CANCELLED_APPT_RESPONSE_STATUS_REF_ID,
		WKR_ID_UPDATED_TXT = @USER_ID,
		UPDATED_DTTM = GETDATE()
		WHERE PROJECT_ID = @PROJECT_ID

		UPDATE [AION].[PROJECT]
		SET PROJECT_STATUS_REF_ID = @CANCELLED_PROJECT_RESPONSE_STATUS_REF_ID,
		WKR_ID_UPDATED_TXT = @USER_ID,
		UPDATED_DTTM = GETDATE()
		WHERE PROJECT_ID = @PROJECT_ID AND PROJECT_STATUS_REF_ID != @CANCELLED_PROJECT_RESPONSE_STATUS_REF_ID
		
        SELECT @RETURN_VALUE = @@ROWCOUNT;
        RETURN;
    END;