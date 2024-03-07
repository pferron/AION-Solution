/***********************************************************************************************************************              
* Object:       usp_select_aion_npa_AllIndByUserIds             
* Description:  Gets a list of NPAs that are listed as AllBuilding, AllReviewers, etc that the input users  
                do not have schedules  
* Parameters:                 
*               @startdate    DATETIME,               
*               @enddate      DATETIME,               
*               @reviewerscsv VARCHAR(8000),  
*               @all_build_ind BIT= 0,   
*               @all_elctr_ind BIT= 0,   
*               @all_mech_ind BIT= 0,   
*               @all_plumb_ind BIT= 0,   
*               @all_zoning_ind BIT= 0,   
*               @all_fire_ind BIT= 0,   
*               @all_backflow_ind BIT= 0,   
*               @all_ehs_food_ind BIT= 0,   
*               @all_ehs_pool_ind BIT= 0,   
*               @all_ehs_lodge_ind BIT= 0,   
*               @all_ehs_daycare_ind BIT= 0  
*              
* Returns:      list              
* Comments:                   
* Version:      1.0              
* Created by:   jlindsay              
* Created:      03/11/2021              
************************************************************************************************************************              
* Change History: Date, Name, Description              
* 03/11/2021    jlindsay     initial 
* 07-21-2022	jlindsay	refactor for users existing appts
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_select_aion_npa_AllIndByUserIds] @startdate DATETIME,
	@enddate DATETIME,
	@reviewerscsv VARCHAR(8000),
	@all_build_ind BIT = 0,
	@all_elctr_ind BIT = 0,
	@all_mech_ind BIT = 0,
	@all_plumb_ind BIT = 0,
	@all_zoning_ind BIT = 0,
	@all_fire_ind BIT = 0,
	@all_backflow_ind BIT = 0,
	@all_ehs_food_ind BIT = 0,
	@all_ehs_pool_ind BIT = 0,
	@all_ehs_lodge_ind BIT = 0,
	@all_ehs_daycare_ind BIT = 0
AS
BEGIN
	--  
	WITH CSVUSERS
	AS (
		SELECT [value] AS [user_id]
		FROM STRING_SPLIT(@reviewerscsv, ',')
		WHERE ISNULL([value], '') != ''
		),
	NPAS
	AS (
		SELECT *
		FROM [AION].non_project_appointment
		WHERE (
				all_plan_reviewers_ind = 1
				OR all_build_ind = 1
				OR all_elctr_ind = 1
				OR all_mech_ind = 1
				OR all_plumb_ind = 1
				OR all_zoning_ind = 1
				OR all_fire_ind = 1
				OR all_backflow_ind = 1
				OR all_ehs_food_ind = 1
				OR all_ehs_pool_ind = 1
				OR all_ehs_lodge_ind = 1
				OR all_ehs_daycare_ind = 1
				)
			AND CAST(APPT_FROM_DTTM AS DATE) <= CAST(@enddate AS DATE)
			AND CAST(APPT_TO_DTTM AS DATE) >= CAST(@startdate AS DATE)
		),
	NPAExistingSchedulesWTrade
	AS (
		SELECT DISTINCT U.[USER_ID],
			NPA.[NON_PROJECT_APPT_ID] AS APPT_ID,
			NPA.APPT_NM,
			US.BUSINESS_REF_ID
		FROM CSVUSERS U
		INNER JOIN [AION].[USER_SCHEDULE] US ON U.[USER_Id] = US.[USER_ID]
		INNER JOIN [AION].[PROJECT_SCHEDULE] PS ON US.[PROJECT_SCHEDULE_ID] = PS.[PROJECT_SCHEDULE_ID]
		INNER JOIN NPAS NPA ON PS.[APPT_ID] = NPA.[NON_PROJECT_APPT_ID]
			AND PS.[PROJECT_SCHEDULE_TYP_DESC] IN ('NPA')
		),
	BNpas -- get a list of all users with all appts for building dept
	AS (
		SELECT CSVUSERS.*,
			business_ref_id = (
				SELECT business_ref_id
				FROM AION.Business_REf
				WHERE enum_mapping_val_nbr = 1
				),
			NPAS.*
		FROM CSVUSERS
		CROSS JOIN NPAS
		WHERE all_build_ind = 1
			AND @all_build_ind = 1
		),
	ENpas
	AS (
		SELECT CSVUSERS.*,
			business_ref_id = (
				SELECT business_ref_id
				FROM AION.Business_REf
				WHERE enum_mapping_val_nbr = 2
				),
			NPAS.*
		FROM CSVUSERS
		CROSS JOIN NPAS
		WHERE all_elctr_ind = 1
			AND @all_elctr_ind = 1
		),
	MNpas
	AS (
		SELECT CSVUSERS.*,
			business_ref_id = (
				SELECT business_ref_id
				FROM AION.Business_REf
				WHERE enum_mapping_val_nbr = 3
				),
			NPAS.*
		FROM CSVUSERS
		CROSS JOIN NPAS
		WHERE all_mech_ind = 1
			AND @all_mech_ind = 1
		),
	PNpas
	AS (
		SELECT CSVUSERS.*,
			business_ref_id = (
				SELECT business_ref_id
				FROM AION.Business_REf
				WHERE enum_mapping_val_nbr = 4
				),
			NPAS.*
		FROM CSVUSERS
		CROSS JOIN NPAS
		WHERE all_plumb_ind = 1
			AND @all_plumb_ind = 1
		),
	FNpas -- fire-davidson, 13 
	AS (
		SELECT CSVUSERS.*,
			business_ref_id = (
				SELECT business_ref_id
				FROM AION.Business_REf
				WHERE enum_mapping_val_nbr = 13
				),
			NPAS.*
		FROM CSVUSERS
		CROSS JOIN NPAS
		WHERE all_fire_ind = 1
			AND @all_fire_ind = 1
		),
	ZNpas --zone davidson 5 
	AS (
		SELECT CSVUSERS.*,
			business_ref_id = (
				SELECT business_ref_id
				FROM AION.Business_REf
				WHERE enum_mapping_val_nbr = 5
				),
			NPAS.*
		FROM CSVUSERS
		CROSS JOIN NPAS
		WHERE all_zoning_ind = 1
			AND @all_zoning_ind = 1
		),
	BFNpas
	AS (
		SELECT CSVUSERS.*,
			business_ref_id = (
				SELECT business_ref_id
				FROM AION.Business_REf
				WHERE enum_mapping_val_nbr = 25
				),
			NPAS.*
		FROM CSVUSERS
		CROSS JOIN NPAS
		WHERE all_backflow_ind = 1
			AND @all_backflow_ind = 1
		),
	FDNpas
	AS (
		SELECT CSVUSERS.*,
			business_ref_id = (
				SELECT business_ref_id
				FROM AION.Business_REf
				WHERE enum_mapping_val_nbr = 22
				),
			NPAS.*
		FROM CSVUSERS
		CROSS JOIN NPAS
		WHERE all_ehs_food_ind = 1
			AND @all_ehs_food_ind = 1
		),
	PLNpas
	AS (
		SELECT CSVUSERS.*,
			business_ref_id = (
				SELECT business_ref_id
				FROM AION.Business_REf
				WHERE enum_mapping_val_nbr = 23
				),
			NPAS.*
		FROM CSVUSERS
		CROSS JOIN NPAS
		WHERE all_ehs_pool_ind = 1
			AND @all_ehs_pool_ind = 1
		),
	LNpas
	AS (
		SELECT CSVUSERS.*,
			business_ref_id = (
				SELECT business_ref_id
				FROM AION.Business_REf
				WHERE enum_mapping_val_nbr = 24
				),
			NPAS.*
		FROM CSVUSERS
		CROSS JOIN NPAS
		WHERE all_ehs_lodge_ind = 1
			AND @all_ehs_lodge_ind = 1
		),
	DCNpas
	AS (
		SELECT CSVUSERS.*,
			business_ref_id = (
				SELECT business_ref_id
				FROM AION.Business_REf
				WHERE enum_mapping_val_nbr = 21
				),
			NPAS.*
		FROM CSVUSERS
		CROSS JOIN NPAS
		WHERE all_ehs_daycare_ind = 1
			AND @all_ehs_daycare_ind = 1
		),
	AllRows
	AS (
		SELECT [USER_ID],
			NON_PROJECT_APPT_ID,
			business_ref_id
		FROM BNpas
		
		UNION
		
		SELECT [USER_ID],
			NON_PROJECT_APPT_ID,
			business_ref_id
		FROM ENpas
		
		UNION
		
		SELECT [USER_ID],
			NON_PROJECT_APPT_ID,
			business_ref_id
		FROM MNpas
		
		UNION
		
		SELECT [USER_ID],
			NON_PROJECT_APPT_ID,
			business_ref_id
		FROM PNpas
		--FIRE  
		
		UNION
		
		SELECT [USER_ID],
			NON_PROJECT_APPT_ID,
			business_ref_id
		FROM FNpas
		--ZONING  
		
		UNION
		
		SELECT [USER_ID],
			NON_PROJECT_APPT_ID,
			business_ref_id
		FROM ZNpas
		--BACKFLOW  
		
		UNION
		
		SELECT [USER_ID],
			NON_PROJECT_APPT_ID,
			business_ref_id
		FROM BFNpas
		--EHS  
		
		UNION
		
		SELECT [USER_ID],
			NON_PROJECT_APPT_ID,
			business_ref_id
		FROM FDNpas
		
		UNION
		
		SELECT [USER_ID],
			NON_PROJECT_APPT_ID,
			business_ref_id
		FROM PLNpas
		
		UNION
		
		SELECT [USER_ID],
			NON_PROJECT_APPT_ID,
			business_ref_id
		FROM LNpas
		
		UNION
		
		SELECT [USER_ID],
			NON_PROJECT_APPT_ID,
			business_ref_id
		FROM DCNpas
		),
	GetAllUserBusinessRefAppts
	AS (
		SELECT b.*
		FROM AllRows b
		LEFT JOIN NPAExistingSchedulesWTrade e ON b.[user_id] = e.[user_id]
			AND b.business_ref_id = e.business_ref_id
			AND b.non_project_appt_id = e.APPT_ID
		WHERE e.APPT_ID IS NULL
		)
	SELECT [USER_ID],
		NON_PROJECT_APPT_ID,
		business_ref_id
	FROM GetAllUserBusinessRefAppts;
END;
