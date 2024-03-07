# AccelaRecords.Model.RequestTaskItemModel
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**ActionbyDepartment** | [**RecordConditionModelActionbyDepartment**](RecordConditionModelActionbyDepartment.md) |  | [optional] 
**ActionbyUser** | [**RecordConditionModelActionbyUser**](RecordConditionModelActionbyUser.md) |  | [optional] 
**Approval** | **string** | Used to indicate supervisory approval of an adhoc task. | [optional] 
**AssignEmailDisplay** | **string** | Indicates whether or not to display the agency employeeâ€™s email address in ACA. Public users can then click the e-mail hyperlink and send an e-mail to the agency employee. â€œYâ€ : display the email address. â€œNâ€ : hide the email address. | [optional] 
**Billable** | **string** | Indicates whether or not the item is billable. | [optional] 
**Comment** | **string** | Comments or notes about the current context. | [optional] 
**CommentDisplay** | **string** | Indicates whether or not Accela Citizen Access users can view the inspection results comments.  | [optional] 
**CommentPublicVisible** | **List&lt;string&gt;** | Specifies the type of user who can view the inspection result comments. &lt;br/&gt;\&quot;All ACA Users\&quot; - Both registered and anonymous Accela Citizen Access users can view the comments for inspection results. &lt;br/&gt;\&quot;Record Creator Only\&quot; - the user who created the record can see the comments for the inspection results. &lt;br/&gt;\&quot;Record Creator and Licensed Professional\&quot; - The user who created the record and the licensed professional associated with the record can see the comments for the inspection results. | [optional] 
**DueDate** | **DateTime?** | The desired completion date of the task. | [optional] 
**EndTime** | **DateTime?** | The time the workflow task was completed. | [optional] 
**HoursSpent** | **double?** | Number of hours used for a workflow or workflow task. | [optional] 
**OverTime** | **string** | A labor cost factor that indicates time worked beyond a worker&#39;s regular working hours. | [optional] 
**StartTime** | **DateTime?** | The time the workflow task started. | [optional] 
**Status** | [**TaskItemModelStatus**](TaskItemModelStatus.md) |  | [optional] 
**StatusDate** | **DateTime?** | The date when the current status changed. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

