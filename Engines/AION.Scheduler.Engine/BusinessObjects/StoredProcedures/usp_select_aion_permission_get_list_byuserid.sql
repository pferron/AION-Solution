/***********************************************************************************************************************        
* Object:       usp_select_aion_permission_get_list_byuserid        
* Description:  Retrieves Permission list for given parameter(s).        
* Parameters:           
*               @userid   int      
* Returns:      Recordset.        
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.        
*               This proc expects id_person and/or id_file to generate list; modify as necessary.        
*               Include ORDER BY clause as necessary.        
* Version:      1.0        
* Created by:   jlindsay        
* Created:      6/10/2020        
************************************************************************************************************************        
* Change History: Date, Name, Description        
* 6/10/2020    jlindsay     create xref list of permissions by user id       
* 12/23/2020    jlindsay change return to show user permissions if available, system role permissions if no user perms. 
* 2/7/2022	jlindsay add module enum value
***********************************************************************************************************************/
ALTER PROCEDURE AION.usp_select_aion_permission_get_list_byuserid @userid INT
AS
BEGIN
	IF EXISTS (
			SELECT 's'
			FROM AION.PERMISSION_REF PERM
			INNER JOIN AION.USER_PERMISSION_XREF usr ON PERM.PERMISSION_REF_ID = usr.PERMISSION_REF_ID
			WHERE [USER_ID] = @userid
			)
	BEGIN
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
		INNER JOIN AION.USER_PERMISSION_XREF usr ON PERM.PERMISSION_REF_ID = usr.PERMISSION_REF_ID
		INNER JOIN AION.MODULE_REF modulref ON PERM.MODULE_REF_ID = modulref.MODULE_REF_ID
		WHERE [USER_ID] = @userid;
	END;
	ELSE
	BEGIN
		WITH userroles
		AS (
			SELECT USER_SYSTEM_ROLE_RELATIONSHIP_ID,
				[USER_ID],
				xref.SYSTEM_ROLE_ID
			FROM AION.USER_SYSTEM_ROLE_RELATIONSHIP xref
			INNER JOIN AION.SYSTEM_ROLE rol ON xref.SYSTEM_ROLE_ID = rol.SYSTEM_ROLE_ID
			WHERE rol.ENABLED_IND = 1
				AND [USER_ID] = @userid
			)
		SELECT PERM.PERMISSION_REF_ID,
			PERM.PERMISSION_NM,
			PERM.MODULE_REF_ID,
			PERM.WKR_ID_CREATED_TXT,
			PERM.CREATED_DTTM,
			PERM.WKR_ID_UPDATED_TXT,
			PERM.UPDATED_DTTM,
			PERM.ENUM_MAPPING_VAL_NBR
		FROM AION.PERMISSION_REF PERM
		INNER JOIN AION.PERMISSION_SYSTEM_ROLE_XREF srole ON PERM.PERMISSION_REF_ID = srole.PERMISSION_REF_ID
		INNER JOIN AION.MODULE_REF modulref ON PERM.MODULE_REF_ID = modulref.MODULE_REF_ID
		INNER JOIN userroles u ON srole.SYSTEM_ROLE_ID = u.SYSTEM_ROLE_ID;
	END;

	RETURN;
END;
