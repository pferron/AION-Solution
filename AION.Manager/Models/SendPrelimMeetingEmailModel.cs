using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AION.Manager.Models
{
    public class SendPrelimMeetingEmailModel
    {
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectAddress { get; set; }
        public DateTime MeetingDate { get; set; }
        public string MeetingRoom { get; set; }
        public string Notes { get; set; }
        public string ProjectCoordinatorName { get; set; }
        public string ProjectCoordinatorEmail { get; set; }
        public string PRojectCoordinatorPhone { get; set; }
    }
}