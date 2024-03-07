# AccelaRecords.Api.RecordsInspectionsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetRecordsRecordIdInspections**](RecordsInspectionsApi.md#v4getrecordsrecordidinspections) | **GET** /v4/records/{recordId}/inspections | Get All Inspections for Record
[**V4GetRecordsRecordIdsInspectionTypes**](RecordsInspectionsApi.md#v4getrecordsrecordidsinspectiontypes) | **GET** /v4/records/{recordIds}/inspectionTypes | Get All Inspection Types for Record


<a name="v4getrecordsrecordidinspections"></a>
# **V4GetRecordsRecordIdInspections**
> ResponseInspectionModelArray V4GetRecordsRecordIdInspections (string contentType, string authorization, string recordId, string fields = null, long? offset = null, long? limit = null, string lang = null)

Get All Inspections for Record

Gets the scheduled inspections for the specified record.   Note: For a citizen token, the Display in ACA setting of the given {recordId} determines whether or not an inspection is returned. If Display in ACA is enabled for the given {recordId}, the inspection is included in the response; otherwise, the inspection will not be included. For an agency token, the Display in ACA setting of the given {recordId} is ignored. **API Endpoint**:  GET /v4/records/{recordId}/inspections   **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdInspectionsExample
    {
        public void main()
        {
            var apiInstance = new RecordsInspectionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Inspections for Record
                ResponseInspectionModelArray result = apiInstance.V4GetRecordsRecordIdInspections(contentType, authorization, recordId, fields, offset, limit, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsInspectionsApi.V4GetRecordsRecordIdInspections: " + e.Message );
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
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseInspectionModelArray**](ResponseInspectionModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getrecordsrecordidsinspectiontypes"></a>
# **V4GetRecordsRecordIdsInspectionTypes**
> ResponseRecordInspectionTypeModelArray V4GetRecordsRecordIdsInspectionTypes (string contentType, string authorization, string recordIds, string fields = null, string lang = null)

Get All Inspection Types for Record

Gets the inspection types associated with the specified record(s). **API Endpoint**:  GET /v4/records/{recordIds}/inspectionTypes  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdsInspectionTypesExample
    {
        public void main()
        {
            var apiInstance = new RecordsInspectionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordIds = recordIds_example;  // string | Comma-delimited IDs of the records to fetch.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Inspection Types for Record
                ResponseRecordInspectionTypeModelArray result = apiInstance.V4GetRecordsRecordIdsInspectionTypes(contentType, authorization, recordIds, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsInspectionsApi.V4GetRecordsRecordIdsInspectionTypes: " + e.Message );
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
 **recordIds** | **string**| Comma-delimited IDs of the records to fetch. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseRecordInspectionTypeModelArray**](ResponseRecordInspectionTypeModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

