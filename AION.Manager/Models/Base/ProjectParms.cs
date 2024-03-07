namespace AION.BL.Models
{
    /// <summary>
    /// Business object used by several processes to simplify method signatures
    /// </summary>
    public class ProjectParms
    {
        public string ProjectId { get; set; }
        public bool PerformAutoEstimation { get; set; }
        public bool IsPreliminary { get; set; }
        public bool IsFutureCycle { get; set; }
        public bool IsAdjustHours { get; set; }
        public string StatusMessage { get; set; }
        public string LoggedInUserEmail { get; set; }
        public bool IsReschedule { get; set; }
        public bool BuildingIsPool { get; set; }
        public bool ElectricIsPool { get; set; }
        public bool MechIsPool { get; set; }
        public bool PlumbIsPool { get; set; }
        public bool ZoneIsPool { get; set; }
        public bool FireIsPool { get; set; }
        public bool FoodServiceIsPool { get; set; }
        public bool PoolIsPool { get; set; }
        public bool FacilityIsPool { get; set; }
        public bool DayCareIsPool { get; set; }
        public bool BackFlowIsPool { get; set; }

        public bool IsControl { get; set; }
        public bool IsAgencyApproval { get; set; }
        public bool IsPlanReview { get; set; }
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
        public string MeetingTypeDesc { get; set; }
        public string AdditionalAttendees { get; set; }
        public string DurationHours { get; set; }
        public string DurationMinutes { get; set; }

        public string RecIdTxt { get; set; }
        public int CycleNbr { get; set; }
        public string ProjectNumber { get; set; }
        public NoteTypeEnum? NoteTypeEnum { get; set; }
    }
}
