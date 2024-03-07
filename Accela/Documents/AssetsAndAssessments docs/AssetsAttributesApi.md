# AccelaAssetsAndAssessments.Api.AssetsAttributesApi

All URIs are relative to *https://apis.accela.com/*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4AssetsIdAttributesGet**](AssetsAttributesApi.md#v4assetsidattributesget) | **GET** /v4/assets/{id}/attributes | Get Asset Custom Attributes
[**V4GetAssetsIdAttributesMeta**](AssetsAttributesApi.md#v4getassetsidattributesmeta) | **GET** /v4/assets/{id}/attributes/meta | Get Metadata of Asset Custom Attributes
[**V4PutAssetsIdAttributes**](AssetsAttributesApi.md#v4putassetsidattributes) | **PUT** /v4/assets/{id}/attributes | Update Asset Custom Attributes

<a name="v4assetsidattributesget"></a>
# **V4AssetsIdAttributesGet**
> ResponseCustomAttributeModelArray V4AssetsIdAttributesGet (string contentType, string authorization, string id, string lang = null)

Get Asset Custom Attributes

Returns the custom attribute values for a given asset. The custom attributes are configured in an attribute template which is assigned to the asset type in Civic Platform Administration.    **API Endpoint**:  GET /v4/assets/{id}/attributes   **Scope**:  assets   **App Type**:  Agency   **Authorization Type**:  Access token   **Civic Platform version**: 9.0.0  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAssetsAndAssessments.Api;
using AccelaAssetsAndAssessments.Client;
using AccelaAssetsAndAssessments.Model;

namespace Example
{
    public class V4AssetsIdAttributesGetExample
    {
        public void main()
        {
            var apiInstance = new AssetsAttributesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = id_example;  // string | The ID of the asset to fetch. See [Get All Assets](./api-assets-assessments.html#operation/v4.get.assets).
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Asset Custom Attributes
                ResponseCustomAttributeModelArray result = apiInstance.V4AssetsIdAttributesGet(contentType, authorization, id, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AssetsAttributesApi.V4AssetsIdAttributesGet: " + e.Message );
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
 **id** | **string**| The ID of the asset to fetch. See [Get All Assets](./api-assets-assessments.html#operation/v4.get.assets). | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseCustomAttributeModelArray**](ResponseCustomAttributeModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="v4getassetsidattributesmeta"></a>
# **V4GetAssetsIdAttributesMeta**
> ResponseAttributeMetadataModelArray V4GetAssetsIdAttributesMeta (string contentType, string authorization, string id, string fields = null, string lang = null)

Get Metadata of Asset Custom Attributes

Returns an array containing the metadata of the asset custom attributes. The metadata is configured in an attributes template which is assigned to the asset type in Civic Platform Administration.    **API Endpoint**:  GET /v4/assets/{id}/attributes/meta   **Scope**:  assets   **App Type**:  Agency   **Authorization Type**:  Access token   **Civic Platform version**: 9.0.0  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAssetsAndAssessments.Api;
using AccelaAssetsAndAssessments.Client;
using AccelaAssetsAndAssessments.Model;

namespace Example
{
    public class V4GetAssetsIdAttributesMetaExample
    {
        public void main()
        {
            var apiInstance = new AssetsAttributesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = id_example;  // string | The ID of the asset to fetch.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Metadata of Asset Custom Attributes
                ResponseAttributeMetadataModelArray result = apiInstance.V4GetAssetsIdAttributesMeta(contentType, authorization, id, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AssetsAttributesApi.V4GetAssetsIdAttributesMeta: " + e.Message );
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
 **id** | **string**| The ID of the asset to fetch. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseAttributeMetadataModelArray**](ResponseAttributeMetadataModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="v4putassetsidattributes"></a>
# **V4PutAssetsIdAttributes**
> ResponseResultModelArray V4PutAssetsIdAttributes (CustomAttributeModel body, string contentType, string authorization, string id, string fields = null, string lang = null)

Update Asset Custom Attributes

Updates the custom attributes for a given asset. The response returns an array containing any custom attribute validation error and the status of the asset update.    **API Endpoint**:  PUT /v4/assets/{id}/attributes   **Scope**:  assets   **App Type**:  Agency   **Authorization Type**:  Access token   **Civic Platform version**: 9.0.0  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAssetsAndAssessments.Api;
using AccelaAssetsAndAssessments.Client;
using AccelaAssetsAndAssessments.Model;

namespace Example
{
    public class V4PutAssetsIdAttributesExample
    {
        public void main()
        {
            var apiInstance = new AssetsAttributesApi();
            var body = new CustomAttributeModel(); // CustomAttributeModel | The custom attributes to be updated. Ex. [{ "field1": "field1Val", "field2": "field2Val"}]
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = id_example;  // string | The ID of the asset to be updated.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Asset Custom Attributes
                ResponseResultModelArray result = apiInstance.V4PutAssetsIdAttributes(body, contentType, authorization, id, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AssetsAttributesApi.V4PutAssetsIdAttributes: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**CustomAttributeModel**](CustomAttributeModel.md)| The custom attributes to be updated. Ex. [{ &quot;field1&quot;: &quot;field1Val&quot;, &quot;field2&quot;: &quot;field2Val&quot;}] | 
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | 
 **authorization** | **string**| Construct oAuth2 authentication token | 
 **id** | **string**| The ID of the asset to be updated. | 
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
