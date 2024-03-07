# AccelaRecords.Api.RecordsProfessionalsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4DeleteRecordsRecordIdProfessionalsIds**](RecordsProfessionalsApi.md#v4deleterecordsrecordidprofessionalsids) | **DELETE** /v4/Records/{recordId}/professionals/{ids} | Delete Record Professionals
[**V4GetRecordsRecordIdProfessionals**](RecordsProfessionalsApi.md#v4getrecordsrecordidprofessionals) | **GET** /v4/records/{recordId}/professionals | Get All Professionals for Record
[**V4PostRecordsRecordIdProfessionals**](RecordsProfessionalsApi.md#v4postrecordsrecordidprofessionals) | **POST** /v4/records/{recordId}/professionals | Create Record Professionals
[**V4PutRecordsRecordIdProfessionalsId**](RecordsProfessionalsApi.md#v4putrecordsrecordidprofessionalsid) | **PUT** /v4/records/{recordId}/professionals/{id} | Update Record Professional


<a name="v4deleterecordsrecordidprofessionalsids"></a>
# **V4DeleteRecordsRecordIdProfessionalsIds**
> ResponseResultModelArray V4DeleteRecordsRecordIdProfessionalsIds (string contentType, string authorization, string recordId, string ids, string lang = null)

Delete Record Professionals

Removes the association between the specified professional(s) and the specified record. **API Endpoint**:  DELETE /v4/records/{recordId}/professionals/{ids}  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4DeleteRecordsRecordIdProfessionalsIdsExample
    {
        public void main()
        {
            var apiInstance = new RecordsProfessionalsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var ids = ids_example;  // string | Comma-delimited IDs of the licensed professionals to be removed.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Delete Record Professionals
                ResponseResultModelArray result = apiInstance.V4DeleteRecordsRecordIdProfessionalsIds(contentType, authorization, recordId, ids, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsProfessionalsApi.V4DeleteRecordsRecordIdProfessionalsIds: " + e.Message );
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
 **ids** | **string**| Comma-delimited IDs of the licensed professionals to be removed. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getrecordsrecordidprofessionals"></a>
# **V4GetRecordsRecordIdProfessionals**
> ResponseLicenseProfessionalModelArray V4GetRecordsRecordIdProfessionals (string contentType, string authorization, string recordId, string fields = null, string lang = null)

Get All Professionals for Record

Gets the professionals for the specified record. **API Endpoint**:  GET /v4/records/{recordId}/professionals  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdProfessionalsExample
    {
        public void main()
        {
            var apiInstance = new RecordsProfessionalsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Professionals for Record
                ResponseLicenseProfessionalModelArray result = apiInstance.V4GetRecordsRecordIdProfessionals(contentType, authorization, recordId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsProfessionalsApi.V4GetRecordsRecordIdProfessionals: " + e.Message );
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
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseLicenseProfessionalModelArray**](ResponseLicenseProfessionalModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4postrecordsrecordidprofessionals"></a>
# **V4PostRecordsRecordIdProfessionals**
> ResponseResultModelArray V4PostRecordsRecordIdProfessionals (string contentType, string authorization, string recordId, List<LicenseProfessionalModel> body, string lang = null)

Create Record Professionals

Creates a new professional and associates the professional with the specified record. **API Endpoint**:  POST /v4/records/{recordId}/professionals  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4PostRecordsRecordIdProfessionalsExample
    {
        public void main()
        {
            var apiInstance = new RecordsProfessionalsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var body = new List<LicenseProfessionalModel>(); // List<LicenseProfessionalModel> | The licensed professional information to be added.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Create Record Professionals
                ResponseResultModelArray result = apiInstance.V4PostRecordsRecordIdProfessionals(contentType, authorization, recordId, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsProfessionalsApi.V4PostRecordsRecordIdProfessionals: " + e.Message );
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
 **body** | [**List&lt;LicenseProfessionalModel&gt;**](LicenseProfessionalModel.md)| The licensed professional information to be added. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4putrecordsrecordidprofessionalsid"></a>
# **V4PutRecordsRecordIdProfessionalsId**
> ResponseLicenseProfessionalModel V4PutRecordsRecordIdProfessionalsId (string contentType, string authorization, string recordId, string id, LicenseProfessionalModel body, string lang = null)

Update Record Professional

Updates information for the specified professional associated with the specified record. **API Endpoint**:  PUT /v4/records/{recordId}/professionals/{id}  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4PutRecordsRecordIdProfessionalsIdExample
    {
        public void main()
        {
            var apiInstance = new RecordsProfessionalsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var id = id_example;  // string | The ID of professional to update.
            var body = new LicenseProfessionalModel(); // LicenseProfessionalModel | The licensed professional information to be updated.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Record Professional
                ResponseLicenseProfessionalModel result = apiInstance.V4PutRecordsRecordIdProfessionalsId(contentType, authorization, recordId, id, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsProfessionalsApi.V4PutRecordsRecordIdProfessionalsId: " + e.Message );
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
 **id** | **string**| The ID of professional to update. | 
 **body** | [**LicenseProfessionalModel**](LicenseProfessionalModel.md)| The licensed professional information to be updated. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseLicenseProfessionalModel**](ResponseLicenseProfessionalModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

