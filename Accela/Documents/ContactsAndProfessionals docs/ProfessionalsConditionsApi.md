# AccelaContactsAndProfessionals.Api.ProfessionalsConditionsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetProfessionalsIdConditions**](ProfessionalsConditionsApi.md#v4getprofessionalsidconditions) | **GET** /v4/professionals/{id}/conditions | Get All Professional Conditions


<a name="v4getprofessionalsidconditions"></a>
# **V4GetProfessionalsIdConditions**
> ResponsePeopleConditionModelArray V4GetProfessionalsIdConditions (string contentType, string authorization, long? id, string type = null, string name = null, string status = null, string fields = null, long? offset = null, long? limit = null, string lang = null)

Get All Professional Conditions

Gets the conditions for the specified professional {id}. **API Endpoint**:  /v4/professionals/{id}/conditions  **Scope**:  professionals  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 8.0.0 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaContactsAndProfessionals.Api;
using AccelaContactsAndProfessionals.Client;
using AccelaContactsAndProfessionals.Model;

namespace Example
{
    public class V4GetProfessionalsIdConditionsExample
    {
        public void main()
        {
            var apiInstance = new ProfessionalsConditionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = 789;  // long? | The ID of the licensed professional to fetch.
            var type = type_example;  // string | Condition type filter (optional) 
            var name = name_example;  // string | Condition name filter (optional) 
            var status = status_example;  // string | Condition status filter (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Professional Conditions
                ResponsePeopleConditionModelArray result = apiInstance.V4GetProfessionalsIdConditions(contentType, authorization, id, type, name, status, fields, offset, limit, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProfessionalsConditionsApi.V4GetProfessionalsIdConditions: " + e.Message );
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
 **id** | **long?**| The ID of the licensed professional to fetch. | 
 **type** | **string**| Condition type filter | [optional] 
 **name** | **string**| Condition name filter | [optional] 
 **status** | **string**| Condition status filter | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponsePeopleConditionModelArray**](ResponsePeopleConditionModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

