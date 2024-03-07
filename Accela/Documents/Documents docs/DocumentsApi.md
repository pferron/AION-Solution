# AccelaDocuments.Api.DocumentsApi

All URIs are relative to *https://apis.accela.com/*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetDocumentsDocumentIdDownload**](DocumentsApi.md#v4getdocumentsdocumentiddownload) | **GET** /v4/documents/{documentId}/download | Download Document
[**V4GetDocumentsDocumentIdThumbnail**](DocumentsApi.md#v4getdocumentsdocumentidthumbnail) | **GET** /v4/documents/{documentId}/thumbnail | Get Image Document Thumbnail
[**V4GetDocumentsDocumentIds**](DocumentsApi.md#v4getdocumentsdocumentids) | **GET** /v4/documents/{documentIds} | Get Documents
[**V4PutDocumentsDocumentId**](DocumentsApi.md#v4putdocumentsdocumentid) | **PUT** /v4/Documents/{documentId} | Update Document

<a name="v4getdocumentsdocumentiddownload"></a>
# **V4GetDocumentsDocumentIdDownload**
> InlineResponse200 V4GetDocumentsDocumentIdDownload (string contentType, string authorization, string documentId, string userId = null, string password = null, string lang = null)

Download Document

Downloads the requested document. **API Endpoint**:  GET /v4/documents/{documentId}/download  **Scope**:  documents  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaDocuments.Api;
using AccelaDocuments.Client;
using AccelaDocuments.Model;

namespace Example
{
    public class V4GetDocumentsDocumentIdDownloadExample
    {
        public void main()
        {
            var apiInstance = new DocumentsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var documentId = documentId_example;  // string | The ID of document to fetch.
            var userId = userId_example;  // string | The EDMS userid for user level authentication. (optional) 
            var password = password_example;  // string | The EDMS password for user level authentication. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Download Document
                InlineResponse200 result = apiInstance.V4GetDocumentsDocumentIdDownload(contentType, authorization, documentId, userId, password, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DocumentsApi.V4GetDocumentsDocumentIdDownload: " + e.Message );
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
 **documentId** | **string**| The ID of document to fetch. | 
 **userId** | **string**| The EDMS userid for user level authentication. | [optional] 
 **password** | **string**| The EDMS password for user level authentication. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**InlineResponse200**](InlineResponse200.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/octet-stream

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="v4getdocumentsdocumentidthumbnail"></a>
# **V4GetDocumentsDocumentIdThumbnail**
> byte[] V4GetDocumentsDocumentIdThumbnail (string contentType, string authorization, string documentId, int? height = null, int? width = null)

Get Image Document Thumbnail

Gets the thumbnail for the requested {documentId} of an image file. **API Endpoint**:  GET /v4/documents/{documentId}/thumbnail  **Scope**:  documents  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaDocuments.Api;
using AccelaDocuments.Client;
using AccelaDocuments.Model;

namespace Example
{
    public class V4GetDocumentsDocumentIdThumbnailExample
    {
        public void main()
        {
            var apiInstance = new DocumentsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var documentId = documentId_example;  // string | The ID of document to fetch.
            var height = 56;  // int? | The height of the thumbnail. (optional) 
            var width = 56;  // int? | The width of the thumbnail. (optional) 

            try
            {
                // Get Image Document Thumbnail
                byte[] result = apiInstance.V4GetDocumentsDocumentIdThumbnail(contentType, authorization, documentId, height, width);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DocumentsApi.V4GetDocumentsDocumentIdThumbnail: " + e.Message );
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
 **documentId** | **string**| The ID of document to fetch. | 
 **height** | **int?**| The height of the thumbnail. | [optional] 
 **width** | **int?**| The width of the thumbnail. | [optional] 

### Return type

**byte[]**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/octet-stream

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="v4getdocumentsdocumentids"></a>
# **V4GetDocumentsDocumentIds**
> ResponseDocumentModelArray V4GetDocumentsDocumentIds (string contentType, string authorization, string documentIds, string fields = null, string lang = null)

Get Documents

Gets a list of requested documents. **API Endpoint**:  GET /v4/documents/{documentIds}  **Scope**:  documents  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaDocuments.Api;
using AccelaDocuments.Client;
using AccelaDocuments.Model;

namespace Example
{
    public class V4GetDocumentsDocumentIdsExample
    {
        public void main()
        {
            var apiInstance = new DocumentsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var documentIds = documentIds_example;  // string | Comma-delimited document ID's.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Documents
                ResponseDocumentModelArray result = apiInstance.V4GetDocumentsDocumentIds(contentType, authorization, documentIds, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DocumentsApi.V4GetDocumentsDocumentIds: " + e.Message );
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
 **documentIds** | **string**| Comma-delimited document ID&#x27;s. | 
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
<a name="v4putdocumentsdocumentid"></a>
# **V4PutDocumentsDocumentId**
> ResponseDocumentModel V4PutDocumentsDocumentId (string contentType, string authorization, string documentId, DocumentModel body = null, string lang = null)

Update Document

Updates the specified document. **API Endpoint**:  PUT /v4/documents/{documentId}  **Scope**:  documents  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**:  7.3.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaDocuments.Api;
using AccelaDocuments.Client;
using AccelaDocuments.Model;

namespace Example
{
    public class V4PutDocumentsDocumentIdExample
    {
        public void main()
        {
            var apiInstance = new DocumentsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var documentId = documentId_example;  // string | The ID of document to fetch.
            var body = new DocumentModel(); // DocumentModel | Document request information. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Document
                ResponseDocumentModel result = apiInstance.V4PutDocumentsDocumentId(contentType, authorization, documentId, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DocumentsApi.V4PutDocumentsDocumentId: " + e.Message );
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
 **documentId** | **string**| The ID of document to fetch. | 
 **body** | [**DocumentModel**](DocumentModel.md)| Document request information. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseDocumentModel**](ResponseDocumentModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
