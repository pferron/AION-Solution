# AccelaSettings.Api.SettingsAssessmentsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetSettingsAssessmentsTypes**](SettingsAssessmentsApi.md#v4getsettingsassessmentstypes) | **GET** /v4/settings/assessments/types | Get All Condition Assessment Types


<a name="v4getsettingsassessmentstypes"></a>
# **V4GetSettingsAssessmentsTypes**
> ResponseConditionAssessmentModelArray V4GetSettingsAssessmentsTypes (string contentType, string fields = null, string lang = null)

Get All Condition Assessment Types

Returns all active condition assessment types configured in Civic Platform Administration. **API Endpoint**:  GET /v4/settings/assessments/types  **Scope**:  records  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 9.0.0 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaSettings.Api;
using AccelaSettings.Client;
using AccelaSettings.Model;

namespace Example
{
    public class V4GetSettingsAssessmentsTypesExample
    {
        public void main()
        {
            var apiInstance = new SettingsAssessmentsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Condition Assessment Types
                ResponseConditionAssessmentModelArray result = apiInstance.V4GetSettingsAssessmentsTypes(contentType, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SettingsAssessmentsApi.V4GetSettingsAssessmentsTypes: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contentType** | **string**| Must be application/x-www-form-urlencoded. | [default to application/x-www-form-urlencoded]
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseConditionAssessmentModelArray**](ResponseConditionAssessmentModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

