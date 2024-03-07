# AccelaInspections.Api.InspectorsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetInspectors**](InspectorsApi.md#v4getinspectors) | **GET** /v4/inspectors | Get All Inspectors
[**V4GetInspectorsId**](InspectorsApi.md#v4getinspectorsid) | **GET** /v4/inspectors/{id} | Get Inspector


<a name="v4getinspectors"></a>
# **V4GetInspectors**
> ResponseInspectorModelArray V4GetInspectors (string contentType, string authorization, string department = null, string lang = null)

Get All Inspectors

Gets all the inspectors in the agency.   ** API Endpoint ** : GET /v4/inspectors    ** Scope ** : inspections   ** App Type ** : Agency   ** Authorization Type ** : Access token   ** Civic Platform version ** : 7.3.2  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4GetInspectorsExample
    {
        public void main()
        {
            var apiInstance = new InspectorsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var department = department_example;  // string | Filter by department code.For example, 'HEALTH/DCA/SUPER/NA/NA/NA/INTAKE' (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Inspectors
                ResponseInspectorModelArray result = apiInstance.V4GetInspectors(contentType, authorization, department, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectorsApi.V4GetInspectors: " + e.Message );
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
 **department** | **string**| Filter by department code.For example, &#39;HEALTH/DCA/SUPER/NA/NA/NA/INTAKE&#39; | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseInspectorModelArray**](ResponseInspectorModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getinspectorsid"></a>
# **V4GetInspectorsId**
> ResponseInspectorModel V4GetInspectorsId (string contentType, string authorization, string id, string lang = null)

Get Inspector

Gets information about an inspector.   ** API Endpoint ** : GET /v4/inspectors/{id}  ** Scope ** : inspections   ** App Type ** : Agency   ** Authorization Type ** : Access token   ** Civic Platform version ** : 7.3.2  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaInspections.Api;
using AccelaInspections.Client;
using AccelaInspections.Model;

namespace Example
{
    public class V4GetInspectorsIdExample
    {
        public void main()
        {
            var apiInstance = new InspectorsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = id_example;  // string | The ID of the inspector to fetch.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Inspector
                ResponseInspectorModel result = apiInstance.V4GetInspectorsId(contentType, authorization, id, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling InspectorsApi.V4GetInspectorsId: " + e.Message );
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
 **id** | **string**| The ID of the inspector to fetch. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseInspectorModel**](ResponseInspectorModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

