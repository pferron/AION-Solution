namespace AION.BL.Adapters
{
    public struct AttendeeDetails
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public bool IsRequired { get; set; } 
        public int UserId { get; set; }
        public string UserPrincipalName { get; set; }
        public string CalendarId { get; set; }
    }
}