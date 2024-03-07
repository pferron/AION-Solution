# AccelaInspections.Model.DocumentModel
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Category** | [**DocumentModelCategory**](DocumentModelCategory.md) |  | [optional] 
**Deletable** | [**UserRolePrivilegeModel**](UserRolePrivilegeModel.md) |  | [optional] 
**Department** | **string** | The name of the department where the inspector works. | [optional] 
**Description** | **string** | The description of the document. | [optional] 
**Downloadable** | [**UserRolePrivilegeModel**](UserRolePrivilegeModel.md) |  | [optional] 
**EntityId** | **string** | The unique ID of the entity or record. | [optional] 
**EntityType** | **string** | The type of entity. | [optional] 
**FileName** | **string** | The name of the file as it displays in the source location. | [optional] 
**Group** | [**DocumentModelGroup**](DocumentModelGroup.md) |  | [optional] 
**Id** | **long?** | The document system id assigned by the Civic Platform server. | [optional] 
**ModifiedBy** | **string** | The user account that last modified the document. | [optional] 
**ModifiedDate** | **DateTime?** | The date the document was last modified. | [optional] 
**ServiceProviderCode** | **string** | The unique agency identifier | [optional] 
**Size** | **double?** | The file size of the document. | [optional] 
**Source** | **string** | The name for your agency&#39; s electronic document management system. | [optional] 
**Status** | [**DocumentModelStatus**](DocumentModelStatus.md) |  | [optional] 
**StatusDate** | **string** | The date when the current status changed. | [optional] 
**TitleViewable** | [**UserRolePrivilegeModel**](UserRolePrivilegeModel.md) |  | [optional] 
**Type** | **string** | The document type. | [optional] 
**UploadedBy** | **string** | The user who uploaded the document to the record. | [optional] 
**UploadedDate** | **string** | The date when the document was uploaded to the record. | [optional] 
**VirtualFolders** | **string** | The virtual folder for storing the attachment.With virtual folders you can organize uploaded attachments in groups | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

