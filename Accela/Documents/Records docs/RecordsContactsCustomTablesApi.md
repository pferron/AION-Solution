# AccelaRecords.Api.RecordsContactsCustomTablesApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetRecordsRecordIdContactsContactIdCustomTables**](RecordsContactsCustomTablesApi.md#v4getrecordsrecordidcontactscontactidcustomtables) | **GET** /v4/records/{recordId}/contacts/{contactId}/customTables | Get All Custom Tables for Record Contact
[**V4GetRecordsRecordIdContactsContactIdCustomTablesMeta**](RecordsContactsCustomTablesApi.md#v4getrecordsrecordidcontactscontactidcustomtablesmeta) | **GET** /v4/records/{recordId}/contacts/{contactId}/customTables/meta | Get Metadata of All Record Contact Custom Tables
[**V4GetRecordsRecordIdContactsContactIdCustomTablesTableIdMeta**](RecordsContactsCustomTablesApi.md#v4getrecordsrecordidcontactscontactidcustomtablestableidmeta) | **GET** /v4/records/{recordId}/contacts/{contactId}/customTables/{tableId}/meta | Get Metadata of a Record Contact Custom Table
[**V4PutRecordsRecordIdContactsContactIdCustomTables**](RecordsContactsCustomTablesApi.md#v4putrecordsrecordidcontactscontactidcustomtables) | **PUT** /v4/records/{recordId}/contacts/{contactId}/customTables | Update Record Custom Tables


<a name="v4getrecordsrecordidcontactscontactidcustomtables"></a>
# **V4GetRecordsRecordIdContactsContactIdCustomTables**
> ResponseTableModelArray V4GetRecordsRecordIdContactsContactIdCustomTables (string contentType, string authorization, string recordId, long? contactId, string fields = null, string lang = null)

Get All Custom Tables for Record Contact

Gets the custom tables associated with the specified record contact. **API Endpoint**:  GET /v4/records/{recordId}/contacts/{contactId}/customTables  **Scope**:  records  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 8.0.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdContactsContactIdCustomTablesExample
    {
        public void main()
        {
            var apiInstance = new RecordsContactsCustomTablesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var contactId = 789;  // long? | The ID of the contact to fetch. See [Get All Contacts](./api-contacts-professionals.html#operation/v4.get.contacts).
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Custom Tables for Record Contact
                ResponseTableModelArray result = apiInstance.V4GetRecordsRecordIdContactsContactIdCustomTables(contentType, authorization, recordId, contactId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsContactsCustomTablesApi.V4GetRecordsRecordIdContactsContactIdCustomTables: " + e.Message );
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

[**ResponseTableModelArray**](ResponseTableModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getrecordsrecordidcontactscontactidcustomtablesmeta"></a>
# **V4GetRecordsRecordIdContactsContactIdCustomTablesMeta**
> ResponseCustomFormSubgroupModelArray V4GetRecordsRecordIdContactsContactIdCustomTablesMeta (string contentType, string authorization, string recordId, long? contactId, string fields = null, string lang = null)

Get Metadata of All Record Contact Custom Tables

Gets the metadata of all custom tables associated with the specified record contact. **API Endpoint**:  GET /v4/records/{recordId}/contacts/{contactId}/customTables/meta  **Scope**:  records  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 8.0.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdContactsContactIdCustomTablesMetaExample
    {
        public void main()
        {
            var apiInstance = new RecordsContactsCustomTablesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var contactId = 789;  // long? | The ID of the contact to fetch. See [Get All Contacts](./api-contacts-professionals.html#operation/v4.get.contacts).
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Metadata of All Record Contact Custom Tables
                ResponseCustomFormSubgroupModelArray result = apiInstance.V4GetRecordsRecordIdContactsContactIdCustomTablesMeta(contentType, authorization, recordId, contactId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsContactsCustomTablesApi.V4GetRecordsRecordIdContactsContactIdCustomTablesMeta: " + e.Message );
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

<a name="v4getrecordsrecordidcontactscontactidcustomtablestableidmeta"></a>
# **V4GetRecordsRecordIdContactsContactIdCustomTablesTableIdMeta**
> ResponseCustomFormSubgroupModelArray V4GetRecordsRecordIdContactsContactIdCustomTablesTableIdMeta (string contentType, string authorization, string recordId, long? contactId, string tableId, string fields = null, string lang = null)

Get Metadata of a Record Contact Custom Table

Gets the metadata of a specified custom table associated with the specified record contact. **API Endpoint**:  GET /v4/records/{recordId}/contacts/{contactId}/customTables/{tableId}/meta  **Scope**:  records  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 8.0.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdContactsContactIdCustomTablesTableIdMetaExample
    {
        public void main()
        {
            var apiInstance = new RecordsContactsCustomTablesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var contactId = 789;  // long? | The ID of the contact to fetch. See [Get All Contacts](./api-contacts-professionals.html#operation/v4.get.contacts).
            var tableId = tableId_example;  // string | The custom table ID to fetch.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Metadata of a Record Contact Custom Table
                ResponseCustomFormSubgroupModelArray result = apiInstance.V4GetRecordsRecordIdContactsContactIdCustomTablesTableIdMeta(contentType, authorization, recordId, contactId, tableId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsContactsCustomTablesApi.V4GetRecordsRecordIdContactsContactIdCustomTablesTableIdMeta: " + e.Message );
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
 **tableId** | **string**| The custom table ID to fetch. | 
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

<a name="v4putrecordsrecordidcontactscontactidcustomtables"></a>
# **V4PutRecordsRecordIdContactsContactIdCustomTables**
> ResponseResultModelArray V4PutRecordsRecordIdContactsContactIdCustomTables (string contentType, string authorization, string recordId, long? contactId, List<TableModel> body = null, string fields = null, string lang = null)

Update Record Custom Tables

Updates the custom tables for the specified record contact. The request body is an array of custom tables, each with the custom table id and an array of rows. Use this API to add, update and delete rows from an existing custom table. (Custom tables are defined in Civic Platform.) Note that the modified custom table data only applies to the transactional record contact, not the reference contact. **API Endpoint**:  PUT /v4/records/{recordId}/contacts/{contactId}/customTables  **Scope**:  records  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 8.0.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4PutRecordsRecordIdContactsContactIdCustomTablesExample
    {
        public void main()
        {
            var apiInstance = new RecordsContactsCustomTablesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var contactId = 789;  // long? | The ID of the contact to fetch. See [Get All Contacts](./api-contacts-professionals.html#operation/v4.get.contacts).
            var body = new List<TableModel>(); // List<TableModel> | The custom table information to be updated. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Record Custom Tables
                ResponseResultModelArray result = apiInstance.V4PutRecordsRecordIdContactsContactIdCustomTables(contentType, authorization, recordId, contactId, body, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsContactsCustomTablesApi.V4PutRecordsRecordIdContactsContactIdCustomTables: " + e.Message );
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
 **body** | [**List&lt;TableModel&gt;**](TableModel.md)| The custom table information to be updated. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

