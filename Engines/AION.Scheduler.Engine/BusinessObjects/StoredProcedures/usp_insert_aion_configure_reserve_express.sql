
/***********************************************************************************************************************
* Object:	usp_insert_aion_configure_reserve_express
* Description:	Inserts ConfigureReserveExpress record.
* Parameters:
*		@RESERVE_EXPRESS_DAY_DESC                                    varchar(100)
*		@START_DTTM                                                  datetime
*		@END_DTTM                                                    datetime
*		@ACTIVE_IND                                                  bit
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      7/29/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 7/29/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_insert_aion_configure_reserve_express]
    @RESERVE_EXPRESS_DAY_DESC                                    varchar(100)
  , @START_DTTM                                                  datetime
  , @END_DTTM                                                    datetime
  , @ACTIVE_IND                                                  bit
  , @WKR_ID_TXT                                                  varchar(100)
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO CONFIGURE_RESERVE_EXPRESS
          (
            RESERVE_EXPRESS_DAY_DESC
          , START_DTTM
          , END_DTTM
          , ACTIVE_IND
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          )
     VALUES
          (
            @RESERVE_EXPRESS_DAY_DESC
          , @START_DTTM
          , @END_DTTM
          , @ACTIVE_IND
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding ConfigureReserveExpress record.', 18,1)

RETURN
GO