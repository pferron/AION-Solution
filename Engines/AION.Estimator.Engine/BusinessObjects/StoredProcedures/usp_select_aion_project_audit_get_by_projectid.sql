/***********************************************************************************************************************  
* Object:       usp_select_aion_project_audit_get_by_projectid  
* Description:  Retrieves ProjectAudit record for given key field(s).  
* Parameters:     
*               @PROJECT_ID                                                    int  
*  
* Returns:      Recordset.  
* Comments:     Developer may need to manually join to other tables, such as code tables,  
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.  
* Version:      1.0  
* Created by:   AION_user  
* Created:      2/27/2020  
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 2/27/2020    AION_user     Auto-generated    
* 2/10/2022 jlindsay    add cycle number and id  
*   
***********************************************************************************************************************/
ALTER PROCEDURE AION.[usp_select_aion_project_audit_get_by_projectid] @PROJECT_ID INT
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

	RETURN
END
