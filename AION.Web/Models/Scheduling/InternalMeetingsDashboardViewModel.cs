using AION.BL;
using AION.Manager.Models;
using AION.Manager.Models.Dashboard;
using AION.Web.BusinessEntities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AION.Web.Models
{
    public class InternalMeetingsDashboardViewModel : ViewModelBase
    {

        public InternalMeetingsDashboardViewModel()
        {
            LoggedInUser = new UserIdentity();
            PermissionMapping = new PermissionMapping();

            var sortableListObject = new List<DashboardSortableList>() {
                new DashboardSortableList() { Id = "th1", Header = "Project Name", List = "1", Selected = "0", Order = "0", Required = "0"},
                new DashboardSortableList() { Id = "th2", Header = "Project Type", List = "1", Selected = "0", Order = "0", Required = "0"},
                new DashboardSortableList() { Id = "th3", Header = "RTAP", List = "1", Selected = "0", Order = "0", Required = "0"},
                new DashboardSortableList() { Id = "th4", Header = "Status", List = "1", Selected = "0", Order = "0", Required = "0"},
                new DashboardSortableList() { Id = "th5", Header = "Meeting Type", List = "1", Selected = "0", Order = "0", Required = "0"},
                new DashboardSortableList() { Id = "th6", Header = "Meeting Date", List = "1", Selected = "0", Order = "0", Required = "0"},
                new DashboardSortableList() { Id = "th7", Header = "Facilitator", List = "1", Selected = "0", Order = "0", Required = "0"},
                new DashboardSortableList() { Id = "th8", Header = "Team Score", List = "1", Selected = "0", Order = "0", Required = "0"},
                new DashboardSortableList() { Id = "th9", Header = "PM Name", List = "1", Selected = "0", Order = "0", Required = "0"},
                new DashboardSortableList() { Id = "th10", Header = "PM Phone", List = "1", Selected = "0", Order = "0", Required = "0"},
                new DashboardSortableList() { Id = "th11", Header = "PM Email", List = "1", Selected = "0", Order = "0", Required = "0"},
                new DashboardSortableList() { Id = "th12", Header = "Building Code Version", List = "1", Selected = "0", Order = "0", Required = "0"}
            };

            SortableList = JsonConvert.SerializeObject(sortableListObject);
        }
        public List<InternalMeetings> MeetingList { get; set; }

        public string StatusMessage { get; set; }

        public string SavedFilterList { get; set; }

        public string SortableList { get; set; }
    }
}