
/***********************************************************************************************************************
* Object:       usp_update_aion_legacy_project_estimation_hours_ref
* Description:  Updates LegacyProjectEstimationHoursRef record using supplied parameters.
* Parameters:   
*               @LEGACY_PROJECT_ESTIMATION_HOURS_REF_ID                      int
*               @OCCUPANCY_TYP_REF_ID                                        int
*               @CONSTR_TYP_TXT                                              varchar(255)
*               @TOTAL_PROJECTS_CNT                                          decimal
*               @BUILD_HOURS_NBR                                             decimal
*               @ELCTR_HOURS_NBR                                             decimal
*               @MECH_HOURS_NBR                                              decimal
*               @PLUMB_HOURS_NBR                                             decimal
*               @UPDATED_DTTM                                                datetime
*               @TOTAL_SQUARE_FOOTAGE_CNT                                    decimal
*               @TOTAL_CONSTR_COST_AMT                                       decimal
*               @TOTAL_SHEETS_CNT                                            decimal
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      8/31/2023
************************************************************************************************************************
* Change History: Date, Name, Description
* 8/31/2023    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_update_aion_legacy_project_estimation_hours_ref]

    @LEGACY_PROJECT_ESTIMATION_HOURS_REF_ID                      int
  , @OCCUPANCY_TYP_REF_ID                                        int
  , @CONSTR_TYP_TXT                                              varchar(255)
  , @TOTAL_PROJECTS_CNT                                          decimal
  , @BUILD_HOURS_NBR                                             decimal
  , @ELCTR_HOURS_NBR                                             decimal
  , @MECH_HOURS_NBR                                              decimal
  , @PLUMB_HOURS_NBR                                             decimal
  , @UPDATED_DTTM                                                datetime
  , @TOTAL_SQUARE_FOOTAGE_CNT                                    decimal
  , @TOTAL_CONSTR_COST_AMT                                       decimal
  , @TOTAL_SHEETS_CNT                                            decimal
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE LEGACY_PROJECT_ESTIMATION_HOURS_REF
       SET
            OCCUPANCY_TYP_REF_ID                                         = @OCCUPANCY_TYP_REF_ID
          , CONSTR_TYP_TXT                                               = @CONSTR_TYP_TXT
          , TOTAL_PROJECTS_CNT                                           = @TOTAL_PROJECTS_CNT
          , BUILD_HOURS_NBR                                              = @BUILD_HOURS_NBR
          , ELCTR_HOURS_NBR                                              = @ELCTR_HOURS_NBR
          , MECH_HOURS_NBR                                               = @MECH_HOURS_NBR
          , PLUMB_HOURS_NBR                                              = @PLUMB_HOURS_NBR
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()
          , TOTAL_SQUARE_FOOTAGE_CNT                                     = @TOTAL_SQUARE_FOOTAGE_CNT
          , TOTAL_CONSTR_COST_AMT                                        = @TOTAL_CONSTR_COST_AMT
          , TOTAL_SHEETS_CNT                                             = @TOTAL_SHEETS_CNT

       WHERE
          LEGACY_PROJECT_ESTIMATION_HOURS_REF_ID                         = @LEGACY_PROJECT_ESTIMATION_HOURS_REF_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating LegacyProjectEstimationHoursRef record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO