# AccelaMiscellanous.Api.UsersApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetUsersMe**](UsersApi.md#v4getusersme) | **GET** /v4/users/me | Get My User Account
[**V4GetUsersUserIdGroups**](UsersApi.md#v4getusersuseridgroups) | **GET** /v4/users/{user_id}/groups | Get All User Groups


<a name="v4getusersme"></a>
# **V4GetUsersMe**
> ResponseUserModel V4GetUsersMe (string contentType, string authorization, string fields = null, string lang = null)

Get My User Account

Returns the account information for the currently logged in agency user. **API Endpoint**:  GET /v4/users/me  **Scope**:  users  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 9.0.0 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaMiscellanous.Api;
using AccelaMiscellanous.Client;
using AccelaMiscellanous.Model;

namespace Example
{
    public class V4GetUsersMeExample
    {
        public void main()
        {
            var apiInstance = new UsersApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note: Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get My User Account
                ResponseUserModel result = apiInstance.V4GetUsersMe(contentType, authorization, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling UsersApi.V4GetUsersMe: " + e.Message );
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
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note: Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseUserModel**](ResponseUserModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getusersuseridgroups"></a>
# **V4GetUsersUserIdGroups**
> ResponseUserGroupModel V4GetUsersUserIdGroups (string contentType, string userId, string authorization, string fields = null, string lang = null)

Get All User Groups

Gets the user groups for a given user. **API Endpoint**:  GET /v4/users/{user_id}/groups  **Scope**:  users  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**:  9.0.0 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaMiscellanous.Api;
using AccelaMiscellanous.Client;
using AccelaMiscellanous.Model;

namespace Example
{
    public class V4GetUsersUserIdGroupsExample
    {
        public void main()
        {
            var apiInstance = new UsersApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var userId = userId_example;  // string | The ID of the user to be fetched.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note: Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All User Groups
                ResponseUserGroupModel result = apiInstance.V4GetUsersUserIdGroups(contentType, userId, authorization, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling UsersApi.V4GetUsersUserIdGroups: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | [default to application/x-www-form-urlencoded]
 **userId** | **string**| The ID of the user to be fetched. | 
 **authorization** | **string**| Construct oAuth2 authentication token | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note: Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseUserGroupModel**](ResponseUserGroupModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

