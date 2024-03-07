# AccelaRecords.Api.RecordsConditionApprovalsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4DeleteRecordsRecordIdConditionApprovalsIds**](RecordsConditionApprovalsApi.md#v4deleterecordsrecordidconditionapprovalsids) | **DELETE** /v4/Records/{recordId}/conditionApprovals/{ids} | Delete Record Approval Conditions
[**V4GetRecordsRecordIdConditionApprovals**](RecordsConditionApprovalsApi.md#v4getrecordsrecordidconditionapprovals) | **GET** /v4/records/{recordId}/conditionApprovals | Get All Approval Conditions for Record
[**V4GetRecordsRecordIdConditionApprovalsId**](RecordsConditionApprovalsApi.md#v4getrecordsrecordidconditionapprovalsid) | **GET** /v4/records/{recordId}/conditionApprovals/{id} | Get Record Approval Condition
[**V4PostRecordsRecordIdConditionApprovals**](RecordsConditionApprovalsApi.md#v4postrecordsrecordidconditionapprovals) | **POST** /v4/records/{recordId}/conditionApprovals | Create Record Approval Conditions
[**V4PutRecordsRecordIdConditionApprovalsId**](RecordsConditionApprovalsApi.md#v4putrecordsrecordidconditionapprovalsid) | **PUT** /v4/records/{recordId}/conditionApprovals/{id} | Update Record Approval Condition


<a name="v4deleterecordsrecordidconditionapprovalsids"></a>
# **V4DeleteRecordsRecordIdConditionApprovalsIds**
> ResponseResultModelArray V4DeleteRecordsRecordIdConditionApprovalsIds (string contentType, string authorization, string recordId, string ids, string lang = null)

Delete Record Approval Conditions

Deletes approval conditions for the specified record. **API Endpoint**:  DELETE /v4/records/{recordId}/conditionApprovals/{ids}  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4DeleteRecordsRecordIdConditionApprovalsIdsExample
    {
        public void main()
        {
            var apiInstance = new RecordsConditionApprovalsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var ids = ids_example;  // string | Comma-delimited IDs of the approval conditions to be deleted.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Delete Record Approval Conditions
                ResponseResultModelArray result = apiInstance.V4DeleteRecordsRecordIdConditionApprovalsIds(contentType, authorization, recordId, ids, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsConditionApprovalsApi.V4DeleteRecordsRecordIdConditionApprovalsIds: " + e.Message );
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
 **ids** | **string**| Comma-delimited IDs of the approval conditions to be deleted. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getrecordsrecordidconditionapprovals"></a>
# **V4GetRecordsRecordIdConditionApprovals**
> List<RecordConditionModel> V4GetRecordsRecordIdConditionApprovals (string contentType, string authorization, string recordId, string fields = null, string lang = null)

Get All Approval Conditions for Record

Gets the conditions of approval for the specified record(s). **API Endpoint**:  GET /v4/records/{recordId}/conditionApprovals  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdConditionApprovalsExample
    {
        public void main()
        {
            var apiInstance = new RecordsConditionApprovalsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Approval Conditions for Record
                List&lt;RecordConditionModel&gt; result = apiInstance.V4GetRecordsRecordIdConditionApprovals(contentType, authorization, recordId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsConditionApprovalsApi.V4GetRecordsRecordIdConditionApprovals: " + e.Message );
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

[**List<RecordConditionModel>**](RecordConditionModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getrecordsrecordidconditionapprovalsid"></a>
# **V4GetRecordsRecordIdConditionApprovalsId**
> ResponseRecordConditionModelArray V4GetRecordsRecordIdConditionApprovalsId (string contentType, string authorization, string recordId, string id, string fields = null, string lang = null)

Get Record Approval Condition

Gets the specified condition of approvals for the specified record(s). **API Endpoint**: GET /v4/records/{recordId}/conditionApprovals/{id}   **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdConditionApprovalsIdExample
    {
        public void main()
        {
            var apiInstance = new RecordsConditionApprovalsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var id = id_example;  // string | The ID of record condition approval to fetch.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Record Approval Condition
                ResponseRecordConditionModelArray result = apiInstance.V4GetRecordsRecordIdConditionApprovalsId(contentType, authorization, recordId, id, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsConditionApprovalsApi.V4GetRecordsRecordIdConditionApprovalsId: " + e.Message );
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
 **id** | **string**| The ID of record condition approval to fetch. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseRecordConditionModelArray**](ResponseRecordConditionModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4postrecordsrecordidconditionapprovals"></a>
# **V4PostRecordsRecordIdConditionApprovals**
> ResponseResultModelArray V4PostRecordsRecordIdConditionApprovals (string contentType, string authorization, string recordId, List<RecordConditionModel> body, string lang = null)

Create Record Approval Conditions

Adds approval conditions to the specified record. **API Endpoint**:  POST /v4/records/{recordId}/conditionApprovals  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4PostRecordsRecordIdConditionApprovalsExample
    {
        public void main()
        {
            var apiInstance = new RecordsConditionApprovalsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var body = new List<RecordConditionModel>(); // List<RecordConditionModel> | Record condition information to be added.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Create Record Approval Conditions
                ResponseResultModelArray result = apiInstance.V4PostRecordsRecordIdConditionApprovals(contentType, authorization, recordId, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsConditionApprovalsApi.V4PostRecordsRecordIdConditionApprovals: " + e.Message );
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
 **body** | [**List&lt;RecordConditionModel&gt;**](RecordConditionModel.md)| Record condition information to be added. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4putrecordsrecordidconditionapprovalsid"></a>
# **V4PutRecordsRecordIdConditionApprovalsId**
> ResponseRecordConditionModelArray V4PutRecordsRecordIdConditionApprovalsId (string contentType, string authorization, string recordId, string id, RequestRecordConditionModel body, string fields = null, string lang = null)

Update Record Approval Condition

Updates the condition of approvals for the specified record(s). **API Endpoint**:  PUT /v4/records/{recordId}/conditionApprovals/{id}  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4PutRecordsRecordIdConditionApprovalsIdExample
    {
        public void main()
        {
            var apiInstance = new RecordsConditionApprovalsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var id = id_example;  // string | The ID of the condition approval to update.
            var body = new RequestRecordConditionModel(); // RequestRecordConditionModel | The condition approval information to be updated.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Record Approval Condition
                ResponseRecordConditionModelArray result = apiInstance.V4PutRecordsRecordIdConditionApprovalsId(contentType, authorization, recordId, id, body, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsConditionApprovalsApi.V4PutRecordsRecordIdConditionApprovalsId: " + e.Message );
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
 **id** | **string**| The ID of the condition approval to update. | 
 **body** | [**RequestRecordConditionModel**](RequestRecordConditionModel.md)| The condition approval information to be updated. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseRecordConditionModelArray**](ResponseRecordConditionModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

