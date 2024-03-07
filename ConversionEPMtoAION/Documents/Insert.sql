

SET IDENTITY_INSERT [SUSPAASNONPROD].[tst_sustainable].[AION_CNV].[PROJECT] ON
 
INSERT INTO [SUSPAASNONPROD].[tst_sustainable].[AION_CNV].[PROJECT]
(
    PROJECT_ID,
	PROJECT_NM,
	EXTERNAL_SYSTEM_REF_ID,
	PROJECT_STATUS_REF_ID,
	PROJECT_TYP_REF_ID,
	ASSIGNED_ESTIMATOR_ID,
	PROJECT_MODE_REF_ID,
	RTAP_IND,
	PROJECT_ADDR_TXT,
	TOTAL_FEE_AMT,
	PROJECT_MANAGER_ID,
	SRC_SYSTEM_VAL_TXT,	
	TAG_CREATED_BY_TS,
	TAG_UPDATED_BY_TS,
	PRELIMINARY_IND,
	GATE_ACCEPTED_IND,
	FIFO_IND,
	PLANS_READY_ON_DT,
	TEAM_GRADE_TXT,
	GATE_DT,
	CYCLE_NBR,
	BUILD_CODE_VERSION_DESC,
	REVIEW_TYP_REF_DESC,
	CODE_SUMMARY_DESC,
	CONSTR_TYP_DESC,
	WORK_TYP_DESC,
	OVERALL_WORK_SCOPE_DESC,
	ELCTR_WORK_SCOPE_DESC,
	MECH_WORK_SCOPE_DESC,
	PLUMB_WORK_SCOPE_DESC,
	CIVIL_WORK_SCOPE_DESC,
	PERMIT_NUM,
	SHEETS_CNT_DESC,
	DESIGNER_DESC,
	SEAL_HOLDERS_DESC,
	FIRE_DETAIL_DESC,
	OCCUPANCY_DESC,
	PRI_OCCUPANCY_DESC,
	SECONDARY_OCCUPANCY_DESC,
	SQUARE_FOOTAGE_DESC,
	ZONING_OF_SITE_DESC,
	CHG_OF_USE_DESC,
	PREVIOUS_BUSINESS_TYP_DESC,
	PROPOSED_BUSINESS_TYP_DESC,
	CONDITIONAL_PERMIT_APPROVAL_DESC,	
	CITY_OF_CHARLOTTE_DESC,
	WATER_SEWER_DETAIL_DESC,
	PROPOSED_OUTDOOR_UNDERGROUND_PIPING_DESC,
	PROPOSED_FIRE_SPRINKLER_PIPING_DESC,
	INSTALL_CMUD_BACKFLOW_PREVENTER_DESC,
	EXTENDING_PUBLIC_WATER_SEWER_DESC,
	GRADE_MOD_WATER_SEWER_EASEMENT_DESC,
	PROPOSED_ENCROACHMENT_WATER_SEWER_EASEMENT_DESC,
	DAY_CARE_DESC,
	HEALTH_DEPT_DETAIL_DESC
)

--select  TOP (5) * from
--(
SELECT  
	  distinct(CASE 
				     WHEN ISNUMERIC(p.project_number)=0 THEN 
					(CASE 
							WHEN LEN(p.[project_number])=13 THEN SUBSTRING(p.project_number, 4, len(p.project_number)-7)
							WHEN LEN(p.[project_number])=14 THEN SUBSTRING(p.project_number, 5, len(p.project_number)-8)
							WHEN LEN(p.[project_number])=15 THEN SUBSTRING(p.project_number, 6, len(p.project_number)-9)
							WHEN LEN(p.[project_number])=20 THEN SUBSTRING(p.project_number, 7, len(p.project_number)-14)
					 END) ELSE p.project_number
			         END) AS PROJECT_ID,	 
				    nme_project as PROJECT_NM, 
			     -1 as EXTERNAL_SYSTEM_REF_ID,
	(CASE 
		WHEN id_project_state = 1003 then 6
		WHEN id_project_state = 1004 then 7
		WHEN id_project_state = 1015 then 8
		WHEN id_project_state = 1016 then 10
		WHEN id_project_state = 1005 then 11
		WHEN id_project_state = 1006 then 13
		WHEN id_project_state = 1025 then 14
		WHEN id_project_state = 1026 then 16
		WHEN id_project_state = 1025 then 17
		WHEN id_project_state = 1013 then 22
		WHEN id_project_state = 1027 then 22
		WHEN id_project_state = 1011 then 24
		WHEN id_project_state = 1012 then 26
	else -1 end) as PROJECT_STATUS_REF_ID,
	(CASE 
		WHEN txt_review_type_code  = 'RT' then 2
		WHEN txt_review_type_code  = 'OS' then 2
		WHEN txt_review_type_code  = 'ER' then 2
		WHEN txt_review_type_code  = 'MP' then 2
		WHEN txt_review_type_code  = 'PS' then 2
		WHEN txt_review_type_code  = 'PL' then 2
		WHEN txt_review_type_code  = 'ER' then 1
	else -1 end) as PROJECT_TYP_REF_ID,
	(CASE WHEN id_user IS NULL THEN NULL ELSE CONCAT(999 , u.id_user) END) AS ASSIGNED_ESTIMATOR_ID,	
	(CASE WHEN txt_review_type_code  = 'OS' then 2 WHEN txt_review_type_code  = 'ER' then 1 ELSE -1 END) AS PROJECT_MODE_REF_ID,
	(CASE WHEN txt_review_type_code  = 'RT' THEN 1 ELSE 0 END) AS RTAP_IND, 
	txt_address as PROJECT_ADDR_TXT,
	permit_fee as TOTAL_FEE_AMT,
	(CASE WHEN id_project_manager IS NULL THEN NULL ELSE CONCAT(999, id_project_manager)END) AS PROJECT_MANAGER_ID, 
	p.[project_number] as SRC_SYSTEM_VAL_TXT,	
	review_start_date as TAG_CREATED_BY_TS,
	review_end_date as TAG_UPDATED_BY_TS,
	(CASE WHEN txt_review_type_code  = 'PL' then 1 else 0 end) AS PRELIMINARY_IND, 
	0 as GATE_ACCEPTED_IND,
	0 as FIFO_IND
	,plans_ready_on as PLANS_READY_ON_DT
	,team_score_indicator as TEAM_GRADE_TXT  
	,[gate_close_date] as GATE_DT
	,[assessment_cycle] as CYCLE_NBR
	,[txt_cde_summary_code] as BUILD_CODE_VERSION_DESC
	,CASE p.app_form_xml.value('(//rblPropertyType)[1]', 'varchar(max)')
		WHEN 'FA' THEN '1-2 Family -=> 3.5 Stories'
		WHEN 'TH' THEN 'TH, LFS -=> 3.5 Stories'
		WHEN 'CN' THEN 'Condo or Apts'
		WHEN 'CO' THEN 'Commercial'
	   ELSE ''
	 END AS REVIEW_TYP_REF_DESC
	,CASE app_form_xml.value('(//rblBuildingCode)[1]', 'varchar(max)')
		WHEN 'BC-2012' THEN '2012 NC Building Code'
		WHEN 'BC-2015' THEN '2015 NC Existing Building Code'
		WHEN 'BC-2018' THEN '2018 NC Building Code'
		WHEN 'EBC-2018' THEN '2018 NC Existing Building Code'
	  ELSE ''
	 END AS [CODE_SUMMARY_DESC],
	 CASE(select txt_constr_type_desc from ctb_constr_types_defs where txt_constr_types_code = app_form_xml.value('(//rblConstrType)[1]', 'varchar(max)')) 
		WHEN 'I-A'   THEN '1A * NONCOMBUSTIBLE/PROTECTED'
		WHEN 'I-B'   THEN '1B * NONCOMBUSTIBLE/UNPROTECTED'
		WHEN 'II-A'  THEN '2A * NONCOMBUSTIBLE/PROTECTED'
		WHEN 'II-B'  THEN '2B * NONCOMBUSTIBLE/UNPROTECTED'
		WHEN 'III-A' THEN '3A * NONCOMBUSTIBLE WALLS/PROTECTED'
		WHEN 'III-B' THEN '3B * NONCOMBUSTIBLE WALLS/UNPROTECTED'
		WHEN 'IV'    THEN '4 * HEAVY TIMBER'
		WHEN 'V-A'   THEN '5A * WOOD FRAME/PROTECTED'
		WHEN 'V-B'   THEN '5B * WOOD FRAME/UNPROTECTED'
	   ELSE ''
	 END AS [CONSTR_TYP_DESC],
	 LTRIM(RTRIM((select txt_worktype_desc from ctb_worktypes_defs where txt_worktype_code = app_form_xml.value('(//rblWorkType)[1]', 'varchar(max)')))) [WORK_TYP_DESC],
	 app_form_xml.value('(//txtProposedWork)[1]', 'varchar(max)') [OVERALL_WORK_SCOPE_DESC],
	 app_form_xml.value('(//txtElecWrkScope)[1]', 'varchar(max)') [ELCTR_WORK_SCOPE_DESC],
	 app_form_xml.value('(//txtMechWrkScope)[1]', 'varchar(max)') [MECH_WORK_SCOPE_DESC],
	 app_form_xml.value('(//txtPlumbWrkScope)[1]', 'varchar(max)') [PLUMB_WORK_SCOPE_DESC],
	 app_form_xml.value('(//txtCivilWrkScope)[1]', 'varchar(max)') [CIVIL_WORK_SCOPE_DESC],
	 p.project_number as [PERMIT_NUM],
	 app_form_xml.value('(//txtTotalSheets)[1]', 'varchar(max)') SHEETS_CNT_DESC,
	 CONCAT
		(
		   (--frmStructural
			   CASE WHEN LEN(p.app_form_xml.value('(//frmStructural/frmDtl/txtMeckId)[1]', 'varchar(max)'))>0 THEN 
			     CASE WHEN ISNULL(app_form_xml.value('(//frmStructural/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = '' 
				       AND ISNULL(app_form_xml.value('(//frmStructural/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
				  THEN App_form_xml.value('(//frmStructural/frmDtl/txtLicenseNum)[1]', 'varchar(max)') 
				  ELSE  app_form_xml.value('(//frmStructural/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmStructural/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
				END
			END),
			(--frmFireProtection
				CASE WHEN LEN(p.app_form_xml.value('(//frmFireProtection/frmDtl/txtMeckId)[1]', 'varchar(max)'))>0 THEN 
					CASE WHEN ISNULL(app_form_xml.value('(//frmFireProtection/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = '' 
				   	     AND ISNULL(app_form_xml.value('(//frmFireProtection/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
					THEN App_form_xml.value('(//frmFireProtection/frmDtl/txtLicenseNum)[1]', 'varchar(max)') 
					ELSE  app_form_xml.value('(//frmFireProtection/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmFireProtection/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
				END
			END),
			(--frmCivil
				CASE WHEN LEN(p.app_form_xml.value('(//frmCivil/frmDtl/txtMeckId)[1]', 'varchar(max)'))>0 THEN 
					CASE WHEN ISNULL(app_form_xml.value('(//frmCivil/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = '' 
						AND ISNULL(app_form_xml.value('(//frmCivil/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
						THEN App_form_xml.value('(//frmCivil/frmDtl/txtLicenseNum)[1]', 'varchar(max)') 
						ELSE  app_form_xml.value('(//frmCivil/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmCivil/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
					END
			END),
			(--frmPlumbing
			CASE WHEN LEN(p.app_form_xml.value('(//frmPlumbing/frmDtl/txtMeckId)[1]', 'varchar(max)'))>0 THEN 
					CASE WHEN ISNULL(app_form_xml.value('(//frmPlumbing/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = '' 
						AND ISNULL(app_form_xml.value('(//frmPlumbing/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
						THEN App_form_xml.value('(//frmPlumbing/frmDtl/txtLicenseNum)[1]', 'varchar(max)') 
						ELSE  app_form_xml.value('(//frmPlumbing/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmPlumbing/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
					END
			END),
			(--frmMechanical
				CASE WHEN LEN(p.app_form_xml.value('(//frmMechanical/frmDtl/txtMeckId)[1]', 'varchar(max)'))>0 THEN 
					 CASE WHEN ISNULL(app_form_xml.value('(//frmMechanical/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = '' 
						   AND ISNULL(app_form_xml.value('(//frmMechanical/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
						  THEN App_form_xml.value('(//frmMechanical/frmDtl/txtLicenseNum)[1]', 'varchar(max)') 
						  ELSE  app_form_xml.value('(//frmMechanical/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmMechanical/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
					 END
			END),
			(--frmElectrical
			CASE WHEN LEN(p.app_form_xml.value('(//frmElectrical/frmDtl/txtMeckId)[1]', 'varchar(max)'))>0 THEN 
				 CASE WHEN ISNULL(app_form_xml.value('(//frmElectrical/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = '' 
				       AND ISNULL(app_form_xml.value('(//frmElectrical/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
					  THEN App_form_xml.value('(//frmElectrical/frmDtl/txtLicenseNum)[1]', 'varchar(max)') 
					  ELSE  app_form_xml.value('(//frmElectrical/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmElectrical/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
				 END
			END),
		 (--frmArchitect
			CASE WHEN LEN(p.app_form_xml.value('(//frmArchitect/frmDtl/txtMeckId)[1]', 'varchar(max)'))>0 THEN
				  CASE WHEN ISNULL(app_form_xml.value('(//frmArchitect/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = '' 
				       AND ISNULL(app_form_xml.value('(//frmArchitect/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
					  THEN App_form_xml.value('(//frmArchitect/frmDtl/txtLicenseNum)[1]', 'varchar(max)') 
					  ELSE  app_form_xml.value('(//frmArchitect/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmArchitect/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
				 END
			END)
		) AS [DESIGNER_DESC],
		CONCAT
		(
			(--frmStructural
				CASE WHEN LEN(p.app_form_xml.value('(//frmStructural/frmDtl/txtLicenseNum)[1]', 'varchar(max)'))>0 THEN 
			  	  CASE WHEN ISNULL(app_form_xml.value('(//frmStructural/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = '' 
					    AND ISNULL(app_form_xml.value('(//frmStructural/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
				  THEN App_form_xml.value('(//frmStructural/frmDtl/txtLicenseNum)[1]', 'varchar(max)') 
				  ELSE  app_form_xml.value('(//frmStructural/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmStructural/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
				END
			END),
			(--frmFireProtection
				CASE WHEN LEN(p.app_form_xml.value('(//frmFireProtection/frmDtl/txtLicenseNum)[1]', 'varchar(max)'))>0 THEN 
					CASE WHEN ISNULL(app_form_xml.value('(//frmFireProtection/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = '' 
				   	     AND ISNULL(app_form_xml.value('(//frmFireProtection/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
					THEN App_form_xml.value('(//frmFireProtection/frmDtl/txtLicenseNum)[1]', 'varchar(max)') 
					ELSE  app_form_xml.value('(//frmFireProtection/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmFireProtection/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
				END
			END),
			(--frmCivil
				CASE WHEN LEN(p.app_form_xml.value('(//frmCivil/frmDtl/txtLicenseNum)[1]', 'varchar(max)'))>0 THEN 
					CASE WHEN ISNULL(app_form_xml.value('(//frmCivil/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = '' 
						AND ISNULL(app_form_xml.value('(//frmCivil/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
						THEN App_form_xml.value('(//frmCivil/frmDtl/txtLicenseNum)[1]', 'varchar(max)') 
						ELSE  app_form_xml.value('(//frmCivil/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmCivil/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
					END
			END),
			(--frmPlumbing
			CASE WHEN LEN(p.app_form_xml.value('(//frmPlumbing/frmDtl/txtLicenseNum)[1]', 'varchar(max)'))>0 THEN 
					CASE WHEN ISNULL(app_form_xml.value('(//frmPlumbing/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = '' 
						AND ISNULL(app_form_xml.value('(//frmPlumbing/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
						THEN App_form_xml.value('(//frmPlumbing/frmDtl/txtLicenseNum)[1]', 'varchar(max)') 
						ELSE  app_form_xml.value('(//frmPlumbing/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmPlumbing/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
					END
			END),
			(--frmMechanical
				CASE WHEN LEN(p.app_form_xml.value('(//frmMechanical/frmDtl/txtLicenseNum)[1]', 'varchar(max)'))>0 THEN 
					 CASE WHEN ISNULL(app_form_xml.value('(//frmMechanical/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = '' 
						   AND ISNULL(app_form_xml.value('(//frmMechanical/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
						  THEN App_form_xml.value('(//frmMechanical/frmDtl/txtLicenseNum)[1]', 'varchar(max)') 
						  ELSE  app_form_xml.value('(//frmMechanical/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmMechanical/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
					 END
			END),
			(--frmElectrical
			CASE WHEN LEN(p.app_form_xml.value('(//frmElectrical/frmDtl/txtLicenseNum)[1]', 'varchar(max)'))>0 THEN 
				 CASE WHEN ISNULL(app_form_xml.value('(//frmElectrical/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = '' 
				       AND ISNULL(app_form_xml.value('(//frmElectrical/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
					  THEN App_form_xml.value('(//frmElectrical/frmDtl/txtLicenseNum)[1]', 'varchar(max)') 
					  ELSE  app_form_xml.value('(//frmElectrical/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmElectrical/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
				 END
			END),
		 (--frmArchitect
			CASE WHEN LEN(p.app_form_xml.value('(//frmArchitect/frmDtl/txtLicenseNum)[1]', 'varchar(max)'))>0 THEN
				  CASE WHEN ISNULL(app_form_xml.value('(//frmArchitect/frmDtl/txtFirstName)[1]', 'varchar(max)'),'') = '' 
				       AND ISNULL(app_form_xml.value('(//frmArchitect/frmDtl/txtLastName)[1]', 'varchar(max)'),'')  = ''
					  THEN App_form_xml.value('(//frmArchitect/frmDtl/txtLicenseNum)[1]', 'varchar(max)') 
					  ELSE  app_form_xml.value('(//frmArchitect/frmDtl/txtFirstName)[1]', 'varchar(max)') +' '+app_form_xml.value('(//frmArchitect/frmDtl/txtLastName)[1]', 'varchar(max)')+';'
				 END
			END)
		) AS [SEAL_HOLDERS_DESC],
		CONCAT
		(
			'FireNFPAType13R:',
			 app_form_xml.value('(//chkNFPAType13R)[1]', 'varchar(max)')
		    ,';','FireFirePump:',
			 app_form_xml.value('(//rblFirePump)[1]', 'varchar(max)')
            ,';','FireElevator:',
		     app_form_xml.value('(//rblElevator)[1]', 'varchar(max)')
            ,';','FireDrawingsIncluded:', 
			 app_form_xml.value('(//rblIncludesAlarmAndSprinklerDwg)[1]', 'varchar(max)')
		    ,';','FireNFPAType13:',
			 app_form_xml.value('(//chkNFPAType13)[1]', 'varchar(max)')
		    ,';','FireStandpipeClassIII:',
			 app_form_xml.value('(//chkStandpipeClassIII)[1]', 'varchar(max)')
		    ,';','FireStandpipeClassI:',
		     app_form_xml.value('(//chkStandpipeClassI)[1]', 'varchar(max)')
		    ,';','FireStandpipeClassWet:',
		     app_form_xml.value('(//chkStandpipeClassWet)[1]', 'varchar(max)')
		    ,';','FireStandpipe:',
		     app_form_xml.value('(//rblStandpipe)[1]', 'varchar(max)')
		    ,';','FirePumpNewOrExisting:',
		     app_form_xml.value('(//rblFirePumpType)[1]', 'varchar(max)')
		    ,';','FireStandpipeClassDry:',
		     app_form_xml.value('(//chkStandpipeClassDry)[1]', 'varchar(max)')
		    ,';','FireSmokeDetector:',
		     app_form_xml.value('(//rblSmokeDetection)[1]', 'varchar(max)')
		    ,';','FireStandpipeClassII:',
		     app_form_xml.value('(//chkStandpipeClassII)[1]', 'varchar(max)')
		    ,';','FireBuildingSprinkled:',		
		     app_form_xml.value('(//rblBuildingSprinklered)[1]', 'varchar(max)')
		    ,';','id:CE_COM-FIRE.cDETAILS;'
		    ,'FireFireAlarm:',
		     app_form_xml.value('(//rblFireAlarm)[1]', 'varchar(max)')
		    ,';','FireNFPAType13D:',
		     app_form_xml.value('(//chkNFPAType13D)[1]', 'varchar(max)')
		    ,';','FireNFPANewOrExisting:',
		     app_form_xml.value('(//rblSprinklerType)[1]', 'varchar(max)')
		    ,';','FireNFPANewOrExisting:',
 		     app_form_xml.value('(//rblSprinklerType)[1]', 'varchar(max)')
		) AS [FIRE_DETAIL_DESC],
		CASE app_form_xml.value('(//frmPrimaryOccupancy/frmFactoryOrIndustryDtl/rblFactoryOrIndustryType)[1]', 'varchar(max)') 
			    WHEN 'F1' THEN 'F1 * Factory/Industrial - Moderate Hazard'
			    WHEN 'F2' THEN 'F2 * Factory/Industrial - Low Hazard'
		    ELSE 	 
		    CASE app_form_xml.value('(//frmPrimaryOccupancy/frmStorageDtl/rblStorageType)[1]', 'varchar(max)') 
			    WHEN 'S1' THEN 'S1 * Storage - Moderate Hazard'
	 	        WHEN 'S2' THEN 'S2 * Storage - Low Hazard'
		    ELSE
		    CASE app_form_xml.value('(//frmPrimaryOccupancy/frmAssemblyDtl/rblAssemblyType)[1]', 'varchar(max)') 
			    WHEN 'A1' THEN 'A1 * Assembly - Theater w/o Stage'
			    WHEN 'A2' THEN 'A2 * Assembly - Restaurants, Bars, Banquet Halls'
			    WHEN 'A3' THEN 'A3C * Assembly ? Common Assemblies'
			    WHEN 'A4' THEN 'A4 * Assembly ? Indoor Arena, Skating Rink, Tennis Court'
			    WHEN 'A5' THEN 'A5 * Assembly ? Outdoor Stadium, Bleacher, Grandstand'
		    ELSE
		    CASE app_form_xml.value('(//frmPrimaryOccupancy/frmHighHazardDtl/rblHighHazardType)[1]', 'varchar(max)') 
	            WHEN 'H1' THEN 'H1 * High Hazard ? Explosives'
			    WHEN 'H2' THEN 'H2 * High Hazard ? Deflagration'
			    WHEN 'H3' THEN 'H3 * High Hazard ? Readily Combustible'
			    WHEN 'H4' THEN 'H4 * High Hazard ? Health Hazard'
			    WHEN 'H5' THEN 'H5 * High Hazard - HPM'
		    ELSE
	        CASE app_form_xml.value('(//frmPrimaryOccupancy/frmResedentialDtl/rblResedentialType)[1]', 'varchar(max)')
		    	WHEN 'R1' THEN 'R1 * Residential ? Hotels'
			    WHEN 'R2' THEN 'R2 * Residential - Multiple Family'
			    WHEN 'R3' THEN 'R3 * Residential - Single Family'
			    WHEN 'R4' THEN 'R4 * Residential - Care/Assisted Living Facilities, Condition 1 (Ambulatory)'
		    ELSE	 
	        CASE app_form_xml.value('(//frmPrimaryOccupancy/frmInstitutionalDtl/rblInstitutionalType)[1]', 'varchar(max)') 
			    WHEN 'I1' THEN 'I1 * Institutional - Supervised Environment, Condition 1 (Ambulatory)'
			    WHEN 'I2' THEN 'I2H * Institutional - Incapacitated ? Hospital, Full Nursing & Medical Treatment'
			    WHEN 'I3' THEN 'I3 * Institutional - Restrained'
			    WHEN 'I4' THEN 'I4 * Institutional - Day Care'
			    WHEN 'I-3 Use Condition' THEN 'I3 * Institutional - Restrained'
	        ELSE
            CASE 
		        WHEN app_form_xml.value('(//frmPrimaryOccupancy/rblOccupancyType)[1]', 'varchar(max)') = 'BS' THEN 'B * Business'
		        WHEN app_form_xml.value('(//frmPrimaryOccupancy/rblOccupancyType)[1]', 'varchar(max)') = 'ED' THEN 'E * Education'
		        WHEN app_form_xml.value('(//frmPrimaryOccupancy/rblOccupancyType)[1]', 'varchar(max)') = 'MR' THEN 'M * Mercantile'
		        WHEN app_form_xml.value('(//frmPrimaryOccupancy/rblOccupancyType)[1]', 'varchar(max)') = 'ED' THEN 'E * Education'
                WHEN app_form_xml.value('(//frmPrimaryOccupancy/rblOccupancyType)[1]', 'varchar(max)') = 'UM' THEN 'U * Utility'
		    ELSE ''
			    END  
			    END
			    END
			    END
			    END
			    END
		    END AS [OCCUPANCY_DESC],
			CASE app_form_xml.value('(//frmPrimaryOccupancy/rblOccupancyType)[1]', 'varchar(max)')
			    WHEN 'AS' THEN 'Assembly'
			    WHEN 'BS' THEN 'Business'
			    WHEN 'ED' THEN 'Educational'
			    WHEN 'MR' THEN 'Mercantile'
			    WHEN 'FI' THEN 'Factory/Industrial'
			    WHEN 'HH' THEN 'High Hazard'
			    WHEN 'IN' THEN 'Institutional'
			    WHEN 'RE' THEN 'Residential'
			    WHEN 'ST' THEN 'Storage'
			    WHEN 'UM' THEN 'Utility/Miscellaneous'
		    ELSE ''
		    END AS [PRI_OCCUPANCY_DESC],
			CASE app_form_xml.value('(//frmSecondaryOccupancy/rblOccupancyType)[1]', 'varchar(max)')
			    WHEN 'AS' THEN 'Assembly'
			    WHEN 'BS' THEN 'Business'
			    WHEN 'ED' THEN 'Educational'
			    WHEN 'MR' THEN 'Mercantile'
			    WHEN 'FI' THEN 'Factory/Industrial'
			    WHEN 'HH' THEN 'High Hazard'
			    WHEN 'IN' THEN 'Institutional'
			    WHEN 'RE' THEN 'Residential'
			    WHEN 'ST' THEN 'Storage'
			    WHEN 'UM' THEN 'Utility/Miscellaneous'
		    ELSE ''
		    END AS [SECONDARY_OCCUPANCY_DESC],
		    CAST(CAST(app_form_xml.value('(//txtSqFtArea)[1]', 'varchar(max)') AS money)AS int) [SQUARE_FOOTAGE_DESC],
		    app_form_xml.value('(//txtZoning)[1]', 'varchar(max)') [ZONING_OF_SITE_DESC],
		    
			CASE
			    WHEN app_form_xml.value('(//chkChangeOfUse)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
			    ELSE 'CHECKED'
		    END AS [CHG_OF_USE_DESC],
		        app_form_xml.value('(//txtPreviousBiz)[1]', 'varchar(max)') [PREVIOUS_BUSINESS_TYP_DESC],
		        app_form_xml.value('(//txtPropsedBiz)[1]', 'varchar(max)') [PROPOSED_BUSINESS_TYP_DESC],		
		    CASE app_form_xml.value('(//rblHasCondProjNum)[1]', 'varchar(max)')
			    WHEN 'N' THEN 'No'
			    WHEN 'Y' THEN 'Yes'			
		    ELSE ''
		    END AS [CONDITIONAL_PERMIT_APPROVAL_DESC],		
		    CONCAT(
				'CityAddingImperviousArea:',
				 app_form_xml.value('(//chkAddingImperviousArea)[1]', 'varchar(max)')						
				,';','CityChangingDrivewayEtc:',
				app_form_xml.value('(//chkChangingDrivewayEtc)[1]', 'varchar(max)')
				 ,';','CityAddingTreePlanting:'
				,app_form_xml.value('(//chkAddingTreePlanting)[1]', 'varchar(max)')
				,';','CityAddingTreeProtection:',
				app_form_xml.value('(//chkAddingTreeProtection)[1]', 'varchar(max)')
				,';','CityGradingMoreThanOneAcre:',
				app_form_xml.value('(//chkGradingMoreThanOneAcre)[1]', 'varchar(max)')
				,';','CityAddingImperviousArea:',
				app_form_xml.value('(//chkAddingImperviousArea)[1]', 'varchar(max)')
				,';','CityChangingDrivewayEtc:',
				app_form_xml.value('(//chkChangingDrivewayEtc)[1]', 'varchar(max)')
				,';','CityAddingTreePlanting:',
				app_form_xml.value('(//chkAddingTreePlanting)[1]', 'varchar(max)')
				,';','CityAddingTreeProtection:',
				app_form_xml.value('(//chkAddingTreeProtection)[1]', 'varchar(max)')
				,';','CityGradingMoreThanOneAcre:',
				app_form_xml.value('(//chkGradingMoreThanOneAcre)[1]', 'varchar(max)')
				,';','CityBuildingOver1000SqFt:',
				app_form_xml.value('(//chkBuildingOver1000SqFt)[1]', 'varchar(max)') 
				,'CityBuildingOver5PercentSqFt:',
				app_form_xml.value('(//chkBuildingOver5PercentSqFt)[1]', 'varchar(max)') 
				,';','CityAdding11OrMoreParkingSpace:',
				app_form_xml.value('(//chkAdding11OrMoreParkingSpace)[1]', 'varchar(max)') 
				,'CityChangingFacadeOver10Percent:',
				app_form_xml.value('(//chkChangingFacadeOver10Percent)[1]', 'varchar(max)')
				,';','CityZonedUrban:',
			CASE
				WHEN app_form_xml.value('(//chkZonedUrban)[1]', 'varchar(max)') = 'ZU' THEN 'CHECKED'
				WHEN app_form_xml.value('(//chkZonedUrban)[1]', 'varchar(max)') = 'Y' THEN 'CHECKED'
				ELSE 'UNCHECKED'
			 END
			,';','CityPlannedMultiFamily:',
			CASE
			   WHEN app_form_xml.value('(//chkPlannedMultiFamily)[1]', 'varchar(max)') = 'PM' THEN 'CHECKED'
			   WHEN app_form_xml.value('(//chkPlannedMultiFamily)[1]', 'varchar(max)') = 'Y' THEN 'CHECKED'
			   ELSE 'UNCHECKED'
			END
			,';','CityAdjoinsPublicStreet:',
			app_form_xml.value('(//chkAdjoinsPublicStreet)[1]', 'varchar(max)')
			,';','CityNewPublicOrPrivateStreet:',
			app_form_xml.value('(//chkNewPublicOrPrivateStreet)[1]', 'varchar(max)')
			,';') AS [CITY_OF_CHARLOTTE_DESC],
		CONCAT(				
			'NewSepticTank:',
			CASE
				WHEN app_form_xml.value('(//chkNewSepticTank)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
				ELSE 'UNCHECKED'
				END ,';',
				'NewPublicWater:',
			CASE
				WHEN app_form_xml.value('(//chkNewPublicWater)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
				ELSE 'UNCHECKED'
				END
			,';','ExistingPublicWater:',
			CASE
				WHEN app_form_xml.value('(//chkExistingPublicWater)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
				ELSE 'UNCHECKED'
			END
			,';','ExistingPublicSewer:',
			CASE
				WHEN app_form_xml.value('(//chkExistingPublicSewer)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
				ELSE 'UNCHECKED'
				END
			,';','NewPublicSewer:',
			CASE
				WHEN app_form_xml.value('(//chkNewPublicSewer)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
				ELSE 'UNCHECKED'
			END
			,';','ExistingWell:',
			CASE
				WHEN app_form_xml.value('(//chkExistingWell)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
				ELSE 'UNCHECKED'
			END
			,';','NewWell',
			CASE
				WHEN app_form_xml.value('(//chkNewWell)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
				ELSE 'UNCHECKED'
				END
			,';'
            ,'id:CE_COM-WATER.cSEWER.cDETAILS;'
            ,'ExistingSepticTank:',
			CASE
				WHEN app_form_xml.value('(//chkExistingSepticTank)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
				ELSE 'UNCHECKED'
				END
			,';') AS [WATER_SEWER_DETAIL_DESC],
			 LTRIM(RTRIM((select txt_backflow_desc from ctb_backflow_defs where txt_backflow_code = app_form_xml.value('(//rblUndergroundPiping)[1]', 'varchar(max)')))) [PROPOSED_OUTDOOR_UNDERGROUND_PIPING_DESC]
			,LTRIM(RTRIM((select txt_backflow_desc from ctb_backflow_defs where txt_backflow_code = app_form_xml.value('(//rblSprinklerPiping)[1]', 'varchar(max)')))) [PROPOSED_FIRE_SPRINKLER_PIPING_DESC],
			app_form_xml.value('(//rblCMUDPreventer)[1]', 'varchar(max)') [INSTALL_CMUD_BACKFLOW_PREVENTER_DESC],
			app_form_xml.value('(//rblExtendsPublicWaterOrSanitarySewerSystem)[1]', 'varchar(max)') [EXTENDING_PUBLIC_WATER_SEWER_DESC],
			app_form_xml.value('(//rblGradeModificationWithinCharlotteEasement)[1]', 'varchar(max)') [GRADE_MOD_WATER_SEWER_EASEMENT_DESC],
			app_form_xml.value('(//rblEncroachmentInCharlotteEasement)[1]', 'varchar(max)') [PROPOSED_ENCROACHMENT_WATER_SEWER_EASEMENT_DESC],
		CONCAT(				
			'HDMeatMarket:',
			CASE
				WHEN app_form_xml.value('(//chkMeatMarket)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
				ELSE 'CHECKED'
			END
			,';','HDAdultDayCare:',
			CASE
				WHEN app_form_xml.value('(//chkAdultDayCare)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
				ELSE 'CHECKED'
			END
			,';','HDLodging:',
			CASE
				WHEN app_form_xml.value('(//chkLodging)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
				ELSE 'CHECKED'
			END
			,';','HDSeafood:',
			CASE
				WHEN app_form_xml.value('(//chkSeafood)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
				ELSE 'CHECKED'
			END
			,';','HDSeafoodUtensilType:',
			CASE  
				WHEN 
					app_form_xml.value('(//chkSeafoodUtensilReusable)[1]', 'varchar(max)') != ''
				AND 
				app_form_xml.value('(//chkSeafoodUtensilDisposable)[1]', 'varchar(max)') != ''
			    THEN 'Both'
			    WHEN 
					app_form_xml.value('(//chkSeafoodUtensilReusable)[1]', 'varchar(max)') != ''
				AND 
				app_form_xml.value('(//chkSeafoodUtensilDisposable)[1]', 'varchar(max)') = ''
			    THEN  'Reusable'
				WHEN 
				app_form_xml.value('(//chkSeafoodUtensilReusable)[1]', 'varchar(max)') = ''
				AND 
				app_form_xml.value('(//chkSeafoodUtensilDisposable)[1]', 'varchar(max)') != ''
				THEN  'Disposable'		  
			END
			,';','HDSeafoodCapacity:',
			app_form_xml.value('(//txtSeafoodCapacity)[1]', 'varchar(max)')
			,';','HDWaterRecreation_Pool:',
			CASE
				WHEN app_form_xml.value('(//chkWaterRecreation)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
				ELSE 'CHECKED'
			END 
			,';','HDBarServiceUtensilType:',
			CASE  
			    WHEN 
				app_form_xml.value('(//chkBarSvcUtensilReusable)[1]', 'varchar(max)') != ''
				AND 
				app_form_xml.value('(//chkBarSvcUtensilDisposable)[1]', 'varchar(max)') != ''
			    THEN 'Both'
			    WHEN 
				app_form_xml.value('(//chkBarSvcUtensilReusable)[1]', 'varchar(max)') != ''
			    AND 
				app_form_xml.value('(//chkBarSvcUtensilDisposable)[1]', 'varchar(max)') = ''
				THEN  'Reusable'
				WHEN 
				app_form_xml.value('(//chkBarSvcUtensilReusable)[1]', 'varchar(max)') = ''
				AND 
				app_form_xml.value('(//chkBarSvcUtensilDisposable)[1]', 'varchar(max)') != ''
				THEN  'Disposable'		  
			END
			,';','HDOtherl:',
			CASE
				WHEN app_form_xml.value('(//chkOther)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
				ELSE 'CHECKED'
			END,
			';','HDBarServiceCapacity:',
				app_form_xml.value('(//txtBarSvcCapacity)[1]', 'varchar(max)') 
			,';','Restaurant:',
			CASE
			    WHEN app_form_xml.value('(//chkRestaurant)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
			    ELSE 'CHECKED'
			END,
			';','RestaurantUtensilType:',
			CASE  
			    WHEN 
				app_form_xml.value('(//chkRestaurantUtensilReusable)[1]', 'varchar(max)') != ''
				AND 
				app_form_xml.value('(//chkRestaurantUtensilDisposable)[1]', 'varchar(max)') != ''
				THEN 'Both'
				WHEN 
				app_form_xml.value('(//chkRestaurantUtensilReusable)[1]', 'varchar(max)') != ''
				AND 
				app_form_xml.value('(//chkRestaurantUtensilDisposable)[1]', 'varchar(max)') = ''
				THEN  'Reusable'
				WHEN 
				app_form_xml.value('(//chkRestaurantUtensilReusable)[1]', 'varchar(max)') = ''
				AND 
				app_form_xml.value('(//chkRestaurantUtensilDisposable)[1]', 'varchar(max)') != ''
				THEN  'Disposable'
				END
			,';','HDAdultDayCareCapacity:',
			app_form_xml.value('(//txrNumAdults)[1]', 'varchar(max)') 
			,';','HDChildDayCare:',
			CASE
			    WHEN app_form_xml.value('(//chkChildDayCare)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
				ELSE 'CHECKED'
				END
			,';','HDChildDayCareCapacity:',
			app_form_xml.value('(//txtNumChildren)[1]', 'varchar(max)') 
			,';','HDOtherDescription:',
			app_form_xml.value('(//txtOtherDesc)[1]', 'varchar(max)') 
			,';','RestaurantCapacity:',
			app_form_xml.value('(//txtRestaurantCapacity)[1]', 'varchar(max)') 
			,';'
            ,'id:CE_COM-HEALTH.cDEPARTMENT.cDETAILS;',
			'BarService:',
			CASE
				WHEN app_form_xml.value('(//chkBarSvc)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
				ELSE 'CHECKED'
				END 
			,';') AS [DAY_CARE_DESC],

			CONCAT(				
			'HDMeatMarket:',
			CASE
				WHEN app_form_xml.value('(//chkMeatMarket)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
				ELSE 'CHECKED'
			END
			,';','HDAdultDayCare:',
			CASE
				WHEN app_form_xml.value('(//chkAdultDayCare)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
				ELSE 'CHECKED'
			END
			,';','HDLodging:',
			CASE
				WHEN app_form_xml.value('(//chkLodging)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
				ELSE 'CHECKED'
			END
			,';','HDSeafood:',
			CASE
				WHEN app_form_xml.value('(//chkSeafood)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
				ELSE 'CHECKED'
			END
			,';','HDSeafoodUtensilType:',
			CASE  
				WHEN 
					app_form_xml.value('(//chkSeafoodUtensilReusable)[1]', 'varchar(max)') != ''
				AND 
				app_form_xml.value('(//chkSeafoodUtensilDisposable)[1]', 'varchar(max)') != ''
			    THEN 'Both'
			    WHEN 
					app_form_xml.value('(//chkSeafoodUtensilReusable)[1]', 'varchar(max)') != ''
				AND 
				app_form_xml.value('(//chkSeafoodUtensilDisposable)[1]', 'varchar(max)') = ''
			    THEN  'Reusable'
				WHEN 
				app_form_xml.value('(//chkSeafoodUtensilReusable)[1]', 'varchar(max)') = ''
				AND 
				app_form_xml.value('(//chkSeafoodUtensilDisposable)[1]', 'varchar(max)') != ''
				THEN  'Disposable'		  
			END
			,';','HDSeafoodCapacity:',
			app_form_xml.value('(//txtSeafoodCapacity)[1]', 'varchar(max)')
			,';','HDWaterRecreation_Pool:',
			CASE
				WHEN app_form_xml.value('(//chkWaterRecreation)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
				ELSE 'CHECKED'
			END 
			,';','HDBarServiceUtensilType:',
			CASE  
			    WHEN 
				app_form_xml.value('(//chkBarSvcUtensilReusable)[1]', 'varchar(max)') != ''
				AND 
				app_form_xml.value('(//chkBarSvcUtensilDisposable)[1]', 'varchar(max)') != ''
			    THEN 'Both'
			    WHEN 
				app_form_xml.value('(//chkBarSvcUtensilReusable)[1]', 'varchar(max)') != ''
			    AND 
				app_form_xml.value('(//chkBarSvcUtensilDisposable)[1]', 'varchar(max)') = ''
				THEN  'Reusable'
				WHEN 
				app_form_xml.value('(//chkBarSvcUtensilReusable)[1]', 'varchar(max)') = ''
				AND 
				app_form_xml.value('(//chkBarSvcUtensilDisposable)[1]', 'varchar(max)') != ''
				THEN  'Disposable'		  
			END
			,';','HDOtherl:',
			CASE
				WHEN app_form_xml.value('(//chkOther)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
				ELSE 'CHECKED'
			END,
			';','HDBarServiceCapacity:',
				app_form_xml.value('(//txtBarSvcCapacity)[1]', 'varchar(max)') 
			,';','Restaurant:',
			CASE
			    WHEN app_form_xml.value('(//chkRestaurant)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
			    ELSE 'CHECKED'
			END,
			';','RestaurantUtensilType:',
			CASE  
			    WHEN 
				app_form_xml.value('(//chkRestaurantUtensilReusable)[1]', 'varchar(max)') != ''
				AND 
				app_form_xml.value('(//chkRestaurantUtensilDisposable)[1]', 'varchar(max)') != ''
				THEN 'Both'
				WHEN 
				app_form_xml.value('(//chkRestaurantUtensilReusable)[1]', 'varchar(max)') != ''
				AND 
				app_form_xml.value('(//chkRestaurantUtensilDisposable)[1]', 'varchar(max)') = ''
				THEN  'Reusable'
				WHEN 
				app_form_xml.value('(//chkRestaurantUtensilReusable)[1]', 'varchar(max)') = ''
				AND 
				app_form_xml.value('(//chkRestaurantUtensilDisposable)[1]', 'varchar(max)') != ''
				THEN  'Disposable'
				END
			,';','HDAdultDayCareCapacity:',
			app_form_xml.value('(//txrNumAdults)[1]', 'varchar(max)') 
			,';','HDChildDayCare:',
			CASE
			    WHEN app_form_xml.value('(//chkChildDayCare)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
				ELSE 'CHECKED'
				END
			,';','HDChildDayCareCapacity:',
			app_form_xml.value('(//txtNumChildren)[1]', 'varchar(max)') 
			,';','HDOtherDescription:',
			app_form_xml.value('(//txtOtherDesc)[1]', 'varchar(max)') 
			,';','RestaurantCapacity:',
			app_form_xml.value('(//txtRestaurantCapacity)[1]', 'varchar(max)') 
			,';'
            ,'id:CE_COM-HEALTH.cDEPARTMENT.cDETAILS;',
			'BarService:',
			CASE
				WHEN app_form_xml.value('(//chkBarSvc)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
				ELSE 'CHECKED'
				END 
			,';') AS [HEALTH_DEPT_DETAIL_DESC]

FROM 
		dbo.tb_project_current_status p 
LEFT JOIN dbo.tb_users u on p.txt_performed_by = u.txt_username
WHERE 	
	txt_review_type_code <> 'WL'
--)as TempTable


SET IDENTITY_INSERT [SUSPAASNONPROD].[tst_sustainable].[AION_CNV].[PROJECT] OFF