/****** Object:  StoredProcedure [AION].[usp_select_aion_user_get_list_bypropertytype]    Script Date: 4/14/2021 8:32:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/***********************************************************************************************************************        
* Object:       usp_select_aion_user_get_list_bypropertytype        
* Description:  Retrieves User list for given parameter(s).        
* Parameters:           
*                    
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
* 12/27/2020    jlindsay     init       
*   01/19/2021  jlindsay    add ordering  
* 03/18/2021    jlindsay    add filtering for just reviewers
* 04/14/2021    jallen      add UserPrincipalName and CalendarId
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_select_aion_user_get_list_bypropertytype] @PROJECT_TYP_REF_ID INT,
	@businessenumcsv VARCHAR(200)
AS
BEGIN
	--get the reviewers enum value, plan reviewers and agency reviewers
	DECLARE @planreviewersystemroleenum INT = 2;
	DECLARE @agencyreviewerparentsystemroleenum INT = 6;
	--
	--get all from system role    
	DECLARE @planreviewerid INT = 0;
	DECLARE @agencyplanreviewerid INT = 0;

	SELECT @planreviewerid = system_role_id
	FROM system_role
	WHERE enum_mapping_val_nbr = @planreviewersystemroleenum;

	--
	SELECT @agencyplanreviewerid = system_role_id
	FROM system_role
	WHERE enum_mapping_val_nbr = @agencyreviewerparentsystemroleenum;

	--
	IF (ISNULL(@PROJECT_TYP_REF_ID, 0) > 0)
	BEGIN
		WITH systemroles
		AS (
			SELECT system_role_id
			FROM system_role
			WHERE system_role_id IN (
					@planreviewerid,
					@agencyplanreviewerid
					)
			
			UNION ALL
			
			SELECT system_role_id
			FROM system_role
			WHERE parent_system_role_id IN (
					@planreviewerid,
					@agencyplanreviewerid
					)
			),
		BusRefIds
		AS (
			SELECT b.[USER_ID]
			FROM STRING_SPLIT(@businessenumcsv, ',') v
			INNER JOIN BUSINESS_REF r ON v.[value] = r.BUSINESS_REF_ID
			INNER JOIN USER_BUSINESS_RELATIONSHIP b ON r.BUSINESS_REF_ID = b.BUSINESS_REF_ID
			INNER JOIN USER_SYSTEM_ROLE_RELATIONSHIP ur ON b.[USER_ID] = ur.[USER_ID]
			INNER JOIN systemroles sr ON ur.SYSTEM_ROLE_ID = sr.SYSTEM_ROLE_ID
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
			u.USER_PRINCIPAL_NAME,
			u.CALENDAR_ID
		FROM [AION].[USER] u
		INNER JOIN USER_PROJECT_TYPE_XREF p ON u.[USER_ID] = p.[USER_ID]
		INNER JOIN BusRefIds b ON u.[USER_ID] = b.[USER_ID]
		WHERE u.ACTIVE_IND = 1
			AND u.[USER_ID] NOT IN (
				- 1,
				1
				)
			AND u.IS_SCHEDULABLE_IND = 1
			AND p.PROJECT_TYP_REF_ID = CASE 
				WHEN ISNULL(@PROJECT_TYP_REF_ID, 0) < 1
					THEN p.PROJECT_TYP_REF_ID
				ELSE @PROJECT_TYP_REF_ID
				END
		ORDER BY U.FIRST_NM,
			U.LAST_NM;
	END;
	ELSE
	BEGIN
		WITH systemroles
		AS (
			SELECT system_role_id
			FROM system_role
			WHERE system_role_id IN (
					@planreviewerid,
					@agencyplanreviewerid
					)
			
			UNION ALL
			
			SELECT system_role_id
			FROM system_role
			WHERE parent_system_role_id IN (
					@planreviewerid,
					@agencyplanreviewerid
					)
			),
		BusRefIds
		AS (
			SELECT b.[USER_ID]
			FROM STRING_SPLIT(@businessenumcsv, ',') v
			INNER JOIN BUSINESS_REF r ON v.[value] = r.BUSINESS_REF_ID
			INNER JOIN USER_BUSINESS_RELATIONSHIP b ON r.BUSINESS_REF_ID = b.BUSINESS_REF_ID
			INNER JOIN USER_SYSTEM_ROLE_RELATIONSHIP ur ON b.[USER_ID] = ur.[USER_ID]
			INNER JOIN systemroles sr ON ur.SYSTEM_ROLE_ID = sr.SYSTEM_ROLE_ID
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
			u.USER_PRINCIPAL_NAME,
			u.CALENDAR_ID
		FROM [AION].[USER] u
		INNER JOIN BusRefIds b ON u.[USER_ID] = b.[USER_ID]
		WHERE u.ACTIVE_IND = 1
			AND u.[USER_ID] NOT IN (
				- 1,
				1
				)
			AND u.IS_SCHEDULABLE_IND = 1
		ORDER BY U.FIRST_NM,
			U.LAST_NM;
	END;

	RETURN;
END;

