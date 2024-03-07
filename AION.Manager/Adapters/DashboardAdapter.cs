using AION.BL;
using AION.BL.Adapters;
using AION.BL.BusinessObjects;
using AION.BL.Common;
using AION.BL.Models;
using AION.BL.Models.Base;
using AION.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.Accessors;
using AION.Manager.Helpers;
using AION.Manager.Models;
using AION.Manager.Models.Dashboard;
using AIONEstimator.Engine.BusinessObjects;
using Meck.Logging;
using Meck.Shared;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;

namespace AION.Manager.Adapters
{
    public class DashboardAdapter : BaseManagerAdapter, IDashboardAdapter
    {
        /// <summary>
        /// Customer Projects Dashboard
        /// Should show all projects except those in Cancelled status
        /// </summary>
        /// <param name="projectManagerId"></param>
        /// <returns></returns>
        public List<ProjectsList> GetProjects(int projectManagerId)
        {
            List<ProjectsList> ret = new List<ProjectsList>();
            try
            {
                ProjectBO bo = new ProjectBO();
                ProjectStatusModelBO projectStatusModelBO = new ProjectStatusModelBO();

                List<ProjectBE> beList = bo.GetList(projectManagerId);
                foreach (ProjectBE item in beList)
                {
                    //LES-3541 jcl exclude projects in cancelled status
                    ProjectStatusEnum projectStatusEnum = new ProjectStatusModelBO().GetInstance(item.ProjectStatusRefId.Value).ProjectStatusEnum;
                    if (projectStatusEnum == ProjectStatusEnum.Cancelled)
                    {
                        continue;
                    }
                    ProjectsList val = new ProjectsList();
                    val.ProjectID = item.ProjectId.Value;
                    val.AccelaProjectRefID = item.SrcSystemValTxt;
                    val.ProjectName = item.ProjectNm;
                    if (item.ProjectStatusRefId.HasValue)
                    {
                        val.ProjectStatus = projectStatusModelBO.GetInstance(item.ProjectStatusRefId.Value).ProjectStatusEnum;

                    }
                    val.ProjectType = (PropertyTypeEnums)item.ProjectTypRefId.Value;

                    val.TentativeStartDate = item.TentativeStartDate.HasValue && item.TentativeStartDate.Value != DateTime.MinValue ? item.TentativeStartDate.Value : (DateTime?)null;

                    val.AcceptanceDeadLine = item.PlanReviewCreatedDate.HasValue && item.PlanReviewCreatedDate != DateTime.MinValue ? DateTimeHelper.DetermineWorkDateAfterDateSpecified(item.PlanReviewCreatedDate.Value, 2) : (DateTime?)null;

                    val.FacilitatorName = "Unassigned"; // set to default to this

                    if (item.AssignedFacilitatorId.HasValue && item.AssignedFacilitatorId.Value > 0)
                    {

                        UserIdentity facilitator = new UserIdentityModelBO().GetInstance(item.AssignedFacilitatorId.Value);
                        val.FacilitatorName = facilitator.FirstName + " " + facilitator.LastName;

                    }
                    val.RecIdTxt = item.RecIdTxt;
                    val.IsPaidStatus = item.IsPaidStatus;

                    // check for open actions
                    val.HasOpenActions = CheckForCustomerOpenActions(projectStatusEnum, item.ProjectId.Value);

                    ret.Add(val);
                }
                return ret;
            }
            catch (Exception ex)
            {
                var errorMessage = "Error in GetProjects - " + ex.Message;
                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

        }

        protected bool CheckForCustomerOpenActions(ProjectStatusEnum projectStatus, int projectId)
        {
            bool hasOpenActions = false;

            // check trade/agency status
            List<ProjectBusinessRelationshipBE> projectBusinessRelationships = new ProjectBusinessRelationshipBO().GetListByProjectId(projectId);
            if (projectBusinessRelationships.Where(x => x.ProjectBusinessRelationshipStatusDesc == "P").Any() == true)
            {
                hasOpenActions = true;
            }

            // check open reviews or estimation pending
            if (projectStatus == ProjectStatusEnum.Tentatively_Scheduled 
                || projectStatus == ProjectStatusEnum.PROD_Not_Known
                )// 08112023 EL, per Azure bug 200051, comment this out -> || projectStatus == ProjectStatusEnum.Auto_Estimation_Pending
            {
                hasOpenActions = true;
            }

            List<FacilitatorMeetingAppointment> meetings = new FMAAccessor().GetListByProjectId(projectId);

            if (meetings.Where(x => x.ApptResponseStatusEnum == AppointmentResponseStatusEnum.Tentatively_Scheduled).Any() == true)
            {
                hasOpenActions = true;
            }

            return hasOpenActions;
        }

        public List<EstimationDashboardListItem> GetEstimationDashboardProjectList(int userid, DateTime? fromdate = null, DateTime? todate = null)
        {
            EstimationAccelaAdapter estimationAccelaAdapter = new EstimationAccelaAdapter();
            List<EstimationDashboardListItem> list = new List<EstimationDashboardListItem>();
            try
            {
                List<ProjectEstimation> projects = estimationAccelaAdapter.GetProjectEstimationList().ToList();
                foreach (ProjectEstimation project in projects.ToList())
                {
                    list.Add(ConvertPEToDBListItem(project));
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in GetEstimationDashboard - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
            return list;
        }
        public DashboardListBase GetEstimationDashboardListBase(int userid, DateTime? fromdate = null, DateTime? todate = null)
        {
            DashboardListBase dashboardListBase = new DashboardListBase();
            dashboardListBase.EstimationDashboardList = GetEstimationDashboardProjectList(userid, fromdate, todate);
            dashboardListBase.UserUISettings = new UserBO().GetById(userid).UiSettings;

            return dashboardListBase;
        }

        public DashboardListBase GetInternalMeetingsListBase(int wrkId)
        {
            DashboardListBase dashboardListBase = new DashboardListBase();
            dashboardListBase.UserUISettings = new UserBO().GetById(wrkId).UiSettings;
            var permissionMapping = new PermissionAdapter().GetPermissionMappingByUserId(wrkId);
            int inputUserId = wrkId;
            if (permissionMapping.IsFacilitator)
            {
                inputUserId = 0;
            }
            dashboardListBase.InternalMeetings = new SchedulerAdapter().GetInternalMeetings(inputUserId);

            return dashboardListBase;
        }
        public DashboardListBase GetSchedulingDashboardListBase(int userid)
        {
            List<SchedulingDashboardListItem> schedulingDashboardListItems = new List<SchedulingDashboardListItem>();
            
            DashboardListBase dashboardListBase = new DashboardListBase();
            
            List<ProjectEstimation> projects = new SchedulerAdapter().GetSchedulingDashboardList();
            
            foreach (var project in projects)
            {
                string facilitatorName = "Unassigned";

                if (project.AssignedFacilitator.HasValue && project.AssignedFacilitator.Value > 0)
                {
                    UserIdentity facilitator = new UserIdentityModelBO().GetInstance(project.AssignedFacilitator.Value);
                    facilitatorName = facilitator.FirstName + " " + facilitator.LastName;
                }

                schedulingDashboardListItems.Add(
                    new SchedulingDashboardListItem()
                    {
                        ProjectEstimation = project,
                        FacilitatorName = facilitatorName
                    });
            }
            dashboardListBase.SchedulingDashboardList = schedulingDashboardListItems; 
            dashboardListBase.UserUISettings = new UserBO().GetById(userid).UiSettings;

            return dashboardListBase;
        }

        #region "Private Methods"

        /// <summary>
        /// Convert project estimation object to dashboard list item
        /// </summary>
        /// <param name="pe"></param>
        /// <returns></returns>
        private EstimationDashboardListItem ConvertPEToDBListItem(ProjectEstimation pe)
        {
            EstimationDashboardListItem item = new EstimationDashboardListItem();
            ProjectTrade trade = new ProjectTrade();
            ProjectAgency agency = new ProjectAgency();
            Helper helper = new Helper();
            item.AIONProjectId = pe.ID;
            item.AccelaProjectId = pe.AccelaProjectRefId;
            item.ProjectType = pe.AccelaPropertyType.ToStringValue();
            item.RTAP = (pe.IsProjectRTAP ? "Y" : "N");
            item.ProjectCost = pe.ProjectCostTotal.HasValue ? pe.ProjectCostTotal.ToString() : "0";
            item.LastUpdate = pe.UpdatedDate.ToString("MM/dd/yyyy");
            item.ProjectName = pe.ProjectName;
            item.IsPreliminary = pe.IsProjectPreliminary;
            //facilitator
            UserIdentity userBE = new UserIdentity();
            string facilitatorname = "Unassigned"; // set as de
            if (pe.AssignedFacilitator.HasValue && pe.AssignedFacilitator.Value > 0)
            {
                userBE = new UserIdentityModelBO().GetInstance(pe.AssignedFacilitator.Value);
                if (userBE != null)
                    facilitatorname = userBE.FirstName + " " + userBE.LastName;
            }

            item.Facilitator = facilitatorname;


            //TODO
            if (pe.Trades.Count > 0)
            {
                //trades
                trade = GetTradeByDeptNameEnum(DepartmentNameEnums.Building, pe);
                item.B = (trade != null ? SetDeptStatus(trade.DepartmentStatus, pe.AccelaProjectCreatedDate.Value, trade.EstimationNotApplicable) : "");
                trade = GetTradeByDeptNameEnum(DepartmentNameEnums.Electrical, pe);
                item.E = (trade != null ? SetDeptStatus(trade.DepartmentStatus, pe.AccelaProjectCreatedDate.Value, trade.EstimationNotApplicable) : "");
                trade = GetTradeByDeptNameEnum(DepartmentNameEnums.Mechanical, pe);
                item.M = (trade != null ? SetDeptStatus(trade.DepartmentStatus, pe.AccelaProjectCreatedDate.Value, trade.EstimationNotApplicable) : "");
                trade = GetTradeByDeptNameEnum(DepartmentNameEnums.Plumbing, pe);
                item.P = (trade != null ? SetDeptStatus(trade.DepartmentStatus, pe.AccelaProjectCreatedDate.Value, trade.EstimationNotApplicable) : "");

                //trades tooltips
                item.BTooltip = GetDepartmentStatusToolTip(item.B);
                item.ETooltip = GetDepartmentStatusToolTip(item.E);
                item.MTooltip = GetDepartmentStatusToolTip(item.M);
                item.PTooltip = GetDepartmentStatusToolTip(item.P);
            }
            if (pe.Agencies.Count > 0)
            {
                //agencies
                agency = pe.Agencies.Where(x => helper.FireDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();
                item.F = (agency != null ? SetDeptStatus(agency.DepartmentStatus, pe.AccelaProjectCreatedDate.Value, agency.EstimationNotApplicable) : "");
                agency = pe.Agencies.Where(x => helper.ZoneDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();
                //LES-3605 jcl find the jurisdiction from the zone department
                item.Jurisdiction = agency != null ? DashboardHelper.GetJurisdictionByDepartmentNameEnum(agency.DepartmentInfo) : "";

                item.Z = (agency != null ? SetDeptStatus(agency.DepartmentStatus, pe.AccelaProjectCreatedDate.Value, agency.EstimationNotApplicable) : "");
                agency = pe.Agencies.Where(x => helper.EhsDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();
                item.EHS = (agency != null ? SetDeptStatus(agency.DepartmentStatus, pe.AccelaProjectCreatedDate.Value, agency.EstimationNotApplicable) : "");
                agency = pe.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.Backflow).FirstOrDefault();
                item.BF = (agency != null ? SetDeptStatus(agency.DepartmentStatus, pe.AccelaProjectCreatedDate.Value, agency.EstimationNotApplicable) : "");

                //agencies tooltips
                item.FTooltip = GetDepartmentStatusToolTip(item.F);
                item.ZTooltip = GetDepartmentStatusToolTip(item.Z);
                item.EHSTooltip = GetDepartmentStatusToolTip(item.EHS);
                item.BFTooltip = GetDepartmentStatusToolTip(item.BF);



            }

            item.CivilScope = "C";
            item.ElectricalScope = "E";
            item.MechanicalScope = "M";
            item.OverallScope = "O";
            item.PlumbingScope = "P";

            //scope tooltips
            item.CivilScopeTooltip = pe.DisplayOnlyInformation.ScopeOfWorkCivil;
            item.ElectricalScopeTooltip = pe.DisplayOnlyInformation.ScopeOfWorkElectrical;
            item.MechanicalScopeTooltip = pe.DisplayOnlyInformation.ScopeOfWorkMechanical;
            item.PlumbingScopeTooltip = pe.DisplayOnlyInformation.ScopeOfWorkPlumbing;
            item.OverallScopeTooltip = pe.DisplayOnlyInformation.ScopeOfWorkOverall;

            item.TeamScore = pe.TeamGradeTxt;
            item.BuildingSheets = "0";
            item.ElectricalSheets = "0";
            item.MechanicalSheets = "0";
            item.PlumbingSheets = "0";

            item.AccelaNumberofSheets = pe.AccelaNumberofSheets.ToString();

            item.RecIdTxt = pe.RecIdTxt;

            item.ProjectCost = pe.ProjectCostTotal.HasValue ? pe.ProjectCostTotal.Value.ToString() : "0";

            return item;
        }

        private ProjectTrade GetTradeByDeptNameEnum(DepartmentNameEnums department, ProjectEstimation pe)
        {
            return pe.Trades.Where(x => x.DepartmentInfo == department).FirstOrDefault();
        }

        public string SetDeptStatus(string deptInternalStatus, DateTime projectCreationTime, bool isDeptNA)
        {

            bool islate = (projectCreationTime.AddDays(1) < DateTime.Now);
            string displaystatus = deptInternalStatus;
            bool isDebug = false;

            if (!isDebug)
            {
                switch (deptInternalStatus)
                {
                    case ProjectDisplayStatus.AutoEstimationComplete:
                        displaystatus = ProjectDisplayStatus.Complete;
                        break;
                    case ProjectDisplayStatus.AutoEstimationCompleteNA:
                        displaystatus = ProjectDisplayStatus.Complete;
                        break;
                    case ProjectDisplayStatus.AutoEstimationInProgress:
                        //what needs to display?
                        break;
                    case ProjectDisplayStatus.NewApplication:
                        if (islate && !isDeptNA)
                        {
                            displaystatus = ProjectDisplayStatus.Late;
                        }
                        else
                        {
                            displaystatus = ProjectDisplayStatus.None;
                        }
                        break;
                };
            }
            return displaystatus;
        }

        /// <summary>
        /// Blank, P, L, C, CR from SetDeptStatus
        /// </summary>
        /// <param name="departmentStatus"></param>
        /// <returns>string with tooltip</returns>
        private string GetDepartmentStatusToolTip(string departmentStatus)
        {
            if (departmentStatus == ProjectDisplayStatus.Complete)
            {
                return "Estimation has been completed estimation or marked as n/a";
            }
            else if (departmentStatus == ProjectDisplayStatus.CustomerResponded)
            {
                return "Customer has replied to the pending response.";
            }
            else if (departmentStatus == ProjectDisplayStatus.AutoEstimationInProgress)
            {
                return "Auto estimator is still calculating the estimation time.";
            }
            else if (departmentStatus == ProjectDisplayStatus.Pending)
            {
                return "Status has been changed to pending";
            }
            else if (departmentStatus == ProjectDisplayStatus.Late)
            {
                return "More than 1 business day from customer submission.";
            }
            else // ProjectDisplayStatus.None
                return "";// "Less than 1 business day / Less than 24 hours from customer submission, Not in pending or Estimation completed.";
        }

        #endregion "Private Methods"
    }

    public interface IDashboardAdapter
    {
        /// <summary>
        /// Used by Customer Dashboards
        /// REturns projects with statuses: Scheduled, Tentatively_Scheduled
        /// </summary>
        /// <param name="projectManagerId"></param>
        /// <returns></returns>
        List<ProjectsList> GetProjects(int projectManagerId);

        /// <summary>
        /// Get the estimation dashboard project list by from/to date and for number of rows
        /// TODO: implement fromdate and todate
        /// </summary>
        /// <param name="fromdate"></param>
        /// <param name="todate"></param>
        /// <returns></returns>
        List<EstimationDashboardListItem> GetEstimationDashboardProjectList(int userid, DateTime? fromdate = null, DateTime? todate = null);

        DashboardListBase GetEstimationDashboardListBase(int userid, DateTime? fromdate = null, DateTime? todate = null);
        DashboardListBase GetInternalMeetingsListBase(int wrkId);
        DashboardListBase GetSchedulingDashboardListBase(int userid);
    }
}