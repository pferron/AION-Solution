
/***********************************************************************************************************************
* Object:	usp_insert_aion_average_estimation_hours_factor
* Description:	Inserts AverageEstimationHoursFactor record.
* Parameters:
*		@OCCUPANCY_TYP_REF_ID                                        int
*		@CONSTR_TYP_TXT                                              varchar(255)
*		@BUILD_SQFT_FACTOR_NBR                                       decimal
*		@ELCTR_SQFT_FACTOR_NBR                                       decimal
*		@MECH_SQFT_FACTOR_NBR                                        decimal
*		@PLUMB_SQFT_FACTOR_NBR                                       decimal
*		@BUILD_COC_FACTOR_NBR                                        decimal
*		@ELCTR_COC_FACTOR_NBR                                        decimal
*		@MECH_COC_FACTOR_NBR                                         decimal
*		@PLUMB_COC_FACTOR_NBR                                        decimal
*		@BUILD_SHEETS_FACTOR_NBR                                     decimal
*		@ELCTR_SHEETS_FACTOR_NBR                                     decimal
*		@MECH_SHEETS_FACTOR_NBR                                      decimal
*		@PLUMB_SHEETS_FACTOR_NBR                                     decimal
*		@ACTIVE_IND                                                  bit
*		@ACTIVE_DT                                                   datetime
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      12/8/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 12/8/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_insert_aion_average_estimation_hours_factor]
    @OCCUPANCY_TYP_REF_ID                                        int
  , @CONSTR_TYP_TXT                                              varchar(255)
  , @BUILD_SQFT_FACTOR_NBR                                       decimal
  , @ELCTR_SQFT_FACTOR_NBR                                       decimal
  , @MECH_SQFT_FACTOR_NBR                                        decimal
  , @PLUMB_SQFT_FACTOR_NBR                                       decimal
  , @BUILD_COC_FACTOR_NBR                                        decimal
  , @ELCTR_COC_FACTOR_NBR                                        decimal
  , @MECH_COC_FACTOR_NBR                                         decimal
  , @PLUMB_COC_FACTOR_NBR                                        decimal
  , @BUILD_SHEETS_FACTOR_NBR                                     decimal
  , @ELCTR_SHEETS_FACTOR_NBR                                     decimal
  , @MECH_SHEETS_FACTOR_NBR                                      decimal
  , @PLUMB_SHEETS_FACTOR_NBR                                     decimal
  , @ACTIVE_IND                                                  bit
  , @ACTIVE_DT                                                   datetime
  , @WKR_ID_TXT                                                  varchar(100)
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO AVERAGE_ESTIMATION_HOURS_FACTOR
          (
            OCCUPANCY_TYP_REF_ID
          , CONSTR_TYP_TXT
          , BUILD_SQFT_FACTOR_NBR
          , ELCTR_SQFT_FACTOR_NBR
          , MECH_SQFT_FACTOR_NBR
          , PLUMB_SQFT_FACTOR_NBR
          , BUILD_COC_FACTOR_NBR
          , ELCTR_COC_FACTOR_NBR
          , MECH_COC_FACTOR_NBR
          , PLUMB_COC_FACTOR_NBR
          , BUILD_SHEETS_FACTOR_NBR
          , ELCTR_SHEETS_FACTOR_NBR
          , MECH_SHEETS_FACTOR_NBR
          , PLUMB_SHEETS_FACTOR_NBR
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , ACTIVE_IND
          , ACTIVE_DT
          )
     VALUES
          (
            @OCCUPANCY_TYP_REF_ID
          , @CONSTR_TYP_TXT
          , @BUILD_SQFT_FACTOR_NBR
          , @ELCTR_SQFT_FACTOR_NBR
          , @MECH_SQFT_FACTOR_NBR
          , @PLUMB_SQFT_FACTOR_NBR
          , @BUILD_COC_FACTOR_NBR
          , @ELCTR_COC_FACTOR_NBR
          , @MECH_COC_FACTOR_NBR
          , @PLUMB_COC_FACTOR_NBR
          , @BUILD_SHEETS_FACTOR_NBR
          , @ELCTR_SHEETS_FACTOR_NBR
          , @MECH_SHEETS_FACTOR_NBR
          , @PLUMB_SHEETS_FACTOR_NBR
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          , @ACTIVE_IND
          , @ACTIVE_DT
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding AverageEstimationHoursFactor record.', 18,1)

RETURN
GO