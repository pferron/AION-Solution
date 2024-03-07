# AccelaInspections.Api.InspectionsTimeAccountingApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4DeleteInspectionsInspectionIdTimeAccountingIds**](InspectionsTimeAccountingApi.md#v4deleteinspectionsinspectionidtimeaccountingids) | **DELETE** /v4/Inspections/{inspectionId}/timeAccounting/{ids} | Delete Inspection Time Accounting Entries
[**V4GetInspectionsInspectionIdTimeAccounting**](InspectionsTimeAccountingApi.md#v4getinspectionsinspectionidtimeaccounting) | **GET** /v4/inspections/{inspectionId}/timeAccounting | Get All Inspection Time Accounting Entries
[**V4PostInspectionsInspectionIdTimeAccounting**](InspectionsTimeAccountingApi.md#v4postinspectionsinspectionidtimeaccounting) | **POST** /v4/inspections/{inspectionId}/timeAccounting | Create Inspection Time Accounting Entries
[**V4PutInspectionsInspectionIdTimeAccountingId**](InspectionsTimeAccountingApi.md#v4putinspectionsinspectionidtimeaccountingid) | **PUT** /v4/inspections/{inspectionId}/timeAccounting/{id} | Update Inspection Time Accounting Entry


<a name="v4deleteinspectionsinspectionidtimeaccountingids"></a>
# **V4DeleteInspectionsInspectionIdTimeAccountingIds**
> ResponseResultModelArray V4DeleteInspectionsInspectionIdTimeAccountingIds (string contentType, string authorization, long? inspectionId, string ids, string lang = null)

Delete Inspection Time Accounting Entries

Deletes one or more time accounting items for an inspection.  ** API Endpoint ** : DELETE /v4/inspections/{inspectionId}/timeAccounting/{ids}   ** Scope ** : inspections   ** App Type ** : Agency   ** Authorization Type ** : Access token   ** Civic Platform version ** : 7.3.2  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4DeleteInspectionsInspectionIdTimeAccountingIdsExample
    {
        public void main()
        {
            var apiInstance = new InspectionsTimeAccountingApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var inspectionId = 789;  // long? | The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections).
            var ids = ids_example;  // string | Comma - delimited IDs of time accounting items to be deleted.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Delete Inspection Time Accounting Entries
                ResponseResultModelArray result = apiInstance.V4DeleteInspectionsInspectionIdTimeAccountingIds(contentType, authorization, inspectionId, ids, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsTimeAccountingApi.V4DeleteInspectionsInspectionIdTimeAccountingIds: " + e.Message );
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
 **ids** | **string**| Comma - delimited IDs of time accounting items to be deleted. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getinspectionsinspectionidtimeaccounting"></a>
# **V4GetInspectionsInspectionIdTimeAccounting**
> ResponseTimeLogModelArray V4GetInspectionsInspectionIdTimeAccounting (string contentType, string authorization, long? inspectionId, string fields = null, string lang = null)

Get All Inspection Time Accounting Entries

 **API Endpoint**:  GET /v4/inspections/{inspectionId}/timeAccounting  **Scope**:  inspections  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4GetInspectionsInspectionIdTimeAccountingExample
    {
        public void main()
        {
            var apiInstance = new InspectionsTimeAccountingApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var inspectionId = 789;  // long? | The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections).
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Inspection Time Accounting Entries
                ResponseTimeLogModelArray result = apiInstance.V4GetInspectionsInspectionIdTimeAccounting(contentType, authorization, inspectionId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsTimeAccountingApi.V4GetInspectionsInspectionIdTimeAccounting: " + e.Message );
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

[**ResponseTimeLogModelArray**](ResponseTimeLogModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4postinspectionsinspectionidtimeaccounting"></a>
# **V4PostInspectionsInspectionIdTimeAccounting**
> ResponseResultModelArray V4PostInspectionsInspectionIdTimeAccounting (string contentType, string authorization, long? inspectionId, List<RequestTimeLogModel> body, string lang = null)

Create Inspection Time Accounting Entries

Creates time accounting items for the specified inspection. **API Endpoint**:  POST /v4/inspections/{inspectionId}/timeAccounting  **Scope**:  inspections  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4PostInspectionsInspectionIdTimeAccountingExample
    {
        public void main()
        {
            var apiInstance = new InspectionsTimeAccountingApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var inspectionId = 789;  // long? | The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections).
            var body = new List<RequestTimeLogModel>(); // List<RequestTimeLogModel> | Inspection time log entries to be added.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Create Inspection Time Accounting Entries
                ResponseResultModelArray result = apiInstance.V4PostInspectionsInspectionIdTimeAccounting(contentType, authorization, inspectionId, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsTimeAccountingApi.V4PostInspectionsInspectionIdTimeAccounting: " + e.Message );
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
 **body** | [**List&lt;RequestTimeLogModel&gt;**](RequestTimeLogModel.md)| Inspection time log entries to be added. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4putinspectionsinspectionidtimeaccountingid"></a>
# **V4PutInspectionsInspectionIdTimeAccountingId**
> ResponseTimeLogModel V4PutInspectionsInspectionIdTimeAccountingId (string contentType, string authorization, long? inspectionId, RequestTimeLogModel body, string id, string lang = null)

Update Inspection Time Accounting Entry

Updates a time accounting item for an inspection.   ** API Endpoint ** : PUT /v4/inspections/{inspectionId}/timeAccounting/{id }   ** Scope ** : inspections   ** App Type ** : All   ** Authorization Type ** : Access token   ** Civic Platform version ** : 7.3.2  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4PutInspectionsInspectionIdTimeAccountingIdExample
    {
        public void main()
        {
            var apiInstance = new InspectionsTimeAccountingApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var inspectionId = 789;  // long? | ID of inspection that needs to be fetched
            var body = new RequestTimeLogModel(); // RequestTimeLogModel | Time accounting information to be updated.
            var id = id_example;  // string | The ID of the inspection time accountint item to update.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Inspection Time Accounting Entry
                ResponseTimeLogModel result = apiInstance.V4PutInspectionsInspectionIdTimeAccountingId(contentType, authorization, inspectionId, body, id, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsTimeAccountingApi.V4PutInspectionsInspectionIdTimeAccountingId: " + e.Message );
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
 **inspectionId** | **long?**| ID of inspection that needs to be fetched | 
 **body** | [**RequestTimeLogModel**](RequestTimeLogModel.md)| Time accounting information to be updated. | 
 **id** | **string**| The ID of the inspection time accountint item to update. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseTimeLogModel**](ResponseTimeLogModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

