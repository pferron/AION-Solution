using System;
using System.Runtime.Serialization;

namespace AION.Scheduler.Engine.BusinessEntities
{
    public class MessageTemplateAppointmentBE
    {
        [DataMember]
        public int ProjectId { get; set; }

        [DataMember]
        public string ProjectScheduleTypDesc { get; set; }

        [DataMember]
        public int? MeetingTypRefId { get; set; }

        [DataMember]
        public string RowType { get; set; }

        [DataMember]
        public DateTime? StartDt { get; set; }

        [DataMember]
        public int? ScheduledUserId { get; set; }

        [DataMember]
        public int? CycleNbr { get; set; }

        [DataMember]
        public int? MeetingRoomRefId { get; set; }

        [DataMember]
        public string PendingNote { get; set; }
    }
}
