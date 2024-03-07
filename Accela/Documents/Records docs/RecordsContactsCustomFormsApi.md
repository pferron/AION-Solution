# AccelaRecords.Api.RecordsContactsCustomFormsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetRecordsRecordIdContactsContactIdCustomForms**](RecordsContactsCustomFormsApi.md#v4getrecordsrecordidcontactscontactidcustomforms) | **GET** /v4/records/{recordId}/contacts/{contactId}/customForms | Get Record Contacts Custom Forms
[**V4GetRecordsRecordIdContactsContactIdCustomFormsFormIdMeta**](RecordsContactsCustomFormsApi.md#v4getrecordsrecordidcontactscontactidcustomformsformidmeta) | **GET** /v4/records/{recordId}/contacts/{contactId}/customForms/{formId}/meta | Get Record Contact Custom Form Metadata
[**V4GetRecordsRecordIdContactsContactIdCustomFormsMeta**](RecordsContactsCustomFormsApi.md#v4getrecordsrecordidcontactscontactidcustomformsmeta) | **GET** /v4/records/{recordId}/contacts/{contactId}/customForms/meta | Get Record Contacts Custom Forms Meta
[**V4PutRecordsRecordIdContactsContactIdCustomForms**](RecordsContactsCustomFormsApi.md#v4putrecordsrecordidcontactscontactidcustomforms) | **PUT** /v4/records/{recordId}/contacts/{contactId}/customForms | Update Record Contact Custom Forms


<a name="v4getrecordsrecordidcontactscontactidcustomforms"></a>
# **V4GetRecordsRecordIdContactsContactIdCustomForms**
> ResponseCustomAttributeModelArray V4GetRecordsRecordIdContactsContactIdCustomForms (string contentType, string authorization, string recordId, long? contactId, string lang = null)

Get Record Contacts Custom Forms

Returns an array of custom forms associated with the specified record contact. Each custom form consists of the custom form id and custom field name-and-value pairs. **API Endpoint**:  GET /v4/records/{recordId}/contacts/{contactId}/customForms  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdContactsContactIdCustomFormsExample
    {
        public void main()
        {
            var apiInstance = new RecordsContactsCustomFormsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var contactId = 789;  // long? | The ID of the contact to fetch. See [Get All Contacts](./api-contacts-professionals.html#operation/v4.get.contacts).
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Record Contacts Custom Forms
                ResponseCustomAttributeModelArray result = apiInstance.V4GetRecordsRecordIdContactsContactIdCustomForms(contentType, authorization, recordId, contactId, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsContactsCustomFormsApi.V4GetRecordsRecordIdContactsContactIdCustomForms: " + e.Message );
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
 **recordId** | **string**| The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine). | 
 **contactId** | **long?**| The ID of the contact to fetch. See [Get All Contacts](./api-contacts-professionals.html#operation/v4.get.contacts). | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseCustomAttributeModelArray**](ResponseCustomAttributeModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getrecordsrecordidcontactscontactidcustomformsformidmeta"></a>
# **V4GetRecordsRecordIdContactsContactIdCustomFormsFormIdMeta**
> ResponseCustomFormMetadataModelArray V4GetRecordsRecordIdContactsContactIdCustomFormsFormIdMeta (string contentType, string authorization, string recordId, long? contactId, string formId, string fields = null, string lang = null)

Get Record Contact Custom Form Metadata

Gets the metadata associated with the requested custom form for the record contact. **API Endpoint**:  GET /v4/records/{recordId}/contacts/{contactId}/customForms/{formId}/meta  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 9.2.0 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdContactsContactIdCustomFormsFormIdMetaExample
    {
        public void main()
        {
            var apiInstance = new RecordsContactsCustomFormsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var contactId = 789;  // long? | The ID of the contact to fetch. See [Get All Contacts](./api-contacts-professionals.html#operation/v4.get.contacts).
            var formId = formId_example;  // string | The ID of the custom form to fetch.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Record Contact Custom Form Metadata
                ResponseCustomFormMetadataModelArray result = apiInstance.V4GetRecordsRecordIdContactsContactIdCustomFormsFormIdMeta(contentType, authorization, recordId, contactId, formId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsContactsCustomFormsApi.V4GetRecordsRecordIdContactsContactIdCustomFormsFormIdMeta: " + e.Message );
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
 **recordId** | **string**| The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine). | 
 **contactId** | **long?**| The ID of the contact to fetch. See [Get All Contacts](./api-contacts-professionals.html#operation/v4.get.contacts). | 
 **formId** | **string**| The ID of the custom form to fetch. | 
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

<a name="v4getrecordsrecordidcontactscontactidcustomformsmeta"></a>
# **V4GetRecordsRecordIdContactsContactIdCustomFormsMeta**
> ResponseCustomFormSubgroupModelArray V4GetRecordsRecordIdContactsContactIdCustomFormsMeta (string contentType, string authorization, string recordId, long? contactId, string fields = null, string lang = null)

Get Record Contacts Custom Forms Meta

Gets the custom forms metadata associated with the specified record contact. **API Endpoint**:  GET /v4/records/{recordId}/contacts/{contactId}/customForms/meta  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdContactsContactIdCustomFormsMetaExample
    {
        public void main()
        {
            var apiInstance = new RecordsContactsCustomFormsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var contactId = 789;  // long? | The ID of the contact to fetch. See [Get All Contacts](./api-contacts-professionals.html#operation/v4.get.contacts).
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Record Contacts Custom Forms Meta
                ResponseCustomFormSubgroupModelArray result = apiInstance.V4GetRecordsRecordIdContactsContactIdCustomFormsMeta(contentType, authorization, recordId, contactId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsContactsCustomFormsApi.V4GetRecordsRecordIdContactsContactIdCustomFormsMeta: " + e.Message );
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
 **recordId** | **string**| The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine). | 
 **contactId** | **long?**| The ID of the contact to fetch. See [Get All Contacts](./api-contacts-professionals.html#operation/v4.get.contacts). | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseCustomFormSubgroupModelArray**](ResponseCustomFormSubgroupModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4putrecordsrecordidcontactscontactidcustomforms"></a>
# **V4PutRecordsRecordIdContactsContactIdCustomForms**
> ResponseResultModelArray V4PutRecordsRecordIdContactsContactIdCustomForms (string contentType, string authorization, string recordId, long? contactId, List<CustomAttributeModel> body, string lang = null)

Update Record Contact Custom Forms

Updates the custom forms for the specified record contact. The request body is an array of custom forms, with each item containing the custom form's id and custom field name/value pairs.  **API Endpoint**:  PUT /v4/records/{recordId}/contacts/{contactId}/customForms   **Scope**:  records  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4PutRecordsRecordIdContactsContactIdCustomFormsExample
    {
        public void main()
        {
            var apiInstance = new RecordsContactsCustomFormsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var contactId = 789;  // long? | The ID of the contact to fetch. See [Get All Contacts](./api-contacts-professionals.html#operation/v4.get.contacts).
            var body = new List<CustomAttributeModel>(); // List<CustomAttributeModel> | The custom form information to be updated. Exeample: [{ \"field1\": \"field1Val\", \"field2\": \"field2Val\", \"id\": \"Group&SubGroup\" }]
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Record Contact Custom Forms
                ResponseResultModelArray result = apiInstance.V4PutRecordsRecordIdContactsContactIdCustomForms(contentType, authorization, recordId, contactId, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsContactsCustomFormsApi.V4PutRecordsRecordIdContactsContactIdCustomForms: " + e.Message );
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
 **recordId** | **string**| The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine). | 
 **contactId** | **long?**| The ID of the contact to fetch. See [Get All Contacts](./api-contacts-professionals.html#operation/v4.get.contacts). | 
 **body** | [**List&lt;CustomAttributeModel&gt;**](CustomAttributeModel.md)| The custom form information to be updated. Exeample: [{ \&quot;field1\&quot;: \&quot;field1Val\&quot;, \&quot;field2\&quot;: \&quot;field2Val\&quot;, \&quot;id\&quot;: \&quot;Group&amp;SubGroup\&quot; }] | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

