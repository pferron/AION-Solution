# AccelaMiscellanous.Model.TimeLogModel
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Access** | [**TimeLogModelAccess**](TimeLogModelAccess.md) |  | [optional] 
**Billable** | **string** | Indicates whether or not the item is billable. | [optional] 
**Cost** | **double?** | The calculated cost of the entry. | [optional] 
**CreateBy** | **string** | The unique user id of the individual that created this entry. | [optional] 
**CreateDate** | **DateTime?** |  The date the entry was created. | [optional] 
**DetailDuration** | [**List&lt;CostingQuantityModel&gt;**](CostingQuantityModel.md) | Contains details about the time accounting duration. | [optional] 
**Duration** | **string** | The duration of the entry, using the format hh:mm. | [optional] 
**EndMileage** | **double?** | The ending mileage for the time accounting item. | [optional] 
**EndTime** | **string** | The end time for the time accounting entry. | [optional] 
**Entity** | **string** | The entity associated with the time accounting item. | [optional] 
**EntityId** | **string** | The unique ID of the entity or record. | [optional] 
**EntityType** | **string** | The type of entity, such as \&quot;Record\&quot;. | [optional] 
**Group** | [**TimeLogModelGroup**](TimeLogModelGroup.md) |  | [optional] 
**Id** | **long?** | The time accounting entry&#39;s system id assigned by the Civic Platform server. | [optional] 
**LastChangedBy** | **string** | The person who last changed the time accounting entry. | [optional] 
**LastChangedDate** | **DateTime?** | The date when the time accounting entry was last changed. | [optional] 
**LoggedDate** | **DateTime?** | The date when the time accounting was logged. | [optional] 
**Materials** | **string** | The materials tracked by the time accounting entry. | [optional] 
**MaterialsCost** | **double?** | The cost of materials tracked by the time accounting entry. | [optional] 
**Notation** | **string** | The notation associated with the time accounting entry. | [optional] 
**Percent** | **double?** | The percentage point for calculating the cost. 50 stands for 50%, 80 stands for 80%, and 150 stands for 150%. | [optional] 
**Rate** | **double?** | The cost rate. | [optional] 
**RecordId** | [**RecordIdModel**](RecordIdModel.md) |  | [optional] 
**ServiceProviderCode** | **string** | The unique agency identifier. | [optional] 
**StartMileage** | **double?** | The starting mileage for the time accounting entry. | [optional] 
**StartTime** | **string** | The start time of the time accounting entry. | [optional] 
**Status** | [**TimeLogModelStatus**](TimeLogModelStatus.md) |  | [optional] 
**TotalMileage** | **double?** | The total mileage for the time accounting entry. | [optional] 
**TotalMinutes** | **long?** | The total number of billable minutes. | [optional] 
**Type** | [**TimeLogModelType**](TimeLogModelType.md) |  | [optional] 
**UserId** | **string** | The userid assigned to the time accounting entry. | [optional] 
**VehicleId** | [**TimeLogModelVehicleId**](TimeLogModelVehicleId.md) |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

