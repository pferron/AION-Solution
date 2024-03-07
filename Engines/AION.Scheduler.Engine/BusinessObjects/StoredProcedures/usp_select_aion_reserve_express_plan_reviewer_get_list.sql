/***********************************************************************************************************************          
* Object:       usp_select_aion_reserve_express_plan_reviewer_get_list          
* Description:  list of reserved express plan reviewers.          
* Parameters:             
*               @businessrefcsv                                            varchar(1000)          
*          
* Returns:      list of reserved express plan reviewers       
* Comments:             
* Version:      1.0          
* Created by:             
* Created:                
************************************************************************************************************************          
* Change History: Date, Name, Description 
* 8/6/2020    AION_user     Auto-generated
* 11/5/2021    jlindsay    add param to search by business ref ids        
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_select_aion_reserve_express_plan_reviewer_get_list] @businessrefcsv VARCHAR(1000)
AS
BEGIN
	WITH busrefids
	AS (
		SELECT [value] AS [business_ref_id]
		FROM STRING_SPLIT(@businessrefcsv, ',')
		)
	SELECT repr.RESERVE_EXPRESS_PLAN_REVIEWER_ID,
		repr.BUSINESS_REF_ID,
		repr.PLAN_REVIEWER_ID,
		repr.ROTATION_NBR,
		repr.WKR_ID_CREATED_TXT,
		repr.CREATED_DTTM,
		repr.WKR_ID_UPDATED_TXT,
		repr.UPDATED_DTTM,
		u.FIRST_NM,
		u.LAST_NM
	FROM RESERVE_EXPRESS_PLAN_REVIEWER repr
	INNER JOIN [USER] u ON repr.PLAN_REVIEWER_ID = u.[USER_ID]
	INNER JOIN busrefids b ON repr.business_ref_id = b.business_ref_id
	ORDER BY repr.BUSINESS_REF_ID,
		repr.ROTATION_NBR;

	RETURN
END
