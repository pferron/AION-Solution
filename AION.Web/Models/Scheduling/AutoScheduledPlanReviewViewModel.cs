using AION.Manager.Models;

namespace AION.Web.Models.Scheduling
{
    public class AutoScheduledPlanReviewViewModel : AutoScheduledPlanReviewValues
    {
        public string BuildingScheduleStartTxt { get; set; }
        public string BuildingScheduleEndTxt { get; set; }
        public string ElectricScheduleStartTxt { get; set; }
        public string ElectricScheduleEndTxt { get; set; }
        public string MechScheduleStartTxt { get; set; }
        public string MechScheduleEndTxt { get; set; }
        public string PlumbScheduleStartTxt { get; set; }
        public string PlumbScheduleEndTxt { get; set; }
        public string ZoneScheduleStartTxt { get; set; }
        public string ZoneScheduleEndTxt { get; set; }
        public string FireScheduleStartTxt { get; set; }
        public string FireScheduleEndTxt { get; set; }
        public string FoodScheduleStartTxt { get; set; }
        public string FoodScheduleEndTxt { get; set; }
        public string PoolScheduleStartTxt { get; set; }
        public string PoolScheduleEndTxt { get; set; }
        public string FacilityScheduleStartTxt { get; set; }
        public string FacilityScheduleEndTxt { get; set; }
        public string DayCareScheduleStartTxt { get; set; }
        public string DayCareScheduleEndTxt { get; set; }
        public string BackFlowScheduleStartTxt { get; set; }
        public string BackFlowScheduleEndTxt { get; set; }
    }
}