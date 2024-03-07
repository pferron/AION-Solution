# AccelaContactsAndProfessionals.Api.ProfessionalsApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetProfessionals**](ProfessionalsApi.md#v4getprofessionals) | **GET** /v4/professionals/ | Get All Professionals
[**V4GetProfessionalsIds**](ProfessionalsApi.md#v4getprofessionalsids) | **GET** /v4/professionals/{ids} | Get Professionals


<a name="v4getprofessionals"></a>
# **V4GetProfessionals**
> ResponseLicenseModelArray V4GetProfessionals (string contentType, string authorization, string id = null, string licenseType = null, string licenseNumber = null, string licenseState = null, string state = null, string country = null, string firstName = null, string middleName = null, string lastName = null, string email = null, string addressLine1 = null, string addressLine2 = null, string addressLine3 = null, string businessLicense = null, string businessName = null, string city = null, string licenseExpirationDate = null, string licenseIssueDate = null, string lastRenewalDate = null, string title = null, long? offset = null, long? limit = null, string fields = null, string lang = null)

Get All Professionals

Gets all professionals in the agency database. **API Endpoint**:  GET /v4/professionals/  **Scope**:  professionals  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaContactsAndProfessionals.Api;
using AccelaContactsAndProfessionals.Client;
using AccelaContactsAndProfessionals.Model;

namespace Example
{
    public class V4GetProfessionalsExample
    {
        public void main()
        {
            var apiInstance = new ProfessionalsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var id = id_example;  // string | Filter by License ID (optional) 
            var licenseType = licenseType_example;  // string | Filter by License Type (optional) 
            var licenseNumber = licenseNumber_example;  // string | Filter by License Number (optional) 
            var licenseState = licenseState_example;  // string | Filter by License State (optional) 
            var state = state_example;  // string | Filter by Address State (optional) 
            var country = country_example;  // string | Filter by Country (optional) 
            var firstName = firstName_example;  // string | Filter by License Contact First Name (optional) 
            var middleName = middleName_example;  // string | Filter by License Contact Middle Name (optional) 
            var lastName = lastName_example;  // string | Filter by License Contact Last Name (optional) 
            var email = email_example;  // string | Filter by Email (optional) 
            var addressLine1 = addressLine1_example;  // string | Filter by first line of address. (optional) 
            var addressLine2 = addressLine2_example;  // string | Filter by second line of address. (optional) 
            var addressLine3 = addressLine3_example;  // string | Filter by third line of address. (optional) 
            var businessLicense = businessLicense_example;  // string | Filter by Business License (optional) 
            var businessName = businessName_example;  // string | Filter by Business Name (optional) 
            var city = city_example;  // string | Filter by City (optional) 
            var licenseExpirationDate = licenseExpirationDate_example;  // string | Filter by License Expiration Date (optional) 
            var licenseIssueDate = licenseIssueDate_example;  // string | Filter by License Issue Date (optional) 
            var lastRenewalDate = lastRenewalDate_example;  // string | Filter by License Last Renewal Date (optional) 
            var title = title_example;  // string | Filter by Title (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Professionals
                ResponseLicenseModelArray result = apiInstance.V4GetProfessionals(contentType, authorization, id, licenseType, licenseNumber, licenseState, state, country, firstName, middleName, lastName, email, addressLine1, addressLine2, addressLine3, businessLicense, businessName, city, licenseExpirationDate, licenseIssueDate, lastRenewalDate, title, offset, limit, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProfessionalsApi.V4GetProfessionals: " + e.Message );
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
 **id** | **string**| Filter by License ID | [optional] 
 **licenseType** | **string**| Filter by License Type | [optional] 
 **licenseNumber** | **string**| Filter by License Number | [optional] 
 **licenseState** | **string**| Filter by License State | [optional] 
 **state** | **string**| Filter by Address State | [optional] 
 **country** | **string**| Filter by Country | [optional] 
 **firstName** | **string**| Filter by License Contact First Name | [optional] 
 **middleName** | **string**| Filter by License Contact Middle Name | [optional] 
 **lastName** | **string**| Filter by License Contact Last Name | [optional] 
 **email** | **string**| Filter by Email | [optional] 
 **addressLine1** | **string**| Filter by first line of address. | [optional] 
 **addressLine2** | **string**| Filter by second line of address. | [optional] 
 **addressLine3** | **string**| Filter by third line of address. | [optional] 
 **businessLicense** | **string**| Filter by Business License | [optional] 
 **businessName** | **string**| Filter by Business Name | [optional] 
 **city** | **string**| Filter by City | [optional] 
 **licenseExpirationDate** | **string**| Filter by License Expiration Date | [optional] 
 **licenseIssueDate** | **string**| Filter by License Issue Date | [optional] 
 **lastRenewalDate** | **string**| Filter by License Last Renewal Date | [optional] 
 **title** | **string**| Filter by Title | [optional] 
 **offset** | **long?**| The offset position of the first record in the results response array. For example, if offset is 100, the first item in the results array in the response is the 100th record in the search result list. | [optional] 
 **limit** | **long?**| Search result size limit. | [optional] 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseLicenseModelArray**](ResponseLicenseModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4getprofessionalsids"></a>
# **V4GetProfessionalsIds**
> ResponseLicenseModelArray V4GetProfessionalsIds (string contentType, string authorization, string ids, string fields = null, string lang = null)

Get Professionals

Gets information for one or more specified professionals. **API Endpoint**:  GET /v4/professionals/{ids}  **Scope**:  professionals  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaContactsAndProfessionals.Api;
using AccelaContactsAndProfessionals.Client;
using AccelaContactsAndProfessionals.Model;

namespace Example
{
    public class V4GetProfessionalsIdsExample
    {
        public void main()
        {
            var apiInstance = new ProfessionalsApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var ids = ids_example;  // string | Comma-delimited IDs of professionals to fetch.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get Professionals
                ResponseLicenseModelArray result = apiInstance.V4GetProfessionalsIds(contentType, authorization, ids, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProfessionalsApi.V4GetProfessionalsIds: " + e.Message );
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
 **ids** | **string**| Comma-delimited IDs of professionals to fetch. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseLicenseModelArray**](ResponseLicenseModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

