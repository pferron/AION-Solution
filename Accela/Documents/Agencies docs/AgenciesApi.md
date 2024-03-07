# AccelaAgencies.Api.AgenciesApi

All URIs are relative to *https://apis.accela.com/*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetAgencies**](AgenciesApi.md#v4getagencies) | **GET** /v4/agencies | Get All Agencies
[**V4GetAgenciesName**](AgenciesApi.md#v4getagenciesname) | **GET** /v4/agencies/{name} | Get Agency
[**V4GetAgenciesNameEnvironments**](AgenciesApi.md#v4getagenciesnameenvironments) | **GET** /v4/agencies/{name}/environments | Get Agency Environments
[**V4GetAgenciesNameEnvironmentsEnvStatus**](AgenciesApi.md#v4getagenciesnameenvironmentsenvstatus) | **GET** /v4/agencies/{name}/environments/{env}/status | Get Agency Environment Status
[**V4GetAgenciesNameLogo**](AgenciesApi.md#v4getagenciesnamelogo) | **GET** /v4/agencies/{name}/logo | Get Agency Logo

<a name="v4getagencies"></a>
# **V4GetAgencies**
> ResponseAgencyArray V4GetAgencies (string contentType, string authorization, string xAccelaAppid, string name = null, int? offset = null, int? limit = null, string order = null, string direction = null)

Get All Agencies

Gets all enabled agencies that are configured on the [Construct Admin Portal](https://admin.accela.com).    **API Endpoint**:  GET /v4/agencies  **Scope**:  agencies   **App Type**:  All   **Authorization Type**:  No authorization required   **Civic Platform version**: All  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAgencies.Api;
using AccelaAgencies.Client;
using AccelaAgencies.Model;

namespace Example
{
    public class V4GetAgenciesExample
    {
        public void main()
        {
            var apiInstance = new AgenciesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var xAccelaAppid = xAccelaAppid_example;  // string | clientid
            var name = name_example;  // string | The name of the object to be queried. (optional) 
            var offset = 56;  // int? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 56;  // int? | Search result size limit. (optional) 
            var order = order_example;  // string |  (optional) 
            var direction = direction_example;  // string |  (optional) 

            try
            {
                // Get All Agencies
                ResponseAgencyArray result = apiInstance.V4GetAgencies(contentType, authorization, xAccelaAppid, name, offset, limit, order, direction);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AgenciesApi.V4GetAgencies: " + e.Message );
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
 **xAccelaAppid** | **string**| clientid | 
 **name** | **string**| The name of the object to be queried. | [optional] 
 **offset** | **int?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **limit** | **int?**| Search result size limit. | [optional] 
 **order** | **string**|  | [optional] 
 **direction** | **string**|  | [optional] 

### Return type

[**ResponseAgencyArray**](ResponseAgencyArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="v4getagenciesname"></a>
# **V4GetAgenciesName**
> ResponseAgency V4GetAgenciesName (string contentType, string authorization, string xAccelaAppid, string name)

Get Agency

Gets information for the specified agency, as configured on the [Construct Admin Portal](https://admin.accela.com).    **API Endpoint**:  GET /v4/agencies/{name}  **Scope**:  agencies   **App Type**:  All   **Authorization Type**:  No authorization required   **Civic Platform version**: All  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAgencies.Api;
using AccelaAgencies.Client;
using AccelaAgencies.Model;

namespace Example
{
    public class V4GetAgenciesNameExample
    {
        public void main()
        {
            var apiInstance = new AgenciesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var xAccelaAppid = xAccelaAppid_example;  // string | clientid
            var name = name_example;  // string | The name of the agency to be queried.

            try
            {
                // Get Agency
                ResponseAgency result = apiInstance.V4GetAgenciesName(contentType, authorization, xAccelaAppid, name);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AgenciesApi.V4GetAgenciesName: " + e.Message );
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
 **xAccelaAppid** | **string**| clientid | 
 **name** | **string**| The name of the agency to be queried. | 

### Return type

[**ResponseAgency**](ResponseAgency.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="v4getagenciesnameenvironments"></a>
# **V4GetAgenciesNameEnvironments**
> ResponseEnvironmentArray V4GetAgenciesNameEnvironments (string contentType, string authorization, string xAccelaAppid, string name)

Get Agency Environments

Gets the API runtime environments for the specified agency, including the environment's API provider and version. The environments are configured on [Construct Admin Portal](https://admin.accela.com) > Agencies > {agencyHost}:AgencyInfo > Environments.    **API Endpoint**:  GET /v4/agencies/{name}/environments  **Scope**:  agencies   **App Type**:  All   **Authorization Type**:  No authorization required   **Civic Platform version**: All  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAgencies.Api;
using AccelaAgencies.Client;
using AccelaAgencies.Model;

namespace Example
{
    public class V4GetAgenciesNameEnvironmentsExample
    {
        public void main()
        {
            var apiInstance = new AgenciesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var xAccelaAppid = xAccelaAppid_example;  // string | clientid
            var name = name_example;  // string | The name of the agency to be queried.

            try
            {
                // Get Agency Environments
                ResponseEnvironmentArray result = apiInstance.V4GetAgenciesNameEnvironments(contentType, authorization, xAccelaAppid, name);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AgenciesApi.V4GetAgenciesNameEnvironments: " + e.Message );
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
 **xAccelaAppid** | **string**| clientid | 
 **name** | **string**| The name of the agency to be queried. | 

### Return type

[**ResponseEnvironmentArray**](ResponseEnvironmentArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="v4getagenciesnameenvironmentsenvstatus"></a>
# **V4GetAgenciesNameEnvironmentsEnvStatus**
> ResponseEnvironmentStatus V4GetAgenciesNameEnvironmentsEnvStatus (string contentType, string authorization, string xAccelaAppid, string env, string name)

Get Agency Environment Status

Gets the status of the specified agency environment. If the environment is unavailable or has been disabled on the [Construct Admin Portal](https://admin.accela.com), \"isAvailable\":\"false\" is returned. If Accela Gateway is unreachable, the Get Environment Status API returns 500.  **API Endpoint**: GET /v4/agencies/{name}/environments/{env}/status   **Scope**:  agencies   **App Type**:  All   **Authorization Type**:  No authorization required   **Civic Platform version**: All  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAgencies.Api;
using AccelaAgencies.Client;
using AccelaAgencies.Model;

namespace Example
{
    public class V4GetAgenciesNameEnvironmentsEnvStatusExample
    {
        public void main()
        {
            var apiInstance = new AgenciesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var xAccelaAppid = xAccelaAppid_example;  // string | clientid
            var env = env_example;  // string | The name of the environment to be queried.
            var name = name_example;  // string | The name of the agency to be queried.

            try
            {
                // Get Agency Environment Status
                ResponseEnvironmentStatus result = apiInstance.V4GetAgenciesNameEnvironmentsEnvStatus(contentType, authorization, xAccelaAppid, env, name);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AgenciesApi.V4GetAgenciesNameEnvironmentsEnvStatus: " + e.Message );
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
 **xAccelaAppid** | **string**| clientid | 
 **env** | **string**| The name of the environment to be queried. | 
 **name** | **string**| The name of the agency to be queried. | 

### Return type

[**ResponseEnvironmentStatus**](ResponseEnvironmentStatus.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="v4getagenciesnamelogo"></a>
# **V4GetAgenciesNameLogo**
> ResponseLogo V4GetAgenciesNameLogo (string contentType, string authorization, string xAccelaAppid, string name)

Get Agency Logo

Gets the logo for the specified agency, as configured on the [Construct Admin Portal](https://admin.accela.com).    **API Endpoint**:  GET /v4/agencies/{name}/logo  **Scope**:  agencies   **App Type**:  All   **Authorization Type**:  No authorization required   **Civic Platform version**: All  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaAgencies.Api;
using AccelaAgencies.Client;
using AccelaAgencies.Model;

namespace Example
{
    public class V4GetAgenciesNameLogoExample
    {
        public void main()
        {
            var apiInstance = new AgenciesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var xAccelaAppid = xAccelaAppid_example;  // string | clientid
            var name = name_example;  // string | The name of the agency to be queried.

            try
            {
                // Get Agency Logo
                ResponseLogo result = apiInstance.V4GetAgenciesNameLogo(contentType, authorization, xAccelaAppid, name);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AgenciesApi.V4GetAgenciesNameLogo: " + e.Message );
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
 **xAccelaAppid** | **string**| clientid | 
 **name** | **string**| The name of the agency to be queried. | 

### Return type

[**ResponseLogo**](ResponseLogo.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
