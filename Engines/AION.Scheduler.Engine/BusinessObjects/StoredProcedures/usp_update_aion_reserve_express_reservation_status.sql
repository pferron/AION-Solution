/****** Object:  StoredProcedure [AION].[usp_update_aion_reserve_express_reservation]    Script Date: 8/23/2020 7:12:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [AION].[usp_update_aion_reserve_express_reservation]
 @RESERVE_EXPRESS_RESERVATION_ID int
 ,@APPT_RESPONSE_STATUS_REF_ID int
 , @ReturnValue int OUTPUT

AS

     DECLARE @error   int

       UPDATE RESERVE_EXPRESS_RESERVATION
       SET
         APPT_RESPONSE_STATUS_REF_ID =@APPT_RESPONSE_STATUS_REF_ID
       , UPDATED_DTTM                                                 = GETDATE()

       WHERE
          RESERVE_EXPRESS_RESERVATION_ID = @RESERVE_EXPRESS_RESERVATION_ID       
        

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating ReserveExpressReservation record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
