/****** EPM Application Form Data Extract  ******/
USE tst_EPM;

SELECT
	p.project_number PERMITNUM
	 ,h.TT_Record
	 ,h.review_type ReviewType
	--,CASE app_form_xml.value('(//rblReviewType)[1]', 'varchar(max)')
	--    WHEN 'OS' THEN 'Commercial Large'
	--	WHEN 'HC' THEN 'Commercial Large'
	--	WHEN 'MP' THEN 'Commercial Large'
	--	WHEN 'PS' THEN 'Commercial Large'
	--	WHEN 'ER' THEN 'Commercial Large'
	--	ELSE ''
	--	END AS [ReviewType]

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

    ,h.express ExpressReview

	,app_form_xml.value('(//dtmDrawingReadyDate)[1]', 'varchar(max)') DrawingsReadyDate
	

	,CASE app_form_xml.value('(//rblPaidBy)[1]', 'varchar(max)')
		WHEN 'CHECK' THEN 'Check'
		WHEN 'LOA' THEN 'LOA'
		WHEN 'CREDIT CARD' THEN 'Credit Card'
		ELSE ''
		END AS [PrepaidFeePaymentType]

	,app_form_xml.value('(//txtAccountNumber)[1]', 'varchar(max)') AccountNumber

	,CASE app_form_xml.value('(//rblIncludesAfforableOrWorkforceHousing)[1]', 'varchar(max)')
		WHEN 'Y' THEN 'Yes'
		WHEN 'N' THEN 'No'
		ELSE ''
		END AS [IncludesAfforableOrWorkforceHousing]

    ,app_form_xml.value('(//txtAfforableHousingUnits)[1]', 'varchar(max)') AfforableHousingUnits
    ,app_form_xml.value('(//txtWorkforceHousingUnits)[1]', 'varchar(max)') WorkforceHousingUnits

    ,app_form_xml.value('(//rblRequiresAsbestosRemoval)[1]', 'varchar(max)') AsbestosRemoval
    ,app_form_xml.value('(//rblAsbestosSurveyConducted)[1]', 'varchar(max)') AsbestosSurveyConducted
	,app_form_xml.value('(//rblDemolitionPerformed)[1]', 'varchar(max)') StructuralDemolition
    
    ,app_form_xml.value('(//rblBuildingOwnedByCharOrMeck)[1]', 'varchar(max)') CityCountyOwnedBldg
    ,app_form_xml.value('(//rblMoreThan20000SqFt)[1]', 'varchar(max)') CityCountyOwnedBldgSQFt
    ,app_form_xml.value('(//rblPreliminaryReviewDone)[1]', 'varchar(max)') PreliminaryReviewDone
    ,app_form_xml.value('(//txtPreliminaryProjectNumber)[1]', 'varchar(max)') PreliminaryProjectNumber
    ,app_form_xml.value('(//rblHasCondProjNum)[1]', 'varchar(max)') ConditionalPermitApproval
    ,app_form_xml.value('(//txtCondProjNum)[1]', 'varchar(max)') ConditionalPermitProjectNumber
    ,app_form_xml.value('(//txtTotalSheets)[1]', 'varchar(max)') TotalNumOfSheets

	,CAST(app_form_xml.value('(//txtCostEstimation)[1]', 'varchar(max)') AS MONEY) ConstructionCost
    ,CAST(app_form_xml.value('(//txtCostEstimationEquipment)[1]', 'varchar(max)') AS MONEY) EquipmentCost
    ,CAST(app_form_xml.value('(//txtCostEstimationTotal)[1]', 'varchar(max)') AS MONEY) ProjectCostTotal

	,LTRIM(RTRIM((select txt_worktype_desc from ctb_worktypes_defs where txt_worktype_code = app_form_xml.value('(//rblWorkType)[1]', 'varchar(max)')))) TypeOfWork

    ,CASE
        WHEN app_form_xml.value('(//chkProfCertification)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [ProfessionalCertification]

	,CASE
        WHEN app_form_xml.value('(//chkChangeOfUse)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [ChangeOfUseForZoning]

	,CASE
        WHEN app_form_xml.value('(//chkDayCare)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [DayCare]

	,CASE
        WHEN app_form_xml.value('(//chkPreEngMetalBldg)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [PreEngMetalBldg]

    ,LTRIM(RTRIM(app_form_xml.value('(//rblPreEngMetalBldgOptions)[1]', 'varchar(max)'))) PreEngMetalBldgOption 

	,CASE 
        WHEN app_form_xml.value('(//chkDownsizingSuite)[1]', 'varchar(max)') is null THEN 'UNCHECKED'
		WHEN app_form_xml.value('(//chkDownsizingSuite)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [DownsizingSuite]

    ,LTRIM(RTRIM(app_form_xml.value('(//rblDownsizingSuiteOptions)[1]', 'varchar(max)'))) DownsizingSuiteOptions


	,CASE app_form_xml.value('(//rbListPhasedConstruction)[1]', 'varchar(max)')
		WHEN 'True' THEN 'Yes'
		WHEN 'False' THEN 'No'
		ELSE ''
		END AS [PhasedConstruction]

    ,app_form_xml.value('(//rblFirstPhase)[1]', 'varchar(max)') FirstPhase
    ,app_form_xml.value('(//txtOrigProjNum)[1]', 'varchar(max)') OriginalProjectNumber
    ,app_form_xml.value('(//rblFirstUpfit)[1]', 'varchar(max)') FirstUpfit
    ,app_form_xml.value('(//txtShellProjNum)[1]', 'varchar(max)') ShellProjectNumber

    ,LTRIM(RTRIM((select txt_backflow_desc from ctb_backflow_defs where txt_backflow_code = app_form_xml.value('(//rblUndergroundPiping)[1]', 'varchar(max)')))) BFUndergroundPiping
    ,LTRIM(RTRIM((select txt_backflow_desc from ctb_backflow_defs where txt_backflow_code = app_form_xml.value('(//rblSprinklerPiping)[1]', 'varchar(max)')))) BFSprinklerPiping

	,app_form_xml.value('(//rblCMUDPreventer)[1]', 'varchar(max)') BFPreventer
	,app_form_xml.value('(//rblExtendsPublicWaterOrSanitarySewerSystem)[1]', 'varchar(max)') BFExtendPublicWaterSewer
    ,app_form_xml.value('(//rblGradeModificationWithinCharlotteEasement)[1]', 'varchar(max)') BFGradeChangeinEasement
    ,app_form_xml.value('(//rblEncroachmentInCharlotteEasement)[1]', 'varchar(max)') BFProposedEncroachmentInEasement
	
	,CASE app_form_xml.value('(//frmPrimaryOccupancy/rblOccupancyType)[1]', 'varchar(max)')
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
		END AS [PrimaryOccupancy]

	  --,app_form_xml.value('(//frmPrimaryOccupancy/rblOccupancyType)[1]', 'varchar(max)') OccupancyCode

	   ,CASE app_form_xml.value('(//frmPrimaryOccupancy/frmFactoryOrIndustryDtl/rblFactoryOrIndustryType)[1]', 'varchar(max)') 
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
END AS [OccupancyCode]  


	,CASE
        WHEN app_form_xml.value('(//frmPrimaryOccupancy/frmStorageDtl/chkStorageOpenParking)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [PrimaryOpenParkingGarage]

	,CASE
        WHEN app_form_xml.value('(//frmPrimaryOccupancy/frmStorageDtl/chkStorageHighPiled)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [PrimaryHighPiled]

	,CASE
        WHEN app_form_xml.value('(//frmPrimaryOccupancy/frmStorageDtl/chkStorageEnclosedParking)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [PrimaryEnclosedParkingGarage]

	,CASE
        WHEN app_form_xml.value('(//frmPrimaryOccupancy/frmStorageDtl/chkStorageRepairParking)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [PrimaryRepairParkingGarage]

	,app_form_xml.value('(//frmPrimaryOccupancy/frmStorageDtl/txtStorageType)[1]', 'varchar(max)') PrimaryIdentifyWhatIsBeingStored

	,CASE app_form_xml.value('(//rblSpecOcc)[1]', 'varchar(max)')
        WHEN 'S' THEN 'Yes'
		WHEN 'NA' THEN 'No'
		ELSE ''
		END AS [SpecialProvisions]

 ,CASE 
        when app_form_xml.value('(//chkSpecOcc5092)[1]', 'varchar(max)') = '1' THEN '509.2'
	    when app_form_xml.value('(//chkSpecOcc5093)[1]', 'varchar(max)') = '2' THEN '509.3'
		when app_form_xml.value('(//chkSpecOcc5094)[1]', 'varchar(max)') = '3' THEN '509.4'
		when app_form_xml.value('(//chkSpecOcc5095)[1]', 'varchar(max)') = '4' THEN '509.5'
		when app_form_xml.value('(//chkSpecOcc5096)[1]', 'varchar(max)') = '5' THEN '509.6'
		when app_form_xml.value('(//chkSpecOcc5097)[1]', 'varchar(max)') = '6' THEN '509.7'
		when app_form_xml.value('(//chkSpecOcc5098)[1]', 'varchar(max)') = '7' THEN '509.8'
		ELSE ''
	END AS [SpecialProvisionsType]
 
   ,CASE app_form_xml.value('(//rblMixOcc)[1]', 'varchar(max)')
        WHEN 'M' THEN 'Yes'
		WHEN 'NA' THEN 'No'
		ELSE ''
		END AS [IsMixedUse]

	,CASE app_form_xml.value('(//rblMixOccDtl)[1]', 'varchar(max)') 
	    WHEN '100' THEN 'Non Separated Mixed Occupancy'
		WHEN '101' THEN 'Separated Mixed Occupancy'
		ELSE ''
	END AS [MixedUseSeperatednonSeparated]
	
	,app_form_xml.value('(//txtSeperation)[1]', 'varchar(max)') SeperationHours
	,app_form_xml.value('(//txtException)[1]', 'varchar(max)') SeparationException


	,CASE app_form_xml.value('(//frmSecondaryOccupancy/rblOccupancyType)[1]', 'varchar(max)')
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
		END AS [SecondaryOccupancy]
	
 ,CASE app_form_xml.value('(//frmSecondaryOccupancy/frmFactoryOrIndustryDtl/rblFactoryOrIndustryType)[1]', 'varchar(max)') 
        WHEN 'F1' THEN 'F1 * Factory/Industrial - Moderate Hazard'
		WHEN 'F2' THEN 'F2 * Factory/Industrial - Low Hazard'
 ELSE  
	 
    CASE app_form_xml.value('(//frmSecondaryOccupancy/frmStorageDtl/rblStorageType)[1]', 'varchar(max)') 
        WHEN 'S1' THEN 'S1 * Storage - Moderate Hazard'
		WHEN 'S2' THEN 'S2 * Storage - Low Hazard'
	ELSE  
	 
	 CASE app_form_xml.value('(//frmSecondaryOccupancy/frmAssemblyDtl/rblAssemblyType)[1]', 'varchar(max)') 
        WHEN 'A1' THEN 'A1 * Assembly - Theater w/o Stage'
		WHEN 'A2' THEN 'A2 * Assembly - Restaurants, Bars, Banquet Halls'
	    WHEN 'A3' THEN 'A3C * Assembly – Common Assemblies'
		WHEN 'A4' THEN 'A4 * Assembly – Indoor Arena, Skating Rink, Tennis Court'
	    WHEN 'A5' THEN 'A5 * Assembly – Outdoor Stadium, Bleacher, Grandstand'
	 ELSE  

	 CASE app_form_xml.value('(//frmSecondaryOccupancy/frmHighHazardDtl/rblHighHazardType)[1]', 'varchar(max)') 
        WHEN 'H1' THEN 'H1 * High Hazard – Explosives'
		WHEN 'H2' THEN 'H2 * High Hazard – Deflagration'
	    WHEN 'H3' THEN 'H3 * High Hazard – Readily Combustible'
		WHEN 'H4' THEN 'H4 * High Hazard – Health Hazard'
	    WHEN 'H5' THEN 'H5 * High Hazard - HPM'
	 ELSE  

	  CASE app_form_xml.value('(//frmSecondaryOccupancy/frmResedentialDtl/rblResedentialType)[1]', 'varchar(max)')
        WHEN 'R1' THEN 'R1 * Residential – Hotels'
		WHEN 'R2' THEN 'R2 * Residential - Multiple Family'
	    WHEN 'R3' THEN 'R3 * Residential - Single Family'
		WHEN 'R4' THEN 'R4 * Residential - Care/Assisted Living Facilities, Condition 1 (Ambulatory)'
	 ELSE  
	 
	 CASE app_form_xml.value('(//frmSecondaryOccupancy/frmInstitutionalDtl/rblInstitutionalType)[1]', 'varchar(max)') 
        WHEN 'I1' THEN 'I1 * Institutional - Supervised Environment, Condition 1 (Ambulatory)'
		WHEN 'I2' THEN 'I2H * Institutional - Incapacitated – Hospital, Full Nursing & Medical Treatment'
	    WHEN 'I3' THEN 'I3 * Institutional - Restrained'
		WHEN 'I4' THEN 'I4 * Institutional - Day Care'
		WHEN 'I-3 Use Condition' THEN 'I3 * Institutional - Restrained'
	 ELSE  

   CASE 
		  WHEN app_form_xml.value('(//frmSecondaryOccupancy/rblOccupancyType)[1]', 'varchar(max)') = 'BS' THEN 'B * Business'
		  WHEN app_form_xml.value('(//frmSecondaryOccupancy/rblOccupancyType)[1]', 'varchar(max)') = 'ED' THEN 'E * Education'
		  WHEN app_form_xml.value('(//frmSecondaryOccupancy/rblOccupancyType)[1]', 'varchar(max)') = 'MR' THEN 'M * Mercantile'
		  WHEN app_form_xml.value('(//frmSecondaryOccupancy/rblOccupancyType)[1]', 'varchar(max)') = 'ED' THEN 'E * Education'
          WHEN app_form_xml.value('(//frmSecondaryOccupancy/rblOccupancyType)[1]', 'varchar(max)') = 'UM' THEN 'U * Utility'
ELSE ''
END  
END
END
END
END
END
END AS [SecondaryOccupancyCode]  

 
	,CASE
        WHEN app_form_xml.value('(//frmSecondaryOccupancy/frmStorageDtl/chkStorageOpenParking)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [SecondaryOpenParkingGarage]

	,CASE
        WHEN app_form_xml.value('(//frmSecondaryOccupancy/frmStorageDtl/chkStorageHighPiled)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [SecondaryHighPiled]

	,CASE
        WHEN app_form_xml.value('(//frmSecondaryOccupancy/frmStorageDtl/chkStorageEnclosedParking)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [SecondaryEnclosedParkingGarage]

	,CASE
        WHEN app_form_xml.value('(//frmSecondaryOccupancy/frmStorageDtl/chkStorageRepairParking)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [SecondaryRepairParkingGarage]

	,app_form_xml.value('(//frmSecondaryOccupancy/frmStorageDtl/txtStorageType)[1]', 'varchar(max)') SecondaryIdentifyWhatIsBeingStored

	,CASE(select txt_constr_type_desc from ctb_constr_types_defs where txt_constr_types_code = app_form_xml.value('(//rblConstrType)[1]', 'varchar(max)')) 
	    WHEN 'I-A' THEN '1A * NONCOMBUSTIBLE/PROTECTED'
		WHEN 'I-B' THEN '1B * NONCOMBUSTIBLE/UNPROTECTED'
		WHEN 'II-A' THEN '2A * NONCOMBUSTIBLE/PROTECTED'
		WHEN 'II-B' THEN '2B * NONCOMBUSTIBLE/UNPROTECTED'
		WHEN 'III-A' THEN '3A * NONCOMBUSTIBLE WALLS/PROTECTED'
		WHEN 'III-B' THEN '3B * NONCOMBUSTIBLE WALLS/UNPROTECTED'
		WHEN 'IV' THEN '4 * HEAVY TIMBER'
		WHEN 'V-A' THEN '5A * WOOD FRAME/PROTECTED'
		WHEN 'V-B' THEN '5B * WOOD FRAME/UNPROTECTED'
		ELSE ''
		END AS [ConstructionType]
	
	,app_form_xml.value('(//txtNumBldg)[1]', 'varchar(max)') NumBldgsForReview
	,cast(cast(app_form_xml.value('(//txtSqFtReview)[1]', 'varchar(max)') as money)as int) SqFtToReview
	 
	,CAST(app_form_xml.value('(//txtBldgHeight)[1]', 'varchar(max)') AS NUMERIC) BuildingHeight
	,cast(cast(app_form_xml.value('(//txtSqFtArea)[1]', 'varchar(max)') as money)as int) GrossSquareFeet
	,app_form_xml.value('(//texNumOfStories)[1]', 'varchar(max)') NumStoriesOverallBldg

	,app_form_xml.value('(//rblHighRise)[1]', 'varchar(max)') HighRise

	,app_form_xml.value('(//chkPrjSustainableDsgn)[1]', 'varchar(max)') SustainableDesign

	,CASE
        WHEN app_form_xml.value('(//chkSpecialInsp)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [SpecialInspections]

	,CASE
        WHEN app_form_xml.value('(//chkMezzanine)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [Mezzanine]

	,CASE
        WHEN app_form_xml.value('(//chkUPS)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [UPS]

	,cast(cast(app_form_xml.value('(//txtIncrAmount)[1]', 'varchar(max)') as money)as int) IncrUsableSpace
	  
	,CASE
        WHEN app_form_xml.value('(//chkIncInUsableSqFtg)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [IncrUsableSqFt ]

	,CASE
        WHEN app_form_xml.value('(//chkGenerators)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [Generators]

	,CASE
        WHEN app_form_xml.value('(//chkBasement)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [HasBasement]
	
	,CASE
        WHEN app_form_xml.value('(//chkElevators)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [Elevators]

	,CASE
        WHEN app_form_xml.value('(//chkCranes)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [Cranes]

	,CASE
        WHEN app_form_xml.value('(//chkUnlimitedArea)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [UnlimitedArea]

	,app_form_xml.value('(//txtUnlimitedPerCodeSection)[1]', 'varchar(max)') UnlimitedPerCodeSection

	 
	,cast(cast(app_form_xml.value('(//txtSqFtExistingTotal)[1]', 'varchar(max)') as money)as int) SqFtExistingTotal
	,cast(cast(app_form_xml.value('(//txtSqFtRenovationTotal)[1]', 'varchar(max)') as money)as int) TotalRenovatedArea
	,cast(cast(app_form_xml.value('(//txtSqFtNewTotal)[1]', 'varchar(max)')  as money)as int) TotalNewHeatedSqFeet

	,CASE app_form_xml.value('(//rblIncludesAlarmAndSprinklerDwg)[1]', 'varchar(max)') 
	    WHEN 'FS_Y' THEN 'Yes'
		WHEN 'FS_N' THEN 'No'
		ELSE ''
	END AS [FireDrawingsIncluded]

	,CASE app_form_xml.value('(//rblBuildingSprinklered)[1]', 'varchar(max)') 
	    WHEN 'SP_Y' THEN 'Yes'
		WHEN 'SP_N' THEN 'No'
		ELSE ''
	END AS [FireBuildingSprinkled]

	,CASE
        WHEN (select txt_fire_dtls_desc from ctb_fire_dtls_defs where cast(fire_dtls_code as varchar) = app_form_xml.value('(//chkNFPAType13)[1]', 'varchar(max)')) = '13' THEN 'CHECKED'
		ELSE 'UNCHECKED'
		END AS [FirekNFPAType13]

	,CASE
        WHEN (select txt_fire_dtls_desc from ctb_fire_dtls_defs where cast(fire_dtls_code as varchar) = app_form_xml.value('(//chkNFPAType13R)[1]', 'varchar(max)')) = '13R' THEN 'CHECKED'
		ELSE 'UNCHECKED'
		END AS [FireNFPAType13R]

	,CASE
        WHEN (select txt_fire_dtls_desc from ctb_fire_dtls_defs where cast(fire_dtls_code as varchar) = app_form_xml.value('(//chkNFPAType13D)[1]', 'varchar(max)')) = '13D' THEN 'CHECKED'
		ELSE 'UNCHECKED'
		END AS [FireNFPAType13D]

	,LTRIM(RTRIM((select txt_fire_dtls_desc from ctb_fire_dtls_defs where cast(fire_dtls_code as varchar) = app_form_xml.value('(//rblSprinklerType)[1]', 'varchar(max)')))) FireNFPANewOrExisting
	
	,CASE app_form_xml.value('(//rblStandpipe)[1]', 'varchar(max)') 
	    WHEN 'ST_Y' THEN 'Yes'
		WHEN 'ST_N' THEN 'No'
		ELSE ''
	END AS [FireStandpipe]

	,CASE
        WHEN (select txt_fire_dtls_desc from ctb_fire_dtls_defs where cast(fire_dtls_code as varchar) = app_form_xml.value('(//chkStandpipeClassI)[1]', 'varchar(max)')) = 'I' THEN 'CHECKED'
		ELSE 'UNCHECKED'
		END AS [FireStandpipeClassI]

	,CASE
        WHEN (select txt_fire_dtls_desc from ctb_fire_dtls_defs where cast(fire_dtls_code as varchar) = app_form_xml.value('(//chkStandpipeClassII)[1]', 'varchar(max)')) = 'II' THEN 'CHECKED'
		ELSE 'UNCHECKED'
		END AS [FireStandpipeClassII]

	,CASE
        WHEN (select txt_fire_dtls_desc from ctb_fire_dtls_defs where cast(fire_dtls_code as varchar) = app_form_xml.value('(//chkStandpipeClassIII)[1]', 'varchar(max)')) = 'III' THEN 'CHECKED'
		ELSE 'UNCHECKED'
		END AS [FireStandpipeClassIII]

	,CASE
        WHEN (select txt_fire_dtls_desc from ctb_fire_dtls_defs where cast(fire_dtls_code as varchar) = app_form_xml.value('(//chkStandpipeClassWet)[1]', 'varchar(max)')) = 'Wet' THEN 'CHECKED'
		ELSE 'UNCHECKED'
		END AS [FireStandpipeClassWet]
	
	,CASE
        WHEN (select txt_fire_dtls_desc from ctb_fire_dtls_defs where cast(fire_dtls_code as varchar) = app_form_xml.value('(//chkStandpipeClassDry)[1]', 'varchar(max)')) = 'Dry' THEN 'CHECKED'
		ELSE 'UNCHECKED'
		END AS [FireStandpipeClassDry]

	,CASE app_form_xml.value('(//rblFirePump)[1]', 'varchar(max)')
	    WHEN 'FP_Y' THEN 'Yes'
		WHEN 'FP_N' THEN 'No'
		ELSE ''
	END AS [FireFirePump]

	,LTRIM(RTRIM((select txt_fire_dtls_desc from ctb_fire_dtls_defs where cast(fire_dtls_code as varchar) = app_form_xml.value('(//rblFirePumpType)[1]', 'varchar(max)')))) FirePumpNewOrExisting
	
	,CASE app_form_xml.value('(//rblElevator)[1]', 'varchar(max)')
	    WHEN 'EL_Y' THEN 'Yes'
		WHEN 'EL_N' THEN 'No'
		ELSE ''
	END AS [FireElevator]

	,CASE app_form_xml.value('(//rblSmokeDetection)[1]', 'varchar(max)')
	    WHEN 'SD_Y' THEN 'Yes'
		WHEN 'SD_N' THEN 'No'
		ELSE ''
	END AS [FireSmokeDetector]

	,CASE app_form_xml.value('(//rblFireAlarm)[1]', 'varchar(max)')
	    WHEN 'FA_Y' THEN 'Yes'
		WHEN 'FA_N' THEN 'No'
		ELSE ''
	END AS [FireFireAlarm ]

	,CASE
        WHEN app_form_xml.value('(//chkExistingSepticTank)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
		ELSE 'UNCHECKED'
		END AS [ExistingSepticTank]

	,CASE
        WHEN app_form_xml.value('(//chkExistingPublicSewer)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
		ELSE 'UNCHECKED'
		END AS [ExistingPublicSewer]

	,CASE
        WHEN app_form_xml.value('(//chkNewSepticTank)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
		ELSE 'UNCHECKED'
		END AS [NewSepticTank]

	,CASE
        WHEN app_form_xml.value('(//chkNewPublicSewer)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
		ELSE 'UNCHECKED'
		END AS [NewPublicSewer]

	,CASE
        WHEN app_form_xml.value('(//chkExistingWell)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
		ELSE 'UNCHECKED'
		END AS [ExistingWell]

	,CASE
        WHEN app_form_xml.value('(//chkExistingPublicWater)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
		ELSE 'UNCHECKED'
		END AS [ExistingPublicWater]

	,CASE
        WHEN app_form_xml.value('(//chkNewWell)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
		ELSE 'UNCHECKED'
		END AS [NewWell]

	,CASE
        WHEN app_form_xml.value('(//chkNewPublicWater)[1]', 'varchar(max)') = 'TRUE' THEN 'CHECKED'
		ELSE 'UNCHECKED'
		END AS [NewPublicWater]

    ,app_form_xml.value('(//txtPreviousBiz)[1]', 'varchar(max)') ZoningPreviousBusiness
    ,app_form_xml.value('(//txtPropsedBiz)[1]', 'varchar(max)') ZoningProposedBusiness
    ,app_form_xml.value('(//txtZoning)[1]', 'varchar(max)') ZoningZoningCode
      
	,CASE
        WHEN app_form_xml.value('(//chkRestaurant)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [Restaurant]

	,app_form_xml.value('(//txtRestaurantCapacity)[1]', 'varchar(max)') RestaurantCapacity
	
	 ,CASE  
	   when 
	        app_form_xml.value('(//chkRestaurantUtensilReusable)[1]', 'varchar(max)') != ''
			and 
			app_form_xml.value('(//chkRestaurantUtensilDisposable)[1]', 'varchar(max)') != ''
		  then 'Both'
	   when 
	        app_form_xml.value('(//chkRestaurantUtensilReusable)[1]', 'varchar(max)') != ''
			and 
			app_form_xml.value('(//chkRestaurantUtensilDisposable)[1]', 'varchar(max)') = ''
		  then  'Reusable'
	   when 
	        app_form_xml.value('(//chkRestaurantUtensilReusable)[1]', 'varchar(max)') = ''
			and 
			app_form_xml.value('(//chkRestaurantUtensilDisposable)[1]', 'varchar(max)') != ''
		  then  'Disposable'
		  
		  end RestaurantUtensilType

	,CASE
        WHEN app_form_xml.value('(//chkBarSvc)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [BarService]

	,app_form_xml.value('(//txtBarSvcCapacity)[1]', 'varchar(max)')  HDBarServiceCapacity
	
	,CASE  
	   when 
	        app_form_xml.value('(//chkBarSvcUtensilReusable)[1]', 'varchar(max)') != ''
			and 
			app_form_xml.value('(//chkBarSvcUtensilDisposable)[1]', 'varchar(max)') != ''
		  then 'Both'
	   when 
	        app_form_xml.value('(//chkBarSvcUtensilReusable)[1]', 'varchar(max)') != ''
			and 
			app_form_xml.value('(//chkBarSvcUtensilDisposable)[1]', 'varchar(max)') = ''
		  then  'Reusable'
		  when 
	        app_form_xml.value('(//chkBarSvcUtensilReusable)[1]', 'varchar(max)') = ''
			and 
			app_form_xml.value('(//chkBarSvcUtensilDisposable)[1]', 'varchar(max)') != ''
		  then  'Disposable'
		  
		  end HDBarServiceUtensilType

	,CASE
        WHEN app_form_xml.value('(//chkSeafood)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [HDSeafood]

	,app_form_xml.value('(//txtSeafoodCapacity)[1]', 'varchar(max)')  HDSeafoodCapacity
	
	,CASE  
	   when 
	        app_form_xml.value('(//chkSeafoodUtensilReusable)[1]', 'varchar(max)') != ''
			and 
			app_form_xml.value('(//chkSeafoodUtensilDisposable)[1]', 'varchar(max)') != ''
		  then 'Both'
	   when 
	        app_form_xml.value('(//chkSeafoodUtensilReusable)[1]', 'varchar(max)') != ''
			and 
			app_form_xml.value('(//chkSeafoodUtensilDisposable)[1]', 'varchar(max)') = ''
		  then  'Reusable'
		  when 
	        app_form_xml.value('(//chkSeafoodUtensilReusable)[1]', 'varchar(max)') = ''
			and 
			app_form_xml.value('(//chkSeafoodUtensilDisposable)[1]', 'varchar(max)') != ''
		  then  'Disposable'
		  
		  end HDSeafoodUtensilType

	,CASE
        WHEN app_form_xml.value('(//chkLodging)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [HDLodging]

	,CASE
        WHEN app_form_xml.value('(//chkAdultDayCare)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [HDAdultDayCare]

	,app_form_xml.value('(//txrNumAdults)[1]', 'varchar(max)') HDAdultDayCareCapacity

	,CASE
        WHEN app_form_xml.value('(//chkMeatMarket)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [HDMeatMarket]

	,CASE
        WHEN app_form_xml.value('(//chkWaterRecreation)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [HDWaterRecreation_Pool]

	,CASE
        WHEN app_form_xml.value('(//chkChildDayCare)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [HDChildDayCare]

	,app_form_xml.value('(//txtNumChildren)[1]', 'varchar(max)') HDChildDayCareCapacity

	,CASE
        WHEN app_form_xml.value('(//chkOther)[1]', 'varchar(max)') = '' THEN 'UNCHECKED'
		ELSE 'CHECKED'
		END AS [HDOther]

	,app_form_xml.value('(//txtOtherDesc)[1]', 'varchar(max)') HDOtherDescription

    ,app_form_xml.value('(//chkAddingImperviousArea)[1]', 'varchar(max)') CityAddingImperviousArea
    ,app_form_xml.value('(//chkChangingDrivewayEtc)[1]', 'varchar(max)') CityChangingDrivewayEtc
	,app_form_xml.value('(//chkAddingTreePlanting)[1]', 'varchar(max)') CityAddingTreePlanting
	,app_form_xml.value('(//chkAddingTreeProtection)[1]', 'varchar(max)') CityAddingTreeProtection
	,app_form_xml.value('(//chkGradingMoreThanOneAcre)[1]', 'varchar(max)') CityGradingMoreThanOneAcre
	,app_form_xml.value('(//chkBuildingOver1000SqFt)[1]', 'varchar(max)') CityBuildingOver1000SqFt
	,app_form_xml.value('(//chkBuildingOver5PercentSqFt)[1]', 'varchar(max)') CityBuildingOver5PercentSqFt
	,app_form_xml.value('(//chkAdding11OrMoreParkingSpace)[1]', 'varchar(max)') CityAdding11OrMoreParkingSpace
	,app_form_xml.value('(//chkChangingFacadeOver10Percent)[1]', 'varchar(max)') CityChangingFacadeOver10Percent

	,CASE
        WHEN app_form_xml.value('(//chkZonedUrban)[1]', 'varchar(max)') = 'ZU' THEN 'CHECKED'
		WHEN app_form_xml.value('(//chkZonedUrban)[1]', 'varchar(max)') = 'Y' THEN 'CHECKED'
		ELSE 'UNCHECKED'
		END AS [CityZonedUrban]

	,CASE
        WHEN app_form_xml.value('(//chkPlannedMultiFamily)[1]', 'varchar(max)') = 'PM' THEN 'CHECKED'
		WHEN app_form_xml.value('(//chkPlannedMultiFamily)[1]', 'varchar(max)') = 'Y' THEN 'CHECKED'
		ELSE 'UNCHECKED'
		END AS [CityPlannedMultiFamily]

	,app_form_xml.value('(//chkAdjoinsPublicStreet)[1]', 'varchar(max)') CityAdjoinsPublicStreet
    ,app_form_xml.value('(//chkNewPublicOrPrivateStreet)[1]', 'varchar(max)') CityNewPublicOrPrivateStreet


    ,app_form_xml.value('(//txtProposedWork)[1]', 'varchar(max)') OverallScopeOfWorkDescription
	,app_form_xml.value('(//txtElecWrkScope)[1]', 'varchar(max)') ElecScopeOfWorkDescription
	,app_form_xml.value('(//txtMechWrkScope)[1]', 'varchar(max)') MechScopeOfWorkDescription
	,app_form_xml.value('(//txtPlumbWrkScope)[1]', 'varchar(max)') PlumScopeOfWorkDescription
	,app_form_xml.value('(//txtCivilWrkScope)[1]', 'varchar(max)') CivilScopeOfWorkDescription
	,f.fee_amt EstimatedFees
	 

INTO epm_conv.CE_Custom_Fields_Application

FROM	[dbo].[tb_project_current_status] p
		LEFT JOIN [epm_conv].[CE_Projects_Accela_Hierarchy] h ON h.PERMITNUM = p.project_number
		LEFT JOIN [dbo].[tb_project_fee] f ON f.project_number = p.project_number and f.id_fee_types = 2

WHERE txt_review_type_code not in ('PL','RT', 'WL')
      and app_form_xml is not null
	  --and p.project_number in ('393937','325350')
	  

    

    



		

	
