select 
		p.project_number,
		p.app_form_xml as xml,		
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
		) AS DESIGNER_DESC,
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
		) AS SEAL_HOLDERS_DESC,	
		CONCAT
		(
			'FireNFPAType13R:',
			 CASE
				 WHEN (select txt_fire_dtls_desc from ctb_fire_dtls_defs where cast(fire_dtls_code as varchar) = app_form_xml.value('(//chkNFPAType13R)[1]', 'varchar(max)')) = '13R' 
				 THEN 'CHECKED'
				 ELSE 'UNCHECKED'
			 END
		,';'
		,'FireFirePump:',
		CASE app_form_xml.value('(//rblFirePump)[1]', 'varchar(max)')
				WHEN 'FP_Y' THEN 'Yes'
				WHEN 'FP_N' THEN 'No'
			ELSE ''
		END ,';',
		'FireElevator:',
		CASE app_form_xml.value('(//rblElevator)[1]', 'varchar(max)')
				WHEN 'EL_Y' THEN 'Yes'
				WHEN 'EL_N' THEN 'No'
			ELSE ''
		END,';',
		'FireDrawingsIncluded:',
		CASE app_form_xml.value('(//rblIncludesAlarmAndSprinklerDwg)[1]', 'varchar(max)') 
				WHEN 'FS_Y' THEN 'Yes'
				WHEN 'FS_N' THEN 'No'
			ELSE ''
		END  
		,';',
		'FireNFPAType13:',
		CASE
			WHEN (select txt_fire_dtls_desc from ctb_fire_dtls_defs where cast(fire_dtls_code as varchar) = app_form_xml.value('(//chkNFPAType13)[1]', 'varchar(max)')) = '13' 
				THEN 'CHECKED'
				ELSE 'UNCHECKED'
		END
		,';',
		'FireStandpipeClassIII:',
		CASE
			WHEN (select txt_fire_dtls_desc from ctb_fire_dtls_defs where cast(fire_dtls_code as varchar) = app_form_xml.value('(//chkStandpipeClassIII)[1]', 'varchar(max)')) = 'III' THEN 'CHECKED'
				ELSE 'UNCHECKED'
		END
		,';',
		'FireStandpipeClassI:',
		CASE
			WHEN (select txt_fire_dtls_desc from ctb_fire_dtls_defs where cast(fire_dtls_code as varchar) = app_form_xml.value('(//chkStandpipeClassI)[1]', 'varchar(max)')) = 'I' THEN 'CHECKED'
				ELSE 'UNCHECKED'
		END
		,';',
		'FireStandpipeClassWet:',
		 CASE
			WHEN (select txt_fire_dtls_desc from ctb_fire_dtls_defs where cast(fire_dtls_code as varchar) = app_form_xml.value('(//chkStandpipeClassWet)[1]', 'varchar(max)')) = 'Wet' THEN 'CHECKED'
				ELSE 'UNCHECKED'
		END
		,';',
		'FireStandpipe:',
		 CASE app_form_xml.value('(//rblStandpipe)[1]', 'varchar(max)') 
			WHEN 'ST_Y' THEN 'Yes'
			WHEN 'ST_N' THEN 'No'
			ELSE ''
		END
		,';'
		,'FirePumpNewOrExisting:',
		  LTRIM(RTRIM((select txt_fire_dtls_desc from ctb_fire_dtls_defs where cast(fire_dtls_code as varchar) = app_form_xml.value('(//rblFirePumpType)[1]', 'varchar(max)')))) 
		,';'
		,'FireStandpipeClassDry:',
		  CASE
             WHEN (select txt_fire_dtls_desc from ctb_fire_dtls_defs where cast(fire_dtls_code as varchar) = app_form_xml.value('(//chkStandpipeClassDry)[1]', 'varchar(max)')) = 'Dry' 
			    THEN 'CHECKED'
			    ELSE 'UNCHECKED'
		  END 
		 ,';'
		 ,'FireSmokeDetector:',
		   CASE app_form_xml.value('(//rblSmokeDetection)[1]', 'varchar(max)')
				WHEN 'SD_Y' THEN 'Yes'
				WHEN 'SD_N' THEN 'No'
			ELSE ''
			END
		 ,';'
		,'FireStandpipeClassII:',
		  CASE
			  WHEN (select txt_fire_dtls_desc from ctb_fire_dtls_defs where cast(fire_dtls_code as varchar) = app_form_xml.value('(//chkStandpipeClassII)[1]', 'varchar(max)')) = 'II' THEN 'CHECKED'
				ELSE 'UNCHECKED'
			  END
		,';'
		,'FireBuildingSprinkled:',		
		  CASE app_form_xml.value('(//rblBuildingSprinklered)[1]', 'varchar(max)') 
				WHEN 'SP_Y' THEN 'Yes'
				WHEN 'SP_N' THEN 'No'
				ELSE ''
			END
		,';'
		,'id:CE_COM-FIRE.cDETAILS;'
		,'FireFireAlarm:',
		  CASE app_form_xml.value('(//rblFireAlarm)[1]', 'varchar(max)')
				WHEN 'FA_Y' THEN 'Yes'
				WHEN 'FA_N' THEN 'No'
				ELSE ''
		  END
		  ,';'
		  ,'FireNFPAType13D:',
		  CASE
			WHEN (select txt_fire_dtls_desc from ctb_fire_dtls_defs where cast(fire_dtls_code as varchar) = app_form_xml.value('(//chkNFPAType13D)[1]', 'varchar(max)')) = '13D' 
			THEN 'CHECKED'
			ELSE 'UNCHECKED'
		  END
		  ,';'
		  ,'FireNFPANewOrExisting:',
		    LTRIM(RTRIM((select txt_fire_dtls_desc from ctb_fire_dtls_defs where cast(fire_dtls_code as varchar) = app_form_xml.value('(//rblSprinklerType)[1]', 'varchar(max)'))))
		  ,';'
		  ,'FireNFPANewOrExisting:',
 		   LTRIM(RTRIM((select txt_fire_dtls_desc from ctb_fire_dtls_defs where cast(fire_dtls_code as varchar) = app_form_xml.value('(//rblSprinklerType)[1]', 'varchar(max)'))))
		) AS FIRE_DETAIL_DESC,		
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
			WHEN 'A3' THEN 'A3C * Assembly – Common Assemblies'
			WHEN 'A4' THEN 'A4 * Assembly – Indoor Arena, Skating Rink, Tennis Court'
			WHEN 'A5' THEN 'A5 * Assembly – Outdoor Stadium, Bleacher, Grandstand'
		ELSE
		CASE app_form_xml.value('(//frmPrimaryOccupancy/frmHighHazardDtl/rblHighHazardType)[1]', 'varchar(max)') 
	        WHEN 'H1' THEN 'H1 * High Hazard – Explosives'
			WHEN 'H2' THEN 'H2 * High Hazard – Deflagration'
			WHEN 'H3' THEN 'H3 * High Hazard – Readily Combustible'
			WHEN 'H4' THEN 'H4 * High Hazard – Health Hazard'
			WHEN 'H5' THEN 'H5 * High Hazard - HPM'
		ELSE
	  CASE app_form_xml.value('(//frmPrimaryOccupancy/frmResedentialDtl/rblResedentialType)[1]', 'varchar(max)')
			WHEN 'R1' THEN 'R1 * Residential – Hotels'
			WHEN 'R2' THEN 'R2 * Residential - Multiple Family'
			WHEN 'R3' THEN 'R3 * Residential - Single Family'
			WHEN 'R4' THEN 'R4 * Residential - Care/Assisted Living Facilities, Condition 1 (Ambulatory)'
		ELSE	 
	  CASE app_form_xml.value('(//frmPrimaryOccupancy/frmInstitutionalDtl/rblInstitutionalType)[1]', 'varchar(max)') 
			WHEN 'I1' THEN 'I1 * Institutional - Supervised Environment, Condition 1 (Ambulatory)'
			WHEN 'I2' THEN 'I2H * Institutional - Incapacitated – Hospital, Full Nursing & Medical Treatment'
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
		END AS [OCCUPANCY_DESC] ,
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
		cast(cast(app_form_xml.value('(//txtSqFtArea)[1]', 'varchar(max)') as money)as int) SQUARE_FOOTAGE_DESC,		
		app_form_xml.value('(//txtZoning)[1]', 'varchar(max)') ZONING_OF_SITE_DESC,
		CASE
			WHEN app_form_xml.value('(//chkChangeOfUse)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
			ELSE 'CHECKED'
		END AS [CHG_OF_USE_DESC],		
		app_form_xml.value('(//txtPreviousBiz)[1]', 'varchar(max)') PREVIOUS_BUSINESS_TYP_DESC,
		app_form_xml.value('(//txtPropsedBiz)[1]', 'varchar(max)') PROPOSED_BUSINESS_TYP_DESC,		
		CASE app_form_xml.value('(//rblHasCondProjNum)[1]', 'varchar(max)')
			WHEN 'N' THEN 'No'
			WHEN 'Y' THEN 'Yes'			
		 ELSE ''
		END AS [ConditionalPermitApproval],
		
		CONCAT(
				'CityAddingImperviousArea:',
				  app_form_xml.value('(//chkAddingImperviousArea)[1]', 'varchar(max)')						
				,';'
				,'CityChangingDrivewayEtc:',
				app_form_xml.value('(//chkChangingDrivewayEtc)[1]', 'varchar(max)')
				,';'
				,'CityAddingTreePlanting:'
				,app_form_xml.value('(//chkAddingTreePlanting)[1]', 'varchar(max)')
				,';'
				,'CityAddingTreeProtection:',
				app_form_xml.value('(//chkAddingTreeProtection)[1]', 'varchar(max)')
				,';',
				'CityGradingMoreThanOneAcre:',
				app_form_xml.value('(//chkGradingMoreThanOneAcre)[1]', 'varchar(max)')
				,';',
				'CityAddingImperviousArea:',
				app_form_xml.value('(//chkAddingImperviousArea)[1]', 'varchar(max)')
				,';',
				'CityChangingDrivewayEtc:',
				app_form_xml.value('(//chkChangingDrivewayEtc)[1]', 'varchar(max)')
				,';',
				'CityAddingTreePlanting:',
				app_form_xml.value('(//chkAddingTreePlanting)[1]', 'varchar(max)')
				,';',
				'CityAddingTreeProtection:',
				app_form_xml.value('(//chkAddingTreeProtection)[1]', 'varchar(max)')
				,';',
				'CityGradingMoreThanOneAcre:',
				app_form_xml.value('(//chkGradingMoreThanOneAcre)[1]', 'varchar(max)')
				,';',
				'CityBuildingOver1000SqFt:',
				app_form_xml.value('(//chkBuildingOver1000SqFt)[1]', 'varchar(max)') 
				,'CityBuildingOver5PercentSqFt:',
				 app_form_xml.value('(//chkBuildingOver5PercentSqFt)[1]', 'varchar(max)') 
				,';',
				'CityAdding11OrMoreParkingSpace:',
				 app_form_xml.value('(//chkAdding11OrMoreParkingSpace)[1]', 'varchar(max)') 
				 ,'CityChangingFacadeOver10Percent:',
				 app_form_xml.value('(//chkChangingFacadeOver10Percent)[1]', 'varchar(max)')
				 ,';',
				 'CityZonedUrban:',
				 CASE
					WHEN app_form_xml.value('(//chkZonedUrban)[1]', 'varchar(max)') = 'ZU' THEN 'CHECKED'
					WHEN app_form_xml.value('(//chkZonedUrban)[1]', 'varchar(max)') = 'Y' THEN 'CHECKED'
					ELSE 'UNCHECKED'
				 END
				 ,';'
				 ,'CityPlannedMultiFamily:',
				 CASE
				 	  WHEN app_form_xml.value('(//chkPlannedMultiFamily)[1]', 'varchar(max)') = 'PM' THEN 'CHECKED'
					  WHEN app_form_xml.value('(//chkPlannedMultiFamily)[1]', 'varchar(max)') = 'Y' THEN 'CHECKED'
				   ELSE 'UNCHECKED'
				  END
				 ,';',
				'CityAdjoinsPublicStreet:',
				 app_form_xml.value('(//chkAdjoinsPublicStreet)[1]', 'varchar(max)')
				 ,';',
				'CityNewPublicOrPrivateStreet:',
				 app_form_xml.value('(//chkNewPublicOrPrivateStreet)[1]', 'varchar(max)')
				 ,';'
		      ) AS CITY_OF_CHARLOTTE_DESC,
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
					,';',
					'ExistingPublicWater:',
					CASE
						WHEN app_form_xml.value('(//chkExistingPublicWater)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
						ELSE 'UNCHECKED'
					END
					,';',
					'ExistingPublicSewer:',
					CASE
						WHEN app_form_xml.value('(//chkExistingPublicSewer)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
						ELSE 'UNCHECKED'
					END
					,';',
					'NewPublicSewer:',
					CASE
						WHEN app_form_xml.value('(//chkNewPublicSewer)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
						ELSE 'UNCHECKED'
					END
					,';',
					'ExistingWell:',
					CASE
						WHEN app_form_xml.value('(//chkExistingWell)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
						ELSE 'UNCHECKED'
					END
					,';'
					,'NewWell',
					CASE
						WHEN app_form_xml.value('(//chkNewWell)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
						ELSE 'UNCHECKED'
					END
					,';',
					'id:CE_COM-WATER.cSEWER.cDETAILS;',
					'ExistingSepticTank:',
					CASE
						WHEN app_form_xml.value('(//chkExistingSepticTank)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
						ELSE 'UNCHECKED'
					END
					,';'
			  ) AS WATER_SEWER_DETAIL_DESC,
			  LTRIM(RTRIM((select txt_backflow_desc from ctb_backflow_defs where txt_backflow_code = app_form_xml.value('(//rblUndergroundPiping)[1]', 'varchar(max)')))) PROPOSED_OUTDOOR_UNDERGROUND_PIPING_DESC
			 ,LTRIM(RTRIM((select txt_backflow_desc from ctb_backflow_defs where txt_backflow_code = app_form_xml.value('(//rblSprinklerPiping)[1]', 'varchar(max)')))) PROPOSED_FIRE_SPRINKLER_PIPING_DESC,
			 app_form_xml.value('(//rblCMUDPreventer)[1]', 'varchar(max)') INSTALL_CMUD_BACKFLOW_PREVENTER_DESC,
			 app_form_xml.value('(//rblExtendsPublicWaterOrSanitarySewerSystem)[1]', 'varchar(max)') EXTENDING_PUBLIC_WATER_SEWER_DESC,
			 app_form_xml.value('(//rblGradeModificationWithinCharlotteEasement)[1]', 'varchar(max)') GRADE_MOD_WATER_SEWER_EASEMENT_DESC,
			 app_form_xml.value('(//rblEncroachmentInCharlotteEasement)[1]', 'varchar(max)') PROPOSED_ENCROACHMENT_WATER_SEWER_EASEMENT_DESC

from tb_project_current_status p
where p.project_number='390604'
 
