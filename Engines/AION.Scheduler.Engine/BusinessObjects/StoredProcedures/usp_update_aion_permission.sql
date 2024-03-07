
/***********************************************************************************************************************
* Object:       usp_update_aion_permission
* Description:  Updates Permission record using supplied parameters.
* Parameters:   
*               @PERMISSION_REF_ID                                           int
*               @PERMISSION_NM                                               varchar(50)
*               @MODULE_REF_ID                                               int
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

CREATE PROCEDURE [usp_update_aion_permission]

    @PERMISSION_REF_ID                                           int
  , @PERMISSION_NM                                               varchar(50)
  , @MODULE_REF_ID                                               int
  , @UPDATED_DTTM                                                datetime
  , @ENUM_MAPPING_VAL_NBR                                        int
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE PERMISSION_REF
       SET
            PERMISSION_NM                                                = @PERMISSION_NM
          , MODULE_REF_ID                                                = @MODULE_REF_ID
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()
          , ENUM_MAPPING_VAL_NBR                                         = @ENUM_MAPPING_VAL_NBR

       WHERE
          PERMISSION_REF_ID                                              = @PERMISSION_REF_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating Permission record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO