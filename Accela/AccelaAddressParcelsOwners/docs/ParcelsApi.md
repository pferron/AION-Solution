# AccelaAddressParcelsOwners.Api.ParcelsApi

All URIs are relative to *http://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetParcels**](ParcelsApi.md#v4getparcels) | **GET** /v4/parcels | Get All Parcels
[**V4GetParcelsId**](ParcelsApi.md#v4getparcelsid) | **GET** /v4/parcels/{id} | Get Parcel
[**V4GetParcelsIdAddresses**](ParcelsApi.md#v4getparcelsidaddresses) | **GET** /v4/parcels/{id}/addresses | Get All Parcel Addresses
[**V4GetParcelsIdRecords**](ParcelsApi.md#v4getparcelsidrecords) | **GET** /v4/parcels/{id}/records | Get All Parcel Records
[**V4GetParcelsParcelIdConditions**](ParcelsApi.md#v4getparcelsparcelidconditions) | **GET** /v4/parcels/{parcelId}/conditions | Get All Parcel Conditions
[**V4GetParcelsParcelIdOwners**](ParcelsApi.md#v4getparcelsparcelidowners) | **GET** /v4/parcels/{parcelId}/owners | Get All Parcel Owners


<a name="v4getparcels"></a>
# **V4GetParcels**
> ResponseParcelModelArray V4GetParcels (string contentType, string authorization, string xAccelaAppid, string parcelNumber = null, string lot = null, string isPrimary = null, string range = null, string subdivision = null, long? section = null, string township = null, string fullName = null, string streetName = null, string city = null, string streetType = null, long? streetStart = null, long? streetEnd = null, string expand = null, long? offset = null, long? limit = null, string fields = null, string lang = null)

Get All Parcels

Gets a list of reference parcels in the agency database. Specify at least one filter criteria.    **API Endpoint**:  GET /v4/parcels   **Scope**:  parcels   **App Type**:  All   **Authorization Type**:  No authorization required   **Civic Platform version**: 7.3.2  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAddressParcelsOwners.Api;
using AccelaAddressParcelsOwners.Client;
using AccelaAddressParcelsOwners.Model;

namespace Example
{
    public class V4GetParcelsExample
    {
        public void main()
        {
            var apiInstance = new ParcelsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var xAccelaAppid = xAccelaAppid_example;  // string | clientid.
            var parcelNumber = parcelNumber_example;  // string | Filter by parcel number. (optional) 
            var lot = lot_example;  // string | Filter by parcel lot. (optional) 
            var isPrimary = isPrimary_example;  // string | Filter by whether or not parcel is primary. (optional) 
            var range = range_example;  // string | Filter by parcel range. (optional) 
            var subdivision = subdivision_example;  // string | Filter by parcel subdivision. (optional) 
            var section = 789;  // long? | Filter by parcel section. (optional) 
            var township = township_example;  // string | Filter by parcel township. (optional) 
            var fullName = fullName_example;  // string | Filter by owner's full name. (optional) 
            var streetName = streetName_example;  // string | Filter by street name. (optional) 
            var city = city_example;  // string | Filter by city. (optional) 
            var streetType = streetType_example;  // string | Filter by street type. (optional) 
            var streetStart = 789;  // long? | Filter by the starting number of a street address range. (optional) 
            var streetEnd = 789;  // long? | Filter by the ending number of a street address range. (optional) 
            var expand = expand_example;  // string | Related objects to be returned with the response. The related object(s) will be returned if data exists; if data does not exist, the requested object(s) will not be included in the response.   Added in Civic Platform version: 9.3.0 (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array.For example,  if offset is 100,the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var fields = fields_example;  // string | Comma - delimited names of fields to be returned in the response.Note - Field names are case -sensitive and only first - level fields are supported.Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N.Default language is en_US. (optional) 

            try
            {
                // Get All Parcels
                ResponseParcelModelArray result = apiInstance.V4GetParcels(contentType, authorization, xAccelaAppid, parcelNumber, lot, isPrimary, range, subdivision, section, township, fullName, streetName, city, streetType, streetStart, streetEnd, expand, offset, limit, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ParcelsApi.V4GetParcels: " + e.Message );
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
 **parcelNumber** | **string**| Filter by parcel number. | [optional] 
 **lot** | **string**| Filter by parcel lot. | [optional] 
 **isPrimary** | **string**| Filter by whether or not parcel is primary. | [optional] 
 **range** | **string**| Filter by parcel range. | [optional] 
 **subdivision** | **string**| Filter by parcel subdivision. | [optional] 
 **section** | **long?**| Filter by parcel section. | [optional] 
 **township** | **string**| Filter by parcel township. | [optional] 
 **fullName** | **string**| Filter by owner&#39;s full name. | [optional] 
 **streetName** | **string**| Filter by street name. | [optional] 
 **city** | **string**| Filter by city. | [optional] 
 **streetType** | **string**| Filter by street type. | [optional] 
 **streetStart** | **long?**| Filter by the starting number of a street address range. | [optional] 
 **streetEnd** | **long?**| Filter by the ending number of a street address range. | [optional] 
 **expand** | **string**| Related objects to be returned with the response. The related object(s) will be returned if data exists; if data does not exist, the requested object(s) will not be included in the response.   Added in Civic Platform version: 9.3.0 | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array.For example,  if offset is 100,the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 
 **fields** | **string**| Comma - delimited names of fields to be returned in the response.Note - Field names are case -sensitive and only first - level fields are supported.Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N.Default language is en_US. | [optional] 

### Return type

[**ResponseParcelModelArray**](ResponseParcelModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getparcelsid"></a>
# **V4GetParcelsId**
> ResponseParcelModel V4GetParcelsId (string contentType, string authorization, string xAccelaAppid, string id, string expand = null, string fields = null, string lang = null)

Get Parcel

Gets information about a reference parcel.    **API Endpoint**:  GET /v4/parcels/{id}   **Scope**:  parcels   **App Type**:  All   **Authorization Type**:  No authorization required   **Civic Platform version**:  7.3.2  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAddressParcelsOwners.Api;
using AccelaAddressParcelsOwners.Client;
using AccelaAddressParcelsOwners.Model;

namespace Example
{
    public class V4GetParcelsIdExample
    {
        public void main()
        {
            var apiInstance = new ParcelsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var xAccelaAppid = xAccelaAppid_example;  // string | clientid.
            var id = id_example;  // string | The system id of the parcel to fetch.
            var expand = expand_example;  // string | Related objects to be returned with the response. The related object(s) will be returned if data exists; if data does not exist, the requested object(s) will not be included in the response.   Added in Civic Platform version: 9.3.0 (optional) 
            var fields = fields_example;  // string | Comma - delimited names of fields to be returned in the response.Note - Field names are case -sensitive and only first - level fields are supported.Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N.Default language is en_US. (optional) 

            try
            {
                // Get Parcel
                ResponseParcelModel result = apiInstance.V4GetParcelsId(contentType, authorization, xAccelaAppid, id, expand, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ParcelsApi.V4GetParcelsId: " + e.Message );
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
 **id** | **string**| The system id of the parcel to fetch. | 
 **expand** | **string**| Related objects to be returned with the response. The related object(s) will be returned if data exists; if data does not exist, the requested object(s) will not be included in the response.   Added in Civic Platform version: 9.3.0 | [optional] 
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

<a name="v4getparcelsidaddresses"></a>
# **V4GetParcelsIdAddresses**
> ResponseAddressModelArray V4GetParcelsIdAddresses (string contentType, string authorization, string xAccelaAppid, string id, string fields = null, string lang = null)

Get All Parcel Addresses

Gets a list of addresses for a reference parcel.    **API Endpoint**:  GET /v4/parcels/{id}/addresses   **Scope**:  parcels   **App Type**:  All   **Authorization Type**:  No authorization required   **Civic Platform version**: 7.3.2  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAddressParcelsOwners.Api;
using AccelaAddressParcelsOwners.Client;
using AccelaAddressParcelsOwners.Model;

namespace Example
{
    public class V4GetParcelsIdAddressesExample
    {
        public void main()
        {
            var apiInstance = new ParcelsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var xAccelaAppid = xAccelaAppid_example;  // string | clientid.
            var id = id_example;  // string | The system id of the parcel to fetch.
            var fields = fields_example;  // string | Comma - delimited names of fields to be returned in the response.Note - Field names are case -sensitive and only first - level fields are supported.Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N.Default language is en_US. (optional) 

            try
            {
                // Get All Parcel Addresses
                ResponseAddressModelArray result = apiInstance.V4GetParcelsIdAddresses(contentType, authorization, xAccelaAppid, id, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ParcelsApi.V4GetParcelsIdAddresses: " + e.Message );
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
 **id** | **string**| The system id of the parcel to fetch. | 
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

<a name="v4getparcelsidrecords"></a>
# **V4GetParcelsIdRecords**
> ResponseSimpleRecordModelArray V4GetParcelsIdRecords (string contentType, string authorization, string xAccelaAppid, string id, bool? includeRecordsFromParcelHistory = null, long? offset = null, long? limit = null, string fields = null, string lang = null)

Get All Parcel Records

Gets the records associated with the given reference parcel.    **API Endpoint**:  GET /v4/parcels/{id}/records   **Scope**:  parcels   **App Type**:  All   **Authorization Type**:  Access token   **Civic Platform version**: 9.3.0   

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAddressParcelsOwners.Api;
using AccelaAddressParcelsOwners.Client;
using AccelaAddressParcelsOwners.Model;

namespace Example
{
    public class V4GetParcelsIdRecordsExample
    {
        public void main()
        {
            var apiInstance = new ParcelsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var xAccelaAppid = xAccelaAppid_example;  // string | clientid.
            var id = id_example;  // string | The system id of the parcel to fetch.
            var includeRecordsFromParcelHistory = true;  // bool? | If true, records for the given reference parcel and all historical parcels in its genealogy (such as its parents, grandparents, or ancestors) will be returned. If false, only records for the given reference parcel will be returned. Default is false. (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array.For example,  if offset is 100,the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var fields = fields_example;  // string | Comma - delimited names of fields to be returned in the response.Note - Field names are case -sensitive and only first - level fields are supported.Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N.Default language is en_US. (optional) 

            try
            {
                // Get All Parcel Records
                ResponseSimpleRecordModelArray result = apiInstance.V4GetParcelsIdRecords(contentType, authorization, xAccelaAppid, id, includeRecordsFromParcelHistory, offset, limit, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ParcelsApi.V4GetParcelsIdRecords: " + e.Message );
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
 **id** | **string**| The system id of the parcel to fetch. | 
 **includeRecordsFromParcelHistory** | **bool?**| If true, records for the given reference parcel and all historical parcels in its genealogy (such as its parents, grandparents, or ancestors) will be returned. If false, only records for the given reference parcel will be returned. Default is false. | [optional] 
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

<a name="v4getparcelsparcelidconditions"></a>
# **V4GetParcelsParcelIdConditions**
> ResponseConditionModelArray V4GetParcelsParcelIdConditions (string contentType, string authorization, string xAccelaAppid, string parcelId, string type = null, string name = null, string status = null, long? offset = null, long? limit = null, string fields = null, string lang = null)

Get All Parcel Conditions

Gets the conditions for the reference parcel.    **API Endpoint**:  GET /v4/parcels/{parcelId}/conditions   **Scope**:  parcels   **App Type**:  All   **Authorization Type**:  Access token   **Civic Platform version**: 8.0.0  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAddressParcelsOwners.Api;
using AccelaAddressParcelsOwners.Client;
using AccelaAddressParcelsOwners.Model;

namespace Example
{
    public class V4GetParcelsParcelIdConditionsExample
    {
        public void main()
        {
            var apiInstance = new ParcelsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var xAccelaAppid = xAccelaAppid_example;  // string | clientid.
            var parcelId = parcelId_example;  // string | The system id of the parcel to fetch.
            var type = type_example;  // string | Filter by condition type.See[Get All Condition Types](. / api - settings.html # operation / v4.get.settings.conditions.types) (optional) 
            var name = name_example;  // string | Filter by condition name. (optional) 
            var status = status_example;  // string | Filter by condition status.See[Get All Standard Condition Statuses](. / api - settings.html # operation / v4.get.settings.conditions.statuses), [Get All Approval Condition Statuses](. / api - settings.html # operation / v4.get.settings.conditionApprovals.statuses) (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array.For example,  if offset is 100,the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var fields = fields_example;  // string | Comma - delimited names of fields to be returned in the response.Note - Field names are case -sensitive and only first - level fields are supported.Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N.Default language is en_US. (optional) 

            try
            {
                // Get All Parcel Conditions
                ResponseConditionModelArray result = apiInstance.V4GetParcelsParcelIdConditions(contentType, authorization, xAccelaAppid, parcelId, type, name, status, offset, limit, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ParcelsApi.V4GetParcelsParcelIdConditions: " + e.Message );
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
 **parcelId** | **string**| The system id of the parcel to fetch. | 
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

<a name="v4getparcelsparcelidowners"></a>
# **V4GetParcelsParcelIdOwners**
> ResponseOwnerModelArray V4GetParcelsParcelIdOwners (string contentType, string authorization, string xAccelaAppid, string parcelId, string fields = null, string lang = null)

Get All Parcel Owners

Gets a list of owners for the reference parcel.    **API Endpoint**:  GET /v4/parcels/{parcelId}/owners   **Scope**:  parcels   **App Type**:  All   **Authorization Type**:  No authorization required   **Civic Platform version**: 7.3.2  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAddressParcelsOwners.Api;
using AccelaAddressParcelsOwners.Client;
using AccelaAddressParcelsOwners.Model;

namespace Example
{
    public class V4GetParcelsParcelIdOwnersExample
    {
        public void main()
        {
            var apiInstance = new ParcelsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var xAccelaAppid = xAccelaAppid_example;  // string | clientid.
            var parcelId = parcelId_example;  // string | The system id of the parcel to fetch.
            var fields = fields_example;  // string | Comma - delimited names of fields to be returned in the response.Note - Field names are case -sensitive and only first - level fields are supported.Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N.Default language is en_US. (optional) 

            try
            {
                // Get All Parcel Owners
                ResponseOwnerModelArray result = apiInstance.V4GetParcelsParcelIdOwners(contentType, authorization, xAccelaAppid, parcelId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ParcelsApi.V4GetParcelsParcelIdOwners: " + e.Message );
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
 **parcelId** | **string**| The system id of the parcel to fetch. | 
 **fields** | **string**| Comma - delimited names of fields to be returned in the response.Note - Field names are case -sensitive and only first - level fields are supported.Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N.Default language is en_US. | [optional] 

### Return type

[**ResponseOwnerModelArray**](ResponseOwnerModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

