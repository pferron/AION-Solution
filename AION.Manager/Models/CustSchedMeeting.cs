using AION.BL;
using System;
using System.Collections.Generic;

namespace AION.Manager.Models
{
    public class CustSchedMeeting
    {
        public int MeetingId { get; set; }

        public DateTime MeetingDate { get; set; }

        public string MeetingTime { get; set; }

        public DateTime? AgendaDue { get; set; }

        public DateTime? ResponseDue { get; set; }

        public int ApptResponseStatusId { get; set; }

        public MeetingTypeEnum MeetingTypeEnum { get; set; }

        public AppointmentResponseStatusEnum AppointmentResponseStatusEnum { get; set; }

        public AppointmentCancellationEnum? AppointmentCancellationEnum { get; set; }
        public string StatusLabel { get; set; }
        /// <summary>
        /// LES-4029
        /// </summary>
        public List<AttendeeInfo> Attendees { get; set; }

    }
}