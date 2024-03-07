# AccelaSettings.Api.SettingsCostsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetSettingsCostsAccounts**](SettingsCostsApi.md#v4getsettingscostsaccounts) | **GET** /v4/settings/costs/accounts | Get All Cost Accounts
[**V4GetSettingsCostsFactors**](SettingsCostsApi.md#v4getsettingscostsfactors) | **GET** /v4/settings/costs/factors | Get All Cost Factors
[**V4GetSettingsCostsTypes**](SettingsCostsApi.md#v4getsettingscoststypes) | **GET** /v4/settings/costs/types | Get All Cost Types
[**V4GetSettingsCostsTypesTypeNameItems**](SettingsCostsApi.md#v4getsettingscoststypestypenameitems) | **GET** /v4/settings/costs/types/{typeName}/items | Get All Cost Type Items
[**V4GetSettingsCostsUnitTypes**](SettingsCostsApi.md#v4getsettingscostsunittypes) | **GET** /v4/settings/costs/unitTypes | Get All Cost Unit Types


<a name="v4getsettingscostsaccounts"></a>
# **V4GetSettingsCostsAccounts**
> ResponseSettingValueModelArray V4GetSettingsCostsAccounts (string contentType, string authorization, string lang = null)

Get All Cost Accounts

Gets all accounts associated with costs. **API Endpoint**:  GET /v4/settings/costs/accounts  **Scope**:  settings  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsCostsAccountsExample
    {
        public void main()
        {
            var apiInstance = new SettingsCostsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Cost Accounts
                ResponseSettingValueModelArray result = apiInstance.V4GetSettingsCostsAccounts(contentType, authorization, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsCostsApi.V4GetSettingsCostsAccounts: " + e.Message );
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

<a name="v4getsettingscostsfactors"></a>
# **V4GetSettingsCostsFactors**
> ResponseSettingValueModelArray V4GetSettingsCostsFactors (string contentType, string authorization, string lang = null)

Get All Cost Factors

Gets all factors associated with costs. **API Endpoint**:  GET /v4/settings/costs/factors  **Scope**:  settings  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsCostsFactorsExample
    {
        public void main()
        {
            var apiInstance = new SettingsCostsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Cost Factors
                ResponseSettingValueModelArray result = apiInstance.V4GetSettingsCostsFactors(contentType, authorization, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsCostsApi.V4GetSettingsCostsFactors: " + e.Message );
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

<a name="v4getsettingscoststypes"></a>
# **V4GetSettingsCostsTypes**
> ResponseSettingValueModelArray V4GetSettingsCostsTypes (string contentType, string authorization, string lang = null)

Get All Cost Types

Gets all type values associated with costs. **API Endpoint**:  GET /v4/settings/costs/types  **Scope**:  settings  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsCostsTypesExample
    {
        public void main()
        {
            var apiInstance = new SettingsCostsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Cost Types
                ResponseSettingValueModelArray result = apiInstance.V4GetSettingsCostsTypes(contentType, authorization, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsCostsApi.V4GetSettingsCostsTypes: " + e.Message );
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

<a name="v4getsettingscoststypestypenameitems"></a>
# **V4GetSettingsCostsTypesTypeNameItems**
> ResponseSettingValueModelArray V4GetSettingsCostsTypesTypeNameItems (string contentType, string authorization, string typeName, string lang = null)

Get All Cost Type Items

Gets all items associated to the specified cost type. **API Endpoint**:  GET /v4/settings/costs/types/{typeName}/items  **Scope**:  settings  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsCostsTypesTypeNameItemsExample
    {
        public void main()
        {
            var apiInstance = new SettingsCostsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var typeName = typeName_example;  // string | The ID of cost type to fetch.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Cost Type Items
                ResponseSettingValueModelArray result = apiInstance.V4GetSettingsCostsTypesTypeNameItems(contentType, authorization, typeName, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsCostsApi.V4GetSettingsCostsTypesTypeNameItems: " + e.Message );
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
 **typeName** | **string**| The ID of cost type to fetch. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseSettingValueModelArray**](ResponseSettingValueModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getsettingscostsunittypes"></a>
# **V4GetSettingsCostsUnitTypes**
> ResponseSettingValueModelArray V4GetSettingsCostsUnitTypes (string contentType, string authorization, string lang = null)

Get All Cost Unit Types

Get the unit types for use with a cost. **API Endpoint**:  GET /v4/settings/costs/unitTypes  **Scope**:  settings  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsCostsUnitTypesExample
    {
        public void main()
        {
            var apiInstance = new SettingsCostsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Cost Unit Types
                ResponseSettingValueModelArray result = apiInstance.V4GetSettingsCostsUnitTypes(contentType, authorization, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsCostsApi.V4GetSettingsCostsUnitTypes: " + e.Message );
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

