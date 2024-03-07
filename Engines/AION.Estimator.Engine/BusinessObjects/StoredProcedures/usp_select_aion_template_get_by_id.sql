/***********************************************************************************************************************
* Object:       usp_select_aion_template_get_by_id
* Description:  Retrieves Template record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   AION_user
* Created:      1/26/2021
************************************************************************************************************************
* Change History: Date, Name, Description
* 1/26/2021    AION_user     Auto-generated
* 
***********************************************************************************************************************/
ALTER PROCEDURE AION.[usp_select_aion_template_get_by_id] @identity INT
AS
SELECT TEMPLATE_ID,
	TEMPLATE_NM,
	TEMPLATE_TYP_ID,
	TEMPLATE_TXT,
	ACTIVE_IND,
	ACTIVE_DT,
	WKR_ID_CREATED_TXT,
	CREATED_DTTM,
	WKR_ID_UPDATED_TXT,
	UPDATED_DTTM
FROM AION.TEMPLATE
WHERE
	TEMPLATE_ID = @identity

RETURN
GO


