
/***********************************************************************************************************************
* Object:       usp_delete_aion_npa_type_ref
* Description:  Deletes NpaTypeRef record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      3/19/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 3/19/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [AION].[usp_delete_aion_npa_type_ref]

    @identity                                                    int

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM NON_PROJECT_APPOINTMENT_TYPE_REF
       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          NON_PROJECT_APPT_TYP_REF_ID = @identity
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting NpaTypeRef record.', 18,1)

RETURN

GO