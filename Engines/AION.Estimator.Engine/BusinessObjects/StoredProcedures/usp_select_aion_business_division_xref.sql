/***********************************************************************************************************************
* Object:       usp_select_aion_business_division_xref
* Description:  Retrieves BusinessDivisionRef record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   AION_user
* Created:      3/21/2022
************************************************************************************************************************
* Change History: Date, Name, Description
* 5/12/2022 jlindsay initial
* 
***********************************************************************************************************************/
ALTER PROCEDURE AION.[usp_select_aion_business_division_xref] 
AS
BEGIN
	SELECT bdr.BUSINESS_DIVISION_REF_ID,
		bdr.BUSINESS_DIVISION_NM,
		bdr.BUSINESS_DIVISION_DESC,
		bdr.ENUM_MAPPING_VAL_NBR,
		bdr.WKR_ID_CREATED_TXT,
		bdr.CREATED_DTTM,
		bdr.WKR_ID_UPDATED_TXT,
		bdr.UPDATED_DTTM,
		br.BUSINESS_REF_ID
	FROM AION.BUSINESS_DIVISION_REF bdr
	INNER JOIN AION.BUSINESS_REF br ON bdr.BUSINESS_DIVISION_REF_ID = br.BUSINESS_DIVISION_REF_ID;

	RETURN
END
GO


