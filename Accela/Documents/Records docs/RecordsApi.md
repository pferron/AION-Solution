# AccelaRecords.Api.RecordsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4DeleteRecordsIds**](RecordsApi.md#v4deleterecordsids) | **DELETE** /v4/records/{ids} | Delete Records
[**V4DeleteRecordsRecordIdRelatedChildRecordIds**](RecordsApi.md#v4deleterecordsrecordidrelatedchildrecordids) | **DELETE** /v4/records/{recordId}/related/{childRecordIds} | Delete Related Details from Record
[**V4GetRecords**](RecordsApi.md#v4getrecords) | **GET** /v4/records | Get All Records
[**V4GetRecordsDescribeCreate**](RecordsApi.md#v4getrecordsdescribecreate) | **GET** /v4/records/describe/create | Describe Required Record Attributes
[**V4GetRecordsIds**](RecordsApi.md#v4getrecordsids) | **GET** /v4/records/{ids} | Get Records
[**V4GetRecordsMine**](RecordsApi.md#v4getrecordsmine) | **GET** /v4/records/mine | Get My Records
[**V4GetRecordsRecordIdAdditional**](RecordsApi.md#v4getrecordsrecordidadditional) | **GET** /v4/records/{recordId}/additional | Get Additional Details for Record
[**V4GetRecordsRecordIdRelated**](RecordsApi.md#v4getrecordsrecordidrelated) | **GET** /v4/records/{recordId}/related | Get All Related Details for Record
[**V4PostRecords**](RecordsApi.md#v4postrecords) | **POST** /v4/records | Create Record
[**V4PostRecordsInitialize**](RecordsApi.md#v4postrecordsinitialize) | **POST** /v4/records/initialize | Create Partial Record
[**V4PostRecordsRecordIdFinalize**](RecordsApi.md#v4postrecordsrecordidfinalize) | **POST** /v4/records/{recordId}/finalize | Finalize Record
[**V4PostRecordsRecordIdRelated**](RecordsApi.md#v4postrecordsrecordidrelated) | **POST** /v4/records/{recordId}/related | Create Related Details for Record
[**V4PutRecordsId**](RecordsApi.md#v4putrecordsid) | **PUT** /v4/Records/{id} | Update Record
[**V4PutRecordsRecordIdAdditional**](RecordsApi.md#v4putrecordsrecordidadditional) | **PUT** /v4/records/{recordId}/additional | Update Additional Details for Record


<a name="v4deleterecordsids"></a>
# **V4DeleteRecordsIds**
> ResponseResultModelArray V4DeleteRecordsIds (string contentType, string authorization, string ids, string lang = null)

Delete Records

Deletes the specified record. **API Endpoint**:  DELETE /v4/records/{ids}  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**:  7.3.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4DeleteRecordsIdsExample
    {
        public void main()
        {
            var apiInstance = new RecordsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var ids = ids_example;  // string | Comma-delimited IDs of the records to be deleted.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Delete Records
                ResponseResultModelArray result = apiInstance.V4DeleteRecordsIds(contentType, authorization, ids, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsApi.V4DeleteRecordsIds: " + e.Message );
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
 **ids** | **string**| Comma-delimited IDs of the records to be deleted. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4deleterecordsrecordidrelatedchildrecordids"></a>
# **V4DeleteRecordsRecordIdRelatedChildRecordIds**
> ResponseResultModelArray V4DeleteRecordsRecordIdRelatedChildRecordIds (string contentType, string authorization, string recordId, string childRecordIds, string lang = null)

Delete Related Details from Record

Removes the relationship between the specifed child record(s) and their specified parent record. **API Endpoint**:  DELETE /v4/records/{recordId}/related/{childRecordIds}  **Scope**:  records  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4DeleteRecordsRecordIdRelatedChildRecordIdsExample
    {
        public void main()
        {
            var apiInstance = new RecordsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var childRecordIds = childRecordIds_example;  // string | Comma-delimited IDs of the related records to be removed.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Delete Related Details from Record
                ResponseResultModelArray result = apiInstance.V4DeleteRecordsRecordIdRelatedChildRecordIds(contentType, authorization, recordId, childRecordIds, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsApi.V4DeleteRecordsRecordIdRelatedChildRecordIds: " + e.Message );
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
 **childRecordIds** | **string**| Comma-delimited IDs of the related records to be removed. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getrecords"></a>
# **V4GetRecords**
> ResponseSimpleRecordModelArray V4GetRecords (string contentType, string authorization, string type = null, string openedDateFrom = null, string openedDateTo = null, string customId = null, string module = null, string status = null, string assignedToDepartment = null, string assignedUser = null, string assignedDateFrom = null, string assignedDateTo = null, string completedDateFrom = null, string completedDateTo = null, string statusDateFrom = null, string statusDateTo = null, string completedByDepartment = null, string completedByUser = null, string closedDateFrom = null, string closedDateTo = null, string closedByDepartment = null, string closedByUser = null, string recordClass = null, long? limit = null, long? offset = null, string fields = null, string lang = null)

Get All Records

Gets record information, based on the specified query parameters. **API Endpoint**:  GET /v4/records  **Scope**:  records  **App Type**:  All  **Authorization Type**:   No authorization required  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsExample
    {
        public void main()
        {
            var apiInstance = new RecordsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var type = type_example;  // string | Filter by record type. See [Get All Record Types](./api-settings.html#operation/v4.get.settings.records.types). (optional) 
            var openedDateFrom = openedDateFrom_example;  // string | Filter by the record's open date range, beginning with this date. (optional) 
            var openedDateTo = openedDateTo_example;  // string | Filter by the record's open date range, ending with this date. (optional) 
            var customId = customId_example;  // string | Filter by the record custom id. (optional) 
            var module = module_example;  // string | Filter by module. See [Get All Modules](./api-settings.html#operation/v4.get.settings.modules). (optional) 
            var status = status_example;  // string | Filter by record status. (optional) 
            var assignedToDepartment = assignedToDepartment_example;  // string | Filter by the assigned department. (optional) 
            var assignedUser = assignedUser_example;  // string | Filter by the assigned user. (optional) 
            var assignedDateFrom = assignedDateFrom_example;  // string | Filter by the record's assigned date range starting with this date. (optional) 
            var assignedDateTo = assignedDateTo_example;  // string | Filter by the record's assigned date range ending with this date. (optional) 
            var completedDateFrom = completedDateFrom_example;  // string | Filter by the record's completed date range starting with this date. (optional) 
            var completedDateTo = completedDateTo_example;  // string | Filter by the record's completed date range ending with this date. (optional) 
            var statusDateFrom = statusDateFrom_example;  // string | Filter by the record's status date range starting with this date. (optional) 
            var statusDateTo = statusDateTo_example;  // string | Filter by the record's status date range ending with this date. (optional) 
            var completedByDepartment = completedByDepartment_example;  // string | Filter by the deparment which completed the application. (optional) 
            var completedByUser = completedByUser_example;  // string | Filter by the user who completed the application. (optional) 
            var closedDateFrom = closedDateFrom_example;  // string | Filter by the record's closed date range starting with this date. (optional) 
            var closedDateTo = closedDateTo_example;  // string | Filter by the record's closed date range ending with this date. (optional) 
            var closedByDepartment = closedByDepartment_example;  // string | Filter by the department which closed the application. (optional) 
            var closedByUser = closedByUser_example;  // string | Filter by the user who closed the application. (optional) 
            var recordClass = recordClass_example;  // string | Filter by record class (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Records
                ResponseSimpleRecordModelArray result = apiInstance.V4GetRecords(contentType, authorization, type, openedDateFrom, openedDateTo, customId, module, status, assignedToDepartment, assignedUser, assignedDateFrom, assignedDateTo, completedDateFrom, completedDateTo, statusDateFrom, statusDateTo, completedByDepartment, completedByUser, closedDateFrom, closedDateTo, closedByDepartment, closedByUser, recordClass, limit, offset, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsApi.V4GetRecords: " + e.Message );
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
 **type** | **string**| Filter by record type. See [Get All Record Types](./api-settings.html#operation/v4.get.settings.records.types). | [optional] 
 **openedDateFrom** | **string**| Filter by the record&#39;s open date range, beginning with this date. | [optional] 
 **openedDateTo** | **string**| Filter by the record&#39;s open date range, ending with this date. | [optional] 
 **customId** | **string**| Filter by the record custom id. | [optional] 
 **module** | **string**| Filter by module. See [Get All Modules](./api-settings.html#operation/v4.get.settings.modules). | [optional] 
 **status** | **string**| Filter by record status. | [optional] 
 **assignedToDepartment** | **string**| Filter by the assigned department. | [optional] 
 **assignedUser** | **string**| Filter by the assigned user. | [optional] 
 **assignedDateFrom** | **string**| Filter by the record&#39;s assigned date range starting with this date. | [optional] 
 **assignedDateTo** | **string**| Filter by the record&#39;s assigned date range ending with this date. | [optional] 
 **completedDateFrom** | **string**| Filter by the record&#39;s completed date range starting with this date. | [optional] 
 **completedDateTo** | **string**| Filter by the record&#39;s completed date range ending with this date. | [optional] 
 **statusDateFrom** | **string**| Filter by the record&#39;s status date range starting with this date. | [optional] 
 **statusDateTo** | **string**| Filter by the record&#39;s status date range ending with this date. | [optional] 
 **completedByDepartment** | **string**| Filter by the deparment which completed the application. | [optional] 
 **completedByUser** | **string**| Filter by the user who completed the application. | [optional] 
 **closedDateFrom** | **string**| Filter by the record&#39;s closed date range starting with this date. | [optional] 
 **closedDateTo** | **string**| Filter by the record&#39;s closed date range ending with this date. | [optional] 
 **closedByDepartment** | **string**| Filter by the department which closed the application. | [optional] 
 **closedByUser** | **string**| Filter by the user who closed the application. | [optional] 
 **recordClass** | **string**| Filter by record class | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
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

<a name="v4getrecordsdescribecreate"></a>
# **V4GetRecordsDescribeCreate**
> ResponseDescribeRecordModel V4GetRecordsDescribeCreate (string authorization, string type, string lang = null)

Describe Required Record Attributes

Gets the field and element values the system requires in order to create a specific type of record. **API Endpoint**:  GET /v4/records/describe/create   **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**:  7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsDescribeCreateExample
    {
        public void main()
        {
            var apiInstance = new RecordsApi();
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var type = type_example;  // string | Filter by record type. See [Get All Record Type](./api-settings.html#operation/v4.get.settings.records.types).
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Describe Required Record Attributes
                ResponseDescribeRecordModel result = apiInstance.V4GetRecordsDescribeCreate(authorization, type, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsApi.V4GetRecordsDescribeCreate: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **authorization** | **string**| Construct oAuth2 authentication token | 
 **type** | **string**| Filter by record type. See [Get All Record Type](./api-settings.html#operation/v4.get.settings.records.types). | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseDescribeRecordModel**](ResponseDescribeRecordModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getrecordsids"></a>
# **V4GetRecordsIds**
> ResponseRecordModelArray V4GetRecordsIds (string contentType, string ids, string expand = null, string expandCustomForms = null, string fields = null, string lang = null)

Get Records

Gets the requested record(s). **API Endpoint**:  GET /v4/records/{ids}  **Scope**:  records  **App Type**:  All  **Authorization Type**:  No authorization required  **Civic Platform version**:  7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsIdsExample
    {
        public void main()
        {
            var apiInstance = new RecordsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var ids = ids_example;  // string | Comma-delimited IDs of the records to fetch.
            var expand = expand_example;  // string | Comma-delimited list of related objects to be returned with the response. The related object(s) will be returned if data exists; if data does not exist, the requested object(s) will not be included in the response. (optional) 
            var expandCustomForms = expandCustomForms_example;  // string | Valid values: \"addresses\" If {expand} includes any of addresses, parcels, owners, or contacts, {expandCustomForms} specifies which {expand} objects should include custom forms. By default, custom forms for addresses, parcels, owners, and contacts are not included in the response data. Note that requesting APO custom forms may have performance implications, depending on the amount of data and connectivity to any external data source. Added in Civic Platform version: 9.2.0  (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Records
                ResponseRecordModelArray result = apiInstance.V4GetRecordsIds(contentType, ids, expand, expandCustomForms, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsApi.V4GetRecordsIds: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | [default to application/x-www-form-urlencoded]
 **ids** | **string**| Comma-delimited IDs of the records to fetch. | 
 **expand** | **string**| Comma-delimited list of related objects to be returned with the response. The related object(s) will be returned if data exists; if data does not exist, the requested object(s) will not be included in the response. | [optional] 
 **expandCustomForms** | **string**| Valid values: \&quot;addresses\&quot; If {expand} includes any of addresses, parcels, owners, or contacts, {expandCustomForms} specifies which {expand} objects should include custom forms. By default, custom forms for addresses, parcels, owners, and contacts are not included in the response data. Note that requesting APO custom forms may have performance implications, depending on the amount of data and connectivity to any external data source. Added in Civic Platform version: 9.2.0  | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseRecordModelArray**](ResponseRecordModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getrecordsmine"></a>
# **V4GetRecordsMine**
> ResponseRecordExtModel1Array V4GetRecordsMine (string contentType, string authorization, string type = null, string openedDateFrom = null, string openedDateTo = null, string customId = null, string module = null, string status = null, string assignedDateFrom = null, string assignedDateTo = null, string completedDateFrom = null, string completedDateTo = null, string statusDateFrom = null, string statusDateTo = null, string updateDateFrom = null, string updateDateTo = null, string completedByDepartment = null, string completedByUser = null, string closedDateFrom = null, string closedDateTo = null, string closedByDepartment = null, string closedByUser = null, string recordClass = null, string types = null, string modules = null, string statusTypes = null, string expand = null, string expandCustomForms = null, long? limit = null, long? offset = null, string fields = null, string lang = null)

Get My Records

Gets records for the currently logged-in user. **API Endpoint**:  GET /v4/records/mine  **Scope**:  records  **App Type**:  All  **Authorization Type**:  No authorization required  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsMineExample
    {
        public void main()
        {
            var apiInstance = new RecordsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var type = type_example;  // string | Filter by record type. See [Get All Record Types](./api-settings.html#operation/v4.get.settings.records.types). (optional) 
            var openedDateFrom = openedDateFrom_example;  // string | Filter by the record's open date range, beginning with this date. (optional) 
            var openedDateTo = openedDateTo_example;  // string | Filter by the record's open date range, ending with this date. (optional) 
            var customId = customId_example;  // string | Filter by the record custom id. (optional) 
            var module = module_example;  // string | Filter by module. See [Get All Modules](./api-settings.html#operation/v4.get.settings.modules). (optional) 
            var status = status_example;  // string | Filter by record status. (optional) 
            var assignedDateFrom = assignedDateFrom_example;  // string | Filter by the record's assigned date range starting with this date. (optional) 
            var assignedDateTo = assignedDateTo_example;  // string | Filter by the record's assigned date range ending with this date. (optional) 
            var completedDateFrom = completedDateFrom_example;  // string | Filter by the record's completed date range starting with this date. (optional) 
            var completedDateTo = completedDateTo_example;  // string | Filter by the record's completed date range ending with this date. (optional) 
            var statusDateFrom = statusDateFrom_example;  // string | Filter by the record's status date range starting with this date. (optional) 
            var statusDateTo = statusDateTo_example;  // string | Filter by the record's status date range ending with this date. (optional) 
            var updateDateFrom = updateDateFrom_example;  // string | Record update Date From (optional) 
            var updateDateTo = updateDateTo_example;  // string | Record update Date To (optional) 
            var completedByDepartment = completedByDepartment_example;  // string | Filter by the deparment which completed the application. (optional) 
            var completedByUser = completedByUser_example;  // string | Filter by the user who completed the application. (optional) 
            var closedDateFrom = closedDateFrom_example;  // string | Filter by the record's closed date range starting with this date. (optional) 
            var closedDateTo = closedDateTo_example;  // string | Filter by the record's closed date range ending with this date. (optional) 
            var closedByDepartment = closedByDepartment_example;  // string | Filter by the department which closed the application. (optional) 
            var closedByUser = closedByUser_example;  // string | Filter by the user who closed the application. (optional) 
            var recordClass = recordClass_example;  // string | Filter by record class (optional) 
            var types = types_example;  // string | Filter by comma-separated multiple record types. (optional) 
            var modules = modules_example;  // string | Filter by comma-separated multiple modules. (optional) 
            var statusTypes = statusTypes_example;  // string | Filter by comma-separated multiple record status types. (optional) 
            var expand = expand_example;  // string | Comma-delimited list of related objects to be returned with the response. The related object(s) will be returned if data exists; if data does not exist, the requested object(s) will not be included in the response. (optional) 
            var expandCustomForms = expandCustomForms_example;  // string | Valid values: \"addresses\"            If {expand} includes any of addresses, parcels, owners, or contacts, {expandCustomForms} specifies which {expand} objects should include custom forms. By default, custom forms for addresses, parcels, owners, and contacts are not included in the response data. Note that requesting APO custom forms may have performance implications, depending on the amount of data and connectivity to any external data source. Added in Civic Platform version: 9.2.0  (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get My Records
                ResponseRecordExtModel1Array result = apiInstance.V4GetRecordsMine(contentType, authorization, type, openedDateFrom, openedDateTo, customId, module, status, assignedDateFrom, assignedDateTo, completedDateFrom, completedDateTo, statusDateFrom, statusDateTo, updateDateFrom, updateDateTo, completedByDepartment, completedByUser, closedDateFrom, closedDateTo, closedByDepartment, closedByUser, recordClass, types, modules, statusTypes, expand, expandCustomForms, limit, offset, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsApi.V4GetRecordsMine: " + e.Message );
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
 **type** | **string**| Filter by record type. See [Get All Record Types](./api-settings.html#operation/v4.get.settings.records.types). | [optional] 
 **openedDateFrom** | **string**| Filter by the record&#39;s open date range, beginning with this date. | [optional] 
 **openedDateTo** | **string**| Filter by the record&#39;s open date range, ending with this date. | [optional] 
 **customId** | **string**| Filter by the record custom id. | [optional] 
 **module** | **string**| Filter by module. See [Get All Modules](./api-settings.html#operation/v4.get.settings.modules). | [optional] 
 **status** | **string**| Filter by record status. | [optional] 
 **assignedDateFrom** | **string**| Filter by the record&#39;s assigned date range starting with this date. | [optional] 
 **assignedDateTo** | **string**| Filter by the record&#39;s assigned date range ending with this date. | [optional] 
 **completedDateFrom** | **string**| Filter by the record&#39;s completed date range starting with this date. | [optional] 
 **completedDateTo** | **string**| Filter by the record&#39;s completed date range ending with this date. | [optional] 
 **statusDateFrom** | **string**| Filter by the record&#39;s status date range starting with this date. | [optional] 
 **statusDateTo** | **string**| Filter by the record&#39;s status date range ending with this date. | [optional] 
 **updateDateFrom** | **string**| Record update Date From | [optional] 
 **updateDateTo** | **string**| Record update Date To | [optional] 
 **completedByDepartment** | **string**| Filter by the deparment which completed the application. | [optional] 
 **completedByUser** | **string**| Filter by the user who completed the application. | [optional] 
 **closedDateFrom** | **string**| Filter by the record&#39;s closed date range starting with this date. | [optional] 
 **closedDateTo** | **string**| Filter by the record&#39;s closed date range ending with this date. | [optional] 
 **closedByDepartment** | **string**| Filter by the department which closed the application. | [optional] 
 **closedByUser** | **string**| Filter by the user who closed the application. | [optional] 
 **recordClass** | **string**| Filter by record class | [optional] 
 **types** | **string**| Filter by comma-separated multiple record types. | [optional] 
 **modules** | **string**| Filter by comma-separated multiple modules. | [optional] 
 **statusTypes** | **string**| Filter by comma-separated multiple record status types. | [optional] 
 **expand** | **string**| Comma-delimited list of related objects to be returned with the response. The related object(s) will be returned if data exists; if data does not exist, the requested object(s) will not be included in the response. | [optional] 
 **expandCustomForms** | **string**| Valid values: \&quot;addresses\&quot;            If {expand} includes any of addresses, parcels, owners, or contacts, {expandCustomForms} specifies which {expand} objects should include custom forms. By default, custom forms for addresses, parcels, owners, and contacts are not included in the response data. Note that requesting APO custom forms may have performance implications, depending on the amount of data and connectivity to any external data source. Added in Civic Platform version: 9.2.0  | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseRecordExtModel1Array**](ResponseRecordExtModel1Array.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getrecordsrecordidadditional"></a>
# **V4GetRecordsRecordIdAdditional**
> ResponseRecordAdditionalModelArray V4GetRecordsRecordIdAdditional (string contentType, string authorization, string recordId, string fields = null, string lang = null)

Get Additional Details for Record

Gets additional information for the requested record.  **API Endpoint**:  GET /v4/records/{recordId}/additional  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**:  7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdAdditionalExample
    {
        public void main()
        {
            var apiInstance = new RecordsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of record to fetch.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Additional Details for Record
                ResponseRecordAdditionalModelArray result = apiInstance.V4GetRecordsRecordIdAdditional(contentType, authorization, recordId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsApi.V4GetRecordsRecordIdAdditional: " + e.Message );
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
 **recordId** | **string**| The ID of record to fetch. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseRecordAdditionalModelArray**](ResponseRecordAdditionalModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getrecordsrecordidrelated"></a>
# **V4GetRecordsRecordIdRelated**
> ResponseRecordRelatedModelArray V4GetRecordsRecordIdRelated (string contentType, string authorization, string recordId, string relationship = null, string fields = null, string lang = null)

Get All Related Details for Record

Gets the records related, by a parent or child relation, to the specified record. **API Endpoint**:  GET /v4/records/{recordId}/related  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdRelatedExample
    {
        public void main()
        {
            var apiInstance = new RecordsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var relationship = relationship_example;  // string | Filter by record relationship. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Related Details for Record
                ResponseRecordRelatedModelArray result = apiInstance.V4GetRecordsRecordIdRelated(contentType, authorization, recordId, relationship, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsApi.V4GetRecordsRecordIdRelated: " + e.Message );
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
 **relationship** | **string**| Filter by record relationship. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseRecordRelatedModelArray**](ResponseRecordRelatedModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4postrecords"></a>
# **V4PostRecords**
> ResponseSimpleRecordModel V4PostRecords (string contentType, string authorization, RequestCreateRecordModel body = null, string fields = null, string lang = null)

Create Record

Creates a new, full record in Civic Platform. The Create Record API triggers the business rules engine event ApplicationSubmitAfter.   Note: The Create Record API does not include custom forms and custom tables in the request body. To add or update custom forms and custom tables, use the [Update Record Custom Forms](./api-records.html#operation/v4.put.records.recordId.customForms) and [Update Record Custom Tables](./api-records.html#operation/v4.put.records.recordId.customForms) after the Create Record request. **API Endpoint**:  POST /v4/records  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4PostRecordsExample
    {
        public void main()
        {
            var apiInstance = new RecordsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var body = new RequestCreateRecordModel(); // RequestCreateRecordModel | The create record information to be added. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Create Record
                ResponseSimpleRecordModel result = apiInstance.V4PostRecords(contentType, authorization, body, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsApi.V4PostRecords: " + e.Message );
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
 **body** | [**RequestCreateRecordModel**](RequestCreateRecordModel.md)| The create record information to be added. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseSimpleRecordModel**](ResponseSimpleRecordModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4postrecordsinitialize"></a>
# **V4PostRecordsInitialize**
> ResponseSimpleRecordModel V4PostRecordsInitialize (string contentType, string authorization, RequestRecordModel body, bool? isFeeEstimate = null, string fields = null, string lang = null)

Create Partial Record

Creates a partial record that allows a user to save an incomplete application that is work in-progress. To submit the completed application, use the [Finalize Record](./api-records.html#operation/v4.post.records.recordId.finalize) method. See [Creating Records](https://developer.accela.com/docs/construct-api-records.html#construct-api-records__creatingRecords) for more information about calling Create Partial Record in tandem with Finalize Record.   The Create Partial Record API triggers the business rules engine event ApplicationSubmitAfter.  Note: The Create Partial Record API does not include custom forms and custom tables in the request body. To add or update custom forms and custom tables, use the [Update Record Custom Forms](./api-records.html#operation/v4.put.records.recordId.customForms) and [Update Record Custom Tables](./api-records.html#operation/v4.put.records.recordId.customForms) between the Create Partial Record and Finalize Record requests. **API Endpoint**:  POST /v4/records/initialize  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**:  7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4PostRecordsInitializeExample
    {
        public void main()
        {
            var apiInstance = new RecordsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var body = new RequestRecordModel(); // RequestRecordModel | The record information to be initialized.
            var isFeeEstimate = true;  // bool? | Indicates whether or not it is for a fee estimate. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Create Partial Record
                ResponseSimpleRecordModel result = apiInstance.V4PostRecordsInitialize(contentType, authorization, body, isFeeEstimate, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsApi.V4PostRecordsInitialize: " + e.Message );
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
 **body** | [**RequestRecordModel**](RequestRecordModel.md)| The record information to be initialized. | 
 **isFeeEstimate** | **bool?**| Indicates whether or not it is for a fee estimate. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseSimpleRecordModel**](ResponseSimpleRecordModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4postrecordsrecordidfinalize"></a>
# **V4PostRecordsRecordIdFinalize**
> ResponseSimpleRecordModel V4PostRecordsRecordIdFinalize (string contentType, string authorization, string recordId, RequestRecordModel body, string fields = null, string lang = null)

Finalize Record

Creates the finalized record in the database. Use this method after calling Create Partial Record to submit the completed record. See [Creating Records](https://developer.accela.com/docs/construct-api-records.html#construct-api-records__creatingRecords) for more information about calling Finalize Record in tandem with Create Partial Record.   The Create Partial Record API triggers the business rules engine event ApplicationSubmitAfter.   Note: The Finalize Record API does not include custom forms and custom tables in the request body. To add or update custom forms and custom tables, use the [Update Record Custom Forms](./api-records.html#operation/v4.put.records.recordId.customForms) and [Update Record Custom Tables](./api-records.html#operation/v4.put.records.recordId.customForms) between the Create Partial Record and Finalize Record requests. **API Endpoint**:  POST /v4/records/{recordId}/finalize  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4PostRecordsRecordIdFinalizeExample
    {
        public void main()
        {
            var apiInstance = new RecordsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var body = new RequestRecordModel(); // RequestRecordModel | Create Record request information.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Finalize Record
                ResponseSimpleRecordModel result = apiInstance.V4PostRecordsRecordIdFinalize(contentType, authorization, recordId, body, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsApi.V4PostRecordsRecordIdFinalize: " + e.Message );
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
 **body** | [**RequestRecordModel**](RequestRecordModel.md)| Create Record request information. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseSimpleRecordModel**](ResponseSimpleRecordModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4postrecordsrecordidrelated"></a>
# **V4PostRecordsRecordIdRelated**
> ResponseResultModelArray V4PostRecordsRecordIdRelated (string contentType, string authorization, string recordId, List<string> body, string lang = null)

Create Related Details for Record

Creates a child relationship to the specified (parent) record. **API Endpoint**:  POST /v4/records/{recordId}/related   **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4PostRecordsRecordIdRelatedExample
    {
        public void main()
        {
            var apiInstance = new RecordsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var body = ;  // List<string> | Related record information to be added.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Create Related Details for Record
                ResponseResultModelArray result = apiInstance.V4PostRecordsRecordIdRelated(contentType, authorization, recordId, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsApi.V4PostRecordsRecordIdRelated: " + e.Message );
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
 **body** | **List&lt;string&gt;**| Related record information to be added. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4putrecordsid"></a>
# **V4PutRecordsId**
> SimpleRecordModel V4PutRecordsId (string contentType, string authorization, string id, RequestSimpleRecordModel body, string fields = null, string lang = null)

Update Record

Updates details for the specified record.  **API Endpoint**:  PUT /v4/records/{id}  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**:  7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4PutRecordsIdExample
    {
        public void main()
        {
            var apiInstance = new RecordsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = id_example;  // string | The id of the record to update.
            var body = new RequestSimpleRecordModel(); // RequestSimpleRecordModel | Record information to be updated.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Record
                SimpleRecordModel result = apiInstance.V4PutRecordsId(contentType, authorization, id, body, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsApi.V4PutRecordsId: " + e.Message );
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
 **id** | **string**| The id of the record to update. | 
 **body** | [**RequestSimpleRecordModel**](RequestSimpleRecordModel.md)| Record information to be updated. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**SimpleRecordModel**](SimpleRecordModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4putrecordsrecordidadditional"></a>
# **V4PutRecordsRecordIdAdditional**
> ResponseRecordAdditionalModelArray V4PutRecordsRecordIdAdditional (string contentType, string authorization, string recordId, RecordAdditionalModel body, string fields = null, string lang = null)

Update Additional Details for Record

Updates additional information for the specified record.  **API Endpoint**:  PUT /v4/records/{recordId}/additional  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**:  7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4PutRecordsRecordIdAdditionalExample
    {
        public void main()
        {
            var apiInstance = new RecordsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch.
            var body = new RecordAdditionalModel(); // RecordAdditionalModel | Additional record information for update.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Additional Details for Record
                ResponseRecordAdditionalModelArray result = apiInstance.V4PutRecordsRecordIdAdditional(contentType, authorization, recordId, body, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsApi.V4PutRecordsRecordIdAdditional: " + e.Message );
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
 **recordId** | **string**| The ID of the record to fetch. | 
 **body** | [**RecordAdditionalModel**](RecordAdditionalModel.md)| Additional record information for update. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseRecordAdditionalModelArray**](ResponseRecordAdditionalModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

