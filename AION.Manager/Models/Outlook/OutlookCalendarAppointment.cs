using AION.Manager.Models.Outlook;
using System;
using System.Collections.Generic;

namespace AION.Manager.Models
{
    public class OutlookCalendarAppointment
    {
        public CalendarEvent Event { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<UserOutlookDetail> UserOutlookDetails { get; set; }
    }
}