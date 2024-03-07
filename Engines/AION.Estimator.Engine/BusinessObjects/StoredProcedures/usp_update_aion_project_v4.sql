/***********************************************************************************************************************            
* Object:       usp_update_aion_project_v4            
* Description:  Updates Project record using supplied parameters.            
* Parameters:               
*               @PROJECT_ID                                                  int          
*               @PROJECT_NM                                                  varchar(100)          
*               @EXTERNAL_SYSTEM_REF_ID                                      int          
*               @UPDATED_DTTM                                                datetime          
*               @PROJECT_STATUS_REF_ID                                       int          
*               @PROJECT_TYP_REF_ID                                          int          
*               @SRC_SYSTEM_VAL_TXT                                          varchar(255)          
*               @TAG_CREATED_ID_NUM                                          varchar(10)          
*               @TAG_CREATED_BY_TS                                           datetime          
*               @TAG_UPDATED_BY_TS                                           datetime          
*               @TAG_UPDATED_BY_ID_NUM                                       varchar(10)          
*               @ASSIGNED_ESTIMATOR_ID                                       int          
*               @ASSIGNED_FACILITATOR_ID                                     int          
*               @PROJECT_MODE_REF_ID                                         int          
*               @WORKFLOW_STATUS_REF_ID                                      int          
*               @RTAP_IND                                                    bit          
*               @PRELIMINARY_IND                                             bit          
*               @PROJECT_LVL_TXT                                             varchar(10)          
*               @GATE_DT                                                     datetime          
*               @PROJECT_ADDR_TXT                                            varchar(255)          
*               @PROJECT_MANAGER_ID                                          int          
*               @BUILD_CONTR_NM                                              varchar(100)          
*               @BUILD_CONTR_ACCT_NUM                                        varchar(100)          
*               @GATE_ACCEPTED_IND                                           bit          
*               @FIFO_DUE_DT                                                 datetime          
*               @PLANS_READY_ON_DT                                           datetime          
*               @CYCLE_NBR                                                   int          
*               @PRELIM_MEETING_COMPLETE_IND                                 bit          
*               @ACCELA_RTAP_PROJECT_REF_ID                                  varchar(255)          
*               @ACCELA_PRELIM_PROJECT_REF_ID                                varchar(255)          
*               @PROJECT_OCCUPANCY_TYP_MAP_NM                                varchar(255)          
*               @CONSTR_TYP_DESC                                             varchar(255)          
*               @CONSTR_COST_AMT                                             decimal          
*               @SHEETS_CNT_DESC                                             varchar(255)          
*               @SQUARE_FOOTAGE_TO_BE_REVIEWED_NBR                           int          
*               @SQUARE_FOOTAGE_OF_OVERALL_BUILD_NBR                         int          
*               @STORIES_CNT                                                 int          
*               @HIGH_RISE_IND                                               bit          
*               @EXPRESS_IND                                                 bit          
*               @REVIEW_TYP_REF_DESC                           varchar(255)          
*               @PRELIM_MEETING_CANCELLED_IND                                bit          
*               @FIFO_IND                                 bit          
*               @TOTAL_JOB_COST_AMT                           decimal          
*               @WORK_TYP_DESC                     varchar(255)          
*               @OCCUPANCY_DESC                                              varchar(255)          
*               @PRI_OCCUPANCY_DESC                                          varchar(255)          
*               @SECONDARY_OCCUPANCY_DESC                                    varchar(255)          
*               @SEAL_HOLDERS_DESC                                           varchar(255)          
*               @DESIGNER_DESC                                               varchar(255)          
*               @FIRE_DETAIL_DESC                                            varchar(255)          
*               @OVERALL_WORK_SCOPE_DESC                                     varchar(1500)          
*               @MECH_WORK_SCOPE_DESC                                        varchar(255)          
*               @ELCTR_WORK_SCOPE_DESC                                       varchar(255)          
*               @PLUMB_WORK_SCOPE_DESC                                       varchar(255)          
*               @CIVIL_WORK_SCOPE_DESC                                       varchar(255)          
*               @ZONING_OF_SITE_DESC                                         varchar(255)          
*               @CHG_OF_USE_DESC                                             varchar(255)          
*               @CONDITIONAL_PERMIT_APPROVAL_DESC                            varchar(255)          
*               @PREVIOUS_BUSINESS_TYP_DESC                                  varchar(255)          
*               @CITY_OF_CHARLOTTE_DESC                                      varchar(1500)          
*               @PROPOSED_BUSINESS_TYP_DESC                                  varchar(255)          
*               @CODE_SUMMARY_DESC                                           varchar(255)          
*               @BACKFLOW_APPLICATION_DETAIL_DESC                            varchar(1500)          
*               @WATER_SEWER_DETAIL_DESC                                     varchar(1500)          
*               @HEALTH_DEPT_DETAIL_DESC                                     varchar(1500)          
*               @DAY_CARE_DESC                                               varchar(1500)          
*               @PROPOSED_OUTDOOR_UNDERGROUND_PIPING_DESC                    varchar(255)          
*               @PROPOSED_FIRE_SPRINKLER_PIPING_DESC                         varchar(255)          
*               @INSTALL_CMUD_BACKFLOW_PREVENTER_DESC                        varchar(255)          
*               @EXTENDING_PUBLIC_WATER_SEWER_DESC                           varchar(255)          
*               @GRADE_MOD_WATER_SEWER_EASEMENT_DESC                         varchar(255)          
*               @PROPOSED_ENCROACHMENT_WATER_SEWER_EASEMENT_DESC             varchar(255)          
*               @PARCEL_NUM                                                  varchar(10)          
*               @AFFORDABLE_HOUSING_DESC                                     varchar(255)          
*               @EXACT_ADDR_TXT                                              varchar(255)          
*               @DELIVERY_MTHD_DESC                                          varchar(255)          
*               @BIM_DESC                                                    varchar(255)          
*               @BIM_DESIGN_DISCIPLINE_DESC                                  varchar(255)          
*               @ATTENDEES_CNT_DESC                                          varchar(255)          
*               @PREVIOUS_PRELIM_REVIEW_DESC                                 varchar(255)          
*               @PROJECT_NUM_PREVIOUS_PRELIM_REVIEW_DESC       varchar(255)          
*               @SAME_REVIEW_TEAM_DESC                                       varchar(255)          
*               @PROPERTY_OWNER_NM                              varchar(100)          
*               @PROPERTY_OWNER_ADDR_TXT                                     varchar(255)          
*      @PROPERTY_OWNER_EMAIL_ADDR_TXT                               varchar(100)          
*               @PROPERTY_OWNER_PHONE_NUM                                    varchar(20)          
*               @PROPERTY_OWNER_AUTO_EMAIL_ADDR_TXT                          varchar(100)          
*               @PROPERTY_MANAGER_NM                            varchar(100)          
*               @PROPERTY_MANAGER_EMAIL_ADDR_TXT                             varchar(100)          
*               @PROPERTY_MANAGER_EMAIL_ADDR_2_TXT                           varchar(100)          
*               @ARCHITECT_DESIGNER_CNTCT_NM                                 varchar(100)          
*               @ARCHITECT_DESIGNER_CNTCT_PHONE_NUM                          varchar(20)          
*               @ARCHITECT_DESIGNER_CNTCT_EMAIL_ADDR_TXT                     varchar(100)          
*               @ARCHITECT_DESIGNER_AUTO_EMAIL_ADDR_TXT                      varchar(100)          
*               @ARCHITECT_DRAWINGS_SEALED_DESC                              varchar(255)          
*               @ARCHITECT_DESIGNER_LICENSE_NUM                              varchar(255)          
*               @ARCHITECT_DESIGNER_LICENSE_BOARD_DESC                       varchar(255)          
*               @ARCHITECT_DESIGNER_EMPLOYEE_DESC                            varchar(255)          
*               @PERMIT_NUM                                                  varchar(50)          
*               @TOTAL_FEE_AMT                                               decimal          
*               @BUILD_CODE_VERSION_DESC                                     varchar(255)          
*               @SQUARE_FOOTAGE_DESC                                         varchar(255)          
*               @PROPERTY_MANAGER_PHONE_NUM                                  varchar(20)          
*    @REC_ID_TXT             varchar(255)        
*    @TEAM_GRADE_TXT            varchar(30)        
*               @WKR_ID_TXT                                                  varchar(100)*       
* @PRELIM_PROJECT_SUMMARY_OBJ_DETAILS_TXT varchar(1500)    
* @PRELIM_GEN_INFO_OBJ_DETAILS_TXT varchar(1500)    
* @PRELIM_MEETING_AGENDA_OBJ_DETAILS_TXT varchar(1500)    
* @PRELIM_PROPOSED_WORK_OBJ_DETAILS_TXT varchar(1500)    
* @PRELIM_SYSTEM_INFO_OBJ_DETAILS_TXT varchar(1500)    
* @PRELIM_BIM_PROJECT_DELIVERY_OBJ_DETAILS_TXT varchar(1500)    
* @PRELIM_WORK_TYP_OBJ_DETAILS_TXT varchar(1500)    
* @PRELIM_MEETING_DETAIL_OBJ_DETAILS_TXT  varchar(1500)    
*@CANCELLATION_FEE_AMT DECIMAL(14,2) = 0,    
* @PAID_STATUS_IND BIT = 0,    
* @ESTIMATED_FEE_DESC varchar(100)  
  
*  
  @RTAP_AFFORDABLE_UNIT_CHG_DESC varchar(100),  
  @RTAP_AFFORDABLE_UNITS_REMOVE_DESC varchar(100),  
  @RTAP_AFFORDABLE_WORKFORCE_UNITS_ADD_DESC varchar(100),  
  @RTAP_WORKFORCE_ADD_DESC varchar(100),  
  @RTAP_WORKFORCE_REMOVE_DESC varchar(100),  
  @PROFESSIONALS_TXT varchar(8000),  
  @ACCT_NUM varchar(100),  
  @EQUIP_COST_DESC varchar(100),  
  @PREPAID_FEE_PAYMENT_TYP_DESC varchar(100),  
  @FIFO_DUE_ACCELA_DT datetime
  
*            @ReturnValue INT OUTPUT          
*            
* Returns:      Number of rows affected.            
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.            
* Version:      1.0            
* Created by:   AION_user            
* Created:      10/10/2019            
************************************************************************************************************************            
* Change History: Date, Name, Description            
* 10/10/2019    AION_user     Auto-generated            
* 04/26/2021 jlindsay add preliminary_ind            
* 05/14/2021 jallen add new fields from Accela           * 07/08/2021 jlindsay add new field for Accela record id REC_ID_TXT        
* 07/15/2021 jallen add new field for Accela team score TEAM_GRADE_TXT        
* 07/23/2021 jlindsay up varchar to 1500 for work typ desc and fire details desc         
* 08/11/2021 jlindsay add new prelim fields    
* 08/17/2021 jlindsay add cancellation fee    
* 08/18/2021 jlindsay add paid status ind     
* 08/27/2021 jlindsay add ESTIMATED_FEE_DESC  
* 09/01/2021 jlindsay add RTAP fields    
* 10/20/2021 jlindsay increase size of overall_work_scope_desc to 1500
* 12/16/2021 jallen   add FIFO_DUE_ACCELA_DT
* 09/21/2022 jallen   increase size of PERMIT_NUM to 50
***********************************************************************************************************************/
CREATE PROCEDURE [usp_update_aion_project_v4] @PROJECT_ID INT,
	@PROJECT_NM VARCHAR(100),
	@EXTERNAL_SYSTEM_REF_ID INT,
	@UPDATED_DTTM DATETIME,
	@PROJECT_STATUS_REF_ID INT,
	@PROJECT_TYP_REF_ID INT,
	@SRC_SYSTEM_VAL_TXT VARCHAR(255),
	@TAG_CREATED_ID_NUM VARCHAR(10),
	@TAG_CREATED_BY_TS DATETIME,
	@TAG_UPDATED_BY_TS DATETIME,
	@TAG_UPDATED_BY_ID_NUM VARCHAR(10),
	@ASSIGNED_ESTIMATOR_ID INT,
	@ASSIGNED_FACILITATOR_ID INT,
	@PROJECT_MODE_REF_ID INT,
	@WORKFLOW_STATUS_REF_ID INT,
	@RTAP_IND BIT,
	@PRELIMINARY_IND BIT,
	@PROJECT_LVL_TXT VARCHAR(10),
	@GATE_DT DATETIME,
	@PROJECT_ADDR_TXT VARCHAR(255),
	@PROJECT_MANAGER_ID INT,
	@BUILD_CONTR_NM VARCHAR(100),
	@BUILD_CONTR_ACCT_NUM VARCHAR(100),
	@GATE_ACCEPTED_IND BIT,
	@FIFO_DUE_DT DATETIME,
	@PLANS_READY_ON_DT DATETIME,
	@CYCLE_NBR INT,
	@PRELIM_MEETING_COMPLETE_IND BIT,
	@ACCELA_RTAP_PROJECT_REF_ID VARCHAR(255),
	@ACCELA_PRELIM_PROJECT_REF_ID VARCHAR(255),
	@PROJECT_OCCUPANCY_TYP_MAP_NM VARCHAR(255),
	@CONSTR_TYP_DESC VARCHAR(255),
	@CONSTR_COST_AMT DECIMAL,
	@SHEETS_CNT_DESC VARCHAR(255),
	@SQUARE_FOOTAGE_TO_BE_REVIEWED_NBR INT,
	@SQUARE_FOOTAGE_OF_OVERALL_BUILD_NBR INT,
	@STORIES_CNT INT,
	@HIGH_RISE_IND BIT,
	@EXPRESS_IND BIT,
	@REVIEW_TYP_REF_DESC VARCHAR(255),
	@PRELIM_MEETING_CANCELLED_IND BIT,
	@FIFO_IND BIT,
	@TOTAL_JOB_COST_AMT DECIMAL,
	@WORK_TYP_DESC VARCHAR(1500),
	@OCCUPANCY_DESC VARCHAR(255),
	@PRI_OCCUPANCY_DESC VARCHAR(255),
	@SECONDARY_OCCUPANCY_DESC VARCHAR(255),
	@SEAL_HOLDERS_DESC VARCHAR(255),
	@DESIGNER_DESC VARCHAR(255),
	@FIRE_DETAIL_DESC VARCHAR(1500),
	@OVERALL_WORK_SCOPE_DESC VARCHAR(1500),
	@MECH_WORK_SCOPE_DESC VARCHAR(255),
	@ELCTR_WORK_SCOPE_DESC VARCHAR(255),
	@PLUMB_WORK_SCOPE_DESC VARCHAR(255),
	@CIVIL_WORK_SCOPE_DESC VARCHAR(255),
	@ZONING_OF_SITE_DESC VARCHAR(255),
	@CHG_OF_USE_DESC VARCHAR(255),
	@CONDITIONAL_PERMIT_APPROVAL_DESC VARCHAR(255),
	@PREVIOUS_BUSINESS_TYP_DESC VARCHAR(255),
	@CITY_OF_CHARLOTTE_DESC VARCHAR(1500),
	@PROPOSED_BUSINESS_TYP_DESC VARCHAR(255),
	@CODE_SUMMARY_DESC VARCHAR(255),
	@BACKFLOW_APPLICATION_DETAIL_DESC VARCHAR(1500),
	@WATER_SEWER_DETAIL_DESC VARCHAR(1500),
	@HEALTH_DEPT_DETAIL_DESC VARCHAR(1500),
	@DAY_CARE_DESC VARCHAR(1500),
	@PROPOSED_OUTDOOR_UNDERGROUND_PIPING_DESC VARCHAR(255),
	@PROPOSED_FIRE_SPRINKLER_PIPING_DESC VARCHAR(255),
	@INSTALL_CMUD_BACKFLOW_PREVENTER_DESC VARCHAR(255),
	@EXTENDING_PUBLIC_WATER_SEWER_DESC VARCHAR(255),
	@GRADE_MOD_WATER_SEWER_EASEMENT_DESC VARCHAR(255),
	@PROPOSED_ENCROACHMENT_WATER_SEWER_EASEMENT_DESC VARCHAR(255),
	@PARCEL_NUM VARCHAR(10),
	@AFFORDABLE_HOUSING_DESC VARCHAR(255),
	@EXACT_ADDR_TXT VARCHAR(255),
	@DELIVERY_MTHD_DESC VARCHAR(255),
	@BIM_DESC VARCHAR(255),
	@BIM_DESIGN_DISCIPLINE_DESC VARCHAR(255),
	@ATTENDEES_CNT_DESC VARCHAR(255),
	@PREVIOUS_PRELIM_REVIEW_DESC VARCHAR(255),
	@PROJECT_NUM_PREVIOUS_PRELIM_REVIEW_DESC VARCHAR(255),
	@SAME_REVIEW_TEAM_DESC VARCHAR(255),
	@PROPERTY_OWNER_NM VARCHAR(100),
	@PROPERTY_OWNER_ADDR_TXT VARCHAR(255),
	@PROPERTY_OWNER_EMAIL_ADDR_TXT VARCHAR(100),
	@PROPERTY_OWNER_PHONE_NUM VARCHAR(20),
	@PROPERTY_OWNER_AUTO_EMAIL_ADDR_TXT VARCHAR(100),
	@PROPERTY_MANAGER_NM VARCHAR(100),
	@PROPERTY_MANAGER_EMAIL_ADDR_TXT VARCHAR(100),
	@PROPERTY_MANAGER_EMAIL_ADDR_2_TXT VARCHAR(100),
	@ARCHITECT_DESIGNER_CNTCT_NM VARCHAR(100),
	@ARCHITECT_DESIGNER_CNTCT_PHONE_NUM VARCHAR(20),
	@ARCHITECT_DESIGNER_CNTCT_EMAIL_ADDR_TXT VARCHAR(100),
	@ARCHITECT_DESIGNER_AUTO_EMAIL_ADDR_TXT VARCHAR(100),
	@ARCHITECT_DRAWINGS_SEALED_DESC VARCHAR(255),
	@ARCHITECT_DESIGNER_LICENSE_NUM VARCHAR(255),
	@ARCHITECT_DESIGNER_LICENSE_BOARD_DESC VARCHAR(255),
	@ARCHITECT_DESIGNER_EMPLOYEE_DESC VARCHAR(255),
	@PERMIT_NUM VARCHAR(50),
	@TOTAL_FEE_AMT DECIMAL,
	@BUILD_CODE_VERSION_DESC VARCHAR(255),
	@SQUARE_FOOTAGE_DESC VARCHAR(255),
	@PROPERTY_MANAGER_PHONE_NUM VARCHAR(20),
	@WKR_ID_TXT VARCHAR(100),
	@REC_ID_TXT VARCHAR(255),
	@TEAM_GRADE_TXT VARCHAR(30),
	@PRELIM_PROJECT_SUMMARY_OBJ_DETAILS_TXT VARCHAR(1500) = '',
	@PRELIM_GEN_INFO_OBJ_DETAILS_TXT VARCHAR(1500) = '',
	@PRELIM_MEETING_AGENDA_OBJ_DETAILS_TXT VARCHAR(1500) = '',
	@PRELIM_PROPOSED_WORK_OBJ_DETAILS_TXT VARCHAR(1500) = '',
	@PRELIM_SYSTEM_INFO_OBJ_DETAILS_TXT VARCHAR(1500) = '',
	@PRELIM_BIM_PROJECT_DELIVERY_OBJ_DETAILS_TXT VARCHAR(1500) = '',
	@PRELIM_WORK_TYP_OBJ_DETAILS_TXT VARCHAR(1500) = '',
	@PRELIM_MEETING_DETAIL_OBJ_DETAILS_TXT VARCHAR(1500) = '',
	@CANCELLATION_FEE_AMT DECIMAL(14, 2) = 0,
	@PAID_STATUS_IND BIT = 0,
	@ESTIMATED_FEE_DESC VARCHAR(100) = '',
	@RTAP_AFFORDABLE_UNIT_CHG_DESC VARCHAR(100) = '',
	@RTAP_AFFORDABLE_UNITS_REMOVE_DESC VARCHAR(100) = '',
	@RTAP_AFFORDABLE_WORKFORCE_UNITS_ADD_DESC VARCHAR(100) = '',
	@RTAP_WORKFORCE_ADD_DESC VARCHAR(100) = '',
	@RTAP_WORKFORCE_REMOVE_DESC VARCHAR(100) = '',
	@PROFESSIONALS_TXT VARCHAR(8000) = '',
	@ACCT_NUM VARCHAR(100) = '',
	@EQUIP_COST_DESC VARCHAR(100) = '',
	@PREPAID_FEE_PAYMENT_TYP_DESC VARCHAR(100) = '',
	@FIFO_DUE_ACCELA_DT DATETIME,
	@ReturnValue INT OUTPUT
AS
BEGIN
	DECLARE @error INT
	DECLARE @RecIdTxt VARCHAR(255);

	IF (isnull(@REC_ID_TXT, '') = '')
	BEGIN
		SET @RecIdTxt = @SRC_SYSTEM_VAL_TXT;
	END
	ELSE
	BEGIN
		SET @RecIdTxt = @REC_ID_TXT;
	END

	UPDATE PROJECT
	SET PROJECT_NM = @PROJECT_NM,
		EXTERNAL_SYSTEM_REF_ID = @EXTERNAL_SYSTEM_REF_ID,
		WKR_ID_UPDATED_TXT = @WKR_ID_TXT,
		UPDATED_DTTM = GETDATE(),
		PROJECT_STATUS_REF_ID = @PROJECT_STATUS_REF_ID,
		PROJECT_TYP_REF_ID = @PROJECT_TYP_REF_ID,
		SRC_SYSTEM_VAL_TXT = @SRC_SYSTEM_VAL_TXT,
		TAG_CREATED_ID_NUM = @TAG_CREATED_ID_NUM,
		TAG_CREATED_BY_TS = @TAG_CREATED_BY_TS,
		TAG_UPDATED_BY_TS = @TAG_UPDATED_BY_TS,
		TAG_UPDATED_BY_ID_NUM = @TAG_UPDATED_BY_ID_NUM,
		ASSIGNED_ESTIMATOR_ID = @ASSIGNED_ESTIMATOR_ID,
		ASSIGNED_FACILITATOR_ID = @ASSIGNED_FACILITATOR_ID,
		PROJECT_MODE_REF_ID = @PROJECT_MODE_REF_ID,
		WORKFLOW_STATUS_REF_ID = @WORKFLOW_STATUS_REF_ID,
		RTAP_IND = @RTAP_IND,
		PRELIMINARY_IND = @PRELIMINARY_IND,
		PROJECT_LVL_TXT = @PROJECT_LVL_TXT,
		GATE_DT = @GATE_DT,
		PROJECT_ADDR_TXT = @PROJECT_ADDR_TXT,
		PROJECT_MANAGER_ID = @PROJECT_MANAGER_ID,
		BUILD_CONTR_NM = @BUILD_CONTR_NM,
		BUILD_CONTR_ACCT_NUM = @BUILD_CONTR_ACCT_NUM,
		GATE_ACCEPTED_IND = @GATE_ACCEPTED_IND,
		FIFO_DUE_DT = @FIFO_DUE_DT,
		PLANS_READY_ON_DT = @PLANS_READY_ON_DT,
		CYCLE_NBR = @CYCLE_NBR,
		PRELIM_MEETING_COMPLETE_IND = @PRELIM_MEETING_COMPLETE_IND,
		ACCELA_RTAP_PROJECT_REF_ID = @ACCELA_RTAP_PROJECT_REF_ID,
		ACCELA_PRELIM_PROJECT_REF_ID = @ACCELA_PRELIM_PROJECT_REF_ID,
		PROJECT_OCCUPANCY_TYP_MAP_NM = @PROJECT_OCCUPANCY_TYP_MAP_NM,
		CONSTR_TYP_DESC = @CONSTR_TYP_DESC,
		CONSTR_COST_AMT = @CONSTR_COST_AMT,
		SHEETS_CNT_DESC = @SHEETS_CNT_DESC,
		SQUARE_FOOTAGE_TO_BE_REVIEWED_NBR = @SQUARE_FOOTAGE_TO_BE_REVIEWED_NBR,
		SQUARE_FOOTAGE_OF_OVERALL_BUILD_NBR = @SQUARE_FOOTAGE_OF_OVERALL_BUILD_NBR,
		STORIES_CNT = @STORIES_CNT,
		HIGH_RISE_IND = @HIGH_RISE_IND,
		EXPRESS_IND = @EXPRESS_IND,
		REVIEW_TYP_REF_DESC = @REVIEW_TYP_REF_DESC,
		PRELIM_MEETING_CANCELLED_IND = @PRELIM_MEETING_CANCELLED_IND,
		FIFO_IND = @FIFO_IND,
		TOTAL_JOB_COST_AMT = @TOTAL_JOB_COST_AMT,
		WORK_TYP_DESC = @WORK_TYP_DESC,
		OCCUPANCY_DESC = @OCCUPANCY_DESC,
		PRI_OCCUPANCY_DESC = @PRI_OCCUPANCY_DESC,
		SECONDARY_OCCUPANCY_DESC = @SECONDARY_OCCUPANCY_DESC,
		SEAL_HOLDERS_DESC = @SEAL_HOLDERS_DESC,
		DESIGNER_DESC = @DESIGNER_DESC,
		FIRE_DETAIL_DESC = @FIRE_DETAIL_DESC,
		OVERALL_WORK_SCOPE_DESC = @OVERALL_WORK_SCOPE_DESC,
		MECH_WORK_SCOPE_DESC = @MECH_WORK_SCOPE_DESC,
		ELCTR_WORK_SCOPE_DESC = @ELCTR_WORK_SCOPE_DESC,
		PLUMB_WORK_SCOPE_DESC = @PLUMB_WORK_SCOPE_DESC,
		CIVIL_WORK_SCOPE_DESC = @CIVIL_WORK_SCOPE_DESC,
		ZONING_OF_SITE_DESC = @ZONING_OF_SITE_DESC,
		CHG_OF_USE_DESC = @CHG_OF_USE_DESC,
		CONDITIONAL_PERMIT_APPROVAL_DESC = @CONDITIONAL_PERMIT_APPROVAL_DESC,
		PREVIOUS_BUSINESS_TYP_DESC = @PREVIOUS_BUSINESS_TYP_DESC,
		CITY_OF_CHARLOTTE_DESC = @CITY_OF_CHARLOTTE_DESC,
		PROPOSED_BUSINESS_TYP_DESC = @PROPOSED_BUSINESS_TYP_DESC,
		CODE_SUMMARY_DESC = @CODE_SUMMARY_DESC,
		BACKFLOW_APPLICATION_DETAIL_DESC = @BACKFLOW_APPLICATION_DETAIL_DESC,
		WATER_SEWER_DETAIL_DESC = @WATER_SEWER_DETAIL_DESC,
		HEALTH_DEPT_DETAIL_DESC = @HEALTH_DEPT_DETAIL_DESC,
		DAY_CARE_DESC = @DAY_CARE_DESC,
		PROPOSED_OUTDOOR_UNDERGROUND_PIPING_DESC = @PROPOSED_OUTDOOR_UNDERGROUND_PIPING_DESC,
		PROPOSED_FIRE_SPRINKLER_PIPING_DESC = @PROPOSED_FIRE_SPRINKLER_PIPING_DESC,
		INSTALL_CMUD_BACKFLOW_PREVENTER_DESC = @INSTALL_CMUD_BACKFLOW_PREVENTER_DESC,
		EXTENDING_PUBLIC_WATER_SEWER_DESC = @EXTENDING_PUBLIC_WATER_SEWER_DESC,
		GRADE_MOD_WATER_SEWER_EASEMENT_DESC = @GRADE_MOD_WATER_SEWER_EASEMENT_DESC,
		PROPOSED_ENCROACHMENT_WATER_SEWER_EASEMENT_DESC = @PROPOSED_ENCROACHMENT_WATER_SEWER_EASEMENT_DESC,
		PARCEL_NUM = @PARCEL_NUM,
		AFFORDABLE_HOUSING_DESC = @AFFORDABLE_HOUSING_DESC,
		EXACT_ADDR_TXT = @EXACT_ADDR_TXT,
		DELIVERY_MTHD_DESC = @DELIVERY_MTHD_DESC,
		BIM_DESC = @BIM_DESC,
		BIM_DESIGN_DISCIPLINE_DESC = @BIM_DESIGN_DISCIPLINE_DESC,
		ATTENDEES_CNT_DESC = @ATTENDEES_CNT_DESC,
		PREVIOUS_PRELIM_REVIEW_DESC = @PREVIOUS_PRELIM_REVIEW_DESC,
		PROJECT_NUM_PREVIOUS_PRELIM_REVIEW_DESC = @PROJECT_NUM_PREVIOUS_PRELIM_REVIEW_DESC,
		SAME_REVIEW_TEAM_DESC = @SAME_REVIEW_TEAM_DESC,
		PROPERTY_OWNER_NM = @PROPERTY_OWNER_NM,
		PROPERTY_OWNER_ADDR_TXT = @PROPERTY_OWNER_ADDR_TXT,
		PROPERTY_OWNER_EMAIL_ADDR_TXT = @PROPERTY_OWNER_EMAIL_ADDR_TXT,
		PROPERTY_OWNER_PHONE_NUM = @PROPERTY_OWNER_PHONE_NUM,
		PROPERTY_OWNER_AUTO_EMAIL_ADDR_TXT = @PROPERTY_OWNER_AUTO_EMAIL_ADDR_TXT,
		PROPERTY_MANAGER_NM = @PROPERTY_MANAGER_NM,
		PROPERTY_MANAGER_EMAIL_ADDR_TXT = @PROPERTY_MANAGER_EMAIL_ADDR_TXT,
		PROPERTY_MANAGER_EMAIL_ADDR_2_TXT = @PROPERTY_MANAGER_EMAIL_ADDR_2_TXT,
		ARCHITECT_DESIGNER_CNTCT_NM = @ARCHITECT_DESIGNER_CNTCT_NM,
		ARCHITECT_DESIGNER_CNTCT_PHONE_NUM = @ARCHITECT_DESIGNER_CNTCT_PHONE_NUM,
		ARCHITECT_DESIGNER_CNTCT_EMAIL_ADDR_TXT = @ARCHITECT_DESIGNER_CNTCT_EMAIL_ADDR_TXT,
		ARCHITECT_DESIGNER_AUTO_EMAIL_ADDR_TXT = @ARCHITECT_DESIGNER_AUTO_EMAIL_ADDR_TXT,
		ARCHITECT_DRAWINGS_SEALED_DESC = @ARCHITECT_DRAWINGS_SEALED_DESC,
		ARCHITECT_DESIGNER_LICENSE_NUM = @ARCHITECT_DESIGNER_LICENSE_NUM,
		ARCHITECT_DESIGNER_LICENSE_BOARD_DESC = @ARCHITECT_DESIGNER_LICENSE_BOARD_DESC,
		ARCHITECT_DESIGNER_EMPLOYEE_DESC = @ARCHITECT_DESIGNER_EMPLOYEE_DESC,
		PERMIT_NUM = @PERMIT_NUM,
		TOTAL_FEE_AMT = @TOTAL_FEE_AMT,
		BUILD_CODE_VERSION_DESC = @BUILD_CODE_VERSION_DESC,
		SQUARE_FOOTAGE_DESC = @SQUARE_FOOTAGE_DESC,
		PROPERTY_MANAGER_PHONE_NUM = @PROPERTY_MANAGER_PHONE_NUM,
		REC_ID_TXT = @RecIdTxt,
		TEAM_GRADE_TXT = @TEAM_GRADE_TXT,
		PRELIM_PROJECT_SUMMARY_OBJ_DETAILS_TXT = @PRELIM_PROJECT_SUMMARY_OBJ_DETAILS_TXT,
		PRELIM_GEN_INFO_OBJ_DETAILS_TXT = @PRELIM_GEN_INFO_OBJ_DETAILS_TXT,
		PRELIM_MEETING_AGENDA_OBJ_DETAILS_TXT = @PRELIM_MEETING_AGENDA_OBJ_DETAILS_TXT,
		PRELIM_PROPOSED_WORK_OBJ_DETAILS_TXT = @PRELIM_PROPOSED_WORK_OBJ_DETAILS_TXT,
		PRELIM_SYSTEM_INFO_OBJ_DETAILS_TXT = @PRELIM_SYSTEM_INFO_OBJ_DETAILS_TXT,
		PRELIM_BIM_PROJECT_DELIVERY_OBJ_DETAILS_TXT = @PRELIM_BIM_PROJECT_DELIVERY_OBJ_DETAILS_TXT,
		PRELIM_WORK_TYP_OBJ_DETAILS_TXT = @PRELIM_WORK_TYP_OBJ_DETAILS_TXT,
		PRELIM_MEETING_DETAIL_OBJ_DETAILS_TXT = @PRELIM_MEETING_DETAIL_OBJ_DETAILS_TXT,
		CANCELLATION_FEE_AMT = @CANCELLATION_FEE_AMT,
		PAID_STATUS_IND = @PAID_STATUS_IND,
		ESTIMATED_FEE_DESC = @ESTIMATED_FEE_DESC,
		RTAP_AFFORDABLE_UNIT_CHG_DESC = @RTAP_AFFORDABLE_UNIT_CHG_DESC,
		RTAP_AFFORDABLE_UNITS_REMOVE_DESC = @RTAP_AFFORDABLE_UNITS_REMOVE_DESC,
		RTAP_AFFORDABLE_WORKFORCE_UNITS_ADD_DESC = @RTAP_AFFORDABLE_WORKFORCE_UNITS_ADD_DESC,
		RTAP_WORKFORCE_ADD_DESC = @RTAP_WORKFORCE_ADD_DESC,
		RTAP_WORKFORCE_REMOVE_DESC = @RTAP_WORKFORCE_REMOVE_DESC,
		PROFESSIONALS_TXT = @PROFESSIONALS_TXT,
		ACCT_NUM = @ACCT_NUM,
		EQUIP_COST_DESC = @EQUIP_COST_DESC,
		PREPAID_FEE_PAYMENT_TYP_DESC = @PREPAID_FEE_PAYMENT_TYP_DESC,
		FIFO_DUE_ACCELA_DT = @FIFO_DUE_ACCELA_DT
	WHERE PROJECT_ID = @PROJECT_ID
		AND ISNULL(CONVERT(VARCHAR(19), UPDATED_DTTM, 120), '') = ISNULL(CONVERT(VARCHAR(19), @UPDATED_DTTM, 120), '')

	SELECT @error = @@ERROR,
		@ReturnValue = @@ROWCOUNT

	IF @error != 0
		RAISERROR (
				'Error updating Project record.',
				18,
				1
				)

	IF @ReturnValue = 0
		RAISERROR (
				'Data was changed/deleted prior to update.',
				18,
				100
				)

	RETURN
END
