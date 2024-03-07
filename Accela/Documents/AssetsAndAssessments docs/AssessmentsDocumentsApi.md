# AccelaAssetsAndAssessments.Api.AssessmentsDocumentsApi

All URIs are relative to *https://apis.accela.com/*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4DeleteAssessmentsAssessmentIdDocumentsDocumentIds**](AssessmentsDocumentsApi.md#v4deleteassessmentsassessmentiddocumentsdocumentids) | **DELETE** /v4/assessments/{assessmentId}/documents/{documentIds} | Delete Assessment Documents
[**V4GetAssessmentsAssessmentIdDocuments**](AssessmentsDocumentsApi.md#v4getassessmentsassessmentiddocuments) | **GET** /v4/assessments/{assessmentId}/documents | Get All Assessment Documents
[**V4PostAssessmentsAssessmentIdDocuments**](AssessmentsDocumentsApi.md#v4postassessmentsassessmentiddocuments) | **POST** /v4/assessments/{assessmentId}/documents | Create Assessment Documents

<a name="v4deleteassessmentsassessmentiddocumentsdocumentids"></a>
# **V4DeleteAssessmentsAssessmentIdDocumentsDocumentIds**
> ResponseResultModelArray V4DeleteAssessmentsAssessmentIdDocumentsDocumentIds (string contentType, string authorization, string assessmentId, string documentIds, string userId = null, string password = null, string lang = null)

Delete Assessment Documents

Deletes one or more documents for the given condition assessment.    **API Endpoint**:  DELETE /v4/assessments/{assessmentId}/documents/{documentIds}   **Scope**:  assessments   **App Type**:  Agency   **Authorization Type**:  Access token   **Civic Platform version**: 9.0.0  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAssetsAndAssessments.Api;
using AccelaAssetsAndAssessments.Client;
using AccelaAssetsAndAssessments.Model;

namespace Example
{
    public class V4DeleteAssessmentsAssessmentIdDocumentsDocumentIdsExample
    {
        public void main()
        {
            var apiInstance = new AssessmentsDocumentsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var assessmentId = assessmentId_example;  // string | The ID of the assessment to fetch.
            var documentIds = documentIds_example;  // string | Comma-delimited IDs of the documents to be deleted.
            var userId = userId_example;  // string | The standard EDMS adapter userid. It's required for user level authentication (optional) 
            var password = password_example;  // string | The standard EDMS adapter password. It's required for user level authentication (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Delete Assessment Documents
                ResponseResultModelArray result = apiInstance.V4DeleteAssessmentsAssessmentIdDocumentsDocumentIds(contentType, authorization, assessmentId, documentIds, userId, password, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AssessmentsDocumentsApi.V4DeleteAssessmentsAssessmentIdDocumentsDocumentIds: " + e.Message );
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
 **assessmentId** | **string**| The ID of the assessment to fetch. | 
 **documentIds** | **string**| Comma-delimited IDs of the documents to be deleted. | 
 **userId** | **string**| The standard EDMS adapter userid. It&#x27;s required for user level authentication | [optional] 
 **password** | **string**| The standard EDMS adapter password. It&#x27;s required for user level authentication | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="v4getassessmentsassessmentiddocuments"></a>
# **V4GetAssessmentsAssessmentIdDocuments**
> ResponseDocumentModelArray V4GetAssessmentsAssessmentIdDocuments (string contentType, string authorization, string assessmentId, long? limit = null, long? offset = null, string fields = null, string lang = null)

Get All Assessment Documents

Returns the documents for a given condition assessment.    **API Endpoint**:  GET /v4/assessments/{assessmentId}/documents   **Scope**:  assessments   **App Type**:  Agency   **Authorization Type**:  Access token   **Civic Platform version**: 9.0.0  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAssetsAndAssessments.Api;
using AccelaAssetsAndAssessments.Client;
using AccelaAssetsAndAssessments.Model;

namespace Example
{
    public class V4GetAssessmentsAssessmentIdDocumentsExample
    {
        public void main()
        {
            var apiInstance = new AssessmentsDocumentsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var assessmentId = assessmentId_example;  // string | The ID of the assessment to fetch.
            var limit = 789;  // long? | Search result size limit. (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Assessment Documents
                ResponseDocumentModelArray result = apiInstance.V4GetAssessmentsAssessmentIdDocuments(contentType, authorization, assessmentId, limit, offset, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AssessmentsDocumentsApi.V4GetAssessmentsAssessmentIdDocuments: " + e.Message );
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
 **assessmentId** | **string**| The ID of the assessment to fetch. | 
 **limit** | **long?**| Search result size limit. | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseDocumentModelArray**](ResponseDocumentModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="v4postassessmentsassessmentiddocuments"></a>
# **V4PostAssessmentsAssessmentIdDocuments**
> ResponseResultModelArray V4PostAssessmentsAssessmentIdDocuments (string contentType, string authorization, string assessmentId, byte[] uploadedFile = null, string fileInfo = null, string group = null, string category = null, string userId = null, string password = null, string lang = null)

Create Assessment Documents

Creates one or more document attachments for the given condition assessment. To specify the documents to be attached, use the HTTP header \"Content-Type:multipart/form-data\" and form-data for \"uploadedFile\" and \"fileInfo\". Note that the \"fileInfo\" is a string containing an array of file attributes. Use \"fileInfo\" to specify one or more documents to be attached. For example:   Content - Disposition: form - data;name = \"uploadedFile\"; filename=\"summaryReport.pdf\"   Content - Disposition: form - data;name = \"fileInfo\"   [    {    \"serviceProviderCode\": \"BPTDEV\",    \"fileName\": \"CXA12-pipe.png\",    \"type\": \"image/png\",    \"description\": \"Condition assessment on pipe\"    }  ]        **API Endpoint**:  POST /v4/assessments/{assessmentId}/documents   **Scope**:  assessments   **App Type**:  Agency   **Authorization Type**:  Access token   **Civic Platform version**: 9.0.0  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAssetsAndAssessments.Api;
using AccelaAssetsAndAssessments.Client;
using AccelaAssetsAndAssessments.Model;

namespace Example
{
    public class V4PostAssessmentsAssessmentIdDocumentsExample
    {
        public void main()
        {
            var apiInstance = new AssessmentsDocumentsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var assessmentId = assessmentId_example;  // string | The ID of the assessment to fetch.
            var uploadedFile = uploadedFile_example;  // byte[] |  (optional) 
            var fileInfo = fileInfo_example;  // string |  (optional) 
            var group = group_example;  // string | The document group. (optional) 
            var category = category_example;  // string | The document category. (optional) 
            var userId = userId_example;  // string | The standard EDMS Adapter userid. It's required for user level authentication (optional) 
            var password = password_example;  // string | The standard EMDS Adapter password. It's required for user level authentication (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Create Assessment Documents
                ResponseResultModelArray result = apiInstance.V4PostAssessmentsAssessmentIdDocuments(contentType, authorization, assessmentId, uploadedFile, fileInfo, group, category, userId, password, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AssessmentsDocumentsApi.V4PostAssessmentsAssessmentIdDocuments: " + e.Message );
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
 **assessmentId** | **string**| The ID of the assessment to fetch. | 
 **uploadedFile** | **byte[]****byte[]**|  | [optional] 
 **fileInfo** | **string**|  | [optional] 
 **group** | **string**| The document group. | [optional] 
 **category** | **string**| The document category. | [optional] 
 **userId** | **string**| The standard EDMS Adapter userid. It&#x27;s required for user level authentication | [optional] 
 **password** | **string**| The standard EMDS Adapter password. It&#x27;s required for user level authentication | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: multipart/form-data
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
