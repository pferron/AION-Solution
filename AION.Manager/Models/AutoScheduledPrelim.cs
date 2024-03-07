using System;

namespace AION.Manager.Models
{
    public class AutoScheduledPrelimValues
    {
        public int BuildingUserID { get; set; }
        public int ElectricUserID { get; set; }
        public int MechUserID { get; set; }
        public int PlumbUserID { get; set; }
        public int ZoneUserID { get; set; }
        public int FireUserID { get; set; }
        public int FoodServiceUserID { get; set; }
        public int PoolUserID { get; set; }
        public int FacilityUserID { get; set; }
        public int DayCareUserID { get; set; }
        public int BackFlowUserID { get; set; }

        public DateTime? ScheduleStart { get; set; }
        public DateTime? ScheduleEnd { get; set; }
    }

    public class AutoScheduledPrelimParams
    {
        public DateTime? SuggestedDate1 { get; set; }
        public DateTime? SuggestedDate2 { get; set; }
        public DateTime? SuggestedDate3 { get; set; }
        public string AccelaProjectIDRef { get; set; }
        public string RecIdTxt { get; set; }
    }
}