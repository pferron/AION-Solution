using System;
using System.Collections.Generic;

namespace AION.Manager.Models
{
    public class ProjectNotificationEmail
    {
        public int? ProjectEmailNotificationId { get; set; }

        public int? ProjectId { get; set; }

        public string EmailTypeDesc { get; set; }

        public string EmailSubjectText { get; set; }

        public string EmailBodyTxt { get; set; }

        public DateTime? EmailSentDt { get; set; }

        public int? SenderUserId { get; set; }

        public List<NotificationEmailList> NotificationEmailList { get; set; }
    }
}