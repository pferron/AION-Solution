# AccelaSettings.Api.SettingsTimeAccountingApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetSettingsTimeAccountingGroups**](SettingsTimeAccountingApi.md#v4getsettingstimeaccountinggroups) | **GET** /v4/settings/timeAccounting/groups | Get All Time Accounting Groups
[**V4GetSettingsTimeAccountingTypes**](SettingsTimeAccountingApi.md#v4getsettingstimeaccountingtypes) | **GET** /v4/settings/timeAccounting/types | Get All Time Accounting Types


<a name="v4getsettingstimeaccountinggroups"></a>
# **V4GetSettingsTimeAccountingGroups**
> ResponseSettingValueModelArray V4GetSettingsTimeAccountingGroups (string contentType, string userIds = null, string lang = null)

Get All Time Accounting Groups

Gets all time accounting groups, optionally for specified userids. **API Endpoint**:  GET /v4/settings/timeAccounting/groups  **Scope**:  settings  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsTimeAccountingGroupsExample
    {
        public void main()
        {
            var apiInstance = new SettingsTimeAccountingApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var userIds = userIds_example;  // string | Filter by comma-delimited user ids. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Time Accounting Groups
                ResponseSettingValueModelArray result = apiInstance.V4GetSettingsTimeAccountingGroups(contentType, userIds, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsTimeAccountingApi.V4GetSettingsTimeAccountingGroups: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | [default to application/x-www-form-urlencoded]
 **userIds** | **string**| Filter by comma-delimited user ids. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseSettingValueModelArray**](ResponseSettingValueModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getsettingstimeaccountingtypes"></a>
# **V4GetSettingsTimeAccountingTypes**
> ResponseSettingValueModelArray V4GetSettingsTimeAccountingTypes (string contentType, long? groupId, string userIds = null, string recordType = null, string lang = null)

Get All Time Accounting Types

Gets all time accounting types for specified time accounting groups, optionally for specified userids and record type. **API Endpoint**:  GET /v4/settings/timeAccounting/types  **Scope**:  settings  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsTimeAccountingTypesExample
    {
        public void main()
        {
            var apiInstance = new SettingsTimeAccountingApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var groupId = 789;  // long? | Filter by time accounting group.
            var userIds = userIds_example;  // string | Filter by comma-delimited the user ids. (optional) 
            var recordType = recordType_example;  // string | Filter by the record type. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Time Accounting Types
                ResponseSettingValueModelArray result = apiInstance.V4GetSettingsTimeAccountingTypes(contentType, groupId, userIds, recordType, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsTimeAccountingApi.V4GetSettingsTimeAccountingTypes: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | [default to application/x-www-form-urlencoded]
 **groupId** | **long?**| Filter by time accounting group. | 
 **userIds** | **string**| Filter by comma-delimited the user ids. | [optional] 
 **recordType** | **string**| Filter by the record type. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseSettingValueModelArray**](ResponseSettingValueModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

