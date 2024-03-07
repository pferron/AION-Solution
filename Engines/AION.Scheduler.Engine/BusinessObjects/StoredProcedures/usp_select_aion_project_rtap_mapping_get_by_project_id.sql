
/***********************************************************************************************************************
* Object:       usp_select_aion_project_rtap_mapping_get_by_project_id
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

CREATE PROCEDURE [usp_select_aion_project_rtap_mapping_get_by_project_id]

    @identity                                                    int

AS

        SELECT 
            PRM.PROJECT_RTAP_MAPPING_ID
          , PRM.PROJECT_ID
          , PRM.ORIGINAL_PROJECT_ID
          , PRM.WKR_ID_CREATED_TXT
          , PRM.CREATED_DTTM
          , PRM.WKR_ID_UPDATED_TXT
          , PRM.UPDATED_DTTM
		  , P.SRC_SYSTEM_VAL_TXT AS 'ORIGINAL_PROJECT_NUMBER'

       FROM PROJECT_RTAP_MAPPING PRM
	   INNER JOIN PROJECT P ON PRM.ORIGINAL_PROJECT_ID = P.PROJECT_ID

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          PRM.PROJECT_ID = @identity
          

RETURN

GO