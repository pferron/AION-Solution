# AccelaMiscellanous.Api.AppSettingsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetAppsettings**](AppSettingsApi.md#v4getappsettings) | **GET** /v4/appsettings | Get All App Settings


<a name="v4getappsettings"></a>
# **V4GetAppsettings**
> ResponseAppSettingsArray V4GetAppsettings (string contentType, string authorization, string keys = null, string agency = null, string appId = null, string appSecret = null)

Get All App Settings

Gets the settings for the application. **API Endpoint**:  GET /v4/appsettings  **Scope**:  app_data  **App Type**:  All  **Authorization Type**:  App Credentials  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaMiscellanous.Api;
using AccelaMiscellanous.Client;
using AccelaMiscellanous.Model;

namespace Example
{
    public class V4GetAppsettingsExample
    {
        public void main()
        {
            var apiInstance = new AppSettingsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var keys = keys_example;  // string | Comma-delimited app setting keys filter (optional) 
            var agency = agency_example;  // string | The agency the app belongs to. (optional) 
            var appId = appId_example;  // string | The ID of the registered app on http://developer.accela.com. (optional) 
            var appSecret = appSecret_example;  // string | The secret key of the registered app on http://developer.accela.com. (optional) 

            try
            {
                // Get All App Settings
                ResponseAppSettingsArray result = apiInstance.V4GetAppsettings(contentType, authorization, keys, agency, appId, appSecret);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AppSettingsApi.V4GetAppsettings: " + e.Message );
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
 **keys** | **string**| Comma-delimited app setting keys filter | [optional] 
 **agency** | **string**| The agency the app belongs to. | [optional] 
 **appId** | **string**| The ID of the registered app on http://developer.accela.com. | [optional] 
 **appSecret** | **string**| The secret key of the registered app on http://developer.accela.com. | [optional] 

### Return type

[**ResponseAppSettingsArray**](ResponseAppSettingsArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

