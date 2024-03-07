# AccelaRecords.Api.RecordsCustomFormsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetRecordsRecordIdCustomForms**](RecordsCustomFormsApi.md#v4getrecordsrecordidcustomforms) | **GET** /v4/records/{recordId}/customForms | Get All Custom Forms for Record
[**V4GetRecordsRecordIdCustomFormsFormIdMeta**](RecordsCustomFormsApi.md#v4getrecordsrecordidcustomformsformidmeta) | **GET** /v4/records/{recordId}/customForms/{formId}/meta | Get Custom Form Metadata for Record
[**V4GetRecordsRecordIdCustomFormsMeta**](RecordsCustomFormsApi.md#v4getrecordsrecordidcustomformsmeta) | **GET** /v4/records/{recordId}/customForms/meta | Get All Custom Forms Metadata for Record
[**V4PutRecordsRecordIdCustomForms**](RecordsCustomFormsApi.md#v4putrecordsrecordidcustomforms) | **PUT** /v4/records/{recordId}/customForms | Update Record Custom Forms


<a name="v4getrecordsrecordidcustomforms"></a>
# **V4GetRecordsRecordIdCustomForms**
> ResponseCustomAttributeModelArray V4GetRecordsRecordIdCustomForms (string contentType, string recordId, string fields = null, string lang = null)

Get All Custom Forms for Record

Returns an array of custom forms associated with the specified record. Each custom form consists of custom field name-and-value pairs. **API Endpoint**:  GET /v4/records/{recordId}/customForms  **Scope**:  records  **App Type**:  All  **Authorization Type**:   No authorization required  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdCustomFormsExample
    {
        public void main()
        {
            var apiInstance = new RecordsCustomFormsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Custom Forms for Record
                ResponseCustomAttributeModelArray result = apiInstance.V4GetRecordsRecordIdCustomForms(contentType, recordId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsCustomFormsApi.V4GetRecordsRecordIdCustomForms: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | [default to application/x-www-form-urlencoded]
 **recordId** | **string**| The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine). | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseCustomAttributeModelArray**](ResponseCustomAttributeModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getrecordsrecordidcustomformsformidmeta"></a>
# **V4GetRecordsRecordIdCustomFormsFormIdMeta**
> ResponseCustomFormSubgroupModelArray V4GetRecordsRecordIdCustomFormsFormIdMeta (string contentType, string authorization, string recordId, string formId, string lang = null)

Get Custom Form Metadata for Record

Gets the detailed data associated with the specified custom form for the specified record. **API Endpoint**:  GET /v4/records/{recordId}/customForms/{formId}/meta  **Scope**:  records  **App Type**:  All  **Authorization Type**:  No authorization required  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdCustomFormsFormIdMetaExample
    {
        public void main()
        {
            var apiInstance = new RecordsCustomFormsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var formId = formId_example;  // string | The ID of the custom form to fetch.).
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Custom Form Metadata for Record
                ResponseCustomFormSubgroupModelArray result = apiInstance.V4GetRecordsRecordIdCustomFormsFormIdMeta(contentType, authorization, recordId, formId, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsCustomFormsApi.V4GetRecordsRecordIdCustomFormsFormIdMeta: " + e.Message );
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
 **formId** | **string**| The ID of the custom form to fetch.). | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseCustomFormSubgroupModelArray**](ResponseCustomFormSubgroupModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getrecordsrecordidcustomformsmeta"></a>
# **V4GetRecordsRecordIdCustomFormsMeta**
> ResponseCustomFormSubgroupModelArray V4GetRecordsRecordIdCustomFormsMeta (string contentType, string authorization, string recordId, string fields = null, string lang = null)

Get All Custom Forms Metadata for Record

Gets the detailed data associated with the custom forms for the specified record. **API Endpoint**:  GET /v4/records/{recordId}/customForms/meta  **Scope**:  records  **App Type**:  All  **Authorization Type**:  No authorization required  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdCustomFormsMetaExample
    {
        public void main()
        {
            var apiInstance = new RecordsCustomFormsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Custom Forms Metadata for Record
                ResponseCustomFormSubgroupModelArray result = apiInstance.V4GetRecordsRecordIdCustomFormsMeta(contentType, authorization, recordId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsCustomFormsApi.V4GetRecordsRecordIdCustomFormsMeta: " + e.Message );
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

[**ResponseCustomFormSubgroupModelArray**](ResponseCustomFormSubgroupModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4putrecordsrecordidcustomforms"></a>
# **V4PutRecordsRecordIdCustomForms**
> ResponseResultModelArray V4PutRecordsRecordIdCustomForms (string contentType, string authorization, string recordId, List<CustomAttributeModel> body, string lang = null)

Update Record Custom Forms

Updates the custom form for the specified record. **API Endpoint**:  PUT /v4/records/{recordId}/customForms   **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4PutRecordsRecordIdCustomFormsExample
    {
        public void main()
        {
            var apiInstance = new RecordsCustomFormsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var body = new List<CustomAttributeModel>(); // List<CustomAttributeModel> | Custom form informatio to be updated. Example: [{ \"field1\": \"field1Val\", \"field2\": \"field2Val\", \"_Id_\": \"Group&SubGroup\" }]
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Record Custom Forms
                ResponseResultModelArray result = apiInstance.V4PutRecordsRecordIdCustomForms(contentType, authorization, recordId, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsCustomFormsApi.V4PutRecordsRecordIdCustomForms: " + e.Message );
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
 **body** | [**List&lt;CustomAttributeModel&gt;**](CustomAttributeModel.md)| Custom form informatio to be updated. Example: [{ \&quot;field1\&quot;: \&quot;field1Val\&quot;, \&quot;field2\&quot;: \&quot;field2Val\&quot;, \&quot;_Id_\&quot;: \&quot;Group&amp;SubGroup\&quot; }] | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

