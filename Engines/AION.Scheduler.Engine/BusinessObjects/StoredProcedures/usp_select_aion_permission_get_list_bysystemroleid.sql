/***********************************************************************************************************************    
* Object:       usp_select_aion_permission_get_list_bysystemroleid    
* Description:  Retrieves Permission list for given parameter(s).    
* Parameters:       
*               @systemroleid   int  
* Returns:      Recordset.    
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.    
*               This proc expects id_person and/or id_file to generate list; modify as necessary.    
*               Include ORDER BY clause as necessary.    
* Version:      1.0    
* Created by:   jlindsay    
* Created:      6/10/2020    
************************************************************************************************************************    
* Change History: Date, Name, Description    
* 6/101/2020    jlindsay     create xref list of permissions by system role id  
* 2/7/2022	jlindsay add module enum value
***********************************************************************************************************************/
ALTER PROCEDURE AION.usp_select_aion_permission_get_list_bysystemroleid @systemroleid INT
AS
SELECT PERM.PERMISSION_REF_ID,
	PERM.PERMISSION_NM,
	PERM.MODULE_REF_ID,
	PERM.WKR_ID_CREATED_TXT,
	PERM.CREATED_DTTM,
	PERM.WKR_ID_UPDATED_TXT,
	PERM.UPDATED_DTTM,
	PERM.ENUM_MAPPING_VAL_NBR,
	modulref.ENUM_MAPPING_VAL_NBR AS MODULE_ENUM_MAPPING_VAL_NBR
FROM AION.PERMISSION_REF PERM
INNER JOIN AION.PERMISSION_SYSTEM_ROLE_XREF srole ON PERM.PERMISSION_REF_ID = srole.PERMISSION_REF_ID
INNER JOIN AION.MODULE_REF modulref ON PERM.MODULE_REF_ID = modulref.MODULE_REF_ID
WHERE SYSTEM_ROLE_ID = @systemroleid;

RETURN;
