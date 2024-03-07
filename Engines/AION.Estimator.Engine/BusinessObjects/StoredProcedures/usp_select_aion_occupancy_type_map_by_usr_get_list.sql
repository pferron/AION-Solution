/****** Object:  StoredProcedure [AION].[usp_select_aion_occupancy_type_map_by_usr_get_list]    Script Date: 7/10/2020 11:59:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

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

ALTER PROCEDURE [AION].[usp_select_aion_occupancy_type_map_by_usr_get_list]

    @identity                                                   int

AS

         
  SELECT 
		U.[USER_ID] as [USER_ID], 
		O.OCCUPANCY_TYP_REF_ID as OCCUPANCY_TYP_REF_ID, 
		P.PROJECT_OCCUPANCY_TYP_MAP_REF_ID as PROJECT_OCCUPANCY_TYP_REF_ID,
		O.OCCUPANCY_TYP_NM as OCCUPANCY_TYP_NM,
		P.PROJECT_OCCUPANCY_TYP_MAP_NM as PROJECT_OCCUPANCY_TYP_MAP_NM

  FROM [AION].[PROJECT_OCCUPANCY_TYPE_RELATIONSHIP] R
  INNER JOIN OCCUPANCY_TYPE_REF O ON R.OCCUPANCY_TYP_REF_ID = O.OCCUPANCY_TYP_REF_ID
  INNER JOIN PROJECT_OCCUPANCY_TYPE_MAP_REF P ON R.PROJECT_OCCUPANCY_TYP_MAP_REF_ID = P.PROJECT_OCCUPANCY_TYP_MAP_REF_ID 
  INNER JOIN OCCUPANCY_SQUARE_FOOTAGE_USER_MAP OU ON OU.OCCUPANCY_TYP_REF_ID = O.OCCUPANCY_TYP_REF_ID
  INNER JOIN AION.[USER] U ON U.[USER_ID] = OU.[USER_ID]

  WHERE U.[USER_ID] = @identity

RETURN

