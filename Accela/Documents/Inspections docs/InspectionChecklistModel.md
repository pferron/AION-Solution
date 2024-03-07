# AccelaInspections.Model.InspectionChecklistModel
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**ChecklistDesc** | **string** | The checklist description. | [optional] 
**ChecklistStatus** | **string** | The status of the checklist. | [optional] 
**CustomId** | **string** | An ID based on a different numbering convention from the numbering convention used by the record ID(xxxxx - xx - xxxxx).Accela Automation auto - generates and applies an alternate ID value when you submit a new application. | [optional] 
**DefaultOrderBy** | **string** | The field by which the checklist items are ordered by default . | [optional] 
**EntityType** | **string** | The type of entity, for example \&quot;INSPECTION\&quot;. | [optional] 
**Group** | **string** | The inspection checklist group. | [optional] 
**GuideType** | [**InspectionChecklistModelGuideType**](InspectionChecklistModelGuideType.md) |  | [optional] 
**Id** | **long?** | The checklist system id assigned by the Civic Platform server. | [optional] 
**InspectionId** | **string** | The ID of the inspection. | [optional] 
**IsRequired** | **string** | Indicates whether or not the checklist is required. | [optional] 
**Items** | [**List&lt;InspectionChecklistItemModel&gt;**](InspectionChecklistItemModel.md) |  | [optional] 
**RecordId** | [**RecordIdModel**](RecordIdModel.md) |  | [optional] 
**ServiceProviderCode** | **string** | The unique agency identifier. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

