# AccelaRecords.Api.RecordsFeesApi

All URIs are relative to *https://apis.accela.com*

Method | HTTP request | Description
------------- | ------------- | -------------
[**V4GetRecordsRecordIdFees**](RecordsFeesApi.md#v4getrecordsrecordidfees) | **GET** /v4/records/{recordId}/fees | Get All Fees for Record
[**V4PostRecordsRecordIdFees**](RecordsFeesApi.md#v4postrecordsrecordidfees) | **POST** /v4/records/{recordId}/fees | Create Record Fees
[**V4PutRecordsRecordIdFeesEstimate**](RecordsFeesApi.md#v4putrecordsrecordidfeesestimate) | **PUT** /v4/records/{recordId}/fees/estimate | Estimate Record Fees


<a name="v4getrecordsrecordidfees"></a>
# **V4GetRecordsRecordIdFees**
> ResponseFeeItemModel1Array V4GetRecordsRecordIdFees (string contentType, string authorization, string recordId, string fields = null, string status = null, string lang = null)

Get All Fees for Record

Gets the fee schedules associated with the specified record. **API Endpoint**:  GET /v4/records/{recordId}/fees  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4GetRecordsRecordIdFeesExample
    {
        public void main()
        {
            var apiInstance = new RecordsFeesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var status = status_example;  // string | Filter by the status of the fee items. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Get All Fees for Record
                ResponseFeeItemModel1Array result = apiInstance.V4GetRecordsRecordIdFees(contentType, authorization, recordId, fields, status, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsFeesApi.V4GetRecordsRecordIdFees: " + e.Message );
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
 **recordId** | **string**| The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine). | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **status** | **string**| Filter by the status of the fee items. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseFeeItemModel1Array**](ResponseFeeItemModel1Array.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4postrecordsrecordidfees"></a>
# **V4PostRecordsRecordIdFees**
> ResponseResultModelArray V4PostRecordsRecordIdFees (string contentType, string authorization, string recordId, List<FeeItemBaseModel> body, string fields = null, string lang = null)

Create Record Fees

Creates fees for the specified record. **API Endpoint**:  POST /v4/records/{recordId}/fees  **Scope**:  records  **App Type**:  Agency  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.2 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4PostRecordsRecordIdFeesExample
    {
        public void main()
        {
            var apiInstance = new RecordsFeesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var body = new List<FeeItemBaseModel>(); // List<FeeItemBaseModel> | Fee item information to be added. eg: [{ \"code\" : {\"value\":\"TESTFEE2\"}, \"feeNotes\":\"test note1\", \"paymentPeriod\" : {\"value\":\"FINAL\"}, \"quantity\" : 9, \"schedule\" : {\"value\":\"J_FEE\"}, \"version\" : {\"value\":\"1.0\"}}, { \"code\" : {\"value\":\"JFEE\"},\"feeNotes\":\"test note1\",\"paymentPeriod\" :{\"value\": \"FINAL\"}, \"quantity\" : 9, \"schedule\" : {\"value\":\"AD_LETTER\"}, \"version\" : {\"value\":\"1\"}} ]
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Create Record Fees
                ResponseResultModelArray result = apiInstance.V4PostRecordsRecordIdFees(contentType, authorization, recordId, body, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsFeesApi.V4PostRecordsRecordIdFees: " + e.Message );
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
 **recordId** | **string**| The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine). | 
 **body** | [**List&lt;FeeItemBaseModel&gt;**](FeeItemBaseModel.md)| Fee item information to be added. eg: [{ \&quot;code\&quot; : {\&quot;value\&quot;:\&quot;TESTFEE2\&quot;}, \&quot;feeNotes\&quot;:\&quot;test note1\&quot;, \&quot;paymentPeriod\&quot; : {\&quot;value\&quot;:\&quot;FINAL\&quot;}, \&quot;quantity\&quot; : 9, \&quot;schedule\&quot; : {\&quot;value\&quot;:\&quot;J_FEE\&quot;}, \&quot;version\&quot; : {\&quot;value\&quot;:\&quot;1.0\&quot;}}, { \&quot;code\&quot; : {\&quot;value\&quot;:\&quot;JFEE\&quot;},\&quot;feeNotes\&quot;:\&quot;test note1\&quot;,\&quot;paymentPeriod\&quot; :{\&quot;value\&quot;: \&quot;FINAL\&quot;}, \&quot;quantity\&quot; : 9, \&quot;schedule\&quot; : {\&quot;value\&quot;:\&quot;AD_LETTER\&quot;}, \&quot;version\&quot; : {\&quot;value\&quot;:\&quot;1\&quot;}} ] | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseResultModelArray**](ResponseResultModelArray.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="v4putrecordsrecordidfeesestimate"></a>
# **V4PutRecordsRecordIdFeesEstimate**
> ResponseEstimateFeeModel V4PutRecordsRecordIdFeesEstimate (string contentType, string authorization, string recordId, List<FeeItemBaseModel1> body, string fields = null, string lang = null)

Estimate Record Fees

Provides fee estimations for the specified record. **API Endpoint**:  PUT /v4/records/{recordId}/fees/estimate  **Scope**:  records  **App Type**:  All  **Authorization Type**:  Access token  **Civic Platform version**: 7.3.3 

### Example
```csharp
using System;
using System.Diagnostics;
using AccelaRecords.Api;
using AccelaRecords.Client;
using AccelaRecords.Model;

namespace Example
{
    public class V4PutRecordsRecordIdFeesEstimateExample
    {
        public void main()
        {
            var apiInstance = new RecordsFeesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded. (default to application/x-www-form-urlencoded)
            var authorization = authorization_example;  // string | Construct oAuth2 authentication token
            var recordId = recordId_example;  // string | The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine).
            var body = new List<FeeItemBaseModel1>(); // List<FeeItemBaseModel1> | The record fee items information to be updated.
            var fields = fields_example;  // string | Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N. Default language is en_US. (optional) 

            try
            {
                // Estimate Record Fees
                ResponseEstimateFeeModel result = apiInstance.V4PutRecordsRecordIdFeesEstimate(contentType, authorization, recordId, body, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling RecordsFeesApi.V4PutRecordsRecordIdFeesEstimate: " + e.Message );
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
 **recordId** | **string**| The ID of the record to fetch. See [Get All Records](./api-records.html#operation/v4.get.records), [Search Records](./api-search.html#operation/v4.post.search.records), or [Get My Records](./api-records.html#operation/v4.get.records.mine). | 
 **body** | [**List&lt;FeeItemBaseModel1&gt;**](FeeItemBaseModel1.md)| The record fee items information to be updated. | 
 **fields** | **string**| Comma-delimited names of fields to be returned in the response. Note - Field names are case-sensitive and only first-level fields are supported. Invalid field names are ignored. | [optional] 
 **lang** | **string**| Language parameter to support I18N. Default language is en_US. | [optional] 

### Return type

[**ResponseEstimateFeeModel**](ResponseEstimateFeeModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

