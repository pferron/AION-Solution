# AccelaSettings.Model.ChecklistItemModel
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Comment** | **string** | Comments about the checklist item. | [optional] 
**DefaultScore** | **long?** |  The default score associated with the checklist item. | [optional] 
**DisplayOrder** | **long?** |  The order of the item in comparison to the other items. | [optional] 
**Id** | **long?** | The ID of the checklist item assigned by the Civic Platform server. | [optional] 
**IsCarryOver** | **string** |  Indicates whether or not the checklist is a carry-over. | [optional] 
**IsCommentVisible** | **string** |  Indicates whether or not the comment is visible in a checklist. | [optional] 
**IsCritical** | **string** |  Indicates whether or not the checklist item is critical. | [optional] 
**IsMaxPointsVisible** | **string** |  Indicates whether or not to display the maximum points for the inspection checklist. | [optional] 
**IsScoreVisible** | **string** |  Indicates whether or not to display the inspection checklist score. | [optional] 
**MaxPoints** | **double?** |  The number of points allowed for an inspection, after which the inspection fails. | [optional] 
**StatusGroup** | **string** | Defines a set of status values for use with inspection types that conform to similar code requirements. For example, status groups for building related inspections, such as mechanical, electrical, uniform plumbing, and zoning. | [optional] 
**Statuses** | [**List&lt;ChecklistItemStatusModel&gt;**](ChecklistItemStatusModel.md) | Uses several parameters to characterize the status of one or more inspection checklists | [optional] 
**Text** | **string** | The checklist item name. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

