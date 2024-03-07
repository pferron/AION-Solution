
/***********************************************************************************************************************
* Object:       usp_update_aion_configure_reserve_express
* Description:  Updates ConfigureReserveExpress record using supplied parameters.
* Parameters:   
*               @CONFIGURE_RESERVE_EXPRESS_ID                                int
*               @RESERVE_EXPRESS_DAY_DESC                                    varchar(100)
*               @START_DTTM                                                  datetime
*               @END_DTTM                                                    datetime
*               @ACTIVE_IND                                                  bit
*               @UPDATED_DTTM                                                datetime
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      7/29/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 7/29/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_update_aion_configure_reserve_express]

    @CONFIGURE_RESERVE_EXPRESS_ID                                int
  , @RESERVE_EXPRESS_DAY_DESC                                    varchar(100)
  , @START_DTTM                                                  datetime
  , @END_DTTM                                                    datetime
  , @ACTIVE_IND                                                  bit
  , @UPDATED_DTTM                                                datetime
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE CONFIGURE_RESERVE_EXPRESS
       SET
            RESERVE_EXPRESS_DAY_DESC                                     = @RESERVE_EXPRESS_DAY_DESC
          , START_DTTM                                                   = @START_DTTM
          , END_DTTM                                                     = @END_DTTM
          , ACTIVE_IND                                                   = @ACTIVE_IND
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()

       WHERE
          CONFIGURE_RESERVE_EXPRESS_ID                                   = @CONFIGURE_RESERVE_EXPRESS_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating ConfigureReserveExpress record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO