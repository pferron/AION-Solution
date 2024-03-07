/***********************************************************************************************************************    
* Object:       usp_select_aion_project_audit_get_list    
* Description:  Retrieves ProjectAudit list for given parameter(s).    
* Parameters:       
*               @PROJECT_ID                                                   int    
*    
* Returns:      Recordset.    
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.    
*               This proc expects id_person and/or id_file to generate list; modify as necessary.    
*               Include ORDER BY clause as necessary.    
* Version:      1.0    
* Created by:   AION_user    
* Created:      2/27/2020    
************************************************************************************************************************    
* Change History: Date, Name, Description    
* 2/27/2020    AION_user     Auto-generated    
* 3/3/2020      jlindsay    search by PROJECT_ID    
* 12/3/2021 jlindsay sort desc by audit dt      
* 2/10/2022 jlindsay    add cycle number and id  
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_select_aion_project_audit_get_list] @PROJECT_ID INT
AS
BEGIN
	SELECT PROJECT_AUDIT.PROJECT_AUDIT_ID,
		PROJECT_AUDIT.PROJECT_ID,
		PROJECT_AUDIT.AUDIT_ACTION_DETAILS_TXT,
		PROJECT_AUDIT.AUDIT_USER_NM,
		PROJECT_AUDIT.AUDIT_DT,
		PROJECT_AUDIT.WKR_ID_CREATED_TXT,
		PROJECT_AUDIT.CREATED_DTTM,
		PROJECT_AUDIT.WKR_ID_UPDATED_TXT,
		PROJECT_AUDIT.UPDATED_DTTM,
		PROJECT_AUDIT.AUDIT_ACTION_REF_ID,
		PROJECT_AUDIT.PROJECT_CYCLE_ID,
		PROJECT_CYCLE.CYCLE_NBR
	FROM AION.PROJECT_AUDIT
	LEFT JOIN AION.PROJECT_CYCLE ON PROJECT_AUDIT.PROJECT_CYCLE_ID = PROJECT_CYCLE.PROJECT_CYCLE_ID
	WHERE PROJECT_AUDIT.PROJECT_ID = @PROJECT_ID
	ORDER BY PROJECT_AUDIT.AUDIT_DT DESC;

	RETURN
END
