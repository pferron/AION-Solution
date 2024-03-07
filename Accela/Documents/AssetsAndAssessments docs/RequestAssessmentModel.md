# AccelaAssetsAndAssessments.Model.RequestAssessmentModel
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Attributes** | [**CustomAttributeModel**](CustomAttributeModel.md) |  | [optional] 
**Comment** | **string** |  Comments or notes about the assessment. | [optional] 
**ConditionAssessment** | **string** | The condition assessment type. See [Get All Condition Assessment Types](./api-settings.html#operation/v4.get.settings.assessments.types). | [optional] 
**InspectionDate** | **string** | The inspection date for the assessment, in yyyy-mm-dd format. | [optional] 
**InspectionTime** | **string** | The inspection time for the assessment, in hh:mm AM/PM format. | [optional] 
**InspectorId** | **string** | The ID of the inspector. See [Get All Inspectors](./api-inspections.html#operation/v4.get.inspectors). | [optional] 
**ScheduleDate** | **string** | The scheduled assessment date, in yyyy-mm-dd format. | [optional] 
**ScheduleTime** | **string** | The scheduled assessment time, in hh:mm AM/PM format. | [optional] 
**Status** | **string** | The status of the condition assessment | 
**TimeSpent** | **string** | The number of hours spent on the assessment. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

