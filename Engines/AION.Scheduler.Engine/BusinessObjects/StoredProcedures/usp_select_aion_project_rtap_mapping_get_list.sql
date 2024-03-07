
/***********************************************************************************************************************
* Object:       usp_select_aion_project_rtap_mapping_get_list
* Description:  Retrieves ProjectRtapMapping list for given parameter(s).
* Parameters:   
*               @identity                                                   int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      10/2/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/2/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_project_rtap_mapping_get_list]

    @identity                                                   int

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