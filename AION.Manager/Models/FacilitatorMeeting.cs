using AION.Manager.Models;
using Meck.Shared.Accela;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AION.BL.Models
{
    public class FacilitatorMeeting
    {
        public string ProjectId { get; set; }
        public string MeetingType { get; set; }
        public bool IsCancelled { get; set; }
        public List<RequestedMeetingDateBE> RequestedMeetingDates { get; set; } = new List<RequestedMeetingDateBE>();
        public int? ExternalAttendeesCnt { get; set; }
    }

    public class MeetingAttendee
    {
        public string AccelaDepartmentDivisionRef { get; set; }
        public string Name { get; set; }
    }
}