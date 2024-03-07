# AccelaMiscellanous.Api.TimeAccountingApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4DeleteTimeAccountingIds**](TimeAccountingApi.md#v4deletetimeaccountingids) | **DELETE** /v4/timeAccounting/{ids} | Delete Time Accounting
[**V4GetTimeAccounting**](TimeAccountingApi.md#v4gettimeaccounting) | **GET** /v4/timeAccounting | Get Time Accounting
[**V4PostTimeAccounting**](TimeAccountingApi.md#v4posttimeaccounting) | **POST** /v4/timeAccounting | Create Time Accounting
[**V4PutTimeAccountingId**](TimeAccountingApi.md#v4puttimeaccountingid) | **PUT** /v4/TimeAccounting/{id} | Update Time Accounting


<a name="v4deletetimeaccountingids"></a>
# **V4DeleteTimeAccountingIds**
> ResponseResultModelArray V4DeleteTimeAccountingIds (string contentType, string authorization, string ids, string lang = null)

Delete Time Accounting

Deletes one or more time accounting records. **API Endpoint**:  DELETE /v4/timeAccounting/{ids}  **Scope**:  timeaccounting  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaMiscellanous.Api;
using AccelaMiscellanous.Client;
using AccelaMiscellanous.Model;

namespace Example
{
    public class V4DeleteTimeAccountingIdsExample
    {
        public void main()
        {
            var apiInstance = new TimeAccountingApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var ids = ids_example;  // string | The comma-delimited IDs of the time accounting entries to be deleted.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Delete Time Accounting
                ResponseResultModelArray result = apiInstance.V4DeleteTimeAccountingIds(contentType, authorization, ids, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling TimeAccountingApi.V4DeleteTimeAccountingIds: " + e.Message );
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
 **ids** | **string**| The comma-delimited IDs of the time accounting entries to be deleted. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4gettimeaccounting"></a>
# **V4GetTimeAccounting**
> ResponseTimeLogModelArray V4GetTimeAccounting (string contentType, string authorization, string lang = null)

Get Time Accounting

Gets time accounting information. **API Endpoint**:  GET /v4/timeAccounting  **Scope**:  timeaccounting  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaMiscellanous.Api;
using AccelaMiscellanous.Client;
using AccelaMiscellanous.Model;

namespace Example
{
    public class V4GetTimeAccountingExample
    {
        public void main()
        {
            var apiInstance = new TimeAccountingApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Time Accounting
                ResponseTimeLogModelArray result = apiInstance.V4GetTimeAccounting(contentType, authorization, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling TimeAccountingApi.V4GetTimeAccounting: " + e.Message );
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

[**ResponseTimeLogModelArray**](ResponseTimeLogModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4posttimeaccounting"></a>
# **V4PostTimeAccounting**
> ResponseResultModelArray V4PostTimeAccounting (string contentType, string authorization, List<TimeLogModel> body = null, string lang = null)

Create Time Accounting

Creates a time accounting record. **API Endpoint**:  POST /v4/timeAccounting  **Scope**:  timeaccounting  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaMiscellanous.Api;
using AccelaMiscellanous.Client;
using AccelaMiscellanous.Model;

namespace Example
{
    public class V4PostTimeAccountingExample
    {
        public void main()
        {
            var apiInstance = new TimeAccountingApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var body = new List<TimeLogModel>(); // List<TimeLogModel> | Time accounting request information (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Create Time Accounting
                ResponseResultModelArray result = apiInstance.V4PostTimeAccounting(contentType, authorization, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling TimeAccountingApi.V4PostTimeAccounting: " + e.Message );
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
 **body** | [**List&lt;TimeLogModel&gt;**](TimeLogModel.md)| Time accounting request information | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4puttimeaccountingid"></a>
# **V4PutTimeAccountingId**
> ResponseTimeLogModel V4PutTimeAccountingId (string contentType, string authorization, long? id, TimeLogModel body = null, string lang = null)

Update Time Accounting

Updates the specified time accounting record. **API Endpoint**:  PUT /v4/timeAccounting/{id}  **Scope**:  timeaccounting  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaMiscellanous.Api;
using AccelaMiscellanous.Client;
using AccelaMiscellanous.Model;

namespace Example
{
    public class V4PutTimeAccountingIdExample
    {
        public void main()
        {
            var apiInstance = new TimeAccountingApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = 789;  // long? | The ID of the time accounting entry to be updated.
            var body = new TimeLogModel(); // TimeLogModel | Time accounting request information. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Time Accounting
                ResponseTimeLogModel result = apiInstance.V4PutTimeAccountingId(contentType, authorization, id, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling TimeAccountingApi.V4PutTimeAccountingId: " + e.Message );
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
 **id** | **long?**| The ID of the time accounting entry to be updated. | 
 **body** | [**TimeLogModel**](TimeLogModel.md)| Time accounting request information. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseTimeLogModel**](ResponseTimeLogModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

