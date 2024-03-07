# AccelaPayments.Api.PaymentsApi

All URIs are relative to *https://apis.accela.com/*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4PostPayments**](PaymentsApi.md#v4postpayments) | **POST** /v4/payments | Create Payment
[**V4PostPaymentsInitialize**](PaymentsApi.md#v4postpaymentsinitialize) | **POST** /v4/payments/initialize | Initialize Payment
[**V4PutPaymentsId**](PaymentsApi.md#v4putpaymentsid) | **PUT** /v4/payments/{id} | Commit Payment
[**V4PutPaymentsPaymentIdVoid**](PaymentsApi.md#v4putpaymentspaymentidvoid) | **PUT** /v4/payments/{paymentId}/void | Void Payment

<a name="v4postpayments"></a>
# **V4PostPayments**
> ResponsePaymentResponseModel V4PostPayments (string contentType, string authorization, PaymentRequestModel body = null, string lang = null)

Create Payment

Creates a payment for the cashier. **API Endpoint**:  POST /v4/payments  **Scope**:  payments  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaPayments.Api;
using AccelaPayments.Client;
using AccelaPayments.Model;

namespace Example
{
    public class V4PostPaymentsExample
    {
        public void main()
        {
            var apiInstance = new PaymentsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var body = new PaymentRequestModel(); // PaymentRequestModel | Payment request information. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Create Payment
                ResponsePaymentResponseModel result = apiInstance.V4PostPayments(contentType, authorization, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PaymentsApi.V4PostPayments: " + e.Message );
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
 **body** | [**PaymentRequestModel**](PaymentRequestModel.md)| Payment request information. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponsePaymentResponseModel**](ResponsePaymentResponseModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="v4postpaymentsinitialize"></a>
# **V4PostPaymentsInitialize**
> ResponsePaymentTransactionModel V4PostPaymentsInitialize (PaymentInitializeModel body, string contentType, string authorization, string fields = null, string lang = null)

Initialize Payment

Initializes citizen payment information for processing by a third party payment system that will send and commit final payment information into Automation. Call Initialize Payment to get the transaction ID required to call Commit Payment. **API Endpoint**:  POST /v4/payments/initialize  **Scope**:  payments  **App Type**:  Citizen  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3.5 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaPayments.Api;
using AccelaPayments.Client;
using AccelaPayments.Model;

namespace Example
{
    public class V4PostPaymentsInitializeExample
    {
        public void main()
        {
            var apiInstance = new PaymentsApi();
            var body = new PaymentInitializeModel(); // PaymentInitializeModel | Payment initialization request information.
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Initialize Payment
                ResponsePaymentTransactionModel result = apiInstance.V4PostPaymentsInitialize(body, contentType, authorization, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PaymentsApi.V4PostPaymentsInitialize: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**PaymentInitializeModel**](PaymentInitializeModel.md)| Payment initialization request information. | 
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | 
 **authorization** | **string**| Construct oAuth2 authentication token | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponsePaymentTransactionModel**](ResponsePaymentTransactionModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="v4putpaymentsid"></a>
# **V4PutPaymentsId**
> ResponsePaymentCommitResultModel V4PutPaymentsId (string contentType, string authorization, string id, PaymentCommitModel body = null, string fields = null, string lang = null)

Commit Payment

Processes and commits citizen payment information for the specified payment transaction ID. The Commit Payment API allows a third-party payment vendor to send payment information to Automation. The Commit Payment API processes the payment information, logs merchant account audit information, triggers the EMSE events ConvertToRealCAPBefore and PaymentReceiveBefore, creates a transaction record, triggers the EMSE event ConvertToRealCAPAfter, finishes the Automation payment, triggers the EMSE event PaymentReceiveAfter, and appoves the transaction. Note: An agency Construct administrator controls which apps can call Commit Payment. By default, Commit Payment is disabled. To allow an app to call Commit Payment, an agency administrator must go to the [Construct Admin Portal](https://admin.accela.com) > Agencies > {Agency} > Apps, and enable the Payment Enabled property for the app. **API Endpoint**:  PUT /v4/payments/{id}  **Scope**:  payments  **App Type**:  Citizen  **Authorization Type**:  Access token  **Civic Platform version**:  7.3.3.4 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaPayments.Api;
using AccelaPayments.Client;
using AccelaPayments.Model;

namespace Example
{
    public class V4PutPaymentsIdExample
    {
        public void main()
        {
            var apiInstance = new PaymentsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = id_example;  // string | The system id of the record payment to void.
            var body = new PaymentCommitModel(); // PaymentCommitModel | The payment information to commit. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Commit Payment
                ResponsePaymentCommitResultModel result = apiInstance.V4PutPaymentsId(contentType, authorization, id, body, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PaymentsApi.V4PutPaymentsId: " + e.Message );
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
 **id** | **string**| The system id of the record payment to void. | 
 **body** | [**PaymentCommitModel**](PaymentCommitModel.md)| The payment information to commit. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponsePaymentCommitResultModel**](ResponsePaymentCommitResultModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="v4putpaymentspaymentidvoid"></a>
# **V4PutPaymentsPaymentIdVoid**
> ResponseResultModel V4PutPaymentsPaymentIdVoid (PaymentVoidModel body, string contentType, string authorization, string paymentId, string lang = null)

Void Payment

Voids a given payment. When you void a payment, Civic Platform decreases the total payment amount and then increases the balance owed, as if you never made the payment. **API Endpoint**:  PUT /v4/payments/{paymentId}/void  **Scope**:  payments  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 9.0.0 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaPayments.Api;
using AccelaPayments.Client;
using AccelaPayments.Model;

namespace Example
{
    public class V4PutPaymentsPaymentIdVoidExample
    {
        public void main()
        {
            var apiInstance = new PaymentsApi();
            var body = new PaymentVoidModel(); // PaymentVoidModel | Payment information to void.
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var paymentId = paymentId_example;  // string | The id of the payment to void. See [Get All Payments for Record](./api-records.html#operation/v4.get.records.recordId.payments)
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Void Payment
                ResponseResultModel result = apiInstance.V4PutPaymentsPaymentIdVoid(body, contentType, authorization, paymentId, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PaymentsApi.V4PutPaymentsPaymentIdVoid: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**PaymentVoidModel**](PaymentVoidModel.md)| Payment information to void. | 
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | 
 **authorization** | **string**| Construct oAuth2 authentication token | 
 **paymentId** | **string**| The id of the payment to void. See [Get All Payments for Record](./api-records.html#operation/v4.get.records.recordId.payments) | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModel**](ResponseResultModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
