# AccelaCitizens.Api.CitizenAccessUserManagementApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4DeleteCitizensIdContactsContactIds**](CitizenAccessUserManagementApi.md#v4deletecitizensidcontactscontactids) | **DELETE** /v4/citizenaccess/citizens/{id}/contacts/{contactIds} | Delete Citizen Contacts
[**V4GetCitizenaccessCitizens**](CitizenAccessUserManagementApi.md#v4getcitizenaccesscitizens) | **GET** /v4/citizenaccess/citizens | Get Citizen Users
[**V4GetCitizenaccessCitizensIdAccounts**](CitizenAccessUserManagementApi.md#v4getcitizenaccesscitizensidaccounts) | **GET** /v4/citizenaccess/citizens/{id}/accounts | Get Citizen Accounts
[**V4GetCitizenaccessCitizensIdTrustAccounts**](CitizenAccessUserManagementApi.md#v4getcitizenaccesscitizensidtrustaccounts) | **GET** /v4/citizenaccess/citizens/{id}/trustAccounts | Get Citizen Trust Accounts
[**V4PostCitizenaccessCitizens**](CitizenAccessUserManagementApi.md#v4postcitizenaccesscitizens) | **POST** /v4/citizenaccess/citizens | Create Citizen User
[**V4PostCitizensIdContacts**](CitizenAccessUserManagementApi.md#v4postcitizensidcontacts) | **POST** /v4/citizenaccess/citizens/{id}/contacts | Add Citizen Contacts
[**V4PutCitizenaccessCitizensIdAccounts**](CitizenAccessUserManagementApi.md#v4putcitizenaccesscitizensidaccounts) | **PUT** /v4/citizenaccess/citizens/{id}/accounts | Update Citizen Account Status
[**V4PutCitizensId**](CitizenAccessUserManagementApi.md#v4putcitizensid) | **PUT** /v4/citizenaccess/citizens/{id} | Update Citizen Profile
[**V4PutCitizensIdPassword**](CitizenAccessUserManagementApi.md#v4putcitizensidpassword) | **PUT** /v4/citizenaccess/citizens/{id}/password | Update Citizen Password


<a name="v4deletecitizensidcontactscontactids"></a>
# **V4DeleteCitizensIdContactsContactIds**
> ResponseResultCountModel V4DeleteCitizensIdContactsContactIds (string contentType, string authorization, string id, string contactIds, string lang = null)

Delete Citizen Contacts

Deletes the specified contacts for the specified citizen user.    **API Endpoint**:  DELETE /v4/citizenaccess/citizens/{id}/contacts/{contactIds}   **Scope**:  users   **App Type**:  All   **Authorization Type**:  Access token   **Civic Platform version**: 7.3.3.4  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaCitizens.Api;
using AccelaCitizens.Client;
using AccelaCitizens.Model;

namespace Example
{
    public class V4DeleteCitizensIdContactsContactIdsExample
    {
        public void main()
        {
            var apiInstance = new CitizenAccessUserManagementApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = id_example;  // string | The clerk ID.
            var contactIds = contactIds_example;  // string | Comma-delimited IDs of contacts to delete.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Delete Citizen Contacts
                ResponseResultCountModel result = apiInstance.V4DeleteCitizensIdContactsContactIds(contentType, authorization, id, contactIds, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CitizenAccessUserManagementApi.V4DeleteCitizensIdContactsContactIds: " + e.Message );
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
 **id** | **string**| The clerk ID. | 
 **contactIds** | **string**| Comma-delimited IDs of contacts to delete. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultCountModel**](ResponseResultCountModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getcitizenaccesscitizens"></a>
# **V4GetCitizenaccessCitizens**
> ResponsePublicUserModelArray V4GetCitizenaccessCitizens (string contentType, string authorization, string loginName = null, string expand = null, long? offset = null, long? limit = null, string fields = null, string lang = null)

Get Citizen Users

Returns the users whose profiles can be viewed or edited by the logged-in user. If the logged-in user is an Authorized Agent, the returned users are Authorized Agent Clerks. A Citizen user is not authorized to see other users' profiles, so if the logged-in user is a Citizen user, no users are returned. If the logged-in user is an Automation user, Citizen Access users are returned.    **API Endpoint**:  GET /v4/citizenaccess/citizens   **Scope**:  users   **App Type**:  All   **Authorization Type**:  No authorization required   **Civic Platform version**: 7.3.3.4  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaCitizens.Api;
using AccelaCitizens.Client;
using AccelaCitizens.Model;

namespace Example
{
    public class V4GetCitizenaccessCitizensExample
    {
        public void main()
        {
            var apiInstance = new CitizenAccessUserManagementApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var loginName = loginName_example;  // string | Filter by the citizen's login name. (optional) 
            var expand = expand_example;  // string | Related objects to be returned with the response. The related object(s) will be returned if data exists; if data does not exist, the requested object(s) will not be included in the response. (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Citizen Users
                ResponsePublicUserModelArray result = apiInstance.V4GetCitizenaccessCitizens(contentType, authorization, loginName, expand, offset, limit, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CitizenAccessUserManagementApi.V4GetCitizenaccessCitizens: " + e.Message );
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
 **loginName** | **string**| Filter by the citizen&#39;s login name. | [optional] 
 **expand** | **string**| Related objects to be returned with the response. The related object(s) will be returned if data exists; if data does not exist, the requested object(s) will not be included in the response. | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponsePublicUserModelArray**](ResponsePublicUserModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getcitizenaccesscitizensidaccounts"></a>
# **V4GetCitizenaccessCitizensIdAccounts**
> ResponseUserPINModelArray V4GetCitizenaccessCitizensIdAccounts (string contentType, string authorization, string id, string lang = null)

Get Citizen Accounts

Gets the status of the citizen accounts associated to the specified user.    **API Endpoint**:  GET /v4/citizenaccess/citizens/{id}/accounts   **Scope**:  users   **App Type**:  All   **Authorization Type**:  Access token   **Civic Platform version**: 7.3.3.4  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaCitizens.Api;
using AccelaCitizens.Client;
using AccelaCitizens.Model;

namespace Example
{
    public class V4GetCitizenaccessCitizensIdAccountsExample
    {
        public void main()
        {
            var apiInstance = new CitizenAccessUserManagementApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = id_example;  // string | The ID of citizen user to fetch.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Citizen Accounts
                ResponseUserPINModelArray result = apiInstance.V4GetCitizenaccessCitizensIdAccounts(contentType, authorization, id, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CitizenAccessUserManagementApi.V4GetCitizenaccessCitizensIdAccounts: " + e.Message );
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
 **id** | **string**| The ID of citizen user to fetch. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseUserPINModelArray**](ResponseUserPINModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getcitizenaccesscitizensidtrustaccounts"></a>
# **V4GetCitizenaccessCitizensIdTrustAccounts**
> ResponseTrustAccountModelArray V4GetCitizenaccessCitizensIdTrustAccounts (string contentType, string authorization, long? id, long? offset = null, long? limit = null, string fields = null, string lang = null)

Get Citizen Trust Accounts

Gets the trust accounts for the specified user. If a clerk needs the associated agent's trust account, call the Get Citizen Accounts for the logged in clerk, and use its agentId response field as the {id} parameter for Get Citizen Trust Accounts.    **API Endpoint**:  GET /v4/citizenaccess/citizens/{id}/trustAccounts   **Scope**:  users   **App Type**:  All   **Authorization Type**:  Access token   **Civic Platform version**: 7.3.3.4  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaCitizens.Api;
using AccelaCitizens.Client;
using AccelaCitizens.Model;

namespace Example
{
    public class V4GetCitizenaccessCitizensIdTrustAccountsExample
    {
        public void main()
        {
            var apiInstance = new CitizenAccessUserManagementApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = 789;  // long? | The ID of citizen user
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Citizen Trust Accounts
                ResponseTrustAccountModelArray result = apiInstance.V4GetCitizenaccessCitizensIdTrustAccounts(contentType, authorization, id, offset, limit, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CitizenAccessUserManagementApi.V4GetCitizenaccessCitizensIdTrustAccounts: " + e.Message );
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
 **id** | **long?**| The ID of citizen user | 
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseTrustAccountModelArray**](ResponseTrustAccountModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4postcitizenaccesscitizens"></a>
# **V4PostCitizenaccessCitizens**
> ResponseResultModel V4PostCitizenaccessCitizens (string contentType, string authorization, PublicUserRegisterModel body, string lang = null)

Create Citizen User

Adds a citizen user to be associated with the currently logged-in user. The userName to be added is required.    **API Endpoint**:  POST /v4/citizenaccess/citizens   **Scope**:  users   **App Type**:  All   **Authorization Type**:  Access token   **Civic Platform version**: 7.3.3.4  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaCitizens.Api;
using AccelaCitizens.Client;
using AccelaCitizens.Model;

namespace Example
{
    public class V4PostCitizenaccessCitizensExample
    {
        public void main()
        {
            var apiInstance = new CitizenAccessUserManagementApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var body = new PublicUserRegisterModel(); // PublicUserRegisterModel | The user information to add.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Create Citizen User
                ResponseResultModel result = apiInstance.V4PostCitizenaccessCitizens(contentType, authorization, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CitizenAccessUserManagementApi.V4PostCitizenaccessCitizens: " + e.Message );
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
 **body** | [**PublicUserRegisterModel**](PublicUserRegisterModel.md)| The user information to add. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModel**](ResponseResultModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4postcitizensidcontacts"></a>
# **V4PostCitizensIdContacts**
> ResponseResultCountModel V4PostCitizensIdContacts (string contentType, string authorization, string id, List<string> body, string lang = null)

Add Citizen Contacts

Adds contacts to the specified citizen user. Include the contact IDs to be added in the request array.    **API Endpoint**:  POST /v4/citizenaccess/citizens/{id}/contacts    **Scope**:  users   **App Type**:  All   **Authorization Type**:  Access token   **Civic Platform version**: 7.3.3.4  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaCitizens.Api;
using AccelaCitizens.Client;
using AccelaCitizens.Model;

namespace Example
{
    public class V4PostCitizensIdContactsExample
    {
        public void main()
        {
            var apiInstance = new CitizenAccessUserManagementApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = id_example;  // string | The clerk ID
            var body = ;  // List<string> | An array of reference contact Ids to add.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Add Citizen Contacts
                ResponseResultCountModel result = apiInstance.V4PostCitizensIdContacts(contentType, authorization, id, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CitizenAccessUserManagementApi.V4PostCitizensIdContacts: " + e.Message );
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
 **id** | **string**| The clerk ID | 
 **body** | **List&lt;string&gt;**| An array of reference contact Ids to add. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultCountModel**](ResponseResultCountModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4putcitizenaccesscitizensidaccounts"></a>
# **V4PutCitizenaccessCitizensIdAccounts**
> ResponseResultModelArray V4PutCitizenaccessCitizensIdAccounts (string contentType, string authorization, string id, List<UserPINModel> body, string lang = null)

Update Citizen Account Status

Updates the status of citizen accounts associated to the specified citizen user.    **API Endpoint**:  PUT /v4/citizenaccess/citizens/{id}/accounts   **Scope**:  users   **App Type**:  All   **Authorization Type**:  Access token   **Civic Platform version**: 7.3.3.4  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaCitizens.Api;
using AccelaCitizens.Client;
using AccelaCitizens.Model;

namespace Example
{
    public class V4PutCitizenaccessCitizensIdAccountsExample
    {
        public void main()
        {
            var apiInstance = new CitizenAccessUserManagementApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = id_example;  // string | The ID of citizen user to fetch.
            var body = new List<UserPINModel>(); // List<UserPINModel> | The user information to be updated.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Citizen Account Status
                ResponseResultModelArray result = apiInstance.V4PutCitizenaccessCitizensIdAccounts(contentType, authorization, id, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CitizenAccessUserManagementApi.V4PutCitizenaccessCitizensIdAccounts: " + e.Message );
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
 **id** | **string**| The ID of citizen user to fetch. | 
 **body** | [**List&lt;UserPINModel&gt;**](UserPINModel.md)| The user information to be updated. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4putcitizensid"></a>
# **V4PutCitizensId**
> ResponseCitizenProfileModel V4PutCitizensId (string contentType, string authorization, RequestCitizenProfileModel body, string id, string lang = null)

Update Citizen Profile

Updates the profile of the specified citizen user.    **API Endpoint**:  PUT /v4/citizenaccess/citizens/{id}   **Scope**:  users   **App Type**:  All   **Authorization Type**:  Access token   **Civic Platform version**: 7.3.3.4  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaCitizens.Api;
using AccelaCitizens.Client;
using AccelaCitizens.Model;

namespace Example
{
    public class V4PutCitizensIdExample
    {
        public void main()
        {
            var apiInstance = new CitizenAccessUserManagementApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var body = new RequestCitizenProfileModel(); // RequestCitizenProfileModel | User profile information to be updated
            var id = id_example;  // string | The clerk citizen ID to update.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Citizen Profile
                ResponseCitizenProfileModel result = apiInstance.V4PutCitizensId(contentType, authorization, body, id, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CitizenAccessUserManagementApi.V4PutCitizensId: " + e.Message );
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
 **body** | [**RequestCitizenProfileModel**](RequestCitizenProfileModel.md)| User profile information to be updated | 
 **id** | **string**| The clerk citizen ID to update. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseCitizenProfileModel**](ResponseCitizenProfileModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4putcitizensidpassword"></a>
# **V4PutCitizensIdPassword**
> InlineResponse200 V4PutCitizensIdPassword (string contentType, string authorization, string id, PublicUserPasswordModel body = null, string lang = null)

Update Citizen Password

Updates the password of the specified citizen user {id}.    **API Endpoint**:  PUT /v4/citizenaccess/citizens/{id}/password   **Scope**:  users   **App Type**:  All   **Authorization Type**:  Access token   **Civic Platform version**: 7.3.3.4  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaCitizens.Api;
using AccelaCitizens.Client;
using AccelaCitizens.Model;

namespace Example
{
    public class V4PutCitizensIdPasswordExample
    {
        public void main()
        {
            var apiInstance = new CitizenAccessUserManagementApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = id_example;  // string | The clerk citizen ID.
            var body = new PublicUserPasswordModel(); // PublicUserPasswordModel | The password to update. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Citizen Password
                InlineResponse200 result = apiInstance.V4PutCitizensIdPassword(contentType, authorization, id, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CitizenAccessUserManagementApi.V4PutCitizensIdPassword: " + e.Message );
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
 **id** | **string**| The clerk citizen ID. | 
 **body** | [**PublicUserPasswordModel**](PublicUserPasswordModel.md)| The password to update. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**InlineResponse200**](InlineResponse200.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

