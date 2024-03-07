
/***********************************************************************************************************************
* Object:       usp_update_aion_appoinment_reccurance_ref
* Description:  Updates AppoinmentReccuranceRef record using supplied parameters.
* Parameters:   
*               @APPT_RECURRENCE_REF_ID                                      int
*               @RECURRENCE_WEEK_DESC                                        varchar(30)
*               @RECURRENCE_DAY_DESC                                         varchar(30)
*               @ENUM_MAPPING_VAL_NBR                                        int
*               @UPDATED_DTTM                                                datetime
*               @ACTIVE_IND                                                  bit
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      3/19/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 3/19/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [AION].[usp_update_aion_appoinment_reccurance_ref]

    @APPT_RECURRENCE_REF_ID                                      int
  , @RECURRENCE_WEEK_DESC                                        varchar(30)
  , @RECURRENCE_DAY_DESC                                         varchar(30)
  , @ENUM_MAPPING_VAL_NBR                                        int
  , @UPDATED_DTTM                                                datetime
  , @ACTIVE_IND                                                  bit
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE APPOINTMENT_RECURRENCE_REF
       SET
            RECURRENCE_WEEK_DESC                                         = @RECURRENCE_WEEK_DESC
          , RECURRENCE_DAY_DESC                                          = @RECURRENCE_DAY_DESC
          , ENUM_MAPPING_VAL_NBR                                         = @ENUM_MAPPING_VAL_NBR
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()
          , ACTIVE_IND                                                   = @ACTIVE_IND

       WHERE
          APPT_RECURRENCE_REF_ID                                         = @APPT_RECURRENCE_REF_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating AppoinmentReccuranceRef record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO