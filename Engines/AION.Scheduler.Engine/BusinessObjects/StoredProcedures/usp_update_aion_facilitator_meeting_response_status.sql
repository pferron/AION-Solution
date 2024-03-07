/***********************************************************************************************************************
* Object:       usp_update_aion_facilitator_meeting_response_status
* Description:  Updates the appointment response status for a facilitator meeting appointment
* Parameters:   
*               @FACILITATOR_MEETING_APPT_IDENTIFIER int
*               @APPT_RESPONSE_STATUS int
*
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      04/10/2021
************************************************************************************************************************
* Change History: Date, Name, Description
* 04/10/2021    jallen  Create
* 04/22/2021   Get the correct DB statuses for updating the appointment  
***********************************************************************************************************************/

CREATE PROCEDURE [AION].[usp_update_aion_facilitator_meeting_response_status]

 @FACILITATOR_MEETING_APPT_IDENTIFIER  int
,@APPT_RESPONSE_STATUS int

 , @ReturnValue                                                 int OUTPUT
AS

     DECLARE @error   int
	 DECLARE @status int
     DECLARE @rejectEnum int
     DECLARE @apptRespStatus int
     DECLARE @rejectRespStatus int
     DECLARE @cancelStatus int

     SELECT @rejectEnum = [ENUM_MAPPING_VAL_NBR] from AION.APPOINTMENT_RESPONSE_STATUS_REF
     WHERE APPT_RESPONSE_STATUS_DESC = 'Reject'

     SELECT @apptRespStatus = [APPT_RESPONSE_STATUS_REF_ID] from AION.APPOINTMENT_RESPONSE_STATUS_REF
	 WHERE [ENUM_MAPPING_VAL_NBR]=@APPT_RESPONSE_STATUS

     SELECT @rejectRespStatus = [APPT_RESPONSE_STATUS_REF_ID] from AION.APPOINTMENT_RESPONSE_STATUS_REF
	 WHERE APPT_RESPONSE_STATUS_DESC = 'Reject'

     SELECT @cancelStatus = [APPT_CANCELLATION_REF_ID] from AION.APPOINTMENT_CANCELLATION_REF
	 WHERE CANCELLATION_DESC = 'Reject'
	
	 If(@APPT_RESPONSE_STATUS = @rejectEnum)
	 BEGIN
       UPDATE [AION].[FACILITATOR_MEETING_APPOINTMENT]
       SET APPT_RESPONSE_STATUS_REF_ID = @rejectRespStatus,
       APPT_CANCELLATION_REF_ID = @cancelStatus
       WHERE FACILITATOR_MEETING_APPT_IDENTIFIER = @FACILITATOR_MEETING_APPT_IDENTIFIER       
 	 END
	 ELSE
	 BEGIN

	   UPDATE [AION].[FACILITATOR_MEETING_APPOINTMENT]
       SET APPT_RESPONSE_STATUS_REF_ID = @apptRespStatus           
       WHERE FACILITATOR_MEETING_APPT_IDENTIFIER = @FACILITATOR_MEETING_APPT_IDENTIFIER     
	END
	
     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting Notes record.', 18,1)
RETURN
