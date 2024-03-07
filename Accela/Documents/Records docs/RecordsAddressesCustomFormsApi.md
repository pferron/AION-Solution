# AccelaRecords.Api.RecordsAddressesCustomFormsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetRecordsRecordIdAddressesAddressIdCustomForms**](RecordsAddressesCustomFormsApi.md#v4getrecordsrecordidaddressesaddressidcustomforms) | **GET** /v4/records/{recordId}/addresses/{addressId}/customForms | Get Record Address Custom Forms
[**V4GetRecordsRecordIdAddressesAddressIdCustomFormsMeta**](RecordsAddressesCustomFormsApi.md#v4getrecordsrecordidaddressesaddressidcustomformsmeta) | **GET** /v4/records/{recordId}/addresses/{addressId}/customForms/meta | Get Record Address Custom Forms Metadata


<a name="v4getrecordsrecordidaddressesaddressidcustomforms"></a>
# **V4GetRecordsRecordIdAddressesAddressIdCustomForms**
> ResponseCustomAttributeModelArray V4GetRecordsRecordIdAddressesAddressIdCustomForms (string contentType, string authorization, string recordId, long? addressId, string fields = null, string lang = null)

Get Record Address Custom Forms

Returns an array of custom form data associated with a given record address. **API Endpoint**:  GET /v4/records/{recordId}/addresses/{addressId}/customForms  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 9.2.0 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdAddressesAddressIdCustomFormsExample
    {
        public void main()
        {
            var apiInstance = new RecordsAddressesCustomFormsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to be fetched
            var addressId = 789;  // long? | The ID of the address to be fetched
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note that field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US (optional) 

            try
            {
                // Get Record Address Custom Forms
                ResponseCustomAttributeModelArray result = apiInstance.V4GetRecordsRecordIdAddressesAddressIdCustomForms(contentType, authorization, recordId, addressId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsAddressesCustomFormsApi.V4GetRecordsRecordIdAddressesAddressIdCustomForms: " + e.Message );
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
 **recordId** | **string**| The ID of the record to be fetched | 
 **addressId** | **long?**| The ID of the address to be fetched | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note that field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US | [optional] 

### Return type

[**ResponseCustomAttributeModelArray**](ResponseCustomAttributeModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getrecordsrecordidaddressesaddressidcustomformsmeta"></a>
# **V4GetRecordsRecordIdAddressesAddressIdCustomFormsMeta**
> ResponseApoCustomFormsMetadata V4GetRecordsRecordIdAddressesAddressIdCustomFormsMeta (string contentType, string authorization, string recordId, long? addressId, string fields = null, string lang = null)

Get Record Address Custom Forms Metadata

Returns the field metadata for all custom forms associated with a given record address. **API Endpoint**:  GET /v4/records/{recordId}/addresses/{addressId}/customForms/meta  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 9.2.0 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdAddressesAddressIdCustomFormsMetaExample
    {
        public void main()
        {
            var apiInstance = new RecordsAddressesCustomFormsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | ID of record to be fetched
            var addressId = 789;  // long? | ID of record address to be fetched
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note that field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US (optional) 

            try
            {
                // Get Record Address Custom Forms Metadata
                ResponseApoCustomFormsMetadata result = apiInstance.V4GetRecordsRecordIdAddressesAddressIdCustomFormsMeta(contentType, authorization, recordId, addressId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsAddressesCustomFormsApi.V4GetRecordsRecordIdAddressesAddressIdCustomFormsMeta: " + e.Message );
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
 **recordId** | **string**| ID of record to be fetched | 
 **addressId** | **long?**| ID of record address to be fetched | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note that field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US | [optional] 

### Return type

[**ResponseApoCustomFormsMetadata**](ResponseApoCustomFormsMetadata.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

