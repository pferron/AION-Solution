
/***********************************************************************************************************************
* Object:       usp_select_aion_occupancy_sqr_footage_usr_map_get_by_id
* Description:  Retrieves OccupancySqrFootageUsrMap record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   AION_user
* Created:      4/30/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 4/30/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_occupancy_sqr_footage_usr_map_get_by_id]

    @identity                                                    int

AS

       SELECT 
            USER_ID
          , OCCUPANCY_TYP_REF_ID
          , SQUARE_FOOTAGE_REF_ID
          , OCCUPANCY_SQUARE_FOOTAGE_USER_MAP_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM

       FROM OCCUPANCY_SQUARE_FOOTAGE_USER_MAP

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          OCCUPANCY_SQUARE_FOOTAGE_USER_MAP_ID = @identity
          

RETURN

GO