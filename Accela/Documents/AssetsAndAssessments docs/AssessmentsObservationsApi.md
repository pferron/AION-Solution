# AccelaAssetsAndAssessments.Api.AssessmentsObservationsApi

All URIs are relative to *https://apis.accela.com/*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetAssessmentsIdObservations**](AssessmentsObservationsApi.md#v4getassessmentsidobservations) | **GET** /v4/assessments/{id}/observations | Get Condition Assessment Observations
[**V4GetAssessmentsIdObservationsMeta**](AssessmentsObservationsApi.md#v4getassessmentsidobservationsmeta) | **GET** /v4/assessments/{id}/observations/meta | Get Observations Metadata for Condition Assessment Type
[**V4PutAssessmentsIdObservations**](AssessmentsObservationsApi.md#v4putassessmentsidobservations) | **PUT** /v4/assessments/{id}/observations | Update Assessment Observations

<a name="v4getassessmentsidobservations"></a>
# **V4GetAssessmentsIdObservations**
> ResponseCustomAttributeModelArray V4GetAssessmentsIdObservations (string contentType, string authorization, string id, string lang = null)

Get Condition Assessment Observations

Returns observation data associated to a given condition assessment.    **API Endpoint**:  GET /v4/assessments/{id}/observations   **Scope**:  assessments   **App Type**:  Agency   **Authorization Type**:  Access token   **Civic Platform version**: 9.0.0  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAssetsAndAssessments.Api;
using AccelaAssetsAndAssessments.Client;
using AccelaAssetsAndAssessments.Model;

namespace Example
{
    public class V4GetAssessmentsIdObservationsExample
    {
        public void main()
        {
            var apiInstance = new AssessmentsObservationsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = id_example;  // string | The ID of the condition assessment to fetch.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Condition Assessment Observations
                ResponseCustomAttributeModelArray result = apiInstance.V4GetAssessmentsIdObservations(contentType, authorization, id, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AssessmentsObservationsApi.V4GetAssessmentsIdObservations: " + e.Message );
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
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseCustomAttributeModelArray**](ResponseCustomAttributeModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="v4getassessmentsidobservationsmeta"></a>
# **V4GetAssessmentsIdObservationsMeta**
> ResponseAttributeMetadataModelArray V4GetAssessmentsIdObservationsMeta (string contentType, string authorization, string id, long? limit = null, long? offset = null, string fields = null, string lang = null)

Get Observations Metadata for Condition Assessment Type

Returns the custom observations metadata of the condition assessment type. The observations metadata is configured for the condition assessment type in Civic Platform Administration.    **API Endpoint**:  GET /v4/assessments/{id}/observations/meta   **Scope**:  assessments   **App Type**:  Agency   **Authorization Type**:  Access token   **Civic Platform version**: 9.0.0  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAssetsAndAssessments.Api;
using AccelaAssetsAndAssessments.Client;
using AccelaAssetsAndAssessments.Model;

namespace Example
{
    public class V4GetAssessmentsIdObservationsMetaExample
    {
        public void main()
        {
            var apiInstance = new AssessmentsObservationsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = id_example;  // string | The ID of the condition assessment to fetch.
            var limit = 789;  // long? | Search result size limit. (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Observations Metadata for Condition Assessment Type
                ResponseAttributeMetadataModelArray result = apiInstance.V4GetAssessmentsIdObservationsMeta(contentType, authorization, id, limit, offset, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AssessmentsObservationsApi.V4GetAssessmentsIdObservationsMeta: " + e.Message );
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
 **limit** | **long?**| Search result size limit. | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
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
<a name="v4putassessmentsidobservations"></a>
# **V4PutAssessmentsIdObservations**
> ResponseResultModelArray V4PutAssessmentsIdObservations (List<CustomAttributeModel> body, string contentType, string authorization, string id, string lang = null)

Update Assessment Observations

Creates, updates, and deletes observations for a given condition assessment. Use the {action} field in the request array item to specify whether the observation is to be created, updated, or deleted. To create or update an observation, specify the observation custom fields in the fields{} request array item. To determine the observation custom fields for the given assessment:   1.   For the requested assessment {id}, get the assessment type.value from [Get Condition Assessment](./api-assets-assessments.html#operation/v4.get.assessments.id).   2. For the assessment type.value, get the custom field metadata identified by {attributeName} from [Get Observations Metadata for Condition Assessment Type](./api-assets-assessments.html#operation/v4.get.assessments.id.observations.meta).    **API Endpoint**:  PUT /v4/assessments/{id}/observations   **Scope**:  assessments   **App Type**:  Agency   **Authorization Type**:  Access token   **Civic Platform version**: 9.0.0  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAssetsAndAssessments.Api;
using AccelaAssetsAndAssessments.Client;
using AccelaAssetsAndAssessments.Model;

namespace Example
{
    public class V4PutAssessmentsIdObservationsExample
    {
        public void main()
        {
            var apiInstance = new AssessmentsObservationsApi();
            var body = new List<CustomAttributeModel>(); // List<CustomAttributeModel> | Observation information to be updated.
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = id_example;  // string | The ID of the observation to update
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Assessment Observations
                ResponseResultModelArray result = apiInstance.V4PutAssessmentsIdObservations(body, contentType, authorization, id, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AssessmentsObservationsApi.V4PutAssessmentsIdObservations: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**List&lt;CustomAttributeModel&gt;**](CustomAttributeModel.md)| Observation information to be updated. | 
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | 
 **authorization** | **string**| Construct oAuth2 authentication token | 
 **id** | **string**| The ID of the observation to update | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
