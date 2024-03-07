/***********************************************************************************************************************  
* Object:       usp_select_aion_user_system_role_relationship_get_by_userroleid  
* Description:  Retrieves UserSystemRoleRelationship record for given key field(s).  
* Parameters:     
*               @userid                                                    int  
*               @roleid              int  
*  
* Returns:      Recordset.  
* Comments:     Developer may need to manually join to other tables, such as code tables,  
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.  
* Version:      1.0  
* Created by:   jeanine  
* Created:      11/04/2019  
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 11/04/2019    jeanine     Auto-generated         
* 2/7/2022	jlindsay add system role enum val  
*   
***********************************************************************************************************************/
ALTER PROCEDURE AION.[usp_select_aion_user_system_role_relationship_get_by_userroleid] @userid INT,
	@roleid INT
AS
BEGIN
	SELECT usr.USER_SYSTEM_ROLE_RELATIONSHIP_ID,
		usr.[USER_ID],
		usr.SYSTEM_ROLE_ID,
		usr.WKR_ID_CREATED_TXT,
		usr.CREATED_DTTM,
		usr.WKR_ID_UPDATED_TXT,
		usr.UPDATED_DTTM,
		sr.ENUM_MAPPING_VAL_NBR AS SYSTEM_ROLE_ENUM_MAPPING_VAL_NBR
	FROM AION.USER_SYSTEM_ROLE_RELATIONSHIP usr
	INNER JOIN AION.SYSTEM_ROLE sr ON usr.SYSTEM_ROLE_ID = sr.SYSTEM_ROLE_ID
	WHERE usr.[User_id] = @userid
		AND usr.system_role_id = @roleid;
END;

RETURN;
