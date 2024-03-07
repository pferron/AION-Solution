# AccelaInspections.Api.InspectionsConditionApprovalsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4DeleteInspectionsInspectionIdConditionApprovalsIds**](InspectionsConditionApprovalsApi.md#v4deleteinspectionsinspectionidconditionapprovalsids) | **DELETE** /v4/Inspections/{inspectionId}/conditionApprovals/{ids} | Delete Inspection Approval Conditions
[**V4GetInspectionsInspectionIdConditionApprovals**](InspectionsConditionApprovalsApi.md#v4getinspectionsinspectionidconditionapprovals) | **GET** /v4/inspections/{inspectionId}/conditionApprovals | Get All Approval Conditions for Inspection
[**V4GetInspectionsInspectionIdConditionApprovalsId**](InspectionsConditionApprovalsApi.md#v4getinspectionsinspectionidconditionapprovalsid) | **GET** /v4/inspections/{inspectionId}/conditionApprovals/{id} | Get Inspection Approval Condition
[**V4PostInspectionsInspectionIdConditionApprovals**](InspectionsConditionApprovalsApi.md#v4postinspectionsinspectionidconditionapprovals) | **POST** /v4/inspections/{inspectionId}/conditionApprovals | Create Inspection Approval Conditions
[**V4PutInspectionsInspectionIdConditionApprovalsId**](InspectionsConditionApprovalsApi.md#v4putinspectionsinspectionidconditionapprovalsid) | **PUT** /v4/inspections/{inspectionId}/conditionApprovals/{id} | Update Inspection Approval Condition


<a name="v4deleteinspectionsinspectionidconditionapprovalsids"></a>
# **V4DeleteInspectionsInspectionIdConditionApprovalsIds**
> ResponseResultModelArray V4DeleteInspectionsInspectionIdConditionApprovalsIds (string contentType, string authorization, long? inspectionId, string ids, string lang = null)

Delete Inspection Approval Conditions

Deletes one or more conditions of approval from the specified inspections. **API Endpoint**:  DELETE /v4/inspections/{inspectionId}/conditionApprovals/{ids}  **Scope**:  inspections  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4DeleteInspectionsInspectionIdConditionApprovalsIdsExample
    {
        public void main()
        {
            var apiInstance = new InspectionsConditionApprovalsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var inspectionId = 789;  // long? | The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections).
            var ids = ids_example;  // string | Comma-delimited IDs of inspection conditions to be deleted.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Delete Inspection Approval Conditions
                ResponseResultModelArray result = apiInstance.V4DeleteInspectionsInspectionIdConditionApprovalsIds(contentType, authorization, inspectionId, ids, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsConditionApprovalsApi.V4DeleteInspectionsInspectionIdConditionApprovalsIds: " + e.Message );
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

<a name="v4getinspectionsinspectionidconditionapprovals"></a>
# **V4GetInspectionsInspectionIdConditionApprovals**
> ResponseInspectionConditionModelArray V4GetInspectionsInspectionIdConditionApprovals (string contentType, string authorization, long? inspectionId, bool? isNeedNoticeInfo = null, DateTime? effectiveDate = null, DateTime? expirationDate = null, string fields = null, string lang = null)

Get All Approval Conditions for Inspection

Gets the conditions of approvals applicable to the specified inspections. **API Endpoint**:  GET /v4/inspections/{inspectionId}/checklists/{checklistId}/checklistItems/{checkListItemId}/documents  **Scope**:  inspections  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4GetInspectionsInspectionIdConditionApprovalsExample
    {
        public void main()
        {
            var apiInstance = new InspectionsConditionApprovalsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var inspectionId = 789;  // long? | The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections).
            var isNeedNoticeInfo = true;  // bool? | Filter whether or not notice information is needed. (optional) 
            var effectiveDate = 2013-10-20T19:20:30+01:00;  // DateTime? | Filter by effective date. (optional) 
            var expirationDate = 2013-10-20T19:20:30+01:00;  // DateTime? | Filter by expiration date. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Approval Conditions for Inspection
                ResponseInspectionConditionModelArray result = apiInstance.V4GetInspectionsInspectionIdConditionApprovals(contentType, authorization, inspectionId, isNeedNoticeInfo, effectiveDate, expirationDate, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsConditionApprovalsApi.V4GetInspectionsInspectionIdConditionApprovals: " + e.Message );
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
 **isNeedNoticeInfo** | **bool?**| Filter whether or not notice information is needed. | [optional] 
 **effectiveDate** | **DateTime?**| Filter by effective date. | [optional] 
 **expirationDate** | **DateTime?**| Filter by expiration date. | [optional] 
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

<a name="v4getinspectionsinspectionidconditionapprovalsid"></a>
# **V4GetInspectionsInspectionIdConditionApprovalsId**
> ResponseInspectionConditionModel V4GetInspectionsInspectionIdConditionApprovalsId (string contentType, string authorization, long? inspectionId, long? id, string fields = null, string lang = null)

Get Inspection Approval Condition

Gets the conditions of approval for the specified inspections. **API Endpoint**:  GET /v4/inspections/{inspectionId}/conditionApprovals/{id}  **Scope**:  inspections  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4GetInspectionsInspectionIdConditionApprovalsIdExample
    {
        public void main()
        {
            var apiInstance = new InspectionsConditionApprovalsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var inspectionId = 789;  // long? | The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections).
            var id = 789;  // long? | The ID of the inspection condition to fetch.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Inspection Approval Condition
                ResponseInspectionConditionModel result = apiInstance.V4GetInspectionsInspectionIdConditionApprovalsId(contentType, authorization, inspectionId, id, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsConditionApprovalsApi.V4GetInspectionsInspectionIdConditionApprovalsId: " + e.Message );
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

[**ResponseInspectionConditionModel**](ResponseInspectionConditionModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4postinspectionsinspectionidconditionapprovals"></a>
# **V4PostInspectionsInspectionIdConditionApprovals**
> ResponseResultModelArray V4PostInspectionsInspectionIdConditionApprovals (string contentType, string authorization, List<InspectionConditionModel> body, long? inspectionId, string lang = null)

Create Inspection Approval Conditions

Adds one or more conditions of approval to the specified inspection. **API Endpoint**:  POST /v4/inspections/{inspectionId}/checklists/{checklistId}/checklistItems/{checkListItemId}/documents  **Scope**:  inspections  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4PostInspectionsInspectionIdConditionApprovalsExample
    {
        public void main()
        {
            var apiInstance = new InspectionsConditionApprovalsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var body = new List<InspectionConditionModel>(); // List<InspectionConditionModel> | The inspection condition approval to add.
            var inspectionId = 789;  // long? | The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections).
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Create Inspection Approval Conditions
                ResponseResultModelArray result = apiInstance.V4PostInspectionsInspectionIdConditionApprovals(contentType, authorization, body, inspectionId, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsConditionApprovalsApi.V4PostInspectionsInspectionIdConditionApprovals: " + e.Message );
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
 **body** | [**List&lt;InspectionConditionModel&gt;**](InspectionConditionModel.md)| The inspection condition approval to add. | 
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

<a name="v4putinspectionsinspectionidconditionapprovalsid"></a>
# **V4PutInspectionsInspectionIdConditionApprovalsId**
> ResponseInspectionConditionModel V4PutInspectionsInspectionIdConditionApprovalsId (string contentType, string authorization, long? inspectionId, InspectionConditionModel body, string id, string lang = null)

Update Inspection Approval Condition

Updates a condition of approval for the specified inspections. **API Endpoint**:  PUT /v4/inspections/{inspectionId}/conditionApprovals/{id}  **Scope**:  inspections  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4PutInspectionsInspectionIdConditionApprovalsIdExample
    {
        public void main()
        {
            var apiInstance = new InspectionsConditionApprovalsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var inspectionId = 789;  // long? | The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections).
            var body = new InspectionConditionModel(); // InspectionConditionModel | The inspection condition information to be updated.
            var id = id_example;  // string | The ID of the inspection condition to update.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Inspection Approval Condition
                ResponseInspectionConditionModel result = apiInstance.V4PutInspectionsInspectionIdConditionApprovalsId(contentType, authorization, inspectionId, body, id, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsConditionApprovalsApi.V4PutInspectionsInspectionIdConditionApprovalsId: " + e.Message );
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
 **body** | [**InspectionConditionModel**](InspectionConditionModel.md)| The inspection condition information to be updated. | 
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

