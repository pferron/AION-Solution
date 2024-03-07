# AccelaRecords.Model.TaskItemModel
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**ActionbyDepartment** | [**RecordConditionModelActionbyDepartment**](RecordConditionModelActionbyDepartment.md) |  | [optional] 
**ActionbyUser** | [**RecordConditionModelActionbyUser**](RecordConditionModelActionbyUser.md) |  | [optional] 
**Approval** | **string** | Used to indicate supervisory approval of an adhoc task. | [optional] 
**AssignEmailDisplay** | **string** | Indicates whether or not to display the agency employeeâ€™s email address in ACA. Public users can then click the e-mail hyperlink and send an e-mail to the agency employee. â€œYâ€ : display the email address. â€œNâ€ : hide the email address. | [optional] 
**AssignedDate** | **DateTime?** | The date of the assigned action. | [optional] 
**AssignedToDepartment** | [**RecordConditionModelActionbyDepartment**](RecordConditionModelActionbyDepartment.md) |  | [optional] 
**AssignedUser** | [**TaskItemModelAssignedUser**](TaskItemModelAssignedUser.md) |  | [optional] 
**Billable** | **string** | Indicates whether or not the item is billable. | [optional] 
**Comment** | **string** | Comments or notes about the current context. | [optional] 
**CommentDisplay** | **string** | Indicates whether or not Accela Citizen Access users can view the inspection results comments.  | [optional] 
**CommentPublicVisible** | **List&lt;string&gt;** | Specifies the type of user who can view the inspection result comments. &lt;br/&gt;\&quot;All ACA Users\&quot; - Both registered and anonymous Accela Citizen Access users can view the comments for inspection results. &lt;br/&gt;\&quot;Record Creator Only\&quot; - the user who created the record can see the comments for the inspection results. &lt;br/&gt;\&quot;Record Creator and Licensed Professional\&quot; - The user who created the record and the licensed professional associated with the record can see the comments for the inspection results. | [optional] 
**CurrentTaskId** | **string** | The ID of the current workflow task. | [optional] 
**DaysDue** | **long?** | The amount of time to complete a task (measured in days). | [optional] 
**Description** | **string** | The description of the record or item. | [optional] 
**DispositionNote** | **string** | A note describing the disposition of the current task. | [optional] 
**DueDate** | **DateTime?** | The desired completion date of the task. | [optional] 
**EndTime** | **DateTime?** | The time the workflow task was completed. | [optional] 
**EstimatedDueDate** | **DateTime?** | The estimated date of completion. | [optional] 
**EstimatedHours** | **double?** | The estimated hours necessary to complete this task. | [optional] 
**HoursSpent** | **double?** | Number of hours used for a workflow or workflow task. | [optional] 
**Id** | **string** | The workflow task system id assigned by the Civic Platform server. | [optional] 
**InPossessionTime** | **double?** | The application level in possession time of the time tracking feature. | [optional] 
**IsActive** | **string** | Indicates whether or not the workflow task is active. | [optional] 
**IsCompleted** | **string** | Indicates whether or not the workflow task is completed. | [optional] 
**LastModifiedDate** | **DateTime?** | The date when the task item was last changed. | [optional] 
**NextTaskId** | **string** | The id of the next task in a workflow. | [optional] 
**OverTime** | **string** | A labor cost factor that indicates time worked beyond a worker&#39;s regular working hours. | [optional] 
**ProcessCode** | **string** | The process code for the next task in a workflow. | [optional] 
**RecordId** | [**RecordIdModel**](RecordIdModel.md) |  | [optional] 
**ServiceProviderCode** | **string** | The unique agency identifier. | [optional] 
**StartTime** | **DateTime?** | The time the workflow task started. | [optional] 
**Status** | [**TaskItemModelStatus**](TaskItemModelStatus.md) |  | [optional] 
**StatusDate** | **DateTime?** | The date when the current status changed. | [optional] 
**TrackStartDate** | **DateTime?** | The date that time tracking is set to begin. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

