# AccelaAddressParcelsOwners.Api.OwnersApi

All URIs are relative to *http://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetOwners**](OwnersApi.md#v4getowners) | **GET** /v4/owners | Get All Owners
[**V4GetOwnersId**](OwnersApi.md#v4getownersid) | **GET** /v4/owners/{id} | Get Owner
[**V4GetOwnersOwnerIdConditions**](OwnersApi.md#v4getownersowneridconditions) | **GET** /v4/owners/{ownerId}/conditions | Get All Owner Conditions


<a name="v4getowners"></a>
# **V4GetOwners**
> ResponseOwnerModelWithCustomFormsArray V4GetOwners (string contentType, string authorization, string xAccelaAppid, string fullName = null, string firstName = null, string lastName = null, string parcelId = null, string email = null, string city = null, string state = null, string country = null, string expand = null, long? offset = null, long? limit = null, string fields = null, string lang = null)

Get All Owners

Gets a list of reference owners in the agency database. Specify at least one filter criteria.    **API Endpoint**:  GET /v4/owners   **Scope**:  owners   **App Type**:  All   **Authorization Type**:  Access token   **Civic Platform version**: 7.3.2  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAddressParcelsOwners.Api;
using AccelaAddressParcelsOwners.Client;
using AccelaAddressParcelsOwners.Model;

namespace Example
{
    public class V4GetOwnersExample
    {
        public void main()
        {
            var apiInstance = new OwnersApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var xAccelaAppid = xAccelaAppid_example;  // string | clientid.
            var fullName = fullName_example;  // string | Filter by owner's full name. (optional) 
            var firstName = firstName_example;  // string | Filter by owner's first name. (optional) 
            var lastName = lastName_example;  // string | Filter by owner's last name. (optional) 
            var parcelId = parcelId_example;  // string | Filter by owner's parcel id. (optional) 
            var email = email_example;  // string | Filter by owner's email. (optional) 
            var city = city_example;  // string | Filter by owner's city. (optional) 
            var state = state_example;  // string | Filter by owner's state. (optional) 
            var country = country_example;  // string | Filter by owner's country. (optional) 
            var expand = expand_example;  // string | Related objects to be returned with the response. The related object(s) will be returned if data exists; if data does not exist, the requested object(s) will not be included in the response.   Added in Civic Platform version: 9.3.0 (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array.For example,  if offset is 100,the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var fields = fields_example;  // string | Comma - delimited names of fields to be returned in the response.Note - Field names are case -sensitive and only first - level fields are supported.Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N.Default language is en_US. (optional) 

            try
            {
                // Get All Owners
                ResponseOwnerModelWithCustomFormsArray result = apiInstance.V4GetOwners(contentType, authorization, xAccelaAppid, fullName, firstName, lastName, parcelId, email, city, state, country, expand, offset, limit, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling OwnersApi.V4GetOwners: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | 
 **authorization** | **string**| Construct oAuth2 authentication token | 
 **xAccelaAppid** | **string**| clientid. | 
 **fullName** | **string**| Filter by owner&#39;s full name. | [optional] 
 **firstName** | **string**| Filter by owner&#39;s first name. | [optional] 
 **lastName** | **string**| Filter by owner&#39;s last name. | [optional] 
 **parcelId** | **string**| Filter by owner&#39;s parcel id. | [optional] 
 **email** | **string**| Filter by owner&#39;s email. | [optional] 
 **city** | **string**| Filter by owner&#39;s city. | [optional] 
 **state** | **string**| Filter by owner&#39;s state. | [optional] 
 **country** | **string**| Filter by owner&#39;s country. | [optional] 
 **expand** | **string**| Related objects to be returned with the response. The related object(s) will be returned if data exists; if data does not exist, the requested object(s) will not be included in the response.   Added in Civic Platform version: 9.3.0 | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array.For example,  if offset is 100,the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 
 **fields** | **string**| Comma - delimited names of fields to be returned in the response.Note - Field names are case -sensitive and only first - level fields are supported.Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N.Default language is en_US. | [optional] 

### Return type

[**ResponseOwnerModelWithCustomFormsArray**](ResponseOwnerModelWithCustomFormsArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getownersid"></a>
# **V4GetOwnersId**
> ResponseOwnerModelWithCustomForms V4GetOwnersId (string contentType, string authorization, string xAccelaAppid, string id, string expand = null, string fields = null, string lang = null)

Get Owner

Gets information about a reference owner.    **API Endpoint**:  GET /v4/owners/{id}   **Scope**:  owners   **App Type**:  All   **Authorization Type**:  Access token   **Civic Platform version**: 7.3.2  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAddressParcelsOwners.Api;
using AccelaAddressParcelsOwners.Client;
using AccelaAddressParcelsOwners.Model;

namespace Example
{
    public class V4GetOwnersIdExample
    {
        public void main()
        {
            var apiInstance = new OwnersApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var xAccelaAppid = xAccelaAppid_example;  // string | clientid.
            var id = id_example;  // string | The system id of the owner to fetch.
            var expand = expand_example;  // string | Related objects to be returned with the response. The related object(s) will be returned if data exists; if data does not exist, the requested object(s) will not be included in the response.   Added in Civic Platform version: 9.3.0 (API-4591) (optional) 
            var fields = fields_example;  // string | Comma - delimited names of fields to be returned in the response.Note - Field names are case -sensitive and only first - level fields are supported.Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N.Default language is en_US. (optional) 

            try
            {
                // Get Owner
                ResponseOwnerModelWithCustomForms result = apiInstance.V4GetOwnersId(contentType, authorization, xAccelaAppid, id, expand, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling OwnersApi.V4GetOwnersId: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | 
 **authorization** | **string**| Construct oAuth2 authentication token | 
 **xAccelaAppid** | **string**| clientid. | 
 **id** | **string**| The system id of the owner to fetch. | 
 **expand** | **string**| Related objects to be returned with the response. The related object(s) will be returned if data exists; if data does not exist, the requested object(s) will not be included in the response.   Added in Civic Platform version: 9.3.0 (API-4591) | [optional] 
 **fields** | **string**| Comma - delimited names of fields to be returned in the response.Note - Field names are case -sensitive and only first - level fields are supported.Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N.Default language is en_US. | [optional] 

### Return type

[**ResponseOwnerModelWithCustomForms**](ResponseOwnerModelWithCustomForms.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getownersowneridconditions"></a>
# **V4GetOwnersOwnerIdConditions**
> ResponseConditionModelArray V4GetOwnersOwnerIdConditions (string contentType, string authorization, string xAccelaAppid, string ownerId, string type = null, string name = null, string status = null, long? offset = null, long? limit = null, string fields = null, string lang = null)

Get All Owner Conditions

Gets the conditions on a reference owner.    **API Endpoint**:  GET /v4/owners/{ownerId}/conditions   **Scope**:  owners   **App Type**:  All   **Authorization Type**:  Access token   **Civic Platform version**: 8.0.0  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAddressParcelsOwners.Api;
using AccelaAddressParcelsOwners.Client;
using AccelaAddressParcelsOwners.Model;

namespace Example
{
    public class V4GetOwnersOwnerIdConditionsExample
    {
        public void main()
        {
            var apiInstance = new OwnersApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var xAccelaAppid = xAccelaAppid_example;  // string | clientid
            var ownerId = ownerId_example;  // string | The system id of the owner to fetch.
            var type = type_example;  // string | Filter by condition type.See[Get All Condition Types](. / api - settings.html # operation / v4.get.settings.conditions.types) (optional) 
            var name = name_example;  // string | Filter by condition name. (optional) 
            var status = status_example;  // string | Filter by condition status.See[Get All Standard Condition Statuses](. / api - settings.html # operation / v4.get.settings.conditions.statuses), [Get All Approval Condition Statuses](. / api - settings.html # operation / v4.get.settings.conditionApprovals.statuses) (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array.For example,  if offset is 100,the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var fields = fields_example;  // string | Comma - delimited names of fields to be returned in the response.Note - Field names are case -sensitive and only first - level fields are supported.Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N.Default language is en_US. (optional) 

            try
            {
                // Get All Owner Conditions
                ResponseConditionModelArray result = apiInstance.V4GetOwnersOwnerIdConditions(contentType, authorization, xAccelaAppid, ownerId, type, name, status, offset, limit, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling OwnersApi.V4GetOwnersOwnerIdConditions: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | 
 **authorization** | **string**| Construct oAuth2 authentication token | 
 **xAccelaAppid** | **string**| clientid | 
 **ownerId** | **string**| The system id of the owner to fetch. | 
 **type** | **string**| Filter by condition type.See[Get All Condition Types](. / api - settings.html # operation / v4.get.settings.conditions.types) | [optional] 
 **name** | **string**| Filter by condition name. | [optional] 
 **status** | **string**| Filter by condition status.See[Get All Standard Condition Statuses](. / api - settings.html # operation / v4.get.settings.conditions.statuses), [Get All Approval Condition Statuses](. / api - settings.html # operation / v4.get.settings.conditionApprovals.statuses) | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array.For example,  if offset is 100,the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 
 **fields** | **string**| Comma - delimited names of fields to be returned in the response.Note - Field names are case -sensitive and only first - level fields are supported.Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N.Default language is en_US. | [optional] 

### Return type

[**ResponseConditionModelArray**](ResponseConditionModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

