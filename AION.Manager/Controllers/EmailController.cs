using AION.Base;
using AION.BL.Adapters;
using AION.Manager.Models;
using Microsoft.Graph;
using System.Collections.Generic;
using System.Net.Mail;
using System.Web.Http;
using System.Web.Http.Description;

namespace AION.Manager.Controllers
{
    [Authorize]
    public class EmailController : BaseApiController
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sendNAEmailModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/Email/SendNAEmail")]
        public IHttpActionResult SendNAEmail(SendNAEmailModel sendNAEmailModel)
        {
            IEmailAdapter thisengine = new EmailAdapter();
            bool success = false;
            MailMessage emailmessage = new MailMessage();

            emailmessage.Body = thisengine.CreateNAEmailMessageBody(
                sendNAEmailModel.AccelaProjectRefId,
                sendNAEmailModel.ProjectName,
                sendNAEmailModel.ProjectAddress);
            emailmessage.Subject = "All Trade / Agencies Estimated Project " + sendNAEmailModel.AccelaProjectRefId + " as NA";
            success = thisengine.SendNAEmail(emailmessage);
            return Ok(success);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sendPendingEmailModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/Email/SendPendingNotificationEmail")]
        public IHttpActionResult SendPendingNotificationEmail(SendPendingEmailModel sendPendingEmailModel)
        {
            IEmailAdapter thisengine = new EmailAdapter();
            bool success = thisengine.SendPendingNotificationEmail(sendPendingEmailModel);
            return Ok(success);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sendPrelimMeetingEmailModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/Email/SendPrelimMeetingScheduledEmail")]
        public IHttpActionResult SendPrelimMeetingScheduledEmail(SendPrelimMeetingEmailModel sendPrelimMeetingEmailModel)
        {
            IEmailAdapter thisengine = new EmailAdapter();
            bool success = false;
            MailMessage emailmessage = new MailMessage();

            emailmessage.Body = thisengine.CreatePrelimMeetingScheduledEmailBody(
                sendPrelimMeetingEmailModel.ProjectId,
                sendPrelimMeetingEmailModel.ProjectName,
                sendPrelimMeetingEmailModel.ProjectAddress,
                sendPrelimMeetingEmailModel.MeetingDate,
                sendPrelimMeetingEmailModel.MeetingRoom,
                sendPrelimMeetingEmailModel.Notes);
            emailmessage.Subject = "Preliminary Meeting Tentatively Scheduled for Project # " + sendPrelimMeetingEmailModel.ProjectId + " " + sendPrelimMeetingEmailModel.ProjectName;
            success = thisengine.SendPrelimMeetingScheduledEmail(emailmessage);
            return Ok(success);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sendPrelimMeetingEmailModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/Email/CancelPrelimMeetingSchedEmail")]
        public IHttpActionResult CancelPrelimMeetingSchedEmail(SendPrelimMeetingEmailModel sendPrelimMeetingEmailModel)
        {
            IEmailAdapter thisengine = new EmailAdapter();
            bool success = false;
            MailMessage emailmessage = new MailMessage();

            emailmessage.Body = thisengine.CancelPrelimMeetingScheduledEmailBody(
                sendPrelimMeetingEmailModel.ProjectId,
                sendPrelimMeetingEmailModel.ProjectName,
                sendPrelimMeetingEmailModel.ProjectAddress,
                sendPrelimMeetingEmailModel.MeetingDate);
            emailmessage.Subject = "Review Tentatively Scheduled Date Cancelled for Project #" + sendPrelimMeetingEmailModel.ProjectId + " " + sendPrelimMeetingEmailModel.ProjectName;
            success = thisengine.SendPrelimMeetingScheduledEmail(emailmessage);
            return Ok(success);
        }

        /// <summary>
        /// Returns List of Notification emails by Project
        /// </summary>
        /// <param name="id">AION Project Id</param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<ProjectNotificationEmail>))]
        [Route("api/Email/GetProjectNotificationEmailList")]
        public IHttpActionResult GetProjectNotificationEmailList(int id)
        {
            IEmailAdapter thisengine = new EmailAdapter();
            var result = thisengine.GetProjectNotificationEmails(id);
            return Ok(result);
        }

        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/Email/ResendProjectNotification")]
        public IHttpActionResult ResendProjectNotification(SendProjectNotification resendProjectNotification)
        {
            IEmailAdapter thisengine = new EmailAdapter();
            var result = thisengine.ResendProjectNotification(resendProjectNotification);
            return Ok(result);
        }
    }
}