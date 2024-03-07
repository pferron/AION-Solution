
/***********************************************************************************************************************
* Object:       usp_select_aion_auto_estimation_ref_get_list
* Description:  Retrieves AutoEstimationRef list for given parameter(s).
* Parameters:   
*               @identity                                                   int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      2/13/2023
************************************************************************************************************************
* Change History: Date, Name, Description
* 2/13/2023    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_auto_estimation_ref_get_list]

    @identity                                                   int

AS

       SELECT 
            ACTIVE_IND
          , ACTIVE_DT
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , AUTO_ESTIMATION_REF_ID
          , MONTH_NBR
          , WEIGHT_SQFT_NBR
          , WEIGHT_COC_NBR
          , WEIGHT_SHEETS_NBR

       FROM AUTO_ESTIMATION_REF

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          AUTO_ESTIMATION_REF_ID = @identity
          

RETURN

GO