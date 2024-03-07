# AccelaContactsAndProfessionals.Api.ContactsRecordsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetContactsIdRecords**](ContactsRecordsApi.md#v4getcontactsidrecords) | **GET** /v4/contacts/{id}/records | Get All Records for Contact


<a name="v4getcontactsidrecords"></a>
# **V4GetContactsIdRecords**
> ResponseSimpleRecordModelArray V4GetContactsIdRecords (string contentType, string authorization, string id, string types = null, string statuses = null, long? offset = null, long? limit = null, string fields = null, string lang = null)

Get All Records for Contact

Gets the records for the specified contact {id}. **API Endpoint**:  GET /v4/contacts/{id}/records  **Scope**:  contacts  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 8.0 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaContactsAndProfessionals.Api;
using AccelaContactsAndProfessionals.Client;
using AccelaContactsAndProfessionals.Model;

namespace Example
{
    public class V4GetContactsIdRecordsExample
    {
        public void main()
        {
            var apiInstance = new ContactsRecordsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = id_example;  // string | The ID of the contact to fetch.
            var types = types_example;  // string | Filter by comma-delimited object types such as a list of record types or inspection types. For example: \"group/type/subType/category,group/type/subType/category\".  See [Get All Inspction Types](./api-settings.html#operation/v4.get.settings.inspections.types), [Get All Record Types](./api-settings.html#operation/v4.get.settings.records.types). (optional) 
            var statuses = statuses_example;  // string | Filter by comma-delimited record statuses. (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Records for Contact
                ResponseSimpleRecordModelArray result = apiInstance.V4GetContactsIdRecords(contentType, authorization, id, types, statuses, offset, limit, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ContactsRecordsApi.V4GetContactsIdRecords: " + e.Message );
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
 **id** | **string**| The ID of the contact to fetch. | 
 **types** | **string**| Filter by comma-delimited object types such as a list of record types or inspection types. For example: \&quot;group/type/subType/category,group/type/subType/category\&quot;.  See [Get All Inspction Types](./api-settings.html#operation/v4.get.settings.inspections.types), [Get All Record Types](./api-settings.html#operation/v4.get.settings.records.types). | [optional] 
 **statuses** | **string**| Filter by comma-delimited record statuses. | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseSimpleRecordModelArray**](ResponseSimpleRecordModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

