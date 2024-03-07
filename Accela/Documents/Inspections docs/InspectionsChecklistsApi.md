# AccelaInspections.Api.InspectionsChecklistsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4DeleteInspectionsInspectionIdChecklistsIds**](InspectionsChecklistsApi.md#v4deleteinspectionsinspectionidchecklistsids) | **DELETE** /v4/inspections/{inspectionId}/checklists/{ids} | Delete Inspection Checklists
[**V4GetInspectionsInspectionIdChecklists**](InspectionsChecklistsApi.md#v4getinspectionsinspectionidchecklists) | **GET** /v4/inspections/{inspectionId}/checklists | Get All Checklists for Inspection
[**V4PostInspectionsInspectionIdChecklists**](InspectionsChecklistsApi.md#v4postinspectionsinspectionidchecklists) | **POST** /v4/inspections/{inspectionId}/checklists | Create Inspection Checklist


<a name="v4deleteinspectionsinspectionidchecklistsids"></a>
# **V4DeleteInspectionsInspectionIdChecklistsIds**
> ResponseResultModelArray V4DeleteInspectionsInspectionIdChecklistsIds (string contentType, string authorization, string inspectionId, string ids, string lang = null)

Delete Inspection Checklists

Deletes checklists for the specified inspection. **API Endpoint**:  DELETE /v4/inspections/{inspectionId}/checklists/{ids}  **Scope**:  inspections  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4DeleteInspectionsInspectionIdChecklistsIdsExample
    {
        public void main()
        {
            var apiInstance = new InspectionsChecklistsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var inspectionId = inspectionId_example;  // string | The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections).
            var ids = ids_example;  // string | Comma-delimited IDs of inspection checklists to delete.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Delete Inspection Checklists
                ResponseResultModelArray result = apiInstance.V4DeleteInspectionsInspectionIdChecklistsIds(contentType, authorization, inspectionId, ids, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsChecklistsApi.V4DeleteInspectionsInspectionIdChecklistsIds: " + e.Message );
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
 **inspectionId** | **string**| The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections). | 
 **ids** | **string**| Comma-delimited IDs of inspection checklists to delete. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getinspectionsinspectionidchecklists"></a>
# **V4GetInspectionsInspectionIdChecklists**
> ResponseInspectionChecklistModelArray V4GetInspectionsInspectionIdChecklists (string contentType, string authorization, string inspectionId, string fields = null, string lang = null)

Get All Checklists for Inspection

Gets the checklists for the specified inspection. **API Endpoint**:  GET /v4/inspections/{inspectionId}/checklists  **Scope**:  inspections  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4GetInspectionsInspectionIdChecklistsExample
    {
        public void main()
        {
            var apiInstance = new InspectionsChecklistsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var inspectionId = inspectionId_example;  // string | Inspection sequence number
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Checklists for Inspection
                ResponseInspectionChecklistModelArray result = apiInstance.V4GetInspectionsInspectionIdChecklists(contentType, authorization, inspectionId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsChecklistsApi.V4GetInspectionsInspectionIdChecklists: " + e.Message );
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
 **inspectionId** | **string**| Inspection sequence number | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseInspectionChecklistModelArray**](ResponseInspectionChecklistModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4postinspectionsinspectionidchecklists"></a>
# **V4PostInspectionsInspectionIdChecklists**
> ResponseChecklistModelArray V4PostInspectionsInspectionIdChecklists (string contentType, string authorization, List<InspectionChecklistModel> body, string inspectionId, string lang = null)

Create Inspection Checklist

Creates checklists for the specified inspections. **API Endpoint**:  POST /v4/inspections/{inspectionId}/checklists  **Scope**:  inspections  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4PostInspectionsInspectionIdChecklistsExample
    {
        public void main()
        {
            var apiInstance = new InspectionsChecklistsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var body = new List<InspectionChecklistModel>(); // List<InspectionChecklistModel> | Inspection checklist to be created
            var inspectionId = inspectionId_example;  // string | The ID of the inspection to fetch
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Create Inspection Checklist
                ResponseChecklistModelArray result = apiInstance.V4PostInspectionsInspectionIdChecklists(contentType, authorization, body, inspectionId, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsChecklistsApi.V4PostInspectionsInspectionIdChecklists: " + e.Message );
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
 **body** | [**List&lt;InspectionChecklistModel&gt;**](InspectionChecklistModel.md)| Inspection checklist to be created | 
 **inspectionId** | **string**| The ID of the inspection to fetch | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseChecklistModelArray**](ResponseChecklistModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

