# AccelaContactsAndProfessionals.Api.ContactsAddressesApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetContactsIdAddresses**](ContactsAddressesApi.md#v4getcontactsidaddresses) | **GET** /v4/contacts/{id}/addresses | Get All Contact Addresses
[**V4PostContactsIdAddresses**](ContactsAddressesApi.md#v4postcontactsidaddresses) | **POST** /v4/contacts/{id}/addresses | Create Contact Addresses
[**V4PutContactsContactIdAddressesId**](ContactsAddressesApi.md#v4putcontactscontactidaddressesid) | **PUT** /v4/contacts/{contactId}/addresses/{id} | Update Contact Address


<a name="v4getcontactsidaddresses"></a>
# **V4GetContactsIdAddresses**
> ResponseContactAddressModelArray V4GetContactsIdAddresses (string contentType, string authorization, string id, string fields = null, long? offset = null, long? limit = null, string lang = null)

Get All Contact Addresses

Gets detailed address information for the specified contact.. **API Endpoint**:  GET /v4/contacts/{id}/addresses  **Scope**:  contacts  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaContactsAndProfessionals.Api;
using AccelaContactsAndProfessionals.Client;
using AccelaContactsAndProfessionals.Model;

namespace Example
{
    public class V4GetContactsIdAddressesExample
    {
        public void main()
        {
            var apiInstance = new ContactsAddressesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = id_example;  // string | The ID of the contact to fetch.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Contact Addresses
                ResponseContactAddressModelArray result = apiInstance.V4GetContactsIdAddresses(contentType, authorization, id, fields, offset, limit, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ContactsAddressesApi.V4GetContactsIdAddresses: " + e.Message );
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
 **id** | **string**| The ID of the contact to fetch. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseContactAddressModelArray**](ResponseContactAddressModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4postcontactsidaddresses"></a>
# **V4PostContactsIdAddresses**
> ResponseResultModelArray V4PostContactsIdAddresses (string contentType, string authorization, string id, List<ContactAddressModel> body, string lang = null)

Create Contact Addresses

Creates detailed address information for the specified contact. **API Endpoint**:  POST /v4/contacts/{id}/addresses  **Scope**:  contacts  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaContactsAndProfessionals.Api;
using AccelaContactsAndProfessionals.Client;
using AccelaContactsAndProfessionals.Model;

namespace Example
{
    public class V4PostContactsIdAddressesExample
    {
        public void main()
        {
            var apiInstance = new ContactsAddressesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = id_example;  // string | The ID of the contact to fetch.
            var body = new List<ContactAddressModel>(); // List<ContactAddressModel> | Contact Address information. Refer to ContactAddressModel json schema
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Create Contact Addresses
                ResponseResultModelArray result = apiInstance.V4PostContactsIdAddresses(contentType, authorization, id, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ContactsAddressesApi.V4PostContactsIdAddresses: " + e.Message );
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
 **id** | **string**| The ID of the contact to fetch. | 
 **body** | [**List&lt;ContactAddressModel&gt;**](ContactAddressModel.md)| Contact Address information. Refer to ContactAddressModel json schema | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4putcontactscontactidaddressesid"></a>
# **V4PutContactsContactIdAddressesId**
> ContactAddressModel V4PutContactsContactIdAddressesId (string contentType, string authorization, long? contactId, ContactAddressModel body, long? id, string lang = null)

Update Contact Address

Updates the address information for the specified contact. **API Endpoint**:  PUT /v4/contacts/{contactId}/addresses/{id}  **Scope**:  contacts  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaContactsAndProfessionals.Api;
using AccelaContactsAndProfessionals.Client;
using AccelaContactsAndProfessionals.Model;

namespace Example
{
    public class V4PutContactsContactIdAddressesIdExample
    {
        public void main()
        {
            var apiInstance = new ContactsAddressesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var contactId = 789;  // long? | The ID of the contact to fetch.
            var body = new ContactAddressModel(); // ContactAddressModel | Contact information to be updated.
            var id = 789;  // long? | The ID of the address to fetch.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Contact Address
                ContactAddressModel result = apiInstance.V4PutContactsContactIdAddressesId(contentType, authorization, contactId, body, id, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ContactsAddressesApi.V4PutContactsContactIdAddressesId: " + e.Message );
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
 **contactId** | **long?**| The ID of the contact to fetch. | 
 **body** | [**ContactAddressModel**](ContactAddressModel.md)| Contact information to be updated. | 
 **id** | **long?**| The ID of the address to fetch. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ContactAddressModel**](ContactAddressModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

