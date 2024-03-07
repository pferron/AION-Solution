/***********************************************************************************************************************  
* Object:       usp_select_aion_user_system_role_relationship_get_list_byuserid  
* Description:  Retrieves UserSystemRoleRelationship list for given parameter(s).  
* Parameters:     
*               @userid                                                   int  
*  
* Returns:      Recordset.  
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.  
*               This proc expects id_person and/or id_file to generate list; modify as necessary.  
*               Include ORDER BY clause as necessary.  
* Version:      1.0  
* Created by:   jeanine lindsay  
* Created:      11/04/2019  
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 11/04/2019    jeanine       
* 2/7/2022	jlindsay add system role enum val  
***********************************************************************************************************************/
ALTER PROCEDURE AION.[usp_select_aion_user_system_role_relationship_get_list_byuserid] @userid INT
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
	WHERE usr.[USER_ID] = @userid;

	RETURN;
END;
