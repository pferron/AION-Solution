
/***********************************************************************************************************************
* Object:       usp_update_aion_appointment_cancellation_ref
* Description:  Updates AppointmentCancellationRef record using supplied parameters.
* Parameters:   
*               @APPT_CANCELLATION_REF_ID                                    int
*               @CANCELLATION_DESC                                           varchar(30)
*               @ENUM_MAPPING_VAL_NBR                                        int
*               @ACTIVE_IND                                                  bit
*               @UPDATED_DTTM                                                datetime
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      4/5/2021
************************************************************************************************************************
* Change History: Date, Name, Description
* 4/5/2021    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_update_aion_appointment_cancellation_ref]

    @APPT_CANCELLATION_REF_ID                                    int
  , @CANCELLATION_DESC                                           varchar(30)
  , @ENUM_MAPPING_VAL_NBR                                        int
  , @ACTIVE_IND                                                  bit
  , @UPDATED_DTTM                                                datetime
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE APPOINTMENT_CANCELLATION_REF
       SET
            CANCELLATION_DESC                                            = @CANCELLATION_DESC
          , ENUM_MAPPING_VAL_NBR                                         = @ENUM_MAPPING_VAL_NBR
          , ACTIVE_IND                                                   = @ACTIVE_IND
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()

       WHERE
          APPT_CANCELLATION_REF_ID                                       = @APPT_CANCELLATION_REF_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating AppointmentCancellationRef record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO