
/***********************************************************************************************************************
* Object:	usp_insert_aion_appointment_cancellation_ref
* Description:	Inserts AppointmentCancellationRef record.
* Parameters:
*		@CANCELLATION_DESC                                           varchar(30)
*		@ENUM_MAPPING_VAL_NBR                                        int
*		@ACTIVE_IND                                                  bit
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      4/5/2021
************************************************************************************************************************
* Change History: Date, Name, Description
* 4/5/2021    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_insert_aion_appointment_cancellation_ref]
    @CANCELLATION_DESC                                           varchar(30)
  , @ENUM_MAPPING_VAL_NBR                                        int
  , @ACTIVE_IND                                                  bit
  , @WKR_ID_TXT                                                  varchar(100)
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO APPOINTMENT_CANCELLATION_REF
          (
            CANCELLATION_DESC
          , ENUM_MAPPING_VAL_NBR
          , ACTIVE_IND
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          )
     VALUES
          (
            @CANCELLATION_DESC
          , @ENUM_MAPPING_VAL_NBR
          , @ACTIVE_IND
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding AppointmentCancellationRef record.', 18,1)

RETURN
GO