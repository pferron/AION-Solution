# AccelaAddressParcelsOwners - the C# library for the Addresses, Parcels, Owners

Use the Address-Parcel-Owner (\"APO\") API to get, create, and update reference information about addresses, parcels, and owners used in land or property management solutions. Because reference APO can be associated to multiple transactional records, a reference APO object cannot be deleted.

This C# SDK is automatically generated by the [Swagger Codegen](https://github.com/swagger-api/swagger-codegen) project:

- API version: v4
- SDK version: 1.0.0
- Build package: io.swagger.codegen.languages.CSharpClientCodegen

<a name="frameworks-supported"></a>
## Frameworks supported
- .NET 4.0 or later
- Windows Phone 7.1 (Mango)

<a name="dependencies"></a>
## Dependencies
- [RestSharp](https://www.nuget.org/packages/RestSharp) - 105.1.0 or later
- [Json.NET](https://www.nuget.org/packages/Newtonsoft.Json/) - 7.0.0 or later
- [JsonSubTypes](https://www.nuget.org/packages/JsonSubTypes/) - 1.2.0 or later

The DLLs included in the package may not be the latest version. We recommend using [NuGet](https://docs.nuget.org/consume/installing-nuget) to obtain the latest version of the packages:
```
Install-Package RestSharp
Install-Package Newtonsoft.Json
Install-Package JsonSubTypes
```

NOTE: RestSharp versions greater than 105.1.0 have a bug which causes file uploads to fail. See [RestSharp#742](https://github.com/restsharp/RestSharp/issues/742)

<a name="installation"></a>
## Installation
Run the following command to generate the DLL
- [Mac/Linux] `/bin/sh build.sh`
- [Windows] `build.bat`

Then include the DLL (under the `bin` folder) in the C# project, and use the namespaces:
```csharp
using AccelaAddressParcelsOwners.Api;
using AccelaAddressParcelsOwners.Client;
using AccelaAddressParcelsOwners.Model;
```
<a name="packaging"></a>
## Packaging

A `.nuspec` is included with the project. You can follow the Nuget quickstart to [create](https://docs.microsoft.com/en-us/nuget/quickstart/create-and-publish-a-package#create-the-package) and [publish](https://docs.microsoft.com/en-us/nuget/quickstart/create-and-publish-a-package#publish-the-package) packages.

This `.nuspec` uses placeholders from the `.csproj`, so build the `.csproj` directly:

```
nuget pack -Build -OutputDirectory out AccelaAddressParcelsOwners.csproj
```

Then, publish to a [local feed](https://docs.microsoft.com/en-us/nuget/hosting-packages/local-feeds) or [other host](https://docs.microsoft.com/en-us/nuget/hosting-packages/overview) and consume the new package via Nuget as usual.

<a name="getting-started"></a>
## Getting Started

```csharp
using System;
using System.Diagnostics;
using AccelaAddressParcelsOwners.Api;
using AccelaAddressParcelsOwners.Client;
using AccelaAddressParcelsOwners.Model;

namespace Example
{
    public class Example
    {
        public void main()
        {

            var apiInstance = new AddressesApi();
            var contentType = contentType_example;  // string | Must be application/x-www-form-urlencoded.
            var xAccelaAppid = xAccelaAppid_example;  // string | clientid
            var id = 789;  // long? | The unique address id assigned by the Civic Platform server. (optional) 
            var type = type_example;  // string | The address type. (optional) 
            var isPrimary = isPrimary_example;  // string | Indicates whether or not to designate the address as the primary address. Only one address can be primary at any given time. (optional) 
            var streetName = streetName_example;  // string | The street name for the address. (optional) 
            var streetStart = 789;  // long? | The start of a range of street numbers. (optional) 
            var streetEnd = 789;  // long? | RefAddress streetEnd (optional) 
            var direction = direction_example;  // string | The street direction of the primary address associated with the application. (optional) 
            var streetSuffixDirection = streetSuffixDirection_example;  // string | The street direction of the primary address associated with the application. (optional) 
            var streetSuffix = streetSuffix_example;  // string | The street direction of the primary address associated with the application. (optional) 
            var city = city_example;  // string | The name of the city. (optional) 
            var country = country_example;  // string | The name of the country. (optional) 
            var state = state_example;  // string | The state corresponding to the address on record. (optional) 
            var expand = expand_example;  // string | Related objects to be returned with the response. The related object(s) will be returned if data exists; if data does not exist, the requested object(s) will not be included in the response.   Added in Civic Platform version: 9.3.0 (optional) 
            var offset = 789;  // long? | The offset position of the first record in the results response array.For example,  if offset is 100,the first item in the results array in the response is the 100th record in the search result list. (optional) 
            var limit = 789;  // long? | Search result size limit. (optional) 
            var fields = fields_example;  // string | Comma - delimited names of fields to be returned in the response.Note - Field names are case -sensitive and only first - level fields are supported.Invalid field names are ignored. (optional) 
            var lang = lang_example;  // string | Language parameter to support I18N.Default language is en_US. (optional) 

            try
            {
                // Get All Addresses
                ResponseAddressModelArray result = apiInstance.V4GetAddresses(contentType, xAccelaAppid, id, type, isPrimary, streetName, streetStart, streetEnd, direction, streetSuffixDirection, streetSuffix, city, country, state, expand, offset, limit, fields, lang);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AddressesApi.V4GetAddresses: " + e.Message );
            }

        }
    }
}
```

<a name="documentation-for-api-endpoints"></a>
## Documentation for API Endpoints

All URIs are relative to *http://apis.accela.com*

Class | Method | HTTP request | Description
------------ | ------------- | ------------- | -------------
*AddressesApi* | [**V4GetAddresses**](docs/AddressesApi.md#v4getaddresses) | **GET** /v4/addresses | Get All Addresses
*AddressesApi* | [**V4GetAddressesId**](docs/AddressesApi.md#v4getaddressesid) | **GET** /v4/addresses/{id} | Get Address
*AddressesApi* | [**V4GetAddressesIdConditions**](docs/AddressesApi.md#v4getaddressesidconditions) | **GET** /v4/addresses/{id}/conditions | Get All Address Conditions
*AddressesApi* | [**V4GetAddressesIdParcels**](docs/AddressesApi.md#v4getaddressesidparcels) | **GET** /v4/addresses/{id}/parcels | Get All Address Parcels
*AddressesApi* | [**V4GetAddressesIdRecords**](docs/AddressesApi.md#v4getaddressesidrecords) | **GET** /v4/addresses/{id}/records | Get All Address Records
*AddressesApi* | [**V4PutAddressesId**](docs/AddressesApi.md#v4putaddressesid) | **PUT** /v4/addresses/{id} | Update an Address
*OwnersApi* | [**V4GetOwners**](docs/OwnersApi.md#v4getowners) | **GET** /v4/owners | Get All Owners
*OwnersApi* | [**V4GetOwnersId**](docs/OwnersApi.md#v4getownersid) | **GET** /v4/owners/{id} | Get Owner
*OwnersApi* | [**V4GetOwnersOwnerIdConditions**](docs/OwnersApi.md#v4getownersowneridconditions) | **GET** /v4/owners/{ownerId}/conditions | Get All Owner Conditions
*ParcelsApi* | [**V4GetParcels**](docs/ParcelsApi.md#v4getparcels) | **GET** /v4/parcels | Get All Parcels
*ParcelsApi* | [**V4GetParcelsId**](docs/ParcelsApi.md#v4getparcelsid) | **GET** /v4/parcels/{id} | Get Parcel
*ParcelsApi* | [**V4GetParcelsIdAddresses**](docs/ParcelsApi.md#v4getparcelsidaddresses) | **GET** /v4/parcels/{id}/addresses | Get All Parcel Addresses
*ParcelsApi* | [**V4GetParcelsIdRecords**](docs/ParcelsApi.md#v4getparcelsidrecords) | **GET** /v4/parcels/{id}/records | Get All Parcel Records
*ParcelsApi* | [**V4GetParcelsParcelIdConditions**](docs/ParcelsApi.md#v4getparcelsparcelidconditions) | **GET** /v4/parcels/{parcelId}/conditions | Get All Parcel Conditions
*ParcelsApi* | [**V4GetParcelsParcelIdOwners**](docs/ParcelsApi.md#v4getparcelsparcelidowners) | **GET** /v4/parcels/{parcelId}/owners | Get All Parcel Owners


<a name="documentation-for-models"></a>
## Documentation for Models

 - [Model.AddressModelWithCustomForms](docs/AddressModelWithCustomForms.md)
 - [Model.AddressModel_](docs/AddressModel_.md)
 - [Model.ConditionModel](docs/ConditionModel.md)
 - [Model.ConditionModelActionbyDepartment](docs/ConditionModelActionbyDepartment.md)
 - [Model.ConditionModelActionbyUser](docs/ConditionModelActionbyUser.md)
 - [Model.ConditionModelActiveStatus](docs/ConditionModelActiveStatus.md)
 - [Model.ConditionModelAppliedbyDepartment](docs/ConditionModelAppliedbyDepartment.md)
 - [Model.ConditionModelAppliedbyUser](docs/ConditionModelAppliedbyUser.md)
 - [Model.ConditionModelGroup](docs/ConditionModelGroup.md)
 - [Model.ConditionModelInheritable](docs/ConditionModelInheritable.md)
 - [Model.ConditionModelPriority](docs/ConditionModelPriority.md)
 - [Model.ConditionModelSeverity](docs/ConditionModelSeverity.md)
 - [Model.ConditionModelStatus](docs/ConditionModelStatus.md)
 - [Model.ConditionModelType](docs/ConditionModelType.md)
 - [Model.CustomAttributeModel](docs/CustomAttributeModel.md)
 - [Model.OwnerAddressModel](docs/OwnerAddressModel.md)
 - [Model.OwnerAddressModelCountry](docs/OwnerAddressModelCountry.md)
 - [Model.OwnerModel](docs/OwnerModel.md)
 - [Model.OwnerModelStatus](docs/OwnerModelStatus.md)
 - [Model.OwnerModelWithCustomForms](docs/OwnerModelWithCustomForms.md)
 - [Model.PageModel](docs/PageModel.md)
 - [Model.ParcelModel](docs/ParcelModel.md)
 - [Model.ParcelModelStatus](docs/ParcelModelStatus.md)
 - [Model.ParcelModelSubdivision](docs/ParcelModelSubdivision.md)
 - [Model.RecordExpirationModel](docs/RecordExpirationModel.md)
 - [Model.RecordExpirationModelExpirationStatus](docs/RecordExpirationModelExpirationStatus.md)
 - [Model.RecordIdModel](docs/RecordIdModel.md)
 - [Model.RecordTypeModel](docs/RecordTypeModel.md)
 - [Model.RequestAddressModelWithCustomForms](docs/RequestAddressModelWithCustomForms.md)
 - [Model.RequestAddressModelWithCustomFormsAddressTypeFlag](docs/RequestAddressModelWithCustomFormsAddressTypeFlag.md)
 - [Model.RequestAddressModelWithCustomFormsCountry](docs/RequestAddressModelWithCustomFormsCountry.md)
 - [Model.RequestAddressModelWithCustomFormsDirection](docs/RequestAddressModelWithCustomFormsDirection.md)
 - [Model.RequestAddressModelWithCustomFormsHouseFractionEnd](docs/RequestAddressModelWithCustomFormsHouseFractionEnd.md)
 - [Model.RequestAddressModelWithCustomFormsHouseFractionStart](docs/RequestAddressModelWithCustomFormsHouseFractionStart.md)
 - [Model.RequestAddressModelWithCustomFormsStatus](docs/RequestAddressModelWithCustomFormsStatus.md)
 - [Model.RequestAddressModelWithCustomFormsStreetSuffix](docs/RequestAddressModelWithCustomFormsStreetSuffix.md)
 - [Model.RequestAddressModelWithCustomFormsStreetSuffixDirection](docs/RequestAddressModelWithCustomFormsStreetSuffixDirection.md)
 - [Model.RequestAddressModelWithCustomFormsUnitType](docs/RequestAddressModelWithCustomFormsUnitType.md)
 - [Model.ResponseAddressModelArray](docs/ResponseAddressModelArray.md)
 - [Model.ResponseAddressModelWithCustomForms](docs/ResponseAddressModelWithCustomForms.md)
 - [Model.ResponseAddressModel_](docs/ResponseAddressModel_.md)
 - [Model.ResponseConditionModelArray](docs/ResponseConditionModelArray.md)
 - [Model.ResponseOwnerModel](docs/ResponseOwnerModel.md)
 - [Model.ResponseOwnerModelArray](docs/ResponseOwnerModelArray.md)
 - [Model.ResponseOwnerModelWithCustomForms](docs/ResponseOwnerModelWithCustomForms.md)
 - [Model.ResponseOwnerModelWithCustomFormsArray](docs/ResponseOwnerModelWithCustomFormsArray.md)
 - [Model.ResponseParcelModel](docs/ResponseParcelModel.md)
 - [Model.ResponseParcelModelArray](docs/ResponseParcelModelArray.md)
 - [Model.ResponseSimpleRecordModelArray](docs/ResponseSimpleRecordModelArray.md)
 - [Model.SimpleRecordModel](docs/SimpleRecordModel.md)
 - [Model.SimpleRecordModelConstructionType](docs/SimpleRecordModelConstructionType.md)
 - [Model.SimpleRecordModelPriority](docs/SimpleRecordModelPriority.md)
 - [Model.SimpleRecordModelReportedChannel](docs/SimpleRecordModelReportedChannel.md)
 - [Model.SimpleRecordModelReportedType](docs/SimpleRecordModelReportedType.md)
 - [Model.SimpleRecordModelStatus](docs/SimpleRecordModelStatus.md)
 - [Model.SimpleRecordModelStatusReason](docs/SimpleRecordModelStatusReason.md)


<a name="documentation-for-authorization"></a>
## Documentation for Authorization

All endpoints do not require authorization.
