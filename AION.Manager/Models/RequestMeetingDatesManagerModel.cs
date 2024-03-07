using System;

namespace AION.Manager.Models
{
    public class RequestMeetingDatesManagerModel
    {
        public int MeetingApptId { get; set; }
        public string MeetingType { get; set; }
        public DateTime RequestDate1 { get; set; }
        public DateTime RequestDate2 { get; set; }
        public DateTime RequestDate3 { get; set; }
        public string ProjectId { get; set; }
        public int LoggedInUserId { get; set; }
    }
}