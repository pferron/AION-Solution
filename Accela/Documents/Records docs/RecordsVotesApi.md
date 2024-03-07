# AccelaRecords.Api.RecordsVotesApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetRecordsRecordIdVotes**](RecordsVotesApi.md#v4getrecordsrecordidvotes) | **GET** /v4/records/{recordId}/votes | Get All Votes for Record
[**V4GetRecordsRecordIdVotesSummary**](RecordsVotesApi.md#v4getrecordsrecordidvotessummary) | **GET** /v4/records/{recordId}/votes/summary | Get Record Votes Summary
[**V4PostRecordsRecordIdVotes**](RecordsVotesApi.md#v4postrecordsrecordidvotes) | **POST** /v4/records/{recordId}/votes | Create Record Votes


<a name="v4getrecordsrecordidvotes"></a>
# **V4GetRecordsRecordIdVotes**
> ResponseVoteResult V4GetRecordsRecordIdVotes (string contentType, string authorization, string recordId, string lang = null)

Get All Votes for Record

Gets the votes for the specified record. **API Endpoint**:  GET /v4/records/{recordId}/votes  **Scope**:  records  **App Type**:  Citizen  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdVotesExample
    {
        public void main()
        {
            var apiInstance = new RecordsVotesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Votes for Record
                ResponseVoteResult result = apiInstance.V4GetRecordsRecordIdVotes(contentType, authorization, recordId, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsVotesApi.V4GetRecordsRecordIdVotes: " + e.Message );
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
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseVoteResult**](ResponseVoteResult.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getrecordsrecordidvotessummary"></a>
# **V4GetRecordsRecordIdVotesSummary**
> ResponseVoteSummary V4GetRecordsRecordIdVotesSummary (string contentType, string authorization, string recordId, string lang = null)

Get Record Votes Summary

Gets the voting summary for the specified record. **API Endpoint**:  GET /v4/records/{recordId}/votes/summary  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdVotesSummaryExample
    {
        public void main()
        {
            var apiInstance = new RecordsVotesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Record Votes Summary
                ResponseVoteSummary result = apiInstance.V4GetRecordsRecordIdVotesSummary(contentType, authorization, recordId, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsVotesApi.V4GetRecordsRecordIdVotesSummary: " + e.Message );
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
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseVoteSummary**](ResponseVoteSummary.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4postrecordsrecordidvotes"></a>
# **V4PostRecordsRecordIdVotes**
> ResponseVoteResult V4PostRecordsRecordIdVotes (string contentType, string authorization, string recordId, VoteRequest voteRequest, string lang = null)

Create Record Votes

Creates a vote for the specified record. **API Endpoint**:  POST /v4/records/{recordId}/votes  **Scope**:  records  **App Type**:  Citizen  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4PostRecordsRecordIdVotesExample
    {
        public void main()
        {
            var apiInstance = new RecordsVotesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var voteRequest = new VoteRequest(); // VoteRequest | The vote request to add.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Create Record Votes
                ResponseVoteResult result = apiInstance.V4PostRecordsRecordIdVotes(contentType, authorization, recordId, voteRequest, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsVotesApi.V4PostRecordsRecordIdVotes: " + e.Message );
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
 **voteRequest** | [**VoteRequest**](VoteRequest.md)| The vote request to add. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseVoteResult**](ResponseVoteResult.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

