# AccelaRecords.Model.PartTransactionModel
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**AccountName** | **string** | The budget account name associated with the part transaction. | [optional] 
**AccountNumber** | **string** | The budget account number associated with the part transaction. | [optional] 
**Comments** | **string** | Comments or notes about the current context. | [optional] 
**CostTotal** | **double?** | The total cost of the part transaction. | [optional] 
**HardReservation** | **string** | Indicates whether or not the part transaction is a hard reservation. \&quot;Y\&quot;: A hard reservation which guarantees the reservation, and subtract the order from the quantity on hand. \&quot;N\&quot; : A soft reservation which alerts the warehouse that houses the part that someone may request the part. The quantity on hand of the part does not change. | [optional] 
**Id** | **long?** | The part transaction system id assigned by the Civic Platform server. | [optional] 
**LocationId** | **long?** | The location ID associated with the part transaction. | [optional] 
**PartBin** | **string** | The name of the part bin. | [optional] 
**PartBrand** | **string** | The name of the part brand. | [optional] 
**PartDescription** | **string** | The description of the part. | [optional] 
**PartId** | **long?** | The part ID. | [optional] 
**PartLocation** | **string** | The location of the part. | [optional] 
**PartNumber** | **string** | The number of the part. | [optional] 
**Quantity** | **double?** | The number of units for which the same fee applies. | [optional] 
**RecordId** | [**RecordIdModel**](RecordIdModel.md) |  | [optional] 
**ResToPartLocation** | **string** |  | [optional] 
**ReservationNumber** | **long?** | The part reservation number. | [optional] 
**ReservationStatus** | **string** | The status of the part reservation. | [optional] 
**ServiceProviderCode** | **string** | The unique agency identifier. | [optional] 
**Status** | [**PartTransactionModelStatus**](PartTransactionModelStatus.md) |  | [optional] 
**Taxable** | **string** | Indicates whether or not the part is taxable. | [optional] 
**TransactionCost** | **double?** | The part transaction cost. | [optional] 
**TransactionDate** | **DateTime?** | The part transaction date. | [optional] 
**TransactionType** | **string** | The part transaction type. Possible values:  \&quot;Issue\&quot; : occurs either when someone requests and receives a part on the spot, or when someone receives a reserved part.  \&quot;Receive\&quot; : occurs when someone purchases a part or returns a part to a location.  \&quot;Transfer\&quot; : occurs when someone moves a part from one location to another.  \&quot;Adjust\&quot; : occurs when someone makes quantity adjustments for cycle counts.  \&quot;Reserve\&quot; : occurs when someone sets aside parts so they can issue them at a later date. | [optional] 
**Type** | [**PartTransactionModelType**](PartTransactionModelType.md) |  | [optional] 
**UnitCost** | **double?** | The unit cost per part. | [optional] 
**UnitMeasurement** | [**PartTransactionModelUnitMeasurement**](PartTransactionModelUnitMeasurement.md) |  | [optional] 
**UpdatedBy** | **string** | The user who last updated the checklist or checklist item. | [optional] 
**WorkOrderTaskCode** | **string** | The work order task code associated with the part transactionmodel. | [optional] 
**WorkOrderTaskCodeIndex** | **long?** | The work order task code index associated with the part transactionmodel. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

