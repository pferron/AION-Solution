using AION.BL.Models;
using System.Collections.Generic;

namespace AION.Manager.Models.Dashboard
{
    public class DashboardListBase
    {
        public string UserUISettings { get; set; }
        public List<EstimationDashboardListItem> EstimationDashboardList { get; set; }

        public List<Facilitator> Facilitators { get; set; }

        public List<SchedulingDashboardListItem> SchedulingDashboardList { get; set; }

        public List<InternalMeetings> InternalMeetings { get; set; }
    }
}