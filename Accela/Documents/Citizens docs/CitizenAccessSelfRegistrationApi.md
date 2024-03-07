# AccelaCitizens.Api.CitizenAccessSelfRegistrationApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4DeleteCivicidCitizenaccessContactsIds**](CitizenAccessSelfRegistrationApi.md#v4deletecivicidcitizenaccesscontactsids) | **DELETE** /v4/citizenaccess/contacts/{ids} | Delete My Contacts
[**V4GetCivicidCitizenaccessContacts**](CitizenAccessSelfRegistrationApi.md#v4getcivicidcitizenaccesscontacts) | **GET** /v4/citizenaccess/contacts | Get My Contacts
[**V4GetCivicidCitizenaccessProfile**](CitizenAccessSelfRegistrationApi.md#v4getcivicidcitizenaccessprofile) | **GET** /v4/citizenaccess/profile | Get My Citizen Profile
[**V4PostCitizenaccessRegister**](CitizenAccessSelfRegistrationApi.md#v4postcitizenaccessregister) | **POST** /v4/citizenaccess/register | Register Citizen
[**V4PostCivicidCitizenaccessContacts**](CitizenAccessSelfRegistrationApi.md#v4postcivicidcitizenaccesscontacts) | **POST** /v4/citizenaccess/contacts | Create My Contacts


<a name="v4deletecivicidcitizenaccesscontactsids"></a>
# **V4DeleteCivicidCitizenaccessContactsIds**
> ResponseResultCountModel V4DeleteCivicidCitizenaccessContactsIds (string contentType, string authorization, string ids, string lang = null)

Delete My Contacts

Deletes the specified contacts from the currently logged in user.    **API Endpoint**:  DELETE /v4/citizenaccess/contacts/{ids}   **Scope**:  users   **App Type**:  Citizen   **Authorization Type**:  Access token   **Civic Platform version**: 7.3.3.4  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaCitizens.Api;
using AccelaCitizens.Client;
using AccelaCitizens.Model;

namespace Example
{
    public class V4DeleteCivicidCitizenaccessContactsIdsExample
    {
        public void main()
        {
            var apiInstance = new CitizenAccessSelfRegistrationApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var ids = ids_example;  // string | Comma-delimited IDs of contacts to delete.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Delete My Contacts
                ResponseResultCountModel result = apiInstance.V4DeleteCivicidCitizenaccessContactsIds(contentType, authorization, ids, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CitizenAccessSelfRegistrationApi.V4DeleteCivicidCitizenaccessContactsIds: " + e.Message );
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
 **ids** | **string**| Comma-delimited IDs of contacts to delete. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultCountModel**](ResponseResultCountModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getcivicidcitizenaccesscontacts"></a>
# **V4GetCivicidCitizenaccessContacts**
> CitizenContactModel V4GetCivicidCitizenaccessContacts (string contentType, string authorization, string fields = null, string lang = null)

Get My Contacts

Gets the contacts for the currently logged in citizen user.    **API Endpoint**:  GET /v4/citizenaccess/contacts   **Scope**:  users   **App Type**:  Citizen   **Authorization Type**:  Access token   **Civic Platform version**: 7.3.3.4  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaCitizens.Api;
using AccelaCitizens.Client;
using AccelaCitizens.Model;

namespace Example
{
    public class V4GetCivicidCitizenaccessContactsExample
    {
        public void main()
        {
            var apiInstance = new CitizenAccessSelfRegistrationApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get My Contacts
                CitizenContactModel result = apiInstance.V4GetCivicidCitizenaccessContacts(contentType, authorization, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CitizenAccessSelfRegistrationApi.V4GetCivicidCitizenaccessContacts: " + e.Message );
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
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**CitizenContactModel**](CitizenContactModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getcivicidcitizenaccessprofile"></a>
# **V4GetCivicidCitizenaccessProfile**
> CitizenProfileModel V4GetCivicidCitizenaccessProfile (string contentType, string authorization, string fields = null, string lang = null)

Get My Citizen Profile

Gets the profile for the currently logged in citizen user.    **API Endpoint**:  GET /v4/citizenaccess/profile   **Scope**:  users   **App Type**:  Citizen   **Authorization Type**:  Access token   **Civic Platform version**: 7.3.3.4  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaCitizens.Api;
using AccelaCitizens.Client;
using AccelaCitizens.Model;

namespace Example
{
    public class V4GetCivicidCitizenaccessProfileExample
    {
        public void main()
        {
            var apiInstance = new CitizenAccessSelfRegistrationApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get My Citizen Profile
                CitizenProfileModel result = apiInstance.V4GetCivicidCitizenaccessProfile(contentType, authorization, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CitizenAccessSelfRegistrationApi.V4GetCivicidCitizenaccessProfile: " + e.Message );
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
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**CitizenProfileModel**](CitizenProfileModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4postcitizenaccessregister"></a>
# **V4PostCitizenaccessRegister**
> ResponsePublicUserRegisterModel V4PostCitizenaccessRegister (string contentType, string authorization, PublicUserRegisterModel body = null, string lang = null)

Register Citizen

Registers a new citizen user. The agency name is required in the HTTP header x-accela-agency. The userName to be registered is required. Note: 7.3.3.4 version supports only 1 contact in the request contacts[]. Multiple contacts will be supported in a future release.    **API Endpoint**:  POST /v4/citizenaccess/register   **Scope**:  users   **App Type**:  All   **Authorization Type**:  No authorization required   **Civic Platform version**: 7.3.3.4  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaCitizens.Api;
using AccelaCitizens.Client;
using AccelaCitizens.Model;

namespace Example
{
    public class V4PostCitizenaccessRegisterExample
    {
        public void main()
        {
            var apiInstance = new CitizenAccessSelfRegistrationApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var body = new PublicUserRegisterModel(); // PublicUserRegisterModel | The user profile to register. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Register Citizen
                ResponsePublicUserRegisterModel result = apiInstance.V4PostCitizenaccessRegister(contentType, authorization, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CitizenAccessSelfRegistrationApi.V4PostCitizenaccessRegister: " + e.Message );
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
 **body** | [**PublicUserRegisterModel**](PublicUserRegisterModel.md)| The user profile to register. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponsePublicUserRegisterModel**](ResponsePublicUserRegisterModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4postcivicidcitizenaccesscontacts"></a>
# **V4PostCivicidCitizenaccessContacts**
> ResponseResultCountModel V4PostCivicidCitizenaccessContacts (string contentType, string authorization, List<string> body, string lang = null)

Create My Contacts

Adds contacts to the currently logged in citizen user.    **API Endpoint**:  POST /v4/citizenaccess/contacts   **Scope**:  users   **App Type**:  Citizen   **Authorization Type**:  Access token   **Civic Platform version**: 7.3.3.4  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaCitizens.Api;
using AccelaCitizens.Client;
using AccelaCitizens.Model;

namespace Example
{
    public class V4PostCivicidCitizenaccessContactsExample
    {
        public void main()
        {
            var apiInstance = new CitizenAccessSelfRegistrationApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var body = ;  // List<string> | An array of reference contact IDs to add.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Create My Contacts
                ResponseResultCountModel result = apiInstance.V4PostCivicidCitizenaccessContacts(contentType, authorization, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CitizenAccessSelfRegistrationApi.V4PostCivicidCitizenaccessContacts: " + e.Message );
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
 **body** | **List&lt;string&gt;**| An array of reference contact IDs to add. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultCountModel**](ResponseResultCountModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

