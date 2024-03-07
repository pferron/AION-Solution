# AccelaRecords.Api.RecordsPaymentsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetRecordsRecordIdPayments**](RecordsPaymentsApi.md#v4getrecordsrecordidpayments) | **GET** /v4/records/{recordId}/payments | Get All Payments for Record
[**V4GetRecordsRecordIdPaymentsPaymentId**](RecordsPaymentsApi.md#v4getrecordsrecordidpaymentspaymentid) | **GET** /v4/records/{recordId}/payments/{paymentId} | Get Record Payment


<a name="v4getrecordsrecordidpayments"></a>
# **V4GetRecordsRecordIdPayments**
> ResponsePaymentModelArray V4GetRecordsRecordIdPayments (string contentType, string authorization, string recordId, string paymentStatus = null, string fields = null, string lang = null)

Get All Payments for Record

Gets information about the payments for the specified record. **API Endpoint**:  GET /v4/records/{recordId}/payments  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 9.0.0 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdPaymentsExample
    {
        public void main()
        {
            var apiInstance = new RecordsPaymentsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var paymentStatus = paymentStatus_example;  // string | Filter by whether or not a payment has been made in full. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Payments for Record
                ResponsePaymentModelArray result = apiInstance.V4GetRecordsRecordIdPayments(contentType, authorization, recordId, paymentStatus, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsPaymentsApi.V4GetRecordsRecordIdPayments: " + e.Message );
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
 **paymentStatus** | **string**| Filter by whether or not a payment has been made in full. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponsePaymentModelArray**](ResponsePaymentModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getrecordsrecordidpaymentspaymentid"></a>
# **V4GetRecordsRecordIdPaymentsPaymentId**
> ResponsePaymentModelArray V4GetRecordsRecordIdPaymentsPaymentId (string contentType, string authorization, string recordId, long? paymentId, string fields = null, string lang = null)

Get Record Payment

Gets information about the specified payment for the specified record. **API Endpoint**: GET /v4/records/{recordId}/payments/{paymentId}   **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdPaymentsPaymentIdExample
    {
        public void main()
        {
            var apiInstance = new RecordsPaymentsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var paymentId = 789;  // long? | The ID of the payment to fetch.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Record Payment
                ResponsePaymentModelArray result = apiInstance.V4GetRecordsRecordIdPaymentsPaymentId(contentType, authorization, recordId, paymentId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsPaymentsApi.V4GetRecordsRecordIdPaymentsPaymentId: " + e.Message );
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
 **paymentId** | **long?**| The ID of the payment to fetch. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponsePaymentModelArray**](ResponsePaymentModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

