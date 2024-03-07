/****** Object:  StoredProcedure [AION].[usp_delete_aion_reserve_express_reservation]    Script Date: 8/24/2020 9:11:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:       usp_delete_aion_reserve_express_reservation
* Description:  Deletes Reserve Express Reservatopm record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      08/24/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/2/2019    AION_user     Auto-generated
* 
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_delete_aion_reserve_express_reservation]

    @identity                                                    int

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM RESERVE_EXPRESS_RESERVATION
       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          RESERVE_EXPRESS_RESERVATION_ID = @identity
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting Reserve Express Reservation record.', 18,1)

RETURN


