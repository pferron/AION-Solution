#region Using

using System;
using System.Runtime.Serialization;

#endregion

namespace AION.Scheduler.Engine.BusinessEntities
{
    public class ScheduleCapacitySearchResultBE
    {
        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public DateTime? ScheduleDate { get; set; }

        [DataMember]
        public decimal TotalPlanReviewHours { get; set; }

        [DataMember]
        public int? ApptId { get; set; }

        [DataMember]
        public string MeetingType { get; set; }

        [DataMember]
        public string MeetingName { get; set; }

        [DataMember]
        public DateTime? StartDttm { get; set; }

        [DataMember]
        public DateTime? EndDttm { get; set; }
    }
}
