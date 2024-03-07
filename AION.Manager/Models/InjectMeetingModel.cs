using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AION.Manager.Models
{
    public class InjectMeetingModel
    {
        public string Subject { get; set; }
        public string HtmlBody { get; set; }
        public List<string> AllAttendeesEmailList { get; set; }
        public string LocationDisplayText { get; set; }
        public string LocationEmail { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}