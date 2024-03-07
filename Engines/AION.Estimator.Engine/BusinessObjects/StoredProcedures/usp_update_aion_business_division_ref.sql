
/***********************************************************************************************************************
* Object:       usp_update_aion_business_division_ref
* Description:  Updates BusinessDivisionRef record using supplied parameters.
* Parameters:   
*               @BUSINESS_DIVISION_REF_ID                                    int
*               @BUSINESS_DIVISION_NM                                        varchar(50)
*               @BUSINESS_DIVISION_DESC                                      varchar(100)
*               @ENUM_MAPPING_VAL_NBR                                        int
*               @UPDATED_DTTM                                                datetime
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      3/21/2022
************************************************************************************************************************
* Change History: Date, Name, Description
* 3/21/2022    AION_user     Auto-generated
* 
***********************************************************************************************************************/

ALTER PROCEDURE AION.[usp_update_aion_business_division_ref]

    @BUSINESS_DIVISION_REF_ID                                    int
  , @BUSINESS_DIVISION_NM                                        varchar(50)
  , @BUSINESS_DIVISION_DESC                                      varchar(100)
  , @ENUM_MAPPING_VAL_NBR                                        int
  , @UPDATED_DTTM                                                datetime
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE AION.BUSINESS_DIVISION_REF
       SET
            BUSINESS_DIVISION_NM                                         = @BUSINESS_DIVISION_NM
          , BUSINESS_DIVISION_DESC                                       = @BUSINESS_DIVISION_DESC
          , ENUM_MAPPING_VAL_NBR                                         = @ENUM_MAPPING_VAL_NBR
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()

       WHERE
          BUSINESS_DIVISION_REF_ID                                       = @BUSINESS_DIVISION_REF_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating BusinessDivisionRef record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO