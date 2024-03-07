
/***********************************************************************************************************************
* Object:       usp_select_aion_occupancy_type_ref_getbyprojectoccpncy
* Description:  Retrieves OccupancyTypeRef record for given key field(s).
* Parameters:   
*           @PROJECT_OCCUPANCY_TYP_MAP_NM VARCHAR(50),
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   carol.lindsay
* Created:      10/29/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/29/2019    carol.lindsay     
* 07/30/2021    jallen        Eliminate a mapping table for project occupancy from Accela.
***********************************************************************************************************************/

ALTER PROCEDURE usp_select_aion_occupancy_type_ref_getbyprojectoccpncy
(
  @PROJECT_OCCUPANCY_TYP_MAP_NM VARCHAR(50))
AS
BEGIN
DECLARE 
  @OCCUPANCY_TYP_REF_ID INT;
SELECT @OCCUPANCY_TYP_REF_ID=OCCUPANCY_TYP_REF_ID
FROM AION.PROJECT_OCCUPANCY_TYPE_MAP_REF
WHERE PROJECT_OCCUPANCY_TYP_MAP_NM=@PROJECT_OCCUPANCY_TYP_MAP_NM;

SELECT OCCUPANCY_TYP_REF_ID, 
       OCCUPANCY_TYP_NM, 
       WKR_ID_CREATED_TXT, 
       CREATED_DTTM, 
       WKR_ID_UPDATED_TXT, 
       UPDATED_DTTM
FROM OCCUPANCY_TYPE_REF
WHERE OCCUPANCY_TYP_REF_ID=@OCCUPANCY_TYP_REF_ID;
RETURN;
END; 
GO