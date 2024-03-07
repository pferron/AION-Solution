# AccelaDocuments.Api.DocumentReviewApi

All URIs are relative to *https://apis.accela.com/*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4DeleteDocumentReviewDocumentsDocumentIdCommentsCommentIds**](DocumentReviewApi.md#v4deletedocumentreviewdocumentsdocumentidcommentscommentids) | **DELETE** /v4/documentReview/documents/{documentId}/comments/{commentIds} | Delete Document Review Comments
[**V4GetDocumentReviewDocumentsDocumentIdCommentsCommentIds_**](DocumentReviewApi.md#v4getdocumentreviewdocumentsdocumentidcommentscommentids_) | **GET** /v4/documentReview/documents/{documentId}/comments/{commentIds} | Get Document Review Comments
[**V4PostDocumentReviewDocumentsDocumentIdComments**](DocumentReviewApi.md#v4postdocumentreviewdocumentsdocumentidcomments) | **POST** /v4/documentReview/documents/{documentId}/comments | Create Document Review Comments
[**V4PostDocumentReviewRecordsRecordIdDocuments**](DocumentReviewApi.md#v4postdocumentreviewrecordsrecordiddocuments) | **POST** /v4/documentReview/records/{recordId}/documents | Attach Document Report
[**V4PostDocumentReviewRecordsRecordIdDocumentsDocumentIdCheckin**](DocumentReviewApi.md#v4postdocumentreviewrecordsrecordiddocumentsdocumentidcheckin) | **POST** /v4/documentReview/records/{recordId}/documents/{documentId}/checkin | Checkin Document Review
[**V4PutDocumentReviewDocumentsDocumentIdCommentsCommentId**](DocumentReviewApi.md#v4putdocumentreviewdocumentsdocumentidcommentscommentid) | **PUT** /v4/DocumentReview/documents/{documentId}/comments/{commentId} | Update Document Review Comment
[**V4PutDocumentReviewDocumentsDocumentIdTasksId_**](DocumentReviewApi.md#v4putdocumentreviewdocumentsdocumentidtasksid_) | **PUT** /v4/documentReview/documents/{documentId}/tasks/{id} | Update Document Review Task Status

<a name="v4deletedocumentreviewdocumentsdocumentidcommentscommentids"></a>
# **V4DeleteDocumentReviewDocumentsDocumentIdCommentsCommentIds**
> ResponseResultModelArray V4DeleteDocumentReviewDocumentsDocumentIdCommentsCommentIds (string contentType, string authorization, long? documentId, string commentIds, string fields = null, string lang = null)

Delete Document Review Comments

Deletes the specified comments for the specified document. **API Endpoint**:  DELETE /v4/documentReview/documents/{documentId}/comments/{commentIds}  **Scope**:  documents  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3.5 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaDocuments.Api;
using AccelaDocuments.Client;
using AccelaDocuments.Model;

namespace Example
{
    public class V4DeleteDocumentReviewDocumentsDocumentIdCommentsCommentIdsExample
    {
        public void main()
        {
            var apiInstance = new DocumentReviewApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var documentId = 789;  // long? | The ID of document to review.
            var commentIds = commentIds_example;  // string | Comma-delimited comment ID's.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Delete Document Review Comments
                ResponseResultModelArray result = apiInstance.V4DeleteDocumentReviewDocumentsDocumentIdCommentsCommentIds(contentType, authorization, documentId, commentIds, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DocumentReviewApi.V4DeleteDocumentReviewDocumentsDocumentIdCommentsCommentIds: " + e.Message );
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
 **documentId** | **long?**| The ID of document to review. | 
 **commentIds** | **string**| Comma-delimited comment ID&#x27;s. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="v4getdocumentreviewdocumentsdocumentidcommentscommentids_"></a>
# **V4GetDocumentReviewDocumentsDocumentIdCommentsCommentIds_**
> ResponseDocumentCommentModel V4GetDocumentReviewDocumentsDocumentIdCommentsCommentIds_ (string contentType, string authorization, string documentId, string commentIds, string isActive, string fields = null, string lang = null)

Get Document Review Comments

Gets document comments specified by {commentIds}. **API Endpoint**:  GET /v4/documentReview/documents/{documentId}/comments/{commentIds} **Scope**:  documents  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3.5 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaDocuments.Api;
using AccelaDocuments.Client;
using AccelaDocuments.Model;

namespace Example
{
    public class V4GetDocumentReviewDocumentsDocumentIdCommentsCommentIds_Example
    {
        public void main()
        {
            var apiInstance = new DocumentReviewApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var documentId = documentId_example;  // string | The ID of document to fetch.
            var commentIds = commentIds_example;  // string | Comma-delimited comment ID's.
            var isActive = isActive_example;  // string | Filter whether the comment is active or inactive.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Document Review Comments
                ResponseDocumentCommentModel result = apiInstance.V4GetDocumentReviewDocumentsDocumentIdCommentsCommentIds_(contentType, authorization, documentId, commentIds, isActive, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DocumentReviewApi.V4GetDocumentReviewDocumentsDocumentIdCommentsCommentIds_: " + e.Message );
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
 **documentId** | **string**| The ID of document to fetch. | 
 **commentIds** | **string**| Comma-delimited comment ID&#x27;s. | 
 **isActive** | **string**| Filter whether the comment is active or inactive. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseDocumentCommentModel**](ResponseDocumentCommentModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="v4postdocumentreviewdocumentsdocumentidcomments"></a>
# **V4PostDocumentReviewDocumentsDocumentIdComments**
> ResponseResultModel V4PostDocumentReviewDocumentsDocumentIdComments (DocumentCommentModel body, string contentType, string authorization, long? documentId, string fields = null, string lang = null)

Create Document Review Comments

Adds a comment to the specified document. **API Endpoint**:  POST /v4/documentReview/documents/{documentId}/comments  **Scope**:  documents  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3.5 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaDocuments.Api;
using AccelaDocuments.Client;
using AccelaDocuments.Model;

namespace Example
{
    public class V4PostDocumentReviewDocumentsDocumentIdCommentsExample
    {
        public void main()
        {
            var apiInstance = new DocumentReviewApi();
            var body = new DocumentCommentModel(); // DocumentCommentModel | Document model for update
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var documentId = 789;  // long? | The ID of document to review.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Create Document Review Comments
                ResponseResultModel result = apiInstance.V4PostDocumentReviewDocumentsDocumentIdComments(body, contentType, authorization, documentId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DocumentReviewApi.V4PostDocumentReviewDocumentsDocumentIdComments: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**DocumentCommentModel**](DocumentCommentModel.md)| Document model for update | 
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | 
 **authorization** | **string**| Construct oAuth2 authentication token | 
 **documentId** | **long?**| The ID of document to review. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModel**](ResponseResultModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="v4postdocumentreviewrecordsrecordiddocuments"></a>
# **V4PostDocumentReviewRecordsRecordIdDocuments**
> ResponseResultModelArray V4PostDocumentReviewRecordsRecordIdDocuments (string contentType, string authorization, string recordId, byte[] uploadedFile = null, string fileInfo = null, string lang = null)

Attach Document Report

Attaches a document report to the specified record. A third party document review application can use the Attach Document Report to send a document report such as a correction notice to Civic Platform. When a correction notice report is generated on a third party document review tool, call Attach Document Report to send the document to Civic Platform as an attachment to a record.  To specify the document to be attached, use the HTTP headers 'Content-Type:multipart/form-data' and 'Content-Disposition:form-data'. For the form-data, use the name=\"uploadedFile\" parameter to specify the \"filename=\", and name=\"fileInfo\" to specify the file attributes. 'fileInfo' is an array of 'serviceProviderCode', 'fileName', 'type', and 'description' properties. For example:   Content-Disposition: form-data; name=\"uploadedFile\"; filename=\"summaryReport.pdf\"  Content-Disposition: form-data; name=\"fileInfo\"  [   {    \"serviceProviderCode\": \"BPTDEV\",    \"fileName\": \"summaryReport.pdf\",    \"type\": \"text/plain\",   \"description\": \"Upload a report with file info unit testing\"   }  ] **API Endpoint**:  POST /v4/documentReview/records/{recordId}/documents  **Scope**:  documents  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3.5 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaDocuments.Api;
using AccelaDocuments.Client;
using AccelaDocuments.Model;

namespace Example
{
    public class V4PostDocumentReviewRecordsRecordIdDocumentsExample
    {
        public void main()
        {
            var apiInstance = new DocumentReviewApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of record to fetch.
            var uploadedFile = uploadedFile_example;  // byte[] |  (optional) 
            var fileInfo = fileInfo_example;  // string |  (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Attach Document Report
                ResponseResultModelArray result = apiInstance.V4PostDocumentReviewRecordsRecordIdDocuments(contentType, authorization, recordId, uploadedFile, fileInfo, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DocumentReviewApi.V4PostDocumentReviewRecordsRecordIdDocuments: " + e.Message );
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
 **recordId** | **string**| The ID of record to fetch. | 
 **uploadedFile** | **byte[]****byte[]**|  | [optional] 
 **fileInfo** | **string**|  | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: multipart/form-data
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="v4postdocumentreviewrecordsrecordiddocumentsdocumentidcheckin"></a>
# **V4PostDocumentReviewRecordsRecordIdDocumentsDocumentIdCheckin**
> ResponseResultModelArray V4PostDocumentReviewRecordsRecordIdDocumentsDocumentIdCheckin (string contentType, string authorization, string documentId, string recordId, byte[] uploadedFile = null, string fileInfo = null, string lang = null)

Checkin Document Review

Checks in a file containing document review comments for the specified record. A third party document review application can use the Checkin Document Review API to check-in a reviewed document file to Civic Platform. When all reviews on a given document have been completed in the third party document review tool, call Checkin Document Review to send the document with all the open comments to Automation as a check-in file. It becomes an updated version of the original version which was submitted for review. To specify the document to be checked in, use the HTTP headers 'Content-Type:multipart/form-data' and 'Content-Disposition:form-data'. For the form-data, use the name=\"uploadedFile\" parameter to specify the \"filename=\", and name=\"fileInfo\" to specify the file attributes. 'fileInfo' is an array of 'serviceProviderCode', 'fileName', 'type', 'resubmit', and 'description' properties.  Set the \"resubmit\" property to \"true\" if the third-party application requires a document resubmittal. For example:  Content-Disposition: form-data; name=\"uploadedFile\"; filename=\"test.pdf\"   Content-Disposition: form-data; name=\"fileInfo\"  [   {    \"serviceProviderCode\": \"BPTDEV\",    \"fileName\": \"test.pdf\",    \"resubmit\": \"true\",    \"type\": \"text/plain\",    \"description\": \"Upload file with file info unit testing\"   }  ] **API Endpoint**:  POST /v4/documentReview/records/{recordId}/documents/{documentId}/checkin  **Scope**:  documents  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3.5 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaDocuments.Api;
using AccelaDocuments.Client;
using AccelaDocuments.Model;

namespace Example
{
    public class V4PostDocumentReviewRecordsRecordIdDocumentsDocumentIdCheckinExample
    {
        public void main()
        {
            var apiInstance = new DocumentReviewApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var documentId = documentId_example;  // string | The ID of document to fetch.
            var recordId = recordId_example;  // string | The id of the record to fetch.
            var uploadedFile = uploadedFile_example;  // byte[] |  (optional) 
            var fileInfo = fileInfo_example;  // string |  (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Checkin Document Review
                ResponseResultModelArray result = apiInstance.V4PostDocumentReviewRecordsRecordIdDocumentsDocumentIdCheckin(contentType, authorization, documentId, recordId, uploadedFile, fileInfo, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DocumentReviewApi.V4PostDocumentReviewRecordsRecordIdDocumentsDocumentIdCheckin: " + e.Message );
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
 **documentId** | **string**| The ID of document to fetch. | 
 **recordId** | **string**| The id of the record to fetch. | 
 **uploadedFile** | **byte[]****byte[]**|  | [optional] 
 **fileInfo** | **string**|  | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: multipart/form-data
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="v4putdocumentreviewdocumentsdocumentidcommentscommentid"></a>
# **V4PutDocumentReviewDocumentsDocumentIdCommentsCommentId**
> ResponseDocumentCommentUpdateModel V4PutDocumentReviewDocumentsDocumentIdCommentsCommentId (DocumentCommentUpdateModel body, string contentType, string authorization, long? documentId, long? commentId, string fields = null, string lang = null)

Update Document Review Comment

Updates the specified comment for the specified document. **API Endpoint**:  PUT /v4/documentReview/documents/{documentId}/comments/{commentId}  **Scope**:  documents  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3.5 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaDocuments.Api;
using AccelaDocuments.Client;
using AccelaDocuments.Model;

namespace Example
{
    public class V4PutDocumentReviewDocumentsDocumentIdCommentsCommentIdExample
    {
        public void main()
        {
            var apiInstance = new DocumentReviewApi();
            var body = new DocumentCommentUpdateModel(); // DocumentCommentUpdateModel | Document comment to update.
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var documentId = 789;  // long? | The ID of document to review.
            var commentId = 789;  // long? | The system id of the comment to fetch.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Document Review Comment
                ResponseDocumentCommentUpdateModel result = apiInstance.V4PutDocumentReviewDocumentsDocumentIdCommentsCommentId(body, contentType, authorization, documentId, commentId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DocumentReviewApi.V4PutDocumentReviewDocumentsDocumentIdCommentsCommentId: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**DocumentCommentUpdateModel**](DocumentCommentUpdateModel.md)| Document comment to update. | 
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | 
 **authorization** | **string**| Construct oAuth2 authentication token | 
 **documentId** | **long?**| The ID of document to review. | 
 **commentId** | **long?**| The system id of the comment to fetch. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseDocumentCommentUpdateModel**](ResponseDocumentCommentUpdateModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="v4putdocumentreviewdocumentsdocumentidtasksid_"></a>
# **V4PutDocumentReviewDocumentsDocumentIdTasksId_**
> ResponseDocumentAssociationModel V4PutDocumentReviewDocumentsDocumentIdTasksId_ (DocumentAssociationModel body, string contentType, string authorization, long? documentId, long? id, string fields = null, string lang = null)

Update Document Review Task Status

Updates the status of the specified task for the specified document. A third party document review application can use the Update Document Review Task to sync the document task status between the third party tool and Civic Platform. **API Endpoint**:  PUT /v4/documentReview/documents/{documentId}/tasks/{id}  **Scope**:  documents  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3.5 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaDocuments.Api;
using AccelaDocuments.Client;
using AccelaDocuments.Model;

namespace Example
{
    public class V4PutDocumentReviewDocumentsDocumentIdTasksId_Example
    {
        public void main()
        {
            var apiInstance = new DocumentReviewApi();
            var body = new DocumentAssociationModel(); // DocumentAssociationModel | Document attributes and status to update.
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var documentId = 789;  // long? | The ID of document to review.
            var id = 789;  // long? | The unique task identifier. Currently, a third-party document review tool supplies the task id that is stored in their system.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Document Review Task Status
                ResponseDocumentAssociationModel result = apiInstance.V4PutDocumentReviewDocumentsDocumentIdTasksId_(body, contentType, authorization, documentId, id, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DocumentReviewApi.V4PutDocumentReviewDocumentsDocumentIdTasksId_: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**DocumentAssociationModel**](DocumentAssociationModel.md)| Document attributes and status to update. | 
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | 
 **authorization** | **string**| Construct oAuth2 authentication token | 
 **documentId** | **long?**| The ID of document to review. | 
 **id** | **long?**| The unique task identifier. Currently, a third-party document review tool supplies the task id that is stored in their system. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseDocumentAssociationModel**](ResponseDocumentAssociationModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
