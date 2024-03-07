using System;
using System.Collections.Generic;

namespace AION.Manager.Models
{
    public class SendPendingEmailModel
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectStatusDesc { get; set; }
        public string PendingCommentsToCustomer { get; set; }
        public string Usernamepublic { get; set; }
        public DateTime Timestamp { get; set; }
        public string AccelaProjectId { get; set; }
        public int WrkId { get; set; }
        public List<int> SendUserIds { get; set; }
        public bool IsPreliminaryMeeting { get; set; }
        public bool IsExpress { get; set; }
    }
}