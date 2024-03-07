# AccelaRecords.Api.RecordsActivitiesApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetRecordsRecordIdActivities**](RecordsActivitiesApi.md#v4getrecordsrecordidactivities) | **GET** /v4/records/{recordId}/activities | Get All Record Activities
[**V4PostRecordsRecordIdActivities**](RecordsActivitiesApi.md#v4postrecordsrecordidactivities) | **POST** /v4/records/{recordId}/activities | Create Record Activities
[**V4PutRecordsRecordIdActivitiesId**](RecordsActivitiesApi.md#v4putrecordsrecordidactivitiesid) | **PUT** /v4/records/{recordId}/activities/{id} | Update Record Activity


<a name="v4getrecordsrecordidactivities"></a>
# **V4GetRecordsRecordIdActivities**
> ResponseActivityModelArray V4GetRecordsRecordIdActivities (string contentType, string authorization, string recordId, string fields = null, string lang = null)

Get All Record Activities

Gets all activities related to the specified record. **API Endpoint**:  GET /v4/records/{recordId}/activities  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdActivitiesExample
    {
        public void main()
        {
            var apiInstance = new RecordsActivitiesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Record Activities
                ResponseActivityModelArray result = apiInstance.V4GetRecordsRecordIdActivities(contentType, authorization, recordId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsActivitiesApi.V4GetRecordsRecordIdActivities: " + e.Message );
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

[**ResponseActivityModelArray**](ResponseActivityModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4postrecordsrecordidactivities"></a>
# **V4PostRecordsRecordIdActivities**
> ResponseResultModelArray V4PostRecordsRecordIdActivities (string contentType, string authorization, string recordId, List<RequestActivityAddModel> body, string lang = null)

Create Record Activities

Creates activities associated to the specified record. **API Endpoint**:  POST /v4/records/{recordId}/activities  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4PostRecordsRecordIdActivitiesExample
    {
        public void main()
        {
            var apiInstance = new RecordsActivitiesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var body = new List<RequestActivityAddModel>(); // List<RequestActivityAddModel> | The activity information to be added.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Create Record Activities
                ResponseResultModelArray result = apiInstance.V4PostRecordsRecordIdActivities(contentType, authorization, recordId, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsActivitiesApi.V4PostRecordsRecordIdActivities: " + e.Message );
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
 **body** | [**List&lt;RequestActivityAddModel&gt;**](RequestActivityAddModel.md)| The activity information to be added. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4putrecordsrecordidactivitiesid"></a>
# **V4PutRecordsRecordIdActivitiesId**
> ResponseResultModel V4PutRecordsRecordIdActivitiesId (string contentType, string authorization, string recordId, long? id, RequestActivityUpdateModel body, string fields = null, string lang = null)

Update Record Activity

Updates the activity for the specified record. **API Endpoint**:  PUT /v4/records/{recordId}/activities/{id}  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4PutRecordsRecordIdActivitiesIdExample
    {
        public void main()
        {
            var apiInstance = new RecordsActivitiesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var id = 789;  // long? | The ID of the record activity to update.
            var body = new RequestActivityUpdateModel(); // RequestActivityUpdateModel | The activity information to be updated.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Record Activity
                ResponseResultModel result = apiInstance.V4PutRecordsRecordIdActivitiesId(contentType, authorization, recordId, id, body, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsActivitiesApi.V4PutRecordsRecordIdActivitiesId: " + e.Message );
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
 **id** | **long?**| The ID of the record activity to update. | 
 **body** | [**RequestActivityUpdateModel**](RequestActivityUpdateModel.md)| The activity information to be updated. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModel**](ResponseResultModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

