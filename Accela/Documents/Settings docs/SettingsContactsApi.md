# AccelaSettings.Api.SettingsContactsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetSettingsContactsPreferredChannels**](SettingsContactsApi.md#v4getsettingscontactspreferredchannels) | **GET** /v4/settings/contacts/preferredChannels | Get All Contact Preferred Channels
[**V4GetSettingsContactsRaces**](SettingsContactsApi.md#v4getsettingscontactsraces) | **GET** /v4/settings/contacts/races | Get All Contact Races
[**V4GetSettingsContactsRelations**](SettingsContactsApi.md#v4getsettingscontactsrelations) | **GET** /v4/settings/contacts/relations | Get All Contact Relations
[**V4GetSettingsContactsSalutations**](SettingsContactsApi.md#v4getsettingscontactssalutations) | **GET** /v4/settings/contacts/salutations | Get All Contact Salutations
[**V4GetSettingsContactsTypes**](SettingsContactsApi.md#v4getsettingscontactstypes) | **GET** /v4/settings/contacts/types | Get All Contact Types
[**V4GetSettingsContactsTypesIdCustomForms**](SettingsContactsApi.md#v4getsettingscontactstypesidcustomforms) | **GET** /v4/settings/contacts/types/{id}/customForms | Get All Custom Forms Metadata for Contact Type
[**V4GetSettingsContactsTypesIdCustomTables**](SettingsContactsApi.md#v4getsettingscontactstypesidcustomtables) | **GET** /v4/settings/contacts/types/{id}/customTables | Get All Custom Tables Metadata for Contact Type


<a name="v4getsettingscontactspreferredchannels"></a>
# **V4GetSettingsContactsPreferredChannels**
> ResponseSettingValueModelArray V4GetSettingsContactsPreferredChannels (string contentType, string authorization, string lang = null)

Get All Contact Preferred Channels

Gets the preferred channels for use with a contact. **API Endpoint**:  GET /v4/settings/contacts/preferredChannels  **Scope**:  contacts  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsContactsPreferredChannelsExample
    {
        public void main()
        {
            var apiInstance = new SettingsContactsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Contact Preferred Channels
                ResponseSettingValueModelArray result = apiInstance.V4GetSettingsContactsPreferredChannels(contentType, authorization, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsContactsApi.V4GetSettingsContactsPreferredChannels: " + e.Message );
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

<a name="v4getsettingscontactsraces"></a>
# **V4GetSettingsContactsRaces**
> ResponseSettingValueModelArray V4GetSettingsContactsRaces (string contentType, string authorization, string lang = null)

Get All Contact Races

Gets the races for use with a contact. **API Endpoint**:  GET /v4/settings/contacts/races  **Scope**:  contacts  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsContactsRacesExample
    {
        public void main()
        {
            var apiInstance = new SettingsContactsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Contact Races
                ResponseSettingValueModelArray result = apiInstance.V4GetSettingsContactsRaces(contentType, authorization, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsContactsApi.V4GetSettingsContactsRaces: " + e.Message );
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

<a name="v4getsettingscontactsrelations"></a>
# **V4GetSettingsContactsRelations**
> ResponseSettingValueModelArray V4GetSettingsContactsRelations (string contentType, string authorization, string lang = null)

Get All Contact Relations

Gets the relations for use with a contact. **API Endpoint**:  GET /v4/settings/contacts/relations  **Scope**:  contacts  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsContactsRelationsExample
    {
        public void main()
        {
            var apiInstance = new SettingsContactsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Contact Relations
                ResponseSettingValueModelArray result = apiInstance.V4GetSettingsContactsRelations(contentType, authorization, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsContactsApi.V4GetSettingsContactsRelations: " + e.Message );
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

<a name="v4getsettingscontactssalutations"></a>
# **V4GetSettingsContactsSalutations**
> ResponseSettingValueModelArray V4GetSettingsContactsSalutations (string contentType, string authorization, string lang = null)

Get All Contact Salutations

Gets the salutations for use with a contact. **API Endpoint**:  GET /v4/settings/contacts/salutations  **Scope**:  contacts  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsContactsSalutationsExample
    {
        public void main()
        {
            var apiInstance = new SettingsContactsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Contact Salutations
                ResponseSettingValueModelArray result = apiInstance.V4GetSettingsContactsSalutations(contentType, authorization, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsContactsApi.V4GetSettingsContactsSalutations: " + e.Message );
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

<a name="v4getsettingscontactstypes"></a>
# **V4GetSettingsContactsTypes**
> ResponseSettingValueModelArray V4GetSettingsContactsTypes (string contentType, string authorization, string module = null, string lang = null)

Get All Contact Types

Gets the contact types for use with a contact. **API Endpoint**:  GET /v4/settings/contacts/types  **Scope**:  contacts  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsContactsTypesExample
    {
        public void main()
        {
            var apiInstance = new SettingsContactsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var module = module_example;  // string | Moudle Name (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Contact Types
                ResponseSettingValueModelArray result = apiInstance.V4GetSettingsContactsTypes(contentType, authorization, module, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsContactsApi.V4GetSettingsContactsTypes: " + e.Message );
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
 **module** | **string**| Moudle Name | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseSettingValueModelArray**](ResponseSettingValueModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getsettingscontactstypesidcustomforms"></a>
# **V4GetSettingsContactsTypesIdCustomForms**
> ResponseCustomFormMetadataModelArray V4GetSettingsContactsTypesIdCustomForms (string contentType, string authorization, string id, string lang = null)

Get All Custom Forms Metadata for Contact Type

Get the metadata of all custom forms for the specified contact type. **API Endpoint**:  GET /v4/settings/contacts/types/{id}/customForms  **Scope**:  settings  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsContactsTypesIdCustomFormsExample
    {
        public void main()
        {
            var apiInstance = new SettingsContactsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = id_example;  // string | The ID of the contact type to fetch.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Custom Forms Metadata for Contact Type
                ResponseCustomFormMetadataModelArray result = apiInstance.V4GetSettingsContactsTypesIdCustomForms(contentType, authorization, id, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsContactsApi.V4GetSettingsContactsTypesIdCustomForms: " + e.Message );
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
 **id** | **string**| The ID of the contact type to fetch. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseCustomFormMetadataModelArray**](ResponseCustomFormMetadataModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getsettingscontactstypesidcustomtables"></a>
# **V4GetSettingsContactsTypesIdCustomTables**
> ResponseCustomTableMetadataModelArray V4GetSettingsContactsTypesIdCustomTables (string contentType, string authorization, string id, string fields = null, string lang = null)

Get All Custom Tables Metadata for Contact Type

Gets the metadata for all custom tables for the specified contact type. **API Endpoint**:  GET /v4/settings/contacts/types/{id}/customTables  **Scope**:  settings  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 8.0.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsContactsTypesIdCustomTablesExample
    {
        public void main()
        {
            var apiInstance = new SettingsContactsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = id_example;  // string | The ID of the contact type to fetch.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Custom Tables Metadata for Contact Type
                ResponseCustomTableMetadataModelArray result = apiInstance.V4GetSettingsContactsTypesIdCustomTables(contentType, authorization, id, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsContactsApi.V4GetSettingsContactsTypesIdCustomTables: " + e.Message );
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
 **id** | **string**| The ID of the contact type to fetch. | 
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

