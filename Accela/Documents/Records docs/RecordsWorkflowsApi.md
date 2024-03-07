# AccelaRecords.Api.RecordsWorkflowsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetRecordsRecordIdWorkflowTasks**](RecordsWorkflowsApi.md#v4getrecordsrecordidworkflowtasks) | **GET** /v4/records/{recordId}/workflowTasks | Get All Workflow Tasks for Record
[**V4GetRecordsRecordIdWorkflowTasksCommentsHistories**](RecordsWorkflowsApi.md#v4getrecordsrecordidworkflowtaskscommentshistories) | **GET** /v4/records/{recordId}/workflowTasks/comments/histories | Get Workflow Task Comment Histories
[**V4GetRecordsRecordIdWorkflowTasksHistories**](RecordsWorkflowsApi.md#v4getrecordsrecordidworkflowtaskshistories) | **GET** /v4/records/{recordId}/workflowTasks/histories | Get All Workflow Task History for Record
[**V4GetRecordsRecordIdWorkflowTasksId**](RecordsWorkflowsApi.md#v4getrecordsrecordidworkflowtasksid) | **GET** /v4/records/{recordId}/workflowTasks/{id} | Get Record Workflow Task
[**V4GetRecordsRecordIdWorkflowTasksIdStatuses**](RecordsWorkflowsApi.md#v4getrecordsrecordidworkflowtasksidstatuses) | **GET** /v4/records/{recordId}/workflowTasks/{id}/statuses | Get All Statuses for Workflow Task
[**V4GetRecordsRecordIdWorkflowTasksTaskIdCustomForms**](RecordsWorkflowsApi.md#v4getrecordsrecordidworkflowtaskstaskidcustomforms) | **GET** /v4/records/{recordId}/workflowTasks/{taskId}/customForms | Get All Custom Forms for Record Workflow Task
[**V4GetRecordsRecordIdWorkflowTasksTaskIdCustomFormsMeta**](RecordsWorkflowsApi.md#v4getrecordsrecordidworkflowtaskstaskidcustomformsmeta) | **GET** /v4/records/{recordId}/workflowTasks/{taskId}/customForms/meta | Get All Custom Forms Metadata for Record Workflow Task
[**V4PutRecordsRecordIdWorkflowTasksId**](RecordsWorkflowsApi.md#v4putrecordsrecordidworkflowtasksid) | **PUT** /v4/records/{recordId}/workflowTasks/{id} | Update Record Workflow Task
[**V4PutRecordsRecordIdWorkflowTasksTaskIdCustomForms**](RecordsWorkflowsApi.md#v4putrecordsrecordidworkflowtaskstaskidcustomforms) | **PUT** /v4/records/{recordId}/workflowTasks/{taskId}/customForms | Update Custom Form for Record Workflow Task


<a name="v4getrecordsrecordidworkflowtasks"></a>
# **V4GetRecordsRecordIdWorkflowTasks**
> ResponseTaskItemModelArray V4GetRecordsRecordIdWorkflowTasks (string contentType, string authorization, string recordId, string fields = null, string lang = null)

Get All Workflow Tasks for Record

Gets all the workflow tasks associated with the specified record. **API Endpoint**:  GET /v4/records/{recordId}/workflowTasks  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdWorkflowTasksExample
    {
        public void main()
        {
            var apiInstance = new RecordsWorkflowsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Workflow Tasks for Record
                ResponseTaskItemModelArray result = apiInstance.V4GetRecordsRecordIdWorkflowTasks(contentType, authorization, recordId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsWorkflowsApi.V4GetRecordsRecordIdWorkflowTasks: " + e.Message );
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

[**ResponseTaskItemModelArray**](ResponseTaskItemModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getrecordsrecordidworkflowtaskscommentshistories"></a>
# **V4GetRecordsRecordIdWorkflowTasksCommentsHistories**
> ResponseWorkflowTaskCommentModelArray V4GetRecordsRecordIdWorkflowTasksCommentsHistories (string contentType, string authorization, string recordId, string fields = null, string lang = null)

Get Workflow Task Comment Histories

Gets the workflow task comment history for the specified record. **API Endpoint**:  GET /v4/records/{recordId}/workflowTasks/comments/histories   **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdWorkflowTasksCommentsHistoriesExample
    {
        public void main()
        {
            var apiInstance = new RecordsWorkflowsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Workflow Task Comment Histories
                ResponseWorkflowTaskCommentModelArray result = apiInstance.V4GetRecordsRecordIdWorkflowTasksCommentsHistories(contentType, authorization, recordId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsWorkflowsApi.V4GetRecordsRecordIdWorkflowTasksCommentsHistories: " + e.Message );
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

[**ResponseWorkflowTaskCommentModelArray**](ResponseWorkflowTaskCommentModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getrecordsrecordidworkflowtaskshistories"></a>
# **V4GetRecordsRecordIdWorkflowTasksHistories**
> ResponseTaskItemActionModelArray V4GetRecordsRecordIdWorkflowTasksHistories (string contentType, string authorization, string recordId, string fields = null, string lang = null)

Get All Workflow Task History for Record

Gets all the workflow task history associated with the specified record. **API Endpoint**:  GET /v4/records/{recordId}/workflowTasks/histories  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdWorkflowTasksHistoriesExample
    {
        public void main()
        {
            var apiInstance = new RecordsWorkflowsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Workflow Task History for Record
                ResponseTaskItemActionModelArray result = apiInstance.V4GetRecordsRecordIdWorkflowTasksHistories(contentType, authorization, recordId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsWorkflowsApi.V4GetRecordsRecordIdWorkflowTasksHistories: " + e.Message );
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

[**ResponseTaskItemActionModelArray**](ResponseTaskItemActionModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getrecordsrecordidworkflowtasksid"></a>
# **V4GetRecordsRecordIdWorkflowTasksId**
> ResponseTaskItemModelArray V4GetRecordsRecordIdWorkflowTasksId (string contentType, string authorization, string recordId, string id, string fields = null, string lang = null)

Get Record Workflow Task

Gets the requested workflow task for the specified record. **API Endpoint**:  GET /v4/records/{recordId}/workflowTasks/{id}  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdWorkflowTasksIdExample
    {
        public void main()
        {
            var apiInstance = new RecordsWorkflowsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var id = id_example;  // string | The ID of task to fetch.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Record Workflow Task
                ResponseTaskItemModelArray result = apiInstance.V4GetRecordsRecordIdWorkflowTasksId(contentType, authorization, recordId, id, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsWorkflowsApi.V4GetRecordsRecordIdWorkflowTasksId: " + e.Message );
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
 **id** | **string**| The ID of task to fetch. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseTaskItemModelArray**](ResponseTaskItemModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getrecordsrecordidworkflowtasksidstatuses"></a>
# **V4GetRecordsRecordIdWorkflowTasksIdStatuses**
> ResponseIdentifierModelArray V4GetRecordsRecordIdWorkflowTasksIdStatuses (string contentType, string authorization, string recordId, string id, string lang = null)

Get All Statuses for Workflow Task

Gets the status of the specified workflow task for the specified record. **API Endpoint**:  GET /v4/records/{recordId}/workflowTasks/{id}/statuses  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdWorkflowTasksIdStatusesExample
    {
        public void main()
        {
            var apiInstance = new RecordsWorkflowsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var id = id_example;  // string | The ID of task to fetch.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Statuses for Workflow Task
                ResponseIdentifierModelArray result = apiInstance.V4GetRecordsRecordIdWorkflowTasksIdStatuses(contentType, authorization, recordId, id, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsWorkflowsApi.V4GetRecordsRecordIdWorkflowTasksIdStatuses: " + e.Message );
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
 **id** | **string**| The ID of task to fetch. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseIdentifierModelArray**](ResponseIdentifierModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getrecordsrecordidworkflowtaskstaskidcustomforms"></a>
# **V4GetRecordsRecordIdWorkflowTasksTaskIdCustomForms**
> ResponseCustomAttributeModelArray V4GetRecordsRecordIdWorkflowTasksTaskIdCustomForms (string contentType, string authorization, string recordId, string taskId, string lang = null)

Get All Custom Forms for Record Workflow Task

Returns the custom forms containing task-specific information for a given workflow task for a specific record. **API Endpoint**:  GET /v4/records/{recordId}/workflowTasks/{taskId}/customForms   **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 8.0.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdWorkflowTasksTaskIdCustomFormsExample
    {
        public void main()
        {
            var apiInstance = new RecordsWorkflowsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var taskId = taskId_example;  // string | The ID of the workflow task to fetch.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Custom Forms for Record Workflow Task
                ResponseCustomAttributeModelArray result = apiInstance.V4GetRecordsRecordIdWorkflowTasksTaskIdCustomForms(contentType, authorization, recordId, taskId, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsWorkflowsApi.V4GetRecordsRecordIdWorkflowTasksTaskIdCustomForms: " + e.Message );
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
 **taskId** | **string**| The ID of the workflow task to fetch. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseCustomAttributeModelArray**](ResponseCustomAttributeModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getrecordsrecordidworkflowtaskstaskidcustomformsmeta"></a>
# **V4GetRecordsRecordIdWorkflowTasksTaskIdCustomFormsMeta**
> ResponseCustomFormSubgroupModelArray V4GetRecordsRecordIdWorkflowTasksTaskIdCustomFormsMeta (string contentType, string authorization, string recordId, string taskId, string fields = null, string lang = null)

Get All Custom Forms Metadata for Record Workflow Task

Returns the metadata associated with all custom forms for a given workflow task for a specific record. **API Endpoint**:  GET /v4/records/{recordId}/workflowTasks/{taskId}/customForms/meta   **Scope**:  records  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdWorkflowTasksTaskIdCustomFormsMetaExample
    {
        public void main()
        {
            var apiInstance = new RecordsWorkflowsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var taskId = taskId_example;  // string | The ID of the workflow task to fetch.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Custom Forms Metadata for Record Workflow Task
                ResponseCustomFormSubgroupModelArray result = apiInstance.V4GetRecordsRecordIdWorkflowTasksTaskIdCustomFormsMeta(contentType, authorization, recordId, taskId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsWorkflowsApi.V4GetRecordsRecordIdWorkflowTasksTaskIdCustomFormsMeta: " + e.Message );
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
 **taskId** | **string**| The ID of the workflow task to fetch. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseCustomFormSubgroupModelArray**](ResponseCustomFormSubgroupModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4putrecordsrecordidworkflowtasksid"></a>
# **V4PutRecordsRecordIdWorkflowTasksId**
> ResponseTaskItemModel V4PutRecordsRecordIdWorkflowTasksId (string contentType, string authorization, string recordId, string id, RequestTaskItemModel body, string fields = null, string lang = null)

Update Record Workflow Task

Updates the requested workflow task for the specified record. **API Endpoint**:  PUT /v4/records/{recordId}/workflowTasks/{id}  **Scope**:  records  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4PutRecordsRecordIdWorkflowTasksIdExample
    {
        public void main()
        {
            var apiInstance = new RecordsWorkflowsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var id = id_example;  // string | The ID of task to fetch.
            var body = new RequestTaskItemModel(); // RequestTaskItemModel | The task information to be updated
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Record Workflow Task
                ResponseTaskItemModel result = apiInstance.V4PutRecordsRecordIdWorkflowTasksId(contentType, authorization, recordId, id, body, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsWorkflowsApi.V4PutRecordsRecordIdWorkflowTasksId: " + e.Message );
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
 **id** | **string**| The ID of task to fetch. | 
 **body** | [**RequestTaskItemModel**](RequestTaskItemModel.md)| The task information to be updated | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseTaskItemModel**](ResponseTaskItemModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4putrecordsrecordidworkflowtaskstaskidcustomforms"></a>
# **V4PutRecordsRecordIdWorkflowTasksTaskIdCustomForms**
> ResponseResultModelArray V4PutRecordsRecordIdWorkflowTasksTaskIdCustomForms (string contentType, string authorization, string recordId, string taskId, List<CustomAttributeModel> body, string lang = null)

Update Custom Form for Record Workflow Task

Updates custom forms containing task-specific information for a given workflow task for a specific record. **API Endpoint**:  PUT /v4/records/{recordId}/workflowTasks/{taskId}/customForms   **Scope**:  records  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 8.0.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4PutRecordsRecordIdWorkflowTasksTaskIdCustomFormsExample
    {
        public void main()
        {
            var apiInstance = new RecordsWorkflowsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var taskId = taskId_example;  // string | The ID of the workflow task to fetch.
            var body = new List<CustomAttributeModel>(); // List<CustomAttributeModel> | The custom form information to be updated. Ex. [{\"apiField1\": \"val1\", \"id\": \"group-subGroup\"   }]
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Custom Form for Record Workflow Task
                ResponseResultModelArray result = apiInstance.V4PutRecordsRecordIdWorkflowTasksTaskIdCustomForms(contentType, authorization, recordId, taskId, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsWorkflowsApi.V4PutRecordsRecordIdWorkflowTasksTaskIdCustomForms: " + e.Message );
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
 **taskId** | **string**| The ID of the workflow task to fetch. | 
 **body** | [**List&lt;CustomAttributeModel&gt;**](CustomAttributeModel.md)| The custom form information to be updated. Ex. [{\&quot;apiField1\&quot;: \&quot;val1\&quot;, \&quot;id\&quot;: \&quot;group-subGroup\&quot;   }] | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

