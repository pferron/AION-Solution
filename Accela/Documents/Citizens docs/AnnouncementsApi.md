# AccelaCitizens.Api.AnnouncementsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetAnnouncements**](AnnouncementsApi.md#v4getannouncements) | **GET** /v4/announcements | Get All Announcements
[**V4PutAnnouncementsIdsRead**](AnnouncementsApi.md#v4putannouncementsidsread) | **PUT** /v4/announcements/{ids}/read | Mark Announcements as Read


<a name="v4getannouncements"></a>
# **V4GetAnnouncements**
> ResponseMessageModelArray V4GetAnnouncements (string contentType, string authorization, long? limit = null, long? offset = null, string isRead = null, string fields = null, string lang = null)

Get All Announcements

Gets all public announcements in the system.    **API Endpoint**:  GET /v4/announcements   **Scope**:  announcements   **App Type**:  All   **Authorization Type**:  Access token   **Civic Platform version**: 7.3.3  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaCitizens.Api;
using AccelaCitizens.Client;
using AccelaCitizens.Model;

namespace Example
{
    public class V4GetAnnouncementsExample
    {
        public void main()
        {
            var apiInstance = new AnnouncementsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var limit = 789;  // long? | Search result size limit. (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var isRead = isRead_example;  // string | Filter by whether or not the announcement has been read. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Announcements
                ResponseMessageModelArray result = apiInstance.V4GetAnnouncements(contentType, authorization, limit, offset, isRead, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AnnouncementsApi.V4GetAnnouncements: " + e.Message );
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
 **limit** | **long?**| Search result size limit. | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **isRead** | **string**| Filter by whether or not the announcement has been read. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseMessageModelArray**](ResponseMessageModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4putannouncementsidsread"></a>
# **V4PutAnnouncementsIdsRead**
> ResponseResultModelArray V4PutAnnouncementsIdsRead (string contentType, string authorization, string ids, string lang = null)

Mark Announcements as Read

Updates announcements to indicate that that the user has read the announcement.    **API Endpoint**:  PUT /v4/announcements/{ids}/read   **Scope**:  announcements   **App Type**:  Citizen   **Authorization Type**:  Access token   **Civic Platform version**: 7.3.3.4  

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaCitizens.Api;
using AccelaCitizens.Client;
using AccelaCitizens.Model;

namespace Example
{
    public class V4PutAnnouncementsIdsReadExample
    {
        public void main()
        {
            var apiInstance = new AnnouncementsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var ids = ids_example;  // string | Comma-delimited IDs of announcements to update.
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Mark Announcements as Read
                ResponseResultModelArray result = apiInstance.V4PutAnnouncementsIdsRead(contentType, authorization, ids, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AnnouncementsApi.V4PutAnnouncementsIdsRead: " + e.Message );
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
 **ids** | **string**| Comma-delimited IDs of announcements to update. | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

