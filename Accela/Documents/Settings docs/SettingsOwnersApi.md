# AccelaSettings.Api.SettingsOwnersApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetSettingsOwnersCustomForms**](SettingsOwnersApi.md#v4getsettingsownerscustomforms) | **GET** /v4/settings/owners/customForms | Get Metadata for Owner Custom Forms


<a name="v4getsettingsownerscustomforms"></a>
# **V4GetSettingsOwnersCustomForms**
> ResponseApoCustomFormsMetadata V4GetSettingsOwnersCustomForms (string contentType, string authorization, string lang = null)

Get Metadata for Owner Custom Forms

Returns the field metadata for all owner custom forms (owner templates) in the system. **API Endpoint**:  GET /v4/settings/owners/customForms   **Scope**:  owners  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 9.3.0 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsOwnersCustomFormsExample
    {
        public void main()
        {
            var apiInstance = new SettingsOwnersApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US (optional) 

            try
            {
                // Get Metadata for Owner Custom Forms
                ResponseApoCustomFormsMetadata result = apiInstance.V4GetSettingsOwnersCustomForms(contentType, authorization, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsOwnersApi.V4GetSettingsOwnersCustomForms: " + e.Message );
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
 **lang** | **string**| Language parameter to support I18N. Default language is en_US | [optional] 

### Return type

[**ResponseApoCustomFormsMetadata**](ResponseApoCustomFormsMetadata.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

