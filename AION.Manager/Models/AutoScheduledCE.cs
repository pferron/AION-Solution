using AION.BL.Models;
using System;
using System.Collections.Generic;

namespace AION.Manager.Models
{
    public class AutoScheduledValues
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
    }

    public class FIFOEngineParams
    {
        public ProjectEstimation CurrentProject { get; set; }
        public PlanReview PlanReview { get; set; }
        public string AccelaWorkflowTaskStatus { get; set; }
        public int Cycle { get; set; }
        public string AccelaProjectIDRef { get; set; }
        public string RecIdTxt { get; set; }
    }
    public class AutoScheduleReportParams : AutoScheduledExpressParams
    {
        public decimal ReviewHours { get; set; }
    }
    public class AutoScheduledParams
    {
        /// <summary>
        /// Send the project for testing
        /// Send the project for scheduling lead time report
        /// </summary>
        public ProjectEstimation CurrentProject { get; set; }

        public string AccelaProjectIDRef { get; set; }
        public int ProjectID { get; set; }
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
        public int Cycle { get; set; }
        public string RecIdTxt { get; set; }
    }

    public class AutoScheduledPlanReviewValues : AutoScheduledValues
    {
        public string ErrorMessage { get; set; }
        public DateTime? BuildingScheduleStart { get; set; }
        public DateTime? BuildingScheduleEnd { get; set; }
        public DateTime? ElectricScheduleStart { get; set; }
        public DateTime? ElectricScheduleEnd { get; set; }
        public DateTime? MechScheduleStart { get; set; }
        public DateTime? MechScheduleEnd { get; set; }
        public DateTime? PlumbScheduleStart { get; set; }
        public DateTime? PlumbScheduleEnd { get; set; }
        public DateTime? ZoneScheduleStart { get; set; }
        public DateTime? ZoneScheduleEnd { get; set; }
        public DateTime? FireScheduleStart { get; set; }
        public DateTime? FireScheduleEnd { get; set; }
        public DateTime? FoodScheduleStart { get; set; }
        public DateTime? FoodScheduleEnd { get; set; }
        public DateTime? PoolScheduleStart { get; set; }
        public DateTime? PoolScheduleEnd { get; set; }
        public DateTime? FacilityScheduleStart { get; set; }
        public DateTime? FacilityScheduleEnd { get; set; }
        public DateTime? DayCareScheduleStart { get; set; }
        public DateTime? DayCareScheduleEnd { get; set; }
        public DateTime? BackFlowScheduleStart { get; set; }
        public DateTime? BackFlowScheduleEnd { get; set; }

        public decimal BuildingHours { get; set; }
        public decimal ElectricHours { get; set; }
        public decimal MechHours { get; set; }
        public decimal PlumbHours { get; set; }
        public decimal ZoneHours { get; set; }
        public decimal FireHours { get; set; }
        public decimal FoodServiceHours { get; set; }
        public decimal PoolHours { get; set; }
        public decimal FacilityHours { get; set; }
        public decimal DayCareHours { get; set; }
        public decimal BackFlowHours { get; set; }

        /// <summary>
        /// LES-3407 jcl - if "Town" is selected for Huntersville or Mint Hill, mark zone as pool
        /// Mark Zone as Pool
        /// </summary>
        public bool ZoneIsPool { get; set; }
    }

    public class AutoScheduledPlanReviewParams : AutoScheduledParams
    {
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

        public bool BuildingIsFIFO { get; set; }
        public bool ElectricIsFIFO { get; set; }
        public bool MechIsFIFO { get; set; }
        public bool PlumbIsFIFO { get; set; }
        public bool ZoneIsFIFO { get; set; }
        public bool FireIsFIFO { get; set; }
        public bool FoodServiceIsFIFO { get; set; }
        public bool PoolIsFIFO { get; set; }
        public bool FacilityIsFIFO { get; set; }
        public bool DayCareIsFIFO { get; set; }
        public bool BackFlowIsFIFO { get; set; }
        public DateTime ScheduleAfterDate { get; set; }
        public DateTime? PlansReadyOnDate { get; set; }
        public bool IsFutureCycle { get; set; }
        public bool IsCycleComparison { get; set; }
        public bool IsAdjustHours { get; set; }

        // jcl LES-186 Used to capture Future Cycle hours and Activate NA review hours
        /// <summary>
        /// Used to capture Future Cycle hours and Activate NA review hours
        /// </summary>
        public decimal UpdatedBuildingHours { get; set; }
        /// <summary>
        /// Used to capture Future Cycle hours and Activate NA review hours
        /// </summary>
        public decimal UpdatedElectricHours { get; set; }
        /// <summary>
        /// Used to capture Future Cycle hours and Activate NA review hours
        /// </summary>
        public decimal UpdatedMechHours { get; set; }
        /// <summary>
        /// Used to capture Future Cycle hours and Activate NA review hours
        /// </summary>
        public decimal UpdatedPlumbHours { get; set; }
        /// <summary>
        /// Used to capture Future Cycle hours and Activate NA review hours
        /// </summary>
        public decimal UpdatedDayCareHours { get; set; }
        /// <summary>
        /// Used to capture Future Cycle hours and Activate NA review hours
        /// </summary>
        public decimal UpdatedFoodHours { get; set; }
        /// <summary>
        /// Used to capture Future Cycle hours and Activate NA review hours
        /// </summary>
        public decimal UpdatedPoolHours { get; set; }
        /// <summary>
        /// Used to capture Future Cycle hours and Activate NA review hours
        /// </summary>
        public decimal UpdatedLodgeHours { get; set; }
        /// <summary>
        /// Used to capture Future Cycle hours and Activate NA review hours
        /// </summary>
        public decimal UpdatedBackflowHours { get; set; }
        /// <summary>
        /// Used to capture Future Cycle hours and Activate NA review hours
        /// </summary>
        public decimal UpdatedZoneHours { get; set; }
        /// <summary>
        /// Used to capture Future Cycle hours and Activate NA review hours
        /// </summary>
        public decimal UpdatedFireHours { get; set; }
        //

        public bool isSelfSchedule { get; set; }
        public DateTime selfScheduleDate { get; set; }

        /// <summary>
        /// Used to indicate to autoschedule process that this includes activated NA estimations
        /// </summary>
        public bool isActivateNAReview { get; set; }

    }

    public class SchedulePlanReviewCapacityParams : AutoScheduledPlanReviewParams
    {
        public decimal BuildingHours { get; set; }
        public decimal ElectricHours { get; set; }
        public decimal MechHours { get; set; }
        public decimal PlumbHours { get; set; }
        public decimal ZoneHours { get; set; }
        public decimal FireHours { get; set; }
        public decimal FoodServiceHours { get; set; }
        public decimal PoolHours { get; set; }
        public decimal FacilityHours { get; set; }
        public decimal DayCareHours { get; set; }
        public decimal BackFlowHours { get; set; }
        public DateTime? BuildingScheduleStart { get; set; }
        public DateTime? BuildingScheduleEnd { get; set; }
        public DateTime? ElectricScheduleStart { get; set; }
        public DateTime? ElectricScheduleEnd { get; set; }
        public DateTime? MechScheduleStart { get; set; }
        public DateTime? MechScheduleEnd { get; set; }
        public DateTime? PlumbScheduleStart { get; set; }
        public DateTime? PlumbScheduleEnd { get; set; }
        public DateTime? ZoneScheduleStart { get; set; }
        public DateTime? ZoneScheduleEnd { get; set; }
        public DateTime? FireScheduleStart { get; set; }
        public DateTime? FireScheduleEnd { get; set; }
        public DateTime? FoodScheduleStart { get; set; }
        public DateTime? FoodScheduleEnd { get; set; }
        public DateTime? PoolScheduleStart { get; set; }
        public DateTime? PoolScheduleEnd { get; set; }
        public DateTime? FacilityScheduleStart { get; set; }
        public DateTime? FacilityScheduleEnd { get; set; }
        public DateTime? DayCareScheduleStart { get; set; }
        public DateTime? DayCareScheduleEnd { get; set; }
        public DateTime? BackFlowScheduleStart { get; set; }
        public DateTime? BackFlowScheduleEnd { get; set; }
    }

    public class AutoScheduledExpressParams : AutoScheduledParams
    {
        public DateTime? ManualStartDateTime { get; set; }
        public DateTime? ManualEndDateTime { get; set; }

        public int MeetingRoomId { get; set; }
    }

    public class AutoScheduledExpressValues : AutoScheduledValues
    {
        public int MeetingRoomId { get; set; }
        public DateTime SelectedStartDateTime { get; set; }
        public DateTime SelectedEndDateTime { get; set; }
        public bool IsReadyForScheduling { get; set; }
        public string ErrorMessage { get; set; }
    }

    /// <summary>
    /// Used from api/Scheduling/GetAutoScheduledDataExpress
    /// Returns string values for reviewers names and meeting room name
    /// </summary>
    public class AutoScheduledExpressUIValues : AutoScheduledExpressValues
    {
        public string BuildingUserName { get; set; }
        public string ElectricUserName { get; set; }
        public string MechUserName { get; set; }
        public string PlumbUserName { get; set; }
        public string ZoneUserName { get; set; }
        public string FireUserName { get; set; }
        public string FoodServiceUserName { get; set; }
        public string PoolUserName { get; set; }
        public string FacilityUserName { get; set; }
        public string DayCareUserName { get; set; }
        public string BackFlowUserName { get; set; }
        public string MeetingRoomName { get; set; }
    }

    public class AutoScheduledFacilitatorMeetingParams : AutoScheduledParams
    {
        public string DurationHours { get; set; }
        public string DurationMinutes { get; set; }
        public DateTime? SuggestedDate1 { get; set; } = null;
        public DateTime? SuggestedDate2 { get; set; } = null;
        public DateTime? SuggestedDate3 { get; set; } = null;
        public int MeetingRoomId { get; set; }
        public string AdditionalAttendees { get; set; }
    }

    public class AutoScheduledFacilitatorMeetingValues : AutoScheduledValues
    {
        public AutoScheduledFacilitatorMeetingValues()
        {
            AdditionalAttendeeIds = new List<int>();
        }
        public int MeetingRoomId { get; set; }
        public DateTime SelectedStartDateTime { get; set; }
        public DateTime SelectedEndDateTime { get; set; }
        public List<int> AdditionalAttendeeIds { get; set; }
        public string DurationHours { get; set; }
        public string DurationMinutes { get; set; }
    }

    public class AutoScheduledFacilitatorMeetingUIValues : AutoScheduledFacilitatorMeetingValues
    {
        public string BuildingUserName { get; set; }
        public string ElectricUserName { get; set; }
        public string MechUserName { get; set; }
        public string PlumbUserName { get; set; }
        public string ZoneUserName { get; set; }
        public string FireUserName { get; set; }
        public string FoodServiceUserName { get; set; }
        public string PoolUserName { get; set; }
        public string FacilityUserName { get; set; }
        public string DayCareUserName { get; set; }
        public string BackFlowUserName { get; set; }
        public string MeetingRoomName { get; set; }
    }

    public class AdditionalAttendee
    {
        public int AttendeeId { get; set; }

    }

    public class AutoScheduledFIFOParams
    {
        public string AccelaProjectIDRef { get; set; }
    }

    public class AutoScheduledFIFOValues : AutoScheduledPlanReviewValues
    {
        public bool IsSameBuildingContractor { get; set; }
        public bool IsReadyForScheduling { get; set; }
    }

}