namespace AION.Web.Models.Scheduling
{
    public class ReviewerCapacityViewModel
    {
        public string ReviewerName { get; set; }
        public string MeetingName { get; set; }
        public string MeetingBeginDtTm { get; set; }
        public string MeetingEndDtTm { get; set; }
        public double MeetingDuration { get; set; }
        public string MeetingTypeName { get; set; }
        public string ApptId { get; set; }
        public int UserId { get; set; }
    }
}