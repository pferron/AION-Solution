# AccelaSettings.Api.SettingsProfesssionalsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetSettingsProfessionalsLicenseBoards**](SettingsProfesssionalsApi.md#v4getsettingsprofessionalslicenseboards) | **GET** /v4/settings/professionals/licenseBoards | Get All Professional License Boards
[**V4GetSettingsProfessionalsSalutations**](SettingsProfesssionalsApi.md#v4getsettingsprofessionalssalutations) | **GET** /v4/settings/professionals/salutations | Get All Professional Salutations
[**V4GetSettingsProfessionalsTypes**](SettingsProfesssionalsApi.md#v4getsettingsprofessionalstypes) | **GET** /v4/settings/professionals/types | Get All Professional License Types


<a name="v4getsettingsprofessionalslicenseboards"></a>
# **V4GetSettingsProfessionalsLicenseBoards**
> ResponseSettingValueModelArray V4GetSettingsProfessionalsLicenseBoards (string contentType, string authorization, string lang = null)

Get All Professional License Boards

Gets the license boards for use with professionals. **API Endpoint**:  GET /v4/settings/professionals/licenseBoards  **Scope**:  professionals  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsProfessionalsLicenseBoardsExample
    {
        public void main()
        {
            var apiInstance = new SettingsProfesssionalsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Professional License Boards
                ResponseSettingValueModelArray result = apiInstance.V4GetSettingsProfessionalsLicenseBoards(contentType, authorization, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsProfesssionalsApi.V4GetSettingsProfessionalsLicenseBoards: " + e.Message );
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

<a name="v4getsettingsprofessionalssalutations"></a>
# **V4GetSettingsProfessionalsSalutations**
> ResponseSettingValueModelArray V4GetSettingsProfessionalsSalutations (string contentType, string authorization, string lang = null)

Get All Professional Salutations

Gets the salutations for use with professionals. **API Endpoint**:  GET /v4/settings/professionals/salutations  **Scope**:  professionals  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsProfessionalsSalutationsExample
    {
        public void main()
        {
            var apiInstance = new SettingsProfesssionalsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Professional Salutations
                ResponseSettingValueModelArray result = apiInstance.V4GetSettingsProfessionalsSalutations(contentType, authorization, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsProfesssionalsApi.V4GetSettingsProfessionalsSalutations: " + e.Message );
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

<a name="v4getsettingsprofessionalstypes"></a>
# **V4GetSettingsProfessionalsTypes**
> ResponseSettingValueModelArray V4GetSettingsProfessionalsTypes (string contentType, string authorization, string lang = null)

Get All Professional License Types

Gets the license types for use with professionals. **API Endpoint**:  GET /v4/settings/professionals/types  **Scope**:  professionals  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsProfessionalsTypesExample
    {
        public void main()
        {
            var apiInstance = new SettingsProfesssionalsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Professional License Types
                ResponseSettingValueModelArray result = apiInstance.V4GetSettingsProfessionalsTypes(contentType, authorization, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsProfesssionalsApi.V4GetSettingsProfessionalsTypes: " + e.Message );
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

