
/***********************************************************************************************************************
* Object:	usp_insert_aion_appoinment_reccurance_ref
* Description:	Inserts AppoinmentReccuranceRef record.
* Parameters:
*		@RECURRENCE_WEEK_DESC                                        varchar(30)
*		@RECURRENCE_DAY_DESC                                         varchar(30)
*		@ENUM_MAPPING_VAL_NBR                                        int
*		@ACTIVE_IND                                                  bit
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      3/19/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 3/19/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [AION].[usp_insert_aion_appoinment_reccurance_ref]
    @RECURRENCE_WEEK_DESC                                        varchar(30)
  , @RECURRENCE_DAY_DESC                                         varchar(30)
  , @ENUM_MAPPING_VAL_NBR                                        int
  , @ACTIVE_IND                                                  bit
  , @WKR_ID_TXT                                                  varchar(100)
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO APPOINTMENT_RECURRENCE_REF
          (
            RECURRENCE_WEEK_DESC
          , RECURRENCE_DAY_DESC
          , ENUM_MAPPING_VAL_NBR
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , ACTIVE_IND
          )
     VALUES
          (
            @RECURRENCE_WEEK_DESC
          , @RECURRENCE_DAY_DESC
          , @ENUM_MAPPING_VAL_NBR
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          , @ACTIVE_IND
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding AppoinmentReccuranceRef record.', 18,1)

RETURN
GO