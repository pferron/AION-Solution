# AccelaPayments.Model.PaymentCommitModel
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Amount** | **double?** | The payment amount. | 
**Comments** | **string** | Comments related to the payment transaction. | [optional] 
**ConvenienceFee** | **double?** | The payment convenience fee to be applied. Set to 0 if none. | 
**CreditCard** | [**CreditCardLastFourDigitsModel**](CreditCardLastFourDigitsModel.md) |  | [optional] 
**MerchantAccountId** | **string** | The account ID of the merchant receiving the payment. | [optional] 
**PayeePhone** | **string** | The area code and phone number of the payee. | [optional] 
**PaymentMethod** | **string** | The method of payment, either Credit Card or Check. | 
**PaymentSystemTransactionId** | **string** | The third party payment system&#x27;s payment transaction ID. | 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

