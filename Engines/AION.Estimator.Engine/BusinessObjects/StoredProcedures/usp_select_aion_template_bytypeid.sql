/***********************************************************************************************************************
* Object:       usp_select_aion_template_list_bytypeid
* Description:  get the template by the type 
* Parameters:   
*               @templatetypeid                                                    int
*
* Returns:      Recordset.
* Comments:     
* Version:      1.0
* Created by:   jlindsay
* Created:      5/05/2021
************************************************************************************************************************
* Change History: Date, Name, Description
* 5/05/2021    jlindsay     get the template by the type 
* 
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_select_aion_template_list_bytypeid] @templatetypeid INT
AS
BEGIN
	SELECT t.TEMPLATE_ID,
		t.TEMPLATE_NM,
		t.TEMPLATE_TYP_ID,
		t.TEMPLATE_TXT,
		t.ACTIVE_IND,
		t.ACTIVE_DT,
		t.WKR_ID_CREATED_TXT,
		t.CREATED_DTTM,
		t.WKR_ID_UPDATED_TXT,
		t.UPDATED_DTTM
	FROM [AION].TEMPLATE t
	INNER JOIN [AION].TEMPLATE_TYPE typ ON t.TEMPLATE_TYP_ID = typ.TEMPLATE_TYP_ID
	WHERE typ.ENUM_MAPPING_VAL_NBR = CASE 
			WHEN @templatetypeid = - 1
				THEN typ.ENUM_MAPPING_VAL_NBR
			ELSE @templatetypeid
			END
		AND typ.TEMPLATE_TYP_EDITABLE_IND = 1;
END
