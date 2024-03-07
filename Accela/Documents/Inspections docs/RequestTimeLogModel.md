# AccelaInspections.Model.RequestTimeLogModel
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Billable** | **string** | Indicates whether or not the item is billable. | [optional] 
**Cost** | **double?** | The calculated cost of the entry. | [optional] 
**CreateBy** | **string** | The unique user id of the individual that created this entry. | [optional] 
**CreateDate** | **DateTime?** |  The date the entry was created. | [optional] 
**Duration** | **string** | The duration of the entry, using the format hh:mm. | [optional] 
**EndTime** | **string** | The end time for the time accounting entry. | [optional] 
**EntityId** | **string** | The unique ID of the entity or record. | [optional] 
**EntityType** | **string** | The type of entity, such as \&quot;Record\&quot;. | [optional] 
**Group** | [**TimeLogModelGroup**](TimeLogModelGroup.md) |  | [optional] 
**Id** | **long?** | The time accounting entry&#39;s system id assigned by the Civic Platform server. | [optional] 
**Percent** | **double?** | The percentage point for calculating the cost. 50 stands for 50%, 80 stands for 80%, and 150 stands for 150%. | [optional] 
**Rate** | **double?** | The cost rate. | [optional] 
**RecordId** | [**RecordIdModel**](RecordIdModel.md) |  | [optional] 
**ServiceProviderCode** | **string** | The unique agency identifier. | [optional] 
**StartTime** | **string** | The start time of the time accounting entry. | [optional] 
**TotalMinutes** | **long?** | The total number of billable minutes. | [optional] 
**Type** | [**TimeLogModelType**](TimeLogModelType.md) |  | [optional] 
**UserId** | **string** | The userid assigned to the time accounting entry. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

