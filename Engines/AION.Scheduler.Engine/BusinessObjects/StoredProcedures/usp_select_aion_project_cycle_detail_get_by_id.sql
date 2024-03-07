
/***********************************************************************************************************************
* Object:       usp_select_aion_project_cycle_detail_get_by_id
* Description:  Retrieves ProjectCycleDetail record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   AION_user
* Created:      10/14/2021
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/14/2021    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_project_cycle_detail_get_by_id]

    @identity                                                    int

AS

       SELECT 
            PROJECT_CYCLE_DETAIL_ID
          , PROJECT_CYCLE_ID
          , BUSINESS_REF_ID
          , REREVIEW_HOURS_NBR
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM

       FROM PROJECT_CYCLE_DETAIL

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          PROJECT_CYCLE_DETAIL_ID = @identity
          

RETURN

GO