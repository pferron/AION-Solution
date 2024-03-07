# AccelaSettings.Api.SettingsConditionsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetSettingsConditionApprovalsStatuses**](SettingsConditionsApi.md#v4getsettingsconditionapprovalsstatuses) | **GET** /v4/settings/conditionApprovals/statuses | Get All Approval Condition Statuses
[**V4GetSettingsConditionsPriorities**](SettingsConditionsApi.md#v4getsettingsconditionspriorities) | **GET** /v4/settings/conditions/priorities | Get All Standard Conditon Priorities
[**V4GetSettingsConditionsStatuses**](SettingsConditionsApi.md#v4getsettingsconditionsstatuses) | **GET** /v4/settings/conditions/statuses | Get All Standard Condition Statuses
[**V4GetSettingsConditionsTypes**](SettingsConditionsApi.md#v4getsettingsconditionstypes) | **GET** /v4/settings/conditions/types | Get All Standard Condition Types


<a name="v4getsettingsconditionapprovalsstatuses"></a>
# **V4GetSettingsConditionApprovalsStatuses**
> ResponseSettingValueModelArray V4GetSettingsConditionApprovalsStatuses (string contentType, string authorization, string lang = null)

Get All Approval Condition Statuses

Gets the statuses for use with a conditon of approval. **API Endpoint**:  GET /v4/settings/conditionApprovals/statuses  **Scope**:  conditions  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsConditionApprovalsStatusesExample
    {
        public void main()
        {
            var apiInstance = new SettingsConditionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Approval Condition Statuses
                ResponseSettingValueModelArray result = apiInstance.V4GetSettingsConditionApprovalsStatuses(contentType, authorization, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsConditionsApi.V4GetSettingsConditionApprovalsStatuses: " + e.Message );
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

<a name="v4getsettingsconditionspriorities"></a>
# **V4GetSettingsConditionsPriorities**
> ResponseSettingValueModelArray V4GetSettingsConditionsPriorities (string contentType, string authorization, string lang = null)

Get All Standard Conditon Priorities

Gets the priorities for use with a standard condition. **API Endpoint**:  GET /v4/settings/conditions/priorities  **Scope**:  conditions  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsConditionsPrioritiesExample
    {
        public void main()
        {
            var apiInstance = new SettingsConditionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Standard Conditon Priorities
                ResponseSettingValueModelArray result = apiInstance.V4GetSettingsConditionsPriorities(contentType, authorization, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsConditionsApi.V4GetSettingsConditionsPriorities: " + e.Message );
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

<a name="v4getsettingsconditionsstatuses"></a>
# **V4GetSettingsConditionsStatuses**
> ResponseConditionStatusModelArray V4GetSettingsConditionsStatuses (string contentType, string authorization, string lang = null)

Get All Standard Condition Statuses

Gets the statuses for use with a standard condition. **API Endpoint**:  GET /v4/settings/conditions/statuses  **Scope**:  conditions  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsConditionsStatusesExample
    {
        public void main()
        {
            var apiInstance = new SettingsConditionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Standard Condition Statuses
                ResponseConditionStatusModelArray result = apiInstance.V4GetSettingsConditionsStatuses(contentType, authorization, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsConditionsApi.V4GetSettingsConditionsStatuses: " + e.Message );
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

[**ResponseConditionStatusModelArray**](ResponseConditionStatusModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getsettingsconditionstypes"></a>
# **V4GetSettingsConditionsTypes**
> ResponseConditionTypeModelArray V4GetSettingsConditionsTypes (string contentType, string authorization, string lang = null)

Get All Standard Condition Types

Gets the standard condition types. **API Endpoint**:  GET /v4/settings/conditions/types  **Scope**:  conditions  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsConditionsTypesExample
    {
        public void main()
        {
            var apiInstance = new SettingsConditionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Standard Condition Types
                ResponseConditionTypeModelArray result = apiInstance.V4GetSettingsConditionsTypes(contentType, authorization, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsConditionsApi.V4GetSettingsConditionsTypes: " + e.Message );
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

[**ResponseConditionTypeModelArray**](ResponseConditionTypeModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

