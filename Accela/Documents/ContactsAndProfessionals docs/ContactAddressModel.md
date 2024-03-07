# AccelaContactsAndProfessionals.Model.ContactAddressModel
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**AddressLine1** | **string** | The first line of the address. | [optional] 
**AddressLine2** | **string** | The second line of the address. | [optional] 
**AddressLine3** | **string** | The third line of the address. | [optional] 
**City** | **string** | The name of the city | [optional] 
**Country** | [**ContactAddressModelCountry**](ContactAddressModelCountry.md) |  | [optional] 
**Direction** | [**ContactAddressModelDirection**](ContactAddressModelDirection.md) |  | [optional] 
**EffectiveDate** | **DateTime?** |  The date when the address takes effect. | [optional] 
**ExpirationDate** | **DateTime?** |  The date when the address expires. | [optional] 
**Fax** | **string** | The fax number for the contact. | [optional] 
**FaxCountryCode** | **string** | Fax Number Country Code | [optional] 
**HouseAlphaEnd** | **string** | The ending street number that makes up the address. | [optional] 
**HouseAlphaStart** | **string** | The beginning street number that makes up the address. | [optional] 
**Id** | **long?** | The id of the address assigned by the Civic Platform server. | [optional] 
**IsPrimary** | **string** | Indicates whether or not to designate the address as the primary address. Only one address can be primary at any given time. | [optional] 
**LevelEnd** | **string** | The ending level number (floor number) that makes up the address within a complex. | [optional] 
**LevelPrefix** | **string** | The prefix for the level numbers (floor numbers) that make up the address. | [optional] 
**LevelStart** | **string** | The beginning level number (floor number) that makes up the address within a complex. | [optional] 
**Phone** | **string** | The phone number of the user. | [optional] 
**PhoneCountryCode** | **string** | The country code for the assoicated phone number. | [optional] 
**PostalCode** | **string** | The postal ZIP code for the address. | [optional] 
**Recipient** | **string** | The contact person for the contact address. | [optional] 
**State** | [**ContactAddressModelState**](ContactAddressModelState.md) |  | [optional] 
**Status** | [**ContactAddressModelStatus**](ContactAddressModelStatus.md) |  | [optional] 
**StreetAddress** | **string** | The street address. | [optional] 
**StreetEnd** | **long?** | The end of a range of street numbers. | [optional] 
**StreetName** | **string** | The street name for the address. | [optional] 
**StreetPrefix** | **string** | Any part of an address that appears before a street name or number. For example, if the address is 123 West Main, \&quot;West\&quot; is the street prefix. | [optional] 
**StreetStart** | **long?** | The start of a range of street numbers. | [optional] 
**StreetSuffix** | [**ContactAddressModelStreetSuffix**](ContactAddressModelStreetSuffix.md) |  | [optional] 
**StreetSuffixDirection** | [**ContactAddressModelStreetSuffixDirection**](ContactAddressModelStreetSuffixDirection.md) |  | [optional] 
**Type** | [**ContactAddressModelType**](ContactAddressModelType.md) |  | [optional] 
**UnitEnd** | **string** | The end parameter of a range of unit numbers. | [optional] 
**UnitStart** | **string** | The starting parameter of a range of unit numbers. | [optional] 
**UnitType** | [**ContactAddressModelUnitType**](ContactAddressModelUnitType.md) |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

