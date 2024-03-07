using System.Collections.Generic;

namespace AION.Manager.Models.Outlook
{
    public class CalendarEvent
    {
        public string Id { get; set; }
        
        public string TransactionId { get; set; }
        
        public string Subject { get; set; }
        
        public Body Body { get; set; }

        public CalendarEventDateTimeZone Start { get; set; }

        public CalendarEventDateTimeZone End { get; set; }

        public bool IsAllDay { get; set; }

        public Location Location { get; set; }

        public string ShowAs { get; set; }

        public bool ResponseRequested { get; set; }
    }

    public class Body
    {
        public string ContentType { get; set; }
        public string Content { get; set; }
    }

    public class Location
    {
        public string DisplayName { get; set; }
    }

    public class EmailAddress
    {
        public string Address { get; set; }
        public string Name { get; set; }

    }

    public class CalendarEventDateTimeZone
    {
        public string TimeZone { get; set; }

        public string DateTime { get; set; }
    }
}