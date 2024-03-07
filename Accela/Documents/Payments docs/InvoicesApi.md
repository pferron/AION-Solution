# AccelaPayments.Api.InvoicesApi

All URIs are relative to *https://apis.accela.com/*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetInvoicesInvoiceIds**](InvoicesApi.md#v4getinvoicesinvoiceids) | **GET** /v4/invoices/{invoiceIds} | Get Invoices

<a name="v4getinvoicesinvoiceids"></a>
# **V4GetInvoicesInvoiceIds**
> ResponseInvoiceModelArray V4GetInvoicesInvoiceIds (string contentType, string authorization, string invoiceIds, string fields = null, string lang = null)

Get Invoices

Returns invoice information for given invoice id's. **API Endpoint**:  GET /v4/invoices/{invoiceIds}  **Scope**:  invoices  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 9.0.0 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaPayments.Api;
using AccelaPayments.Client;
using AccelaPayments.Model;

namespace Example
{
    public class V4GetInvoicesInvoiceIdsExample
    {
        public void main()
        {
            var apiInstance = new InvoicesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var invoiceIds = invoiceIds_example;  // string | One or more comma-separated IDs of invoices to fetch.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Invoices
                ResponseInvoiceModelArray result = apiInstance.V4GetInvoicesInvoiceIds(contentType, authorization, invoiceIds, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InvoicesApi.V4GetInvoicesInvoiceIds: " + e.Message );
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
 **invoiceIds** | **string**| One or more comma-separated IDs of invoices to fetch. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseInvoiceModelArray**](ResponseInvoiceModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
