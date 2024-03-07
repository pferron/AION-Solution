# AccelaContactsAndProfessionals.Api.ContactsCustomTablesApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetContactsContactIdCustomTables**](ContactsCustomTablesApi.md#v4getcontactscontactidcustomtables) | **GET** /v4/contacts/{contactId}/customTables | Get All Custom Tables for Contact
[**V4GetContactsContactIdCustomTablesMeta**](ContactsCustomTablesApi.md#v4getcontactscontactidcustomtablesmeta) | **GET** /v4/contacts/{contactId}/customTables/meta | Get All Custom Tables Metadata for Contact
[**V4GetContactsContactIdCustomTablesTableIdMeta**](ContactsCustomTablesApi.md#v4getcontactscontactidcustomtablestableidmeta) | **GET** /v4/contacts/{contactId}/customTables/{tableId}/meta | Get Custom Table Metadata for Contact
[**V4PutContactsContactIdCustomTables**](ContactsCustomTablesApi.md#v4putcontactscontactidcustomtables) | **PUT** /v4/contacts/{contactId}/customTables | Update Custom Tables for Contact


<a name="v4getcontactscontactidcustomtables"></a>
# **V4GetContactsContactIdCustomTables**
> ResponseTableModelArray V4GetContactsContactIdCustomTables (string contentType, string authorization, long? contactId, string fields = null, string lang = null)

Get All Custom Tables for Contact

Gets the custom tables associated with the specified contact. **API Endpoint**:  GET /v4/contacts/{contactId}/customTables  **Scope**:  contacts  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3.4 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaContactsAndProfessionals.Api;
using AccelaContactsAndProfessionals.Client;
using AccelaContactsAndProfessionals.Model;

namespace Example
{
    public class V4GetContactsContactIdCustomTablesExample
    {
        public void main()
        {
            var apiInstance = new ContactsCustomTablesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var contactId = 789;  // long? | The ID of the contact to fetch.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Custom Tables for Contact
                ResponseTableModelArray result = apiInstance.V4GetContactsContactIdCustomTables(contentType, authorization, contactId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ContactsCustomTablesApi.V4GetContactsContactIdCustomTables: " + e.Message );
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
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseTableModelArray**](ResponseTableModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getcontactscontactidcustomtablesmeta"></a>
# **V4GetContactsContactIdCustomTablesMeta**
> ResponseCustomFormMetadataModelArray V4GetContactsContactIdCustomTablesMeta (string contentType, string authorization, long? contactId, string fields = null, string lang = null)

Get All Custom Tables Metadata for Contact

Gets the metadata associated with all custom tables for the contact. **API Endpoint**:  GET /v4/contacts/{contactId}/customTables/meta  **Scope**:  contacts  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3.4 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaContactsAndProfessionals.Api;
using AccelaContactsAndProfessionals.Client;
using AccelaContactsAndProfessionals.Model;

namespace Example
{
    public class V4GetContactsContactIdCustomTablesMetaExample
    {
        public void main()
        {
            var apiInstance = new ContactsCustomTablesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var contactId = 789;  // long? | The ID of the contact to fetch.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Custom Tables Metadata for Contact
                ResponseCustomFormMetadataModelArray result = apiInstance.V4GetContactsContactIdCustomTablesMeta(contentType, authorization, contactId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ContactsCustomTablesApi.V4GetContactsContactIdCustomTablesMeta: " + e.Message );
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

<a name="v4getcontactscontactidcustomtablestableidmeta"></a>
# **V4GetContactsContactIdCustomTablesTableIdMeta**
> ResponseCustomFormMetadataModelArray V4GetContactsContactIdCustomTablesTableIdMeta (string contentType, string authorization, long? contactId, string tableId, string fields = null, string lang = null)

Get Custom Table Metadata for Contact

Gets the metadata associated with the specified custom table for the contact. **API Endpoint**:  GET /v4/contacts/{contactId}/customTables/{tableId}/meta  **Scope**:  contacts  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3.4 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaContactsAndProfessionals.Api;
using AccelaContactsAndProfessionals.Client;
using AccelaContactsAndProfessionals.Model;

namespace Example
{
    public class V4GetContactsContactIdCustomTablesTableIdMetaExample
    {
        public void main()
        {
            var apiInstance = new ContactsCustomTablesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var contactId = 789;  // long? | The ID of the contact to fetch.
            var tableId = tableId_example;  // string | The ID of the custom table to fetch.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Custom Table Metadata for Contact
                ResponseCustomFormMetadataModelArray result = apiInstance.V4GetContactsContactIdCustomTablesTableIdMeta(contentType, authorization, contactId, tableId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ContactsCustomTablesApi.V4GetContactsContactIdCustomTablesTableIdMeta: " + e.Message );
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
 **tableId** | **string**| The ID of the custom table to fetch. | 
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

<a name="v4putcontactscontactidcustomtables"></a>
# **V4PutContactsContactIdCustomTables**
> ResponseResultModelArray V4PutContactsContactIdCustomTables (string contentType, string authorization, long? contactId, List<TableModel> body, string lang = null)

Update Custom Tables for Contact

Updates a custom table for the specified contact. **API Endpoint**:  PUT /v4/contacts/{contactId}/customTables  **Scope**:  contacts  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaContactsAndProfessionals.Api;
using AccelaContactsAndProfessionals.Client;
using AccelaContactsAndProfessionals.Model;

namespace Example
{
    public class V4PutContactsContactIdCustomTablesExample
    {
        public void main()
        {
            var apiInstance = new ContactsCustomTablesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var contactId = 789;  // long? | The ID of the contact to fetch.
            var body = new List<TableModel>(); // List<TableModel> | Custom tables to be updated.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Custom Tables for Contact
                ResponseResultModelArray result = apiInstance.V4PutContactsContactIdCustomTables(contentType, authorization, contactId, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ContactsCustomTablesApi.V4PutContactsContactIdCustomTables: " + e.Message );
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
 **body** | [**List&lt;TableModel&gt;**](TableModel.md)| Custom tables to be updated. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

