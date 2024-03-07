# AccelaSettings.Api.SettingsDocumentsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4DeleteSettingsDocumentsFoldersIds**](SettingsDocumentsApi.md#v4deletesettingsdocumentsfoldersids) | **DELETE** /v4/settings/documents/folders/{ids} | Delete Folders
[**V4GetSettingsDocumentsCategories**](SettingsDocumentsApi.md#v4getsettingsdocumentscategories) | **GET** /v4/settings/documents/categories | Get All Document Categories
[**V4GetSettingsDocumentsFolderGroups**](SettingsDocumentsApi.md#v4getsettingsdocumentsfoldergroups) | **GET** /v4/settings/documents/folderGroups | Get All Folder Groups
[**V4GetSettingsDocumentsFoldersGroupId**](SettingsDocumentsApi.md#v4getsettingsdocumentsfoldersgroupid) | **GET** /v4/Settings/documents/folders/{groupId} | Get Folder Group
[**V4PostSettingsDocumentsFolders**](SettingsDocumentsApi.md#v4postsettingsdocumentsfolders) | **POST** /v4/settings/documents/folders | Create Folders
[**V4PutSettingsDocumentsFolderGroups**](SettingsDocumentsApi.md#v4putsettingsdocumentsfoldergroups) | **PUT** /v4/settings/documents/folderGroups | Update Folder Groups
[**V4PutSettingsDocumentsFolders**](SettingsDocumentsApi.md#v4putsettingsdocumentsfolders) | **PUT** /v4/settings/documents/folders | Update Folders


<a name="v4deletesettingsdocumentsfoldersids"></a>
# **V4DeleteSettingsDocumentsFoldersIds**
> ResponseResultModelArray V4DeleteSettingsDocumentsFoldersIds (string contentType, string authorization, string ids, string lang = null)

Delete Folders

Deletes the specified document folders. **API Endpoint**:  DELETE /v4/settings/documents/folders/{ids}   **Scope**:  documents  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4DeleteSettingsDocumentsFoldersIdsExample
    {
        public void main()
        {
            var apiInstance = new SettingsDocumentsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var ids = ids_example;  // string | Comma-delimited IDs of the document folders to delete.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Delete Folders
                ResponseResultModelArray result = apiInstance.V4DeleteSettingsDocumentsFoldersIds(contentType, authorization, ids, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsDocumentsApi.V4DeleteSettingsDocumentsFoldersIds: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | [default to application/x-www-form-urlencoded]
 **authorization** | **string**| Construct oAuth2 authentication token | 
 **ids** | **string**| Comma-delimited IDs of the document folders to delete. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getsettingsdocumentscategories"></a>
# **V4GetSettingsDocumentsCategories**
> ResponseDocumentTypeModelArray V4GetSettingsDocumentsCategories (string contentType, string authorization, string lang = null)

Get All Document Categories

Gets the document types. **API Endpoint**:  GET /v4/settings/documents/categories  **Scope**:  documents  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsDocumentsCategoriesExample
    {
        public void main()
        {
            var apiInstance = new SettingsDocumentsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Document Categories
                ResponseDocumentTypeModelArray result = apiInstance.V4GetSettingsDocumentsCategories(contentType, authorization, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsDocumentsApi.V4GetSettingsDocumentsCategories: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | [default to application/x-www-form-urlencoded]
 **authorization** | **string**| Construct oAuth2 authentication token | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseDocumentTypeModelArray**](ResponseDocumentTypeModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getsettingsdocumentsfoldergroups"></a>
# **V4GetSettingsDocumentsFolderGroups**
> ResponseFolderGroupModelArray V4GetSettingsDocumentsFolderGroups (string contentType, string authorization, string isActive, string lang = null)

Get All Folder Groups

Gets all folder groups in the system. **API Endpoint**:  GET /v4/settings/documents/folderGroups  **Scope**:  documents  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsDocumentsFolderGroupsExample
    {
        public void main()
        {
            var apiInstance = new SettingsDocumentsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var isActive = isActive_example;  // string | Filter whether or not the folder gorup is active.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Folder Groups
                ResponseFolderGroupModelArray result = apiInstance.V4GetSettingsDocumentsFolderGroups(contentType, authorization, isActive, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsDocumentsApi.V4GetSettingsDocumentsFolderGroups: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | [default to application/x-www-form-urlencoded]
 **authorization** | **string**| Construct oAuth2 authentication token | 
 **isActive** | **string**| Filter whether or not the folder gorup is active. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseFolderGroupModelArray**](ResponseFolderGroupModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getsettingsdocumentsfoldersgroupid"></a>
# **V4GetSettingsDocumentsFoldersGroupId**
> ResponseDocumentFolderModelArray V4GetSettingsDocumentsFoldersGroupId (string contentType, string authorization, string groupId, string isActive, string lang = null)

Get Folder Group

Gets the folders in the specified folder group. **API Endpoint**:  GET /v4/settings/documents/folders/{groupId}  **Scope**:  documents  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsDocumentsFoldersGroupIdExample
    {
        public void main()
        {
            var apiInstance = new SettingsDocumentsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var groupId = groupId_example;  // string | The ID of the folder group to fetch.
            var isActive = isActive_example;  // string | Filter by whether or not the folder is active.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Folder Group
                ResponseDocumentFolderModelArray result = apiInstance.V4GetSettingsDocumentsFoldersGroupId(contentType, authorization, groupId, isActive, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsDocumentsApi.V4GetSettingsDocumentsFoldersGroupId: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | [default to application/x-www-form-urlencoded]
 **authorization** | **string**| Construct oAuth2 authentication token | 
 **groupId** | **string**| The ID of the folder group to fetch. | 
 **isActive** | **string**| Filter by whether or not the folder is active. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseDocumentFolderModelArray**](ResponseDocumentFolderModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4postsettingsdocumentsfolders"></a>
# **V4PostSettingsDocumentsFolders**
> ResponseResultModelArray V4PostSettingsDocumentsFolders (string contentType, string authorization, List<DocumentFolderModel> body, string lang = null)

Create Folders

Creates document folders. **API Endpoint**:   POST /v4/settings/documents/folders  **Scope**:  documents  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4PostSettingsDocumentsFoldersExample
    {
        public void main()
        {
            var apiInstance = new SettingsDocumentsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var body = new List<DocumentFolderModel>(); // List<DocumentFolderModel> | The document folders to create.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Create Folders
                ResponseResultModelArray result = apiInstance.V4PostSettingsDocumentsFolders(contentType, authorization, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsDocumentsApi.V4PostSettingsDocumentsFolders: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | [default to application/x-www-form-urlencoded]
 **authorization** | **string**| Construct oAuth2 authentication token | 
 **body** | [**List&lt;DocumentFolderModel&gt;**](DocumentFolderModel.md)| The document folders to create. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4putsettingsdocumentsfoldergroups"></a>
# **V4PutSettingsDocumentsFolderGroups**
> ResponseResultModelArray V4PutSettingsDocumentsFolderGroups (string contentType, string authorization, List<FolderGroupModel> body, string lang = null)

Update Folder Groups

Updates folder groups for documents. **API Endpoint**:  PUT /v4/settings/documents/folderGroups   **Scope**:  documents  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4PutSettingsDocumentsFolderGroupsExample
    {
        public void main()
        {
            var apiInstance = new SettingsDocumentsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var body = new List<FolderGroupModel>(); // List<FolderGroupModel> | The document folder group information to be updated.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Folder Groups
                ResponseResultModelArray result = apiInstance.V4PutSettingsDocumentsFolderGroups(contentType, authorization, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsDocumentsApi.V4PutSettingsDocumentsFolderGroups: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | [default to application/x-www-form-urlencoded]
 **authorization** | **string**| Construct oAuth2 authentication token | 
 **body** | [**List&lt;FolderGroupModel&gt;**](FolderGroupModel.md)| The document folder group information to be updated. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4putsettingsdocumentsfolders"></a>
# **V4PutSettingsDocumentsFolders**
> ResponseResultModelArray V4PutSettingsDocumentsFolders (string contentType, string authorization, List<DocumentFolderModel> body, string lang = null)

Update Folders

Updates document folders. **API Endpoint**:  PUT /v4/settings/documents/folders  **Scope**:  documents  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4PutSettingsDocumentsFoldersExample
    {
        public void main()
        {
            var apiInstance = new SettingsDocumentsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var body = new List<DocumentFolderModel>(); // List<DocumentFolderModel> | The document folder information to be updated.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Folders
                ResponseResultModelArray result = apiInstance.V4PutSettingsDocumentsFolders(contentType, authorization, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsDocumentsApi.V4PutSettingsDocumentsFolders: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | [default to application/x-www-form-urlencoded]
 **authorization** | **string**| Construct oAuth2 authentication token | 
 **body** | [**List&lt;DocumentFolderModel&gt;**](DocumentFolderModel.md)| The document folder information to be updated. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

