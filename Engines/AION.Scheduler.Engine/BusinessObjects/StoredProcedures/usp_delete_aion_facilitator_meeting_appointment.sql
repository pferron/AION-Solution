
/***********************************************************************************************************************
* Object:       usp_delete_aion_facilitator_meeting_appointment
* Description:  Deletes FacilitatorMeetingAppointment record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      10/7/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/7/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_delete_aion_facilitator_meeting_appointment]

    @identity                                                    int

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM FACILITATOR_MEETING_APPOINTMENT
       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          FACILITATOR_MEETING_APPT_IDENTIFIER = @identity
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting FacilitatorMeetingAppointment record.', 18,1)

RETURN

GO