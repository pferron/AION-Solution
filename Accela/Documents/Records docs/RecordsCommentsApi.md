# AccelaRecords.Api.RecordsCommentsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4DeleteRecordsRecordIdCommentsIds**](RecordsCommentsApi.md#v4deleterecordsrecordidcommentsids) | **DELETE** /v4/Records/{recordId}/comments/{ids} | Delete Record Comments
[**V4GetRecordsRecordIdComments**](RecordsCommentsApi.md#v4getrecordsrecordidcomments) | **GET** /v4/records/{recordId}/comments | Get All Comments for Record
[**V4PostRecordsRecordIdComments**](RecordsCommentsApi.md#v4postrecordsrecordidcomments) | **POST** /v4/records/{recordId}/comments | Add Comments to a Record
[**V4PutRecordsRecordIdCommentsId**](RecordsCommentsApi.md#v4putrecordsrecordidcommentsid) | **PUT** /v4/records/{recordId}/comments/{id} | Update Record Comment


<a name="v4deleterecordsrecordidcommentsids"></a>
# **V4DeleteRecordsRecordIdCommentsIds**
> ResponseResultModelArray V4DeleteRecordsRecordIdCommentsIds (string contentType, string authorization, string recordId, string ids, string lang = null)

Delete Record Comments

Deletes the specified comment(s) for the specified record. **API Endpoint**:  DELETE /v4/records/{recordId}/comments/{idS}  **Scope**:   **API Endpoint**:   **Scope**:  addresses  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2   **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4DeleteRecordsRecordIdCommentsIdsExample
    {
        public void main()
        {
            var apiInstance = new RecordsCommentsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var ids = ids_example;  // string | Comma-delimited IDs of the record comments to be deleted.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Delete Record Comments
                ResponseResultModelArray result = apiInstance.V4DeleteRecordsRecordIdCommentsIds(contentType, authorization, recordId, ids, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsCommentsApi.V4DeleteRecordsRecordIdCommentsIds: " + e.Message );
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
 **ids** | **string**| Comma-delimited IDs of the record comments to be deleted. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getrecordsrecordidcomments"></a>
# **V4GetRecordsRecordIdComments**
> ResponseRecordCommentModelArray V4GetRecordsRecordIdComments (string contentType, string authorization, string recordId, string fields = null, string lang = null)

Get All Comments for Record

Gets comments associated to a record. **API Endpoint**:  GET /v4/records/{recordId}/comments  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdCommentsExample
    {
        public void main()
        {
            var apiInstance = new RecordsCommentsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Comments for Record
                ResponseRecordCommentModelArray result = apiInstance.V4GetRecordsRecordIdComments(contentType, authorization, recordId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsCommentsApi.V4GetRecordsRecordIdComments: " + e.Message );
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

[**ResponseRecordCommentModelArray**](ResponseRecordCommentModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4postrecordsrecordidcomments"></a>
# **V4PostRecordsRecordIdComments**
> ResponseResultModelArray V4PostRecordsRecordIdComments (string contentType, string authorization, string recordId, List<CommentModel> body = null, string lang = null)

Add Comments to a Record

Add comments to a record. **API Endpoint**:  POST /v4/records/{recordId}/comments   **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4PostRecordsRecordIdCommentsExample
    {
        public void main()
        {
            var apiInstance = new RecordsCommentsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var body = new List<CommentModel>(); // List<CommentModel> | Record comments to be added. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Add Comments to a Record
                ResponseResultModelArray result = apiInstance.V4PostRecordsRecordIdComments(contentType, authorization, recordId, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsCommentsApi.V4PostRecordsRecordIdComments: " + e.Message );
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
 **body** | [**List&lt;CommentModel&gt;**](CommentModel.md)| Record comments to be added. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4putrecordsrecordidcommentsid"></a>
# **V4PutRecordsRecordIdCommentsId**
> ResponseRecordCommentModel V4PutRecordsRecordIdCommentsId (string authorization, string recordId, long? id, CommentModel body = null, string fields = null, string lang = null)

Update Record Comment

Update a record comment. **API Endpoint**:  PUT /v4/records/{recordId}/comments/{id}  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4PutRecordsRecordIdCommentsIdExample
    {
        public void main()
        {
            var apiInstance = new RecordsCommentsApi();
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var id = 789;  // long? | The ID of the comment to update.
            var body = new CommentModel(); // CommentModel | Comments to be updated. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Record Comment
                ResponseRecordCommentModel result = apiInstance.V4PutRecordsRecordIdCommentsId(authorization, recordId, id, body, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsCommentsApi.V4PutRecordsRecordIdCommentsId: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **authorization** | **string**| Construct oAuth2 authentication token | 
 **recordId** | **string**| The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine). | 
 **id** | **long?**| The ID of the comment to update. | 
 **body** | [**CommentModel**](CommentModel.md)| Comments to be updated. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseRecordCommentModel**](ResponseRecordCommentModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

