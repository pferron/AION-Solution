
/***********************************************************************************************************************
* Object:       usp_delete_aion_preliminary_meeting_appointment
* Description:  Deletes PreliminaryMeetingAppointment record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      6/24/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 6/24/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_delete_aion_preliminary_meeting_appointment]

    @identity                                                    int

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM PRELIMINARY_MEETING_APPOINTMENT
       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          PRELIMINARY_MEETING_APPT_ID = @identity
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting PreliminaryMeetingAppointment record.', 18,1)

RETURN

GO