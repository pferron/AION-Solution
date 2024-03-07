using AION.BL;
using AION.BL.Models;
using System;

namespace AION.Manager.Models
{
    public class PlanReviewSchedule : ModelBase
    {
        public int? PlanReviewScheduleId { get; set; }

        public int? ProjectCycleId { get; set; }

        public string ProjectScheduleTypDesc { get; set; }

        public bool? IsRescheduleInd { get; set; }

        public int? ApptResponseStatusRefId { get; set; }

        public int? ApptCancellationRefId { get; set; }

        public bool? VirtualMeetingInd { get; set; }

        public DateTime? Proposed1Dt { get; set; }

        public DateTime? Proposed2Dt { get; set; }

        public DateTime? Proposed3Dt { get; set; }

        public DateTime? CancelAfterDt { get; set; }

        public int? MeetingRoomRefId { get; set; }
    }
}