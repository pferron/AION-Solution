# AccelaInspections.Api.InspectionsDocumentsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4DeleteInspectionsInspectionIdDocumentsIds**](InspectionsDocumentsApi.md#v4deleteinspectionsinspectioniddocumentsids) | **DELETE** /v4/inspections/{inspectionId}/documents/{ids} | Delete Inspection Documents
[**V4GetInspectionsInspectionIdDocuments**](InspectionsDocumentsApi.md#v4getinspectionsinspectioniddocuments) | **GET** /v4/inspections/{inspectionId}/documents | Get All Documents for Inspection
[**V4PostInspectionsInspectionIdDocuments**](InspectionsDocumentsApi.md#v4postinspectionsinspectioniddocuments) | **POST** /v4/inspections/{inspectionId}/documents | Create Inspection Documents


<a name="v4deleteinspectionsinspectioniddocumentsids"></a>
# **V4DeleteInspectionsInspectionIdDocumentsIds**
> ResponseResultModelArray V4DeleteInspectionsInspectionIdDocumentsIds (string contentType, string authorization, string inspectionId, string ids, string userId = null, string password = null, string lang = null)

Delete Inspection Documents

Deletes documents from the specified inspection. **API Endpoint**:  DELETE /v4/inspections/{inspectionId}/documents/{ids}  **Scope**:  inspections  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4DeleteInspectionsInspectionIdDocumentsIdsExample
    {
        public void main()
        {
            var apiInstance = new InspectionsDocumentsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var inspectionId = inspectionId_example;  // string | Inspection sequence number
            var ids = ids_example;  // string | Comma-delimited IDs of documents to be deleted.
            var userId = userId_example;  // string | The standard EDMS Adapter userid. It's required for user level authentication (optional) 
            var password = password_example;  // string | The standard EMDS Adapter password. It's required for user level authentication (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Delete Inspection Documents
                ResponseResultModelArray result = apiInstance.V4DeleteInspectionsInspectionIdDocumentsIds(contentType, authorization, inspectionId, ids, userId, password, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsDocumentsApi.V4DeleteInspectionsInspectionIdDocumentsIds: " + e.Message );
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
 **inspectionId** | **string**| Inspection sequence number | 
 **ids** | **string**| Comma-delimited IDs of documents to be deleted. | 
 **userId** | **string**| The standard EDMS Adapter userid. It&#39;s required for user level authentication | [optional] 
 **password** | **string**| The standard EMDS Adapter password. It&#39;s required for user level authentication | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getinspectionsinspectioniddocuments"></a>
# **V4GetInspectionsInspectionIdDocuments**
> ResponseDocumentModelArray V4GetInspectionsInspectionIdDocuments (string contentType, string authorization, long? inspectionId, string fields = null, string lang = null)

Get All Documents for Inspection

Gets the documents for the specified inspection. **API Endpoint**:  GET /v4/inspections/{inspectionId}/documents  **Scope**:  inspections  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4GetInspectionsInspectionIdDocumentsExample
    {
        public void main()
        {
            var apiInstance = new InspectionsDocumentsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var inspectionId = 789;  // long? | The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections).
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Documents for Inspection
                ResponseDocumentModelArray result = apiInstance.V4GetInspectionsInspectionIdDocuments(contentType, authorization, inspectionId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsDocumentsApi.V4GetInspectionsInspectionIdDocuments: " + e.Message );
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

<a name="v4postinspectionsinspectioniddocuments"></a>
# **V4PostInspectionsInspectionIdDocuments**
> ResponseResultModelArray V4PostInspectionsInspectionIdDocuments (string contentType, string authorization, string inspectionId, System.IO.Stream uploadedFile, string fileInfo, string group = null, string category = null, string userId = null, string password = null, string lang = null)

Create Inspection Documents

Creates one or more document attachments for the specified inspection. To specify the documents to be attached, use the HTTP header \"Content-Type:multipart/form-data\"and form-data for \"uploadedFile\"and \"fileInfo\". Note that the \"fileInfo\"is a string containing an array of file attributes. Use \"fileInfo\"to specify one or more documents to be attached. For example:  Content - Disposition: form - data;name = \"uploadedFile\"; filename=\"summaryReport.pdf\"  Content - Disposition: form - data;name = \"fileInfo\"  [    {    \"serviceProviderCode\": \"BPTDEV\",    \"fileName\": \"CXA12-pipe.png\",    \"type\": \"image/png\",    \"description\": \"Inspected pipe\"    }  ] **API Endpoint**: POST /v4/inspections/{inspectionId}/documents   **Scope** : inspections  **App Type** : All  **Authorization Type** : Access token  **Civic Platform version** : 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4PostInspectionsInspectionIdDocumentsExample
    {
        public void main()
        {
            var apiInstance = new InspectionsDocumentsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var inspectionId = inspectionId_example;  // string | Inspection sequence number
            var uploadedFile = new System.IO.Stream(); // System.IO.Stream | Specify the filename parameter with the file to be uploaded. See example for details.
            var fileInfo = fileInfo_example;  // string | A string array containing the file metadata for each specified filename. See example for details.
            var group = group_example;  // string | The document group. (optional) 
            var category = category_example;  // string | The document category. (optional) 
            var userId = userId_example;  // string | The standard EDMS Adapter userid. It's required for user level authentication (optional) 
            var password = password_example;  // string | The standard EMDS Adapter password. It's required for user level authentication (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Create Inspection Documents
                ResponseResultModelArray result = apiInstance.V4PostInspectionsInspectionIdDocuments(contentType, authorization, inspectionId, uploadedFile, fileInfo, group, category, userId, password, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsDocumentsApi.V4PostInspectionsInspectionIdDocuments: " + e.Message );
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
 **inspectionId** | **string**| Inspection sequence number | 
 **uploadedFile** | **System.IO.Stream**| Specify the filename parameter with the file to be uploaded. See example for details. | 
 **fileInfo** | **string**| A string array containing the file metadata for each specified filename. See example for details. | 
 **group** | **string**| The document group. | [optional] 
 **category** | **string**| The document category. | [optional] 
 **userId** | **string**| The standard EDMS Adapter userid. It&#39;s required for user level authentication | [optional] 
 **password** | **string**| The standard EMDS Adapter password. It&#39;s required for user level authentication | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: multipart/form-data
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

