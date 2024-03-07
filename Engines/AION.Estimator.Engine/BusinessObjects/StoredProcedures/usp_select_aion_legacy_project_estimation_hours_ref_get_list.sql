
/***********************************************************************************************************************
* Object:       usp_select_aion_legacy_project_estimation_hours_ref_get_list
* Description:  Retrieves LegacyProjectEstimationHoursRef list for given parameter(s).
* Parameters:   
*               @identity                                                   int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      8/31/2023
************************************************************************************************************************
* Change History: Date, Name, Description
* 8/31/2023    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_legacy_project_estimation_hours_ref_get_list]

    @identity                                                   int

AS

       SELECT 
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

       FROM LEGACY_PROJECT_ESTIMATION_HOURS_REF

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          LEGACY_PROJECT_ESTIMATION_HOURS_REF_ID = @identity
          

RETURN

GO