/***********************************************************************************************************************  
* Object:       usp_select_aion_user_business_relationship_get_list  
* Description:  Retrieves UserBusinessRelationship list for given parameter(s).  
* Parameters:     
*               @identity                                                   int  
*  
* Returns:      Recordset.  
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.  
*               This proc expects id_person and/or id_file to generate list; modify as necessary.  
*               Include ORDER BY clause as necessary.  
* Version:      1.0  
* Created by:   AION_user  
* Created:      10/10/2019  
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 10/10/2019    AION_user     Auto-generated  
* 06/08/2021	jcl			add process_npa_ind  
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_select_aion_user_business_relationship_get_list] @identity INT
AS
SELECT USER_BUSINESS_RELATIONSHIP_ID,
	USER_ID,
	BUSINESS_REF_ID,
	WKR_ID_CREATED_TXT,
	CREATED_DTTM,
	WKR_ID_UPDATED_TXT,
	UPDATED_DTTM,
	PROCESS_NPA_IND
FROM USER_BUSINESS_RELATIONSHIP
WHERE [USER_ID] = @identity

RETURN
