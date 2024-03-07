# AccelaAddressParcelsOwners.Model.ConditionModel
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**ActionbyDepartment** | [**ConditionModelActionbyDepartment**](ConditionModelActionbyDepartment.md) |  | [optional] 
**ActionbyUser** | [**ConditionModelActionbyUser**](ConditionModelActionbyUser.md) |  | [optional] 
**ActiveStatus** | [**ConditionModelActiveStatus**](ConditionModelActiveStatus.md) |  | [optional] 
**AdditionalInformation** | **string** | An unlimited text field to use if other fields are filled. | [optional] 
**AppliedDate** | **DateTime?** | The date when condition is applied. The Applied Date defaults to the current date in any of the following scenarios: 1) you are adding a standard condition, 2) you are creating a new condition, or 3) The condition is auto-assigned to a record. | [optional] 
**AppliedbyDepartment** | [**ConditionModelAppliedbyDepartment**](ConditionModelAppliedbyDepartment.md) |  | [optional] 
**AppliedbyUser** | [**ConditionModelAppliedbyUser**](ConditionModelAppliedbyUser.md) |  | [optional] 
**DisplayNoticeInAgency** | **bool?** | Indicates whether or not to display the condition notice in Civic Platform when a condition is applied. | [optional] 
**DisplayNoticeInCitizens** | **bool?** | Indicates whether or not to display the condition notice in Citizen Access when a condition to a record or parcel is applied. | [optional] 
**DisplayNoticeInCitizensFee** | **bool?** | Indicates whether or not to display the condition notice in Citizen Access Fee Estimate page when a condition to a record or parcel is applied. | [optional] 
**EffectiveDate** | **DateTime?** | The date when the condition becomes effective. | [optional] 
**ExpirationDate** | **DateTime?** | The date when the condition expires. | [optional] 
**Group** | [**ConditionModelGroup**](ConditionModelGroup.md) |  | [optional] 
**Id** | **long?** | The condition system id assigned by the Civic Platform server. | [optional] 
**Inheritable** | [**ConditionModelInheritable**](ConditionModelInheritable.md) |  | [optional] 
**IsIncludeNameInNotice** | **bool?** | Indicates whether or not to display the condition name in the notice. | [optional] 
**IsIncludeShortCommentsInNotice** | **bool?** | Indicates whether or not to display the condition comments in the notice. | [optional] 
**LongComments** | **string** | Narrative comments to help identify the purpose or uses of the standard condition. | [optional] 
**Name** | **string** | The full name for the application contact. | [optional] 
**OwnerNumber** | **string** | (For owner conditions only) The owner number the condition applies to. | [optional] 
**ParcelNumber** | **string** | (For parcel conditions only) The parcel number the condition applies to. | [optional] 
**Priority** | [**ConditionModelPriority**](ConditionModelPriority.md) |  | [optional] 
**PublicDisplayMessage** | **string** | Text entered into this field displays in the condition notice or condition status bar for the Condition Name for the public user in Accela IVR (AIVR) and Citizen Access (ACA). | [optional] 
**ResolutionAction** | **string** | The action performed in response to a condition. | [optional] 
**ServiceProviderCode** | **string** | The unique agency identifier. | [optional] 
**Severity** | [**ConditionModelSeverity**](ConditionModelSeverity.md) |  | [optional] 
**ShortComments** | **string** | A brief description of the condition name. For example, the text may describe the situation that requires the system to apply the condition. You can set these short comments to display when a user accesses an application with this condition applied to it | [optional] 
**Status** | [**ConditionModelStatus**](ConditionModelStatus.md) |  | [optional] 
**StatusDate** | **DateTime?** | The date when the current status changed. | [optional] 
**StatusType** | **string** | The status type for a standard condition or an approval condition, applied or not applied for example. | [optional] 
**Type** | [**ConditionModelType**](ConditionModelType.md) |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

