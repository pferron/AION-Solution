# AccelaInspections.Api.InspectionsConditionsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4DeleteInspectionsInspectionIdConditionsIds**](InspectionsConditionsApi.md#v4deleteinspectionsinspectionidconditionsids) | **DELETE** /v4/Inspections/{inspectionId}/conditions/{ids} | Delete Inspection Conditions
[**V4GetInspectionsInspectionIdConditions**](InspectionsConditionsApi.md#v4getinspectionsinspectionidconditions) | **GET** /v4/inspections/{inspectionId}/conditions | Get All Standard Conditions for Inspection
[**V4GetInspectionsInspectionIdConditionsId**](InspectionsConditionsApi.md#v4getinspectionsinspectionidconditionsid) | **GET** /v4/inspections/{inspectionId}/conditions/{id} | Get Inspection Condition
[**V4GetInspectionsInspectionIdConditionsIdHistories**](InspectionsConditionsApi.md#v4getinspectionsinspectionidconditionsidhistories) | **GET** /v4/inspections/{inspectionId}/conditions/{id}/histories | Get Inspection Condition History
[**V4PostInspectionsInspectionIdConditions**](InspectionsConditionsApi.md#v4postinspectionsinspectionidconditions) | **POST** /v4/inspections/{inspectionId}/conditions | Create Inspection Standard Conditions
[**V4PutInspectionsInspectionIdConditionsId**](InspectionsConditionsApi.md#v4putinspectionsinspectionidconditionsid) | **PUT** /v4/inspections/{inspectionId}/conditions/{id} | Update Inspection Condition


<a name="v4deleteinspectionsinspectionidconditionsids"></a>
# **V4DeleteInspectionsInspectionIdConditionsIds**
> ResponseResultModelArray V4DeleteInspectionsInspectionIdConditionsIds (string contentType, string authorization, long? inspectionId, string ids, string lang = null)

Delete Inspection Conditions

Deletes conditons from the specified inspections. **API Endpoint**:  DELETE /v4/inspections/{inspectionId}/conditions/{ids}  **Scope**:  inspections  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4DeleteInspectionsInspectionIdConditionsIdsExample
    {
        public void main()
        {
            var apiInstance = new InspectionsConditionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var inspectionId = 789;  // long? | The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections).
            var ids = ids_example;  // string | Comma-delimited IDs of inspection conditions to be deleted.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Delete Inspection Conditions
                ResponseResultModelArray result = apiInstance.V4DeleteInspectionsInspectionIdConditionsIds(contentType, authorization, inspectionId, ids, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsConditionsApi.V4DeleteInspectionsInspectionIdConditionsIds: " + e.Message );
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
 **ids** | **string**| Comma-delimited IDs of inspection conditions to be deleted. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getinspectionsinspectionidconditions"></a>
# **V4GetInspectionsInspectionIdConditions**
> ResponseInspectionConditionModelArray V4GetInspectionsInspectionIdConditions (string contentType, string authorization, long? inspectionId, string fields = null, string lang = null)

Get All Standard Conditions for Inspection

Gets the conditions that apply to the specified inspections. **API Endpoint**:  GET /v4/inspections/{inspectionId}/conditions  **Scope**:  inspections  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4GetInspectionsInspectionIdConditionsExample
    {
        public void main()
        {
            var apiInstance = new InspectionsConditionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var inspectionId = 789;  // long? | The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections).
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Standard Conditions for Inspection
                ResponseInspectionConditionModelArray result = apiInstance.V4GetInspectionsInspectionIdConditions(contentType, authorization, inspectionId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsConditionsApi.V4GetInspectionsInspectionIdConditions: " + e.Message );
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

[**ResponseInspectionConditionModelArray**](ResponseInspectionConditionModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getinspectionsinspectionidconditionsid"></a>
# **V4GetInspectionsInspectionIdConditionsId**
> ResponseInspectionConditionModel V4GetInspectionsInspectionIdConditionsId (string contentType, string authorization, long? inspectionId, long? id, string fields = null, string lang = null)

Get Inspection Condition

Gets the condition for the specified inspection. **API Endpoint**:  GET /v4/inspections/{inspectionId}/conditions/{id}  **Scope**:  inspections  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4GetInspectionsInspectionIdConditionsIdExample
    {
        public void main()
        {
            var apiInstance = new InspectionsConditionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var inspectionId = 789;  // long? | The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections).
            var id = 789;  // long? | The ID of inspection condition to fetch.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Inspection Condition
                ResponseInspectionConditionModel result = apiInstance.V4GetInspectionsInspectionIdConditionsId(contentType, authorization, inspectionId, id, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsConditionsApi.V4GetInspectionsInspectionIdConditionsId: " + e.Message );
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
 **id** | **long?**| The ID of inspection condition to fetch. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseInspectionConditionModel**](ResponseInspectionConditionModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getinspectionsinspectionidconditionsidhistories"></a>
# **V4GetInspectionsInspectionIdConditionsIdHistories**
> ResponseConditionHistoryModelArray V4GetInspectionsInspectionIdConditionsIdHistories (string contentType, string authorization, long? inspectionId, long? id, string fields = null, string lang = null)

Get Inspection Condition History

Gets the history for the specified conditions associated with the specified inspections. **API Endpoint**:  GET /v4/inspections/{inspectionId}/conditions/{id}/histories  **Scope**:  inspections  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4GetInspectionsInspectionIdConditionsIdHistoriesExample
    {
        public void main()
        {
            var apiInstance = new InspectionsConditionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var inspectionId = 789;  // long? | The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections).
            var id = 789;  // long? | The ID of the inspection condition to fetch.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Inspection Condition History
                ResponseConditionHistoryModelArray result = apiInstance.V4GetInspectionsInspectionIdConditionsIdHistories(contentType, authorization, inspectionId, id, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsConditionsApi.V4GetInspectionsInspectionIdConditionsIdHistories: " + e.Message );
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
 **id** | **long?**| The ID of the inspection condition to fetch. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseConditionHistoryModelArray**](ResponseConditionHistoryModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4postinspectionsinspectionidconditions"></a>
# **V4PostInspectionsInspectionIdConditions**
> ResponseResultModelArray V4PostInspectionsInspectionIdConditions (string contentType, string authorization, List<InspectionConditionModel> body, long? inspectionId, string lang = null)

Create Inspection Standard Conditions

Adds a set of conditions to the specified inspections. **API Endpoint**:  POST /v4/inspections/{inspectionId}/conditions  **Scope**:  inspections  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4PostInspectionsInspectionIdConditionsExample
    {
        public void main()
        {
            var apiInstance = new InspectionsConditionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var body = new List<InspectionConditionModel>(); // List<InspectionConditionModel> | The inspection condition to add.
            var inspectionId = 789;  // long? | The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections).
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Create Inspection Standard Conditions
                ResponseResultModelArray result = apiInstance.V4PostInspectionsInspectionIdConditions(contentType, authorization, body, inspectionId, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsConditionsApi.V4PostInspectionsInspectionIdConditions: " + e.Message );
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
 **body** | [**List&lt;InspectionConditionModel&gt;**](InspectionConditionModel.md)| The inspection condition to add. | 
 **inspectionId** | **long?**| The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections). | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4putinspectionsinspectionidconditionsid"></a>
# **V4PutInspectionsInspectionIdConditionsId**
> ResponseInspectionConditionModel V4PutInspectionsInspectionIdConditionsId (string contentType, string authorization, InspectionConditionModel body, long? inspectionId, string id, string lang = null)

Update Inspection Condition

Updates conditions for the specified inspection. **API Endpoint**:  PUT /v4/inspections/{inspectionId}/conditions/{id}  **Scope**:  inspections  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4PutInspectionsInspectionIdConditionsIdExample
    {
        public void main()
        {
            var apiInstance = new InspectionsConditionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var body = new InspectionConditionModel(); // InspectionConditionModel | The condition information to be updated.
            var inspectionId = 789;  // long? | The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections).
            var id = id_example;  // string | The ID of the inspection condition to update.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Inspection Condition
                ResponseInspectionConditionModel result = apiInstance.V4PutInspectionsInspectionIdConditionsId(contentType, authorization, body, inspectionId, id, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsConditionsApi.V4PutInspectionsInspectionIdConditionsId: " + e.Message );
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
 **body** | [**InspectionConditionModel**](InspectionConditionModel.md)| The condition information to be updated. | 
 **inspectionId** | **long?**| The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections). | 
 **id** | **string**| The ID of the inspection condition to update. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseInspectionConditionModel**](ResponseInspectionConditionModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

