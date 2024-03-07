using AION.BL;
using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace AION.Manager.Models
{
    public class SendProjectNotification
    {
        /// <summary>
        /// AION Project Id
        /// </summary>
        public int ProjectId { get; set; }
        /// <summary>
        /// Accela Project ID
        /// </summary>
        public string AccelaProjectId { get; set; }
        /// <summary>
        /// the type of notification, used for sorting on Resend UI
        /// </summary>
        public EmailNotifType EmailNotif { get; set; }
        /// <summary>
        /// the Body and subject of the notification
        /// </summary>
        public MailMessage MailMessage { get; set; }
        /// <summary>
        /// ProjectNotificationEmail table identity
        /// </summary>
        public int ProjectNotificationEmailId { get; set; }
        /// <summary>
        /// List of User ids to send to
        /// </summary>
        public List<int> UserIds { get; set; }
        /// <summary>
        /// List of entered email txts to send to
        /// </summary>
        public List<string> EmailTxts { get; set; }
        /// <summary>
        /// Send to all who recieved this previously
        /// if this is true, the lists will be null
        /// </summary>
        public bool SendToAll { get; set; }
        /// <summary>
        /// User id of person who initiated the send
        /// </summary>
        public int WrkId { get; set; }

        /// <summary>
        /// Date the notification is sent
        /// </summary>
        public DateTime? SendDate { get; internal set; }
    }
}