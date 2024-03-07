# AccelaAssetsAndAssessments.Api.AssetsRecordsApi

All URIs are relative to *https://apis.accela.com/*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetAssetsAssetIdRecords**](AssetsRecordsApi.md#v4getassetsassetidrecords) | **GET** /v4/assets/{assetId}/records | Get All Asset Records

<a name="v4getassetsassetidrecords"></a>
# **V4GetAssetsAssetIdRecords**
> ResponseSimpleRecordModelArray V4GetAssetsAssetIdRecords (string contentType, string authorization, string assetId, long? offset = null, long? limit = null, string fields = null, string lang = null)

Get All Asset Records

Returns all records for a given asset.    **API Endpoint**:  GET /v4/assets/{assetId}/records   **Scope**:  assets   **App Type**:  Agency   **Authorization Type**:  Access token   **Civic Platform version**: 9.0.0  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAssetsAndAssessments.Api;
using AccelaAssetsAndAssessments.Client;
using AccelaAssetsAndAssessments.Model;

namespace Example
{
    public class V4GetAssetsAssetIdRecordsExample
    {
        public void main()
        {
            var apiInstance = new AssetsRecordsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var assetId = assetId_example;  // string | The ID of the asset to fetch. See [Get All Assets](./api-assets-assessments.html#operation/v4.get.assets).
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Asset Records
                ResponseSimpleRecordModelArray result = apiInstance.V4GetAssetsAssetIdRecords(contentType, authorization, assetId, offset, limit, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AssetsRecordsApi.V4GetAssetsAssetIdRecords: " + e.Message );
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
 **assetId** | **string**| The ID of the asset to fetch. See [Get All Assets](./api-assets-assessments.html#operation/v4.get.assets). | 
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseSimpleRecordModelArray**](ResponseSimpleRecordModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
