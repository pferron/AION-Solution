
/***********************************************************************************************************************
* Object:       usp_update_aion_average_estimation_hours_factor
* Description:  Updates AverageEstimationHoursFactor record using supplied parameters.
* Parameters:   
*               @AVERAGE_ESTIMATION_HOURS_FACTOR_ID                          int
*               @OCCUPANCY_TYP_REF_ID                                        int
*               @CONSTR_TYP_TXT                                              varchar(255)
*               @BUILD_SQFT_FACTOR_NBR                                       decimal
*               @ELCTR_SQFT_FACTOR_NBR                                       decimal
*               @MECH_SQFT_FACTOR_NBR                                        decimal
*               @PLUMB_SQFT_FACTOR_NBR                                       decimal
*               @BUILD_COC_FACTOR_NBR                                        decimal
*               @ELCTR_COC_FACTOR_NBR                                        decimal
*               @MECH_COC_FACTOR_NBR                                         decimal
*               @PLUMB_COC_FACTOR_NBR                                        decimal
*               @BUILD_SHEETS_FACTOR_NBR                                     decimal
*               @ELCTR_SHEETS_FACTOR_NBR                                     decimal
*               @MECH_SHEETS_FACTOR_NBR                                      decimal
*               @PLUMB_SHEETS_FACTOR_NBR                                     decimal
*               @UPDATED_DTTM                                                datetime
*               @ACTIVE_IND                                                  bit
*               @ACTIVE_DT                                                   datetime
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      12/8/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 12/8/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_update_aion_average_estimation_hours_factor]

    @AVERAGE_ESTIMATION_HOURS_FACTOR_ID                          int
  , @OCCUPANCY_TYP_REF_ID                                        int
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
  , @UPDATED_DTTM                                                datetime
  , @ACTIVE_IND                                                  bit
  , @ACTIVE_DT                                                   datetime
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE AVERAGE_ESTIMATION_HOURS_FACTOR
       SET
            OCCUPANCY_TYP_REF_ID                                         = @OCCUPANCY_TYP_REF_ID
          , CONSTR_TYP_TXT                                               = @CONSTR_TYP_TXT
          , BUILD_SQFT_FACTOR_NBR                                        = @BUILD_SQFT_FACTOR_NBR
          , ELCTR_SQFT_FACTOR_NBR                                        = @ELCTR_SQFT_FACTOR_NBR
          , MECH_SQFT_FACTOR_NBR                                         = @MECH_SQFT_FACTOR_NBR
          , PLUMB_SQFT_FACTOR_NBR                                        = @PLUMB_SQFT_FACTOR_NBR
          , BUILD_COC_FACTOR_NBR                                         = @BUILD_COC_FACTOR_NBR
          , ELCTR_COC_FACTOR_NBR                                         = @ELCTR_COC_FACTOR_NBR
          , MECH_COC_FACTOR_NBR                                          = @MECH_COC_FACTOR_NBR
          , PLUMB_COC_FACTOR_NBR                                         = @PLUMB_COC_FACTOR_NBR
          , BUILD_SHEETS_FACTOR_NBR                                      = @BUILD_SHEETS_FACTOR_NBR
          , ELCTR_SHEETS_FACTOR_NBR                                      = @ELCTR_SHEETS_FACTOR_NBR
          , MECH_SHEETS_FACTOR_NBR                                       = @MECH_SHEETS_FACTOR_NBR
          , PLUMB_SHEETS_FACTOR_NBR                                      = @PLUMB_SHEETS_FACTOR_NBR
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()
          , ACTIVE_IND                                                   = @ACTIVE_IND
          , ACTIVE_DT                                                    = @ACTIVE_DT

       WHERE
          AVERAGE_ESTIMATION_HOURS_FACTOR_ID                             = @AVERAGE_ESTIMATION_HOURS_FACTOR_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating AverageEstimationHoursFactor record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO