﻿/***********************************************************************************************************************    
* Object:       usp_select_aion_project_get_list_byprojectstatus   
* Description:  Retrieves Project list for given parameter(s).    
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
* 2020-09-02    jlindsay     init    
* 2021-02-05    smithtc      add project ref list     
* 07/08/2021	jlindsay add new field for Accela record id REC_ID_TXT
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_select_aion_project_get_list_byprojectstatus] @projectStatusRefList VARCHAR(max)
AS
IF (len(@projectStatusRefList) > 0)
BEGIN
	SELECT [PROJECT_ID],
		[PROJECT_NM],
		Project.[EXTERNAL_SYSTEM_REF_ID],
		Project.[WKR_ID_CREATED_TXT],
		Project.[CREATED_DTTM],
		Project.[WKR_ID_UPDATED_TXT],
		Project.[UPDATED_DTTM],
		Project.[PROJECT_STATUS_REF_ID],
		[PROJECT_TYP_REF_ID],
		Project.[SRC_SYSTEM_VAL_TXT],
		QUEUE.PROCESS_STATUS_DESC AS SrcProjectStatus,
		[TAG_CREATED_ID_NUM],
		[TAG_CREATED_BY_TS],
		[TAG_UPDATED_BY_TS],
		[TAG_UPDATED_BY_ID_NUM],
		[ASSIGNED_ESTIMATOR_ID],
		[ASSIGNED_FACILITATOR_ID],
		[PROJECT_MODE_REF_ID],
		[WORKFLOW_STATUS_REF_ID],
		[RTAP_IND],
		[PRELIMINARY_IND],
		[PROJECT_LVL_TXT],
		[GATE_DT],
		[PROJECT_ADDR_TXT],
		[PROJECT_MANAGER_ID],
		[BUILD_CONTR_NM],
		[BUILD_CONTR_ACCT_NUM],
		[GATE_ACCEPTED_IND],
		[FIFO_DUE_DT],
		[PLANS_READY_ON_DT],
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
		NULL AS [ACCEPTANCE_DEADLINE],
		NULL AS [TENTATIVE_STARTDT],
		REC_ID_TXT
	FROM PROJECT
	LEFT OUTER JOIN PROJECT_STATUS_REF AS projectStatus ON projectStatus.PROJECT_STATUS_REF_ID = Project.PROJECT_STATUS_REF_ID
	LEFT OUTER JOIN ACCELA_RECEIVED_RECORD_QUEUE AS QUEUE ON QUEUE.REC_ID_NUM = Project.SRC_SYSTEM_VAL_TXT
		AND QUEUE.PROCESS_STATUS_DESC = projectStatus.PROJECT_STATUS_REF_DESC
	WHERE projectStatus.PROJECT_STATUS_REF_DESC IN (
			SELECT [value] AS PROJECT_STATUS_REF_DESC
			FROM STRING_SPLIT(@projectStatusRefList, ',')
			)
		AND project_id > 5019
END;

BEGIN
	SELECT [PROJECT_ID],
		[PROJECT_NM],
		Project.[EXTERNAL_SYSTEM_REF_ID],
		Project.[WKR_ID_CREATED_TXT],
		Project.[CREATED_DTTM],
		Project.[WKR_ID_UPDATED_TXT],
		Project.[UPDATED_DTTM],
		Project.[PROJECT_STATUS_REF_ID],
		[PROJECT_TYP_REF_ID],
		Project.[SRC_SYSTEM_VAL_TXT],
		QUEUE.PROCESS_STATUS_DESC AS SrcProjectStatus,
		[TAG_CREATED_ID_NUM],
		[TAG_CREATED_BY_TS],
		[TAG_UPDATED_BY_TS],
		[TAG_UPDATED_BY_ID_NUM],
		[ASSIGNED_ESTIMATOR_ID],
		[ASSIGNED_FACILITATOR_ID],
		[PROJECT_MODE_REF_ID],
		[WORKFLOW_STATUS_REF_ID],
		[RTAP_IND],
		[PRELIMINARY_IND],
		[PROJECT_LVL_TXT],
		[GATE_DT],
		[PROJECT_ADDR_TXT],
		[PROJECT_MANAGER_ID],
		[BUILD_CONTR_NM],
		[BUILD_CONTR_ACCT_NUM],
		[GATE_ACCEPTED_IND],
		[FIFO_DUE_DT],
		[PLANS_READY_ON_DT],
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
		NULL AS [ACCEPTANCE_DEADLINE],
		NULL AS [TENTATIVE_STARTDT],
		REC_ID_TXT
	FROM PROJECT
	LEFT OUTER JOIN PROJECT_STATUS_REF AS projectStatus ON projectStatus.PROJECT_STATUS_REF_ID = Project.PROJECT_STATUS_REF_ID
	LEFT OUTER JOIN ACCELA_RECEIVED_RECORD_QUEUE AS QUEUE ON QUEUE.REC_ID_NUM = Project.SRC_SYSTEM_VAL_TXT
		AND QUEUE.PROCESS_STATUS_DESC = projectStatus.PROJECT_STATUS_REF_DESC
	WHERE projectStatus.PROJECT_STATUS_REF_DESC NOT IN (
			'NA',
			'Closed',
			'Cancelled'
			)
		AND project_id > 5019

	RETURN;
END;