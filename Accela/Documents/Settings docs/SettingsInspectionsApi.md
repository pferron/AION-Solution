# AccelaSettings.Api.SettingsInspectionsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetSettingsInspectionsChecklistIdChecklistItemIdCommentsGroups**](SettingsInspectionsApi.md#v4getsettingsinspectionschecklistidchecklistitemidcommentsgroups) | **GET** /v4/settings/inspections/checklists/{checklistId}/checklistItems/{checklistItemId}/comments/groups | Get All Standard Comment Groups for Inspection Checklist
[**V4GetSettingsInspectionsChecklists**](SettingsInspectionsApi.md#v4getsettingsinspectionschecklists) | **GET** /v4/settings/inspections/checklists | Get All Checklists
[**V4GetSettingsInspectionsChecklistsChecklistIdChecklistItemsChecklistItemIdCustomForms**](SettingsInspectionsApi.md#v4getsettingsinspectionschecklistschecklistidchecklistitemschecklistitemidcustomforms) | **GET** /v4/settings/inspections/checklists/{checklistId}/checklistItems/{checklistItemid}/customForms | Get All Custom Forms Metadata for Checklist Item
[**V4GetSettingsInspectionsChecklistsChecklistIdChecklistItemsChecklistItemIdCustomTables**](SettingsInspectionsApi.md#v4getsettingsinspectionschecklistschecklistidchecklistitemschecklistitemidcustomtables) | **GET** /v4/settings/inspections/checklists/{checklistId}/checklistItems/{checklistItemid}/customTables | Get All Custom Tables Metadata for Checklist Item
[**V4GetSettingsInspectionsChecklistsGroups**](SettingsInspectionsApi.md#v4getsettingsinspectionschecklistsgroups) | **GET** /v4/settings/inspections/checklistsGroups | Get All Checklist Groups
[**V4GetSettingsInspectionsChecklistsIds**](SettingsInspectionsApi.md#v4getsettingsinspectionschecklistsids) | **GET** /v4/settings/inspections/checklists/{ids} | Get Checklists
[**V4GetSettingsInspectionsChecklistsStatuses**](SettingsInspectionsApi.md#v4getsettingsinspectionschecklistsstatuses) | **GET** /v4/settings/inspections/checklists/statuses | Get Inspection Checklist Statuses
[**V4GetSettingsInspectionsGrades**](SettingsInspectionsApi.md#v4getsettingsinspectionsgrades) | **GET** /v4/settings/inspections/grades | Get All Inspection Grades
[**V4GetSettingsInspectionsStatuses**](SettingsInspectionsApi.md#v4getsettingsinspectionsstatuses) | **GET** /v4/settings/inspections/statuses | Get All Inspection Statuses
[**V4GetSettingsInspectionsTypeCommentsGroups**](SettingsInspectionsApi.md#v4getsettingsinspectionstypecommentsgroups) | **GET** /v4/settings/inspections/types/{id}/comments/groups | Get All Standard Comment Groups for Inspection Type
[**V4GetSettingsInspectionsTypes**](SettingsInspectionsApi.md#v4getsettingsinspectionstypes) | **GET** /v4/settings/inspections/types | Get All Inspection Types
[**V4GetSettingsInspectionsTypesIds**](SettingsInspectionsApi.md#v4getsettingsinspectionstypesids) | **GET** /v4/settings/inspections/types/{ids} | Get Inspection Types


<a name="v4getsettingsinspectionschecklistidchecklistitemidcommentsgroups"></a>
# **V4GetSettingsInspectionsChecklistIdChecklistItemIdCommentsGroups**
> ResponseStandardCommentGroupModelArray V4GetSettingsInspectionsChecklistIdChecklistItemIdCommentsGroups (string contentType, string authorization, long? checklistId, long? checklistItemId, string lang = null, long? offset = null, long? limit = null)

Get All Standard Comment Groups for Inspection Checklist

Gets the standard comment groups for a given checklist item. **API Endpoint**:  GET /v4/settings/inspections/checklists/{checklistId}/checklistItems/{checklistItemId}/comments/groups  **Scope**:  inspections  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 9.3.0  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsInspectionsChecklistIdChecklistItemIdCommentsGroupsExample
    {
        public void main()
        {
            var apiInstance = new SettingsInspectionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var checklistId = 789;  // long? | The system id of the checklist the item belongs to. See [Get All Checklists](./api-settings.html#operation/v4.get.settings.inspections.checklists).
            var checklistItemId = 789;  // long? | The system id of the checklist item to fetch. For valid values, use the id field in items[] returned by [Get All Checklists](./api-settings.html#operation/v4.get.settings.inspections.checklists).
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 

            try
            {
                // Get All Standard Comment Groups for Inspection Checklist
                ResponseStandardCommentGroupModelArray result = apiInstance.V4GetSettingsInspectionsChecklistIdChecklistItemIdCommentsGroups(contentType, authorization, checklistId, checklistItemId, lang, offset, limit);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsInspectionsApi.V4GetSettingsInspectionsChecklistIdChecklistItemIdCommentsGroups: " + e.Message );
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
 **checklistId** | **long?**| The system id of the checklist the item belongs to. See [Get All Checklists](./api-settings.html#operation/v4.get.settings.inspections.checklists). | 
 **checklistItemId** | **long?**| The system id of the checklist item to fetch. For valid values, use the id field in items[] returned by [Get All Checklists](./api-settings.html#operation/v4.get.settings.inspections.checklists). | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 

### Return type

[**ResponseStandardCommentGroupModelArray**](ResponseStandardCommentGroupModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getsettingsinspectionschecklists"></a>
# **V4GetSettingsInspectionsChecklists**
> ResponseChecklistModelArray V4GetSettingsInspectionsChecklists (string contentType, string authorization, string group = null, string fields = null, long? offset = null, long? limit = null, string lang = null)

Get All Checklists

Gets all checklists you can use for an inspection. **API Endpoint**:  GET /v4/settings/inspections/checklists  **Scope**:  inspections  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsInspectionsChecklistsExample
    {
        public void main()
        {
            var apiInstance = new SettingsInspectionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var group = group_example;  // string | Filter by checklist group. See [Get All Checklist Groups](./api-settings.html#operation/v4.get.settings.inspections.checklistsGroups). (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Checklists
                ResponseChecklistModelArray result = apiInstance.V4GetSettingsInspectionsChecklists(contentType, authorization, group, fields, offset, limit, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsInspectionsApi.V4GetSettingsInspectionsChecklists: " + e.Message );
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
 **group** | **string**| Filter by checklist group. See [Get All Checklist Groups](./api-settings.html#operation/v4.get.settings.inspections.checklistsGroups). | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseChecklistModelArray**](ResponseChecklistModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getsettingsinspectionschecklistschecklistidchecklistitemschecklistitemidcustomforms"></a>
# **V4GetSettingsInspectionsChecklistsChecklistIdChecklistItemsChecklistItemIdCustomForms**
> ResponseCustomFormMetadataModelArray V4GetSettingsInspectionsChecklistsChecklistIdChecklistItemsChecklistItemIdCustomForms (string contentType, string authorization, string checklistId, long? checklistItemid, string lang = null)

Get All Custom Forms Metadata for Checklist Item

Gets the metadata for all custom forms for the specified checklist item. **API Endpoint**:  GET /v4/settings/inspections/checklists/{checklistId}/checklistItems/{checklistItemid}/customForms  **Scope**:  inspections  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsInspectionsChecklistsChecklistIdChecklistItemsChecklistItemIdCustomFormsExample
    {
        public void main()
        {
            var apiInstance = new SettingsInspectionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var checklistId = checklistId_example;  // string | The ID of the checklist to fetch.
            var checklistItemid = 789;  // long? | The ID of the checklist item to fetch.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Custom Forms Metadata for Checklist Item
                ResponseCustomFormMetadataModelArray result = apiInstance.V4GetSettingsInspectionsChecklistsChecklistIdChecklistItemsChecklistItemIdCustomForms(contentType, authorization, checklistId, checklistItemid, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsInspectionsApi.V4GetSettingsInspectionsChecklistsChecklistIdChecklistItemsChecklistItemIdCustomForms: " + e.Message );
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
 **checklistId** | **string**| The ID of the checklist to fetch. | 
 **checklistItemid** | **long?**| The ID of the checklist item to fetch. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseCustomFormMetadataModelArray**](ResponseCustomFormMetadataModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getsettingsinspectionschecklistschecklistidchecklistitemschecklistitemidcustomtables"></a>
# **V4GetSettingsInspectionsChecklistsChecklistIdChecklistItemsChecklistItemIdCustomTables**
> ResponseCustomTableMetadataModelArray V4GetSettingsInspectionsChecklistsChecklistIdChecklistItemsChecklistItemIdCustomTables (string contentType, string authorization, long? checklistId, long? checklistItemid, string lang = null)

Get All Custom Tables Metadata for Checklist Item

Gets the metadata of all custom tables for the specified checklist item. **API Endpoint**:  GET /v4/settings/inspections/checklists/{checklistId}/checklistItems/{checklistItemid}/customTables  **Scope**:  inspections  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsInspectionsChecklistsChecklistIdChecklistItemsChecklistItemIdCustomTablesExample
    {
        public void main()
        {
            var apiInstance = new SettingsInspectionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var checklistId = 789;  // long? | The id of the checklist to fetch.
            var checklistItemid = 789;  // long? | The id of the checklist item to fetch.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Custom Tables Metadata for Checklist Item
                ResponseCustomTableMetadataModelArray result = apiInstance.V4GetSettingsInspectionsChecklistsChecklistIdChecklistItemsChecklistItemIdCustomTables(contentType, authorization, checklistId, checklistItemid, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsInspectionsApi.V4GetSettingsInspectionsChecklistsChecklistIdChecklistItemsChecklistItemIdCustomTables: " + e.Message );
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
 **checklistId** | **long?**| The id of the checklist to fetch. | 
 **checklistItemid** | **long?**| The id of the checklist item to fetch. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseCustomTableMetadataModelArray**](ResponseCustomTableMetadataModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getsettingsinspectionschecklistsgroups"></a>
# **V4GetSettingsInspectionsChecklistsGroups**
> ResponseChecklistGroupModelArray V4GetSettingsInspectionsChecklistsGroups (string contentType, string authorization, string lang = null)

Get All Checklist Groups

Gets the checklist groups for use with an inspection. **API Endpoint**:  GET /v4/settings/inspections/checklistsGroups  **Scope**:  inspections  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsInspectionsChecklistsGroupsExample
    {
        public void main()
        {
            var apiInstance = new SettingsInspectionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Checklist Groups
                ResponseChecklistGroupModelArray result = apiInstance.V4GetSettingsInspectionsChecklistsGroups(contentType, authorization, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsInspectionsApi.V4GetSettingsInspectionsChecklistsGroups: " + e.Message );
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

[**ResponseChecklistGroupModelArray**](ResponseChecklistGroupModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getsettingsinspectionschecklistsids"></a>
# **V4GetSettingsInspectionsChecklistsIds**
> ResponseChecklistModelArray V4GetSettingsInspectionsChecklistsIds (string contentType, string authorization, string ids, string fields = null, string lang = null)

Get Checklists

Gets specific checklists. **API Endpoint**:  GET /v4/settings/inspections/checklists/{ids}  **Scope**:  inspections  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsInspectionsChecklistsIdsExample
    {
        public void main()
        {
            var apiInstance = new SettingsInspectionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var ids = ids_example;  // string | Comma-delimited IDs of the checklists to fetch.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Checklists
                ResponseChecklistModelArray result = apiInstance.V4GetSettingsInspectionsChecklistsIds(contentType, authorization, ids, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsInspectionsApi.V4GetSettingsInspectionsChecklistsIds: " + e.Message );
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
 **ids** | **string**| Comma-delimited IDs of the checklists to fetch. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseChecklistModelArray**](ResponseChecklistModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getsettingsinspectionschecklistsstatuses"></a>
# **V4GetSettingsInspectionsChecklistsStatuses**
> ResponseChecklistItemStatusModelArray V4GetSettingsInspectionsChecklistsStatuses (string contentType, string authorization, string group, string lang = null)

Get Inspection Checklist Statuses

Gets the status values for checklists **API Endpoint**:  GET /v4/settings/inspections/checklists/statuses  **Scope**:  inspections  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsInspectionsChecklistsStatusesExample
    {
        public void main()
        {
            var apiInstance = new SettingsInspectionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var group = group_example;  // string | Filter by status group
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Inspection Checklist Statuses
                ResponseChecklistItemStatusModelArray result = apiInstance.V4GetSettingsInspectionsChecklistsStatuses(contentType, authorization, group, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsInspectionsApi.V4GetSettingsInspectionsChecklistsStatuses: " + e.Message );
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
 **group** | **string**| Filter by status group | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseChecklistItemStatusModelArray**](ResponseChecklistItemStatusModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getsettingsinspectionsgrades"></a>
# **V4GetSettingsInspectionsGrades**
> ResponseInspectionGradeModelArray V4GetSettingsInspectionsGrades (string contentType, string authorization, string group = null, string fields = null, string lang = null)

Get All Inspection Grades

Gets all available grades for use in an inspection. **API Endpoint**:  GET /v4/settings/inspections/grades  **Scope**:  inspections  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsInspectionsGradesExample
    {
        public void main()
        {
            var apiInstance = new SettingsInspectionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var group = group_example;  // string | Filter by the inspection grade group (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Inspection Grades
                ResponseInspectionGradeModelArray result = apiInstance.V4GetSettingsInspectionsGrades(contentType, authorization, group, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsInspectionsApi.V4GetSettingsInspectionsGrades: " + e.Message );
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
 **group** | **string**| Filter by the inspection grade group | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseInspectionGradeModelArray**](ResponseInspectionGradeModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getsettingsinspectionsstatuses"></a>
# **V4GetSettingsInspectionsStatuses**
> ResponseInspectionResultStatusModelArray V4GetSettingsInspectionsStatuses (string contentType, string authorization, string group = null, string fields = null, string lang = null)

Get All Inspection Statuses

Gets all available status values for use in an inspection. **API Endpoint**:  GET /v4/settings/inspections/statuses **Scope**:  inspections  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsInspectionsStatusesExample
    {
        public void main()
        {
            var apiInstance = new SettingsInspectionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var group = group_example;  // string | Filter by the inspection status group. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Inspection Statuses
                ResponseInspectionResultStatusModelArray result = apiInstance.V4GetSettingsInspectionsStatuses(contentType, authorization, group, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsInspectionsApi.V4GetSettingsInspectionsStatuses: " + e.Message );
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
 **group** | **string**| Filter by the inspection status group. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseInspectionResultStatusModelArray**](ResponseInspectionResultStatusModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getsettingsinspectionstypecommentsgroups"></a>
# **V4GetSettingsInspectionsTypeCommentsGroups**
> ResponseStandardCommentGroupModelArray V4GetSettingsInspectionsTypeCommentsGroups (string contentType, string authorization, long? id, string lang = null, long? offset = null, long? limit = null)

Get All Standard Comment Groups for Inspection Type

Gets the standard comment groups for a given inspection type. **API Endpoint**:  GET /v4/settings/inspections/types/{id}/comments/groups  **Scope**:  inspections  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 9.3.0  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsInspectionsTypeCommentsGroupsExample
    {
        public void main()
        {
            var apiInstance = new SettingsInspectionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = 789;  // long? | The system id of the inspection type to fetch. See [Get All Inspection Types](./api-settings.html#operation/v4.get.settings.inspections.types).
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 

            try
            {
                // Get All Standard Comment Groups for Inspection Type
                ResponseStandardCommentGroupModelArray result = apiInstance.V4GetSettingsInspectionsTypeCommentsGroups(contentType, authorization, id, lang, offset, limit);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsInspectionsApi.V4GetSettingsInspectionsTypeCommentsGroups: " + e.Message );
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
 **id** | **long?**| The system id of the inspection type to fetch. See [Get All Inspection Types](./api-settings.html#operation/v4.get.settings.inspections.types). | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 

### Return type

[**ResponseStandardCommentGroupModelArray**](ResponseStandardCommentGroupModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getsettingsinspectionstypes"></a>
# **V4GetSettingsInspectionsTypes**
> ResponseInspectionTypeModelArray V4GetSettingsInspectionsTypes (string contentType, string authorization, string group = null, string expand = null, long? limit = null, long? offset = null, string fields = null, string lang = null)

Get All Inspection Types

Gets all available inspection types. **API Endpoint**:  GET v4/settings/inspections/types  **Scope**:  inspections  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsInspectionsTypesExample
    {
        public void main()
        {
            var apiInstance = new SettingsInspectionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var group = group_example;  // string | Filter by inspection type group code. (optional) 
            var expand = expand_example;  // string | The related objects to be returned with the response. The related object(s) will be returned if data exists; if data does not exist, the requested object(s) will not be included in the response. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Inspection Types
                ResponseInspectionTypeModelArray result = apiInstance.V4GetSettingsInspectionsTypes(contentType, authorization, group, expand, limit, offset, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsInspectionsApi.V4GetSettingsInspectionsTypes: " + e.Message );
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
 **group** | **string**| Filter by inspection type group code. | [optional] 
 **expand** | **string**| The related objects to be returned with the response. The related object(s) will be returned if data exists; if data does not exist, the requested object(s) will not be included in the response. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseInspectionTypeModelArray**](ResponseInspectionTypeModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getsettingsinspectionstypesids"></a>
# **V4GetSettingsInspectionsTypesIds**
> ResponseInspectionTypeModelArray V4GetSettingsInspectionsTypesIds (string contentType, string authorization, string ids, string expand = null, string fields = null, string lang = null)

Get Inspection Types

Gets specific inspection types. **API Endpoint**:  GET /v4/settings/inspections/types/{ids}  **Scope**:  inspections  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsInspectionsTypesIdsExample
    {
        public void main()
        {
            var apiInstance = new SettingsInspectionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var ids = ids_example;  // string | Comma-delimited IDs of inspection types to fetch.
            var expand = expand_example;  // string | The related objects to be returned with the response. The related object(s) will be returned if data exists; if data does not exist, the requested object(s) will not be included in the response. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Inspection Types
                ResponseInspectionTypeModelArray result = apiInstance.V4GetSettingsInspectionsTypesIds(contentType, authorization, ids, expand, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsInspectionsApi.V4GetSettingsInspectionsTypesIds: " + e.Message );
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
 **ids** | **string**| Comma-delimited IDs of inspection types to fetch. | 
 **expand** | **string**| The related objects to be returned with the response. The related object(s) will be returned if data exists; if data does not exist, the requested object(s) will not be included in the response. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseInspectionTypeModelArray**](ResponseInspectionTypeModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

