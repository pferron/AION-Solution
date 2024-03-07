# AccelaAssetsAndAssessments.Api.AssetsAssessmentsApi

All URIs are relative to *https://apis.accela.com/*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4PostAssetsIdAssessments**](AssetsAssessmentsApi.md#v4postassetsidassessments) | **POST** /v4/assets/{id}/assessments | Create Asset Assessment

<a name="v4postassetsidassessments"></a>
# **V4PostAssetsIdAssessments**
> ResponseResultModelArray V4PostAssetsIdAssessments (RequestAssessmentModel body, string contentType, string authorization, string id, string lang = null)

Create Asset Assessment

Creates condition assessments for a given asset.    **API Endpoint**:  POST /v4/assets/{id}/assessments   **Scope**:  assessments   **App Type**:  Agency   **Authorization Type**:  Access token   **Civic Platform version**: 9.0.0  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAssetsAndAssessments.Api;
using AccelaAssetsAndAssessments.Client;
using AccelaAssetsAndAssessments.Model;

namespace Example
{
    public class V4PostAssetsIdAssessmentsExample
    {
        public void main()
        {
            var apiInstance = new AssetsAssessmentsApi();
            var body = new RequestAssessmentModel(); // RequestAssessmentModel | Asset attributes to be added. Ex. [{ "field1": "field1Val", "field2": "field2Val"}]
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = id_example;  // string | The ID of the asset to fetch. See [Get All Assets](./api-assets-assessments.html#operation/v4.get.assets).
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Create Asset Assessment
                ResponseResultModelArray result = apiInstance.V4PostAssetsIdAssessments(body, contentType, authorization, id, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AssetsAssessmentsApi.V4PostAssetsIdAssessments: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**RequestAssessmentModel**](RequestAssessmentModel.md)| Asset attributes to be added. Ex. [{ &quot;field1&quot;: &quot;field1Val&quot;, &quot;field2&quot;: &quot;field2Val&quot;}] | 
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | 
 **authorization** | **string**| Construct oAuth2 authentication token | 
 **id** | **string**| The ID of the asset to fetch. See [Get All Assets](./api-assets-assessments.html#operation/v4.get.assets). | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
