# AccelaMiscellanous.Api.BatchApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4PostBatch**](BatchApi.md#v4postbatch) | **POST** /v4/batch | Batch Request


<a name="v4postbatch"></a>
# **V4PostBatch**
> ResponseBatchResponseArray V4PostBatch (string contentType, string authorization, BatchRequestModel batchRequest)

Batch Request

Invokes multiple operations on data related to multiple agencies in a single HTTP request. The Batch Request API accepts an array of up to 25 HTTP requests, each with its required headers, body, method, and URL. The individual operations are invoked via HTTPS. The response is an array containing the results from all requested operations in the same order used in the request array. **API Endpoint**:  POST /v4/batch  **Scope**:  batch_request  **App Type**:  All  **Authorization Type**:  No authorization required  **Civic Platform version**: All 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaMiscellanous.Api;
using AccelaMiscellanous.Client;
using AccelaMiscellanous.Model;

namespace Example
{
    public class V4PostBatchExample
    {
        public void main()
        {
            var apiInstance = new BatchApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var batchRequest = new BatchRequestModel(); // BatchRequestModel | The batch request information.

            try
            {
                // Batch Request
                ResponseBatchResponseArray result = apiInstance.V4PostBatch(contentType, authorization, batchRequest);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling BatchApi.V4PostBatch: " + e.Message );
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
 **batchRequest** | [**BatchRequestModel**](BatchRequestModel.md)| The batch request information. | 

### Return type

[**ResponseBatchResponseArray**](ResponseBatchResponseArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

