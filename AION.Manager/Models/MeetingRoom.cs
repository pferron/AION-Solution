namespace AION.BL.Models
{
    public class MeetingRoom : ModelBase
    {
        public int? MeetingRoomRefID { get; set; }
        public string MeetingRoomName { get; set; }
        public bool? IsActive { get; set; }
        public string MeetingRoomEmail { get; set; }
        public string UserPrincipalName { get; set; }
        public string CalendarId { get; set; }
    }
}
