using Newtonsoft.Json;

namespace AION.BL.Models
{
    public class UiSettings
    {
        [JsonProperty("estimationDashboard")]
        public DashboardUiSetting EstimationDashboard { get; set; }
        [JsonProperty("meetingDashboard")]
        public DashboardUiSetting MeetingDashboard { get; set; }
        [JsonProperty("schedulingDashboard")]
        public DashboardUiSetting SchedulingDashboard { get; set; }
    }
    public class DashboardUiSetting
    {
        [JsonProperty("columnsFilter")]
        public string ColumnsFilter { get; set; }
    }
}
