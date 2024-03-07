
/***********************************************************************************************************************
* Object:       usp_delete_aion_configure_reserve_express
* Description:  Deletes ConfigureReserveExpress record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      7/29/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 7/29/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [AION].[usp_delete_aion_configure_reserve_express]

   @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM CONFIGURE_RESERVE_EXPRESS
 
     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting ConfigureReserveExpress record.', 18,1)

RETURN

GO