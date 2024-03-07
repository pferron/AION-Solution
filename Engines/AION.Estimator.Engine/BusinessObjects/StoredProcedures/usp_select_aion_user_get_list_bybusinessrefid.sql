/***********************************************************************************************************************          
* Object:       usp_select_aion_user_get_list_bybusinessrefid          
* Description:  Retrieves User list for given parameter(s).          
* Parameters:   @businessenumcsv VARCHAR(200)          
*                      
*          
* Returns:      Recordset.          

* Version:      1.0          
* Created by:   jlindsay          
* Created:       01/10/2022         
************************************************************************************************************************          
* Change History: Date, Name, Description          
* 01/10/2022	jlindsay	init
***********************************************************************************************************************/
CREATE PROCEDURE [AION].[usp_select_aion_user_get_list_bybusinessrefid] @businessenumcsv VARCHAR(200)
AS
BEGIN
	--  
	WITH BusRefIds
	AS (
		SELECT b.[USER_ID]
		FROM STRING_SPLIT(@businessenumcsv, ',') v
		INNER JOIN BUSINESS_REF r ON v.[value] = r.BUSINESS_REF_ID
		INNER JOIN USER_BUSINESS_RELATIONSHIP b ON r.BUSINESS_REF_ID = b.BUSINESS_REF_ID
		GROUP BY b.[USER_ID]
		)
	SELECT DISTINCT u.[USER_ID],
		u.FIRST_NM,
		u.LAST_NM,
		u.EXTERNAL_SYSTEM_REF_ID,
		u.SRC_SYSTEM_VAL_TXT,
		u.WKR_ID_CREATED_TXT,
		u.CREATED_DTTM,
		u.WKR_ID_UPDATED_TXT,
		u.UPDATED_DTTM,
		u.ACTIVE_IND,
		u.USER_INTERFACE_SETTING_TXT,
		u.IS_EXPRESS_SCHEDULED_IND,
		u.USER_NM,
		u.LAN_ID_TXT,
		u.PHONE_NUM,
		u.EMAIL_ADDR_TXT,
		u.NOTES_TXT,
		u.IS_SCHEDULABLE_IND,
		u.PLAN_REVIEW_OVERRIDE_HOURS_NBR,
		u.HOURS_ESTIMATED_DESC,
		u.JURISDICTION_TYP_ID,
		u.SCHEDULABLE_LVL_DESC,
		u.IS_PRELIM_MEETING_ALLOWED_IND,
		u.USER_PRINCIPAL_NM,
		u.CALENDAR_ID,
		u.CITY_IND
	FROM [AION].[USER] u
	INNER JOIN BusRefIds b ON u.[USER_ID] = b.[USER_ID]
	WHERE u.[USER_ID] > 1
	ORDER BY U.FIRST_NM,
		U.LAST_NM;

	RETURN;
END;
