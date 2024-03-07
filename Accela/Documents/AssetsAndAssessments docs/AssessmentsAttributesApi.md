# AccelaAssetsAndAssessments.Api.AssessmentsAttributesApi

All URIs are relative to *https://apis.accela.com/*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetAssessmentsIdAttributesMeta**](AssessmentsAttributesApi.md#v4getassessmentsidattributesmeta) | **GET** /v4/assessments/{id}/attributes/meta | Get Attributes Metadata for Condition Assessment Type

<a name="v4getassessmentsidattributesmeta"></a>
# **V4GetAssessmentsIdAttributesMeta**
> ResponseAttributeMetadataModelArray V4GetAssessmentsIdAttributesMeta (string contentType, string authorization, string id, string fields = null, string lang = null)

Get Attributes Metadata for Condition Assessment Type

Returns the custom attributes metadata of the condition assessment type. The attributes metadata is configured for the condition assessment type in Civic Platform Administration.    **API Endpoint**:  GET /v4/assessments/{id}/attributes/meta   **Scope**:  assessments   **App Type**:  Agency   **Authorization Type**:  Access token   **Civic Platform version**: 9.0.0  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAssetsAndAssessments.Api;
using AccelaAssetsAndAssessments.Client;
using AccelaAssetsAndAssessments.Model;

namespace Example
{
    public class V4GetAssessmentsIdAttributesMetaExample
    {
        public void main()
        {
            var apiInstance = new AssessmentsAttributesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = id_example;  // string | The ID of the condition assessment to fetch.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Attributes Metadata for Condition Assessment Type
                ResponseAttributeMetadataModelArray result = apiInstance.V4GetAssessmentsIdAttributesMeta(contentType, authorization, id, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AssessmentsAttributesApi.V4GetAssessmentsIdAttributesMeta: " + e.Message );
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
 **id** | **string**| The ID of the condition assessment to fetch. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseAttributeMetadataModelArray**](ResponseAttributeMetadataModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
