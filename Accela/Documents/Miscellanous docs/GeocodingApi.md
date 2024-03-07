# AccelaMiscellanous.Api.GeocodingApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetGeoGeocodeReverse**](GeocodingApi.md#v4getgeogeocodereverse) | **GET** /v4/geo/geocode/reverse | Reverse Geocode Address


<a name="v4getgeogeocodereverse"></a>
# **V4GetGeoGeocodeReverse**
> ResponseGeocodeAddressModel V4GetGeoGeocodeReverse (string contentType, string authorization, string longitude, string latitude, string lang = null)

Reverse Geocode Address

Returns address information for the given longitude and latitude coordinates. This API returns address information from map services configured on **[Construct Admin Portal](https://admin.accela.com) > {Agency} > Agency Settings > GIS Settings**. **API Endpoint**:  GET /v4/geo/geocode/reverse  **Scope**:  gis  **App Type**:  All  **Authorization Type**:  No authorization required  **Civic Platform version**: All 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaMiscellanous.Api;
using AccelaMiscellanous.Client;
using AccelaMiscellanous.Model;

namespace Example
{
    public class V4GetGeoGeocodeReverseExample
    {
        public void main()
        {
            var apiInstance = new GeocodingApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var longitude = longitude_example;  // string | The north-south, x-coordinate of an address. The value must be between -180 and +180.  
            var latitude = latitude_example;  // string | The east-west, y-coordinate of an address. The value must be between -90 and +90.   
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Reverse Geocode Address
                ResponseGeocodeAddressModel result = apiInstance.V4GetGeoGeocodeReverse(contentType, authorization, longitude, latitude, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling GeocodingApi.V4GetGeoGeocodeReverse: " + e.Message );
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
 **longitude** | **string**| The north-south, x-coordinate of an address. The value must be between -180 and +180.   | 
 **latitude** | **string**| The east-west, y-coordinate of an address. The value must be between -90 and +90.    | 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseGeocodeAddressModel**](ResponseGeocodeAddressModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

