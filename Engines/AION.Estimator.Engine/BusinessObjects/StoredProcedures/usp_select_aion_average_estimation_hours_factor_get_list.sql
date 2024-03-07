
/***********************************************************************************************************************
* Object:       usp_select_aion_average_estimation_hours_factor_get_list
* Description:  Retrieves AverageEstimationHoursFactor list for given parameter(s).
* Parameters:   
*               @identity                                                   int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      12/8/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 12/8/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_average_estimation_hours_factor_get_list]

    @identity                                                   int

AS

       SELECT 
            AVERAGE_ESTIMATION_HOURS_FACTOR_ID
          , OCCUPANCY_TYP_REF_ID
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

       FROM AVERAGE_ESTIMATION_HOURS_FACTOR

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          AVERAGE_ESTIMATION_HOURS_FACTOR_ID = @identity
          

RETURN

GO