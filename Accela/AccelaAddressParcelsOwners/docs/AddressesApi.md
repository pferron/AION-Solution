# AccelaAddressParcelsOwners.Api.AddressesApi

All URIs are relative to *http://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetAddresses**](AddressesApi.md#v4getaddresses) | **GET** /v4/addresses | Get All Addresses
[**V4GetAddressesId**](AddressesApi.md#v4getaddressesid) | **GET** /v4/addresses/{id} | Get Address
[**V4GetAddressesIdConditions**](AddressesApi.md#v4getaddressesidconditions) | **GET** /v4/addresses/{id}/conditions | Get All Address Conditions
[**V4GetAddressesIdParcels**](AddressesApi.md#v4getaddressesidparcels) | **GET** /v4/addresses/{id}/parcels | Get All Address Parcels
[**V4GetAddressesIdRecords**](AddressesApi.md#v4getaddressesidrecords) | **GET** /v4/addresses/{id}/records | Get All Address Records
[**V4PutAddressesId**](AddressesApi.md#v4putaddressesid) | **PUT** /v4/addresses/{id} | Update an Address


<a name="v4getaddresses"></a>
# **V4GetAddresses**
> ResponseAddressModelArray V4GetAddresses (string contentType, string xAccelaAppid, long? id = null, string type = null, string isPrimary = null, string streetName = null, long? streetStart = null, long? streetEnd = null, string direction = null, string streetSuffixDirection = null, string streetSuffix = null, string city = null, string country = null, string state = null, string expand = null, long? offset = null, long? limit = null, string fields = null, string lang = null)

Get All Addresses

Gets the reference addresses available in the system. Specify at least one filter criteria.    **API Endpoint**:  GET /v4/addresses   **Scope**:  addresses   **App Type**:  All   **Authorization Type**:  No authorization required   **Civic Platform version**: 7.3.2  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAddressParcelsOwners.Api;
using AccelaAddressParcelsOwners.Client;
using AccelaAddressParcelsOwners.Model;

namespace Example
{
    public class V4GetAddressesExample
    {
        public void main()
        {
            var apiInstance = new AddressesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var xAccelaAppid = xAccelaAppid_example;  // string | clientid
            var id = 789;  // long? | The unique address id assigned by the Civic Platform server. (optional) 
            var type = type_example;  // string | The address type. (optional) 
            var isPrimary = isPrimary_example;  // string | Indicates whether or not to designate the address as the primary address. Only one address can be primary at any given time. (optional) 
            var streetName = streetName_example;  // string | The street name for the address. (optional) 
            var streetStart = 789;  // long? | The start of a range of street numbers. (optional) 
            var streetEnd = 789;  // long? | RefAddress streetEnd (optional) 
            var direction = direction_example;  // string | The street direction of the primary address associated with the application. (optional) 
            var streetSuffixDirection = streetSuffixDirection_example;  // string | The street direction of the primary address associated with the application. (optional) 
            var streetSuffix = streetSuffix_example;  // string | The street direction of the primary address associated with the application. (optional) 
            var city = city_example;  // string | The name of the city. (optional) 
            var country = country_example;  // string | The name of the country. (optional) 
            var state = state_example;  // string | The state corresponding to the address on record. (optional) 
            var expand = expand_example;  // string | Related objects to be returned with the response. The related object(s) will be returned if data exists; if data does not exist, the requested object(s) will not be included in the response.   Added in Civic Platform version: 9.3.0 (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array.For example,  if offset is 100,the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var fields = fields_example;  // string | Comma - delimited names of fields to be returned in the response.Note - Field names are case -sensitive and only first - level fields are supported.Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N.Default language is en_US. (optional) 

            try
            {
                // Get All Addresses
                ResponseAddressModelArray result = apiInstance.V4GetAddresses(contentType, xAccelaAppid, id, type, isPrimary, streetName, streetStart, streetEnd, direction, streetSuffixDirection, streetSuffix, city, country, state, expand, offset, limit, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AddressesApi.V4GetAddresses: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | 
 **xAccelaAppid** | **string**| clientid | 
 **id** | **long?**| The unique address id assigned by the Civic Platform server. | [optional] 
 **type** | **string**| The address type. | [optional] 
 **isPrimary** | **string**| Indicates whether or not to designate the address as the primary address. Only one address can be primary at any given time. | [optional] 
 **streetName** | **string**| The street name for the address. | [optional] 
 **streetStart** | **long?**| The start of a range of street numbers. | [optional] 
 **streetEnd** | **long?**| RefAddress streetEnd | [optional] 
 **direction** | **string**| The street direction of the primary address associated with the application. | [optional] 
 **streetSuffixDirection** | **string**| The street direction of the primary address associated with the application. | [optional] 
 **streetSuffix** | **string**| The street direction of the primary address associated with the application. | [optional] 
 **city** | **string**| The name of the city. | [optional] 
 **country** | **string**| The name of the country. | [optional] 
 **state** | **string**| The state corresponding to the address on record. | [optional] 
 **expand** | **string**| Related objects to be returned with the response. The related object(s) will be returned if data exists; if data does not exist, the requested object(s) will not be included in the response.   Added in Civic Platform version: 9.3.0 | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array.For example,  if offset is 100,the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 
 **fields** | **string**| Comma - delimited names of fields to be returned in the response.Note - Field names are case -sensitive and only first - level fields are supported.Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N.Default language is en_US. | [optional] 

### Return type

[**ResponseAddressModelArray**](ResponseAddressModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getaddressesid"></a>
# **V4GetAddressesId**
> ResponseAddressModelWithCustomForms V4GetAddressesId (string contentType, string xAccelaAppid, long? id, string expand = null, string fields = null, string lang = null)

Get Address

Gets detailed information for the specified address.    **API Endpoint**:  GET /v4/addresses/{id}   **Scope**:  addresses   **App Type**:  All   **Authorization Type**:  No authorization required   **Civic Platform version**: 7.3.2  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAddressParcelsOwners.Api;
using AccelaAddressParcelsOwners.Client;
using AccelaAddressParcelsOwners.Model;

namespace Example
{
    public class V4GetAddressesIdExample
    {
        public void main()
        {
            var apiInstance = new AddressesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var xAccelaAppid = xAccelaAppid_example;  // string | clientid
            var id = 789;  // long? | The system id of the address to fetch.
            var expand = expand_example;  // string | Related objects to be returned with the response. The related object(s) will be returned if data exists; if data does not exist, the requested object(s) will not be included in the response.   Added in Civic Platform version: 9.3.0  (optional) 
            var fields = fields_example;  // string | Comma - delimited names of fields to be returned in the response.Note - Field names are case -sensitive and only first - level fields are supported.Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N.Default language is en_US. (optional) 

            try
            {
                // Get Address
                ResponseAddressModelWithCustomForms result = apiInstance.V4GetAddressesId(contentType, xAccelaAppid, id, expand, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AddressesApi.V4GetAddressesId: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | 
 **xAccelaAppid** | **string**| clientid | 
 **id** | **long?**| The system id of the address to fetch. | 
 **expand** | **string**| Related objects to be returned with the response. The related object(s) will be returned if data exists; if data does not exist, the requested object(s) will not be included in the response.   Added in Civic Platform version: 9.3.0  | [optional] 
 **fields** | **string**| Comma - delimited names of fields to be returned in the response.Note - Field names are case -sensitive and only first - level fields are supported.Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N.Default language is en_US. | [optional] 

### Return type

[**ResponseAddressModelWithCustomForms**](ResponseAddressModelWithCustomForms.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getaddressesidconditions"></a>
# **V4GetAddressesIdConditions**
> ResponseConditionModelArray V4GetAddressesIdConditions (string contentType, string authorization, string xAccelaAppid, long? id, string type = null, string name = null, string status = null, long? offset = null, long? limit = null, string fields = null, string lang = null)

Get All Address Conditions

Returns all the address conditions for a given address. The results can be filtered by type and status.    **API Endpoint**:  GET /v4/addresses/{id}/conditions    **Scope**:  addresses   **App Type**:  All   **Authorization Type**:  Access token   **Civic Platform version**: 9.0.0  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAddressParcelsOwners.Api;
using AccelaAddressParcelsOwners.Client;
using AccelaAddressParcelsOwners.Model;

namespace Example
{
    public class V4GetAddressesIdConditionsExample
    {
        public void main()
        {
            var apiInstance = new AddressesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var xAccelaAppid = xAccelaAppid_example;  // string | clientid.
            var id = 789;  // long? | The system id of the address to fetch.
            var type = type_example;  // string | Filter by condition type.See[Get All Condition Types](. / api - settings.html # operation / v4.get.settings.conditions.types) (optional) 
            var name = name_example;  // string | Filter by condition name. (optional) 
            var status = status_example;  // string | Filter by condition status.See[Get All Standard Condition Statuses](. / api - settings.html # operation / v4.get.settings.conditions.statuses), [Get All Approval Condition Statuses](. / api - settings.html # operation / v4.get.settings.conditionApprovals.statuses) (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array.For example,  if offset is 100,the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var fields = fields_example;  // string | Comma - delimited names of fields to be returned in the response.Note - Field names are case -sensitive and only first - level fields are supported.Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N.Default language is en_US. (optional) 

            try
            {
                // Get All Address Conditions
                ResponseConditionModelArray result = apiInstance.V4GetAddressesIdConditions(contentType, authorization, xAccelaAppid, id, type, name, status, offset, limit, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AddressesApi.V4GetAddressesIdConditions: " + e.Message );
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
 **xAccelaAppid** | **string**| clientid. | 
 **id** | **long?**| The system id of the address to fetch. | 
 **type** | **string**| Filter by condition type.See[Get All Condition Types](. / api - settings.html # operation / v4.get.settings.conditions.types) | [optional] 
 **name** | **string**| Filter by condition name. | [optional] 
 **status** | **string**| Filter by condition status.See[Get All Standard Condition Statuses](. / api - settings.html # operation / v4.get.settings.conditions.statuses), [Get All Approval Condition Statuses](. / api - settings.html # operation / v4.get.settings.conditionApprovals.statuses) | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array.For example,  if offset is 100,the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 
 **fields** | **string**| Comma - delimited names of fields to be returned in the response.Note - Field names are case -sensitive and only first - level fields are supported.Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N.Default language is en_US. | [optional] 

### Return type

[**ResponseConditionModelArray**](ResponseConditionModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getaddressesidparcels"></a>
# **V4GetAddressesIdParcels**
> ResponseParcelModel V4GetAddressesIdParcels (string contentType, string authorization, string xAccelaAppid, long? id, string fields = null, string lang = null)

Get All Address Parcels

Gets the parcels associated with the specified addresses.    **API Endpoint**:  GET /v4/addresses/{id}/parcels   **Scope**:  addresses   **App Type**:  All   **Authorization Type**:  Access token   **Civic Platform version**: 7.3.2  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAddressParcelsOwners.Api;
using AccelaAddressParcelsOwners.Client;
using AccelaAddressParcelsOwners.Model;

namespace Example
{
    public class V4GetAddressesIdParcelsExample
    {
        public void main()
        {
            var apiInstance = new AddressesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var xAccelaAppid = xAccelaAppid_example;  // string | clientid.
            var id = 789;  // long? | The system id of the address to fetch.
            var fields = fields_example;  // string | Comma - delimited names of fields to be returned in the response.Note - Field names are case -sensitive and only first - level fields are supported.Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N.Default language is en_US. (optional) 

            try
            {
                // Get All Address Parcels
                ResponseParcelModel result = apiInstance.V4GetAddressesIdParcels(contentType, authorization, xAccelaAppid, id, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AddressesApi.V4GetAddressesIdParcels: " + e.Message );
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
 **xAccelaAppid** | **string**| clientid. | 
 **id** | **long?**| The system id of the address to fetch. | 
 **fields** | **string**| Comma - delimited names of fields to be returned in the response.Note - Field names are case -sensitive and only first - level fields are supported.Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N.Default language is en_US. | [optional] 

### Return type

[**ResponseParcelModel**](ResponseParcelModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getaddressesidrecords"></a>
# **V4GetAddressesIdRecords**
> ResponseSimpleRecordModelArray V4GetAddressesIdRecords (string contentType, string authorization, string xAccelaAppid, long? id, string types = null, string statuses = null, long? offset = null, long? limit = null, string fields = null, string lang = null)

Get All Address Records

Gets the records associated with the given reference address.    **API Endpoint**:  GET /v4/addresses/{id}/records   **Scope**:  addresses   **App Type**:  All   **Authorization Type**:  Access token   **Civic Platform version**: 9.3.0   

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAddressParcelsOwners.Api;
using AccelaAddressParcelsOwners.Client;
using AccelaAddressParcelsOwners.Model;

namespace Example
{
    public class V4GetAddressesIdRecordsExample
    {
        public void main()
        {
            var apiInstance = new AddressesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var xAccelaAppid = xAccelaAppid_example;  // string | clientid.
            var id = 789;  // long? | The system id of the address to fetch.
            var types = types_example;  // string | Filter by one or more comma-delimited record types. Specify a record value using its hierarchical structure, for example: types=AMS%2FElectric%2FPole%2FInstall,AMS%2FElectric%2FPole%2FMaintain   See [Get All Record Types](./api-settings.html#operation/v4.get.settings.records.types). (optional) 
            var statuses = statuses_example;  // string | Filter by one or more comma-delimited record statuses. For example: statuses=Open,Ready%20to%20Issue   See [Get All Statuses for Record Type](./api-settings.html#operation/v4.get.settings.records.types.id.statuses). (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array.For example,  if offset is 100,the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var fields = fields_example;  // string | Comma - delimited names of fields to be returned in the response.Note - Field names are case -sensitive and only first - level fields are supported.Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N.Default language is en_US. (optional) 

            try
            {
                // Get All Address Records
                ResponseSimpleRecordModelArray result = apiInstance.V4GetAddressesIdRecords(contentType, authorization, xAccelaAppid, id, types, statuses, offset, limit, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AddressesApi.V4GetAddressesIdRecords: " + e.Message );
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
 **xAccelaAppid** | **string**| clientid. | 
 **id** | **long?**| The system id of the address to fetch. | 
 **types** | **string**| Filter by one or more comma-delimited record types. Specify a record value using its hierarchical structure, for example: types&#x3D;AMS%2FElectric%2FPole%2FInstall,AMS%2FElectric%2FPole%2FMaintain   See [Get All Record Types](./api-settings.html#operation/v4.get.settings.records.types). | [optional] 
 **statuses** | **string**| Filter by one or more comma-delimited record statuses. For example: statuses&#x3D;Open,Ready%20to%20Issue   See [Get All Statuses for Record Type](./api-settings.html#operation/v4.get.settings.records.types.id.statuses). | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array.For example,  if offset is 100,the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 
 **fields** | **string**| Comma - delimited names of fields to be returned in the response.Note - Field names are case -sensitive and only first - level fields are supported.Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N.Default language is en_US. | [optional] 

### Return type

[**ResponseSimpleRecordModelArray**](ResponseSimpleRecordModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4putaddressesid"></a>
# **V4PutAddressesId**
> ResponseAddressModelWithCustomForms V4PutAddressesId (string contentType, string authorization, string xAccelaAppid, long? id, RequestAddressModelWithCustomForms address, string lang = null)

Update an Address

Updates a reference address, including address custom fields.     **API Endpoint**:  PUT /v4/addresses/{id}   **Scope**:  addresses   **App Type**:  Agency   **Authorization Type**:  Access token   **Civic Platform version**: 9.3.0  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAddressParcelsOwners.Api;
using AccelaAddressParcelsOwners.Client;
using AccelaAddressParcelsOwners.Model;

namespace Example
{
    public class V4PutAddressesIdExample
    {
        public void main()
        {
            var apiInstance = new AddressesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var xAccelaAppid = xAccelaAppid_example;  // string | clientid.
            var id = 789;  // long? | The system id of the address to fetch.
            var address = new RequestAddressModelWithCustomForms(); // RequestAddressModelWithCustomForms | The reference address to be updated
            var lang = lang_example;  // string | Language parameter to support I18N.Default language is en_US. (optional) 

            try
            {
                // Update an Address
                ResponseAddressModelWithCustomForms result = apiInstance.V4PutAddressesId(contentType, authorization, xAccelaAppid, id, address, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AddressesApi.V4PutAddressesId: " + e.Message );
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
 **xAccelaAppid** | **string**| clientid. | 
 **id** | **long?**| The system id of the address to fetch. | 
 **address** | [**RequestAddressModelWithCustomForms**](RequestAddressModelWithCustomForms.md)| The reference address to be updated | 
 **lang** | **string**| Language parameter to support I18N.Default language is en_US. | [optional] 

### Return type

[**ResponseAddressModelWithCustomForms**](ResponseAddressModelWithCustomForms.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

