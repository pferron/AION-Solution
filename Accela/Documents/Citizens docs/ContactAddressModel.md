# AccelaCitizens.Model.ContactAddressModel
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**AddressLine1** | **string** | The first line of the address. | [optional] 
**AddressLine2** | **string** | The first line of the address. | [optional] 
**City** | **string** | The name of the city. | [optional] 
**Country** | [**ContactAddressModelCountry**](ContactAddressModelCountry.md) |  | [optional] 
**Direction** | [**ContactAddressModelDirection**](ContactAddressModelDirection.md) |  | [optional] 
**EffectiveDate** | **DateTime?** |  | [optional] 
**ExpirationDate** | **DateTime?** |  | [optional] 
**Fax** | **string** |  | [optional] 
**FaxCountryCode** | **string** |  | [optional] 
**HouseAlphaStart** | **string** | The beginning alphabetic unit in street address. | [optional] 
**HouseAlphaEnd** | **string** | The ending alphabetic unit in street address. | [optional] 
**Id** | **long?** | The unique address id assigned by the Civic Platform server. | [optional] 
**IsPrimary** | **string** | Indicates whether or not to designate the address as the primary address. Only one address can be primary at any given time. | [optional] 
**LevelStart** | **string** | The starting level number (floor number) that makes up the address within a complex. | [optional] 
**LevelEnd** | **string** | The ending level number (floor number) that makes up the address within a complex. | [optional] 
**LevelPrefix** | **string** | The prefix for the level numbers (floor numbers) that make up the address. | [optional] 
**Phone** | **string** |  | [optional] 
**PhoneCountryCode** | **string** |  | [optional] 
**PostalCode** | **string** | The postal ZIP code for the address. | [optional] 
**Recipient** | **string** |  | [optional] 
**State** | [**ContactAddressModelState**](ContactAddressModelState.md) |  | [optional] 
**Status** | [**ContactAddressModelStatus**](ContactAddressModelStatus.md) |  | [optional] 
**StreetAddress** | **string** | The street address. | [optional] 
**StreetEnd** | **decimal?** | The ending number of a street address range. | [optional] 
**StreetName** | **string** | The name of the street. | [optional] 
**StreetPrefix** | **string** | Any part of an address that appears before a street name or number. For example, if the address is 123 West Main, \&quot;West\&quot; is the street prefix. | [optional] 
**StreetStart** | **decimal?** | The starting number of a street address range. | [optional] 
**StreetSuffix** | [**ContactAddressModelStreetSuffix**](ContactAddressModelStreetSuffix.md) |  | [optional] 
**StreetSuffixDirection** | [**ContactAddressModelStreetSuffixDirection**](ContactAddressModelStreetSuffixDirection.md) |  | [optional] 
**UnitStart** | **string** | The starting value of a range of unit numbers. | [optional] 
**UnitEnd** | **string** | The ending value of a range of unit numbers. | [optional] 
**UnitType** | [**ContactAddressModelUnitType**](ContactAddressModelUnitType.md) |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

