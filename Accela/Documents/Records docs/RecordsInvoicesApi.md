# AccelaRecords.Api.RecordsInvoicesApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetRecordsRecordIdInvoices**](RecordsInvoicesApi.md#v4getrecordsrecordidinvoices) | **GET** /v4/records/{recordId}/invoices | Get All Record Invoices
[**V4PostRecordsRecordIdInvoices**](RecordsInvoicesApi.md#v4postrecordsrecordidinvoices) | **POST** /v4/records/{recordId}/invoices | Create Record Invoices


<a name="v4getrecordsrecordidinvoices"></a>
# **V4GetRecordsRecordIdInvoices**
> ResponseInvoiceModelArray V4GetRecordsRecordIdInvoices (string contentType, string recordId, string authorization, string fields = null, string lang = null)

Get All Record Invoices

Returns all invoices for a given record. **API Endpoint**:  GET /v4/records/{recordId}/invoices  **Scope**:  records  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 9.0.0 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdInvoicesExample
    {
        public void main()
        {
            var apiInstance = new RecordsInvoicesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Record Invoices
                ResponseInvoiceModelArray result = apiInstance.V4GetRecordsRecordIdInvoices(contentType, recordId, authorization, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsInvoicesApi.V4GetRecordsRecordIdInvoices: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | [default to application/x-www-form-urlencoded]
 **recordId** | **string**| The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine). | 
 **authorization** | **string**| Construct oAuth2 authentication token | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseInvoiceModelArray**](ResponseInvoiceModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4postrecordsrecordidinvoices"></a>
# **V4PostRecordsRecordIdInvoices**
> ResponseResultModel V4PostRecordsRecordIdInvoices (string contentType, string authorization, string recordId, List<long?> invoiceFeeId, string fields = null, string lang = null)

Create Record Invoices

Adds or links invoices to a given record. **API Endpoint**:  POST /v4/records/{recordId}/invoices  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4PostRecordsRecordIdInvoicesExample
    {
        public void main()
        {
            var apiInstance = new RecordsInvoicesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var invoiceFeeId = ;  // List<long?> | An array of invoice fee IDs to be added to the record. eg: [7295255, 7295256]
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Create Record Invoices
                ResponseResultModel result = apiInstance.V4PostRecordsRecordIdInvoices(contentType, authorization, recordId, invoiceFeeId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsInvoicesApi.V4PostRecordsRecordIdInvoices: " + e.Message );
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
 **invoiceFeeId** | **List&lt;long?&gt;**| An array of invoice fee IDs to be added to the record. eg: [7295255, 7295256] | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModel**](ResponseResultModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

