using AION.BL;
using AION.BL.BusinessObjects;
using System;

namespace AION.Manager.AccelaBusinessObjects
{
    public class AccelaProjectStatusBO
    {
        private PropertyTypeEnums _propertyType;
        private bool _isPRODRequired;
        private DateTime? _plansReadyOnDate;
        private bool _isProdBlank;
        ProjectStatus _incomingProjectStatus;
        string _workflowTaskStatusDesc;
        public AccelaProjectStatusBO(PropertyTypeEnums propertyType, DateTime? plansReadyOnDate, ProjectStatus incomingProjectStatus)
        {
            _propertyType = propertyType;
            _isPRODRequired = IsPRODRequired();
            _plansReadyOnDate = plansReadyOnDate;
            _isProdBlank = IsPRODBlank();
            _incomingProjectStatus = incomingProjectStatus;
        }

        public string GetPrettyAccelaStatus(string WORKFLOW_TASK_STATUS, string STATUS_DESC)
        {
            _workflowTaskStatusDesc = WORKFLOW_TASK_STATUS + " - " + STATUS_DESC;
            return GetPrettyAccelaStatusString();
        }
        public ProjectStatus GetCurrentProjectStatusFromAccelaStatus(string WORKFLOW_TASK_STATUS, string STATUS_DESC)
        {

            _workflowTaskStatusDesc = WORKFLOW_TASK_STATUS + " - " + STATUS_DESC;
            ProjectStatus currentProjectStatus = ProcessAIONStatusChange();

            return currentProjectStatus;
        }

        private bool IsPRODRequired()
        {
            switch (_propertyType)
            {
                case PropertyTypeEnums.FIFO_Master_Plans:
                case PropertyTypeEnums.FIFO_Single_Family_Homes:
                case PropertyTypeEnums.FIFO_Small_Commercial:
                case PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home:
                    return false;

                default:
                    return true;
            }
        }

        private bool IsPRODBlank()
        {
            if (_plansReadyOnDate.HasValue == false || (_plansReadyOnDate.HasValue && _plansReadyOnDate.Value == DateTime.MinValue))
            {
                return true;
            }
            return false;
        }

        private ProjectStatus ProcessAIONStatusChange()
        {
            ProjectStatus currentProjectStatus = _incomingProjectStatus;
            ProjectStatusModelBO projectStatusModelBO = new ProjectStatusModelBO();
            switch (_workflowTaskStatusDesc)
            {
                case "Abort Package - Aborted":
                    //les-1261 abort package status
                    currentProjectStatus = projectStatusModelBO.GetInstance(ProjectStatusEnum.Abort_Package);
                    break;
                case "Awaiting Plans - Awaiting Plans":
                    //jcl 7/27/2021 - catches any other changes to existing projects and checks if the PROD is blank.
                    if ((_isPRODRequired && _isProdBlank) || (_incomingProjectStatus.ProjectStatusEnum == ProjectStatusEnum.Abort_Package))
                    {
                        currentProjectStatus = projectStatusModelBO.GetInstance(ProjectStatusEnum.PROD_Not_Known);
                    }
                    break;
                case "Awaiting Plans - Fees Paid - Suspended Awaiting Plans":
                case "NA - In Review":
                    //les-1273 Customer paid after suspend status applied. Makes changes as needed during integration
                    //les-3804
                    if (_incomingProjectStatus.ProjectStatusEnum == ProjectStatusEnum.Suspended_Fees_Due)
                    {
                        currentProjectStatus = projectStatusModelBO.GetInstance(ProjectStatusEnum.Scheduled);
                    }
                    break;
                case "Awaiting Plans - No Fees Paid - Suspended Awaiting Fees":
                    currentProjectStatus = projectStatusModelBO.GetInstance(ProjectStatusEnum.Suspended_Fees_Due);
                    break;
                case "Awaiting Revisions - Awaiting Revisions":
                    //jcl 7/27/2021 - catches any other changes to existing projects and checks if the PROD is blank.
                    if ((_isPRODRequired && _isProdBlank) || (_incomingProjectStatus.ProjectStatusEnum == ProjectStatusEnum.Abort_Package))
                    {
                        currentProjectStatus = projectStatusModelBO.GetInstance(ProjectStatusEnum.PROD_Not_Known);
                    }
                    break;
                case "NA - Cancelled":
                    currentProjectStatus = projectStatusModelBO.GetInstance(ProjectStatusEnum.Cancelled);
                    break;
                case "Cancel Project - Pndg Canc-Awaiting Canc Fees":
                    currentProjectStatus = projectStatusModelBO.GetInstance(ProjectStatusEnum.Pending_Cancellation_Awaiting_Cancellation_Fees);
                    break;
                case "Package Rejected - No Fees Paid - Suspended Awaiting Fees":
                case "Package Rejected - Fees Paid - Suspended Awaiting Revisions":
                    break;
                case "Plans Received - Plans Received":
                case "NA - Plans Received":
                    if (_incomingProjectStatus.ProjectStatusEnum == ProjectStatusEnum.Suspended_Fees_Due)
                    {
                        currentProjectStatus = projectStatusModelBO.GetInstance(ProjectStatusEnum.Scheduled);
                    }
                    //jcl 7/27/2021 - catches any other changes to existing projects and checks if the PROD is blank.
                    else if ((_isPRODRequired && _isProdBlank)
                        || (_incomingProjectStatus.ProjectStatusEnum == ProjectStatusEnum.Abort_Package))
                    {
                        currentProjectStatus = projectStatusModelBO.GetInstance(ProjectStatusEnum.PROD_Not_Known);
                    }
                    break;
                case "Revisions Required - Revisions Required":
                    //jcl 7/27/2021 - this could be either scheduled or prod not known
                    //If all pool scenario, mark as scheduled. 

                    break;
                case "Closure - Project Ended - Success":
                case "Complete - Project Ended - Success":
                    //jcl 7/27/2021 - add complete status catch
                    currentProjectStatus = projectStatusModelBO.GetInstance(ProjectStatusEnum.Complete);
                    break;
                case "Abandon Project - Pndg Abdn-Awaiting Canc Fees":
                    currentProjectStatus = projectStatusModelBO.GetInstance(ProjectStatusEnum.Pending_Abandonment);
                    break;
                case "Gate Accepted - No Fees Paid - Suspended Awaiting Fees":
                case "Gate Accepted - No Fees Paid - Suspended-Fees Due":
                    currentProjectStatus = projectStatusModelBO.GetInstance(ProjectStatusEnum.Suspended_Fees_Due);
                    break;
                case "NA - Project Abandoned":
                    currentProjectStatus = projectStatusModelBO.GetInstance(ProjectStatusEnum.Abandoned);
                    break;
                default:
                    //jcl 7/26/2021 leave as whatever is in AION
                    break;
            }
            //this needs to happen after the ProjectStatus object is assigned
            currentProjectStatus.AccelaProjectStatus = _workflowTaskStatusDesc;

            return currentProjectStatus;
        }

        private string GetPrettyAccelaStatusString()
        {
            switch (_workflowTaskStatusDesc)
            {
                case "Abort Package - Aborted":
                case "Awaiting Plans - Fees Paid - Suspended Awaiting Plans":
                case "Awaiting Plans - No Fees Paid - Suspended Awaiting Fees":
                case "Cancel Project - Pndg Canc-Awaiting Canc Fees":
                case "Package Rejected - No Fees Paid - Suspended Awaiting Fees":
                case "Package Rejected - Fees Paid - Suspended Awaiting Revisions":
                case "Closure - Project Ended - Success":
                case "Complete - Project Ended - Success":
                case "Abandon Project - Pndg Abdn-Awaiting Canc Fees":
                case "Gate Accepted - No Fees Paid - Suspended Awaiting Fees":
                case "Gate Accepted - No Fees Paid - Suspended-Fees Due":
                    return _workflowTaskStatusDesc;

                case "NA - Project Abandoned":
                    return "Project Abandoned";
                case "NA - In Review":
                    return "In Review";
                case "NA - Cancelled":
                    return "Cancelled";
                case "Awaiting Revisions - Awaiting Revisions":
                    return "Awaiting Revisions";
                case "Plans Received - Plans Received":
                case "NA - Plans Received":
                    return "Plans Received";
                case "Revisions Required - Revisions Required":
                    return "Revisions Required";
                case "Awaiting Plans - Awaiting Plans":
                    return "Awaiting Plans";

                default:
                    return _workflowTaskStatusDesc;
            }

        }

    }
}