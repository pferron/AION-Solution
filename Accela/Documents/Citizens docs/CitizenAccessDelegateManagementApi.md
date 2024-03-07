# AccelaCitizens.Api.CitizenAccessDelegateManagementApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4DeleteCitizenaccessCitizensDelegatesIds**](CitizenAccessDelegateManagementApi.md#v4deletecitizenaccesscitizensdelegatesids) | **DELETE** /v4/citizenaccess/citizens/delegates/{ids} | Delete Delegates
[**V4GetCitizenaccessCitizensDelegatePrivileges**](CitizenAccessDelegateManagementApi.md#v4getcitizenaccesscitizensdelegateprivileges) | **GET** /v4/citizenaccess/citizens/delegatePrivileges | Get Citizen Delegate Privileges
[**V4GetCitizenaccessCitizensDelegates**](CitizenAccessDelegateManagementApi.md#v4getcitizenaccesscitizensdelegates) | **GET** /v4/citizenaccess/citizens/delegates | Get All Delegates
[**V4PostCitizenaccessCitizensDelegates**](CitizenAccessDelegateManagementApi.md#v4postcitizenaccesscitizensdelegates) | **POST** /v4/citizenaccess/citizens/delegates | Create Delegates


<a name="v4deletecitizenaccesscitizensdelegatesids"></a>
# **V4DeleteCitizenaccessCitizensDelegatesIds**
> ResponseResultCountModel V4DeleteCitizenaccessCitizensDelegatesIds (string contentType, string authorization, string ids, string lang = null)

Delete Delegates

Deletes the specified delegates whom the logged - in user has delegated.   ** API Endpoint ** : DELETE /v4/citizenaccess/citizens/delegates/{ids}   ** Scope ** : users   ** App Type ** : Citizen   ** Authorization Type ** : Access token   ** Civic Platform version ** : 7.3.3.5 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaCitizens.Api;
using AccelaCitizens.Client;
using AccelaCitizens.Model;

namespace Example
{
    public class V4DeleteCitizenaccessCitizensDelegatesIdsExample
    {
        public void main()
        {
            var apiInstance = new CitizenAccessDelegateManagementApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var ids = ids_example;  // string | Comma - delimited IDs of delegates to delete .
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Delete Delegates
                ResponseResultCountModel result = apiInstance.V4DeleteCitizenaccessCitizensDelegatesIds(contentType, authorization, ids, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CitizenAccessDelegateManagementApi.V4DeleteCitizenaccessCitizensDelegatesIds: " + e.Message );
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
 **ids** | **string**| Comma - delimited IDs of delegates to delete . | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultCountModel**](ResponseResultCountModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getcitizenaccesscitizensdelegateprivileges"></a>
# **V4GetCitizenaccessCitizensDelegatePrivileges**
> ResponseDelegatePrivilegeModelArray V4GetCitizenaccessCitizensDelegatePrivileges (string contentType, string authorization, long? offset = null, long? limit = null, string lang = null)

Get Citizen Delegate Privileges

Gets the privileges of the citizen delegates associated to the logged-in user. **API Endpoint**:  GET /v4/citizenaccess/citizens/delegatePrivileges  **Scope**:  users  **App Type**:  Citizen  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3.5 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaCitizens.Api;
using AccelaCitizens.Client;
using AccelaCitizens.Model;

namespace Example
{
    public class V4GetCitizenaccessCitizensDelegatePrivilegesExample
    {
        public void main()
        {
            var apiInstance = new CitizenAccessDelegateManagementApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Citizen Delegate Privileges
                ResponseDelegatePrivilegeModelArray result = apiInstance.V4GetCitizenaccessCitizensDelegatePrivileges(contentType, authorization, offset, limit, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CitizenAccessDelegateManagementApi.V4GetCitizenaccessCitizensDelegatePrivileges: " + e.Message );
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
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseDelegatePrivilegeModelArray**](ResponseDelegatePrivilegeModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getcitizenaccesscitizensdelegates"></a>
# **V4GetCitizenaccessCitizensDelegates**
> ResponsePublicUserDelegateModelArray V4GetCitizenaccessCitizensDelegates (string contentType, string authorization, string userName = null, string name = null, string delegateStatus = null, long? offset = null, long? limit = null, string lang = null)

Get All Delegates

Gets the citizen users delegated by the logged - in user.  ** API Endpoint ** : GET /v4/citizenaccess/citizens/delegates  ** Scope ** : users   ** App Type ** : Citizen   ** Authorization Type ** : Access token   ** Civic Platform version ** : 7.3.3.5  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaCitizens.Api;
using AccelaCitizens.Client;
using AccelaCitizens.Model;

namespace Example
{
    public class V4GetCitizenaccessCitizensDelegatesExample
    {
        public void main()
        {
            var apiInstance = new CitizenAccessDelegateManagementApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var userName = userName_example;  // string | Filter by the delegate 's login name. (optional) 
            var name = name_example;  // string | Filter by the delegate' s name. (optional) 
            var delegateStatus = delegateStatus_example;  // string | Filter by the delegate status. (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Delegates
                ResponsePublicUserDelegateModelArray result = apiInstance.V4GetCitizenaccessCitizensDelegates(contentType, authorization, userName, name, delegateStatus, offset, limit, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CitizenAccessDelegateManagementApi.V4GetCitizenaccessCitizensDelegates: " + e.Message );
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
 **userName** | **string**| Filter by the delegate &#39;s login name. | [optional] 
 **name** | **string**| Filter by the delegate&#39; s name. | [optional] 
 **delegateStatus** | **string**| Filter by the delegate status. | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponsePublicUserDelegateModelArray**](ResponsePublicUserDelegateModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4postcitizenaccesscitizensdelegates"></a>
# **V4PostCitizenaccessCitizensDelegates**
> ResponseResultModel V4PostCitizenaccessCitizensDelegates (string contentType, string authorization, List<RequestPublicUserDelegateModel> body, string lang = null)

Create Delegates

Creates the specified delegate users.The Create Delegates API allows the logged - in user to send delegate requests to one or more users,as specified in the request array.The specified userNames must be valid citizen users.The logged - in user cannot send more than one delegate request to the same userName.For each new delegate,the delegateStatus is set to PENDING.The citizen user receiving the delegate request will need to accept the delegate request(via Update Invitation API)to enable the assigned permissions.If the citizen user rejects the invitation(via Update Invitation API),the permissions will not take effect; the delegate record remains until it is deleted by the Delete Delegates API.   ** API Endpoint ** : POST /v4/citizenaccess/citizens/delegates   ** Scope ** : users   ** App Type ** : Citizen   ** Authorization Type ** : Access token   ** Civic Platform version ** : 7.3.3.5  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaCitizens.Api;
using AccelaCitizens.Client;
using AccelaCitizens.Model;

namespace Example
{
    public class V4PostCitizenaccessCitizensDelegatesExample
    {
        public void main()
        {
            var apiInstance = new CitizenAccessDelegateManagementApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var body = new List<RequestPublicUserDelegateModel>(); // List<RequestPublicUserDelegateModel> | Delegate information to add.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Create Delegates
                ResponseResultModel result = apiInstance.V4PostCitizenaccessCitizensDelegates(contentType, authorization, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CitizenAccessDelegateManagementApi.V4PostCitizenaccessCitizensDelegates: " + e.Message );
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
 **body** | [**List&lt;RequestPublicUserDelegateModel&gt;**](RequestPublicUserDelegateModel.md)| Delegate information to add. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModel**](ResponseResultModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

