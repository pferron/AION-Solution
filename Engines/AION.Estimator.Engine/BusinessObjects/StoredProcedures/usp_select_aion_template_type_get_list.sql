/***********************************************************************************************************************
* Object:       usp_select_aion_template_type_get_list
* Description:  Retrieves TemplateType list for given parameter(s).
* Parameters:   
*               @moduleid                                                   int
*
* Returns:      Recordset.
* Comments:     
* Version:      1.0
* Created by:   AION_user
* Created:      5/6/2021
************************************************************************************************************************
* Change History: Date, Name, Description
* 5/6/2021    jlindsay     Auto-generated, added module type filter
* 
***********************************************************************************************************************/
ALTER PROCEDURE AION.[usp_select_aion_template_type_get_list] @moduleid INT
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
		TEMPLATE_TYP_EDITABLE_IND
	FROM AION.TEMPLATE_TYPE
	WHERE TEMPLATE_MODULE_ID = @moduleid
		AND TEMPLATE_TYP_EDITABLE_IND = 1

	RETURN
END
