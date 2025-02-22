/****** Object:  StoredProcedure [AION].[usp_insert_aion_reserve_express_reservation]    Script Date: 8/17/2020 10:56:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [AION].[usp_insert_aion_reserve_express_reservation]
    @RESERVE_EXPRESS_DT datetime
  , @START_TM  time
  , @END_TM time
  , @BUSINESS_REF_ID  int
  , @MEETING_ROOM_ID int
  , @PLAN_REVIEWER_ID   int
  , @WKR_ID_TXT varchar(100)
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
          , GETDATE()
          )
     SELECT @error = @@ERROR

     IF @error != 0
          RAISERROR('Error adding ConfigureReserveExpress record.', 18,1)

RETURN
