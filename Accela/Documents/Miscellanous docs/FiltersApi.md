# AccelaMiscellanous.Api.FiltersApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetFilters**](FiltersApi.md#v4getfilters) | **GET** /v4/filters | Get All Filters
[**V4PostFiltersIdResults**](FiltersApi.md#v4postfiltersidresults) | **POST** /v4/filters/{id}/results | Get Filter Results


<a name="v4getfilters"></a>
# **V4GetFilters**
> ResponseFilterModelArray V4GetFilters (string contentType, string authorization, string type = null, string module = null, long? offset = null, long? limit = null, string lang = null)

Get All Filters

Returns Civic Platform search filters (previously known as \"quick queries\" in the Civic Platform application). This API returns global filters and custom filters that the currently logged in user has permissions in the requested type and module. Include the {type} URI parameter to get either a record, inspection, or workflow task filters. **API Endpoint**:  GET /v4/filters **Scope**:  filters  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 9.0.0 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaMiscellanous.Api;
using AccelaMiscellanous.Client;
using AccelaMiscellanous.Model;

namespace Example
{
    public class V4GetFiltersExample
    {
        public void main()
        {
            var apiInstance = new FiltersApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var type = type_example;  // string | Filter by Civic Platform filter type.  (optional) 
            var module = module_example;  // string | Filter by Civic Platform module. If module is not specified, the default module is the currently logged-in user's default Civic Platform module.  **Added in Civic Platform version**: 9.2.0 (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list.  **Added in Civic Platform version**: 9.2.0 (optional) 
            var limit = 789;  // long? | Search result size limit.  **Added in Civic Platform version**: 9.2.0 (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Filters
                ResponseFilterModelArray result = apiInstance.V4GetFilters(contentType, authorization, type, module, offset, limit, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling FiltersApi.V4GetFilters: " + e.Message );
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
 **type** | **string**| Filter by Civic Platform filter type.  | [optional] 
 **module** | **string**| Filter by Civic Platform module. If module is not specified, the default module is the currently logged-in user&#39;s default Civic Platform module.  **Added in Civic Platform version**: 9.2.0 | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list.  **Added in Civic Platform version**: 9.2.0 | [optional] 
 **limit** | **long?**| Search result size limit.  **Added in Civic Platform version**: 9.2.0 | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseFilterModelArray**](ResponseFilterModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4postfiltersidresults"></a>
# **V4PostFiltersIdResults**
> List<ResponseFilterResults> V4PostFiltersIdResults (string contentType, string authorization, long? id, Body body = null, long? limit = null, long? offset = null, string fields = null, string lang = null)

Get Filter Results

Returns the search results of a given search filter (also known as \"Quick Queries\" or \"Saved Searches\"). Starting with Civic Platform 9.2.0, Get Filter Results supports the following fixed format parameters for all filter types (Filter parameters are configured in Civic Platform Administration Tool > Data Filter.):  <br/>$$TODAY$$ <br/>$$TODAY+N$$ <br/>$$MODULE$$ <br/>$$USERID$$ <br/>**Note**: The $$MODULE$$ fixed format parameter is not available for inspection filters in the Civic Platform Admin Tool. To specify a module for an inspection filter, use the \"module\" field in the request payload.  **API Endpoint**:  POST /v4/filters/{id}/results  **Scope**:  filters  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 9.0.0 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaMiscellanous.Api;
using AccelaMiscellanous.Client;
using AccelaMiscellanous.Model;

namespace Example
{
    public class V4PostFiltersIdResultsExample
    {
        public void main()
        {
            var apiInstance = new FiltersApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = 789;  // long? | The ID of the filter to fetch.
            var body = new Body(); // Body | Contains optional filter parameters. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note: Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Filter Results
                List&lt;ResponseFilterResults&gt; result = apiInstance.V4PostFiltersIdResults(contentType, authorization, id, body, limit, offset, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling FiltersApi.V4PostFiltersIdResults: " + e.Message );
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
 **id** | **long?**| The ID of the filter to fetch. | 
 **body** | [**Body**](Body.md)| Contains optional filter parameters. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note: Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**List<ResponseFilterResults>**](ResponseFilterResults.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

