# AccelaInspections.Api.InspectionsChecklistsChecklistItemsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetInspectionsInspectionIdChecklistsChecklistIdChecklistItems**](InspectionsChecklistsChecklistItemsApi.md#v4getinspectionsinspectionidchecklistschecklistidchecklistitems) | **GET** /v4/inspections/{inspectionId}/checklists/{checklistId}/checklistItems | Get All Inspection Checklist Items
[**V4GetInspectionsInspectionIdChecklistsChecklistIdChecklistItemsChecklistItemIdDocuments**](InspectionsChecklistsChecklistItemsApi.md#v4getinspectionsinspectionidchecklistschecklistidchecklistitemschecklistitemiddocuments) | **GET** /v4/inspections/{inspectionId}/checklists/{checklistId}/checklistItems/{checkListItemId}/documents | Get All Inspection Checklist Item Documents
[**V4GetInspectionsInspectionIdChecklistsChecklistIdChecklistItemsChecklistItemIdHistories**](InspectionsChecklistsChecklistItemsApi.md#v4getinspectionsinspectionidchecklistschecklistidchecklistitemschecklistitemidhistories) | **GET** /v4/inspections/{inspectionId}/checklists/{checklistId}/checklistItems/{checklistItemId}/histories | Get Inspection Checklist Item History
[**V4GetInspectionsInspectionIdChecklistsChecklistIdChecklistItemsChecklistItemIdStatuses**](InspectionsChecklistsChecklistItemsApi.md#v4getinspectionsinspectionidchecklistschecklistidchecklistitemschecklistitemidstatuses) | **GET** /v4/inspections/{inspectionId}/checklists/{checklistId}/checklistItems/{checklistItemId}/statuses | Get All Inspection Checklist Item Statuses
[**V4PostInspectionsInspectionIdChecklistsChecklistIdChecklistItemsChecklistItemIdDocuments**](InspectionsChecklistsChecklistItemsApi.md#v4postinspectionsinspectionidchecklistschecklistidchecklistitemschecklistitemiddocuments) | **POST** /v4/inspections/{inspectionId}/checklists/{checklistId}/checklistItems/{checkListItemId}/documents | Create Checklist Item Documents
[**V4PutInspectionsChecklistsIdChecklistItems**](InspectionsChecklistsChecklistItemsApi.md#v4putinspectionschecklistsidchecklistitems) | **PUT** /v4/inspections/checklists/{id}/checklistItems | Update Checklist Items


<a name="v4getinspectionsinspectionidchecklistschecklistidchecklistitems"></a>
# **V4GetInspectionsInspectionIdChecklistsChecklistIdChecklistItems**
> ResponseInspectionChecklistItemModelArray V4GetInspectionsInspectionIdChecklistsChecklistIdChecklistItems (string contentType, string authorization, long? inspectionId, long? checklistId, string fields = null, string lang = null)

Get All Inspection Checklist Items

Gets the checklist items for the specified inspection checklist. **API Endpoint**: GET /v4/inspections/{inspectionId}/checklists/{checklistId}/checklistItems  **Scope**:  inspections  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4GetInspectionsInspectionIdChecklistsChecklistIdChecklistItemsExample
    {
        public void main()
        {
            var apiInstance = new InspectionsChecklistsChecklistItemsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var inspectionId = 789;  // long? | The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections).
            var checklistId = 789;  // long? | The ID of the inspection checklist to fetch. See [Get All Inspection Checklists](./api-settings.html#operation/v4.get.settings.inspections.checklists), [Get All Checklists for Inspection](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists).
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Inspection Checklist Items
                ResponseInspectionChecklistItemModelArray result = apiInstance.V4GetInspectionsInspectionIdChecklistsChecklistIdChecklistItems(contentType, authorization, inspectionId, checklistId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsChecklistsChecklistItemsApi.V4GetInspectionsInspectionIdChecklistsChecklistIdChecklistItems: " + e.Message );
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
 **inspectionId** | **long?**| The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections). | 
 **checklistId** | **long?**| The ID of the inspection checklist to fetch. See [Get All Inspection Checklists](./api-settings.html#operation/v4.get.settings.inspections.checklists), [Get All Checklists for Inspection](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists). | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseInspectionChecklistItemModelArray**](ResponseInspectionChecklistItemModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getinspectionsinspectionidchecklistschecklistidchecklistitemschecklistitemiddocuments"></a>
# **V4GetInspectionsInspectionIdChecklistsChecklistIdChecklistItemsChecklistItemIdDocuments**
> ResponseDocumentModelArray V4GetInspectionsInspectionIdChecklistsChecklistIdChecklistItemsChecklistItemIdDocuments (string contentType, string authorization, long? inspectionId, long? checklistId, string checkListItemId, string fields = null, string lang = null)

Get All Inspection Checklist Item Documents

Gets the documents associated with the specified checklist item. **API Endpoint**:  GET /v4/inspections/{inspectionId}/checklists/{checklistId}/checklistItems/{checkListItemId}/documents  **Scope**:  inspections  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4GetInspectionsInspectionIdChecklistsChecklistIdChecklistItemsChecklistItemIdDocumentsExample
    {
        public void main()
        {
            var apiInstance = new InspectionsChecklistsChecklistItemsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var inspectionId = 789;  // long? | The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections).
            var checklistId = 789;  // long? | The ID of the inspection checklist to fetch. See [Get All Inspection Checklists](./api-settings.html#operation/v4.get.settings.inspections.checklists), [Get All Checklists for Inspection](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists).
            var checkListItemId = checkListItemId_example;  // string | The ID of the checklist item to fetch.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Inspection Checklist Item Documents
                ResponseDocumentModelArray result = apiInstance.V4GetInspectionsInspectionIdChecklistsChecklistIdChecklistItemsChecklistItemIdDocuments(contentType, authorization, inspectionId, checklistId, checkListItemId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsChecklistsChecklistItemsApi.V4GetInspectionsInspectionIdChecklistsChecklistIdChecklistItemsChecklistItemIdDocuments: " + e.Message );
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
 **inspectionId** | **long?**| The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections). | 
 **checklistId** | **long?**| The ID of the inspection checklist to fetch. See [Get All Inspection Checklists](./api-settings.html#operation/v4.get.settings.inspections.checklists), [Get All Checklists for Inspection](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists). | 
 **checkListItemId** | **string**| The ID of the checklist item to fetch. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseDocumentModelArray**](ResponseDocumentModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getinspectionsinspectionidchecklistschecklistidchecklistitemschecklistitemidhistories"></a>
# **V4GetInspectionsInspectionIdChecklistsChecklistIdChecklistItemsChecklistItemIdHistories**
> ResponseChekclistItemHistoryModel V4GetInspectionsInspectionIdChecklistsChecklistIdChecklistItemsChecklistItemIdHistories (string contentType, string authorization, string inspectionId, string checklistId, string checklistItemId, string lang = null)

Get Inspection Checklist Item History

Gets historical (audit) data related to a checklist item for the specified inspection. **API Endpoint**:  GET /v4/inspections/{inspectionId}/checklists/{checklistId}/checklistItems/{checklistItemId}/histories  **Scope**:  inspections  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4GetInspectionsInspectionIdChecklistsChecklistIdChecklistItemsChecklistItemIdHistoriesExample
    {
        public void main()
        {
            var apiInstance = new InspectionsChecklistsChecklistItemsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var inspectionId = inspectionId_example;  // string | The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections).
            var checklistId = checklistId_example;  // string | The ID of the inspection checklist to fetch. See [Get All Inspection Checklists](./api-settings.html#operation/v4.get.settings.inspections.checklists), [Get All Checklists for Inspection](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists).
            var checklistItemId = checklistItemId_example;  // string | The ID of the inspection checklist item to fetch. See [Get All Checklist Items for Checklist](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists.checklistId.checklistItems).
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Inspection Checklist Item History
                ResponseChekclistItemHistoryModel result = apiInstance.V4GetInspectionsInspectionIdChecklistsChecklistIdChecklistItemsChecklistItemIdHistories(contentType, authorization, inspectionId, checklistId, checklistItemId, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsChecklistsChecklistItemsApi.V4GetInspectionsInspectionIdChecklistsChecklistIdChecklistItemsChecklistItemIdHistories: " + e.Message );
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
 **inspectionId** | **string**| The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections). | 
 **checklistId** | **string**| The ID of the inspection checklist to fetch. See [Get All Inspection Checklists](./api-settings.html#operation/v4.get.settings.inspections.checklists), [Get All Checklists for Inspection](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists). | 
 **checklistItemId** | **string**| The ID of the inspection checklist item to fetch. See [Get All Checklist Items for Checklist](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists.checklistId.checklistItems). | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseChekclistItemHistoryModel**](ResponseChekclistItemHistoryModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getinspectionsinspectionidchecklistschecklistidchecklistitemschecklistitemidstatuses"></a>
# **V4GetInspectionsInspectionIdChecklistsChecklistIdChecklistItemsChecklistItemIdStatuses**
> ResponseChecklistItemStatusModelArray V4GetInspectionsInspectionIdChecklistsChecklistIdChecklistItemsChecklistItemIdStatuses (string contentType, string authorization, string inspectionId, string checklistId, string checklistItemId, string lang = null)

Get All Inspection Checklist Item Statuses

Gets historical (audit) data related to a checklist item for the specified inspection. **API Endpoint**:  GET /v4/inspections/{inspectionId}/checklists/{checklistId}/checklistItems/{checklistItemId}/statuses  **Scope**:  inspections  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4GetInspectionsInspectionIdChecklistsChecklistIdChecklistItemsChecklistItemIdStatusesExample
    {
        public void main()
        {
            var apiInstance = new InspectionsChecklistsChecklistItemsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var inspectionId = inspectionId_example;  // string | The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections).
            var checklistId = checklistId_example;  // string | The ID of the inspection checklist to fetch. See [Get All Inspection Checklists](./api-settings.html#operation/v4.get.settings.inspections.checklists), [Get All Checklists for Inspection](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists).
            var checklistItemId = checklistItemId_example;  // string | The ID of the inspection checklist item to fetch. See [Get All Checklist Items for Checklist](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists.checklistId.checklistItems).
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Inspection Checklist Item Statuses
                ResponseChecklistItemStatusModelArray result = apiInstance.V4GetInspectionsInspectionIdChecklistsChecklistIdChecklistItemsChecklistItemIdStatuses(contentType, authorization, inspectionId, checklistId, checklistItemId, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsChecklistsChecklistItemsApi.V4GetInspectionsInspectionIdChecklistsChecklistIdChecklistItemsChecklistItemIdStatuses: " + e.Message );
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
 **inspectionId** | **string**| The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections). | 
 **checklistId** | **string**| The ID of the inspection checklist to fetch. See [Get All Inspection Checklists](./api-settings.html#operation/v4.get.settings.inspections.checklists), [Get All Checklists for Inspection](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists). | 
 **checklistItemId** | **string**| The ID of the inspection checklist item to fetch. See [Get All Checklist Items for Checklist](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists.checklistId.checklistItems). | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseChecklistItemStatusModelArray**](ResponseChecklistItemStatusModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4postinspectionsinspectionidchecklistschecklistidchecklistitemschecklistitemiddocuments"></a>
# **V4PostInspectionsInspectionIdChecklistsChecklistIdChecklistItemsChecklistItemIdDocuments**
> ResponseResultModelArray V4PostInspectionsInspectionIdChecklistsChecklistIdChecklistItemsChecklistItemIdDocuments (string contentType, string authorization, long? inspectionId, long? checklistId, string checkListItemId, System.IO.Stream uploadedFile, string fileInfo, string group = null, string category = null, string userId = null, string password = null, string lang = null)

Create Checklist Item Documents

Attaches documents to a checklist item. **API Endpoint**:  POST /v4/inspections/{inspectionId}/checklists/{checklistId}/checklistItems/{checkListItemId}/documents  **Scope**:  inspections  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4PostInspectionsInspectionIdChecklistsChecklistIdChecklistItemsChecklistItemIdDocumentsExample
    {
        public void main()
        {
            var apiInstance = new InspectionsChecklistsChecklistItemsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var inspectionId = 789;  // long? | The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections).
            var checklistId = 789;  // long? | The ID of the inspection checklist to fetch. See [Get All Inspection Checklists](./api-settings.html#operation/v4.get.settings.inspections.checklists), [Get All Checklists for Inspection](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists).
            var checkListItemId = checkListItemId_example;  // string | The ID of the checklist item to fetch.
            var uploadedFile = new System.IO.Stream(); // System.IO.Stream | Specify the filename parameter with the file to be uploaded. See example for details.
            var fileInfo = fileInfo_example;  // string | A string array containing the file metadata for each specified filename. See example for details.
            var group = group_example;  // string | The document group. (optional) 
            var category = category_example;  // string | The document category. The list of category options varies depending on the document group. See [Get All Document Categories](./api-settings.html#operation/v4.get.settings.documents.categories). (optional) 
            var userId = userId_example;  // string | The standard EDMS adapter userid. It's required for user level authentication (optional) 
            var password = password_example;  // string | The standard EMDS adapter password. It's required for user level authentication (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Create Checklist Item Documents
                ResponseResultModelArray result = apiInstance.V4PostInspectionsInspectionIdChecklistsChecklistIdChecklistItemsChecklistItemIdDocuments(contentType, authorization, inspectionId, checklistId, checkListItemId, uploadedFile, fileInfo, group, category, userId, password, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsChecklistsChecklistItemsApi.V4PostInspectionsInspectionIdChecklistsChecklistIdChecklistItemsChecklistItemIdDocuments: " + e.Message );
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
 **inspectionId** | **long?**| The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections). | 
 **checklistId** | **long?**| The ID of the inspection checklist to fetch. See [Get All Inspection Checklists](./api-settings.html#operation/v4.get.settings.inspections.checklists), [Get All Checklists for Inspection](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists). | 
 **checkListItemId** | **string**| The ID of the checklist item to fetch. | 
 **uploadedFile** | **System.IO.Stream**| Specify the filename parameter with the file to be uploaded. See example for details. | 
 **fileInfo** | **string**| A string array containing the file metadata for each specified filename. See example for details. | 
 **group** | **string**| The document group. | [optional] 
 **category** | **string**| The document category. The list of category options varies depending on the document group. See [Get All Document Categories](./api-settings.html#operation/v4.get.settings.documents.categories). | [optional] 
 **userId** | **string**| The standard EDMS adapter userid. It&#39;s required for user level authentication | [optional] 
 **password** | **string**| The standard EMDS adapter password. It&#39;s required for user level authentication | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: multipart/form-data
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4putinspectionschecklistsidchecklistitems"></a>
# **V4PutInspectionsChecklistsIdChecklistItems**
> ResponseChecklistModelArray V4PutInspectionsChecklistsIdChecklistItems (string contentType, string authorization, string id, List<InspectionChecklistItemModel> body, string lang = null)

Update Checklist Items

Updates the checklist items for the specified checklist. **API Endpoint**:  PUT /v4/inspections/checklists/{id}/checklistItems  **Scope**:  inspections  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4PutInspectionsChecklistsIdChecklistItemsExample
    {
        public void main()
        {
            var apiInstance = new InspectionsChecklistsChecklistItemsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = id_example;  // string | The ID of the checklist to fetch.
            var body = new List<InspectionChecklistItemModel>(); // List<InspectionChecklistItemModel> | The checklist items to be updated.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Checklist Items
                ResponseChecklistModelArray result = apiInstance.V4PutInspectionsChecklistsIdChecklistItems(contentType, authorization, id, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsChecklistsChecklistItemsApi.V4PutInspectionsChecklistsIdChecklistItems: " + e.Message );
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
 **id** | **string**| The ID of the checklist to fetch. | 
 **body** | [**List&lt;InspectionChecklistItemModel&gt;**](InspectionChecklistItemModel.md)| The checklist items to be updated. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseChecklistModelArray**](ResponseChecklistModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

