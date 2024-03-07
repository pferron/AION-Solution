/***********************************************************************************************************************  
* Object:       usp_select_aion_user_business_relationship_processnpas  
* Description:  Retrieves UserBusinessRelationship list for given parameter(s).  
* Parameters:     
*  
* Returns:      Recordset.  
* Comments:     
* Version:      1.0  
* Created by:   jcl  
* Created:      06/08/2021  
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 06/08/2021	jcl			process_npa_ind  
***********************************************************************************************************************/
CREATE PROCEDURE [AION].[usp_select_aion_user_business_relationship_processnpas] 
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
WHERE PROCESS_NPA_IND = 1

RETURN
