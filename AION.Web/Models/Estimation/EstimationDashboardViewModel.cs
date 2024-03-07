using AION.BL;
using AION.BL.Common;
using AION.BL.Models;
using AION.Manager.Models.Dashboard;
using AION.Web.BusinessEntities;
using Meck.Shared;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace AION.Web.Models
{
    public class EstimationDashboardViewModel : ViewModelBase
    {
        public EstimationDashboardViewModel()
        {
            LoggedInUser = new UserIdentity();
            PermissionMapping = new PermissionMapping();

            var sortableListObject = new List<DashboardSortableList>() {
                new DashboardSortableList() { Id = "th2", Header = "Project Name", List = "1", Selected = "0", Order = "2", Required = "0"},
                new DashboardSortableList() { Id = "th3", Header = "Jurisdiction", List = "1", Selected = "1", Order = "3", Required = "0"},
                new DashboardSortableList() { Id = "th4", Header = "Project Type", List = "1", Selected = "0", Order = "4", Required = "0"},
                new DashboardSortableList() { Id = "th5", Header = "RTAP", List = "1", Selected = "0", Order = "5", Required = "0"},
                new DashboardSortableList() { Id = "th6", Header = "BEMP", List = "1", Selected = "0", Order = "6,7,8,9", Required = "0"},
                new DashboardSortableList() { Id = "th10", Header = "F", List = "1", Selected = "0", Order = "9", Required = "0"},
                new DashboardSortableList() { Id = "th11", Header = "Z", List = "1", Selected = "0", Order = "10", Required = "0"},
                new DashboardSortableList() { Id = "th12", Header = "EHS", List = "1", Selected = "0", Order = "11", Required = "0"},
                new DashboardSortableList() { Id = "th13", Header = "BF", List = "1", Selected = "0", Order = "12", Required = "0"},
                new DashboardSortableList() { Id = "th14", Header = "Project Cost", List = "1", Selected = "0", Order = "13", Required = "0"},
                new DashboardSortableList() { Id = "th15", Header = "Total Sheets", List = "1", Selected = "0", Order = "14", Required = "0"},
                new DashboardSortableList() { Id = "th16", Header = "Facilitator", List = "1", Selected = "0", Order = "15", Required = "0"},
                new DashboardSortableList() { Id = "th17", Header = "Team Score", List = "1", Selected = "0", Order = "16", Required = "0"},
                new DashboardSortableList() { Id = "th18", Header = "Overall Scope", List = "1", Selected = "0", Order = "17", Required = "0"},
                new DashboardSortableList() { Id = "th19", Header = "Electrical Scope", List = "1", Selected = "0", Order = "18", Required = "0"},
                new DashboardSortableList() { Id = "th20", Header = "Mechanical Scope", List = "1", Selected = "0", Order = "19", Required = "0"},
                new DashboardSortableList() { Id = "th21", Header = "Plumbing Scope", List = "1", Selected = "0", Order = "20", Required = "0"},
                new DashboardSortableList() { Id = "th22", Header = "Civil Scope", List = "1", Selected = "0", Order = "21", Required = "0"},
                new DashboardSortableList() { Id = "th23", Header = "Last Updated", List = "1", Selected = "1", Order = "22", Required = "0"}
            };

            SortableList = JsonConvert.SerializeObject(sortableListObject);
        }

        public string SavedFilterList { get; set; }
        public List<EstimationDashboardListItem> EstimationDashboardListItems { get; set; }
        public string StatusMessage { get; set; }
        public string SortableList { get; set; }
        public DashboardListBase DashboardListBase { get; set; }
    }

    public class ProjectEstimationDashBoardDetails : ViewModelBase
    {
        private List<Facilitator> _facilitators;
        private Helper _helper;

        public ProjectEstimationDashBoardDetails(ProjectEstimation baseobj)
        {
            Base = baseobj;
            _helper = new Helper();
        }
        public ProjectEstimation Base { get; set; }

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
        public ProjectTrade BuildTrade
        {
            get
            {
                AION.BL.ProjectTrade etrade = Base.Trades.Where(x => x.DepartmentInfo == AION.BL.DepartmentNameEnums.Building).FirstOrDefault();
                return etrade;
            }
        }
        public ProjectTrade ElecTrade
        {
            get
            {
                AION.BL.ProjectTrade etrade = Base.Trades.Where(x => x.DepartmentInfo == AION.BL.DepartmentNameEnums.Electrical).FirstOrDefault();
                return etrade;
            }
        }
        public ProjectTrade MechTrade
        {
            get
            {
                AION.BL.ProjectTrade etrade = Base.Trades.Where(x => x.DepartmentInfo == AION.BL.DepartmentNameEnums.Mechanical).FirstOrDefault();
                return etrade;
            }
        }
        public ProjectTrade PlumbTrade
        {
            get
            {
                AION.BL.ProjectTrade etrade = Base.Trades.Where(x => x.DepartmentInfo == AION.BL.DepartmentNameEnums.Plumbing).FirstOrDefault();
                return etrade;
            }
        }
        public ProjectAgency FireAgency
        {
            get
            {
                AION.BL.ProjectAgency etrade = Base.Agencies.Where(x => _helper.FireDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();
                return etrade;
            }
        }
        public ProjectAgency ZoneAgency
        {
            get
            {
                AION.BL.ProjectAgency etrade = Base.Agencies.Where(x => _helper.ZoneDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();
                return etrade;
            }

        }
        public ProjectAgency HealthAgency
        {
            get
            {
                AION.BL.ProjectAgency etrade = Base.Agencies.Where(x => _helper.EhsDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();
                return etrade;
            }
        }
        public ProjectAgency BkFlwAgency
        {
            get
            {
                AION.BL.ProjectAgency etrade = Base.Agencies.Where(x => x.DepartmentInfo == AION.BL.DepartmentNameEnums.Backflow).FirstOrDefault();
                return etrade;
            }

        }
        public string BuildToolTip
        {
            get
            {
                return BuildTrade != null ? GetDepartmentStatusToolTip(BuildTrade.DepartmentStatus) : GetDepartmentStatusToolTip("");
            }
        }


        public string MechToolTip
        {
            get
            {
                return MechTrade != null ? GetDepartmentStatusToolTip(MechTrade.DepartmentStatus) : GetDepartmentStatusToolTip("");
            }
        }


        public string ElectToolTip
        {
            get
            {
                return ElecTrade != null ? GetDepartmentStatusToolTip(ElecTrade.DepartmentStatus) : GetDepartmentStatusToolTip("");
            }

        }

        public string PlumpToolTip
        {
            get
            {
                return PlumbTrade != null ? GetDepartmentStatusToolTip(PlumbTrade.DepartmentStatus) : GetDepartmentStatusToolTip("");
            }
        }

        public string FireToolTip
        {
            get
            {
                return FireAgency != null ? GetDepartmentStatusToolTip(FireAgency.DepartmentStatus) : GetDepartmentStatusToolTip("");
            }
        }

        public string ZoneToolTip
        {
            get
            {
                return ZoneAgency != null ? GetDepartmentStatusToolTip(ZoneAgency.DepartmentStatus) : GetDepartmentStatusToolTip("");
            }
        }

        public string EhsToolTip
        {
            get
            {
                return HealthAgency != null ? GetDepartmentStatusToolTip(HealthAgency.DepartmentStatus) : GetDepartmentStatusToolTip("");
            }
        }

        public string BfToolTip
        {
            get
            {
                return BkFlwAgency != null ? GetDepartmentStatusToolTip(BkFlwAgency.DepartmentStatus) : GetDepartmentStatusToolTip("");
            }
        }
        public string BuildDeptStat
        {
            get
            {
                return BuildTrade != null ? BuildTrade.DepartmentStatus : "";
            }
        }
        public string ElecDeptStat
        {
            get
            {
                return ElecTrade != null ? ElecTrade.DepartmentStatus : "";
            }
        }
        public string MechDeptStat
        {
            get
            {
                return MechTrade != null ? MechTrade.DepartmentStatus : "";
            }
        }
        public string PlumbDeptStat
        {
            get
            {
                return PlumbTrade != null ? PlumbTrade.DepartmentStatus : "";
            }
        }
        public string FireDeptStat
        {
            get
            {
                return FireAgency != null ? FireAgency.DepartmentStatus : "";
            }
        }
        public string ZoneDeptStat
        {
            get
            {
                return ZoneAgency != null ? ZoneAgency.DepartmentStatus : "";
            }
        }
        public string HealthDeptStat
        {
            get
            {
                return HealthAgency != null ? HealthAgency.DepartmentStatus : "";
            }
        }
        public string BkFlwDeptStat
        {
            get
            {
                return BkFlwAgency != null ? BkFlwAgency.DepartmentStatus : "";
            }
        }
        public string FacilitatorName
        {
            get
            {
                return Facilitators.Where(y => y.ID == Base.AssignedFacilitator).Select(x => x.FirstName + " " + x.LastName).FirstOrDefault();

            }
        }
        public List<Facilitator> Facilitators
        {
            get
            {
                return _facilitators;
            }
            set
            {
                _facilitators = value;
            }
        }
    }

}