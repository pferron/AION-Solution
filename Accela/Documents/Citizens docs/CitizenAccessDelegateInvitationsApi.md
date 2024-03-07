# AccelaCitizens.Api.CitizenAccessDelegateInvitationsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetCitizenaccessCitizensInvitations**](CitizenAccessDelegateInvitationsApi.md#v4getcitizenaccesscitizensinvitations) | **GET** /v4/citizenaccess/citizens/invitations | Get All Invitations
[**V4PutCitizenaccessCitizensInvitationId**](CitizenAccessDelegateInvitationsApi.md#v4putcitizenaccesscitizensinvitationid) | **PUT** /v4/citizenaccess/citizens/invitation/{id} | Update Invitation


<a name="v4getcitizenaccesscitizensinvitations"></a>
# **V4GetCitizenaccessCitizensInvitations**
> ResponsePublicUserDelegateModelArray V4GetCitizenaccessCitizensInvitations (string contentType, string authorization, string userName = null, string name = null, string delegateStatus = null, long? offset = null, long? limit = null, string lang = null)

Get All Invitations

Gets invitations or delegate requests received by the logged - in user.To get the logged - in user 's pending invitations, set the {delegateStatus} parameter to PENDING.    **API Endpoint**:  GET /v4/citizenaccess/citizens/invitations   **Scope**:  users   **App Type**:  Citizen   **Authorization Type**:  Access token   **Civic Platform version**: 7.3.3.5  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaCitizens.Api;
using AccelaCitizens.Client;
using AccelaCitizens.Model;

namespace Example
{
    public class V4GetCitizenaccessCitizensInvitationsExample
    {
        public void main()
        {
            var apiInstance = new CitizenAccessDelegateInvitationsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var userName = userName_example;  // string | Filter by the delegate' s login name. (optional) 
            var name = name_example;  // string | Filter by the delegate's name. (optional) 
            var delegateStatus = delegateStatus_example;  // string | Filter by the delegate status. (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Invitations
                ResponsePublicUserDelegateModelArray result = apiInstance.V4GetCitizenaccessCitizensInvitations(contentType, authorization, userName, name, delegateStatus, offset, limit, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CitizenAccessDelegateInvitationsApi.V4GetCitizenaccessCitizensInvitations: " + e.Message );
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
 **userName** | **string**| Filter by the delegate&#39; s login name. | [optional] 
 **name** | **string**| Filter by the delegate&#39;s name. | [optional] 
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

<a name="v4putcitizenaccesscitizensinvitationid"></a>
# **V4PutCitizenaccessCitizensInvitationId**
> ResponsePublicUserDelegateStatusModel V4PutCitizenaccessCitizensInvitationId (string contentType, string authorization, PublicUserDelegateStatusModel body, string id, string lang = null)

Update Invitation

Updates the status of the delegate invitation sent by a specified user. The Update Invitation API allows the logged-in user to accept or reject the delegate invitation sent by the user {id}.    **API Endpoint**:  PUT /v4/citizenaccess/citizens/invitation/{id}    **Scope**:  users   **App Type**:  Citizen   **Authorization Type**:  Access token   **Civic Platform version**: 7.3.3.5  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaCitizens.Api;
using AccelaCitizens.Client;
using AccelaCitizens.Model;

namespace Example
{
    public class V4PutCitizenaccessCitizensInvitationIdExample
    {
        public void main()
        {
            var apiInstance = new CitizenAccessDelegateInvitationsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var body = new PublicUserDelegateStatusModel(); // PublicUserDelegateStatusModel | Delegate information including the delegate status to be updated.
            var id = id_example;  // string | The id of the citizen user who sent the delegate request.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Invitation
                ResponsePublicUserDelegateStatusModel result = apiInstance.V4PutCitizenaccessCitizensInvitationId(contentType, authorization, body, id, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CitizenAccessDelegateInvitationsApi.V4PutCitizenaccessCitizensInvitationId: " + e.Message );
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
 **body** | [**PublicUserDelegateStatusModel**](PublicUserDelegateStatusModel.md)| Delegate information including the delegate status to be updated. | 
 **id** | **string**| The id of the citizen user who sent the delegate request. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponsePublicUserDelegateStatusModel**](ResponsePublicUserDelegateStatusModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

