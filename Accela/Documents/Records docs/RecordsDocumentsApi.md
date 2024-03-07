# AccelaRecords.Api.RecordsDocumentsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4DeleteRecordsRecordIdDocumentsDocumentIds**](RecordsDocumentsApi.md#v4deleterecordsrecordiddocumentsdocumentids) | **DELETE** /v4/records/{recordId}/documents/{documentIds} | Delete Record Documents
[**V4GetRecordsRecordIdDocumentCategories**](RecordsDocumentsApi.md#v4getrecordsrecordiddocumentcategories) | **GET** /v4/records/{recordId}/documentCategories | Get All Document Categories for Record
[**V4GetRecordsRecordIdDocuments**](RecordsDocumentsApi.md#v4getrecordsrecordiddocuments) | **GET** /v4/records/{recordId}/documents | Get All Documents for Record
[**V4PostRecordsRecordIdDocuments**](RecordsDocumentsApi.md#v4postrecordsrecordiddocuments) | **POST** /v4/records/{recordId}/documents | Create Record Documents


<a name="v4deleterecordsrecordiddocumentsdocumentids"></a>
# **V4DeleteRecordsRecordIdDocumentsDocumentIds**
> ResponseResultModelArray V4DeleteRecordsRecordIdDocumentsDocumentIds (string contentType, string authorization, string recordId, string documentIds, string userId, string password, string lang = null)

Delete Record Documents

Deletes documents attached to a record. **API Endpoint**:  DELETE /v4/records/{recordId}/documents/{documentIds}  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4DeleteRecordsRecordIdDocumentsDocumentIdsExample
    {
        public void main()
        {
            var apiInstance = new RecordsDocumentsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var documentIds = documentIds_example;  // string | Comma-delimited IDs of the documents to be deleted.
            var userId = userId_example;  // string | The EDMS Adapter User ID. It's required for user level authentication
            var password = password_example;  // string | The EMDS Adapter password. It's required for user level authentication
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Delete Record Documents
                ResponseResultModelArray result = apiInstance.V4DeleteRecordsRecordIdDocumentsDocumentIds(contentType, authorization, recordId, documentIds, userId, password, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsDocumentsApi.V4DeleteRecordsRecordIdDocumentsDocumentIds: " + e.Message );
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
 **recordId** | **string**| The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine). | 
 **documentIds** | **string**| Comma-delimited IDs of the documents to be deleted. | 
 **userId** | **string**| The EDMS Adapter User ID. It&#39;s required for user level authentication | 
 **password** | **string**| The EMDS Adapter password. It&#39;s required for user level authentication | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getrecordsrecordiddocumentcategories"></a>
# **V4GetRecordsRecordIdDocumentCategories**
> ResponseDocumentTypeModelArray V4GetRecordsRecordIdDocumentCategories (string contentType, string authorization, string recordId, string lang = null)

Get All Document Categories for Record

Gets the document types associated with the specified record. **API Endpoint**:  GET /v4/records/{recordId}/documentCategories  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdDocumentCategoriesExample
    {
        public void main()
        {
            var apiInstance = new RecordsDocumentsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Document Categories for Record
                ResponseDocumentTypeModelArray result = apiInstance.V4GetRecordsRecordIdDocumentCategories(contentType, authorization, recordId, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsDocumentsApi.V4GetRecordsRecordIdDocumentCategories: " + e.Message );
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
 **recordId** | **string**| The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine). | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseDocumentTypeModelArray**](ResponseDocumentTypeModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getrecordsrecordiddocuments"></a>
# **V4GetRecordsRecordIdDocuments**
> ResponseDocumentModelArray V4GetRecordsRecordIdDocuments (string contentType, string authorization, string recordId, string fields = null, string lang = null)

Get All Documents for Record

Gets the documents associated with the specified record. **API Endpoint**:  GET /v4/records/{recordId}/documents  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdDocumentsExample
    {
        public void main()
        {
            var apiInstance = new RecordsDocumentsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Documents for Record
                ResponseDocumentModelArray result = apiInstance.V4GetRecordsRecordIdDocuments(contentType, authorization, recordId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsDocumentsApi.V4GetRecordsRecordIdDocuments: " + e.Message );
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
 **recordId** | **string**| The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine). | 
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

<a name="v4postrecordsrecordiddocuments"></a>
# **V4PostRecordsRecordIdDocuments**
> ResponseResultModelArray V4PostRecordsRecordIdDocuments (string contentType, string authorization, string recordId, System.IO.Stream uploadedFile, string fileInfo, string group = null, string category = null, string userId = null, string password = null, string lang = null)

Create Record Documents

Creates one or more document attachments for the specified record. To specify the documents to be attached, use the HTTP header \"Content-Type:multipart/form-data\" and form-data for \"uploadedFile\" and \"fileInfo\". Note that the \"fileInfo\" is a string containing an array of file attributes. Use \"fileInfo\" to specify one or more documents to be attached. See the example for details. **API Endpoint**:  POST /v4/records/{recordId}/documents   **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4PostRecordsRecordIdDocumentsExample
    {
        public void main()
        {
            var apiInstance = new RecordsDocumentsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var uploadedFile = new System.IO.Stream(); // System.IO.Stream | Specify the filename parameter with the file to be uploaded. See example for details.
            var fileInfo = fileInfo_example;  // string | A string array containing the file metadata for each specified filename. See example for details.
            var group = group_example;  // string | The document group (optional) 
            var category = category_example;  // string | The document category (optional) 
            var userId = userId_example;  // string | The EDMS Adapter User ID. It's required for user level authentication (optional) 
            var password = password_example;  // string | The EMDS Adapter password. It's required for user level authentication (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Create Record Documents
                ResponseResultModelArray result = apiInstance.V4PostRecordsRecordIdDocuments(contentType, authorization, recordId, uploadedFile, fileInfo, group, category, userId, password, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsDocumentsApi.V4PostRecordsRecordIdDocuments: " + e.Message );
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
 **recordId** | **string**| The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine). | 
 **uploadedFile** | **System.IO.Stream**| Specify the filename parameter with the file to be uploaded. See example for details. | 
 **fileInfo** | **string**| A string array containing the file metadata for each specified filename. See example for details. | 
 **group** | **string**| The document group | [optional] 
 **category** | **string**| The document category | [optional] 
 **userId** | **string**| The EDMS Adapter User ID. It&#39;s required for user level authentication | [optional] 
 **password** | **string**| The EMDS Adapter password. It&#39;s required for user level authentication | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: multipart/form-data
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

