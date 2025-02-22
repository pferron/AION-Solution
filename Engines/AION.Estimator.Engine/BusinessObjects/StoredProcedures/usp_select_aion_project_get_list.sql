﻿/***********************************************************************************************************************                    
* Object:       usp_select_aion_project_get_list                    
* Description:  Retrieves Project list by Project Manager                
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
* 03/24/2021 jlindsay add project fields                 
* 05/03/2021 jlindsay add stub in for acceptance deadline and tentative start dt for dashboard                
* 05/14/2021 jallen add new fields from Accela                    
* 07/08/2021 jlindsay add new field for Accela record id REC_ID_TXT              
* 07/13/2021 jallen add new field for Accela team score TEAM_GRADE_TXT              
* 08/26/2021 jlindsay add all new fields            
* 8/27/2021 jlindsay add tentative start date             
* 08/27/2021 jlindsay add ESTIMATED_FEE_DESC          
* 09/01/2021 jlindsay add RTAP fields          
* 09/30/2021 jlindsay change inner join to left join because some projects aren't scheduled yet        
* 10/05/2021 jlindsay exclude pr and fifo that have been closed or cancelled, show blank if project is suspended for [TENTATIVE_STARTDT]     
* 10/14/2021 jlindsay add express meeting appointment to min dates    
* 11/03/2021 jlindsay removed jba notation  
* 11/08/2021 jallen    changed for getting Express and FIFO from new Plan Review tables  
* 11/19/2021 jlindsay add changes for new sub cycle tables
* 12/16/2021 jallen	  add FIFO_DUE_ACCELA_DT
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_select_aion_project_get_list] @identity INT
AS
BEGIN
	DECLARE @closedapptstatus INT;
	DECLARE @cancelledapptstatus INT;
	DECLARE @SuspendedProjectStatus INT;

	--** get the appt status of closed and cancelled            
	--(@closedapptstatus,@cancelledapptstatus)            
	SELECT @closedapptstatus = APPT_RESPONSE_STATUS_REF_ID
	FROM [AION].[APPOINTMENT_RESPONSE_STATUS_REF]
	WHERE ENUM_MAPPING_VAL_NBR = 8;

	--cancelled sts            
	SELECT @cancelledapptstatus = APPT_RESPONSE_STATUS_REF_ID
	FROM [AION].[APPOINTMENT_RESPONSE_STATUS_REF]
	WHERE ENUM_MAPPING_VAL_NBR = 7;

	--** get the project status of suspended            
	SELECT @SuspendedProjectStatus = PROJECT_STATUS_REF_ID
	FROM [AION].[PROJECT_STATUS_REF]
	WHERE ENUM_MAPPING_VAL_NBR = 9;

	WITH ReviewMinDates
	AS (
		SELECT pc.project_id,
			min(psd.start_dt) AS min_start_dttm
		FROM project_cycle pc
		INNER JOIN plan_review_schedule pr ON pc.project_cycle_id = pr.project_cycle_id
			AND project_schedule_typ_desc IN (
				'PR',
				'FIFO',
				'EMA'
				)
		INNER JOIN PLAN_REVIEW_SCHEDULE_DETAIL psd ON pr.plan_review_schedule_id = psd.plan_review_schedule_id
		WHERE pc.CURRENT_CYCLE_IND = 1
			AND pr.APPT_RESPONSE_STATUS_REF_ID NOT IN (
				@closedapptstatus,
				@cancelledapptstatus
				)
		GROUP BY pc.project_id
		),
	ReviewStartDates
	AS (
		SELECT pc.project_id,
			min(pr.created_dttm) AS min_created_dttm
		FROM project_cycle pc
		INNER JOIN plan_review_schedule pr ON pc.project_cycle_id = pr.project_cycle_id
			AND project_schedule_typ_desc IN (
				'PR',
				'FIFO',
				'EMA'
				)
		WHERE pc.CURRENT_CYCLE_IND = 1
			AND pr.APPT_RESPONSE_STATUS_REF_ID NOT IN (
				@closedapptstatus,
				@cancelledapptstatus
				)
		GROUP BY pc.project_id
		)
	SELECT p.PROJECT_ID,
		PROJECT_NM,
		EXTERNAL_SYSTEM_REF_ID,
		WKR_ID_CREATED_TXT,
		CREATED_DTTM,
		WKR_ID_UPDATED_TXT,
		UPDATED_DTTM,
		PROJECT_STATUS_REF_ID,
		PROJECT_TYP_REF_ID,
		SRC_SYSTEM_VAL_TXT,
		TAG_CREATED_ID_NUM,
		TAG_CREATED_BY_TS,
		TAG_UPDATED_BY_TS,
		TAG_UPDATED_BY_ID_NUM,
		ASSIGNED_ESTIMATOR_ID,
		ASSIGNED_FACILITATOR_ID,
		PROJECT_MODE_REF_ID,
		WORKFLOW_STATUS_REF_ID,
		RTAP_IND,
		PRELIMINARY_IND,
		PROJECT_LVL_TXT,
		GATE_DT,
		PROJECT_ADDR_TXT,
		PROJECT_MANAGER_ID,
		BUILD_CONTR_NM,
		BUILD_CONTR_ACCT_NUM,
		GATE_ACCEPTED_IND,
		FIFO_DUE_DT,
		PLANS_READY_ON_DT,
		CYCLE_NBR,
		PRELIM_MEETING_COMPLETE_IND,
		ACCELA_RTAP_PROJECT_REF_ID,
		ACCELA_PRELIM_PROJECT_REF_ID,
		PROJECT_OCCUPANCY_TYP_MAP_NM,
		CONSTR_TYP_DESC,
		CONSTR_COST_AMT,
		SHEETS_CNT_DESC,
		SQUARE_FOOTAGE_TO_BE_REVIEWED_NBR,
		SQUARE_FOOTAGE_OF_OVERALL_BUILD_NBR,
		STORIES_CNT,
		HIGH_RISE_IND,
		EXPRESS_IND,
		REVIEW_TYP_REF_DESC,
		PRELIM_MEETING_CANCELLED_IND,
		FIFO_IND,
		TOTAL_JOB_COST_AMT,
		WORK_TYP_DESC,
		OCCUPANCY_DESC,
		PRI_OCCUPANCY_DESC,
		SECONDARY_OCCUPANCY_DESC,
		SEAL_HOLDERS_DESC,
		DESIGNER_DESC,
		FIRE_DETAIL_DESC,
		OVERALL_WORK_SCOPE_DESC,
		MECH_WORK_SCOPE_DESC,
		ELCTR_WORK_SCOPE_DESC,
		PLUMB_WORK_SCOPE_DESC,
		CIVIL_WORK_SCOPE_DESC,
		ZONING_OF_SITE_DESC,
		CHG_OF_USE_DESC,
		CONDITIONAL_PERMIT_APPROVAL_DESC,
		PREVIOUS_BUSINESS_TYP_DESC,
		CITY_OF_CHARLOTTE_DESC,
		PROPOSED_BUSINESS_TYP_DESC,
		CODE_SUMMARY_DESC,
		BACKFLOW_APPLICATION_DETAIL_DESC,
		WATER_SEWER_DETAIL_DESC,
		HEALTH_DEPT_DETAIL_DESC,
		DAY_CARE_DESC,
		PROPOSED_OUTDOOR_UNDERGROUND_PIPING_DESC,
		PROPOSED_FIRE_SPRINKLER_PIPING_DESC,
		INSTALL_CMUD_BACKFLOW_PREVENTER_DESC,
		EXTENDING_PUBLIC_WATER_SEWER_DESC,
		GRADE_MOD_WATER_SEWER_EASEMENT_DESC,
		PROPOSED_ENCROACHMENT_WATER_SEWER_EASEMENT_DESC,
		PARCEL_NUM,
		AFFORDABLE_HOUSING_DESC,
		EXACT_ADDR_TXT,
		DELIVERY_MTHD_DESC,
		BIM_DESC,
		BIM_DESIGN_DISCIPLINE_DESC,
		ATTENDEES_CNT_DESC,
		PREVIOUS_PRELIM_REVIEW_DESC,
		PROJECT_NUM_PREVIOUS_PRELIM_REVIEW_DESC,
		SAME_REVIEW_TEAM_DESC,
		PROPERTY_OWNER_NM,
		PROPERTY_OWNER_ADDR_TXT,
		PROPERTY_OWNER_EMAIL_ADDR_TXT,
		PROPERTY_OWNER_PHONE_NUM,
		PROPERTY_OWNER_AUTO_EMAIL_ADDR_TXT,
		PROPERTY_MANAGER_NM,
		PROPERTY_MANAGER_EMAIL_ADDR_TXT,
		PROPERTY_MANAGER_EMAIL_ADDR_2_TXT,
		ARCHITECT_DESIGNER_CNTCT_NM,
		ARCHITECT_DESIGNER_CNTCT_PHONE_NUM,
		ARCHITECT_DESIGNER_CNTCT_EMAIL_ADDR_TXT,
		ARCHITECT_DESIGNER_AUTO_EMAIL_ADDR_TXT,
		ARCHITECT_DRAWINGS_SEALED_DESC,
		ARCHITECT_DESIGNER_LICENSE_NUM,
		ARCHITECT_DESIGNER_LICENSE_BOARD_DESC,
		ARCHITECT_DESIGNER_EMPLOYEE_DESC,
		PERMIT_NUM,
		TOTAL_FEE_AMT,
		BUILD_CODE_VERSION_DESC,
		SQUARE_FOOTAGE_DESC,
		PROPERTY_MANAGER_PHONE_NUM,
		REC_ID_TXT,
		TEAM_GRADE_TXT,
		PRELIM_PROJECT_SUMMARY_OBJ_DETAILS_TXT,
		PRELIM_GEN_INFO_OBJ_DETAILS_TXT,
		PRELIM_MEETING_AGENDA_OBJ_DETAILS_TXT,
		PRELIM_PROPOSED_WORK_OBJ_DETAILS_TXT,
		PRELIM_SYSTEM_INFO_OBJ_DETAILS_TXT,
		PRELIM_BIM_PROJECT_DELIVERY_OBJ_DETAILS_TXT,
		PRELIM_WORK_TYP_OBJ_DETAILS_TXT,
		PRELIM_MEETING_DETAIL_OBJ_DETAILS_TXT,
		CANCELLATION_FEE_AMT,
		PAID_STATUS_IND,
		NULL AS [ACCEPTANCE_DEADLINE],
		CASE 
			WHEN p.project_status_ref_id = @SuspendedProjectStatus
				THEN ''
			ELSE ISNULL(cast(s.min_created_dttm AS VARCHAR), '')
			END AS [PLAN_REVIEW_CREATED_DTTM],
		CASE 
			WHEN p.project_status_ref_id = @SuspendedProjectStatus
				THEN ''
			ELSE ISNULL(cast(r.min_start_dttm AS VARCHAR), '')
			END AS [TENTATIVE_STARTDT],
		ESTIMATED_FEE_DESC,
		RTAP_AFFORDABLE_UNIT_CHG_DESC,
		RTAP_AFFORDABLE_UNITS_REMOVE_DESC,
		RTAP_AFFORDABLE_WORKFORCE_UNITS_ADD_DESC,
		RTAP_WORKFORCE_ADD_DESC,
		RTAP_WORKFORCE_REMOVE_DESC,
		PROFESSIONALS_TXT,
		ACCT_NUM,
		EQUIP_COST_DESC,
		PREPAID_FEE_PAYMENT_TYP_DESC,
		FIFO_DUE_ACCELA_DT
	FROM PROJECT p
	LEFT JOIN ReviewMinDates r ON p.PROJECT_ID = r.PROJECT_ID
	LEFT JOIN ReviewStartDates s ON p.PROJECT_ID = s.PROJECT_ID
	WHERE PROJECT_MANAGER_ID = @identity;

	RETURN
END
