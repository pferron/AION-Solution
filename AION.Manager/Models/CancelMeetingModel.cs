namespace AION.Manager.Models
{
    public class CancelMeetingModel
    {
        public int AppointmentId { get; set; }
        public string Notes { get; set; }
        public int UserId { get; set; }
        public string ProjectId { get; set; }
        public string RecIdTxt { get; set; }
    }
}