# AccelaRecords.Api.RecordsTrustAccountsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetRecordsRecordIdTrustAccounts**](RecordsTrustAccountsApi.md#v4getrecordsrecordidtrustaccounts) | **GET** /v4/records/{recordId}/trustAccounts | Get All Trust Accounts for Record


<a name="v4getrecordsrecordidtrustaccounts"></a>
# **V4GetRecordsRecordIdTrustAccounts**
> ResponseTrustAccountModelArray V4GetRecordsRecordIdTrustAccounts (string contentType, string authorization, string recordId, long? offset = null, long? limit = null, string fields = null, string lang = null)

Get All Trust Accounts for Record

Gets all trust accounts for the specified record. **API Endpoint**:  GET /v4/records/{recordId}/trustAccounts  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3.4 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdTrustAccountsExample
    {
        public void main()
        {
            var apiInstance = new RecordsTrustAccountsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Trust Accounts for Record
                ResponseTrustAccountModelArray result = apiInstance.V4GetRecordsRecordIdTrustAccounts(contentType, authorization, recordId, offset, limit, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsTrustAccountsApi.V4GetRecordsRecordIdTrustAccounts: " + e.Message );
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
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

