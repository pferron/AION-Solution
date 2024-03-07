# AccelaSettings.Api.SettingsRecordsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetSettingsRecordsConstructionTypes**](SettingsRecordsApi.md#v4getsettingsrecordsconstructiontypes) | **GET** /v4/settings/records/constructionTypes | Get All Record Construction Types
[**V4GetSettingsRecordsExpirationStatuses**](SettingsRecordsApi.md#v4getsettingsrecordsexpirationstatuses) | **GET** /v4/settings/records/expirationStatuses | Get All Record Expiration Statuses
[**V4GetSettingsRecordsTypes**](SettingsRecordsApi.md#v4getsettingsrecordstypes) | **GET** /v4/settings/records/types | Get All Record Types
[**V4GetSettingsRecordsTypesIdCustomForms**](SettingsRecordsApi.md#v4getsettingsrecordstypesidcustomforms) | **GET** /v4/settings/records/types/{id}/customForms | Get All Custom Forms Metadata for Record Type
[**V4GetSettingsRecordsTypesIdCustomTables**](SettingsRecordsApi.md#v4getsettingsrecordstypesidcustomtables) | **GET** /v4/settings/records/types/{id}/customTables | Get All Custom Tables Metadata for Record Type
[**V4GetSettingsRecordsTypesIdFeesSchedules**](SettingsRecordsApi.md#v4getsettingsrecordstypesidfeesschedules) | **GET** /v4/settings/records/types/{id}/fees/schedules | Get All Fee Schedules for Record Type
[**V4GetSettingsRecordsTypesIdStatuses**](SettingsRecordsApi.md#v4getsettingsrecordstypesidstatuses) | **GET** /v4/settings/records/types/{id}/statuses | Get All Statuses for Record Type
[**V4GetSettingsRecordsTypesRecordTypeIdAssetTypes**](SettingsRecordsApi.md#v4getsettingsrecordstypesrecordtypeidassettypes) | **GET** /v4/settings/records/types/{recordTypeId}/assetTypes | Get All Asset Types for Record Type


<a name="v4getsettingsrecordsconstructiontypes"></a>
# **V4GetSettingsRecordsConstructionTypes**
> ResponseSettingValueModelArray V4GetSettingsRecordsConstructionTypes (string contentType, string authorization, string lang = null)

Get All Record Construction Types

Gets the construction types for the specified record type. **API Endpoint**:  GET /v4/settings/records/constructionTypes  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsRecordsConstructionTypesExample
    {
        public void main()
        {
            var apiInstance = new SettingsRecordsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Record Construction Types
                ResponseSettingValueModelArray result = apiInstance.V4GetSettingsRecordsConstructionTypes(contentType, authorization, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsRecordsApi.V4GetSettingsRecordsConstructionTypes: " + e.Message );
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

[**ResponseSettingValueModelArray**](ResponseSettingValueModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getsettingsrecordsexpirationstatuses"></a>
# **V4GetSettingsRecordsExpirationStatuses**
> ResponseSettingValueModelArray V4GetSettingsRecordsExpirationStatuses (string contentType, string authorization, string lang = null)

Get All Record Expiration Statuses

Gets the expiration status values for use with records.. **API Endpoint**:  GET /v4/settings/records/expirationStatuses  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsRecordsExpirationStatusesExample
    {
        public void main()
        {
            var apiInstance = new SettingsRecordsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Record Expiration Statuses
                ResponseSettingValueModelArray result = apiInstance.V4GetSettingsRecordsExpirationStatuses(contentType, authorization, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsRecordsApi.V4GetSettingsRecordsExpirationStatuses: " + e.Message );
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

[**ResponseSettingValueModelArray**](ResponseSettingValueModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getsettingsrecordstypes"></a>
# **V4GetSettingsRecordsTypes**
> ResponseRecordTypeModelArray V4GetSettingsRecordsTypes (string contentType, string module, long? offset = null, long? limit = null, bool? isFeeEstimate = null, string action = null, string expand = null, string filterName = null, string fields = null, string lang = null)

Get All Record Types

Gets predefined record types. **API Endpoint**:  GET /v4/settings/records/types  **Scope**:  records  **App Type**:  All  **Authorization Type**:  No authorization required  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsRecordsTypesExample
    {
        public void main()
        {
            var apiInstance = new SettingsRecordsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var module = module_example;  // string | Filter by module.
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var isFeeEstimate = true;  // bool? | Filter by whether or not it is a fee estimate. (optional) 
            var action = action_example;  // string | Filter by action associated with the record type. (optional) 
            var expand = expand_example;  // string | The related objects to be returned with the response. The related object(s) will be returned if data exists; if data does not exist, the requested object(s) will not be included in the response. (optional) 
            var filterName = filterName_example;  // string | Filter by record type filter name. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Record Types
                ResponseRecordTypeModelArray result = apiInstance.V4GetSettingsRecordsTypes(contentType, module, offset, limit, isFeeEstimate, action, expand, filterName, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsRecordsApi.V4GetSettingsRecordsTypes: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | [default to application/x-www-form-urlencoded]
 **module** | **string**| Filter by module. | 
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 
 **isFeeEstimate** | **bool?**| Filter by whether or not it is a fee estimate. | [optional] 
 **action** | **string**| Filter by action associated with the record type. | [optional] 
 **expand** | **string**| The related objects to be returned with the response. The related object(s) will be returned if data exists; if data does not exist, the requested object(s) will not be included in the response. | [optional] 
 **filterName** | **string**| Filter by record type filter name. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseRecordTypeModelArray**](ResponseRecordTypeModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getsettingsrecordstypesidcustomforms"></a>
# **V4GetSettingsRecordsTypesIdCustomForms**
> ResponseCustomFormMetadataModelArray V4GetSettingsRecordsTypesIdCustomForms (string contentType, string id, string fields = null, string lang = null)

Get All Custom Forms Metadata for Record Type

Gets the metadata of custom forms for a specified record type. **API Endpoint**:  GET /v4/settings/records/types/{id}/customForms  **Scope**:  records  **App Type**:  All  **Authorization Type**:  No authorization required  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsRecordsTypesIdCustomFormsExample
    {
        public void main()
        {
            var apiInstance = new SettingsRecordsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var id = id_example;  // string | The id of the record type to fetch.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Custom Forms Metadata for Record Type
                ResponseCustomFormMetadataModelArray result = apiInstance.V4GetSettingsRecordsTypesIdCustomForms(contentType, id, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsRecordsApi.V4GetSettingsRecordsTypesIdCustomForms: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | [default to application/x-www-form-urlencoded]
 **id** | **string**| The id of the record type to fetch. | 
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

<a name="v4getsettingsrecordstypesidcustomtables"></a>
# **V4GetSettingsRecordsTypesIdCustomTables**
> ResponseCustomTableMetadataModelArray V4GetSettingsRecordsTypesIdCustomTables (string contentType, string id, string fields = null, string lang = null)

Get All Custom Tables Metadata for Record Type

Gets the metadata of all custom tables for a specified record type. **API Endpoint**:  GET /v4/settings/records/types/{id}/customTables  **Scope**:  records  **App Type**:  All  **Authorization Type**:  No authorization required  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsRecordsTypesIdCustomTablesExample
    {
        public void main()
        {
            var apiInstance = new SettingsRecordsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var id = id_example;  // string | The id of the record type to fetch.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Custom Tables Metadata for Record Type
                ResponseCustomTableMetadataModelArray result = apiInstance.V4GetSettingsRecordsTypesIdCustomTables(contentType, id, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsRecordsApi.V4GetSettingsRecordsTypesIdCustomTables: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | [default to application/x-www-form-urlencoded]
 **id** | **string**| The id of the record type to fetch. | 
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

<a name="v4getsettingsrecordstypesidfeesschedules"></a>
# **V4GetSettingsRecordsTypesIdFeesSchedules**
> ResponseSettingValueModelArray V4GetSettingsRecordsTypesIdFeesSchedules (string contentType, string authorization, string id, string fields = null, string lang = null)

Get All Fee Schedules for Record Type

Gets the types of available fee schedules. **API Endpoint**:  GET /v4/settings/records/types/{id}/fees/schedules  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsRecordsTypesIdFeesSchedulesExample
    {
        public void main()
        {
            var apiInstance = new SettingsRecordsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = id_example;  // string | The ID of the record type to fetch.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Fee Schedules for Record Type
                ResponseSettingValueModelArray result = apiInstance.V4GetSettingsRecordsTypesIdFeesSchedules(contentType, authorization, id, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsRecordsApi.V4GetSettingsRecordsTypesIdFeesSchedules: " + e.Message );
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
 **id** | **string**| The ID of the record type to fetch. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseSettingValueModelArray**](ResponseSettingValueModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getsettingsrecordstypesidstatuses"></a>
# **V4GetSettingsRecordsTypesIdStatuses**
> ResponseRecordStatusModelArray V4GetSettingsRecordsTypesIdStatuses (string contentType, string authorization, string id, string lang = null)

Get All Statuses for Record Type

Gets the status values for use with a specified record type. **API Endpoint**:  GET /v4/settings/records/types/{id}/statuses  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsRecordsTypesIdStatusesExample
    {
        public void main()
        {
            var apiInstance = new SettingsRecordsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = id_example;  // string | The ID of the record type to fetch.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Statuses for Record Type
                ResponseRecordStatusModelArray result = apiInstance.V4GetSettingsRecordsTypesIdStatuses(contentType, authorization, id, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsRecordsApi.V4GetSettingsRecordsTypesIdStatuses: " + e.Message );
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
 **id** | **string**| The ID of the record type to fetch. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseRecordStatusModelArray**](ResponseRecordStatusModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getsettingsrecordstypesrecordtypeidassettypes"></a>
# **V4GetSettingsRecordsTypesRecordTypeIdAssetTypes**
> ResponseRecordTypeAssetTypeModelArray V4GetSettingsRecordsTypesRecordTypeIdAssetTypes (string contentType, string recordTypeId, string group = null, long? offset = null, long? limit = null, string fields = null, string lang = null)

Get All Asset Types for Record Type

Returns all asset types for a given record type. **API Endpoint**:  GET /v4/settings/records/types/{recordTypeId}/assetTypes  **Scope**:  records  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 9.0.0 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsRecordsTypesRecordTypeIdAssetTypesExample
    {
        public void main()
        {
            var apiInstance = new SettingsRecordsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var recordTypeId = recordTypeId_example;  // string | The id of the record type to fetch.
            var group = group_example;  // string | Filter by asset group. (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Asset Types for Record Type
                ResponseRecordTypeAssetTypeModelArray result = apiInstance.V4GetSettingsRecordsTypesRecordTypeIdAssetTypes(contentType, recordTypeId, group, offset, limit, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsRecordsApi.V4GetSettingsRecordsTypesRecordTypeIdAssetTypes: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | [default to application/x-www-form-urlencoded]
 **recordTypeId** | **string**| The id of the record type to fetch. | 
 **group** | **string**| Filter by asset group. | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseRecordTypeAssetTypeModelArray**](ResponseRecordTypeAssetTypeModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

