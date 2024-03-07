/***********************************************************************************************************************  
* Object:       usp_select_aion_npa_type_ref_get_by_id  
* Description:  Retrieves NpaTypeRef record for given key field(s).  
* Parameters:     
*               @identity                                                    int  
*  
* Returns:      Recordset.  
* Comments:     Developer may need to manually join to other tables, such as code tables,  
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.  
* Version:      1.0  
* Created by:   AION_user  
* Created:      3/19/2020  
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 3/19/2020    AION_user     Auto-generated  
*  12/7/2021 jlindsay add time allocation type ref id 
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_select_aion_npa_type_ref_get_by_id] @identity INT
AS
SELECT NON_PROJECT_APPT_TYP_REF_ID,
	APPT_TYP_DESC,
	ACTIVE_IND,
	WKR_ID_CREATED_TXT,
	CREATED_DTTM,
	WKR_ID_UPDATED_TXT,
	UPDATED_DTTM,
	TIME_ALLOCATION_TYP_REF_ID
FROM NON_PROJECT_APPOINTMENT_TYPE_REF
WHERE
	-- @TODO:  Correct the following as necessary  
	NON_PROJECT_APPT_TYP_REF_ID = @identity

RETURN
