using AION.BL.Adapters;
using AION.BL.Common;
using AION.BL.Models;
using AION.Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AION.BL
{ 
    public class Appointment : ModelBase
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? PrevStartDate { get; set; }
        public DateTime? PrevEndDate { get; set; }
        public List<ScheduleTime> RecurringDates { get; set; } = new List<ScheduleTime>();
        public bool? VirtualMeetingInd { get; set; }
        public int? MeetingRoomRefId { get; set; }
        public MeetingRoom MeetingRoom { get; set; }
        public int? MeetingTypeRefId { get; set; }
        public MeetingTypeEnum MeetingTypeEnum { get; set; }
        public int? ProjectID { get; set; }
        public List<AttendeeDetails> AttendeeDetails { get; set; } = new List<AttendeeDetails>();
        public int? ApptResponseStatusRefId { get; set; }
        public AppointmentResponseStatusEnum ApptResponseStatusEnum { get; set; }
        public int? ApptCancellationRefId { get; set; }
        public AppointmentCancellationEnum ApptCancellationEnum { get; set; }
        public ProjectScheduleRefEnum ProjectScheduleRefEnum { get; set; }
        public List<UserIdentity> Users { get; set; }
        public List<AttendeeInfo> Attendees { get; set; } = new List<AttendeeInfo>();
        public List<AttendeeInfo> NewAttendees { get; set; } = new List<AttendeeInfo>();
        public ProjectSchedule ProjectSchedule { get; set; }
        public HolidayConfig HolidayConfig { get; set; }
        public List<ScheduleTime> ScheduleTimes { get; set; } = new List<ScheduleTime>();
        public ProjectEstimation ProjectEstimation { get; set; }
        public bool IsSubmit { get; set; }
        public bool IsReschedule { get; set; }
        public bool IsCancellation { get; set; }
        public string InternalNotes { get; set; }
        public string UserId { get; set; }

        //specific to EMA and PMA
        public List<AttendeeInfo> AssignedReviewers { get; set; } = new List<AttendeeInfo>();
        public List<AttendeeInfo> PrimaryReviewers { get; set; } = new List<AttendeeInfo>();
        public List<AttendeeInfo> SecondaryReviewers { get; set; } = new List<AttendeeInfo>();
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
    }
}