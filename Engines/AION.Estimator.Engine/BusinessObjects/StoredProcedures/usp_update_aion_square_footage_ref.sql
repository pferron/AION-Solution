
/***********************************************************************************************************************
* Object:       usp_update_aion_square_footage_ref
* Description:  Updates SquareFootageRef record using supplied parameters.
* Parameters:   
*               @SQUARE_FOOTAGE_REF_ID                                       int
*               @SQUARE_FOOTAGE_DESC                                         varchar(255)
*               @SQUARE_FOOTAGE_VAL_TXT                                      varchar(100)
*               @ENUM_MAPPING_VAL_NBR                                        int
*               @UPDATED_DTTM                                                datetime
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      4/30/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 4/30/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_update_aion_square_footage_ref]

    @SQUARE_FOOTAGE_REF_ID                                       int
  , @SQUARE_FOOTAGE_DESC                                         varchar(255)
  , @SQUARE_FOOTAGE_VAL_TXT                                      varchar(100)
  , @ENUM_MAPPING_VAL_NBR                                        int
  , @UPDATED_DTTM                                                datetime
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE SQUARE_FOOTAGE_REF
       SET
            SQUARE_FOOTAGE_DESC                                          = @SQUARE_FOOTAGE_DESC
          , SQUARE_FOOTAGE_VAL_TXT                                       = @SQUARE_FOOTAGE_VAL_TXT
          , ENUM_MAPPING_VAL_NBR                                         = @ENUM_MAPPING_VAL_NBR
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()

       WHERE
          SQUARE_FOOTAGE_REF_ID                                          = @SQUARE_FOOTAGE_REF_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating SquareFootageRef record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO