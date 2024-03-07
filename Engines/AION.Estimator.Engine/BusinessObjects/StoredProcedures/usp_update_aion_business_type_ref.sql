
/***********************************************************************************************************************
* Object:       usp_update_aion_business_type_ref
* Description:  Updates BusinessTypeRef record using supplied parameters.
* Parameters:   
*               @BUSINESS_TYP_REF_ID                                         int
*               @BUSINESS_REF_TYP_SHORT_DESC                                 varchar(30)
*               @BUSINESS_REF_DISPLAY_NM                                     varchar(100)
*               @EXTERNAL_SYSTEM_REF_ID                                      int
*               @SRC_SYSTEM_VAL_TXT                                          varchar(255)
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

CREATE PROCEDURE [usp_update_aion_business_type_ref]

    @BUSINESS_TYP_REF_ID                                         int
  , @BUSINESS_REF_TYP_SHORT_DESC                                 varchar(30)
  , @BUSINESS_REF_DISPLAY_NM                                     varchar(100)
  , @EXTERNAL_SYSTEM_REF_ID                                      int
  , @SRC_SYSTEM_VAL_TXT                                          varchar(255)
  , @UPDATED_DTTM                                                datetime
  , @ENUM_MAPPING_VAL_NBR                                        int
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE BUSINESS_TYPE_REF
       SET
            BUSINESS_REF_TYP_SHORT_DESC                                  = @BUSINESS_REF_TYP_SHORT_DESC
          , BUSINESS_REF_DISPLAY_NM                                      = @BUSINESS_REF_DISPLAY_NM
          , EXTERNAL_SYSTEM_REF_ID                                       = @EXTERNAL_SYSTEM_REF_ID
          , SRC_SYSTEM_VAL_TXT                                           = @SRC_SYSTEM_VAL_TXT
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()
          , ENUM_MAPPING_VAL_NBR                                         = @ENUM_MAPPING_VAL_NBR

       WHERE
          BUSINESS_TYP_REF_ID                                            = @BUSINESS_TYP_REF_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating BusinessTypeRef record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO