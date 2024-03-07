# AccelaSettings.Model.AssetTypeModel
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Display** | **string** |  Displays the hierarchical name of the asset type, which includes the asset group and type. | [optional] 
**GisIDForAssetID** | **string** |  The name of the GIS ID field that is mapped to the asset ID. | [optional] 
**GisLayer** | [**GISLayerIdModel**](GISLayerIdModel.md) |  | [optional] 
**GisService** | **string** |  The GIS service to be used with the asset. The GIS service should be a configured map service in Accela GIS Administration. | [optional] 
**Group** | **string** | The name of the asset group. For example: Water or Street. An Asset Group is an agency-defined collection of objects the agency owns or maintains. | [optional] 
**Id** | **string** | The id of the asset type. | [optional] 
**Type** | **string** | The type of asset. For example: Hydrant or Manhole. An Asset Type is an agencydefined classification of similar objects that share the same standard asset attributes. Related asset types belong to an asset group, and multiple asset types can share the same Class Type. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

