# AccelaAssetsAndAssessments.Api.AssessmentsWorkOrdersApi

All URIs are relative to *https://apis.accela.com/*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetAssessmentsCaIdRecords**](AssessmentsWorkOrdersApi.md#v4getassessmentscaidrecords) | **GET** /v4/assessments/{caId}/records | Get All Records for an Assessment
[**V4PostAssessmentsIdWorkorders**](AssessmentsWorkOrdersApi.md#v4postassessmentsidworkorders) | **POST** /v4/assessments/{id}/workorders | Add Work Orders to Condition Assessments

<a name="v4getassessmentscaidrecords"></a>
# **V4GetAssessmentsCaIdRecords**
> ResponseSimpleRecordModelArray V4GetAssessmentsCaIdRecords (string contentType, string authorization, string caId, string type = null, string status = null, long? limit = null, long? offset = null, string fields = null, string lang = null)

Get All Records for an Assessment

Returns the work order records associated to a given condition assessment.    **API Endpoint**:  GET /v4/assessments/{caId}/records   **Scope**:  assessments   **App Type**:  Agency   **Authorization Type**:  Access token   **Civic Platform version**: 9.0.0  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAssetsAndAssessments.Api;
using AccelaAssetsAndAssessments.Client;
using AccelaAssetsAndAssessments.Model;

namespace Example
{
    public class V4GetAssessmentsCaIdRecordsExample
    {
        public void main()
        {
            var apiInstance = new AssessmentsWorkOrdersApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var caId = caId_example;  // string | The ID of the condition assessment to fetch.
            var type = type_example;  // string | Filter by record type. See [Get All Record Types](./api-settings.html#operation/v4.get.settings.records.types). (optional) 
            var status = status_example;  // string | Filter by record status. See [Get All Record Type Statuses](./api-settings.html#operation/v4.get.settings.records.types.id.statuses). (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Records for an Assessment
                ResponseSimpleRecordModelArray result = apiInstance.V4GetAssessmentsCaIdRecords(contentType, authorization, caId, type, status, limit, offset, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AssessmentsWorkOrdersApi.V4GetAssessmentsCaIdRecords: " + e.Message );
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
 **caId** | **string**| The ID of the condition assessment to fetch. | 
 **type** | **string**| Filter by record type. See [Get All Record Types](./api-settings.html#operation/v4.get.settings.records.types). | [optional] 
 **status** | **string**| Filter by record status. See [Get All Record Type Statuses](./api-settings.html#operation/v4.get.settings.records.types.id.statuses). | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
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
<a name="v4postassessmentsidworkorders"></a>
# **V4PostAssessmentsIdWorkorders**
> ResponseResultModelArray V4PostAssessmentsIdWorkorders (List<string> body, string contentType, string authorization, string id, string lang = null)

Add Work Orders to Condition Assessments

Adds or links work orders to a given condition assessment.    **API Endpoint**:  POST /v4/assessments/{id}/workorders   **Scope**:  assessments   **App Type**:  Agency   **Authorization Type**:  Access token   **Civic Platform version**: 9.0.0  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAssetsAndAssessments.Api;
using AccelaAssetsAndAssessments.Client;
using AccelaAssetsAndAssessments.Model;

namespace Example
{
    public class V4PostAssessmentsIdWorkordersExample
    {
        public void main()
        {
            var apiInstance = new AssessmentsWorkOrdersApi();
            var body = new List<string>(); // List<string> | An array of work order record IDs to be linked to the assessment. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = id_example;  // string | The ID of the condition assessment to fetch.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Add Work Orders to Condition Assessments
                ResponseResultModelArray result = apiInstance.V4PostAssessmentsIdWorkorders(body, contentType, authorization, id, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AssessmentsWorkOrdersApi.V4PostAssessmentsIdWorkorders: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**List&lt;string&gt;**](string.md)| An array of work order record IDs to be linked to the assessment. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine). | 
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | 
 **authorization** | **string**| Construct oAuth2 authentication token | 
 **id** | **string**| The ID of the condition assessment to fetch. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
