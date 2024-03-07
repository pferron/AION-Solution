
/***********************************************************************************************************************
* Object:       usp_update_aion_external_system_ref
* Description:  Updates ExternalSystemRef record using supplied parameters.
* Parameters:   
*               @EXTERNAL_SYSTEM_REF_ID                                      int
*               @EXTERNAL_SYSTEM_NM                                          varchar(100)
*               @EXTERNAL_SYSTEM_DESC                                        varchar(100)
*               @ADDL_INFORMATION_TXT                                        varchar(255)
*               @UPDATED_DTTM                                                datetime
*               @ENUM_MAPPING_VAL_NBR                                        int
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      10/10/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/10/2019    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_update_aion_external_system_ref]

    @EXTERNAL_SYSTEM_REF_ID                                      int
  , @EXTERNAL_SYSTEM_NM                                          varchar(100)
  , @EXTERNAL_SYSTEM_DESC                                        varchar(100)
  , @ADDL_INFORMATION_TXT                                        varchar(255)
  , @UPDATED_DTTM                                                datetime
  , @ENUM_MAPPING_VAL_NBR                                        int
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE EXTERNAL_SYSTEM_REF
       SET
            EXTERNAL_SYSTEM_NM                                           = @EXTERNAL_SYSTEM_NM
          , EXTERNAL_SYSTEM_DESC                                         = @EXTERNAL_SYSTEM_DESC
          , ADDL_INFORMATION_TXT                                         = @ADDL_INFORMATION_TXT
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()
          , ENUM_MAPPING_VAL_NBR                                         = @ENUM_MAPPING_VAL_NBR

       WHERE
          EXTERNAL_SYSTEM_REF_ID                                         = @EXTERNAL_SYSTEM_REF_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating ExternalSystemRef record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO