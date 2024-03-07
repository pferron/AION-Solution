/****** EPM RTAP Application Form Data Extract  ******/

USE tst_EPM;

SELECT
	p.project_number PERMITNUM
	 ,h.TT_Record
	,CASE app_form_xml.value('(//rblPropertyType)[1]', 'varchar(max)')
		WHEN 'FA' THEN '1-2 Family -=> 3.5 Stories'
		WHEN 'TH' THEN 'TH, LFS -=> 3.5 Stories'
		WHEN 'CN' THEN 'Condo or Apts'
		WHEN 'CO' THEN 'Commercial'
		ELSE ''
		END AS [PropertyType]
	,CASE app_form_xml.value('(//rblBuildingCode)[1]', 'varchar(max)')
		WHEN 'BC-2012' THEN '2012 NC Building Code'
		WHEN 'CC-2012' THEN '2012 NC Building Code Chapter 34'
		WHEN 'RC-2012' THEN '2012 NC Rehabilitation Code'
		WHEN 'BC-2015' THEN '2015 NC Existing Building Code'
		WHEN 'BC-2018' THEN '2018 NC Building Code'
		WHEN 'EBC-2018' THEN '2018 NC Existing Building Code'
		ELSE ''
		END AS [BuildingCode]
	,h.express ExpressReview
	,app_form_xml.value('(//txtOriginalProjectNumber)[1]', 'varchar(max)') OriginalProjectNumber
    ,app_form_xml.value('(//dtmDrawingReadyDate)[1]', 'varchar(max)') DrawingsReadyDate

	,CASE app_form_xml.value('(//rblIncludesAfforableOrWorkforceHousing)[1]', 'varchar(max)')
		WHEN 'Y' THEN 'Yes'
		WHEN 'N' THEN 'No'
		ELSE ''
		END AS [IncludesAfforableOrWorkforceHousing]

    ,app_form_xml.value('(//txtTotalSheets)[1]', 'varchar(max)') TotalNumOfSheets
	,CASE app_form_xml.value('(//rblPaidBy)[1]', 'varchar(max)')
		WHEN 'CHECK' THEN 'Check'
		WHEN 'LOA' THEN 'LOA'
		WHEN 'CREDIT CARD' THEN 'Credit Card'
		ELSE ''
		END AS [PrepaidFeePaymentType]

    ,app_form_xml.value('(//txtAccountNumber)[1]', 'varchar(max)') AccountNumber
	,f.fee_amt TotalEstimatedFees
	,CASE app_form_xml.value('(//rblRevisionAddsOrRemovesHousingUnits)[1]', 'varchar(max)')
		WHEN 'Y' THEN 'Yes'
		WHEN 'N' THEN 'No'
		ELSE ''
		END AS [RTAPAffordableUnitChange] 

    ,app_form_xml.value('(//txtAfforableHousingUnitsAdded)[1]', 'varchar(max)') RTAPAffordableWorkforceUnitsAdd
    ,app_form_xml.value('(//txtAfforableHousingUnitsRemoved)[1]', 'varchar(max)') RTAPAffordableUnitsRemove
    ,app_form_xml.value('(//txtWorkforceHousingUnitsAdded)[1]', 'varchar(max)') RTAPWorkforceAdd
    ,app_form_xml.value('(//txtWorkforceHousingUnitsRemoved)[1]', 'varchar(max)') RTAPWorkforceRemove
    ,CAST(app_form_xml.value('(//txtCostEstimation)[1]', 'varchar(max)') AS MONEY) ConstructionCost
    ,CAST(app_form_xml.value('(//txtCostEstimationEquipment)[1]', 'varchar(max)') AS MONEY) EquipmentCost
    ,CAST(app_form_xml.value('(//txtCostEstimationTotal)[1]', 'varchar(max)') AS MONEY) ProjectCostTotal
    ,app_form_xml.value('(//txtRTAPScopeDetails)[1]', 'varchar(max)') RTAPScopeOfWork
	
	
INTO epm_conv.CE_RTAP_Custom_Fields

FROM	[dbo].[tb_project_current_status] p
		LEFT JOIN [epm_conv].[CE_Projects_Accela_Hierarchy] h ON h.PERMITNUM = p.project_number
		LEFT JOIN [dbo].[tb_project_fee] f ON f.project_number = p.project_number and f.id_fee_types = 2

WHERE p.txt_review_type_code = 'RT' and app_form_xml is not null