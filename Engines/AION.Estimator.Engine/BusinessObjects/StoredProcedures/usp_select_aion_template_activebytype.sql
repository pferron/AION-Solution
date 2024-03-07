/***********************************************************************************************************************
* Object:       usp_select_aion_template_activebytype
* Description:  get the template by the type 
* Parameters:   
*               @templatetypeenumid                                                   int
*               @dtfilter DATETIME
* Returns:      Recordset.
* Comments:     
* Version:      1.0
* Created by:   jlindsay
* Created:      5/13/2021
************************************************************************************************************************
* Change History: Date, Name, Description
* 5/13/2021    jlindsay     get the active template by the type 
* 
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_select_aion_template_activebytype] @templatetypeenumid INT,
	@dtfilter DATETIME
AS
BEGIN
	SELECT TOP 1 t.TEMPLATE_ID,
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
	WHERE typ.ENUM_MAPPING_VAL_NBR = @templatetypeenumid
		AND t.ACTIVE_IND = 1
		AND t.ACTIVE_DT <= @dtfilter
	ORDER BY t.ACTIVE_DT DESC
END
