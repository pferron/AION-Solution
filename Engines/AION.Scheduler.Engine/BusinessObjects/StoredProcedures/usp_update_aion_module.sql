
/***********************************************************************************************************************
* Object:       usp_update_aion_module
* Description:  Updates Module record using supplied parameters.
* Parameters:   
*               @MODULE_REF_ID                                               int
*               @MODULE_NM                                                   varchar(50)
*               @UPDATED_DTTM                                                datetime
*               @ENUM_MAPPING_VAL_NBR                                        int
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      5/11/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 5/11/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_update_aion_module]

    @MODULE_REF_ID                                               int
  , @MODULE_NM                                                   varchar(50)
  , @UPDATED_DTTM                                                datetime
  , @ENUM_MAPPING_VAL_NBR                                        int
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE MODULE_REF
       SET
            MODULE_NM                                                    = @MODULE_NM
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()
          , ENUM_MAPPING_VAL_NBR                                         = @ENUM_MAPPING_VAL_NBR

       WHERE
          MODULE_REF_ID                                                  = @MODULE_REF_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating Module record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO