# AccelaRecords.Model.RequestSimpleRecordModel
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**ActualProductionUnit** | **double?** | Estimated cost per production unit. | [optional] 
**AppearanceDate** | **DateTime?** | The date for a hearing appearance. | [optional] 
**AppearanceDayOfWeek** | **string** | The day for a hearing appearance. | [optional] 
**AssignedDate** | **DateTime?** | The date the application was assigned. | [optional] 
**AssignedToDepartment** | **string** | The department responsible for the action. See [Get All Departments](./api-settings.html#operation/v4.get.settings.departments). | [optional] 
**Balance** | **double?** | The amount due. | [optional] 
**Booking** | **bool?** | Indicates whether or not there was a booking in addition to a citation. | [optional] 
**ClosedByDepartment** | **string** | The department responsible for closing the record. See [Get All Departments](./api-settings.html#operation/v4.get.settings.departments). | [optional] 
**ClosedDate** | **DateTime?** | The date the application was closed. | [optional] 
**CompleteDate** | **DateTime?** | The date the application was completed. | [optional] 
**CompletedByDepartment** | **string** | The department responsible for completion. See [Get All Departments](./api-settings.html#operation/v4.get.settings.departments). | [optional] 
**DefendantSignature** | **bool?** | Indicates whether or not a defendant&#39;s signature has been obtained. | [optional] 
**Description** | **string** | The description of the record or item. | [optional] 
**EnforceDepartment** | **string** | The name of the department responsible for enforcement. See [Get All Departments](./api-settings.html#operation/v4.get.settings.departments). | [optional] 
**EstimatedProductionUnit** | **double?** | The estimated number of production units. | [optional] 
**EstimatedTotalJobCost** | **double?** | The estimated cost of the job. | [optional] 
**FirstIssuedDate** | **DateTime?** | The first issued date for license | [optional] 
**Infraction** | **bool?** | Indicates whether or not an infraction occurred. | [optional] 
**InspectorDepartment** | **string** | The name of the department where the inspector works. See [Get All Departments](./api-settings.html#operation/v4.get.settings.departments). | [optional] 
**InspectorId** | **string** | The ID number of the inspector. See [Get All Inspectors](./api-inspections.html#operation/v4.get.inspectors). | [optional] 
**InspectorName** | **string** | The name of the inspector. See [Get All Inspectors](./api-inspections.html#operation/v4.get.inspectors). | [optional] 
**JobValue** | **double?** | The value of the job. | [optional] 
**Misdemeanor** | **bool?** | Indicates whether or not a misdemeanor occurred. | [optional] 
**Name** | **string** | The name associated to the record. | [optional] 
**OffenseWitnessed** | **bool?** | Indicates whether or not  there was a witness to the alleged offense. | [optional] 
**Priority** | [**RecordAPOCustomFormsModelPriority**](RecordAPOCustomFormsModelPriority.md) |  | [optional] 
**PublicOwned** | **bool?** | Indicates whether or not the record is for the public. | [optional] 
**RenewalInfo** | [**RecordExpirationModel**](RecordExpirationModel.md) |  | [optional] 
**ReportedChannel** | [**RecordAPOCustomFormsModelReportedChannel**](RecordAPOCustomFormsModelReportedChannel.md) |  | [optional] 
**ReportedDate** | **DateTime?** | The date the complaint was reported. | [optional] 
**ReportedType** | [**RecordAPOCustomFormsModelReportedType**](RecordAPOCustomFormsModelReportedType.md) |  | [optional] 
**ScheduledDate** | **DateTime?** | The date when the inspection gets scheduled. | [optional] 
**Severity** | [**RecordAPOCustomFormsModelSeverity**](RecordAPOCustomFormsModelSeverity.md) |  | [optional] 
**ShortNotes** | **string** | A brief note about the record subject. | [optional] 
**Status** | [**RecordAPOCustomFormsModelStatus**](RecordAPOCustomFormsModelStatus.md) |  | [optional] 
**StatusReason** | [**RecordAPOCustomFormsModelStatusReason**](RecordAPOCustomFormsModelStatusReason.md) |  | [optional] 
**TotalFee** | **double?** | The total amount of the fees invoiced to the record. | [optional] 
**TotalPay** | **double?** | The total amount of pay. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

