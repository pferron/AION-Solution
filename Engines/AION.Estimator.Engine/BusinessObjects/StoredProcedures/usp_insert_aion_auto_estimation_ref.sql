
/***********************************************************************************************************************
* Object:	usp_insert_aion_auto_estimation_ref
* Description:	Inserts AutoEstimationRef record.
* Parameters:
*		@ACTIVE_IND                                                  bit
*		@ACTIVE_DT                                                   datetime
*		@MONTH_NBR                                                   int
*		@WEIGHT_SQFT_NBR                                             decimal
*		@WEIGHT_COC_NBR                                              decimal
*		@WEIGHT_SHEETS_NBR                                           decimal
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      2/13/2023
************************************************************************************************************************
* Change History: Date, Name, Description
* 2/13/2023    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_insert_aion_auto_estimation_ref]
    @ACTIVE_IND                                                  bit
  , @ACTIVE_DT                                                   datetime
  , @MONTH_NBR                                                   int
  , @WEIGHT_SQFT_NBR                                             decimal
  , @WEIGHT_COC_NBR                                              decimal
  , @WEIGHT_SHEETS_NBR                                           decimal
  , @WKR_ID_TXT                                                  varchar(100)
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO AUTO_ESTIMATION_REF
          (
            ACTIVE_IND
          , ACTIVE_DT
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , MONTH_NBR
          , WEIGHT_SQFT_NBR
          , WEIGHT_COC_NBR
          , WEIGHT_SHEETS_NBR
          )
     VALUES
          (
            @ACTIVE_IND
          , @ACTIVE_DT
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          , @MONTH_NBR
          , @WEIGHT_SQFT_NBR
          , @WEIGHT_COC_NBR
          , @WEIGHT_SHEETS_NBR
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding AutoEstimationRef record.', 18,1)

RETURN
GO