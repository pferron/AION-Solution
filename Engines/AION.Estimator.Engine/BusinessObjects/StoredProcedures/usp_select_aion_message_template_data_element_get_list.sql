/***********************************************************************************************************************
* Object:       usp_select_aion_message_template_data_element_get_list
* Description:  Retrieves DataElement list for given parameter(s).
* Parameters:   
*
* Returns:      Recordset.
* Comments:     
* Version:      1.0
* Created by:   AION_user
* Created:      1/28/2021
************************************************************************************************************************
* Change History: Date, Name, Description
* 1/28/2021    AION_user     Auto-generated
* 5/7/2021  jlindsay    removed filter parameter
***********************************************************************************************************************/
ALTER PROCEDURE AION.[usp_select_aion_message_template_data_element_get_list]
AS
BEGIN
	SELECT DATA_ELEMENT_ID,
		DATA_ELEMENT_NM,
		DATA_ELEMENT_DESC,
		VAL_TXT,
		WKR_ID_CREATED_TXT,
		CREATED_DTTM,
		WKR_ID_UPDATED_TXT,
		UPDATED_DTTM,
		ENUM_MAPPING_VAL_NBR
	FROM AION.DATA_ELEMENTS

	RETURN
END
