# AccelaMiscellanous.Api.ConditionsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetConditionApprovalsStandard**](ConditionsApi.md#v4getconditionapprovalsstandard) | **GET** /v4/conditionApprovals/standard | Get All Approval Conditions
[**V4GetConditionsStandard**](ConditionsApi.md#v4getconditionsstandard) | **GET** /v4/conditions/standard | Get All Standard Conditions


<a name="v4getconditionapprovalsstandard"></a>
# **V4GetConditionApprovalsStandard**
> ResponseConditionApprovalModelArray V4GetConditionApprovalsStandard (string contentType, string authorization, string type = null, string group = null, string name = null, string severity = null, string publicDisplayMessage = null, string inheritable = null, long? priority = null, string shortComments = null, string longComments = null, string resolutionAction = null, bool? includeNameInNotice = null, bool? includeShortCommentsInNotice = null, bool? displayNoticeInAgency = null, bool? displayNoticeInCitizens = null, bool? displayNoticeInCitizensFee = null, long? offset = null, long? limit = null, string sort = null, string direction = null, string fields = null, string lang = null)

Get All Approval Conditions

Gets the conditions of approval available in the system. **API Endpoint**:  GET /v4/conditionApprovals/standard **Scope**:  conditions  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaMiscellanous.Api;
using AccelaMiscellanous.Client;
using AccelaMiscellanous.Model;

namespace Example
{
    public class V4GetConditionApprovalsStandardExample
    {
        public void main()
        {
            var apiInstance = new ConditionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var type = type_example;  // string | Filter by the standard condition type. See [Get All Standard Condition Types](./api-settings.html#operation/v4.get.settings.conditions.types). (optional) 
            var group = group_example;  // string | Filter by standard condition group. (optional) 
            var name = name_example;  // string | Filter by standard condition name. (optional) 
            var severity = severity_example;  // string | Filter by standard condition severity. (optional) 
            var publicDisplayMessage = publicDisplayMessage_example;  // string | Filter by standard condition public display message. (optional) 
            var inheritable = inheritable_example;  // string | Filter whether or not the standard condition is inheritable. (optional) 
            var priority = 789;  // long? | Filter by standard condition priority. See [Get All Standard Condition Priorities](./api-settings.html#operation/v4.get.settings.conditions.priorities). (optional) 
            var shortComments = shortComments_example;  // string | Filter by standard condition short comments. (optional) 
            var longComments = longComments_example;  // string | Filter by standard condition long comments. (optional) 
            var resolutionAction = resolutionAction_example;  // string | Filter by standard condition  resolution action (optional) 
            var includeNameInNotice = true;  // bool? | Filter whether or not the standard condition includes name in notice. (optional) 
            var includeShortCommentsInNotice = true;  // bool? | Filter whether or not the standard condition includes short comments in notice. (optional) 
            var displayNoticeInAgency = true;  // bool? | Filter whether or not the standard condition displays the notice to the agency. (optional) 
            var displayNoticeInCitizens = true;  // bool? | Filter whether or not the standard condition displays the notice to the citizens. (optional) 
            var displayNoticeInCitizensFee = true;  // bool? | Filter whether or not the standard condition displays the notice in the citizen fee. (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var sort = sort_example;  // string | Sort search result by one field name. (optional) 
            var direction = direction_example;  // string | The sort direction. Default is ASC. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note: Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Approval Conditions
                ResponseConditionApprovalModelArray result = apiInstance.V4GetConditionApprovalsStandard(contentType, authorization, type, group, name, severity, publicDisplayMessage, inheritable, priority, shortComments, longComments, resolutionAction, includeNameInNotice, includeShortCommentsInNotice, displayNoticeInAgency, displayNoticeInCitizens, displayNoticeInCitizensFee, offset, limit, sort, direction, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ConditionsApi.V4GetConditionApprovalsStandard: " + e.Message );
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
 **type** | **string**| Filter by the standard condition type. See [Get All Standard Condition Types](./api-settings.html#operation/v4.get.settings.conditions.types). | [optional] 
 **group** | **string**| Filter by standard condition group. | [optional] 
 **name** | **string**| Filter by standard condition name. | [optional] 
 **severity** | **string**| Filter by standard condition severity. | [optional] 
 **publicDisplayMessage** | **string**| Filter by standard condition public display message. | [optional] 
 **inheritable** | **string**| Filter whether or not the standard condition is inheritable. | [optional] 
 **priority** | **long?**| Filter by standard condition priority. See [Get All Standard Condition Priorities](./api-settings.html#operation/v4.get.settings.conditions.priorities). | [optional] 
 **shortComments** | **string**| Filter by standard condition short comments. | [optional] 
 **longComments** | **string**| Filter by standard condition long comments. | [optional] 
 **resolutionAction** | **string**| Filter by standard condition  resolution action | [optional] 
 **includeNameInNotice** | **bool?**| Filter whether or not the standard condition includes name in notice. | [optional] 
 **includeShortCommentsInNotice** | **bool?**| Filter whether or not the standard condition includes short comments in notice. | [optional] 
 **displayNoticeInAgency** | **bool?**| Filter whether or not the standard condition displays the notice to the agency. | [optional] 
 **displayNoticeInCitizens** | **bool?**| Filter whether or not the standard condition displays the notice to the citizens. | [optional] 
 **displayNoticeInCitizensFee** | **bool?**| Filter whether or not the standard condition displays the notice in the citizen fee. | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 
 **sort** | **string**| Sort search result by one field name. | [optional] 
 **direction** | **string**| The sort direction. Default is ASC. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note: Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseConditionApprovalModelArray**](ResponseConditionApprovalModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getconditionsstandard"></a>
# **V4GetConditionsStandard**
> ResponseConditionModelArray V4GetConditionsStandard (string contentType, string authorization, string type = null, string group = null, string name = null, string severity = null, string publicDisplayMessage = null, string inheritable = null, long? priority = null, string shortComments = null, string longComments = null, string resolutionAction = null, bool? includeNameInNotice = null, bool? includeShortCommentsInNotice = null, bool? displayNoticeInAgency = null, bool? displayNoticeInCitizens = null, bool? displayNoticeInCitizensFee = null, long? offset = null, long? limit = null, string sort = null, string direction = null, string fields = null, string lang = null)

Get All Standard Conditions

Gets the standard conditions available in the system. **API Endpoint**:  GET /v4/conditions/standard  **Scope**:  conditions  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaMiscellanous.Api;
using AccelaMiscellanous.Client;
using AccelaMiscellanous.Model;

namespace Example
{
    public class V4GetConditionsStandardExample
    {
        public void main()
        {
            var apiInstance = new ConditionsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var type = type_example;  // string | Filter by the standard condition type. See [Get All Standard Condition Types](./api-settings.html#operation/v4.get.settings.conditions.types). (optional) 
            var group = group_example;  // string | Filter by standard condition group. (optional) 
            var name = name_example;  // string | Filter by standard condition name. (optional) 
            var severity = severity_example;  // string | Filter by standard condition severity. (optional) 
            var publicDisplayMessage = publicDisplayMessage_example;  // string | Filter by standard condition public display message. (optional) 
            var inheritable = inheritable_example;  // string | Filter whether or not the standard condition is inheritable. (optional) 
            var priority = 789;  // long? | Filter by standard condition priority. See [Get All Standard Condition Priorities](./api-settings.html#operation/v4.get.settings.conditions.priorities). (optional) 
            var shortComments = shortComments_example;  // string | Filter by standard condition short comments. (optional) 
            var longComments = longComments_example;  // string | Filter by standard condition long comments. (optional) 
            var resolutionAction = resolutionAction_example;  // string | Filter by standard condition  resolution action (optional) 
            var includeNameInNotice = true;  // bool? | Filter whether or not the standard condition includes name in notice. (optional) 
            var includeShortCommentsInNotice = true;  // bool? | Filter whether or not the standard condition includes short comments in notice. (optional) 
            var displayNoticeInAgency = true;  // bool? | Filter whether or not the standard condition displays the notice to the agency. (optional) 
            var displayNoticeInCitizens = true;  // bool? | Filter whether or not the standard condition displays the notice to the citizens. (optional) 
            var displayNoticeInCitizensFee = true;  // bool? | Filter whether or not the standard condition displays the notice in the citizen fee. (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var sort = sort_example;  // string | Sort search result by one field name. (optional) 
            var direction = direction_example;  // string | The sort direction. Default is ASC. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note: Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Standard Conditions
                ResponseConditionModelArray result = apiInstance.V4GetConditionsStandard(contentType, authorization, type, group, name, severity, publicDisplayMessage, inheritable, priority, shortComments, longComments, resolutionAction, includeNameInNotice, includeShortCommentsInNotice, displayNoticeInAgency, displayNoticeInCitizens, displayNoticeInCitizensFee, offset, limit, sort, direction, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ConditionsApi.V4GetConditionsStandard: " + e.Message );
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
 **type** | **string**| Filter by the standard condition type. See [Get All Standard Condition Types](./api-settings.html#operation/v4.get.settings.conditions.types). | [optional] 
 **group** | **string**| Filter by standard condition group. | [optional] 
 **name** | **string**| Filter by standard condition name. | [optional] 
 **severity** | **string**| Filter by standard condition severity. | [optional] 
 **publicDisplayMessage** | **string**| Filter by standard condition public display message. | [optional] 
 **inheritable** | **string**| Filter whether or not the standard condition is inheritable. | [optional] 
 **priority** | **long?**| Filter by standard condition priority. See [Get All Standard Condition Priorities](./api-settings.html#operation/v4.get.settings.conditions.priorities). | [optional] 
 **shortComments** | **string**| Filter by standard condition short comments. | [optional] 
 **longComments** | **string**| Filter by standard condition long comments. | [optional] 
 **resolutionAction** | **string**| Filter by standard condition  resolution action | [optional] 
 **includeNameInNotice** | **bool?**| Filter whether or not the standard condition includes name in notice. | [optional] 
 **includeShortCommentsInNotice** | **bool?**| Filter whether or not the standard condition includes short comments in notice. | [optional] 
 **displayNoticeInAgency** | **bool?**| Filter whether or not the standard condition displays the notice to the agency. | [optional] 
 **displayNoticeInCitizens** | **bool?**| Filter whether or not the standard condition displays the notice to the citizens. | [optional] 
 **displayNoticeInCitizensFee** | **bool?**| Filter whether or not the standard condition displays the notice in the citizen fee. | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 
 **sort** | **string**| Sort search result by one field name. | [optional] 
 **direction** | **string**| The sort direction. Default is ASC. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note: Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseConditionModelArray**](ResponseConditionModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

