/***********************************************************************************************************************    
* Object:       usp_select_aion_system_role_get_list    
* Description:  Retrieves SystemRole list for given parameter(s).    
* Parameters:       
*               @identity                                                   int    
*    
* Returns:      Recordset.    
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.    
*               This proc expects id_person and/or id_file to generate list; modify as necessary.    
*               Include ORDER BY clause as necessary.    
* Version:      1.0    
* Created by:   AION_user    
* Created:      10/3/2019    
************************************************************************************************************************    
* Change History: Date, Name, Description    
* 10/3/2019    AION_user     Auto-generated    
* 10/11/2019 JL  Correct Columns    
* 10/07/2020    JL  add parent system role id  
* 06-03-2022	jcl clean up code
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_select_aion_system_role_get_Alllist]
AS
BEGIN
	SELECT SYSTEM_ROLE_ID,
		SYSTEM_ROLE_NM,
		WKR_ID_CREATED_TXT,
		CREATED_DTTM,
		WKR_ID_UPDATED_TXT,
		UPDATED_DTTM,
		EXTERNAL_SYSTEM_REF_ID,
		SRC_SYSTEM_VAL_TXT,
		ENUM_MAPPING_VAL_NBR,
		SYSTEM_ROLE_TXT,
		ROLE_OPTIONS_TXT,
		ENABLED_IND,
		PARENT_SYSTEM_ROLE_ID
	FROM [AION].SYSTEM_ROLE;

	RETURN;
END
