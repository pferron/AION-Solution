# AccelaSettings.Api.SettingsPickListsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetSettingsPickLists**](SettingsPickListsApi.md#v4getsettingspicklists) | **GET** /v4/settings/pickLists | Get All Pick Lists
[**V4GetSettingsPickListsId**](SettingsPickListsApi.md#v4getsettingspicklistsid) | **GET** /v4/settings/pickLists/{id} | Get Pick List


<a name="v4getsettingspicklists"></a>
# **V4GetSettingsPickLists**
> List<ResponseSharedPickListModelArray> V4GetSettingsPickLists (string contentType, string authorization, string lang = null)

Get All Pick Lists

Gets all pick lists in the system. **API Endpoint**:  GET /v4/settings/pickLists  **Scope**:  settings  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsPickListsExample
    {
        public void main()
        {
            var apiInstance = new SettingsPickListsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Pick Lists
                List&lt;ResponseSharedPickListModelArray&gt; result = apiInstance.V4GetSettingsPickLists(contentType, authorization, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsPickListsApi.V4GetSettingsPickLists: " + e.Message );
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

[**List<ResponseSharedPickListModelArray>**](ResponseSharedPickListModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getsettingspicklistsid"></a>
# **V4GetSettingsPickListsId**
> ResponseSharedPickListValuesModel V4GetSettingsPickListsId (string contentType, string authorization, string id, string lang = null)

Get Pick List

Gets the specified pick list, its values, and the fields that are associated with it. **API Endpoint**:  GET /v4/settings/pickLists/{id}  **Scope**:  settings  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsPickListsIdExample
    {
        public void main()
        {
            var apiInstance = new SettingsPickListsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = id_example;  // string | The ID of the pick list to fetch.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Pick List
                ResponseSharedPickListValuesModel result = apiInstance.V4GetSettingsPickListsId(contentType, authorization, id, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsPickListsApi.V4GetSettingsPickListsId: " + e.Message );
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
 **id** | **string**| The ID of the pick list to fetch. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseSharedPickListValuesModel**](ResponseSharedPickListValuesModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

