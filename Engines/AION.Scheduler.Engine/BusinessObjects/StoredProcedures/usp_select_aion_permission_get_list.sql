/***********************************************************************************************************************        
* Object:       usp_select_aion_permission_get_list        
* Description:  Retrieves Permission list for given parameter(s).        
* Parameters:           
*        
* Returns:      Recordset.        
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.        
*               This proc expects id_person and/or id_file to generate list; modify as necessary.        
*               Include ORDER BY clause as necessary.        
* Version:      1.0        
* Created by:   AION_user        
* Created:      5/11/2020        
************************************************************************************************************************        
* Change History: Date, Name, Description        
* 5/11/2020    AION_user     Auto-generated        
* 01/08/2021    jlindsay    exclude 'deprecated' module       
* 2/7/2022 jlindsay add module enum value    
* 3/30/2022 jlindsay add PERMISSION_DISPLAY_NM  
* 5/4/2023 jlindsay remove module for "add project files" 8
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_select_aion_permission_get_list]
AS
BEGIN
	-- Exclude inactive modules, Add Project Files and deprecated     
	WITH Modules
	AS (
		SELECT MODULE_REF_ID,ENUM_MAPPING_VAL_NBR AS MODULE_ENUM_MAPPING_VAL_NBR
		FROM AION.MODULE_REF
		WHERE ENUM_MAPPING_VAL_NBR NOT IN (
				10,
				8
				)
		)
	SELECT PERM.PERMISSION_REF_ID,
		PERM.PERMISSION_NM,
		PERM.MODULE_REF_ID,
		PERM.WKR_ID_CREATED_TXT,
		PERM.CREATED_DTTM,
		PERM.WKR_ID_UPDATED_TXT,
		PERM.UPDATED_DTTM,
		PERM.ENUM_MAPPING_VAL_NBR,
		MODULE_ENUM_MAPPING_VAL_NBR,
		PERM.PERMISSION_DISPLAY_NM
	FROM AION.PERMISSION_REF PERM
	INNER JOIN Modules modulref ON PERM.MODULE_REF_ID = modulref.MODULE_REF_ID

	--      
	RETURN;
END;
