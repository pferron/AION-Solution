
/***********************************************************************************************************************
* Object:       usp_update_aion_auto_estimation_ref
* Description:  Updates AutoEstimationRef record using supplied parameters.
* Parameters:   
*               @ACTIVE_IND                                                  bit
*               @ACTIVE_DT                                                   datetime
*               @UPDATED_DTTM                                                datetime
*               @AUTO_ESTIMATION_REF_ID                                      int
*               @MONTH_NBR                                                   int
*               @WEIGHT_SQFT_NBR                                             decimal
*               @WEIGHT_COC_NBR                                              decimal
*               @WEIGHT_SHEETS_NBR                                           decimal
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      2/13/2023
************************************************************************************************************************
* Change History: Date, Name, Description
* 2/13/2023    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_update_aion_auto_estimation_ref]

    @ACTIVE_IND                                                  bit
  , @ACTIVE_DT                                                   datetime
  , @UPDATED_DTTM                                                datetime
  , @AUTO_ESTIMATION_REF_ID                                      int
  , @MONTH_NBR                                                   int
  , @WEIGHT_SQFT_NBR                                             decimal
  , @WEIGHT_COC_NBR                                              decimal
  , @WEIGHT_SHEETS_NBR                                           decimal
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE AUTO_ESTIMATION_REF
       SET
            ACTIVE_IND                                                   = @ACTIVE_IND
          , ACTIVE_DT                                                    = @ACTIVE_DT
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()
          , MONTH_NBR                                                    = @MONTH_NBR
          , WEIGHT_SQFT_NBR                                              = @WEIGHT_SQFT_NBR
          , WEIGHT_COC_NBR                                               = @WEIGHT_COC_NBR
          , WEIGHT_SHEETS_NBR                                            = @WEIGHT_SHEETS_NBR

       WHERE
          AUTO_ESTIMATION_REF_ID                                         = @AUTO_ESTIMATION_REF_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating AutoEstimationRef record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO