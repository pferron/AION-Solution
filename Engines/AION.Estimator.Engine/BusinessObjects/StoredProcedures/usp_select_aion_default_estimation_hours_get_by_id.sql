
/***********************************************************************************************************************
* Object:       usp_select_aion_default_estimation_hours_get_by_id
* Description:  Retrieves DefaultEstimationHours record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   AION_user
* Created:      10/10/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/10/2019    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_default_estimation_hours_get_by_id]

    @identity                                                    int

AS

       SELECT 
            DEFAULT_ESTIMATION_HOURS_ID
          , DEFAULT_HOURS_NBR
          , WKR_ID_CREATED_TXT
          , WKR_ID_UPDATED_TXT
          , CREATED_DTTM
          , UPDATED_DTTM
          , BUSINESS_REF_ID
          , PROJECT_TYP_REF_ID

       FROM DEFAULT_ESTIMATION_HOURS

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          DEFAULT_ESTIMATION_HOURS_ID = @identity
          

RETURN

GO