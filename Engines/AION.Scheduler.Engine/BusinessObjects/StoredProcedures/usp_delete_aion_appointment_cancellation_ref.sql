
/***********************************************************************************************************************
* Object:       usp_delete_aion_appointment_cancellation_ref
* Description:  Deletes AppointmentCancellationRef record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      4/5/2021
************************************************************************************************************************
* Change History: Date, Name, Description
* 4/5/2021    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_delete_aion_appointment_cancellation_ref]

    @identity                                                    int

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM APPOINTMENT_CANCELLATION_REF
       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          APPT_CANCELLATION_REF_ID = @identity
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting AppointmentCancellationRef record.', 18,1)

RETURN

GO