# AccelaSettings.Api.SettingsAddressesApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetSettingsAddressesCountries**](SettingsAddressesApi.md#v4getsettingsaddressescountries) | **GET** /v4/settings/addresses/countries | Get All Address Countries
[**V4GetSettingsAddressesCustomForms**](SettingsAddressesApi.md#v4getsettingsaddressescustomforms) | **GET** /v4/settings/addresses/customForms | Get Metadata for Address Custom Forms
[**V4GetSettingsAddressesStates**](SettingsAddressesApi.md#v4getsettingsaddressesstates) | **GET** /v4/settings/addresses/states | Get All Address States
[**V4GetSettingsAddressesStreetDirections**](SettingsAddressesApi.md#v4getsettingsaddressesstreetdirections) | **GET** /v4/settings/addresses/streetDirections | Get All Address Street Directions
[**V4GetSettingsAddressesStreetFractions**](SettingsAddressesApi.md#v4getsettingsaddressesstreetfractions) | **GET** /v4/settings/addresses/streetFractions | Get All Address Street Fractions
[**V4GetSettingsAddressesStreetSuffixes**](SettingsAddressesApi.md#v4getsettingsaddressesstreetsuffixes) | **GET** /v4/settings/addresses/streetSuffixes | Get All Address Street Suffixes
[**V4GetSettingsAddressesUnitTypes**](SettingsAddressesApi.md#v4getsettingsaddressesunittypes) | **GET** /v4/settings/addresses/unitTypes | Get All Address Unit Types


<a name="v4getsettingsaddressescountries"></a>
# **V4GetSettingsAddressesCountries**
> ResponseSettingValueModelArray V4GetSettingsAddressesCountries (string contentType, string authorization, string lang = null)

Get All Address Countries

Gets the countries for use in an address. **API Endpoint**:  GET /v4/settings/addresses/countries  **Scope**:  addresses  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsAddressesCountriesExample
    {
        public void main()
        {
            var apiInstance = new SettingsAddressesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Address Countries
                ResponseSettingValueModelArray result = apiInstance.V4GetSettingsAddressesCountries(contentType, authorization, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsAddressesApi.V4GetSettingsAddressesCountries: " + e.Message );
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

<a name="v4getsettingsaddressescustomforms"></a>
# **V4GetSettingsAddressesCustomForms**
> ResponseApoCustomFormsMetadata V4GetSettingsAddressesCustomForms (string contentType, string authorization, string lang = null)

Get Metadata for Address Custom Forms

Returns the field metadata for all address custom forms (address templates) in the system. **API Endpoint**:  GET /v4/settings/addresses/customForms  **Scope**:  addresses  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 9.3.0 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsAddressesCustomFormsExample
    {
        public void main()
        {
            var apiInstance = new SettingsAddressesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US (optional) 

            try
            {
                // Get Metadata for Address Custom Forms
                ResponseApoCustomFormsMetadata result = apiInstance.V4GetSettingsAddressesCustomForms(contentType, authorization, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsAddressesApi.V4GetSettingsAddressesCustomForms: " + e.Message );
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

<a name="v4getsettingsaddressesstates"></a>
# **V4GetSettingsAddressesStates**
> ResponseSettingValueModelArray V4GetSettingsAddressesStates (string contentType, string authorization, string lang = null)

Get All Address States

Gets the states for use in an address. **API Endpoint**:  /v4/settings/addresses/states GET  **Scope**:  addresses  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsAddressesStatesExample
    {
        public void main()
        {
            var apiInstance = new SettingsAddressesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Address States
                ResponseSettingValueModelArray result = apiInstance.V4GetSettingsAddressesStates(contentType, authorization, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsAddressesApi.V4GetSettingsAddressesStates: " + e.Message );
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

<a name="v4getsettingsaddressesstreetdirections"></a>
# **V4GetSettingsAddressesStreetDirections**
> ResponseSettingValueModelArray V4GetSettingsAddressesStreetDirections (string contentType, string authorization, string lang = null)

Get All Address Street Directions

Gets the street directions for use in an address. **API Endpoint**:  GET /v4/settings/addresses/streetDirections  **Scope**:  addresses  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsAddressesStreetDirectionsExample
    {
        public void main()
        {
            var apiInstance = new SettingsAddressesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Address Street Directions
                ResponseSettingValueModelArray result = apiInstance.V4GetSettingsAddressesStreetDirections(contentType, authorization, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsAddressesApi.V4GetSettingsAddressesStreetDirections: " + e.Message );
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

<a name="v4getsettingsaddressesstreetfractions"></a>
# **V4GetSettingsAddressesStreetFractions**
> ResponseSettingValueModelArray V4GetSettingsAddressesStreetFractions (string contentType, string authorization, string lang = null)

Get All Address Street Fractions

Gets the street fractions for use in an address. **API Endpoint**:  GET /v4/settings/addresses/streetFractions  **Scope**:  addresses  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsAddressesStreetFractionsExample
    {
        public void main()
        {
            var apiInstance = new SettingsAddressesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Address Street Fractions
                ResponseSettingValueModelArray result = apiInstance.V4GetSettingsAddressesStreetFractions(contentType, authorization, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsAddressesApi.V4GetSettingsAddressesStreetFractions: " + e.Message );
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

<a name="v4getsettingsaddressesstreetsuffixes"></a>
# **V4GetSettingsAddressesStreetSuffixes**
> ResponseSettingValueModelArray V4GetSettingsAddressesStreetSuffixes (string contentType, string authorization, string lang = null)

Get All Address Street Suffixes

Gets the street types for use in an address. **API Endpoint**:  GET /v4/settings/addresses/streetSuffixes  **Scope**:  addresses  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsAddressesStreetSuffixesExample
    {
        public void main()
        {
            var apiInstance = new SettingsAddressesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Address Street Suffixes
                ResponseSettingValueModelArray result = apiInstance.V4GetSettingsAddressesStreetSuffixes(contentType, authorization, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsAddressesApi.V4GetSettingsAddressesStreetSuffixes: " + e.Message );
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

<a name="v4getsettingsaddressesunittypes"></a>
# **V4GetSettingsAddressesUnitTypes**
> ResponseSettingValueModelArray V4GetSettingsAddressesUnitTypes (string contentType, string authorization, string lang = null)

Get All Address Unit Types

Gets the unit types for use in an address. **API Endpoint**:  GET /v4/settings/addresses/unitTypes  **Scope**:  addresses  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsAddressesUnitTypesExample
    {
        public void main()
        {
            var apiInstance = new SettingsAddressesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Address Unit Types
                ResponseSettingValueModelArray result = apiInstance.V4GetSettingsAddressesUnitTypes(contentType, authorization, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsAddressesApi.V4GetSettingsAddressesUnitTypes: " + e.Message );
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

