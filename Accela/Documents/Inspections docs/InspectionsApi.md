# AccelaInspections.Api.InspectionsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4DeleteInspectionsIdRelatedChildIds**](InspectionsApi.md#v4deleteinspectionsidrelatedchildids) | **DELETE** /v4/inspections/{id}/related/{childIds} | Delete Related Inspections
[**V4DeleteInspectionsIds**](InspectionsApi.md#v4deleteinspectionsids) | **DELETE** /v4/Inspections/{ids} | Delete Inspections
[**V4DeleteInspectionsIdsCancel**](InspectionsApi.md#v4deleteinspectionsidscancel) | **DELETE** /v4/inspections/{ids}/cancel | Cancel Inspections
[**V4GetInspections**](InspectionsApi.md#v4getinspections) | **GET** /v4/inspections | Get All Inspections
[**V4GetInspectionsAvailableDates**](InspectionsApi.md#v4getinspectionsavailabledates) | **GET** /v4/inspections/availableDates | Get All Available Dates for Inspection
[**V4GetInspectionsCheckAvailability**](InspectionsApi.md#v4getinspectionscheckavailability) | **GET** /v4/inspections/checkAvailability | Check Inspection Availability
[**V4GetInspectionsIdRelated**](InspectionsApi.md#v4getinspectionsidrelated) | **GET** /v4/inspections/{id}/related | Get Related Inspections
[**V4GetInspectionsIds**](InspectionsApi.md#v4getinspectionsids) | **GET** /v4/Inspections/{ids} | Get Inspections
[**V4GetInspectionsInspectionIdComments**](InspectionsApi.md#v4getinspectionsinspectionidcomments) | **GET** /v4/inspections/{inspectionId}/comments | Get All Comments for Inspection
[**V4GetInspectionsInspectionIdsHistories**](InspectionsApi.md#v4getinspectionsinspectionidshistories) | **GET** /v4/inspections/{inspectionIds}/histories | Get Inspection History
[**V4PostInspectionsIdRelated**](InspectionsApi.md#v4postinspectionsidrelated) | **POST** /v4/inspections/{id}/related | Create Related Inspections
[**V4PostInspectionsSchedule**](InspectionsApi.md#v4postinspectionsschedule) | **POST** /v4/inspections/schedule | Schedule Inspection
[**V4PutInspectionsId**](InspectionsApi.md#v4putinspectionsid) | **PUT** /v4/inspections/{id} | Update Inspection
[**V4PutInspectionsIdReschedule**](InspectionsApi.md#v4putinspectionsidreschedule) | **PUT** /v4/inspections/{id}/reschedule | Reschedule Inspection
[**V4PutInspectionsIdResult**](InspectionsApi.md#v4putinspectionsidresult) | **PUT** /v4/inspections/{id}/result | Result Inspection
[**V4PutInspectionsIdSchedule**](InspectionsApi.md#v4putinspectionsidschedule) | **PUT** /v4/inspections/{id}/schedule | Schedule Pending Inspection
[**V4PutInspectionsIdsAssign**](InspectionsApi.md#v4putinspectionsidsassign) | **PUT** /v4/inspections/{ids}/assign | Assign Inspections


<a name="v4deleteinspectionsidrelatedchildids"></a>
# **V4DeleteInspectionsIdRelatedChildIds**
> ResponseResultModelArray V4DeleteInspectionsIdRelatedChildIds (string contentType, string authorization, string id, string childIds, string lang = null)

Delete Related Inspections

Deletes one or more related (child) inspections from the specified parent inspection. **API Endpoint**:  DELETE /v4/inspections/{id}/related/{childIds}  **Scope**:  inspections  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4DeleteInspectionsIdRelatedChildIdsExample
    {
        public void main()
        {
            var apiInstance = new InspectionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = id_example;  // string | The ID of the inspection to fetch.
            var childIds = childIds_example;  // string | Comma-delimited IDs of child inspections to be deleted.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Delete Related Inspections
                ResponseResultModelArray result = apiInstance.V4DeleteInspectionsIdRelatedChildIds(contentType, authorization, id, childIds, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsApi.V4DeleteInspectionsIdRelatedChildIds: " + e.Message );
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
 **id** | **string**| The ID of the inspection to fetch. | 
 **childIds** | **string**| Comma-delimited IDs of child inspections to be deleted. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4deleteinspectionsids"></a>
# **V4DeleteInspectionsIds**
> ResponseResultModelArray V4DeleteInspectionsIds (string contentType, string authorization, string ids, string lang = null)

Delete Inspections

Deletes one or more specified inspections. **API Endpoint**:  DELETE /v4/inspections/{ids}  **Scope**:  inspections  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4DeleteInspectionsIdsExample
    {
        public void main()
        {
            var apiInstance = new InspectionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var ids = ids_example;  // string | Comma-delimited IDs of the inspections to be deleted.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Delete Inspections
                ResponseResultModelArray result = apiInstance.V4DeleteInspectionsIds(contentType, authorization, ids, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsApi.V4DeleteInspectionsIds: " + e.Message );
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
 **ids** | **string**| Comma-delimited IDs of the inspections to be deleted. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4deleteinspectionsidscancel"></a>
# **V4DeleteInspectionsIdsCancel**
> ResponseResultModelArray V4DeleteInspectionsIdsCancel (string contentType, string authorization, string ids, string lang = null)

Cancel Inspections

Cancels scheduled inspections. **API Endpoint**:  DELETE /v4/inspections/{ids}/cancel  **Scope**:  inspections  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4DeleteInspectionsIdsCancelExample
    {
        public void main()
        {
            var apiInstance = new InspectionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var ids = ids_example;  // string | Comma-delimited IDs of the inspections to cancel.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Cancel Inspections
                ResponseResultModelArray result = apiInstance.V4DeleteInspectionsIdsCancel(contentType, authorization, ids, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsApi.V4DeleteInspectionsIdsCancel: " + e.Message );
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
 **ids** | **string**| Comma-delimited IDs of the inspections to cancel. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getinspections"></a>
# **V4GetInspections**
> ResponseInspectionModelArray V4GetInspections (string contentType, string authorization, string types = null, string scheduledDateFrom = null, string scheduledDateTo = null, string inspectorIds = null, string districtIds = null, long? offset = null, long? limit = null, string module = null, string lang = null)

Get All Inspections

Gets a list of inspections stored in the system. **API Endpoint**:  GET /v4/inspections  **Scope**:  inspections  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4GetInspectionsExample
    {
        public void main()
        {
            var apiInstance = new InspectionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var types = types_example;  // string | Filter by multiple (comma-delimited) inspection types. See [Get All Inspection Types](./api-settings.html#operation/v4.get.settings.inspections.types). (optional) 
            var scheduledDateFrom = scheduledDateFrom_example;  // string | The start date of schedule date range filter. Use the date format yyyy-mm-dd. (optional) 
            var scheduledDateTo = scheduledDateTo_example;  // string | The end date of schedule date range filter. Use the date format yyyy-mm-dd. (optional) 
            var inspectorIds = inspectorIds_example;  // string | Filter by inspector IDs. See [Get All Inspectors](./api-inspections.html#operation/v4.get.inspectors). (optional) 
            var districtIds = districtIds_example;  // string | Filter by district IDs. (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var module = module_example;  // string | Filter by module. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Inspections
                ResponseInspectionModelArray result = apiInstance.V4GetInspections(contentType, authorization, types, scheduledDateFrom, scheduledDateTo, inspectorIds, districtIds, offset, limit, module, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsApi.V4GetInspections: " + e.Message );
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
 **types** | **string**| Filter by multiple (comma-delimited) inspection types. See [Get All Inspection Types](./api-settings.html#operation/v4.get.settings.inspections.types). | [optional] 
 **scheduledDateFrom** | **string**| The start date of schedule date range filter. Use the date format yyyy-mm-dd. | [optional] 
 **scheduledDateTo** | **string**| The end date of schedule date range filter. Use the date format yyyy-mm-dd. | [optional] 
 **inspectorIds** | **string**| Filter by inspector IDs. See [Get All Inspectors](./api-inspections.html#operation/v4.get.inspectors). | [optional] 
 **districtIds** | **string**| Filter by district IDs. | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 
 **module** | **string**| Filter by module. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseInspectionModelArray**](ResponseInspectionModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getinspectionsavailabledates"></a>
# **V4GetInspectionsAvailableDates**
> List<DateTime?> V4GetInspectionsAvailableDates (string contentType, string authorization, string recordId, string startDate, long? typeId = null, long? limit = null, long? offset = null, string fields = null, string lang = null)

Get All Available Dates for Inspection

Gets available dates for scheduling an inspection, starting on the specified {startDate}. This API allows a date range of up to 31 days. Note that this API does not filter the available dates based on the Schedule Cut-off Time and Schedule Number of Days Out fields on the Civic Platform inspection calendar. To filter the available dates based on the Schedule Cut-off Time and Schedule Number of Days Out inspection calendar fields, set both {validateScheduleNumOfDays} and {validateScheduleNumOfDays} parameters to true. **API Endpoint**:  GET /v4/inspections/availableDates  **Scope**:  inspections  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4GetInspectionsAvailableDatesExample
    {
        public void main()
        {
            var apiInstance = new InspectionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | Filter by record id.
            var startDate = startDate_example;  // string | Filter by start date.
            var typeId = 789;  // long? | Filter by inspection type id. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Available Dates for Inspection
                List&lt;DateTime?&gt; result = apiInstance.V4GetInspectionsAvailableDates(contentType, authorization, recordId, startDate, typeId, limit, offset, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsApi.V4GetInspectionsAvailableDates: " + e.Message );
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
 **recordId** | **string**| Filter by record id. | 
 **startDate** | **string**| Filter by start date. | 
 **typeId** | **long?**| Filter by inspection type id. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

**List<DateTime?>**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getinspectionscheckavailability"></a>
# **V4GetInspectionsCheckAvailability**
> ResponseInspectionAvailabilityArray V4GetInspectionsCheckAvailability (string contentType, string authorization, string recordId, string inspectionTypeId, string inspectionId = null, string department = null, string startDate = null, string endDate = null, string fields = null, string lang = null)

Check Inspection Availability

Checks inspection availability for a given record and inspection type. This API returns an array of inspectors and their available inspection dates and times, based on the record's inspection workflow, calendar, and permissions. The results include available dates for the current and following months.  **API Endpoint**:  GET /v4/inspections/checkAvailability  **Scope**:  inspections  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3.4 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4GetInspectionsCheckAvailabilityExample
    {
        public void main()
        {
            var apiInstance = new InspectionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The record to check. See [Get Records](./api-records.html#operation/v4.get.records.ids).
            var inspectionTypeId = inspectionTypeId_example;  // string | The inspection type to check for the specified record. See [Get All Inspection Types for Record](./api-records.html#operation/v4.get.records.recordIds.inspectionTypes).
            var inspectionId = inspectionId_example;  // string | Filter by inspection id. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections). (optional) 
            var department = department_example;  // string | Filter by department. See [Get All Departments](./api-settings.html#operation/v4.get.settings.departments). (optional) 
            var startDate = startDate_example;  // string | Filter by start date, using the date format yyyy-mm-dd. (optional) 
            var endDate = endDate_example;  // string | Filter by end date, using the date format yyyy-mm-dd. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Check Inspection Availability
                ResponseInspectionAvailabilityArray result = apiInstance.V4GetInspectionsCheckAvailability(contentType, authorization, recordId, inspectionTypeId, inspectionId, department, startDate, endDate, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsApi.V4GetInspectionsCheckAvailability: " + e.Message );
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
 **recordId** | **string**| The record to check. See [Get Records](./api-records.html#operation/v4.get.records.ids). | 
 **inspectionTypeId** | **string**| The inspection type to check for the specified record. See [Get All Inspection Types for Record](./api-records.html#operation/v4.get.records.recordIds.inspectionTypes). | 
 **inspectionId** | **string**| Filter by inspection id. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections). | [optional] 
 **department** | **string**| Filter by department. See [Get All Departments](./api-settings.html#operation/v4.get.settings.departments). | [optional] 
 **startDate** | **string**| Filter by start date, using the date format yyyy-mm-dd. | [optional] 
 **endDate** | **string**| Filter by end date, using the date format yyyy-mm-dd. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseInspectionAvailabilityArray**](ResponseInspectionAvailabilityArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getinspectionsidrelated"></a>
# **V4GetInspectionsIdRelated**
> List<ResponseInspectionRelatedModelArray> V4GetInspectionsIdRelated (string contentType, string authorization, long? id, string relationship = null, string fields = null, string lang = null)

Get Related Inspections

Gets the related (child) inspections for the specified parent inspection. **API Endpoint**:  GET /v4/inspections/{id}/related  **Scope**:  inspections  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4GetInspectionsIdRelatedExample
    {
        public void main()
        {
            var apiInstance = new InspectionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = 789;  // long? | The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections).
            var relationship = relationship_example;  // string | Filter by type of inspection relationship (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Related Inspections
                List&lt;ResponseInspectionRelatedModelArray&gt; result = apiInstance.V4GetInspectionsIdRelated(contentType, authorization, id, relationship, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsApi.V4GetInspectionsIdRelated: " + e.Message );
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
 **id** | **long?**| The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections). | 
 **relationship** | **string**| Filter by type of inspection relationship | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**List<ResponseInspectionRelatedModelArray>**](ResponseInspectionRelatedModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getinspectionsids"></a>
# **V4GetInspectionsIds**
> ResponseInspectionModelArray V4GetInspectionsIds (string contentType, string authorization, string ids, string fields = null, string lang = null)

Get Inspections

Gets the information for one or more requested inspections. **API Endpoint**:  GET /v4/inspections/{ids}  **Scope**:  inspections  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4GetInspectionsIdsExample
    {
        public void main()
        {
            var apiInstance = new InspectionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var ids = ids_example;  // string | Comma-delimited IDs of the inspections to fetch.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Inspections
                ResponseInspectionModelArray result = apiInstance.V4GetInspectionsIds(contentType, authorization, ids, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsApi.V4GetInspectionsIds: " + e.Message );
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
 **ids** | **string**| Comma-delimited IDs of the inspections to fetch. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseInspectionModelArray**](ResponseInspectionModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getinspectionsinspectionidcomments"></a>
# **V4GetInspectionsInspectionIdComments**
> ResponseInspectionCommentModelArray V4GetInspectionsInspectionIdComments (string contentType, string authorization, long? inspectionId, string fields = null, string lang = null)

Get All Comments for Inspection

Gets the comments for the specified inspection. **API Endpoint**:  GET /v4/inspections/{inspectionId}/comments  **Scope**:  inspections  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4GetInspectionsInspectionIdCommentsExample
    {
        public void main()
        {
            var apiInstance = new InspectionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var inspectionId = 789;  // long? | The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections).
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Comments for Inspection
                ResponseInspectionCommentModelArray result = apiInstance.V4GetInspectionsInspectionIdComments(contentType, authorization, inspectionId, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsApi.V4GetInspectionsInspectionIdComments: " + e.Message );
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
 **inspectionId** | **long?**| The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections). | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseInspectionCommentModelArray**](ResponseInspectionCommentModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getinspectionsinspectionidshistories"></a>
# **V4GetInspectionsInspectionIdsHistories**
> ResponseInspectionModelArray V4GetInspectionsInspectionIdsHistories (string contentType, string authorization, string inspectionIds, string fields = null, string lang = null)

Get Inspection History

Gets the history for the specified inspections. **API Endpoint**: GET /v4/inspections/{inspectionIds}/histories   **Scope**:  inspections  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4GetInspectionsInspectionIdsHistoriesExample
    {
        public void main()
        {
            var apiInstance = new InspectionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var inspectionIds = inspectionIds_example;  // string | Comma-delimited IDs of inspections to fetch.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Inspection History
                ResponseInspectionModelArray result = apiInstance.V4GetInspectionsInspectionIdsHistories(contentType, authorization, inspectionIds, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsApi.V4GetInspectionsInspectionIdsHistories: " + e.Message );
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
 **inspectionIds** | **string**| Comma-delimited IDs of inspections to fetch. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseInspectionModelArray**](ResponseInspectionModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4postinspectionsidrelated"></a>
# **V4PostInspectionsIdRelated**
> ResponseResultModelArray V4PostInspectionsIdRelated (string contentType, string authorization, long? id, List<long?> body, string lang = null)

Create Related Inspections

Adds related (child) inspections to the specified parent inspection. **API Endpoint**:  POST /v4/inspections/{id}/related  **Scope**:  inspections  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4PostInspectionsIdRelatedExample
    {
        public void main()
        {
            var apiInstance = new InspectionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = 789;  // long? | The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections).
            var body = ;  // List<long?> | An array of inspection IDs to be linked to the parent inspection. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Get Related Inspections](./api-inspections.html#operation/v4.get.inspections.id.related).
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Create Related Inspections
                ResponseResultModelArray result = apiInstance.V4PostInspectionsIdRelated(contentType, authorization, id, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsApi.V4PostInspectionsIdRelated: " + e.Message );
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
 **id** | **long?**| The ID of the inspection to fetch. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Search Inspections](./api-search.html#operation/v4.post.search.inspections). | 
 **body** | **List&lt;long?&gt;**| An array of inspection IDs to be linked to the parent inspection. See [Get All Inspections](./api-inspections.html#operation/v4.get.inspections), [Get Related Inspections](./api-inspections.html#operation/v4.get.inspections.id.related). | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4postinspectionsschedule"></a>
# **V4PostInspectionsSchedule**
> ResponseInspectionModel V4PostInspectionsSchedule (string contentType, string authorization, RequestScheduleInspectionModel body, string lang = null)

Schedule Inspection

Creates an inspection with the specified inspection and scheduling information. The Schedule Inspection API automatically sets the inspection status to \"Scheduled\"and category to \"Insp Scheduled\". **API Endpoint**:  POST /v4/inspections/schedule  **Scope**:  inspections  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4PostInspectionsScheduleExample
    {
        public void main()
        {
            var apiInstance = new InspectionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var body = new RequestScheduleInspectionModel(); // RequestScheduleInspectionModel | The inspection to schedule.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Schedule Inspection
                ResponseInspectionModel result = apiInstance.V4PostInspectionsSchedule(contentType, authorization, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsApi.V4PostInspectionsSchedule: " + e.Message );
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
 **body** | [**RequestScheduleInspectionModel**](RequestScheduleInspectionModel.md)| The inspection to schedule. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseInspectionModel**](ResponseInspectionModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4putinspectionsid"></a>
# **V4PutInspectionsId**
> ResponseInspectionModel V4PutInspectionsId (string contentType, string authorization, RequestUpdateInspectionModel body, long? id, string lang = null)

Update Inspection

Updates an inspection with the specified inspection details such as schedule date, time, and inspector. The Update Inspection API updates the inspection status and category with the specified status and category request fields. **API Endpoint**:  PUT /v4/inspections/{id}  **Scope**:  inspections  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4PutInspectionsIdExample
    {
        public void main()
        {
            var apiInstance = new InspectionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var body = new RequestUpdateInspectionModel(); // RequestUpdateInspectionModel | The inspection information to be updated.
            var id = 789;  // long? | The ID of the inspection to update.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Inspection
                ResponseInspectionModel result = apiInstance.V4PutInspectionsId(contentType, authorization, body, id, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsApi.V4PutInspectionsId: " + e.Message );
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
 **body** | [**RequestUpdateInspectionModel**](RequestUpdateInspectionModel.md)| The inspection information to be updated. | 
 **id** | **long?**| The ID of the inspection to update. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseInspectionModel**](ResponseInspectionModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4putinspectionsidreschedule"></a>
# **V4PutInspectionsIdReschedule**
> ResponseInspectionModel V4PutInspectionsIdReschedule (string contentType, string authorization, RequestRescheduleInspectionModel body, long? id, string lang = null)

Reschedule Inspection

Updates an inspection with the specified schedule date, time, inspectorId, and comments. The Reschedule Inspection API automatically sets the inspection status to \"Scheduled\"and category to \"Insp Scheduled\". **API Endpoint**:  PUT /v4/inspections/{id}/reschedule  **Scope**:  inspections  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4PutInspectionsIdRescheduleExample
    {
        public void main()
        {
            var apiInstance = new InspectionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var body = new RequestRescheduleInspectionModel(); // RequestRescheduleInspectionModel | The inspection information to reschedule.
            var id = 789;  // long? | Inspection Id
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Reschedule Inspection
                ResponseInspectionModel result = apiInstance.V4PutInspectionsIdReschedule(contentType, authorization, body, id, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsApi.V4PutInspectionsIdReschedule: " + e.Message );
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
 **body** | [**RequestRescheduleInspectionModel**](RequestRescheduleInspectionModel.md)| The inspection information to reschedule. | 
 **id** | **long?**| Inspection Id | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseInspectionModel**](ResponseInspectionModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4putinspectionsidresult"></a>
# **V4PutInspectionsIdResult**
> ResponseInspectionModel V4PutInspectionsIdResult (string contentType, string authorization, RequestUpdateInspectionModel body, long? id, string lang = null)

Result Inspection

Provides the results of a specified inspection. **API Endpoint**:  PUT /v4/inspections/{id}/result  **Scope**:  inspections  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4PutInspectionsIdResultExample
    {
        public void main()
        {
            var apiInstance = new InspectionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var body = new RequestUpdateInspectionModel(); // RequestUpdateInspectionModel | The inspection result to update.
            var id = 789;  // long? | Inspection Id
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Result Inspection
                ResponseInspectionModel result = apiInstance.V4PutInspectionsIdResult(contentType, authorization, body, id, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsApi.V4PutInspectionsIdResult: " + e.Message );
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
 **body** | [**RequestUpdateInspectionModel**](RequestUpdateInspectionModel.md)| The inspection result to update. | 
 **id** | **long?**| Inspection Id | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseInspectionModel**](ResponseInspectionModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4putinspectionsidschedule"></a>
# **V4PutInspectionsIdSchedule**
> ResponseInspectionModel V4PutInspectionsIdSchedule (string contentType, string authorization, RequestScheduleInspectionModel body, long? id, string lang = null)

Schedule Pending Inspection

Updates a pending inspection with the specified inspection details such as schedule date, time, and inspector. The Schedule Pending Inspection API automatically sets the inspection status to \"Scheduled\"and category to \"Insp Scheduled\". **API Endpoint**:  PUT /v4/inspections/{id}/schedule  **Scope**:  inspections  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4PutInspectionsIdScheduleExample
    {
        public void main()
        {
            var apiInstance = new InspectionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var body = new RequestScheduleInspectionModel(); // RequestScheduleInspectionModel | The inspection information to update.
            var id = 789;  // long? | Inspection Id
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Schedule Pending Inspection
                ResponseInspectionModel result = apiInstance.V4PutInspectionsIdSchedule(contentType, authorization, body, id, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsApi.V4PutInspectionsIdSchedule: " + e.Message );
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
 **body** | [**RequestScheduleInspectionModel**](RequestScheduleInspectionModel.md)| The inspection information to update. | 
 **id** | **long?**| Inspection Id | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseInspectionModel**](ResponseInspectionModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4putinspectionsidsassign"></a>
# **V4PutInspectionsIdsAssign**
> ResponseResultModelArray V4PutInspectionsIdsAssign (string contentType, string authorization, string ids, string inspectorId, string lang = null)

Assign Inspections

Assigns an inspector to the specified inspection. **API Endpoint**:  PUT /v4/inspections/{ids}/assign  **Scope**:  inspections  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4PutInspectionsIdsAssignExample
    {
        public void main()
        {
            var apiInstance = new InspectionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var ids = ids_example;  // string | Comma-delimited IDs of the inspections to assign
            var inspectorId = inspectorId_example;  // string | The inspector to assign to.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Assign Inspections
                ResponseResultModelArray result = apiInstance.V4PutInspectionsIdsAssign(contentType, authorization, ids, inspectorId, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectionsApi.V4PutInspectionsIdsAssign: " + e.Message );
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
 **ids** | **string**| Comma-delimited IDs of the inspections to assign | 
 **inspectorId** | **string**| The inspector to assign to. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

