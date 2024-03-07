using AION.BL;
using AION.BL.Models;
using AION.Web.BusinessEntities;
using System;
using System.Collections.Generic;

namespace AION.Web.Models
{
    public class ScheduleSaveViewModel : ViewModelBase
    {
        public ScheduleSaveViewModel()
        {
            LoggedInUser = new UserIdentity();
            PermissionMapping = new PermissionMapping();
            ExcludedPlanReviewersBuild = new List<string>();
            ExcludedPlanReviewersElectric = new List<string>();
            ExcludedPlanReviewersMech = new List<string>();
            ExcludedPlanReviewersPlumb = new List<string>();
            ExcludedPlanReviewersZone = new List<string>();
            ExcludedPlanReviewersFire = new List<string>();
            ExcludedPlanReviewersBackFlow = new List<string>();
            ExcludedPlanReviewersFood = new List<string>();
            ExcludedPlanReviewersPool = new List<string>();
            ExcludedPlanReviewersLodge = new List<string>();
            ExcludedPlanReviewersDayCare = new List<string>();
        }

        private ProjectEstimation _project;
        public bool IsAllNAChecked { get; set; }
        public string SchedulingNotes { get; set; }
        public string MandatorySchedulingNotes { get; set; }
        public string AddSchedulingNotes { get; set; }
        public string SchedulingStandardNotes { get; set; }

        public int? FifoScheduleId { get; set; }
        public DateTime? FifoDueDt { get; set; }

        public int? PlanReviewScheduleId { get; set; }
        public AppointmentResponseStatusEnum ApptResponseStatusEnum { get; set; }

        public ProjectEstimation Project
        {
            get { return _project; }
            set
            {
                _project = value;
            }
        }

        public bool HrsNABuilding { get; set; }

        public bool HrsNAElectric { get; set; }

        public bool HrsNAMech { get; set; }

        public bool HrsNAPlumbing { get; set; }

        public bool HrsNAZone { get; set; }

        public bool HrsNAFire { get; set; }

        public bool HrsNABackFlow { get; set; }

        public bool HrsNAFood { get; set; }

        public bool HrsNAPool { get; set; }

        public bool HrsNAFacility { get; set; }

        public bool HrsNADayCare { get; set; }

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

        public string AssignedFacilitator { get; set; }
        public string PrimaryReviewerBuilding { get; set; }
        public string SecondaryReviewerBuilding { get; set; }
        public string PrimaryReviewerElectrical { get; set; }
        public string SecondaryReviewerelectrical { get; set; }
        public string PrimaryReviewerMechanical { get; set; }
        public string SecondaryReviewerMechanical { get; set; }
        public string PrimaryReviewerPlumbing { get; set; }
        public string SecondaryReviewerPlumbing { get; set; }
        public string PrimaryReviewerZone { get; set; }
        public string SecondaryReviewerZone { get; set; }
        public string PrimaryReviewerFire { get; set; }
        public string PrimaryReviewerBackFlow { get; set; }
        public string SecondaryReviewerFire { get; set; }
        public string SecondaryReviewerBackFlow { get; set; }
        public string PrimaryReviewerFood { get; set; }
        public string SecondaryReviewerFood { get; set; }
        public string PrimaryReviewerPool { get; set; }
        public string SecondaryReviewerPool { get; set; }
        public string PrimaryReviewerFacilities { get; set; }
        public string SecondaryReviewerFacilities { get; set; }
        public string PrimaryReviewerDayCare { get; set; }
        public string SecondaryReviewerDayCare { get; set; }
        public string StatusMessage { get; set; }
        public int Cycle { get; set; }
        public int ProjectCycleId { get; set; }
        public bool IsNewCycle { get; set; }
        public bool IsFutureCycle { get; set; }
        public bool IsCycleComparison { get; set; }
        public bool IsAdjustHours { get; set; }
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

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime ScheduleDate { get; set; }

        public MeetingRoom SelectedMeetingRoom { get; set; }
        public string SelectedMeetingRoomName { get; set; }

        public int MeetingRoomRefIDSelected { get; set; }

        public string ScheduledReviewerBuilding { get; set; }
        public string ScheduledReviewerElectrical { get; set; }
        public string ScheduledReviewerPlumbing { get; set; }
        public string ScheduledReviewerMechanical { get; set; }
        public string ScheduledReviewerFire { get; set; }
        public string ScheduledReviewerZone { get; set; }
        public string ScheduledReviewerBackFlow { get; set; }
        public string ScheduledReviewerFood { get; set; }
        public string ScheduledReviewerPool { get; set; }
        public string ScheduledReviewerFacilities { get; set; }
        public string ScheduledReviewerDayCare { get; set; }
        public string InternalNotes { get; set; }
        public DateTime ProposedDate1 { get; set; }
        public DateTime ProposedDate2 { get; set; }
        public DateTime ProposedDate3 { get; set; }
        public int TotalCustomerAttendees { get; set; }
        public DateTime? PlansReadyOnDate { get; set; }
        public DateTime GateDate { get; set; }
        public string TeamScore { get; set; }
        /// <summary>
        /// csv of useridentity.id, used in Submit method
        /// </summary>
        public string AttendeeIds { get; set; }
        /// <summary>
        /// this changes depending on the user Save or Submit
        /// </summary>
        public bool IsSubmit { get; set; }
        /// <summary>
        /// hidden field that holds the id for this appointment
        /// </summary>
        public int? PreliminaryMeetingApptID { get; set; }
        /// <summary>
        /// hidden field that holds the id for this appointment
        /// </summary>
        public int? ExpressMeetingApptID { get; set; }

        public int? FacilitatorMeetingApptID { get; set; }
        /// <summary>
        /// Current update date for updating scheduling item
        /// </summary>
        public DateTime PMAUpdateDate { get; set; }

        /// <summary>
        /// update date for the current appt
        /// used in saving stored procedure
        /// </summary>
        public DateTime UpdatedDate { get; set; }

        public bool IsRescheduleOverwrite { get; set; }

        public bool IsVirtualMeeting { get; set; } = false;

        public string MeetingTypeDesc { get; set; }
        public int MeetingTypeRefId { get; set; }

        #region Start and End Dates for the Trades/Agencies

        /************** Start and End Dates for the Trades/Agencies **********/

        public DateTime BuildStartDate { get; set; }
        public DateTime ElectStartDate { get; set; }
        public DateTime MechaStartDate { get; set; }
        public DateTime PlumbStartDate { get; set; }
        public DateTime FireStartDate { get; set; }
        public DateTime ZoneStartDate { get; set; }
        public DateTime BackfStartDate { get; set; }
        public DateTime PoolStartDate { get; set; }
        public DateTime FoodStartDate { get; set; }
        public DateTime FacilStartDate { get; set; }
        public DateTime DaycStartDate { get; set; }

        public DateTime BuildEndDate { get; set; }
        public DateTime ElectEndDate { get; set; }
        public DateTime MechaEndDate { get; set; }
        public DateTime PlumbEndDate { get; set; }
        public DateTime FireEndDate { get; set; }
        public DateTime ZoneEndDate { get; set; }
        public DateTime BackfEndDate { get; set; }
        public DateTime PoolEndDate { get; set; }
        public DateTime FoodEndDate { get; set; }
        public DateTime FacilEndDate { get; set; }
        public DateTime DaycEndDate { get; set; }

        #endregion Start and End Dates for the Trades/Agencies

        #region Pool/Fifo for Trades/Agencies
        public bool? BuildFifo { get; set; }
        public bool? ElectFifo { get; set; }
        public bool? MechaFifo { get; set; }
        public bool? PlumbFifo { get; set; }
        public bool? FireFifo { get; set; }
        public bool? ZoneFifo { get; set; }
        public bool? BackfFifo { get; set; }
        public bool? PoolFifo { get; set; }
        public bool? FoodFifo { get; set; }
        public bool? FacilFifo { get; set; }
        public bool? DaycFifo { get; set; }

        public bool? BuildPool { get; set; }
        public bool? ElectPool { get; set; }
        public bool? MechaPool { get; set; }
        public bool? PlumbPool { get; set; }
        public bool? FirePool { get; set; }
        public bool? ZonePool { get; set; }
        public bool? BackfPool { get; set; }
        public bool? PoolPool { get; set; }
        public bool? FoodPool { get; set; }
        public bool? FacilPool { get; set; }
        public bool? DaycPool { get; set; }
        #endregion Pool/Fifo for Trades/Agencies

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

        //jcl save these on the front end so when we do updates, we have the correct enum
        public DepartmentNameEnums FireAgency { get; set; }
        public DepartmentNameEnums ZoneAgency { get; set; }

        public bool IsActivateNAReview { get; set; }

        /// <summary>
        /// LES-3554 jcl 9/27/2021
        /// Retain the original assigned facilitator id to compare
        /// If this is different than the one saved in the dropdown,
        /// call the Update FAcilitator method
        /// </summary>
        public int PreviousAssignedFacilitator { get; set; }

        /// <summary>
        /// LES-3809 - add project audit for auto schedule
        /// </summary>
        public bool AutoScheduled { get; set; }
    }
}