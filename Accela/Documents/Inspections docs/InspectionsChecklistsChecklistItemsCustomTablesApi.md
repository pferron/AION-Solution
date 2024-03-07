# AccelaInspections.Api.InspectionsChecklistsChecklistItemsCustomTablesApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomTables**](InspectionsChecklistsChecklistItemsCustomTablesApi.md#v4getinspectionsidchecklistschecklistidchecklistitemschecklistitemidcustomtables) | **GET** /v4/inspections/{id}/checklists/{checklistId}/checklistItems/{checklistItemId}/customTables | Get All Custom Tables for Inspection Checklist Item
[**V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomTablesMeta**](InspectionsChecklistsChecklistItemsCustomTablesApi.md#v4getinspectionsidchecklistschecklistidchecklistitemschecklistitemidcustomtablesmeta) | **GET** /v4/inspections/{id}/checklists/{checklistId}/checklistItems/{checklistItemId}/customTables/meta | Get All Custom Tables Metadata for an Inspection Checklist Item
[**V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomTablesTableId**](InspectionsChecklistsChecklistItemsCustomTablesApi.md#v4getinspectionsidchecklistschecklistidchecklistitemschecklistitemidcustomtablestableid) | **GET** /v4/inspections/{id}/checklists/{checklistId}/checklistItems/{checklistItemId}/customTables/{tableId} | Get Custom Table for Inspection Checklist Item
[**V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomTablesTableIdMeta**](InspectionsChecklistsChecklistItemsCustomTablesApi.md#v4getinspectionsidchecklistschecklistidchecklistitemschecklistitemidcustomtablestableidmeta) | **GET** /v4/inspections/{id}/checklists/{checklistId}/checklistItems/{checklistItemId}/customTables/{tableId}/meta | Get Custom Table Metadata for an Inspection Checklist Item
[**V4PutInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomTables**](InspectionsChecklistsChecklistItemsCustomTablesApi.md#v4putinspectionsidchecklistschecklistidchecklistitemschecklistitemidcustomtables) | **PUT** /v4/inspections/{id}/checklists/{checklistId}/checklistItems/{checklistItemId}/customTables | Update Custom Tables for Inspection Checklist Item


<a name="v4getinspectionsidchecklistschecklistidchecklistitemschecklistitemidcustomtables"></a>
# **V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomTables**
> ResponseCustomTablesModelArray V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomTables (string contentType, string authorization, long? id, long? checklistId, long? checklistItemId, string fields = null, string lang = null)

Get All Custom Tables for Inspection Checklist Item

Gets the custom tables for the requested checklist item for a specified inspection. **API Endpoint**:  GET /v4/inspections/{id}/checklists/{checklistId}/checklistItems/{checklistItemId}/customTables  **Scope**:  inspections  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomTablesExample
    {
        public void main()
        {
            var apiInstance = new InspectionsChecklistsChecklistItemsCustomTablesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = 789;  // long? | The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections).
            var checklistId = 789;  // long? | The ID of the inspection checklist to fetch. See [Get All Inspection Checklists](./api-settings.html#operation/v4.get.settings.inspections.checklists), [Get All Checklists for Inspection](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists).
            var checklistItemId = 789;  // long? | The ID of the inspection checklist item to fetch. See [Get All Checklist Items for Checklist](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists.checklistId.checklistItems).
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Custom Tables for Inspection Checklist Item
                ResponseCustomTablesModelArray result = apiInstance.V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomTables(contentType, authorization, id, checklistId, checklistItemId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsChecklistsChecklistItemsCustomTablesApi.V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomTables: " + e.Message );
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

[**ResponseCustomTablesModelArray**](ResponseCustomTablesModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getinspectionsidchecklistschecklistidchecklistitemschecklistitemidcustomtablesmeta"></a>
# **V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomTablesMeta**
> ResponseCustomTableMetadataModelArray V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomTablesMeta (string contentType, string authorization, long? id, long? checklistId, long? checklistItemId, string fields = null, string lang = null)

Get All Custom Tables Metadata for an Inspection Checklist Item

Gets the metadata for the custom tables of the requested checklist item for the specified inspection. **API Endpoint**:  GET /v4/inspections/{id}/checklists/{checklistId}/checklistItems/{checklistItemId}/customTables/meta   **Scope**:  inspections  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomTablesMetaExample
    {
        public void main()
        {
            var apiInstance = new InspectionsChecklistsChecklistItemsCustomTablesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = 789;  // long? | The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections).
            var checklistId = 789;  // long? | The ID of the inspection checklist to fetch. See [Get All Inspection Checklists](./api-settings.html#operation/v4.get.settings.inspections.checklists), [Get All Checklists for Inspection](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists).
            var checklistItemId = 789;  // long? | The ID of the inspection checklist item to fetch. See [Get All Checklist Items for Checklist](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists.checklistId.checklistItems).
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Custom Tables Metadata for an Inspection Checklist Item
                ResponseCustomTableMetadataModelArray result = apiInstance.V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomTablesMeta(contentType, authorization, id, checklistId, checklistItemId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsChecklistsChecklistItemsCustomTablesApi.V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomTablesMeta: " + e.Message );
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

[**ResponseCustomTableMetadataModelArray**](ResponseCustomTableMetadataModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getinspectionsidchecklistschecklistidchecklistitemschecklistitemidcustomtablestableid"></a>
# **V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomTablesTableId**
> ResponseCustomTablesModelArray V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomTablesTableId (string contentType, string authorization, long? id, long? checklistId, long? checklistItemId, string tableId, string fields = null, string lang = null)

Get Custom Table for Inspection Checklist Item

Gets a custom table for the requested checklist item for a specified inspection. **API Endpoint**:  GET /v4/inspections/{id}/checklists/{checklistId}/checklistItems/{checklistItemId}/customTables/{tableId}  **Scope**:  inspections  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomTablesTableIdExample
    {
        public void main()
        {
            var apiInstance = new InspectionsChecklistsChecklistItemsCustomTablesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = 789;  // long? | The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections).
            var checklistId = 789;  // long? | The ID of the inspection checklist to fetch. See [Get All Inspection Checklists](./api-settings.html#operation/v4.get.settings.inspections.checklists), [Get All Checklists for Inspection](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists).
            var checklistItemId = 789;  // long? | The ID of the inspection checklist item to fetch. See [Get All Checklist Items for Checklist](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists.checklistId.checklistItems).
            var tableId = tableId_example;  // string | The ID of the custom table to fetch.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Custom Table for Inspection Checklist Item
                ResponseCustomTablesModelArray result = apiInstance.V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomTablesTableId(contentType, authorization, id, checklistId, checklistItemId, tableId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsChecklistsChecklistItemsCustomTablesApi.V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomTablesTableId: " + e.Message );
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
 **tableId** | **string**| The ID of the custom table to fetch. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseCustomTablesModelArray**](ResponseCustomTablesModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getinspectionsidchecklistschecklistidchecklistitemschecklistitemidcustomtablestableidmeta"></a>
# **V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomTablesTableIdMeta**
> ResponseCustomTableMetadataModelArray V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomTablesTableIdMeta (string contentType, string authorization, long? id, long? checklistId, long? checklistItemId, string tableId, string fields = null, string lang = null)

Get Custom Table Metadata for an Inspection Checklist Item

Gets the metadata for the custom tables of the requested checklist item for the specified inspection. **API Endpoint**:  GET /v4/inspections/{id}/checklists/{checklistId}/checklistItems/{checklistItemId}/customTables/{tableId}/meta   **Scope**:  inspections  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomTablesTableIdMetaExample
    {
        public void main()
        {
            var apiInstance = new InspectionsChecklistsChecklistItemsCustomTablesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = 789;  // long? | The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections).
            var checklistId = 789;  // long? | The ID of the inspection checklist to fetch. See [Get All Inspection Checklists](./api-settings.html#operation/v4.get.settings.inspections.checklists), [Get All Checklists for Inspection](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists).
            var checklistItemId = 789;  // long? | The ID of the inspection checklist item to fetch. See [Get All Checklist Items for Checklist](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists.checklistId.checklistItems).
            var tableId = tableId_example;  // string | The ID of the custom table to fetch.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Custom Table Metadata for an Inspection Checklist Item
                ResponseCustomTableMetadataModelArray result = apiInstance.V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomTablesTableIdMeta(contentType, authorization, id, checklistId, checklistItemId, tableId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsChecklistsChecklistItemsCustomTablesApi.V4GetInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomTablesTableIdMeta: " + e.Message );
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
 **tableId** | **string**| The ID of the custom table to fetch. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseCustomTableMetadataModelArray**](ResponseCustomTableMetadataModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4putinspectionsidchecklistschecklistidchecklistitemschecklistitemidcustomtables"></a>
# **V4PutInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomTables**
> ResponseResultModelArray V4PutInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomTables (string contentType, string authorization, long? id, long? checklistId, long? checklistItemId, List<InspectionChecklistItemCustomTable> body, string fields = null, string lang = null)

Update Custom Tables for Inspection Checklist Item

Updates custom tables for the requested checklist item for a specified inspection. **API Endpoint**:  PUT /v4/inspections/{id}/checklists/{checklistId}/checklistItems/{checklistItemId}/customTables  **Scope**:  inspections  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4PutInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomTablesExample
    {
        public void main()
        {
            var apiInstance = new InspectionsChecklistsChecklistItemsCustomTablesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = 789;  // long? | The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections).
            var checklistId = 789;  // long? | The ID of the inspection checklist to fetch. See [Get All Inspection Checklists](./api-settings.html#operation/v4.get.settings.inspections.checklists), [Get All Checklists for Inspection](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists).
            var checklistItemId = 789;  // long? | The ID of the inspection checklist item to fetch. See [Get All Checklist Items for Checklist](./api-inspections.html#operation/v4.get.inspections.inspectionId.checklists.checklistId.checklistItems).
            var body = new List<InspectionChecklistItemCustomTable>(); // List<InspectionChecklistItemCustomTable> | Custom tables to be updated.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Custom Tables for Inspection Checklist Item
                ResponseResultModelArray result = apiInstance.V4PutInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomTables(contentType, authorization, id, checklistId, checklistItemId, body, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsChecklistsChecklistItemsCustomTablesApi.V4PutInspectionsIdChecklistsChecklistIdChecklistItemsChecklistItemIdCustomTables: " + e.Message );
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
 **body** | [**List&lt;InspectionChecklistItemCustomTable&gt;**](InspectionChecklistItemCustomTable.md)| Custom tables to be updated. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

