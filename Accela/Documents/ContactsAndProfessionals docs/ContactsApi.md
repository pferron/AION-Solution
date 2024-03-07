# AccelaContactsAndProfessionals.Api.ContactsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4DeleteContactsIds**](ContactsApi.md#v4deletecontactsids) | **DELETE** /v4/contacts/{ids} | Delete Contacts
[**V4GetContacts**](ContactsApi.md#v4getcontacts) | **GET** /v4/contacts | Get All Contacts
[**V4GetContactsIds**](ContactsApi.md#v4getcontactsids) | **GET** /v4/contacts/{ids} | Get Contacts
[**V4PostContacts**](ContactsApi.md#v4postcontacts) | **POST** /v4/contacts | Create Contacts
[**V4PutContactsId**](ContactsApi.md#v4putcontactsid) | **PUT** /v4/Contacts/{id} | Update Contact


<a name="v4deletecontactsids"></a>
# **V4DeleteContactsIds**
> ResponseResultModelArray V4DeleteContactsIds (string contentType, string authorization, string ids, string lang = null)

Delete Contacts

Deletes contacts. **API Endpoint**:  DELETE /v4/contacts/{ids}   **Scope**:  contacts  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaContactsAndProfessionals.Api;
using AccelaContactsAndProfessionals.Client;
using AccelaContactsAndProfessionals.Model;

namespace Example
{
    public class V4DeleteContactsIdsExample
    {
        public void main()
        {
            var apiInstance = new ContactsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var ids = ids_example;  // string | Comma-delimited IDs of contacts to delete.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Delete Contacts
                ResponseResultModelArray result = apiInstance.V4DeleteContactsIds(contentType, authorization, ids, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ContactsApi.V4DeleteContactsIds: " + e.Message );
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

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getcontacts"></a>
# **V4GetContacts**
> ResponseContactModelArray V4GetContacts (string contentType, string authorization, string state = null, string country = null, string firstName = null, string middleName = null, string lastName = null, string email = null, string addressLine1 = null, string addressLine2 = null, string addressLine3 = null, string businessName = null, string city = null, string title = null, long? offset = null, long? limit = null, string fields = null, string lang = null)

Get All Contacts

Gets all contacts available in the system. **API Endpoint**:  GET /v4/contacts  **Scope**:  contacts  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaContactsAndProfessionals.Api;
using AccelaContactsAndProfessionals.Client;
using AccelaContactsAndProfessionals.Model;

namespace Example
{
    public class V4GetContactsExample
    {
        public void main()
        {
            var apiInstance = new ContactsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var state = state_example;  // string | Filter by contact's address state. (optional) 
            var country = country_example;  // string | Filter by contact's address country. (optional) 
            var firstName = firstName_example;  // string | Filter by contact's first name. (optional) 
            var middleName = middleName_example;  // string | Filter by contact's middle name. (optional) 
            var lastName = lastName_example;  // string | Filter by contact's last name. (optional) 
            var email = email_example;  // string | Filter by contact's email address. (optional) 
            var addressLine1 = addressLine1_example;  // string | Filter by contact's first line of address. (optional) 
            var addressLine2 = addressLine2_example;  // string | Filter by contact's second line of address. (optional) 
            var addressLine3 = addressLine3_example;  // string | Filter by contact's third line of address. (optional) 
            var businessName = businessName_example;  // string | Filter by contact's business name. (optional) 
            var city = city_example;  // string | Filter by contact's address city. (optional) 
            var title = title_example;  // string | Filter by contact's title. (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Contacts
                ResponseContactModelArray result = apiInstance.V4GetContacts(contentType, authorization, state, country, firstName, middleName, lastName, email, addressLine1, addressLine2, addressLine3, businessName, city, title, offset, limit, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ContactsApi.V4GetContacts: " + e.Message );
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
 **state** | **string**| Filter by contact&#39;s address state. | [optional] 
 **country** | **string**| Filter by contact&#39;s address country. | [optional] 
 **firstName** | **string**| Filter by contact&#39;s first name. | [optional] 
 **middleName** | **string**| Filter by contact&#39;s middle name. | [optional] 
 **lastName** | **string**| Filter by contact&#39;s last name. | [optional] 
 **email** | **string**| Filter by contact&#39;s email address. | [optional] 
 **addressLine1** | **string**| Filter by contact&#39;s first line of address. | [optional] 
 **addressLine2** | **string**| Filter by contact&#39;s second line of address. | [optional] 
 **addressLine3** | **string**| Filter by contact&#39;s third line of address. | [optional] 
 **businessName** | **string**| Filter by contact&#39;s business name. | [optional] 
 **city** | **string**| Filter by contact&#39;s address city. | [optional] 
 **title** | **string**| Filter by contact&#39;s title. | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseContactModelArray**](ResponseContactModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getcontactsids"></a>
# **V4GetContactsIds**
> ResponseContactModelArray V4GetContactsIds (string contentType, string authorization, string ids, string fields = null, string lang = null)

Get Contacts

Gets information for requested reference contacts. **API Endpoint**:  GET /v4/contacts/{ids}  **Scope**:  contacts  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaContactsAndProfessionals.Api;
using AccelaContactsAndProfessionals.Client;
using AccelaContactsAndProfessionals.Model;

namespace Example
{
    public class V4GetContactsIdsExample
    {
        public void main()
        {
            var apiInstance = new ContactsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var ids = ids_example;  // string | Comma-delimited IDs of reference contacts to fetch.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Contacts
                ResponseContactModelArray result = apiInstance.V4GetContactsIds(contentType, authorization, ids, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ContactsApi.V4GetContactsIds: " + e.Message );
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
 **ids** | **string**| Comma-delimited IDs of reference contacts to fetch. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseContactModelArray**](ResponseContactModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4postcontacts"></a>
# **V4PostContacts**
> ResponseResultModelArray V4PostContacts (string contentType, string authorization, List<ContactModel> body, string lang = null)

Create Contacts

Creates new reference contacts. **API Endpoint**:  POST /v4/contacts  **Scope**:  contacts  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaContactsAndProfessionals.Api;
using AccelaContactsAndProfessionals.Client;
using AccelaContactsAndProfessionals.Model;

namespace Example
{
    public class V4PostContactsExample
    {
        public void main()
        {
            var apiInstance = new ContactsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var body = new List<ContactModel>(); // List<ContactModel> | Reference contact information to add.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Create Contacts
                ResponseResultModelArray result = apiInstance.V4PostContacts(contentType, authorization, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ContactsApi.V4PostContacts: " + e.Message );
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
 **body** | [**List&lt;ContactModel&gt;**](ContactModel.md)| Reference contact information to add. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4putcontactsid"></a>
# **V4PutContactsId**
> ResponseContactModel V4PutContactsId (string contentType, string authorization, string id, RequestContactModel body, string lang = null)

Update Contact

Updates contact information. **API Endpoint**:  PUT /v4/contacts/{id}  **Scope**:  contacts  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaContactsAndProfessionals.Api;
using AccelaContactsAndProfessionals.Client;
using AccelaContactsAndProfessionals.Model;

namespace Example
{
    public class V4PutContactsIdExample
    {
        public void main()
        {
            var apiInstance = new ContactsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = id_example;  // string | The ID of the contact to fetch.
            var body = new RequestContactModel(); // RequestContactModel | The contact information to be updated
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Contact
                ResponseContactModel result = apiInstance.V4PutContactsId(contentType, authorization, id, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ContactsApi.V4PutContactsId: " + e.Message );
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
 **body** | [**RequestContactModel**](RequestContactModel.md)| The contact information to be updated | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseContactModel**](ResponseContactModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

