# AccelaAddressParcelsOwners.Model.ParcelModel
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Block** | **string** | The block number associated with the parcel. | [optional] 
**Book** | **string** | A reference to the physical location of parcel information in the County Assessor&#39;s office. | [optional] 
**CensusTract** | **string** | The unique number assigned by the Census Bureau that identifies the tract to which this parcel belongs. | [optional] 
**CouncilDistrict** | **string** | The council district to which the parcel belongs. | [optional] 
**CustomForms** | [**List&lt;CustomAttributeModel&gt;**](CustomAttributeModel.md) |  | [optional] 
**ExemptionValue** | **double?** | The total value of any tax exemptions that apply to the land within the parcel. | [optional] 
**GisSequenceNumber** | **long?** | The GIS object ID of the parcel. | [optional] 
**Id** | **string** | The system id of the parcel assigned by the Civic Platform server. | [optional] 
**ImprovedValue** | **double?** | The total value of any improvements to the land within the parcel. | [optional] 
**IsPrimary** | **string** | Indicates whether or not to designate the parcel as the primary parcel. | [optional] 
**LandValue** | **double?** | The total value of the land within the parcel. | [optional] 
**LegalDescription** | **string** | The legal description of the parcel. | [optional] 
**Lot** | **string** | The lot name. | [optional] 
**MapNumber** | **string** | The unique map number that identifies the map for this parcel. | [optional] 
**MapReferenceInfo** | **string** | The map reference for this parcel. | [optional] 
**Page** | **string** | A reference to the physical location of the parcel information in the records of the County Assessor (or other responsible department). | [optional] 
**Parcel** | **string** | The official parcel name or number, as determined by the county assessor or other responsible department. | [optional] 
**ParcelArea** | **double?** | The total area of the parcel. Your agency determines the standard unit of measure. | [optional] 
**ParcelNumber** | **string** | The alpha-numeric parcel number. | [optional] 
**PlanArea** | **string** | The total area of the parcel. Your agency determines the standard unit of measure. | [optional] 
**Range** | **string** | When land is surveyed using the rectangular-survey system, range represents the measure of units east and west of the base line. | [optional] 
**Section** | **long?** | A piece of a township measuring 640 acres, one square mile, numbered with reference to the base line and meridian line. | [optional] 
**Status** | [**ParcelModelStatus**](ParcelModelStatus.md) |  | [optional] 
**Subdivision** | [**ParcelModelSubdivision**](ParcelModelSubdivision.md) |  | [optional] 
**SupervisorDistrict** | **string** | The supervisor district to which the parcel belongs. | [optional] 
**Township** | **string** | When land is surveyed using the rectangular-survey system, township represents the measure of units North or South of the base line. Townships typically measure 6 miles to a side, or 36 square miles. | [optional] 
**Tract** | **string** | The name of the tract associated with this application. A tract may contain one or more related parcels. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

