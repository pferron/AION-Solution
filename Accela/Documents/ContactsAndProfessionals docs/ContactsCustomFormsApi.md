# AccelaContactsAndProfessionals.Api.ContactsCustomFormsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetContactsIdCustomForms**](ContactsCustomFormsApi.md#v4getcontactsidcustomforms) | **GET** /v4/contacts/{id}/customForms | Get All Custom Forms for Contact
[**V4GetContactsIdCustomFormsFormIdMeta**](ContactsCustomFormsApi.md#v4getcontactsidcustomformsformidmeta) | **GET** /v4/contacts/{id}/customForms/{formId}/meta | Get Custom Form Metadata for Contact
[**V4GetContactsIdCustomFormsMeta**](ContactsCustomFormsApi.md#v4getcontactsidcustomformsmeta) | **GET** /v4/contacts/{id}/customForms/meta | Get All Custom Forms Metadata for Contact
[**V4PutContactsIdCustomForms**](ContactsCustomFormsApi.md#v4putcontactsidcustomforms) | **PUT** /v4/contacts/{id}/customForms | Update Contact Custom Forms


<a name="v4getcontactsidcustomforms"></a>
# **V4GetContactsIdCustomForms**
> ResponseCustomAttributeModelArray V4GetContactsIdCustomForms (string contentType, string authorization, string id, string lang = null)

Get All Custom Forms for Contact

Gets all custom forms associated with the specified contact. **API Endpoint**:  GET /v4/contacts/{id}/customForms  **Scope**:  contacts  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3.4 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaContactsAndProfessionals.Api;
using AccelaContactsAndProfessionals.Client;
using AccelaContactsAndProfessionals.Model;

namespace Example
{
    public class V4GetContactsIdCustomFormsExample
    {
        public void main()
        {
            var apiInstance = new ContactsCustomFormsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = id_example;  // string | The ID of the contact to fetch.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Custom Forms for Contact
                ResponseCustomAttributeModelArray result = apiInstance.V4GetContactsIdCustomForms(contentType, authorization, id, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ContactsCustomFormsApi.V4GetContactsIdCustomForms: " + e.Message );
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
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseCustomAttributeModelArray**](ResponseCustomAttributeModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getcontactsidcustomformsformidmeta"></a>
# **V4GetContactsIdCustomFormsFormIdMeta**
> ResponseCustomFormMetadataModelArray V4GetContactsIdCustomFormsFormIdMeta (string contentType, string authorization, string id, string formId, string lang = null)

Get Custom Form Metadata for Contact

Gets the metadata associated with the specified custom form for the contact. **API Endpoint**:  GET /v4/contacts/{id}/customForms/{formId}/meta  **Scope**:  contacts  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3.4 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaContactsAndProfessionals.Api;
using AccelaContactsAndProfessionals.Client;
using AccelaContactsAndProfessionals.Model;

namespace Example
{
    public class V4GetContactsIdCustomFormsFormIdMetaExample
    {
        public void main()
        {
            var apiInstance = new ContactsCustomFormsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = id_example;  // string | The ID of the contact to fetch.
            var formId = formId_example;  // string | The ID of the custom form to fetch.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Custom Form Metadata for Contact
                ResponseCustomFormMetadataModelArray result = apiInstance.V4GetContactsIdCustomFormsFormIdMeta(contentType, authorization, id, formId, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ContactsCustomFormsApi.V4GetContactsIdCustomFormsFormIdMeta: " + e.Message );
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
 **formId** | **string**| The ID of the custom form to fetch. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseCustomFormMetadataModelArray**](ResponseCustomFormMetadataModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getcontactsidcustomformsmeta"></a>
# **V4GetContactsIdCustomFormsMeta**
> ResponseCustomFormMetadataModelArray V4GetContactsIdCustomFormsMeta (string contentType, string authorization, string id, string fields = null, string lang = null)

Get All Custom Forms Metadata for Contact

Gets the metadata associated with all custom forms for the contact. **API Endpoint**:  GET /v4/contacts/{id}/customForms/meta  **Scope**:  contacts  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3.4 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaContactsAndProfessionals.Api;
using AccelaContactsAndProfessionals.Client;
using AccelaContactsAndProfessionals.Model;

namespace Example
{
    public class V4GetContactsIdCustomFormsMetaExample
    {
        public void main()
        {
            var apiInstance = new ContactsCustomFormsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = id_example;  // string | The ID of the contact to fetch.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Custom Forms Metadata for Contact
                ResponseCustomFormMetadataModelArray result = apiInstance.V4GetContactsIdCustomFormsMeta(contentType, authorization, id, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ContactsCustomFormsApi.V4GetContactsIdCustomFormsMeta: " + e.Message );
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
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseCustomFormMetadataModelArray**](ResponseCustomFormMetadataModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4putcontactsidcustomforms"></a>
# **V4PutContactsIdCustomForms**
> ResponseResultModelArray V4PutContactsIdCustomForms (string contentType, string authorization, string id, List<CustomFormModel> body, string lang = null)

Update Contact Custom Forms

Updates the custom forms for the specified record contact. The request body is an array of custom forms, with each item containing the custom form's id and custom field name/value pairs.  **API Endpoint**:  PUT /v4/contacts/{id}/customForms  **Scope**:  contacts  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3.4 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaContactsAndProfessionals.Api;
using AccelaContactsAndProfessionals.Client;
using AccelaContactsAndProfessionals.Model;

namespace Example
{
    public class V4PutContactsIdCustomFormsExample
    {
        public void main()
        {
            var apiInstance = new ContactsCustomFormsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = id_example;  // string | The ID of the contact to fetch.
            var body = new List<CustomFormModel>(); // List<CustomFormModel> | Custom forms information to be updated. Ex. [{\"apiField1\": \"val1\", \"id\": \"group-subGroup\"}]
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Contact Custom Forms
                ResponseResultModelArray result = apiInstance.V4PutContactsIdCustomForms(contentType, authorization, id, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ContactsCustomFormsApi.V4PutContactsIdCustomForms: " + e.Message );
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
 **body** | [**List&lt;CustomFormModel&gt;**](CustomFormModel.md)| Custom forms information to be updated. Ex. [{\&quot;apiField1\&quot;: \&quot;val1\&quot;, \&quot;id\&quot;: \&quot;group-subGroup\&quot;}] | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

