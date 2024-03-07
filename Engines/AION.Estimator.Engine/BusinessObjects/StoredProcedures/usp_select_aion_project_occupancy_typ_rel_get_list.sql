
/***********************************************************************************************************************
* Object:       usp_select_aion_project_occupancy_typ_rel_get_list
* Description:  Retrieves ProjectOccupancyTypRel list for given parameter(s).
* Parameters:   
*               @identity                                                   int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      10/28/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/28/2019    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_project_occupancy_typ_rel_get_list]

    @identity                                                   int

AS

       SELECT 
            PROJECT_OCCUPANCY_TYPE_RELATIONSHIP_ID
          , OCCUPANCY_TYP_REF_ID
          , PROJECT_OCCUPANCY_TYP_MAP_REF_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM

       FROM PROJECT_OCCUPANCY_TYPE_RELATIONSHIP

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          PROJECT_OCCUPANCY_TYPE_RELATIONSHIP_ID = @identity
          

RETURN

GO