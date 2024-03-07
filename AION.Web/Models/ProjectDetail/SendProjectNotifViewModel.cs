using AION.BL;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AION.Web.Models.ProjectDetail
{
    public class SendProjectNotifViewModel : ViewModelBase
    {
        public int ProjectNotifEmailId { get; set; }
        public DateTime SendDate { get; set; }
        public EmailNotifType EmailNotif { get; set; }
        public bool ResendAll { get; set; }
        public List<SelectListItem> Recipients { get; set; }
        public string AddRecipients { get; set; }
        public string SelectedRecipient { get; set; }
    }
}