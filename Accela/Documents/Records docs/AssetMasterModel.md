# AccelaRecords.Model.AssetMasterModel
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**AssetId** | **string** | The unique alpha-numeric asset ID in an asset group.  **Added in Civic Platform version**: 9.2.0  | [optional] 
**ClassType** | **string** | A Class Type is how Civic Platform groups objects that an agency owns or maintains. The five class types are component, linear, node-link linear, point, and polygon. Asset class types provide the ability to assign or group multiple asset types together.  | [optional] 
**Comments** | [**AssetMasterModelComments**](AssetMasterModelComments.md) |  | [optional] 
**CurrentValue** | **double?** | The current value of the asset. | [optional] 
**DateOfService** | **DateTime?** | The date the asset was initially placed into service. | [optional] 
**DependentFlag** | **string** | Indicates whether or not the parent asset is dependent on this asset. | [optional] 
**DepreciationAmount** | **double?** | The decline in the asset value by the asset depreciation calculation. | [optional] 
**DepreciationEndDate** | **DateTime?** | The end date for the asset depreciation calculation. This field is used in the asset depreciation calculation. | [optional] 
**DepreciationStartDate** | **DateTime?** | The start date for the asset depreciation calculation. This field is used in the asset depreciation calculation. | [optional] 
**DepreciationValue** | **double?** | The asset value after the asset depreciation calculation, which is based on the start value, depreciation start and end dates, useful life, and salvage value. | [optional] 
**Description** | [**AssetMasterModelDescription**](AssetMasterModelDescription.md) |  | [optional] 
**EndID** | **string** | The ending point asset ID. | [optional] 
**GisObjects** | [**List&lt;GISObjectModel&gt;**](GISObjectModel.md) |  | [optional] 
**Id** | **long?** | The asset system id assigned by the Civic Platform server. | [optional] 
**Name** | [**AssetMasterModelName**](AssetMasterModelName.md) |  | [optional] 
**Number** | **string** | The unique, alpha-numeric asset ID. | [optional] 
**SalvageValue** | **double?** | The residual value of the asset at the end of itâ€™s useful life. | [optional] 
**ServiceProviderCode** | **string** | The unique agency identifier. | [optional] 
**Size** | **double?** | A positive numeric value for the asset size. | [optional] 
**SizeUnit** | **string** | The unit of measure corresponding to the asset size. | [optional] 
**StartID** | **string** | The starting point asset ID. | [optional] 
**StartValue** | **double?** | The beginning value or purchase price of the asset. | [optional] 
**Status** | [**AssetMasterModelStatus**](AssetMasterModelStatus.md) |  | [optional] 
**StatusDate** | **DateTime?** | The date the asset status changed. | [optional] 
**Type** | [**AssetMasterModelType**](AssetMasterModelType.md) |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

