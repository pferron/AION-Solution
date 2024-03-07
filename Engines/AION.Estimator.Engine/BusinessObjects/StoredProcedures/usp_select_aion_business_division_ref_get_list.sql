/***********************************************************************************************************************
* Object:       usp_select_aion_business_division_ref_get_list
* Description:  Retrieves BusinessDivisionRef list for given parameter(s).
* Parameters:   
*               @identity                                                   int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      3/21/2022
************************************************************************************************************************
* Change History: Date, Name, Description
* 3/21/2022    AION_user     Auto-generated
* 5/12/2022	   jlindsay return only list, remove param, clean up
***********************************************************************************************************************/
ALTER PROCEDURE AION.[usp_select_aion_business_division_ref_get_list]
AS
BEGIN
	SELECT BUSINESS_DIVISION_REF_ID,
		BUSINESS_DIVISION_NM,
		BUSINESS_DIVISION_DESC,
		ENUM_MAPPING_VAL_NBR,
		WKR_ID_CREATED_TXT,
		CREATED_DTTM,
		WKR_ID_UPDATED_TXT,
		UPDATED_DTTM
	FROM AION.BUSINESS_DIVISION_REF

	RETURN
END
GO


