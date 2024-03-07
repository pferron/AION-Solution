# AccelaSettings.Api.SettingsDepartmentsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetSettingsDepartments**](SettingsDepartmentsApi.md#v4getsettingsdepartments) | **GET** /v4/settings/departments | Get All Department Staff
[**V4GetSettingsDepartmentsIdStaffs**](SettingsDepartmentsApi.md#v4getsettingsdepartmentsidstaffs) | **GET** /v4/settings/departments/{id}/staffs | Get All Department Staff


<a name="v4getsettingsdepartments"></a>
# **V4GetSettingsDepartments**
> ResponseDepartmentModelArray V4GetSettingsDepartments (string contentType, string authorization, string lang = null)

Get All Department Staff

Gets a list of agency departments. **API Endpoint**:  GET /v4/settings/departments  **Scope**:  settings  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsDepartmentsExample
    {
        public void main()
        {
            var apiInstance = new SettingsDepartmentsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Department Staff
                ResponseDepartmentModelArray result = apiInstance.V4GetSettingsDepartments(contentType, authorization, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsDepartmentsApi.V4GetSettingsDepartments: " + e.Message );
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
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseDepartmentModelArray**](ResponseDepartmentModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getsettingsdepartmentsidstaffs"></a>
# **V4GetSettingsDepartmentsIdStaffs**
> ResponseUserModelArray V4GetSettingsDepartmentsIdStaffs (string contentType, string authorization, string id, string lang = null)

Get All Department Staff

Gets the staff members for a specified department. NOTE: To return the users within a department, this API requires an Automation administrator to activate FID 0045 (Admin Organization Hierarchy) and FID 0004 (Admin Users) with read-only access. **API Endpoint**:  GET /v4/settings/departments/{id}/staffs  **Scope**:  settings  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsDepartmentsIdStaffsExample
    {
        public void main()
        {
            var apiInstance = new SettingsDepartmentsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = id_example;  // string | The ID of the department to fetch.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Department Staff
                ResponseUserModelArray result = apiInstance.V4GetSettingsDepartmentsIdStaffs(contentType, authorization, id, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsDepartmentsApi.V4GetSettingsDepartmentsIdStaffs: " + e.Message );
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
 **id** | **string**| The ID of the department to fetch. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseUserModelArray**](ResponseUserModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

