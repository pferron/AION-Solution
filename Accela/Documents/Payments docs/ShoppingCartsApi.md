# AccelaPayments.Api.ShoppingCartsApi

All URIs are relative to *https://apis.accela.com/*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4DeleteShoppingCartIds**](ShoppingCartsApi.md#v4deleteshoppingcartids) | **DELETE** /v4/shoppingCart/{ids} | Delete Shopping Carts
[**V4GetShoppingCart**](ShoppingCartsApi.md#v4getshoppingcart) | **GET** /v4/shoppingCart | Get All Shopping Carts
[**V4GetShoppingCartIds**](ShoppingCartsApi.md#v4getshoppingcartids) | **GET** /v4/shoppingCart/{ids} | Get Shopping Carts
[**V4PostShoppingCart**](ShoppingCartsApi.md#v4postshoppingcart) | **POST** /v4/shoppingCart | Create Shopping Cart
[**V4PutShoppingCartId**](ShoppingCartsApi.md#v4putshoppingcartid) | **PUT** /v4/ShoppingCart/{id} | Update Shopping Cart

<a name="v4deleteshoppingcartids"></a>
# **V4DeleteShoppingCartIds**
> ResponseResultModelArray V4DeleteShoppingCartIds (string contentType, string authorization, string ids, string lang = null)

Delete Shopping Carts

Deletes one or more shopping carts. **API Endpoint**:  DELETE /v4/shoppingCart/{ids}  **Scope**:  shoppingcart  **App Type**:  Citizen  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaPayments.Api;
using AccelaPayments.Client;
using AccelaPayments.Model;

namespace Example
{
    public class V4DeleteShoppingCartIdsExample
    {
        public void main()
        {
            var apiInstance = new ShoppingCartsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var ids = ids_example;  // string | One or more comma-separated id's of shopping carts to delete.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Delete Shopping Carts
                ResponseResultModelArray result = apiInstance.V4DeleteShoppingCartIds(contentType, authorization, ids, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ShoppingCartsApi.V4DeleteShoppingCartIds: " + e.Message );
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
 **ids** | **string**| One or more comma-separated id&#x27;s of shopping carts to delete. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="v4getshoppingcart"></a>
# **V4GetShoppingCart**
> ResponseShoppingCartModelArray V4GetShoppingCart (string contentType, string authorization, string fields = null, string lang = null)

Get All Shopping Carts

Gets all shopping carts in the system. **API Endpoint**:  GET /v4/shoppingCart  **Scope**:  shoppingcart  **App Type**:  Citizen  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaPayments.Api;
using AccelaPayments.Client;
using AccelaPayments.Model;

namespace Example
{
    public class V4GetShoppingCartExample
    {
        public void main()
        {
            var apiInstance = new ShoppingCartsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Shopping Carts
                ResponseShoppingCartModelArray result = apiInstance.V4GetShoppingCart(contentType, authorization, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ShoppingCartsApi.V4GetShoppingCart: " + e.Message );
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
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseShoppingCartModelArray**](ResponseShoppingCartModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="v4getshoppingcartids"></a>
# **V4GetShoppingCartIds**
> ResponseShoppingCartModelArray V4GetShoppingCartIds (string contentType, string authorization, string ids, string fields = null, string lang = null)

Get Shopping Carts

Gets information about the specified shopping carts. **API Endpoint**:  GET /v4/shoppingCart/{ids}  **Scope**:  shoppingcart  **App Type**:  Citizen  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaPayments.Api;
using AccelaPayments.Client;
using AccelaPayments.Model;

namespace Example
{
    public class V4GetShoppingCartIdsExample
    {
        public void main()
        {
            var apiInstance = new ShoppingCartsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var ids = ids_example;  // string | One or more comma-separated id's of shopping carts to fetch.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Shopping Carts
                ResponseShoppingCartModelArray result = apiInstance.V4GetShoppingCartIds(contentType, authorization, ids, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ShoppingCartsApi.V4GetShoppingCartIds: " + e.Message );
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
 **ids** | **string**| One or more comma-separated id&#x27;s of shopping carts to fetch. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseShoppingCartModelArray**](ResponseShoppingCartModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="v4postshoppingcart"></a>
# **V4PostShoppingCart**
> ResponseResultModelArray V4PostShoppingCart (List<ShoppingCartModel> body, string contentType, string authorization, string lang = null)

Create Shopping Cart

Creates a shopping cart. **API Endpoint**:  POST /v4/shoppingCart  **Scope**:  shoppingcart  **App Type**:  Citizen  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaPayments.Api;
using AccelaPayments.Client;
using AccelaPayments.Model;

namespace Example
{
    public class V4PostShoppingCartExample
    {
        public void main()
        {
            var apiInstance = new ShoppingCartsApi();
            var body = new List<ShoppingCartModel>(); // List<ShoppingCartModel> | Shopping cart item models for create
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Create Shopping Cart
                ResponseResultModelArray result = apiInstance.V4PostShoppingCart(body, contentType, authorization, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ShoppingCartsApi.V4PostShoppingCart: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**List&lt;ShoppingCartModel&gt;**](ShoppingCartModel.md)| Shopping cart item models for create | 
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | 
 **authorization** | **string**| Construct oAuth2 authentication token | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="v4putshoppingcartid"></a>
# **V4PutShoppingCartId**
> ResponseShoppingCartModel V4PutShoppingCartId (ShoppingCartModel body, string contentType, string authorization, string id, string fields = null, string lang = null)

Update Shopping Cart

Updates the specified shopping cart. **API Endpoint**:  PUT /v4/shoppingCart/{id}  **Scope**:  shoppingcart  **App Type**:  Citizen  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaPayments.Api;
using AccelaPayments.Client;
using AccelaPayments.Model;

namespace Example
{
    public class V4PutShoppingCartIdExample
    {
        public void main()
        {
            var apiInstance = new ShoppingCartsApi();
            var body = new ShoppingCartModel(); // ShoppingCartModel | Shopping cart information to update
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = id_example;  // string | The system id of the shopping cart to update.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Update Shopping Cart
                ResponseShoppingCartModel result = apiInstance.V4PutShoppingCartId(body, contentType, authorization, id, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ShoppingCartsApi.V4PutShoppingCartId: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**ShoppingCartModel**](ShoppingCartModel.md)| Shopping cart information to update | 
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | 
 **authorization** | **string**| Construct oAuth2 authentication token | 
 **id** | **string**| The system id of the shopping cart to update. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseShoppingCartModel**](ResponseShoppingCartModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
