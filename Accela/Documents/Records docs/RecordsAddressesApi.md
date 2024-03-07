# AccelaRecords.Api.RecordsAddressesApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4DeleteRecordsRecordIdAddressesIds**](RecordsAddressesApi.md#v4deleterecordsrecordidaddressesids) | **DELETE** /v4/Records/{recordId}/addresses/{ids} | Delete Record Addresses
[**V4GetRecordsRecordIdAddresses**](RecordsAddressesApi.md#v4getrecordsrecordidaddresses) | **GET** /v4/records/{recordId}/addresses | Get All Record Addresses
[**V4PostRecordsRecordIdAddresses**](RecordsAddressesApi.md#v4postrecordsrecordidaddresses) | **POST** /v4/records/{recordId}/addresses | Create Record Addresses
[**V4PutRecordsRecordIdAddressesId**](RecordsAddressesApi.md#v4putrecordsrecordidaddressesid) | **PUT** /v4/records/{recordId}/addresses/{id} | Update Record Address


<a name="v4deleterecordsrecordidaddressesids"></a>
# **V4DeleteRecordsRecordIdAddressesIds**
> ResponseResultModelArray V4DeleteRecordsRecordIdAddressesIds (string contentType, string authorization, string recordId, string ids, string lang = null)

Delete Record Addresses

Deletes addresses from the specified record. **API Endpoint**:  DELETE /v4/records/{recordId}/addresses/{idS}  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4DeleteRecordsRecordIdAddressesIdsExample
    {
        public void main()
        {
            var apiInstance = new RecordsAddressesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var ids = ids_example;  // string | Comma-delimited IDs of of the addresses to be deleted.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Delete Record Addresses
                ResponseResultModelArray result = apiInstance.V4DeleteRecordsRecordIdAddressesIds(contentType, authorization, recordId, ids, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsAddressesApi.V4DeleteRecordsRecordIdAddressesIds: " + e.Message );
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
 **ids** | **string**| Comma-delimited IDs of of the addresses to be deleted. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getrecordsrecordidaddresses"></a>
# **V4GetRecordsRecordIdAddresses**
> List<RecordAddressModel> V4GetRecordsRecordIdAddresses (string contentType, string authorization, string recordId, string isPrimary = null, string fields = null, string lang = null)

Get All Record Addresses

Gets the addresses linked to the specified record.  **API Endpoint**:  GET /v4/records/{recordId}/addresses  **Scope**:  records  **App Type**:  All  **Authorization Type**:  No auth required  **Civic Platform version**: 7.3.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdAddressesExample
    {
        public void main()
        {
            var apiInstance = new RecordsAddressesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var isPrimary = isPrimary_example;  // string | Filter by the primary address flag. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Record Addresses
                List&lt;RecordAddressModel&gt; result = apiInstance.V4GetRecordsRecordIdAddresses(contentType, authorization, recordId, isPrimary, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsAddressesApi.V4GetRecordsRecordIdAddresses: " + e.Message );
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
 **isPrimary** | **string**| Filter by the primary address flag. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**List<RecordAddressModel>**](RecordAddressModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4postrecordsrecordidaddresses"></a>
# **V4PostRecordsRecordIdAddresses**
> ResponseResultModelArray V4PostRecordsRecordIdAddresses (string contentType, string authorization, string recordId, List<RecordAddressModel> body = null, string lang = null)

Create Record Addresses

Creates new address(es) for the specified record. **API Endpoint**:  POST /v4/records/{recordId}/addresses  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4PostRecordsRecordIdAddressesExample
    {
        public void main()
        {
            var apiInstance = new RecordsAddressesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var body = new List<RecordAddressModel>(); // List<RecordAddressModel> | Record address request information to be created. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Create Record Addresses
                ResponseResultModelArray result = apiInstance.V4PostRecordsRecordIdAddresses(contentType, authorization, recordId, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsAddressesApi.V4PostRecordsRecordIdAddresses: " + e.Message );
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
 **body** | [**List&lt;RecordAddressModel&gt;**](RecordAddressModel.md)| Record address request information to be created. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4putrecordsrecordidaddressesid"></a>
# **V4PutRecordsRecordIdAddressesId**
> AddressModel V4PutRecordsRecordIdAddressesId (string contentType, string authorization, string recordId, long? id, RequestRecordAddressModel body, string lang = null)

Update Record Address

Updates the address for the specified record. **API Endpoint**:  PUT /v4/records/{recordId}/addresses/{id}  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4PutRecordsRecordIdAddressesIdExample
    {
        public void main()
        {
            var apiInstance = new RecordsAddressesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var id = 789;  // long? | The ID of the address to update.
            var body = new RequestRecordAddressModel(); // RequestRecordAddressModel | Address information to be updated.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Record Address
                AddressModel result = apiInstance.V4PutRecordsRecordIdAddressesId(contentType, authorization, recordId, id, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsAddressesApi.V4PutRecordsRecordIdAddressesId: " + e.Message );
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
 **id** | **long?**| The ID of the address to update. | 
 **body** | [**RequestRecordAddressModel**](RequestRecordAddressModel.md)| Address information to be updated. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**AddressModel**](AddressModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

