using AION.Manager.Models;
using System;
using System.Collections.Generic;

namespace AION.BL.Models
{
    public class FIFOSchedule : ModelBase
    {
        public int? FIFOScheduleId { get; set; }

        public int? ApptResponseStatusRefId { get; set; }

        public AppointmentResponseStatusEnum ApptResponseStatusEnum { get; set; }

        public DateTime? StartDt { get; set; }

        public DateTime? EndDt { get; set; }

        public DateTime? FifoDueDt { get; set; }

        public DateTime? EarliestDt { get; set; }

        public bool IsSameBuildingContractor { get; set; }

        public bool IsManualAssignment { get; set; }

        public List<AttendeeInfo> AssignedReviewers { get; set; }

        public List<AttendeeInfo> Attendees { get; set; }

        public int? ProjectId { get; set; }

        public int Cycle { get; set; }

        public DateTime? GateDt { get; set; }

        #region Start and End Dates for the Trades/Agencies

        public DateTime? BuildStartDate { get; set; }
        public DateTime? ElectStartDate { get; set; }
        public DateTime? MechaStartDate { get; set; }
        public DateTime? PlumbStartDate { get; set; }
        public DateTime? FireStartDate { get; set; }
        public DateTime? ZoneStartDate { get; set; }
        public DateTime? BackfStartDate { get; set; }
        public DateTime? PoolStartDate { get; set; }
        public DateTime? FoodStartDate { get; set; }
        public DateTime? FacilStartDate { get; set; }
        public DateTime? DaycStartDate { get; set; }

        public DateTime? BuildEndDate { get; set; }
        public DateTime? ElectEndDate { get; set; }
        public DateTime? MechaEndDate { get; set; }
        public DateTime? PlumbEndDate { get; set; }
        public DateTime? FireEndDate { get; set; }
        public DateTime? ZoneEndDate { get; set; }
        public DateTime? BackfEndDate { get; set; }
        public DateTime? PoolEndDate { get; set; }
        public DateTime? FoodEndDate { get; set; }
        public DateTime? FacilEndDate { get; set; }
        public DateTime? DaycEndDate { get; set; }

        #endregion Start and End Dates for the Trades/Agencies

        #region Hours and Assigned Reviewers for the Trades/Agencies
        public decimal BuildingHours { get; set; }
        public int BuildingAssignedReviewer { get; set; }

        public decimal BackflowHours { get; set; }
        public int BackflowAssignedReviewer { get; set; }

        public decimal DayCareHours { get; set; }
        public int DayCareAssignedReviewer { get; set; }

        public decimal ElectricHours { get; set; }
        public int ElectricAssignedReviewer { get; set; }

        public decimal FacilityHours { get; set; }
        public int FacilityAssignedReviewer { get; set; }

        public decimal FireHours { get; set; }
        public int FireAssignedReviewer { get; set; }

        public decimal FoodServiceHours { get; set; }
        public int FoodServiceAssignedReviewer { get; set; }

        public decimal MechHours { get; set; }
        public int MechAssignedReviewer { get; set; }

        public decimal PlumbHours { get; set; }
        public int PlumbAssignedReviewer { get; set; }

        public decimal PoolHours { get; set; }
        public int PoolAssignedReviewer { get; set; }

        public decimal ZoningHours { get; set; }
        public int ZoningAssignedReviewer { get; set; }
        #endregion

        public int? BuildPlanReviewScheduleId { get; set; }
        public int? ElectPlanReviewScheduleId { get; set; }
        public int? MechaPlanReviewScheduleId { get; set; }
        public int? PlumbPlanReviewScheduleId { get; set; }
        public int? FirePlanReviewScheduleId { get; set; }
        public int? ZonePlanReviewScheduleId { get; set; }
        public int? BackfPlanReviewScheduleId { get; set; }
        public int? PoolPlanReviewScheduleId { get; set; }
        public int? FoodPlanReviewScheduleId { get; set; }
        public int? FacilPlanReviewScheduleId { get; set; }
        public int? DaycPlanReviewScheduleId { get; set; }

        public DateTime? BuildPRSUpdateDate { get; set; }
        public DateTime? ElectPRSUpdateDate { get; set; }
        public DateTime? MechaPRSUpdateDate { get; set; }
        public DateTime? PlumbPRSUpdateDate { get; set; }
        public DateTime? FirePRSUpdateDate { get; set; }
        public DateTime? ZonePRSUpdateDate { get; set; }
        public DateTime? BackfPRSUpdateDate { get; set; }
        public DateTime? PoolPRSUpdateDate { get; set; }
        public DateTime? FoodPRSUpdateDate { get; set; }
        public DateTime? FacilPRSUpdateDate { get; set; }
        public DateTime? DaycPRSUpdateDate { get; set; }

        public bool BuildIsSameBuildingContractor { get; set; }
        public bool ElectIsSameBuildingContractor { get; set; }
        public bool MechaIsSameBuildingContractor { get; set; }
        public bool PlumbIsSameBuildingContractor { get; set; }
        public bool FireIsSameBuildingContractor { get; set; }
        public bool ZoneIsSameBuildingContractor { get; set; }
        public bool BackfIsSameBuildingContractor { get; set; }
        public bool PoolIsSameBuildingContractor { get; set; }
        public bool FoodIsSameBuildingContractor { get; set; }
        public bool FacilIsSameBuildingContractor { get; set; }
        public bool DaycIsSameBuildingContractor { get; set; }

        public bool BuildIsManualAssignment { get; set; }
        public bool ElectIsManualAssignment { get; set; }
        public bool MechaIsManualAssignment { get; set; }
        public bool PlumbIsManualAssignment { get; set; }
        public bool FireIsManualAssignment { get; set; }
        public bool ZoneIsManualAssignment { get; set; }
        public bool BackfIsManualAssignment { get; set; }
        public bool PoolIsManualAssignment { get; set; }
        public bool FoodIsManualAssignment { get; set; }
        public bool FacilIsManualAssignment { get; set; }
        public bool DaycIsManualAssignment { get; set; }
        /// <summary>
        /// LES-3809 - add project audit for auto schedule
        /// </summary>
        public bool AutoScheduled { get; set; }

    }
}