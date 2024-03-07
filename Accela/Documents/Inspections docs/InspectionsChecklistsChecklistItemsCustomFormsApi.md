# AccelaInspections.Api.InspectionsChecklistsChecklistItemsCustomFormsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomForms**](InspectionsChecklistsChecklistItemsCustomFormsApi.md#v4getinspectionsidchecklistschecklistidchecklistitemschecklistitemidcustomforms) | **GET** /v4/inspections/{id}/checklists/{checklistId}/checklistItems/{checklistItemId}/customForms | Get All Custom Forms for Checklist Item
[**V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomFormsFormIdMeta**](InspectionsChecklistsChecklistItemsCustomFormsApi.md#v4getinspectionsidchecklistschecklistidchecklistitemschecklistitemidcustomformsformidmeta) | **GET** /v4/inspections/{id}/checklists/{checklistId}/checklistItems/{checklistItemId}/customForms/{formId}/meta | Get Custom Form Metadata for Checklist Item
[**V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomFormsMeta**](InspectionsChecklistsChecklistItemsCustomFormsApi.md#v4getinspectionsidchecklistschecklistidchecklistitemschecklistitemidcustomformsmeta) | **GET** /v4/inspections/{id}/checklists/{checklistId}/checklistItems/{checklistItemId}/customForms/meta | Get All Custom Forms Metadata for Checklist Item
[**V4PutInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomForms**](InspectionsChecklistsChecklistItemsCustomFormsApi.md#v4putinspectionsidchecklistschecklistidchecklistitemschecklistitemidcustomforms) | **PUT** /v4/inspections/{id}/checklists/{checklistId}/checklistItems/{checklistItemId}/customForms | Update Custom Forms for Checklist Item


<a name="v4getinspectionsidchecklistschecklistidchecklistitemschecklistitemidcustomforms"></a>
# **V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomForms**
> ResponseCustomAttributeModelArray V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomForms (string contentType, string authorization, long? id, long? checklistId, long? checklistItemId, string fields = null, string lang = null)

Get All Custom Forms for Checklist Item

Returns an array of custom forms associated with the specified record contact. Each custom form consists of custom field name-and-value pairs. **API Endpoint**:  GET /v4/inspections/{id}/checklists/{checklistId}/checklistItems/{checklistItemId}/customForms  **Scope**:  inspections  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomFormsExample
    {
        public void main()
        {
            var apiInstance = new InspectionsChecklistsChecklistItemsCustomFormsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = 789;  // long? | The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections).
            var checklistId = 789;  // long? | The ID of the inspection checklist to fetch. See [Get All Inspection Checklists](./api-settings.html#operation/v4.get.settings.inspections.checklists), [Get All Checklists for Inspection](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists).
            var checklistItemId = 789;  // long? | The ID of the inspection checklist item to fetch. See [Get All Checklist Items for Checklist](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists.checklistId.checklistItems).
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Custom Forms for Checklist Item
                ResponseCustomAttributeModelArray result = apiInstance.V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomForms(contentType, authorization, id, checklistId, checklistItemId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsChecklistsChecklistItemsCustomFormsApi.V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomForms: " + e.Message );
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
 **id** | **long?**| The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections). | 
 **checklistId** | **long?**| The ID of the inspection checklist to fetch. See [Get All Inspection Checklists](./api-settings.html#operation/v4.get.settings.inspections.checklists), [Get All Checklists for Inspection](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists). | 
 **checklistItemId** | **long?**| The ID of the inspection checklist item to fetch. See [Get All Checklist Items for Checklist](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists.checklistId.checklistItems). | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseCustomAttributeModelArray**](ResponseCustomAttributeModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getinspectionsidchecklistschecklistidchecklistitemschecklistitemidcustomformsformidmeta"></a>
# **V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomFormsFormIdMeta**
> ResponseCustomFormMetadataModelArray V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomFormsFormIdMeta (string contentType, string authorization, long? id, long? checklistId, long? checklistItemId, string formId, string lang = null)

Get Custom Form Metadata for Checklist Item

Gets the custom form metadata for the specified inspection checklist item. **API Endpoint**:  GET /v4/inspections/{id}/checklists/{checklistId}/checklistItems/{checklistItemId}/customForms/{formId}/meta  **Scope**:  inspections  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomFormsFormIdMetaExample
    {
        public void main()
        {
            var apiInstance = new InspectionsChecklistsChecklistItemsCustomFormsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = 789;  // long? | The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections).
            var checklistId = 789;  // long? | The ID of the inspection checklist to fetch. See [Get All Inspection Checklists](./api-settings.html#operation/v4.get.settings.inspections.checklists), [Get All Checklists for Inspection](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists).
            var checklistItemId = 789;  // long? | The ID of the inspection checklist item to fetch. See [Get All Checklist Items for Checklist](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists.checklistId.checklistItems).
            var formId = formId_example;  // string | The ID of the custom form to fetch.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Custom Form Metadata for Checklist Item
                ResponseCustomFormMetadataModelArray result = apiInstance.V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomFormsFormIdMeta(contentType, authorization, id, checklistId, checklistItemId, formId, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsChecklistsChecklistItemsCustomFormsApi.V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomFormsFormIdMeta: " + e.Message );
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
 **id** | **long?**| The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections). | 
 **checklistId** | **long?**| The ID of the inspection checklist to fetch. See [Get All Inspection Checklists](./api-settings.html#operation/v4.get.settings.inspections.checklists), [Get All Checklists for Inspection](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists). | 
 **checklistItemId** | **long?**| The ID of the inspection checklist item to fetch. See [Get All Checklist Items for Checklist](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists.checklistId.checklistItems). | 
 **formId** | **string**| The ID of the custom form to fetch. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseCustomFormMetadataModelArray**](ResponseCustomFormMetadataModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getinspectionsidchecklistschecklistidchecklistitemschecklistitemidcustomformsmeta"></a>
# **V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomFormsMeta**
> ResponseCustomFormMetadataModelArray V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomFormsMeta (string contentType, string authorization, long? id, long? checklistId, long? checklistItemId, string fields = null, string lang = null)

Get All Custom Forms Metadata for Checklist Item

Gets the custom forms metadata for the specified inspection checklist item. **API Endpoint**:  GET /v4/inspections/{id}/checklists/{checklistId}/checklistItems/{checklistItemId}/customForms/meta  **Scope**:  inspections  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomFormsMetaExample
    {
        public void main()
        {
            var apiInstance = new InspectionsChecklistsChecklistItemsCustomFormsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = 789;  // long? | The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections).
            var checklistId = 789;  // long? | The ID of the inspection checklist to fetch. See [Get All Inspection Checklists](./api-settings.html#operation/v4.get.settings.inspections.checklists), [Get All Checklists for Inspection](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists).
            var checklistItemId = 789;  // long? | The ID of the inspection checklist item to fetch. See [Get All Checklist Items for Checklist](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists.checklistId.checklistItems).
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Custom Forms Metadata for Checklist Item
                ResponseCustomFormMetadataModelArray result = apiInstance.V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomFormsMeta(contentType, authorization, id, checklistId, checklistItemId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsChecklistsChecklistItemsCustomFormsApi.V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomFormsMeta: " + e.Message );
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
 **id** | **long?**| The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections). | 
 **checklistId** | **long?**| The ID of the inspection checklist to fetch. See [Get All Inspection Checklists](./api-settings.html#operation/v4.get.settings.inspections.checklists), [Get All Checklists for Inspection](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists). | 
 **checklistItemId** | **long?**| The ID of the inspection checklist item to fetch. See [Get All Checklist Items for Checklist](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists.checklistId.checklistItems). | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseCustomFormMetadataModelArray**](ResponseCustomFormMetadataModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4putinspectionsidchecklistschecklistidchecklistitemschecklistitemidcustomforms"></a>
# **V4PutInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomForms**
> ResponseCustomAttributeModelArray V4PutInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomForms (string contentType, string authorization, long? id, long? checklistId, long? checklistItemId, List<CustomAttributeModel> body, string lang = null)

Update Custom Forms for Checklist Item

Updates custom forms for the requested checklist item. **API Endpoint**:  PUT /v4/inspections/{id}/checklists/{checklistId}/checklistItems/{checklistItemId}/customForms  **Scope**:  inspections  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4PutInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomFormsExample
    {
        public void main()
        {
            var apiInstance = new InspectionsChecklistsChecklistItemsCustomFormsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = 789;  // long? | The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections).
            var checklistId = 789;  // long? | The ID of the inspection checklist to fetch. See [Get All Inspection Checklists](./api-settings.html#operation/v4.get.settings.inspections.checklists), [Get All Checklists for Inspection](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists).
            var checklistItemId = 789;  // long? | The ID of the inspection checklist item to fetch. See [Get All Checklist Items for Checklist](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists.checklistId.checklistItems).
            var body = new List<CustomAttributeModel>(); // List<CustomAttributeModel> | Custom forms to be updated.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Custom Forms for Checklist Item
                ResponseCustomAttributeModelArray result = apiInstance.V4PutInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomForms(contentType, authorization, id, checklistId, checklistItemId, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsChecklistsChecklistItemsCustomFormsApi.V4PutInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomForms: " + e.Message );
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
 **id** | **long?**| The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections). | 
 **checklistId** | **long?**| The ID of the inspection checklist to fetch. See [Get All Inspection Checklists](./api-settings.html#operation/v4.get.settings.inspections.checklists), [Get All Checklists for Inspection](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists). | 
 **checklistItemId** | **long?**| The ID of the inspection checklist item to fetch. See [Get All Checklist Items for Checklist](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists.checklistId.checklistItems). | 
 **body** | [**List&lt;CustomAttributeModel&gt;**](CustomAttributeModel.md)| Custom forms to be updated. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseCustomAttributeModelArray**](ResponseCustomAttributeModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

