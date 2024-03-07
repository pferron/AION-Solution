# AccelaSettings.Api.SettingsParcelsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetSettingsParcelsCustomForms**](SettingsParcelsApi.md#v4getsettingsparcelscustomforms) | **GET** /v4/settings/parcels/customForms | Get Metadata for Parcel Custom Forms


<a name="v4getsettingsparcelscustomforms"></a>
# **V4GetSettingsParcelsCustomForms**
> ResponseApoCustomFormsMetadata V4GetSettingsParcelsCustomForms (string contentType, string authorization, string lang = null)

Get Metadata for Parcel Custom Forms

Returns the field metadata for all parcel custom forms (parcel templates) in the system. **API Endpoint**:  GET /v4/settings/parcels/customForms  **Scope**:  parcels  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 9.3.0  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsParcelsCustomFormsExample
    {
        public void main()
        {
            var apiInstance = new SettingsParcelsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US (optional) 

            try
            {
                // Get Metadata for Parcel Custom Forms
                ResponseApoCustomFormsMetadata result = apiInstance.V4GetSettingsParcelsCustomForms(contentType, authorization, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsParcelsApi.V4GetSettingsParcelsCustomForms: " + e.Message );
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

