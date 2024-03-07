/***********************************************************************************************************************
* Object:       usp_select_aion_time_allocation_type_ref_get_list
* Description:  Retrieves TimeAllocationTypeRef list for given parameter(s).
* Parameters:   
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      12/7/2021
************************************************************************************************************************
* Change History: Date, Name, Description
* 12/7/2021    AION_user     Auto-generated
* 
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_select_aion_time_allocation_type_ref_get_list]
AS
SELECT TIME_ALLOCATION_TYP_REF_ID,
	TIME_ALLOCATION_TYP_REF_DESC,
	ENUM_MAPPING_VAL_NBR,
	ACTIVE_IND,
	WKR_ID_CREATED_TXT,
	CREATED_DTTM,
	WKR_ID_UPDATED_TXT,
	UPDATED_DTTM
FROM [AION].TIME_ALLOCATION_TYPE_REF

RETURN
GO


