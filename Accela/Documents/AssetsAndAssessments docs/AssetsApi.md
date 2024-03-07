# AccelaAssetsAndAssessments.Api.AssetsApi

All URIs are relative to *https://apis.accela.com/*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4DeleteAssetsIds**](AssetsApi.md#v4deleteassetsids) | **DELETE** /v4/assets/{ids} | Delete Assets
[**V4GetAssets**](AssetsApi.md#v4getassets) | **GET** /v4/assets | Get All Assets
[**V4GetAssetsIdLinkedAssets**](AssetsApi.md#v4getassetsidlinkedassets) | **GET** /v4/assets/{id}/linkedAssets | Get All Linked Assets
[**V4GetAssetsIds**](AssetsApi.md#v4getassetsids) | **GET** /v4/assets/{ids} | Get Assets
[**V4PutAssetsId**](AssetsApi.md#v4putassetsid) | **PUT** /v4/Assets/{id} | Update Asset

<a name="v4deleteassetsids"></a>
# **V4DeleteAssetsIds**
> ResponseResultModelArray V4DeleteAssetsIds (string contentType, string authorization, string ids, string lang = null)

Delete Assets

Deletes one or more assets.    **API Endpoint**:  DELETE /v4/assets/{ids}   **Scope**:  assets   **App Type**:  Agency   **Authorization Type**:  Access token   **Civic Platform version**: 9.0.0  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAssetsAndAssessments.Api;
using AccelaAssetsAndAssessments.Client;
using AccelaAssetsAndAssessments.Model;

namespace Example
{
    public class V4DeleteAssetsIdsExample
    {
        public void main()
        {
            var apiInstance = new AssetsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var ids = ids_example;  // string | Comma-delimited IDs of assets to delete.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Delete Assets
                ResponseResultModelArray result = apiInstance.V4DeleteAssetsIds(contentType, authorization, ids, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AssetsApi.V4DeleteAssetsIds: " + e.Message );
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
 **ids** | **string**| Comma-delimited IDs of assets to delete. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="v4getassets"></a>
# **V4GetAssets**
> ResponseAssetMasterModelArray V4GetAssets (string contentType, string authorization, string assetId = null, string name = null, string group = null, string type = null, long? offset = null, long? limit = null, string fields = null, string lang = null)

Get All Assets

Returns all assets. Specify at least one URI filter parameter: asssetId, group, type, name.    **API Endpoint**:  GET /v4/assets   **Scope**:  assets   **App Type**:  Agency   **Authorization Type**:  Access token   **Civic Platform version**: 9.0.0  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAssetsAndAssessments.Api;
using AccelaAssetsAndAssessments.Client;
using AccelaAssetsAndAssessments.Model;

namespace Example
{
    public class V4GetAssetsExample
    {
        public void main()
        {
            var apiInstance = new AssetsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var assetId = assetId_example;  // string | Filter by asset ID.  (optional) 
            var name = name_example;  // string | Filter by asset name. (optional) 
            var group = group_example;  // string | Filter by asset group. See [Get All Asset Groups](./api-settings.html#operation/v4.get.settings.assets.groups). (optional) 
            var type = type_example;  // string | Filter by asset type. See [Get All Asset Types](./api-settings.html#operation/v4.get.settings.assettypes.types). (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Assets
                ResponseAssetMasterModelArray result = apiInstance.V4GetAssets(contentType, authorization, assetId, name, group, type, offset, limit, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AssetsApi.V4GetAssets: " + e.Message );
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
 **assetId** | **string**| Filter by asset ID.  | [optional] 
 **name** | **string**| Filter by asset name. | [optional] 
 **group** | **string**| Filter by asset group. See [Get All Asset Groups](./api-settings.html#operation/v4.get.settings.assets.groups). | [optional] 
 **type** | **string**| Filter by asset type. See [Get All Asset Types](./api-settings.html#operation/v4.get.settings.assettypes.types). | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseAssetMasterModelArray**](ResponseAssetMasterModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="v4getassetsidlinkedassets"></a>
# **V4GetAssetsIdLinkedAssets**
> ResponseAssetRelatedModelArray V4GetAssetsIdLinkedAssets (string contentType, string authorization, string id, string expand = null, string fields = null, string lang = null)

Get All Linked Assets

Returns all linked assets for a given asset ID.    **API Endpoint**:  GET /v4/assets/{id}/linkedAssets   **Scope**:  assets   **App Type**:  Agency   **Authorization Type**:  Access token   **Civic Platform version**: 9.0.0  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAssetsAndAssessments.Api;
using AccelaAssetsAndAssessments.Client;
using AccelaAssetsAndAssessments.Model;

namespace Example
{
    public class V4GetAssetsIdLinkedAssetsExample
    {
        public void main()
        {
            var apiInstance = new AssetsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = id_example;  // string | The ID of the asset whose linked assets need to be fetched.
            var expand = expand_example;  // string | Comma-delimited list of related objects to be returned with the response. The related object(s) will be returned if data exists; if data does not exist, the requested object(s) will not be included in the response. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Linked Assets
                ResponseAssetRelatedModelArray result = apiInstance.V4GetAssetsIdLinkedAssets(contentType, authorization, id, expand, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AssetsApi.V4GetAssetsIdLinkedAssets: " + e.Message );
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
 **id** | **string**| The ID of the asset whose linked assets need to be fetched. | 
 **expand** | **string**| Comma-delimited list of related objects to be returned with the response. The related object(s) will be returned if data exists; if data does not exist, the requested object(s) will not be included in the response. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseAssetRelatedModelArray**](ResponseAssetRelatedModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="v4getassetsids"></a>
# **V4GetAssetsIds**
> ResponseAssetExtModelArray V4GetAssetsIds (string contentType, string authorization, string ids, string expand = null, string fields = null, string lang = null)

Get Assets

Returns asset information for one or more given assets, identified in comma-separated {ids}. Associated records can be requested via the expand parameter.    **API Endpoint**:  GET /v4/assets/{ids}   **Scope**:  assets   **App Type**:  Agency   **Authorization Type**:  Access token   **Civic Platform version**: 9.0.0  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAssetsAndAssessments.Api;
using AccelaAssetsAndAssessments.Client;
using AccelaAssetsAndAssessments.Model;

namespace Example
{
    public class V4GetAssetsIdsExample
    {
        public void main()
        {
            var apiInstance = new AssetsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var ids = ids_example;  // string | Comma-delimited IDs of assets to fetch.
            var expand = expand_example;  // string | Comma-delimited list of related objects to be returned with the response. The related object(s) will be returned if data exists; if data does not exist, the requested object(s) will not be included in the response. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Assets
                ResponseAssetExtModelArray result = apiInstance.V4GetAssetsIds(contentType, authorization, ids, expand, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AssetsApi.V4GetAssetsIds: " + e.Message );
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
 **ids** | **string**| Comma-delimited IDs of assets to fetch. | 
 **expand** | **string**| Comma-delimited list of related objects to be returned with the response. The related object(s) will be returned if data exists; if data does not exist, the requested object(s) will not be included in the response. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseAssetExtModelArray**](ResponseAssetExtModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="v4putassetsid"></a>
# **V4PutAssetsId**
> ResponseResultModelArray V4PutAssetsId (AssetWithAttributesModel body, string contentType, string authorization, string id, string fields = null, string lang = null)

Update Asset

Updates a given asset.    **API Endpoint**:  PUT /v4/assets/{id}   **Scope**:  assets   **App Type**:  Agency   **Authorization Type**:  Access token   **Civic Platform version**: 9.0.0  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAssetsAndAssessments.Api;
using AccelaAssetsAndAssessments.Client;
using AccelaAssetsAndAssessments.Model;

namespace Example
{
    public class V4PutAssetsIdExample
    {
        public void main()
        {
            var apiInstance = new AssetsApi();
            var body = new AssetWithAttributesModel(); // AssetWithAttributesModel | The asset information to be updated.
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = id_example;  // string | The ID of the asset to fetch. See [Get All Assets](./api-assets-assessments.html#operation/v4.get.assets).
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Asset
                ResponseResultModelArray result = apiInstance.V4PutAssetsId(body, contentType, authorization, id, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AssetsApi.V4PutAssetsId: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**AssetWithAttributesModel**](AssetWithAttributesModel.md)| The asset information to be updated. | 
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | 
 **authorization** | **string**| Construct oAuth2 authentication token | 
 **id** | **string**| The ID of the asset to fetch. See [Get All Assets](./api-assets-assessments.html#operation/v4.get.assets). | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
