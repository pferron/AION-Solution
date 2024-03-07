# AccelaRecords.Model.InspectionTypeModel
## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**AllowFailChecklistItems** | **string** | Indicates whether or not to allow inspection to pass with failed checklist items for the current inspection type or from previous inspections. | [optional] 
**AllowMultiInspections** | **string** | Indicates whether or not to allow the scheduling of multiple inspections for this inspection type. | [optional] 
**Associations** | [**InspectionTypeAssociationsModel**](InspectionTypeAssociationsModel.md) |  | [optional] 
**CancelRestriction** | [**InspectionRestrictionModel**](InspectionRestrictionModel.md) |  | [optional] 
**CarryoverFlag** | **string** | Indicates how failed guidesheet items for an inspection type are carried over to the next inspection guidesheet.  NULL or empty string : Guidesheet items are not carried over.  \&quot;A\&quot; : Automatic carry-over of failed guidesheet items to the next inspection guidesheet item. | [optional] 
**DefaultDepartment** | [**DepartmentModel**](DepartmentModel.md) |  | [optional] 
**Disciplines** | **List&lt;string&gt;** | The inspection disciplines assigned to the inspection type. | [optional] 
**FlowEnabledFlag** | **string** | Indicates whether or not to include the inspection in the inspection flow process. | [optional] 
**Grade** | **string** | The name of the inspection grade. | [optional] 
**Group** | **string** | The name of a group of inspection types.  | [optional] 
**GroupName** | [**InspectionTypeModelGroupName**](InspectionTypeModelGroupName.md) |  | [optional] 
**GuideGroup** | [**RGuideSheetGroupModel**](RGuideSheetGroupModel.md) |  | [optional] 
**HasCancelPermission** | **string** | Indicates whether or not the user can reschedule the inspection. | [optional] 
**HasFlowFlag** | **string** | Indicates whether or not to include the inspection in the inspection flow process. | [optional] 
**HasNextInspectionAdvance** | **string** | Indicates whether or not the next inspection can be scheduled in advance. | [optional] 
**HasReschdulePermission** | **string** | Indicates whether or not the user can reschedule the inspection. | [optional] 
**HasSchdulePermission** | **string** | Indicates whether or not the user can schedule the inspection. Note that hasSchdulePermission returns \&quot;Y\&quot; if result.inspectionTypes.schdulePermission is either \&quot;REQUEST_ONLY_PENDING\&quot;, \&quot;REQUEST_SAME_DAY_NEXT_DAY\&quot;, or \&quot;SCHEDULE_USING_CALENDAR\&quot;. If result.inspectionTypes.schdulePermission is \&quot;NONE\&quot; or null, hasSchdulePermission returns \&quot;N\&quot;. | [optional] 
**Id** | **long?** | The inspection type system id assigned by the Civic Platform server. | [optional] 
**InspectionEditable** | **string** | Indicates whether or not inspection result, grade or checklist can be edited. | [optional] 
**IsAutoAssign** | **string** | Indicates whether or not you want to automatically reschedule the inspection when the previous inspection status attains Approved status. | [optional] 
**IsRequired** | **string** | Indicates whether or not the information is required. | [optional] 
**IvrNumber** | **long?** | The IVR (Interactive Voice Response) number assigned to the inspection type.  Added in Civic Platform 9.3.0 | [optional] 
**MaxPoints** | **double?** | The number of points allowed for an inspection, after which the inspection fails. | [optional] 
**Priority** | **string** | The priority level assigned to the inspection type. | [optional] 
**PublicVisible** | **string** | Indicates whether or not Accela Citizen Access users can view comment about the inspection results. | [optional] 
**RefereceNumber** | **string** | The reference number associated with an inspection. | [optional] 
**RescheduleRestriction** | [**InspectionRestrictionModel**](InspectionRestrictionModel.md) |  | [optional] 
**ResultGroup** | **string** | The name of a grouping of Inspection results, usually indicative of a range of inspection scores. | [optional] 
**SchdulePermission** | **string** | Returns one of the scheduling permissions in Citizen Access:  NONE - Does not allow public users to schedule this inspection type online.  REQUEST_ONLY_PENDING - Only allows public users to request for an inspection online. The agency coordinates the appointment for the inspection date and time.  REQUEST_SAME_DAY_NEXT_DAY - Allows public users to request an inspection for the same day, next day, or next available day, based on the inspection type calendar parameters defined on the inspection type.  SCHEDULE_USING_CALENDAR - Allows public users to schedule inspections based on the availability on the inspection type calendar. | [optional] 
**Text** | **string** | The localized display text. | [optional] 
**TotalScore** | **long?** | The overall score of the inspection that includes the inspection result, inspection grade, checklist total score and checklist major violation option. | [optional] 
**TotalScoreOption** | **string** | Indicates the method for calculating total scores of checklist items. There are four options:   TOTAL - Gets the total score of all checklists as the inspection score.  MAX - Gets the max score of all checklists as the inspection score.  MIN - Gets the min score of all checklists as the inspection score.  AVG - Gets the average score of all checklists as the inspection score.  SUBTRACT - Subtracts the total score of all the checklist items from the Total Score defined for the inspection type. | [optional] 
**UnitNumber** | **string** | The number of time units (see timeUnitDuration) comprising an inspection. | [optional] 
**Units** | **double?** | The amount of time comprising the smallest time unit for conducting an inspection. | [optional] 
**Value** | **string** | The value for the specified parameter. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

