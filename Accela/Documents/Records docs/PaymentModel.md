# AccelaRecords.Model.PaymentModel
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Amount** | **double?** | The amount of a payment transaction or account balance. | [optional] 
**AmountNotAllocated** | **double?** | The payment amount which has not been allocated. | [optional] 
**CashierId** | **string** | The unique ID associated with the cashier. | [optional] 
**Id** | **long?** | The payment system id assigned by the Civic Platform server. | [optional] 
**PaymentDate** | **DateTime?** | The date a payment was entered into the system. | [optional] 
**PaymentMethod** | **string** | Describes the method of payment, for example; credit card, cash, debit card, and so forth. | [optional] 
**PaymentStatus** | **string** | Indicates whether or not a payment has been made in full. | [optional] 
**ReceiptId** | **long?** | The unique ID generated for the recipient. | [optional] 
**RecordId** | [**RecordIdModel**](RecordIdModel.md) |  | [optional] 
**TransactionCode** | **string** | An industry standard code that identifies the type of transaction. | [optional] 
**TransactionId** | **long?** | A unique number, assigned by the system, that indentifies the transaction. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

