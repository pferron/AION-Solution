# AccelaAssetsAndAssessments.Api.AssetsDocumentsApi

All URIs are relative to *https://apis.accela.com/*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4DeleteAssetsAssetIdDocumentsDocumentIds**](AssetsDocumentsApi.md#v4deleteassetsassetiddocumentsdocumentids) | **DELETE** /v4/assets/{assetId}/documents/{documentIds} | Delete Asset Documents
[**V4GetAssetsAssetIdDocuments**](AssetsDocumentsApi.md#v4getassetsassetiddocuments) | **GET** /v4/assets/{assetId}/documents | Get All Asset Documents
[**V4PostAssetsAssetIdDocuments**](AssetsDocumentsApi.md#v4postassetsassetiddocuments) | **POST** /v4/assets/{assetId}/documents | Create Asset Documents

<a name="v4deleteassetsassetiddocumentsdocumentids"></a>
# **V4DeleteAssetsAssetIdDocumentsDocumentIds**
> ResponseResultModelArray V4DeleteAssetsAssetIdDocumentsDocumentIds (string contentType, string authorization, string assetId, string documentIds, string userId = null, string password = null, string lang = null)

Delete Asset Documents

Deletes one or more documents for the given asset.    **API Endpoint**:  DELETE /v4/assets/{assetId}/documents/{documentIds}   **Scope**:  assets   **App Type**:  Agency   **Authorization Type**:  Access token   **Civic Platform version**: 9.0.0  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAssetsAndAssessments.Api;
using AccelaAssetsAndAssessments.Client;
using AccelaAssetsAndAssessments.Model;

namespace Example
{
    public class V4DeleteAssetsAssetIdDocumentsDocumentIdsExample
    {
        public void main()
        {
            var apiInstance = new AssetsDocumentsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var assetId = assetId_example;  // string | The ID of the asset to fetch. See [Get All Assets](./api-assets-assessments.html#operation/v4.get.assets).
            var documentIds = documentIds_example;  // string | Comma-delimited IDs of documents to be deleted.
            var userId = userId_example;  // string | The standard EDMS Adapter userid. It's required for user level authentication (optional) 
            var password = password_example;  // string | The standard EDMS Adapter password. It's required for user level authentication (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Delete Asset Documents
                ResponseResultModelArray result = apiInstance.V4DeleteAssetsAssetIdDocumentsDocumentIds(contentType, authorization, assetId, documentIds, userId, password, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AssetsDocumentsApi.V4DeleteAssetsAssetIdDocumentsDocumentIds: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | 
 **authorization** | **string**| Construct oAuth2 authentication token | 
 **assetId** | **string**| The ID of the asset to fetch. See [Get All Assets](./api-assets-assessments.html#operation/v4.get.assets). | 
 **documentIds** | **string**| Comma-delimited IDs of documents to be deleted. | 
 **userId** | **string**| The standard EDMS Adapter userid. It&#x27;s required for user level authentication | [optional] 
 **password** | **string**| The standard EDMS Adapter password. It&#x27;s required for user level authentication | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="v4getassetsassetiddocuments"></a>
# **V4GetAssetsAssetIdDocuments**
> ResponseDocumentModelArray V4GetAssetsAssetIdDocuments (string contentType, string authorization, string assetId, long? limit = null, long? offset = null, string fields = null, string lang = null)

Get All Asset Documents

Returns all documents for a given asset.    **API Endpoint**:  GET /v4/assets/{assetId}/documents   **Scope**:  assets   **App Type**:  Agency   **Authorization Type**:  Access token   **Civic Platform version**: 9.0.0  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAssetsAndAssessments.Api;
using AccelaAssetsAndAssessments.Client;
using AccelaAssetsAndAssessments.Model;

namespace Example
{
    public class V4GetAssetsAssetIdDocumentsExample
    {
        public void main()
        {
            var apiInstance = new AssetsDocumentsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var assetId = assetId_example;  // string | The ID of the asset to fetch. See [Get All Assets](./api-assets-assessments.html#operation/v4.get.assets).
            var limit = 789;  // long? | Search result size limit. (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Asset Documents
                ResponseDocumentModelArray result = apiInstance.V4GetAssetsAssetIdDocuments(contentType, authorization, assetId, limit, offset, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AssetsDocumentsApi.V4GetAssetsAssetIdDocuments: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | 
 **authorization** | **string**| Construct oAuth2 authentication token | 
 **assetId** | **string**| The ID of the asset to fetch. See [Get All Assets](./api-assets-assessments.html#operation/v4.get.assets). | 
 **limit** | **long?**| Search result size limit. | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseDocumentModelArray**](ResponseDocumentModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="v4postassetsassetiddocuments"></a>
# **V4PostAssetsAssetIdDocuments**
> ResponseResultModelArray V4PostAssetsAssetIdDocuments (string contentType, string authorization, string assetId, byte[] uploadedFile = null, string fileInfo = null, string group = null, string category = null, string userId = null, string password = null, string lang = null)

Create Asset Documents

Creates one or more document attachments for the given asset. To specify the documents to be attached, use the HTTP header \"Content-Type:multipart/form-data\" and form-data for \"uploadedFile\" and \"fileInfo\". Note that the \"fileInfo\" is a string containing an array of file attributes. Use \"fileInfo\" to specify one or more documents to be attached. For example:   Content - Disposition: form - data;name = \"uploadedFile\"; filename=\"summaryReport.pdf\"   Content - Disposition: form - data;name = \"fileInfo\"   [    {    \"serviceProviderCode\": \"BPTDEV\",    \"fileName\": \"CXA12-pipe.png\",    \"type\": \"image/png\",    \"description\": \"Inspected pipe\"    }  ]         **API Endpoint**:  POST /v4/assets/{assetId}/documents   **Scope**:  assets   **App Type**:  Agency   **Authorization Type**:  Access token   **Civic Platform version**: 9.0.0  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAssetsAndAssessments.Api;
using AccelaAssetsAndAssessments.Client;
using AccelaAssetsAndAssessments.Model;

namespace Example
{
    public class V4PostAssetsAssetIdDocumentsExample
    {
        public void main()
        {
            var apiInstance = new AssetsDocumentsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var assetId = assetId_example;  // string | The ID of the asset to fetch. See [Get All Assets](./api-assets-assessments.html#operation/v4.get.assets).
            var uploadedFile = uploadedFile_example;  // byte[] |  (optional) 
            var fileInfo = fileInfo_example;  // string |  (optional) 
            var group = group_example;  // string | The document group. (optional) 
            var category = category_example;  // string | The document category. (optional) 
            var userId = userId_example;  // string | The EDMS Adapter User ID. It's required for user level authentication (optional) 
            var password = password_example;  // string | The EMDS Adapter password. It's required for user level authentication (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Create Asset Documents
                ResponseResultModelArray result = apiInstance.V4PostAssetsAssetIdDocuments(contentType, authorization, assetId, uploadedFile, fileInfo, group, category, userId, password, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AssetsDocumentsApi.V4PostAssetsAssetIdDocuments: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | 
 **authorization** | **string**| Construct oAuth2 authentication token | 
 **assetId** | **string**| The ID of the asset to fetch. See [Get All Assets](./api-assets-assessments.html#operation/v4.get.assets). | 
 **uploadedFile** | **byte[]****byte[]**|  | [optional] 
 **fileInfo** | **string**|  | [optional] 
 **group** | **string**| The document group. | [optional] 
 **category** | **string**| The document category. | [optional] 
 **userId** | **string**| The EDMS Adapter User ID. It&#x27;s required for user level authentication | [optional] 
 **password** | **string**| The EMDS Adapter password. It&#x27;s required for user level authentication | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: multipart/form-data
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
