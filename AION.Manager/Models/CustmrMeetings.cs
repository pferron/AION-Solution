using AION.BL;
using System;

namespace AION.Manager.Models
{

    public class CustmrMeetings
    {
        public DateTime MeetingDate { get; set; }

        public DateTime MeetingTime { get; set; }

        public MeetingTypeEnum MeetingType { get; set; }

        public AppointmentResponseStatusEnum MeetingStatus { get; set; }

        public PropertyTypeEnums ProjectType { get; set; }

        public string ProjectExternalRefID { get; set; }

        public int ProjectID { get; set; }

        public string ProjectName { get; set; }

        public ProjectStatusEnum ProjectStatus { get; set; }

        public DateTime? AppendixAgendaDue { get; set; }

        public DateTime MinutesDue { get; set; }
        public string RecIdTxt { get; set; }

    }
}