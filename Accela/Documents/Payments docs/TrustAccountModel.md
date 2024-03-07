# AccelaPayments.Model.TrustAccountModel
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Account** | **string** | The account ID number for the trust account. | [optional] 
**Associations** | [**TrustAccountRequestModelAssociations**](TrustAccountRequestModelAssociations.md) |  | [optional] 
**Balance** | **double?** | The balance of the trust account in dollars. | [optional] 
**Description** | **string** | The description of the trust account. | [optional] 
**Id** | **long?** | The trust account system id assigned by the Civic Platform server. | [optional] 
**IsPrimary** | **string** | Indicates whether or not to designate the trust account as the primary source. | [optional] 
**LedgerAccount** | **string** | The ledger account of the trust account. | [optional] 
**Overdraft** | [**TrustAccountRequestModelOverdraft**](TrustAccountRequestModelOverdraft.md) |  | [optional] 
**OverdraftLimit** | **double?** | The overdraft limit amount, in dollars, for the trust account. | [optional] 
**ServiceProviderCode** | **string** | The unique agency identifier. | [optional] 
**Status** | [**TrustAccountRequestModelStatus**](TrustAccountRequestModelStatus.md) |  | [optional] 
**ThresholdAmount** | **double?** | The minimum amount required in a trust account. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

