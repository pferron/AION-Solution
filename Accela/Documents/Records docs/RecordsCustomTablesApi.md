# AccelaRecords.Api.RecordsCustomTablesApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetRecordsRecordIdCustomTables**](RecordsCustomTablesApi.md#v4getrecordsrecordidcustomtables) | **GET** /v4/records/{recordId}/customTables | Get All Custom Tables for Record
[**V4GetRecordsRecordIdCustomTablesMeta**](RecordsCustomTablesApi.md#v4getrecordsrecordidcustomtablesmeta) | **GET** /v4/records/{recordId}/customTables/meta | Get All Custom Tables Metadata for Record
[**V4GetRecordsRecordIdCustomTablesTableId**](RecordsCustomTablesApi.md#v4getrecordsrecordidcustomtablestableid) | **GET** /v4/records/{recordId}/customTables/{tableId} | Get Record Custom Table
[**V4GetRecordsRecordIdCustomTablesTableIdMeta**](RecordsCustomTablesApi.md#v4getrecordsrecordidcustomtablestableidmeta) | **GET** /v4/records/{recordId}/customTables/{tableId}/meta | Get Custom Table Metadata for Record
[**V4PutRecordsRecordIdCustomTables**](RecordsCustomTablesApi.md#v4putrecordsrecordidcustomtables) | **PUT** /v4/records/{recordId}/customTables | Update Record Custom Tables


<a name="v4getrecordsrecordidcustomtables"></a>
# **V4GetRecordsRecordIdCustomTables**
> ResponseTableModelArray V4GetRecordsRecordIdCustomTables (string contentType, string authorization, string recordId, string fields = null, string lang = null)

Get All Custom Tables for Record

Gets all the custom tables associated with the specified record. **API Endpoint**:  GET /v4/records/{recordId}/customTables  **Scope**:  records  **App Type**:  All  **Authorization Type**:   No authorization required  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdCustomTablesExample
    {
        public void main()
        {
            var apiInstance = new RecordsCustomTablesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Custom Tables for Record
                ResponseTableModelArray result = apiInstance.V4GetRecordsRecordIdCustomTables(contentType, authorization, recordId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsCustomTablesApi.V4GetRecordsRecordIdCustomTables: " + e.Message );
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

<a name="v4getrecordsrecordidcustomtablesmeta"></a>
# **V4GetRecordsRecordIdCustomTablesMeta**
> ResponseCustomFormSubgroupModelArray V4GetRecordsRecordIdCustomTablesMeta (string contentType, string authorization, string xAccelaAppid, string recordId, string fields = null, string lang = null)

Get All Custom Tables Metadata for Record

Gets detailed data associated with the custom tables for the specified record. **API Endpoint**:  GET /v4/records/{recordId}/customTables/meta  **Scope**:  records  **App Type**:  All  **Authorization Type**:  No authorization required  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdCustomTablesMetaExample
    {
        public void main()
        {
            var apiInstance = new RecordsCustomTablesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var xAccelaAppid = xAccelaAppid_example;  // string | clientid
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Custom Tables Metadata for Record
                ResponseCustomFormSubgroupModelArray result = apiInstance.V4GetRecordsRecordIdCustomTablesMeta(contentType, authorization, xAccelaAppid, recordId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsCustomTablesApi.V4GetRecordsRecordIdCustomTablesMeta: " + e.Message );
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
 **xAccelaAppid** | **string**| clientid | 
 **recordId** | **string**| The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine). | 
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

<a name="v4getrecordsrecordidcustomtablestableid"></a>
# **V4GetRecordsRecordIdCustomTablesTableId**
> ResponseTableModelArray V4GetRecordsRecordIdCustomTablesTableId (string contentType, string authorization, string xAccelaAppid, string recordId, string tableId, string fields = null, string lang = null)

Get Record Custom Table

Gets the requested custom table for the specified record. **API Endpoint**:  GET /v4/records/{recordId}/customTables/{tableId}  **Scope**:  records  **App Type**:  All  **Authorization Type**:   No authorization required  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdCustomTablesTableIdExample
    {
        public void main()
        {
            var apiInstance = new RecordsCustomTablesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var xAccelaAppid = xAccelaAppid_example;  // string | clientid
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var tableId = tableId_example;  // string | The ID of the custom table to fetch.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Record Custom Table
                ResponseTableModelArray result = apiInstance.V4GetRecordsRecordIdCustomTablesTableId(contentType, authorization, xAccelaAppid, recordId, tableId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsCustomTablesApi.V4GetRecordsRecordIdCustomTablesTableId: " + e.Message );
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
 **xAccelaAppid** | **string**| clientid | 
 **recordId** | **string**| The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine). | 
 **tableId** | **string**| The ID of the custom table to fetch. | 
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

<a name="v4getrecordsrecordidcustomtablestableidmeta"></a>
# **V4GetRecordsRecordIdCustomTablesTableIdMeta**
> ResponseCustomFormSubgroupModelArray V4GetRecordsRecordIdCustomTablesTableIdMeta (string contentType, string authorization, string xAccelaAppid, string recordId, string tableId, string fields = null, string lang = null)

Get Custom Table Metadata for Record

Gets the detailed data associated with the specified custom table for the specified record. **API Endpoint**:  GET /v4/records/{recordId}/customTables/{tableId}/meta  **Scope**:  records  **App Type**:  All  **Authorization Type**:  No authorization required  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdCustomTablesTableIdMetaExample
    {
        public void main()
        {
            var apiInstance = new RecordsCustomTablesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var xAccelaAppid = xAccelaAppid_example;  // string | clientid
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var tableId = tableId_example;  // string | The ID of the custom table to fetch.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Custom Table Metadata for Record
                ResponseCustomFormSubgroupModelArray result = apiInstance.V4GetRecordsRecordIdCustomTablesTableIdMeta(contentType, authorization, xAccelaAppid, recordId, tableId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsCustomTablesApi.V4GetRecordsRecordIdCustomTablesTableIdMeta: " + e.Message );
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
 **xAccelaAppid** | **string**| clientid | 
 **recordId** | **string**| The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine). | 
 **tableId** | **string**| The ID of the custom table to fetch. | 
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

<a name="v4putrecordsrecordidcustomtables"></a>
# **V4PutRecordsRecordIdCustomTables**
> ResponseResultModelArray V4PutRecordsRecordIdCustomTables (string contentType, string authorization, string recordId, List<TableModel> body, string fields = null, string lang = null)

Update Record Custom Tables

Updates the custom table for the specified record. **API Endpoint**:  PUT /v4/records/{recordId}/customTables   **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4PutRecordsRecordIdCustomTablesExample
    {
        public void main()
        {
            var apiInstance = new RecordsCustomTablesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var body = new List<TableModel>(); // List<TableModel> | Custom table data to be updated.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Record Custom Tables
                ResponseResultModelArray result = apiInstance.V4PutRecordsRecordIdCustomTables(contentType, authorization, recordId, body, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsCustomTablesApi.V4PutRecordsRecordIdCustomTables: " + e.Message );
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
 **body** | [**List&lt;TableModel&gt;**](TableModel.md)| Custom table data to be updated. | 
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

