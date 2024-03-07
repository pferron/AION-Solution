
/***********************************************************************************************************************
* Object:	usp_insert_aion_legacy_project_estimation_hours_ref
* Description:	Inserts LegacyProjectEstimationHoursRef record.
* Parameters:
*		@LEGACY_PROJECT_ESTIMATION_HOURS_REF_ID                      int
*		@OCCUPANCY_TYP_REF_ID                                        int
*		@CONSTR_TYP_TXT                                              varchar(255)
*		@TOTAL_PROJECTS_CNT                                          decimal
*		@BUILD_HOURS_NBR                                             decimal
*		@ELCTR_HOURS_NBR                                             decimal
*		@MECH_HOURS_NBR                                              decimal
*		@PLUMB_HOURS_NBR                                             decimal
*		@TOTAL_SQUARE_FOOTAGE_CNT                                    decimal
*		@TOTAL_CONSTR_COST_AMT                                       decimal
*		@TOTAL_SHEETS_CNT                                            decimal
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      N/A
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      8/31/2023
************************************************************************************************************************
* Change History: Date, Name, Description
* 8/31/2023    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_insert_aion_legacy_project_estimation_hours_ref]
    @LEGACY_PROJECT_ESTIMATION_HOURS_REF_ID                      int
  , @OCCUPANCY_TYP_REF_ID                                        int
  , @CONSTR_TYP_TXT                                              varchar(255)
  , @TOTAL_PROJECTS_CNT                                          decimal
  , @BUILD_HOURS_NBR                                             decimal
  , @ELCTR_HOURS_NBR                                             decimal
  , @MECH_HOURS_NBR                                              decimal
  , @PLUMB_HOURS_NBR                                             decimal
  , @TOTAL_SQUARE_FOOTAGE_CNT                                    decimal
  , @TOTAL_CONSTR_COST_AMT                                       decimal
  , @TOTAL_SHEETS_CNT                                            decimal
  , @WKR_ID_TXT                                                  varchar(100)

AS

     DECLARE @error   int

     INSERT INTO LEGACY_PROJECT_ESTIMATION_HOURS_REF
          (
            LEGACY_PROJECT_ESTIMATION_HOURS_REF_ID
          , OCCUPANCY_TYP_REF_ID
          , CONSTR_TYP_TXT
          , TOTAL_PROJECTS_CNT
          , BUILD_HOURS_NBR
          , ELCTR_HOURS_NBR
          , MECH_HOURS_NBR
          , PLUMB_HOURS_NBR
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , TOTAL_SQUARE_FOOTAGE_CNT
          , TOTAL_CONSTR_COST_AMT
          , TOTAL_SHEETS_CNT
          )
     VALUES
          (
            @LEGACY_PROJECT_ESTIMATION_HOURS_REF_ID
          , @OCCUPANCY_TYP_REF_ID
          , @CONSTR_TYP_TXT
          , @TOTAL_PROJECTS_CNT
          , @BUILD_HOURS_NBR
          , @ELCTR_HOURS_NBR
          , @MECH_HOURS_NBR
          , @PLUMB_HOURS_NBR
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          , @TOTAL_SQUARE_FOOTAGE_CNT
          , @TOTAL_CONSTR_COST_AMT
          , @TOTAL_SHEETS_CNT
          )

     SELECT @error = @@ERROR
     IF @error != 0
          RAISERROR('Error adding LegacyProjectEstimationHoursRef record.', 18,1)

RETURN
GO