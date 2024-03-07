# AccelaSettings.Api.SettingsAssetsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetSettingsAssetsGroups**](SettingsAssetsApi.md#v4getsettingsassetsgroups) | **GET** /v4/settings/assets/groups | Get All Asset Groups
[**V4GetSettingsAssetsSizeUnits**](SettingsAssetsApi.md#v4getsettingsassetssizeunits) | **GET** /v4/settings/assets/sizeUnits | Get All Asset Size Units
[**V4GetSettingsAssetsStatuses**](SettingsAssetsApi.md#v4getsettingsassetsstatuses) | **GET** /v4/settings/assets/statuses | Get All Asset Statuses


<a name="v4getsettingsassetsgroups"></a>
# **V4GetSettingsAssetsGroups**
> ResponseSettingValueModelArray V4GetSettingsAssetsGroups (string contentType, string fields = null, string lang = null)

Get All Asset Groups

Get all configured asset groups. **API Endpoint**:  GET /v4/settings/assets/groups  **Scope**:  assets  **App Type**:  Agency  **Authorization Type**:  No authorization required  **Civic Platform version**: 9.0.0 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsAssetsGroupsExample
    {
        public void main()
        {
            var apiInstance = new SettingsAssetsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Asset Groups
                ResponseSettingValueModelArray result = apiInstance.V4GetSettingsAssetsGroups(contentType, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsAssetsApi.V4GetSettingsAssetsGroups: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | [default to application/x-www-form-urlencoded]
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

<a name="v4getsettingsassetssizeunits"></a>
# **V4GetSettingsAssetsSizeUnits**
> ResponseSettingValueModelArray V4GetSettingsAssetsSizeUnits (string contentType, string lang = null)

Get All Asset Size Units

Gets all configured asset size units. **API Endpoint**:  GET /v4/settings/assets/sizeUnits  **Scope**:  assets  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 9.0.0 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsAssetsSizeUnitsExample
    {
        public void main()
        {
            var apiInstance = new SettingsAssetsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Asset Size Units
                ResponseSettingValueModelArray result = apiInstance.V4GetSettingsAssetsSizeUnits(contentType, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsAssetsApi.V4GetSettingsAssetsSizeUnits: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | [default to application/x-www-form-urlencoded]
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseSettingValueModelArray**](ResponseSettingValueModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getsettingsassetsstatuses"></a>
# **V4GetSettingsAssetsStatuses**
> ResponseSettingValueModelArray V4GetSettingsAssetsStatuses (string contentType, string lang = null)

Get All Asset Statuses

Gets all configured asset statuses. **API Endpoint**:  GET /v4/settings/assets/statuses  **Scope**:  assets  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 9.0.0 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsAssetsStatusesExample
    {
        public void main()
        {
            var apiInstance = new SettingsAssetsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Asset Statuses
                ResponseSettingValueModelArray result = apiInstance.V4GetSettingsAssetsStatuses(contentType, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsAssetsApi.V4GetSettingsAssetsStatuses: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | [default to application/x-www-form-urlencoded]
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseSettingValueModelArray**](ResponseSettingValueModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

