# AccelaSettings.Api.SettingsPartsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetSettingsPartsLocations**](SettingsPartsApi.md#v4getsettingspartslocations) | **GET** /v4/settings/parts/locations | Get All Part Locations
[**V4GetSettingsPartsTypes**](SettingsPartsApi.md#v4getsettingspartstypes) | **GET** /v4/settings/parts/types | Get Part Types


<a name="v4getsettingspartslocations"></a>
# **V4GetSettingsPartsLocations**
> ResponsePartLocationModelArray V4GetSettingsPartsLocations (string contentType, string transactionType = null, string partId = null, string lang = null)

Get All Part Locations

Gets all part locations in the system. **API Endpoint**:  GET /v4/settings/parts/locations  **Scope**:  settings  **App Type**:  All  **Authorization Type**:  No authorization required  **Civic Platform version**: 7.3.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsPartsLocationsExample
    {
        public void main()
        {
            var apiInstance = new SettingsPartsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var transactionType = transactionType_example;  // string | Filter by the transaction type. (optional) 
            var partId = partId_example;  // string | Filter by part id. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Part Locations
                ResponsePartLocationModelArray result = apiInstance.V4GetSettingsPartsLocations(contentType, transactionType, partId, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsPartsApi.V4GetSettingsPartsLocations: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | [default to application/x-www-form-urlencoded]
 **transactionType** | **string**| Filter by the transaction type. | [optional] 
 **partId** | **string**| Filter by part id. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponsePartLocationModelArray**](ResponsePartLocationModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getsettingspartstypes"></a>
# **V4GetSettingsPartsTypes**
> ResponseSettingValueModelArray V4GetSettingsPartsTypes (string contentType, string authorization, string lang = null)

Get Part Types

Gets the part type settings. **API Endpoint**:  GET /v4/settings/parts/types  **Scope**:  settings  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsPartsTypesExample
    {
        public void main()
        {
            var apiInstance = new SettingsPartsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Part Types
                ResponseSettingValueModelArray result = apiInstance.V4GetSettingsPartsTypes(contentType, authorization, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsPartsApi.V4GetSettingsPartsTypes: " + e.Message );
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

