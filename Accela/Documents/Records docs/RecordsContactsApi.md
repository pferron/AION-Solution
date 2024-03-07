# AccelaRecords.Api.RecordsContactsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4DeleteRecordsRecordIdContactsContactIdAddressesIds**](RecordsContactsApi.md#v4deleterecordsrecordidcontactscontactidaddressesids) | **DELETE** /v4/Records/{recordId}/contacts/{contactId}/addresses/{ids} | Delete Record Contact Addresses
[**V4DeleteRecordsRecordIdContactsIds**](RecordsContactsApi.md#v4deleterecordsrecordidcontactsids) | **DELETE** /v4/Records/{recordId}/contacts/{ids} | Delete Record Contacts
[**V4GetRecordsRecordIdContacts**](RecordsContactsApi.md#v4getrecordsrecordidcontacts) | **GET** /v4/records/{recordId}/contacts | Get All Contacts for Record
[**V4GetRecordsRecordIdContactsContactIdAddresses**](RecordsContactsApi.md#v4getrecordsrecordidcontactscontactidaddresses) | **GET** /v4/records/{recordId}/contacts/{contactId}/addresses | Get All Addresses for Contact
[**V4PostRecordsRecordIdContacts**](RecordsContactsApi.md#v4postrecordsrecordidcontacts) | **POST** /v4/records/{recordId}/contacts | Create Record Contacts
[**V4PostRecordsRecordIdContactsContactIdAddresses**](RecordsContactsApi.md#v4postrecordsrecordidcontactscontactidaddresses) | **POST** /v4/records/{recordId}/contacts/{contactId}/addresses | Create Record Contact Addresses
[**V4PutRecordsRecordIdContactsContactIdAddressesId**](RecordsContactsApi.md#v4putrecordsrecordidcontactscontactidaddressesid) | **PUT** /v4/records/{recordId}/contacts/{contactId}/addresses/{id} | Update Record Contact Address
[**V4PutRecordsRecordIdContactsId**](RecordsContactsApi.md#v4putrecordsrecordidcontactsid) | **PUT** /v4/records/{recordId}/contacts/{id} | Update Record Contact


<a name="v4deleterecordsrecordidcontactscontactidaddressesids"></a>
# **V4DeleteRecordsRecordIdContactsContactIdAddressesIds**
> ResponseResultModelArray V4DeleteRecordsRecordIdContactsContactIdAddressesIds (string contentType, string authorization, string recordId, long? contactId, string ids, string lang = null)

Delete Record Contact Addresses

Deletes the specified addresses from the specified contacts and specified records. **API Endpoint**:  DELETE /v4/records/{recordId}/contacts/{contactId}/addresses/{ids}  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4DeleteRecordsRecordIdContactsContactIdAddressesIdsExample
    {
        public void main()
        {
            var apiInstance = new RecordsContactsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var contactId = 789;  // long? | The ID of the contact to fetch. See [Get All Contacts](./api-contacts-professionals.html#operation/v4.get.contacts).
            var ids = ids_example;  // string | Comma-delimited IDs of the addresses to be deleted.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Delete Record Contact Addresses
                ResponseResultModelArray result = apiInstance.V4DeleteRecordsRecordIdContactsContactIdAddressesIds(contentType, authorization, recordId, contactId, ids, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsContactsApi.V4DeleteRecordsRecordIdContactsContactIdAddressesIds: " + e.Message );
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
 **contactId** | **long?**| The ID of the contact to fetch. See [Get All Contacts](./api-contacts-professionals.html#operation/v4.get.contacts). | 
 **ids** | **string**| Comma-delimited IDs of the addresses to be deleted. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4deleterecordsrecordidcontactsids"></a>
# **V4DeleteRecordsRecordIdContactsIds**
> ResponseResultModelArray V4DeleteRecordsRecordIdContactsIds (string contentType, string authorization, string recordId, string ids, string fields = null, string lang = null)

Delete Record Contacts

Removes the association of specified contacts from a specified record. **API Endpoint**:  DELETE /v4/records/{recordId}/contacts/{ids}  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4DeleteRecordsRecordIdContactsIdsExample
    {
        public void main()
        {
            var apiInstance = new RecordsContactsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var ids = ids_example;  // string | Comma-delimited IDs of the contacts to remove.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Delete Record Contacts
                ResponseResultModelArray result = apiInstance.V4DeleteRecordsRecordIdContactsIds(contentType, authorization, recordId, ids, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsContactsApi.V4DeleteRecordsRecordIdContactsIds: " + e.Message );
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
 **ids** | **string**| Comma-delimited IDs of the contacts to remove. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getrecordsrecordidcontacts"></a>
# **V4GetRecordsRecordIdContacts**
> ResponseRecordContactSimpleModelArray V4GetRecordsRecordIdContacts (string authorization, string recordId, string fields = null, string lang = null)

Get All Contacts for Record

Gets contacts associated to a record. **API Endpoint**:  GET /v4/records/{recordId}/contacts  **Scope**:  addresses  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdContactsExample
    {
        public void main()
        {
            var apiInstance = new RecordsContactsApi();
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Contacts for Record
                ResponseRecordContactSimpleModelArray result = apiInstance.V4GetRecordsRecordIdContacts(authorization, recordId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsContactsApi.V4GetRecordsRecordIdContacts: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **authorization** | **string**| Construct oAuth2 authentication token | 
 **recordId** | **string**| The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine). | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseRecordContactSimpleModelArray**](ResponseRecordContactSimpleModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getrecordsrecordidcontactscontactidaddresses"></a>
# **V4GetRecordsRecordIdContactsContactIdAddresses**
> ResponseContactAddressArray V4GetRecordsRecordIdContactsContactIdAddresses (string contentType, string authorization, string recordId, long? contactId, string lang = null)

Get All Addresses for Contact

Gets the addresses for the specified contacts and specified records. **API Endpoint**:  GET /v4/records/{recordId}/contacts/{contactId}/addresses  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdContactsContactIdAddressesExample
    {
        public void main()
        {
            var apiInstance = new RecordsContactsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var contactId = 789;  // long? | The ID of the contact to fetch. See [Get All Contacts](./api-contacts-professionals.html#operation/v4.get.contacts).
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Addresses for Contact
                ResponseContactAddressArray result = apiInstance.V4GetRecordsRecordIdContactsContactIdAddresses(contentType, authorization, recordId, contactId, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsContactsApi.V4GetRecordsRecordIdContactsContactIdAddresses: " + e.Message );
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
 **contactId** | **long?**| The ID of the contact to fetch. See [Get All Contacts](./api-contacts-professionals.html#operation/v4.get.contacts). | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseContactAddressArray**](ResponseContactAddressArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4postrecordsrecordidcontacts"></a>
# **V4PostRecordsRecordIdContacts**
> ResponseResultModelArray V4PostRecordsRecordIdContacts (string contentType, string authorization, string recordId, List<RecordContactSimpleModel> body, string fields = null, string lang = null)

Create Record Contacts

Creates new contact(s) for the specified record. **API Endpoint**:  POST /v4/records/{recordId}/contacts  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4PostRecordsRecordIdContactsExample
    {
        public void main()
        {
            var apiInstance = new RecordsContactsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var body = new List<RecordContactSimpleModel>(); // List<RecordContactSimpleModel> | The contact information to be added.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Create Record Contacts
                ResponseResultModelArray result = apiInstance.V4PostRecordsRecordIdContacts(contentType, authorization, recordId, body, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsContactsApi.V4PostRecordsRecordIdContacts: " + e.Message );
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
 **body** | [**List&lt;RecordContactSimpleModel&gt;**](RecordContactSimpleModel.md)| The contact information to be added. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4postrecordsrecordidcontactscontactidaddresses"></a>
# **V4PostRecordsRecordIdContactsContactIdAddresses**
> ResponseResultModelArray V4PostRecordsRecordIdContactsContactIdAddresses (string contentType, string authorization, string recordId, long? contactId, List<ContactAddress> body = null, string lang = null)

Create Record Contact Addresses

Creates addresses for the specified contact for the specified record. **API Endpoint**:  POST /v4/records/{recordId}/contacts/{contactId}/addresses  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4PostRecordsRecordIdContactsContactIdAddressesExample
    {
        public void main()
        {
            var apiInstance = new RecordsContactsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var contactId = 789;  // long? | The ID of the contact to fetch. See [Get All Contacts](./api-contacts-professionals.html#operation/v4.get.contacts).
            var body = new List<ContactAddress>(); // List<ContactAddress> | The address information to be added. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Create Record Contact Addresses
                ResponseResultModelArray result = apiInstance.V4PostRecordsRecordIdContactsContactIdAddresses(contentType, authorization, recordId, contactId, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsContactsApi.V4PostRecordsRecordIdContactsContactIdAddresses: " + e.Message );
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
 **contactId** | **long?**| The ID of the contact to fetch. See [Get All Contacts](./api-contacts-professionals.html#operation/v4.get.contacts). | 
 **body** | [**List&lt;ContactAddress&gt;**](ContactAddress.md)| The address information to be added. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4putrecordsrecordidcontactscontactidaddressesid"></a>
# **V4PutRecordsRecordIdContactsContactIdAddressesId**
> ContactAddress V4PutRecordsRecordIdContactsContactIdAddressesId (string contentType, string authorization, string recordId, long? contactId, long? id, ContactAddress body = null, string lang = null)

Update Record Contact Address

Updates the specified address for the specified contact and specified record. **API Endpoint**:  PUT /v4/records/{recordId}/contacts/{contactId}/addresses/{id}  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4PutRecordsRecordIdContactsContactIdAddressesIdExample
    {
        public void main()
        {
            var apiInstance = new RecordsContactsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var contactId = 789;  // long? | The ID of the contact to fetch. See [Get All Contacts](./api-contacts-professionals.html#operation/v4.get.contacts).
            var id = 789;  // long? | The ID of the address to be updated.
            var body = new ContactAddress(); // ContactAddress | The address information to be updated. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Record Contact Address
                ContactAddress result = apiInstance.V4PutRecordsRecordIdContactsContactIdAddressesId(contentType, authorization, recordId, contactId, id, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsContactsApi.V4PutRecordsRecordIdContactsContactIdAddressesId: " + e.Message );
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
 **contactId** | **long?**| The ID of the contact to fetch. See [Get All Contacts](./api-contacts-professionals.html#operation/v4.get.contacts). | 
 **id** | **long?**| The ID of the address to be updated. | 
 **body** | [**ContactAddress**](ContactAddress.md)| The address information to be updated. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ContactAddress**](ContactAddress.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4putrecordsrecordidcontactsid"></a>
# **V4PutRecordsRecordIdContactsId**
> ResponseRecordContactSimpleModelArray V4PutRecordsRecordIdContactsId (string contentType, string authorization, string recordId, RecordContactSimpleModel body, string id, string fields = null, string lang = null)

Update Record Contact

Updates information for a specified contact associated with a specified record. **API Endpoint**:  PUT /v4/records/{recordId}/contacts/{id}  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4PutRecordsRecordIdContactsIdExample
    {
        public void main()
        {
            var apiInstance = new RecordsContactsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var body = new RecordContactSimpleModel(); // RecordContactSimpleModel | The contact information to be updated.
            var id = id_example;  // string | The ID of the contact to update.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Record Contact
                ResponseRecordContactSimpleModelArray result = apiInstance.V4PutRecordsRecordIdContactsId(contentType, authorization, recordId, body, id, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsContactsApi.V4PutRecordsRecordIdContactsId: " + e.Message );
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
 **body** | [**RecordContactSimpleModel**](RecordContactSimpleModel.md)| The contact information to be updated. | 
 **id** | **string**| The ID of the contact to update. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseRecordContactSimpleModelArray**](ResponseRecordContactSimpleModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

