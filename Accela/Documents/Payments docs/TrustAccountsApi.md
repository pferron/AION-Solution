# AccelaPayments.Api.TrustAccountsApi

All URIs are relative to *https://apis.accela.com/*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetTrustAccounts**](TrustAccountsApi.md#v4gettrustaccounts) | **GET** /v4/trustAccounts | Get All Trust Accounts

<a name="v4gettrustaccounts"></a>
# **V4GetTrustAccounts**
> ResponseTrustAccountModelArray V4GetTrustAccounts (string contentType, string authorization, long? offset = null, long? limit = null, string fields = null, string lang = null)

Get All Trust Accounts

Gets all trust accounts. **API Endpoint**:  GET /v4/trustAccounts  **Scope**:  trustaccounts  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3.4 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaPayments.Api;
using AccelaPayments.Client;
using AccelaPayments.Model;

namespace Example
{
    public class V4GetTrustAccountsExample
    {
        public void main()
        {
            var apiInstance = new TrustAccountsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Trust Accounts
                ResponseTrustAccountModelArray result = apiInstance.V4GetTrustAccounts(contentType, authorization, offset, limit, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling TrustAccountsApi.V4GetTrustAccounts: " + e.Message );
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
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseTrustAccountModelArray**](ResponseTrustAccountModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)