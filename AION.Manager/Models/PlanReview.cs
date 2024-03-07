using AION.BL;
using AION.BL.Models;
using System;
using System.Collections.Generic;

namespace AION.Manager.Models
{
    public class PlanReview : ModelBase
    {
        public ProjectCycle ProjectCycle { get; set; }
        public ProjectScheduleRefEnum ProjectScheduleRefEnum { get; set; }
        public int? PlanReviewId { get; set; }
        public int? PlanReviewScheduleId { get; set; }
        public int ProjectId { get; set; }
        public int ProjectCycleId { get; set; }
        public int ApptResponseStatusRefId { get; set; }
        public AppointmentResponseStatusEnum ApptResponseStatusEnum { get; set; }
        public bool IsManualAssignment { get; set; }
        public bool IsSameBuildingContractor { get; set; }

        public DateTime? ScheduleDate { get; set; }
        public DateTime? ResponseDate { get; set; }


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

        public bool? BuildFifo { get; set; } = false;
        public bool? ElectFifo { get; set; } = false;
        public bool? MechaFifo { get; set; } = false;
        public bool? PlumbFifo { get; set; } = false;
        public bool? FireFifo { get; set; } = false;
        public bool? ZoneFifo { get; set; } = false;
        public bool? BackfFifo { get; set; } = false;
        public bool? PoolFifo { get; set; } = false;
        public bool? FoodFifo { get; set; } = false;
        public bool? FacilFifo { get; set; } = false;
        public bool? DaycFifo { get; set; } = false;

        public bool? BuildPool { get; set; } = false;
        public bool? ElectPool { get; set; } = false;
        public bool? MechaPool { get; set; } = false;
        public bool? PlumbPool { get; set; } = false;
        public bool? FirePool { get; set; } = false;
        public bool? ZonePool { get; set; } = false;
        public bool? BackfPool { get; set; } = false;
        public bool? PoolPool { get; set; } = false;
        public bool? FoodPool { get; set; } = false;
        public bool? FacilPool { get; set; } = false;
        public bool? DaycPool { get; set; } = false;

        public decimal HoursDefault { get; set; }
        public decimal HoursBuilding { get; set; }
        public decimal HoursElectic { get; set; }
        public decimal HoursMech { get; set; }
        public decimal HoursPlumb { get; set; }
        public decimal HoursZoning { get; set; }
        public decimal HoursFire { get; set; }
        public decimal HoursBackFlow { get; set; }
        public decimal HoursFood { get; set; }
        public decimal HoursPool { get; set; }
        public decimal HoursLodge { get; set; }
        public decimal HoursDayCare { get; set; }

 
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

        public int PlanReviewProjectDetailsId { get; set; }

        public int Cycle { get; set; }
        public bool IsCurrentCycle { get; set; }
        public bool IsFutureCycle { get; set; }
        public bool HasFutureCycle { get; set; }

        public decimal? ReReviewBuilding { get; set; }
        public decimal? ReReviewElectric { get; set; }
        public decimal? ReReviewMech { get; set; }
        public decimal? ReReviewPlumb { get; set; }
        public decimal? ReReviewZoning { get; set; }
        public decimal? ReReviewFire { get; set; }
        public decimal? ReReviewBackFlow { get; set; }
        public decimal? ReReviewFood { get; set; }
        public decimal? ReReviewPool { get; set; }
        public decimal? ReReviewLodge { get; set; }
        public decimal? ReReviewDayCare { get; set; }
        public decimal? ProposedBuilding { get; set; }
        public decimal? ProposedElectric { get; set; }
        public decimal? ProposedMech { get; set; }
        public decimal? ProposedPlumb { get; set; }
        public decimal? ProposedZoning { get; set; }
        public decimal? ProposedFire { get; set; }
        public decimal? ProposedBackFlow { get; set; }
        public decimal? ProposedFood { get; set; }
        public decimal? ProposedPool { get; set; }
        public decimal? ProposedLodge { get; set; }
        public decimal? ProposedDayCare { get; set; }
        public DateTime? ScheduleAfterDate { get; set; }
        public bool RequestExpressNextCycle { get; set; }
        public DateTime? ProdDate { get; set; }
        public DateTime? GateDate { get; set; }
        public DateTime? FIFODueDate { get; set; }
        public DateTime? EarliestDate { get; set; }
        public DateTime? MaxDate { get; set; }
        /// <summary>
        /// Indicates if this was a Submit vs Save
        /// Triggers appt emails send
        /// </summary>
        public bool IsSubmit { get; set; }

        /// <summary>
        /// Indicates if this is a resecheduled appt
        /// Triggers cancellation emails for previous appt and new email for reschedule
        /// </summary>
        public bool IsReschedule { get; set; }

        /// <summary>
        /// Attendees list for the View
        /// </summary>
        public List<UserIdentity> Attendees { get; set; }

        /// <summary>
        /// Attendees
        /// List for saving PMA
        /// </summary>
        public List<AttendeeInfo> NewAttendees { get; set; }

        /// <summary>
        /// Assigned Reviewers
        /// Scheduled reviewers for this PMA
        /// </summary>
        public List<AttendeeInfo> AssignedReviewers { get; set; }

        /// <summary>
        /// Notes to save for the project
        /// </summary>
        public string InternalNotes { get; set; }
        public string SchedulingNotes { get; set; }
        public string MandatorySchedulingNotes { get; set; }
        public string AddSchedulingNotes { get; set; }
        public string SchedulingStandardNotes { get; set; }

        public List<AttendeeInfo> PrimaryReviewers { get; set; }
        public List<AttendeeInfo> SecondaryReviewers { get; set; }
        public string AssignedFacilitator { get; set; }
        public List<string> ExcludedPlanReviewersBuild { get; set; }
        public List<string> ExcludedPlanReviewersElectric { get; set; }
        public List<string> ExcludedPlanReviewersMech { get; set; }
        public List<string> ExcludedPlanReviewersPlumb { get; set; }
        public List<string> ExcludedPlanReviewersZone { get; set; }
        public List<string> ExcludedPlanReviewersFire { get; set; }
        public List<string> ExcludedPlanReviewersBackFlow { get; set; }
        public List<string> ExcludedPlanReviewersFood { get; set; }
        public List<string> ExcludedPlanReviewersPool { get; set; }
        public List<string> ExcludedPlanReviewersLodge { get; set; }
        public List<string> ExcludedPlanReviewersDayCare { get; set; }

        /// <summary>
        /// This links to the Auto Schedule staging table
        /// If this is > 0 then the user selected Auto Schedule
        /// </summary>
        public int? PlanReviewAutoScheduleId { get; set; }

        public string AccelaProjectRefId { get; set; }

        //jcl 8/10/21 LES-3463 - do not change response status if this was Activate NA Review
        /// <summary>
        /// Indicates to PlanReviewAdapter FinalizePlanReview whether the Project status needs to be updated
        /// </summary>
        public bool UpdateProjectStatus { get; set; }

        //jcl 8/10/21 LES-3463 - do not change response status if this was Activate NA Review
        /// <summary>
        /// Indicates if the email required should be sent
        /// </summary>
        public bool SendEmail { get; set; }
        //jcl 8/10/21 LES-3463 - do not change response status if this was Activate NA Review
        /// <summary>
        /// Indicates if the approval will be reset for the plan review project details
        /// </summary>
        public bool ResetApproval { get; set; }

        /// <summary>
        /// UI - used on customer page to render correct cancellation message
        /// Built by the Message Template Engine
        /// </summary>
        public string CancellationMessage { get; set; }
        /// <summary>
        /// LES-3809 - add project audit for auto schedule
        /// </summary>
        public bool AutoScheduled { get; set; }

        //assigned reviewers
        public int BuildAssignedReviewerId { get; set; }
        public int ElectricAssignedReviewerId { get; set; }
        public int MechAssignedReviewerId { get; set; }
        public int PlumbAssignedReviewerId { get; set; }
        public int ZoningAssigedReviewerId { get; set; }
        public int FireAssignedReviewerId { get; set; }
        public int BackFlowAssignedReviewerId { get; set; }
        public int FoodAssignedReviewerId { get; set; }
        public int PoolAssignedReviewerId { get; set; }
        public int FacilityAssignedReviewerId { get; set; }
        public int DayCareAssignedReviewerId { get; set; }

        //LES-4028 add assigned reviewers names
        public string BuildAssignedReviewerName { get; set; }
        public string ElectAssignedReviewerName { get; set; }
        public string MechaAssignedReviewerName { get; set; }
        public string PlumbAssignedReviewerName { get; set; }
        public string FireAssignedReviewerName { get; set; }
        public string ZoneAssignedReviewerName { get; set; }
        public string BackfAssignedReviewerName { get; set; }
        public string PoolAssignedReviewerName { get; set; }
        public string FoodAssignedReviewerName { get; set; }
        public string FacilAssignedReviewerName { get; set; }
        public string DaycAssignedReviewerName { get; set; }

        private bool _allPool;
        public bool AllPool
        {
            get { return CalculateAllPool(); }
            set { _allPool = value; }
        }

        private bool _allHoursOneOrLess;
        public bool AllHoursOneOrLess
        {
            get { return CalculateAllHoursOneOrLess(); }
            set { _allHoursOneOrLess = value; }
        }


        #region EXPRESS
        public int? MeetingRoomRefId { get; set; }
        public MeetingRoom MeetingRoom { get; set; }
        public DateTime? ProposedDate1 { get; set; }
        public DateTime? ProposedDate2 { get; set; }
        public DateTime? ProposedDate3 { get; set; }
        public DateTime? CancelAfterDt { get; set; }
        public bool? VirtualMeetingInd { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        #endregion

        #region Private
        private bool CalculateAllPool()
        {
            if ((BuildAssignedReviewerId < 0 || (BuildAssignedReviewerId >= 0 && BuildPool != null && BuildPool == true))
                && (ElectricAssignedReviewerId < 0 || (ElectricAssignedReviewerId >= 0 && ElectPool != null && ElectPool == true))
                && (MechAssignedReviewerId < 0 || (MechAssignedReviewerId >= 0 && MechaPool != null && MechaPool == true))
                && (PlumbAssignedReviewerId < 0 || (PlumbAssignedReviewerId >= 0 && PlumbPool != null && PlumbPool == true))
                && (FireAssignedReviewerId < 0 || (FireAssignedReviewerId >= 0 && FirePool != null && FirePool == true))
                && (ZoningAssigedReviewerId < 0 || (ZoningAssigedReviewerId >= 0 && ZonePool != null && ZonePool == true))
                && (BackFlowAssignedReviewerId < 0 || (BackFlowAssignedReviewerId >= 0 && BackfPool != null && BackfPool == true))
                && (PoolAssignedReviewerId < 0 || (PoolAssignedReviewerId >= 0 && PoolPool != null && PoolPool == true))
                && (FoodAssignedReviewerId < 0 || (FoodAssignedReviewerId >= 0 && FoodPool != null && FoodPool == true))
                && (FacilityAssignedReviewerId < 0 || (FacilityAssignedReviewerId >= 0 && FacilPool != null && FacilPool == true))
                && (DayCareAssignedReviewerId < 0 || (DayCareAssignedReviewerId >= 0 && DaycPool != null && DaycPool == true)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CalculateAllHoursOneOrLess()
        {
            if (HoursBuilding <= 1
                 && HoursElectic <= 1
                 && HoursMech <= 1
                 && HoursPlumb <= 1
                 && HoursZoning <= 1
                 && HoursFire <= 1
                 && HoursBackFlow <= 1
                 && HoursFood <= 1
                 && HoursPool <= 1
                 && HoursLodge <= 1
                 && HoursDayCare <= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}