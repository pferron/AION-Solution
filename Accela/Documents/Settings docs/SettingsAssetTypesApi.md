# AccelaSettings.Api.SettingsAssetTypesApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetSettingsAssettypesClassTypes**](SettingsAssetTypesApi.md#v4getsettingsassettypesclasstypes) | **GET** /v4/settings/assettypes/classTypes | Get All Asset Class Types
[**V4GetSettingsAssettypesRatingTypes**](SettingsAssetTypesApi.md#v4getsettingsassettypesratingtypes) | **GET** /v4/settings/assettypes/ratingTypes | Get All Asset Rating Types
[**V4GetSettingsAssettypesRecordtypes**](SettingsAssetTypesApi.md#v4getsettingsassettypesrecordtypes) | **GET** /v4/settings/assettypes/recordtypes | Get All Record Types for Asset Type
[**V4GetSettingsAssettypesTypes**](SettingsAssetTypesApi.md#v4getsettingsassettypestypes) | **GET** /v4/settings/assettypes/types | Get All Asset Types
[**V4GetSettingsAssettypesUsageTypes**](SettingsAssetTypesApi.md#v4getsettingsassettypesusagetypes) | **GET** /v4/settings/assettypes/usageTypes | Get All Asset Usage Types


<a name="v4getsettingsassettypesclasstypes"></a>
# **V4GetSettingsAssettypesClassTypes**
> InlineResponse200 V4GetSettingsAssettypesClassTypes (string contentType, string authorization, long? offset = null, long? limit = null, string fields = null, string lang = null)

Get All Asset Class Types

Returns all out-of-the-box asset class types in Civic Platform. **API Endpoint**:  GET /v4/settings/assettypes/classTypes  **Scope**:  assets  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 9.0.0 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsAssettypesClassTypesExample
    {
        public void main()
        {
            var apiInstance = new SettingsAssetTypesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Asset Class Types
                InlineResponse200 result = apiInstance.V4GetSettingsAssettypesClassTypes(contentType, authorization, offset, limit, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsAssetTypesApi.V4GetSettingsAssettypesClassTypes: " + e.Message );
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
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**InlineResponse200**](InlineResponse200.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getsettingsassettypesratingtypes"></a>
# **V4GetSettingsAssettypesRatingTypes**
> ResponseRatingTypeModelArray V4GetSettingsAssettypesRatingTypes (string contentType, string assetGroup, string assetType, long? offset = null, long? limit = null, string fields = null, string lang = null)

Get All Asset Rating Types

Returns all configured asset rating types. Specify both {assetType} and {assetGroup} parameters to filter by asset type and asset group. **API Endpoint**:  GET /v4/settings/assettypes/ratingTypes  **Scope**:  assets  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 9.0.0 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsAssettypesRatingTypesExample
    {
        public void main()
        {
            var apiInstance = new SettingsAssetTypesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var assetGroup = assetGroup_example;  // string | Filter by asset group. For example: Water or Street. An Asset Group is an agency-defined collection of objects the agency owns or maintains.
            var assetType = assetType_example;  // string | Filter by type of asset. For example: Hydrant or Manhole. An Asset Type is an agencydefined classification of similar objects that share the same standard asset attributes. Related asset types belong to an asset group, and multiple asset types can share the same Class Type. See [Get All Asset Types](./api-settings.html#operation/v4.get.settings.assettypes.types).
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Asset Rating Types
                ResponseRatingTypeModelArray result = apiInstance.V4GetSettingsAssettypesRatingTypes(contentType, assetGroup, assetType, offset, limit, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsAssetTypesApi.V4GetSettingsAssettypesRatingTypes: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | [default to application/x-www-form-urlencoded]
 **assetGroup** | **string**| Filter by asset group. For example: Water or Street. An Asset Group is an agency-defined collection of objects the agency owns or maintains. | 
 **assetType** | **string**| Filter by type of asset. For example: Hydrant or Manhole. An Asset Type is an agencydefined classification of similar objects that share the same standard asset attributes. Related asset types belong to an asset group, and multiple asset types can share the same Class Type. See [Get All Asset Types](./api-settings.html#operation/v4.get.settings.assettypes.types). | 
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseRatingTypeModelArray**](ResponseRatingTypeModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getsettingsassettypesrecordtypes"></a>
# **V4GetSettingsAssettypesRecordtypes**
> ResponseAssetRecordTypeModelArray V4GetSettingsAssettypesRecordtypes (string contentType, string authorization, string assetGroup, string assetType = null, long? offset = null, long? limit = null, string fields = null, string lang = null)

Get All Record Types for Asset Type

Returns all configured record types for a given asset type or asset group. **API Endpoint**:  GET /v4/settings/assettypes/recordtypes  **Scope**:  settings  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 9.0.0 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsAssettypesRecordtypesExample
    {
        public void main()
        {
            var apiInstance = new SettingsAssetTypesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var assetGroup = assetGroup_example;  // string | Filter by asset group. For example: Water or Street. An Asset Group is an agency-defined collection of objects the agency owns or maintains.
            var assetType = assetType_example;  // string | Filter by type of asset. For example: Hydrant or Manhole. An Asset Type is an agencydefined classification of similar objects that share the same standard asset attributes. Related asset types belong to an asset group, and multiple asset types can share the same Class Type. See [Get All Asset Types](./api-settings.html#operation/v4.get.settings.assettypes.types). (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Record Types for Asset Type
                ResponseAssetRecordTypeModelArray result = apiInstance.V4GetSettingsAssettypesRecordtypes(contentType, authorization, assetGroup, assetType, offset, limit, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsAssetTypesApi.V4GetSettingsAssettypesRecordtypes: " + e.Message );
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
 **assetGroup** | **string**| Filter by asset group. For example: Water or Street. An Asset Group is an agency-defined collection of objects the agency owns or maintains. | 
 **assetType** | **string**| Filter by type of asset. For example: Hydrant or Manhole. An Asset Type is an agencydefined classification of similar objects that share the same standard asset attributes. Related asset types belong to an asset group, and multiple asset types can share the same Class Type. See [Get All Asset Types](./api-settings.html#operation/v4.get.settings.assettypes.types). | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseAssetRecordTypeModelArray**](ResponseAssetRecordTypeModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getsettingsassettypestypes"></a>
# **V4GetSettingsAssettypesTypes**
> ResponseAssetTypeModelArray V4GetSettingsAssettypesTypes (string contentType, string authorization, string assetGroup = null, long? offset = null, long? limit = null, string fields = null, string lang = null)

Get All Asset Types

Returns all asset types, which can be filtered by asset group. **API Endpoint**: GET /v4/settings/assettypes/types  **Scope** : assets **App Type** : Agency **Authorization Type** : Access token **Civic Platform version** : 9.0.0  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsAssettypesTypesExample
    {
        public void main()
        {
            var apiInstance = new SettingsAssetTypesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var assetGroup = assetGroup_example;  // string | Filter by asset group. (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Asset Types
                ResponseAssetTypeModelArray result = apiInstance.V4GetSettingsAssettypesTypes(contentType, authorization, assetGroup, offset, limit, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsAssetTypesApi.V4GetSettingsAssettypesTypes: " + e.Message );
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
 **assetGroup** | **string**| Filter by asset group. | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseAssetTypeModelArray**](ResponseAssetTypeModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getsettingsassettypesusagetypes"></a>
# **V4GetSettingsAssettypesUsageTypes**
> ResponseAssetUnitTypeModelArray V4GetSettingsAssettypesUsageTypes (string contentType, string assetGroup = null, string assetType = null, long? offset = null, long? limit = null, string fields = null, string lang = null)

Get All Asset Usage Types

Returns all configured asset usage types. **API Endpoint**:  GET /v4/settings/assettypes/usageTypes  **Scope**:  assets  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 9.0.0 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsAssettypesUsageTypesExample
    {
        public void main()
        {
            var apiInstance = new SettingsAssetTypesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var assetGroup = assetGroup_example;  // string | Asset group filter (optional) 
            var assetType = assetType_example;  // string | Asset type filter (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Asset Usage Types
                ResponseAssetUnitTypeModelArray result = apiInstance.V4GetSettingsAssettypesUsageTypes(contentType, assetGroup, assetType, offset, limit, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsAssetTypesApi.V4GetSettingsAssettypesUsageTypes: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | [default to application/x-www-form-urlencoded]
 **assetGroup** | **string**| Asset group filter | [optional] 
 **assetType** | **string**| Asset type filter | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseAssetUnitTypeModelArray**](ResponseAssetUnitTypeModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

