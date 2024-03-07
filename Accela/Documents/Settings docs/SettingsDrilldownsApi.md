# AccelaSettings.Api.SettingsDrilldownsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetSettingsDrillDownsDrillId**](SettingsDrilldownsApi.md#v4getsettingsdrilldownsdrillid) | **GET** /v4/settings/drillDown/{drillId} | Get Drilldown Settings


<a name="v4getsettingsdrilldownsdrillid"></a>
# **V4GetSettingsDrillDownsDrillId**
> ResponseSettingValueModelArray V4GetSettingsDrillDownsDrillId (string contentType, long? drillId, string parentValue, string lang = null)

Get Drilldown Settings

Gets the values for the specified drilldown. **API Endpoint**:  GET /v4/settings/drillDown/{drillId}  **Scope**:  settings  **App Type**:  All  **Authorization Type**:  No authorization required  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsDrillDownsDrillIdExample
    {
        public void main()
        {
            var apiInstance = new SettingsDrilldownsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var drillId = 789;  // long? | The ID of of the drilldown field to fetch.
            var parentValue = parentValue_example;  // string | The name of the parent table to which the drilldown values apply.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Drilldown Settings
                ResponseSettingValueModelArray result = apiInstance.V4GetSettingsDrillDownsDrillId(contentType, drillId, parentValue, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsDrilldownsApi.V4GetSettingsDrillDownsDrillId: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | [default to application/x-www-form-urlencoded]
 **drillId** | **long?**| The ID of of the drilldown field to fetch. | 
 **parentValue** | **string**| The name of the parent table to which the drilldown values apply. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseSettingValueModelArray**](ResponseSettingValueModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

