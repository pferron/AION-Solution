# AccelaPayments.Model.PaymentRequestModel
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Amount** | **double?** | The amount of a payment transaction or account balance. | [optional] 
**Check** | [**CheckModel**](CheckModel.md) |  | [optional] 
**CreditCard** | [**CreditCardModel**](CreditCardModel.md) |  | [optional] 
**Currency** | **string** | The standard ISO 4217 currency code. For example, \&quot;USD\&quot; for US Dollars. | [optional] 
**EntityId** | **string** | The unique ID of the entity or record the payment applies to. | [optional] 
**EntityType** | **string** | The type of entity the payment applies to. | [optional] 
**PaymentMethod** | **string** | The method of payment, for example, credit card, check, trust account. | [optional] 
**TrustAccount** | [**TrustAccountRequestModel**](TrustAccountRequestModel.md) |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

