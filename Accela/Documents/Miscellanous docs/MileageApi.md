# AccelaMiscellanous.Api.MileageApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4PostMileage**](MileageApi.md#v4postmileage) | **POST** /v4/mileage | Create Mileage


<a name="v4postmileage"></a>
# **V4PostMileage**
> ResponseResultModelArray V4PostMileage (string contentType, string authorization, List<MileageModel> body = null, string lang = null)

Create Mileage

Creates a vehicle mileage entry for a specified inspector on a particular date. **API Endpoint**:  POST /v4/mileage  **Scope**:  mileage  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaMiscellanous.Api;
using AccelaMiscellanous.Client;
using AccelaMiscellanous.Model;

namespace Example
{
    public class V4PostMileageExample
    {
        public void main()
        {
            var apiInstance = new MileageApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var body = new List<MileageModel>(); // List<MileageModel> | Mileage request information (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Create Mileage
                ResponseResultModelArray result = apiInstance.V4PostMileage(contentType, authorization, body, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling MileageApi.V4PostMileage: " + e.Message );
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
 **body** | [**List&lt;MileageModel&gt;**](MileageModel.md)| Mileage request information | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

