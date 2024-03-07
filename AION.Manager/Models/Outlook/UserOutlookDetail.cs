using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AION.Manager.Models
{
    public class UserOutlookDetail
    {
        public string UserPrincipalName { get; set; }
        public string CalendarId { get; set; }
        public bool IsProcessed { get; set; }

        //public string EventId { get; set; }  // MS Graph Event
        //public int? UserEventId { get; set; } // User Event table id
    }
}