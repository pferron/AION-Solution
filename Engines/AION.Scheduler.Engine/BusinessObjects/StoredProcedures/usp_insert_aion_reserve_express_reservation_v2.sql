CREATE PROCEDURE [AION].[usp_insert_aion_reserve_express_reservation_v2]
    @RESERVE_EXPRESS_DT datetime
  , @START_TM  time
  , @END_TM time
  , @MEETING_ROOM_ID int
  , @WKR_ID_TXT varchar(100)
  , @APPT_RESPONSE_STATUS_REF_ID int
  , @CANCEL_AFTER_DT datetime
  , @ReturnValue  int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO RESERVE_EXPRESS_RESERVATION
          (
            RESERVE_EXPRESS_DT
          , START_TM
          , END_TM
          , MEETING_ROOM_REF_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
		  , APPT_RESPONSE_STATUS_REF_ID
		  , CANCEL_AFTER_DT
          , UPDATED_DTTM
          )
     VALUES
          (
            @RESERVE_EXPRESS_DT
          , @START_TM
          , @END_TM
		  , @MEETING_ROOM_ID
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
		  , @APPT_RESPONSE_STATUS_REF_ID
		  , @CANCEL_AFTER_DT
          , GETDATE()
          )

     SELECT @error = @@ERROR,
		@ReturnValue = @@IDENTITY

     IF @error != 0
          RAISERROR('Error adding ReserveExpressReservation record.', 18,1)

RETURN
