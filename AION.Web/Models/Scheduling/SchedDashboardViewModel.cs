using AION.BL;
using AION.BL.Models;
using AION.Manager.Models.Dashboard;
using AION.Web.BusinessEntities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AION.Web.Models
{
    public class SchedDashboardViewModel : ViewModelBase
    {
        public SchedDashboardViewModel()
        {
            LoggedInUser = new UserIdentity();
            PermissionMapping = new PermissionMapping();

            var sortableListObject = new List<DashboardSortableList>() {
                new DashboardSortableList() { Id = "th2", Header = "Project Name", List = "1", Selected = "0", Order = "0", Required = "0"},
                new DashboardSortableList() { Id = "th3", Header = "Project Type", List = "1", Selected = "0", Order = "0", Required = "0"},
                new DashboardSortableList() { Id = "th4", Header = "Meeting Type", List = "1", Selected = "0", Order = "0", Required = "0"},
                new DashboardSortableList() { Id = "th5", Header = "RTAP", List = "1", Selected = "0", Order = "0", Required = "0"},
                new DashboardSortableList() { Id = "th6", Header = "Status", List = "1", Selected = "0", Order = "0", Required = "0"},
                new DashboardSortableList() { Id = "th7", Header = "Paid", List = "1", Selected = "0", Order = "0", Required = "0"},
                new DashboardSortableList() { Id = "th8", Header = "App Received", List = "1", Selected = "0", Order = "0", Required = "0"},
                new DashboardSortableList() { Id = "th9", Header = "PROD", List = "1", Selected = "0", Order = "0", Required = "0"},
                new DashboardSortableList() { Id = "th10", Header = "Review Start", List = "1", Selected = "0", Order = "0", Required = "0"},
                new DashboardSortableList() { Id = "th11", Header = "Acceptance Deadline", List = "1", Selected = "0", Order = "0", Required = "0"},
                new DashboardSortableList() { Id = "th12", Header = "Facilitator", List = "1", Selected = "0", Order = "0", Required = "0"},
                new DashboardSortableList() { Id = "th13", Header = "Team Score", List = "1", Selected = "0", Order = "0", Required = "0"},
                new DashboardSortableList() { Id = "th14", Header = "PM Name", List = "1", Selected = "0", Order = "0", Required = "0"},
                new DashboardSortableList() { Id = "th15", Header = "PM Phone", List = "1", Selected = "0", Order = "0", Required = "0"},
                new DashboardSortableList() { Id = "th16", Header = "PM Email", List = "1", Selected = "0", Order = "0", Required = "0"}
            };

            SortableList = JsonConvert.SerializeObject(sortableListObject);
        }
        public string SavedFilterList { get; set; }
        public string SortableList { get; set; }
        private List<SchedulingDashBoardDetails> _projects;
        private List<Facilitator> _facilitators;
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


        public List<SchedulingDashBoardDetails> Projects
        {
            get
            {
                if (_projects != null)
                    _projects = _projects.Where(x =>
                        GetEstimationComplete(x.Base.Agencies, x.Base.Trades) == true || x.Base.IsFacilitatorMeeting == true).ToList();
                return _projects;
            }
            set
            {
                _projects = value;
            }
        }
        public string StatusMessage { get; set; }
    }

    public class SchedulingDashBoardDetails : ViewModelBase
    {
        public string FacilitatorName { get; set; }

        public SchedulingDashBoardDetails(SchedulingDashboardListItem schedulingDashboardListItem)
        {
            Base = schedulingDashboardListItem.ProjectEstimation;
            FacilitatorName = schedulingDashboardListItem.FacilitatorName;
        }

        public ProjectEstimation Base { get; set; }

        public string MeetingTypeDesc
        {
            get
            {
                return Base.MeetingTypeEnum != null ? Base.MeetingTypeEnum.ToStringValue() : string.Empty;
            }
        }

        public string Status
        {
            get
            {
                return Base.AIONProjectStatus.ProjectStatusEnum.ToStringValue();
            }
        }

        public string MeetingStatus
        {
            get
            {
                return Base.MeetingStatusEnum != null ? Base.MeetingStatusEnum.ToStringValue() : string.Empty;
            }
        }

        public string PROD
        {
            get
            {
                return Base.PlansReadyOnDate.HasValue ? Base.PlansReadyOnDate.Value.ToShortDateString() : string.Empty;
            }
        }

        public string OnTime
        {
            get
            {
                var today = DateTime.Now;
                DateTime updatedDate = Base.UpdatedDate;

                if (updatedDate.AddHours(24) <= DateTime.Now)
                    return "N";
                else
                    return "Y";
            }


        }

    }
}