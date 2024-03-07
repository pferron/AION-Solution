/***********************************************************************************************************************
* Object:       usp_delete_aion_facilitator_meeting_appt_detail_byFMAId
* Description:  Deletes FacilitatorMeetingApptDetail record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      4/21/2022
************************************************************************************************************************
* Change History: Date, Name, Description
* 4/21/2022    AION_user     Auto-generated
* 
***********************************************************************************************************************/
CREATE PROCEDURE [usp_delete_aion_facilitator_meeting_appt_detail_byFMAId] @FACILITATOR_MEETING_APPT_IDENTIFIER INT
	,@ReturnValue INT OUTPUT
AS
BEGIN
	DECLARE @error INT

	DELETE
	FROM FACILITATOR_MEETING_APPOINTMENT_DETAIL
	WHERE FACILITATOR_MEETING_APPT_IDENTIFIER = @FACILITATOR_MEETING_APPT_IDENTIFIER

	SELECT @error = @@ERROR
		,@ReturnValue = @@ROWCOUNT

	IF @error != 0
		RAISERROR (
				'Error deleting FacilitatorMeetingApptDetail record.'
				,18
				,1
				)

	RETURN
END
