# AccelaSettings.Model.ApoCustomFormsMetadataFields
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **long?** | The unique custom field id. | [optional] 
**Name** | **string** | The field name. | [optional] 
**Description** | **string** | Describes the usage or purpose of the custom field. | [optional] 
**Label** | [**ApoCustomFormsMetadataFieldsLabel**](ApoCustomFormsMetadataFieldsLabel.md) |  | [optional] 
**DataType** | **string** | The field data type. If the custom field is a DropdownList, the options[] array contains the list of possible values, or the sharedDropdownListName specifies the name of a shared dropdown list containing the possible values. | [optional] 
**DefaultValue** | **string** | Any default value for the custom field. | [optional] 
**DisplayOrder** | **long?** | The display order of the field on the custom form. | [optional] 
**Unit** | **string** | The unit of measure of a numeric custom field. | [optional] 
**IsRequired** | **string** | Indicates whether or not the field is required. | [optional] 
**IsPublicVisible** | **string** | Indicates whether or not a citizen user can see this field. | [optional] 
**IsRecordSearchable** | **string** | Indicates whether or not the field is searchable. | [optional] 
**MaxLength** | **long?** | The field maximum length. | [optional] 
**Options** | [**List&lt;ApoCustomFormsMetadataFieldsOptions&gt;**](ApoCustomFormsMetadataFieldsOptions.md) | Contains possible field values, if the field is a dropdown field type. | [optional] 
**SharedDropdownListName** | **string** | The name of the shared dropdown list, if the field is a dropdown field type. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

