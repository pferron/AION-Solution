# AccelaSettings.Api.SettingsActivitiesApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetSettingsActivitiesPriorities**](SettingsActivitiesApi.md#v4getsettingsactivitiespriorities) | **GET** /v4/settings/activities/priorities | Get All Activity Priorities
[**V4GetSettingsActivitiesStatuses**](SettingsActivitiesApi.md#v4getsettingsactivitiesstatuses) | **GET** /v4/settings/activities/statuses | Get All Activity Statuses
[**V4GetSettingsActivitiesTypes**](SettingsActivitiesApi.md#v4getsettingsactivitiestypes) | **GET** /v4/settings/activities/types | Get All Activity Types


<a name="v4getsettingsactivitiespriorities"></a>
# **V4GetSettingsActivitiesPriorities**
> ResponseSettingValueModelArray V4GetSettingsActivitiesPriorities (string contentType, string authorization, string lang = null)

Get All Activity Priorities

Gets a list of available priorities. **API Endpoint**: GET /v4/settings/activities/priorities **Scope**:  settings  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsActivitiesPrioritiesExample
    {
        public void main()
        {
            var apiInstance = new SettingsActivitiesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Activity Priorities
                ResponseSettingValueModelArray result = apiInstance.V4GetSettingsActivitiesPriorities(contentType, authorization, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsActivitiesApi.V4GetSettingsActivitiesPriorities: " + e.Message );
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

<a name="v4getsettingsactivitiesstatuses"></a>
# **V4GetSettingsActivitiesStatuses**
> ResponseSettingValueModelArray V4GetSettingsActivitiesStatuses (string contentType, string authorization, string lang = null)

Get All Activity Statuses

Gets all status values associated with activities. **API Endpoint**:  GET /v4/settings/activities/statuses  **Scope**:  settings  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsActivitiesStatusesExample
    {
        public void main()
        {
            var apiInstance = new SettingsActivitiesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Activity Statuses
                ResponseSettingValueModelArray result = apiInstance.V4GetSettingsActivitiesStatuses(contentType, authorization, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsActivitiesApi.V4GetSettingsActivitiesStatuses: " + e.Message );
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

<a name="v4getsettingsactivitiestypes"></a>
# **V4GetSettingsActivitiesTypes**
> ResponseSettingValueModelArray V4GetSettingsActivitiesTypes (string contentType, string authorization, string entityType, string lang = null)

Get All Activity Types

Gets all type values associated with activities. **API Endpoint**:  GET /v4/settings/activities/types  **Scope**:  settings  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsActivitiesTypesExample
    {
        public void main()
        {
            var apiInstance = new SettingsActivitiesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var entityType = entityType_example;  // string | Filter by entity type.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Activity Types
                ResponseSettingValueModelArray result = apiInstance.V4GetSettingsActivitiesTypes(contentType, authorization, entityType, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsActivitiesApi.V4GetSettingsActivitiesTypes: " + e.Message );
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
 **entityType** | **string**| Filter by entity type. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseSettingValueModelArray**](ResponseSettingValueModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

