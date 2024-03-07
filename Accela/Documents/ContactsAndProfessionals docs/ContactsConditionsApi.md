# AccelaContactsAndProfessionals.Api.ContactsConditionsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4DeleteContactsContactIdConditionsId**](ContactsConditionsApi.md#v4deletecontactscontactidconditionsid) | **DELETE** /v4/contacts/{contactId}/conditions/{id} | Delete Contact Condition
[**V4GetContactsContactIdConditions**](ContactsConditionsApi.md#v4getcontactscontactidconditions) | **GET** /v4/contacts/{contactId}/conditions | Get All Contact Conditions
[**V4PostContactsContactIdConditions**](ContactsConditionsApi.md#v4postcontactscontactidconditions) | **POST** /v4/contacts/{contactId}/conditions | Create Contact Conditions
[**V4PutContactsContactIdConditionsId**](ContactsConditionsApi.md#v4putcontactscontactidconditionsid) | **PUT** /v4/contacts/{contactId}/conditions/{id} | Update Contact Condition


<a name="v4deletecontactscontactidconditionsid"></a>
# **V4DeleteContactsContactIdConditionsId**
> ResponseCommonConditionModel V4DeleteContactsContactIdConditionsId (string contentType, string authorization, string contactId, long? id, string lang = null)

Delete Contact Condition

Deletes the condition for the specified contact. **API Endpoint**:  DELETE /v4/contacts/{contactId}/conditions/{id}  **Scope**:  contacts  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaContactsAndProfessionals.Api;
using AccelaContactsAndProfessionals.Client;
using AccelaContactsAndProfessionals.Model;

namespace Example
{
    public class V4DeleteContactsContactIdConditionsIdExample
    {
        public void main()
        {
            var apiInstance = new ContactsConditionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var contactId = contactId_example;  // string | The ID of the contact to fetch.
            var id = 789;  // long? | The ID of the condition to delete.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Delete Contact Condition
                ResponseCommonConditionModel result = apiInstance.V4DeleteContactsContactIdConditionsId(contentType, authorization, contactId, id, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ContactsConditionsApi.V4DeleteContactsContactIdConditionsId: " + e.Message );
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
 **contactId** | **string**| The ID of the contact to fetch. | 
 **id** | **long?**| The ID of the condition to delete. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseCommonConditionModel**](ResponseCommonConditionModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getcontactscontactidconditions"></a>
# **V4GetContactsContactIdConditions**
> ResponseCommonConditionModelArray V4GetContactsContactIdConditions (string contentType, string authorization, string contactId, string type = null, string name = null, string status = null, string fields = null, long? offset = null, long? limit = null, string lang = null)

Get All Contact Conditions

Gets condition information for the specified contact. **API Endpoint**:  GET /v4/contacts/{contactId}/conditions  **Scope**:  contacts  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3.4 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaContactsAndProfessionals.Api;
using AccelaContactsAndProfessionals.Client;
using AccelaContactsAndProfessionals.Model;

namespace Example
{
    public class V4GetContactsContactIdConditionsExample
    {
        public void main()
        {
            var apiInstance = new ContactsConditionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var contactId = contactId_example;  // string | The ID of the contact to fetch.
            var type = type_example;  // string | Filter by contact types. See [Get All Contact Types](./api-settings.html#operation/v4.get.settings.contacts.types). (optional) 
            var name = name_example;  // string | Filter by condition name. See [Get All Approval Conditions](./api-misc.html#operation/v4.get.conditionApprovals.standard), [Get All Standard Conditions](./api-misc.html#operation/v4.get.conditions.standard). (optional) 
            var status = status_example;  // string | Filter by status. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Contact Conditions
                ResponseCommonConditionModelArray result = apiInstance.V4GetContactsContactIdConditions(contentType, authorization, contactId, type, name, status, fields, offset, limit, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ContactsConditionsApi.V4GetContactsContactIdConditions: " + e.Message );
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
 **contactId** | **string**| The ID of the contact to fetch. | 
 **type** | **string**| Filter by contact types. See [Get All Contact Types](./api-settings.html#operation/v4.get.settings.contacts.types). | [optional] 
 **name** | **string**| Filter by condition name. See [Get All Approval Conditions](./api-misc.html#operation/v4.get.conditionApprovals.standard), [Get All Standard Conditions](./api-misc.html#operation/v4.get.conditions.standard). | [optional] 
 **status** | **string**| Filter by status. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseCommonConditionModelArray**](ResponseCommonConditionModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4postcontactscontactidconditions"></a>
# **V4PostContactsContactIdConditions**
> ResponseResultModelArray V4PostContactsContactIdConditions (string contentType, string authorization, string contactId, List<RequestCommonConditionModel> body = null, string lang = null)

Create Contact Conditions

Adds condition information to the specified contact. **API Endpoint**:  POST /v4/contacts/{contactId}/conditions  **Scope**:  contacts  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaContactsAndProfessionals.Api;
using AccelaContactsAndProfessionals.Client;
using AccelaContactsAndProfessionals.Model;

namespace Example
{
    public class V4PostContactsContactIdConditionsExample
    {
        public void main()
        {
            var apiInstance = new ContactsConditionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var contactId = contactId_example;  // string | The ID of the contact to fetch.
            var body = new List<RequestCommonConditionModel>(); // List<RequestCommonConditionModel> | Add contact condition request information. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Create Contact Conditions
                ResponseResultModelArray result = apiInstance.V4PostContactsContactIdConditions(contentType, authorization, contactId, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ContactsConditionsApi.V4PostContactsContactIdConditions: " + e.Message );
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
 **contactId** | **string**| The ID of the contact to fetch. | 
 **body** | [**List&lt;RequestCommonConditionModel&gt;**](RequestCommonConditionModel.md)| Add contact condition request information. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4putcontactscontactidconditionsid"></a>
# **V4PutContactsContactIdConditionsId**
> ResponseCommonConditionModel V4PutContactsContactIdConditionsId (string contentType, string authorization, string contactId, string id, RequestCommonConditionModel body, string lang = null)

Update Contact Condition

Updates the condition information for the specified contact. **API Endpoint**:  PUT /v4/contacts/{contactId}/conditions/{id}  **Scope**:  contacts  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3.4 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaContactsAndProfessionals.Api;
using AccelaContactsAndProfessionals.Client;
using AccelaContactsAndProfessionals.Model;

namespace Example
{
    public class V4PutContactsContactIdConditionsIdExample
    {
        public void main()
        {
            var apiInstance = new ContactsConditionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var contactId = contactId_example;  // string | The ID of the contact to fetch.
            var id = id_example;  // string | The ID of the condition to be updated.
            var body = new RequestCommonConditionModel(); // RequestCommonConditionModel | Update contact condition request information.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Contact Condition
                ResponseCommonConditionModel result = apiInstance.V4PutContactsContactIdConditionsId(contentType, authorization, contactId, id, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ContactsConditionsApi.V4PutContactsContactIdConditionsId: " + e.Message );
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
 **contactId** | **string**| The ID of the contact to fetch. | 
 **id** | **string**| The ID of the condition to be updated. | 
 **body** | [**RequestCommonConditionModel**](RequestCommonConditionModel.md)| Update contact condition request information. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseCommonConditionModel**](ResponseCommonConditionModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

