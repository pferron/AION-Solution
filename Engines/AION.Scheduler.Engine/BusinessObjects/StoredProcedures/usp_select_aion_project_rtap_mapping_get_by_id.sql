
/***********************************************************************************************************************
* Object:       usp_select_aion_project_rtap_mapping_get_by_id
* Description:  Retrieves ProjectRtapMapping record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   AION_user
* Created:      10/2/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/2/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_project_rtap_mapping_get_by_id]

    @identity                                                    int

AS

       SELECT 
            PROJECT_RTAP_MAPPING_ID
          , PROJECT_ID
          , ORIGINAL_PROJECT_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM

       FROM PROJECT_RTAP_MAPPING

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          PROJECT_RTAP_MAPPING_ID = @identity
          

RETURN

GO