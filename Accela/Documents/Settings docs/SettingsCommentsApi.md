# AccelaSettings.Api.SettingsCommentsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetSettingsComments**](SettingsCommentsApi.md#v4getsettingscomments) | **GET** /v4/settings/comments | Get All Standard Comments
[**V4GetSettingsCommentsGroups**](SettingsCommentsApi.md#v4getsettingscommentsgroups) | **GET** /v4/settings/comments/groups | Get All Standard Comment Groups


<a name="v4getsettingscomments"></a>
# **V4GetSettingsComments**
> ResponseStandardCommentModelArray V4GetSettingsComments (string contentType, string authorization, string groups = null, string types = null, string lang = null, long? offset = null, long? limit = null)

Get All Standard Comments

Gets the standard comments available in the system. **API Endpoint**:  GET v4/settings/comments  **Scope**:  settings  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsCommentsExample
    {
        public void main()
        {
            var apiInstance = new SettingsCommentsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var groups = groups_example;  // string | Filter by group. (optional) 
            var types = types_example;  // string | Filter by one or more (comma-delimited) standard comment types. For valid values, use types[] in [Get All Standard Comment Groups](./api-settings.html#operation/v4.get.settings.comments.groups). **Civic Platform version**: 9.3.0  (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 

            try
            {
                // Get All Standard Comments
                ResponseStandardCommentModelArray result = apiInstance.V4GetSettingsComments(contentType, authorization, groups, types, lang, offset, limit);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsCommentsApi.V4GetSettingsComments: " + e.Message );
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
 **groups** | **string**| Filter by group. | [optional] 
 **types** | **string**| Filter by one or more (comma-delimited) standard comment types. For valid values, use types[] in [Get All Standard Comment Groups](./api-settings.html#operation/v4.get.settings.comments.groups). **Civic Platform version**: 9.3.0  | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 

### Return type

[**ResponseStandardCommentModelArray**](ResponseStandardCommentModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getsettingscommentsgroups"></a>
# **V4GetSettingsCommentsGroups**
> ResponseStandardCommentGroupModelArray V4GetSettingsCommentsGroups (string contentType, string authorization, string lang = null, long? offset = null, long? limit = null)

Get All Standard Comment Groups

Gets available standard comment groups. **API Endpoint**:  GET /v4/settings/comments/groups  **Scope**:  settings  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsCommentsGroupsExample
    {
        public void main()
        {
            var apiInstance = new SettingsCommentsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 

            try
            {
                // Get All Standard Comment Groups
                ResponseStandardCommentGroupModelArray result = apiInstance.V4GetSettingsCommentsGroups(contentType, authorization, lang, offset, limit);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsCommentsApi.V4GetSettingsCommentsGroups: " + e.Message );
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
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 

### Return type

[**ResponseStandardCommentGroupModelArray**](ResponseStandardCommentGroupModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

