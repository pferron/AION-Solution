
/***********************************************************************************************************************
* Object:       usp_select_aion_occupancy_sqr_footage_usr_map_get_list
* Description:  Retrieves OccupancySqrFootageUsrMap list for given parameter(s).
* Parameters:   
*               @identity                                                   int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      4/30/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 4/30/2020    AION_user     Auto-generated
* * 5/26/2020 gayatri Get list by user_ID
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_occupancy_sqr_footage_usr_map_get_list]

    @identity                                                   int

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
        
          USER_ID = @identity
          

RETURN

GO