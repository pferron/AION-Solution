# AccelaContactsAndProfessionals.Model.PeopleConditionModel
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**ActionbyDepartment** | [**CommonConditionModelActionbyDepartment**](CommonConditionModelActionbyDepartment.md) |  | [optional] 
**ActionbyUser** | [**CommonConditionModelActionbyUser**](CommonConditionModelActionbyUser.md) |  | [optional] 
**ActiveStatus** | [**CommonConditionModelActiveStatus**](CommonConditionModelActiveStatus.md) |  | [optional] 
**AdditionalInformation** | **string** | An unlimited text field to use if other fields are filled. | [optional] 
**AppliedDate** | **DateTime?** | The date the standard condition was applied. | [optional] 
**AppliedbyDepartment** | [**CommonConditionModelAppliedbyDepartment**](CommonConditionModelAppliedbyDepartment.md) |  | [optional] 
**AppliedbyUser** | [**CommonConditionModelAppliedbyUser**](CommonConditionModelAppliedbyUser.md) |  | [optional] 
**ContactID** | **string** | The ID of the contact. | [optional] 
**ContactSeqNumber** | **long?** | The contact id assigned by the Civic Platform server. | [optional] 
**DisplayNoticeInAgency** | **bool?** | Indicates whether or not to display the condition notice in Accela Automation when a condition to a record or parcel is applied. | [optional] 
**DisplayNoticeInCitizens** | **bool?** | Indicates whether or not to display the condition notice in Accela Citizen Access when a condition to a record or parcel is applied. | [optional] 
**DisplayNoticeInCitizensFee** | **bool?** | Indicates whether or not to display the condition notice in Accela Citizen Access Fee Estimate page when a condition to a record or parcel is applied. | [optional] 
**EffectiveDate** | **DateTime?** | The date when you want the condition to become effective. | [optional] 
**ExpirationDate** | **DateTime?** | The date when the condition expires. | [optional] 
**Group** | [**CommonConditionModelGroup**](CommonConditionModelGroup.md) |  | [optional] 
**Id** | **long?** | The condition system id assigned by the Civic Platform server. | [optional] 
**Inheritable** | [**CommonConditionModelInheritable**](CommonConditionModelInheritable.md) |  | [optional] 
**IsIncludeNameInNotice** | **bool?** | Indicates whether or not to display the condition name in the notice. | [optional] 
**IsIncludeShortCommentsInNotice** | **bool?** | Indicates whether or not to display the condition comments in the notice. | [optional] 
**LicenseSeqNo** | **long?** | The license id assigned by the Civic Platform server. | [optional] 
**LongComments** | **string** | Narrative comments to help identify the purpose or uses of the standard condition. | [optional] 
**Name** | **string** | The name of the standard condition. | [optional] 
**Priority** | [**CommonConditionModelPriority**](CommonConditionModelPriority.md) |  | [optional] 
**PublicDisplayMessage** | **string** | Text entered into this field displays in the condition notice or condition status bar for the Condition Name for the public user in Accela IVR (AIVR) and Accela Citizen Access (ACA). | [optional] 
**ResolutionAction** | **string** | he action performed in response to a condition. | [optional] 
**ServiceProviderCode** | **string** | The unique agency identifier. | [optional] 
**Severity** | [**CommonConditionModelSeverity**](CommonConditionModelSeverity.md) |  | [optional] 
**ShortComments** | **string** | A brief description of the condition name. For example, the text may describe the situation that requires the system to apply the condition. You can set these short comments to display when a user accesses an application with this condition applied to it | [optional] 
**Status** | [**CommonConditionModelStatus**](CommonConditionModelStatus.md) |  | [optional] 
**StatusDate** | **DateTime?** | The date when the current status changed. | [optional] 
**StatusType** | **string** | The status type for a standard condition or an approval condition, applied or not applied for example. | [optional] 
**Type** | [**CommonConditionModelType**](CommonConditionModelType.md) |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

