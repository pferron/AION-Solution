/***********************************************************************************************************************  
* Object:       usp_select_aion_template_type_get_by_id  
* Description:  Retrieves TemplateType record for given key field(s).  
* Parameters:     
*               @identity                                                    int  
*  
* Returns:      Recordset.  
* Comments:     Developer may need to manually join to other tables, such as code tables,  
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.  
* Version:      1.0  
* Created by:   AION_user  
* Created:      1/28/2021  
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 1/28/2021    AION_user     Auto-generated  
* 10/4/2021	jlindsay	add new fields  
***********************************************************************************************************************/
ALTER PROCEDURE AION.[usp_select_aion_template_type_get_by_id] @identity INT
AS
BEGIN
	SELECT TEMPLATE_TYP_ID,
		TEMPLATE_TYP_NM,
		TEMPLATE_TYP_DESC,
		WKR_ID_CREATED_TXT,
		CREATED_DTTM,
		WKR_ID_UPDATED_TXT,
		UPDATED_DTTM,
		ENUM_MAPPING_VAL_NBR,
		TEMPLATE_MODULE_ID,
		TEMPLATE_TYP_EDITABLE_IND,
		DATA_ELEMENT_ALLOWED_IND
	FROM AION.TEMPLATE_TYPE
	WHERE TEMPLATE_TYP_ID = @identity;

	RETURN
END
