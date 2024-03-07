/****** EPM Preliminary Application Form Data Extract  ******/
 
USE tst_EPM;

SELECT
	[project_number] PERMITNUM
	 ,h.TT_Record
	 ,app_form_xml.value('(//txtScopeDetails)[1]', 'varchar(max)') ProposedScopeOfWork
	 --,app_form_xml.value('(//rblReviewType)[1]', 'varchar(max)') ReviewType
	 ,h.review_type ReviewType
	 --,'Commercial' ReviewType
	 ,CASE app_form_xml.value('(//rblPropertyType)[1]', 'varchar(max)')
		WHEN 'FA' THEN '1-2 Family -=> 3.5 Stories'
		WHEN 'TH' THEN 'TH, LFS -=> 3.5 Stories'
		WHEN 'CN' THEN 'Condo or Apts'
		WHEN 'CO' THEN 'Commercial'
		ELSE ''
		END AS [PropertyType]
	,CASE app_form_xml.value('(//rblBuildingCode)[1]', 'varchar(max)')
		WHEN 'BC-2012' THEN '2012 NC Building Code'
		WHEN 'BC-2015' THEN '2015 NC Existing Building Code'
		WHEN 'BC-2018' THEN '2018 NC Building Code'
		WHEN 'EBC-2018' THEN '2018 NC Existing Building Code'
		ELSE ''
		END AS [BuildingCode]

	   ,CASE
		WHEN app_form_xml.value('(//chkUpfit)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [TypeOfWorkUpfit]
		,CASE
		WHEN app_form_xml.value('(//chkRenovation)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [TypeOfWorkPrevOccupiedBldg]
		,CASE
		WHEN app_form_xml.value('(//chkAddition)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [TypeOfWorkAddition]
		,CASE
		WHEN app_form_xml.value('(//chkNewFull)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [TypeOfWorkNewConFull]
		,CASE
		WHEN app_form_xml.value('(//chkNewFootFound)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [TypeOfWorkNewConFootFound]
		,CASE
		WHEN app_form_xml.value('(//chkNewShellFootFound)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [TypeOfWorkNewConShellFootFound]
     
	 	,CASE
		WHEN app_form_xml.value('(//chkNewShellFootFoundApproved)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [TypeOfWorkNewConShellFootFoundPrev]
      
	 	,CASE
		WHEN app_form_xml.value('(//chkNewShellCoreFootFound)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [TypeOfWorkNewConSellFootFoundCore]
    
	 	,CASE
		WHEN app_form_xml.value('(//chkNewShellCoreFootFoundApproved)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [TypeOfWorkNewConsShellFootFoundCorePrev]
     
	 	,CASE
		WHEN app_form_xml.value('(//chkProfCertification)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [TypeOfWorkProCert]
     
	 	,CASE
		WHEN app_form_xml.value('(//chkChangeOfUse)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [TypeOfWorkChangeOfUse]
     
     ,CASE
		WHEN app_form_xml.value('(//chkDayCare)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [TypeOfWorkDayCare]
	 ,CASE
		WHEN app_form_xml.value('(//chkPreEngMetalBldg)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [TypeOfWorkPreEngMetalBldg]
     ,LTRIM(RTRIM(app_form_xml.value('(//rblPreEngMetalBldgOptions)[1]', 'varchar(max)'))) TypeOfWorkPreEngMetalBldgOption 

	 ,app_form_xml.value('(//chkDMDesignBidBuild)[1]', 'varchar(max)') ProjectDeliveryMethodDesignBidBuild
     ,app_form_xml.value('(//chkDMDesignBuild)[1]', 'varchar(max)') ProjectDeliveryMethodDesignBuild
	 ,app_form_xml.value('(//chkDMDesignBuildArchitectural)[1]', 'varchar(max)') PDMDisciplinesDesignBuildArchStruct
     ,app_form_xml.value('(//chkDMDesignBuildMechanical)[1]', 'varchar(max)') PDMDisciplinesDesignBuildMech
     ,app_form_xml.value('(//chkDMDesignBuildElectrical)[1]', 'varchar(max)') PDMDisciplinesDesignBuildElec
     ,app_form_xml.value('(//chkDMDesignBuildPlumbing)[1]', 'varchar(max)') PDMDisciplinesDesignBuildPlumb
	 ,app_form_xml.value('(//chkDMDesignAssist)[1]', 'varchar(max)') ProjectDeliveryMethodDesignAssist
	 ,app_form_xml.value('(//chkDMDesignAssistArchitectural)[1]', 'varchar(max)') PDMDisciplinesDesignAssistArchStruct
     ,app_form_xml.value('(//chkDMDesignAssistMechanical)[1]', 'varchar(max)') PDMDisciplinesDesignAssistMech
     ,app_form_xml.value('(//chkDMDesignAssistElectrical)[1]', 'varchar(max)') PDMDisciplinesDesignAssistElec
     ,app_form_xml.value('(//chkDMDesignAssistPlumbing)[1]', 'varchar(max)') PDMDisciplinesDesignAssistPlumb
	 ,app_form_xml.value('(//chkDMCMOwnerAgent)[1]', 'varchar(max)') ProjectDeliveryMethodCMOwnerAgent
	 ,app_form_xml.value('(//chkDMIPDorVariation)[1]', 'varchar(max)') ProjectDeliveryMethodIPDorVariation
	 ,app_form_xml.value('(//chkDMFastTrack)[1]', 'varchar(max)') ProjectDeliveryMethodFastTrack
     ,app_form_xml.value('(//chkDMOther)[1]', 'varchar(max)') ProjectDeliveryMethodOther
	 ,app_form_xml.value('(//txtDMOtherDescription )[1]', 'varchar(max)') PDMOtherDescription
	 ,app_form_xml.value('(//rblProducedUsingBIM)[1]', 'varchar(max)') ProjectIsBIM
	 ,app_form_xml.value('(//chkBIMArchitectural)[1]', 'varchar(max)') BIMDisciplinesArch
	 ,app_form_xml.value('(//chkBIMStructural)[1]', 'varchar(max)') BIMDisciplinesStruct
     ,app_form_xml.value('(//chkBIMMechanical)[1]', 'varchar(max)') BIMDisciplinesMech
     ,app_form_xml.value('(//chkBIMElectrical)[1]', 'varchar(max)') BIMDisciplinesElec
     ,app_form_xml.value('(//chkBIMPlumbing)[1]', 'varchar(max)') BIMDisciplinesPlumb
     ,app_form_xml.value('(//txtAgenda)[1]', 'varchar(max)') Agenda
	 ,app_form_xml.value('(//txtNumberOfAttendees)[1]', 'varchar(max)') NumberOfAttendees
	 ,app_form_xml.value('(//dtmMeetingRequestFrom)[1]', 'varchar(max)') RequestedBeginDateRange
     ,app_form_xml.value('(//dtmMeetingRequestTo)[1]', 'varchar(max)') RequestedEndDateRange
	 ,app_form_xml.value('(//rblPreliminaryAlreadyDone)[1]', 'varchar(max)') PreviousPrelimReview
	 ,app_form_xml.value('(//txtPreviousProjectNumber)[1]', 'varchar(max)') PreviousPrelimProjectNumber
	 ,app_form_xml.value('(//chkBuildingReviewer)[1]', 'varchar(max)') PrelimTradeBuilding
     ,LTRIM(RTRIM(app_form_xml.value('(//txtBuildingReviewer)[1]', 'varchar(max)'))) PrelimTradeBuildingReviewers
     ,app_form_xml.value('(//chkElectricalReviewer)[1]', 'varchar(max)') PrelimTradeElectrical
     ,LTRIM(RTRIM(app_form_xml.value('(//txtElectricalReviewer)[1]', 'varchar(max)'))) PrelimTradeElectricalReviewers
     ,app_form_xml.value('(//chkMechanicalReviewer)[1]', 'varchar(max)') PrelimTradeMechanical
     ,LTRIM(RTRIM(app_form_xml.value('(//txtMechanicalReviewer)[1]', 'varchar(max)'))) PrelimTradeMechanicalReviewers
     ,app_form_xml.value('(//chkPlumbingReviewer)[1]', 'varchar(max)') PrelimTradePlumbing
     ,LTRIM(RTRIM(app_form_xml.value('(//txtPlumbingReviewer)[1]', 'varchar(max)'))) PrelimTradePlumbingReviewers
     ,app_form_xml.value('(//chkZoningReviewer)[1]', 'varchar(max)') PrelimTradeZoning
     ,LTRIM(RTRIM(app_form_xml.value('(//txtZoningReviewer)[1]', 'varchar(max)'))) PrelimTradeZoningReviewers
     ,app_form_xml.value('(//chkFireReviewer)[1]', 'varchar(max)') PrelimTradeFire
     ,LTRIM(RTRIM(app_form_xml.value('(//txtFireReviewer)[1]', 'varchar(max)'))) PrelimTradeFireReviewers
     ,app_form_xml.value('(//chkHealthReviewer)[1]', 'varchar(max)') PrelimTradeHealth
     ,LTRIM(RTRIM(app_form_xml.value('(//txtHealthReviewer)[1]', 'varchar(max)'))) PrelimTradeHealthReviewers
     ,app_form_xml.value('(//chkBackflowReviewer)[1]', 'varchar(max)') PrelimTradeBackflow
     ,LTRIM(RTRIM(app_form_xml.value('(//txtBackflowReviewer)[1]', 'varchar(max)'))) PrelimTradeBackflowReviewers
     ,app_form_xml.value('(//rblIncludesAfforableOrWorkforceHousing)[1]', 'varchar(max)') IncludesAfforableOrWorkforceHousing
     ,app_form_xml.value('(//txtAfforableHousingUnits)[1]', 'varchar(max)') AfforableHousingUnits
     ,app_form_xml.value('(//txtWorkforceHousingUnits)[1]', 'varchar(max)') WorkforceHousingUnits
    
INTO epm_conv.CE_PL_Custom_Fields

FROM	[dbo].[tb_project_current_status] p
		LEFT JOIN [epm_conv].[CE_Projects_Accela_Hierarchy] h ON h.PERMITNUM = p.project_number
		WHERE p.txt_review_type_code  = 'PL' and app_form_xml is not null




