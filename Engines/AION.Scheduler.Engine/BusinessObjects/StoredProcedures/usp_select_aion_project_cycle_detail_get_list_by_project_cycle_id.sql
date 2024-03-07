
/***********************************************************************************************************************
* Object:       usp_select_aion_project_cycle_detail_get_list_by_project_cycle_id
* Description:  Retrieves ProjectCycleDetail list for given parameter(s).
* Parameters:   
*               @projectCycleId                                                   int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      10/11/2021
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/11/2021    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_project_cycle_detail_get_list_by_project_cycle_id]

    @projectCycleId                                                   int

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
        
          PROJECT_CYCLE_ID = @projectCycleId
          

RETURN

GO