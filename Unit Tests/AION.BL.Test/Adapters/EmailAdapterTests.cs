using AION.BL.Adapters;
using AION.Manager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace AION.BL.Test.Adapters
{
    [TestClass]
    public class EmailAdapterTests
    {
        [TestMethod]
        [Ignore]
        public void SendApptCity()
        {
            EmailAdapter adapter = new EmailAdapter();

            RecurrenceEnum recurrence = RecurrenceEnum.Once;
            DateTime startDate = DateTime.Now.AddHours(1);
            DateTime endDate = startDate.AddHours(1);
            List<AttendeeDetails> attendees = new List<AttendeeDetails>() {
                new AttendeeDetails() {
                    EmailId = "janessa.allen@mecklenburgcountync.gov",
                    FirstName = "Janessa",
                    IsRequired = true,
                    LastName = "Allen",
                    UserId = 264
                }
            };
            string title = "Appointment Title";
            string description = "<p>Appointment Description</p>";
            string location = "Appointment location";

            bool success = adapter.SendAppt(recurrence, startDate, endDate, attendees, title, description, location);

            Assert.IsTrue(success);
        }

        //[TestMethod]
        public void ProjectManagerToAdminEmailCreationSuccess()
        {
            EmailAdapter adapter = new EmailAdapter();
            bool success = adapter.SendUpdateProjectManagerToAdmin("project manager name", "project mananager email", "PRE-DEMO-TOWNHOME");
            Assert.IsTrue(success);
        }

        //[TestMethod]
        public void SendNewProjectManagerToAdminSuccess()
        {
            EmailAdapter adapter = new EmailAdapter();
            bool success = adapter.SendNewProjectManagerToAdmin("project manager name", "project mananager email", "COS-PM-000098");
            Assert.IsTrue(success);
        }

        //[TestMethod]
        public void SendPendingNotificationEmailSuccess()
        {
            IEmailAdapter adapter = new EmailAdapter();
            List<int> users = new List<int>();
            users.Add(112);
            users.Add(65);

            SendPendingEmailModel sendPendingEmailModel = new SendPendingEmailModel
            {
                ProjectId = 7882,
                AccelaProjectId = "JEANINE-COMMERCIAL-03102021-PRELIM30",
                IsPreliminaryMeeting = false,
                //PendingCommentsToCustomer = "this is all the comments for this",
                ProjectName = "JEANINE-COMMERCIAL-03102021-PRELIM30",
                ProjectStatusDesc = "Scope Drawings Required",
                SendUserIds = users,
                Timestamp = DateTime.Parse("2021-12-08 17:46:00"),
                Usernamepublic = "jeanine",
                WrkId = 112
            };
            bool success = adapter.SendPendingNotificationEmail(sendPendingEmailModel);
            Assert.IsTrue(success);
        }
        [Ignore]
        [TestMethod]
        public void CreatePrelimMeetingScheduledEmailBodySuccess()
        {
            IEmailAdapter adapter = new EmailAdapter();
            string projectId = "JBA-PRELIM-05-05";
            string projectName = "JBA-PRELIM-05-05";
            string projectAddress = "";
            DateTime meetingDate = DateTime.Parse("05/25/2021 8:00AM");
            string meetingRoom = "";
            string notes = "lotta notes<br/>more notes";

            string emailbody = adapter.CreatePrelimMeetingScheduledEmailBody(projectId, projectName, projectAddress,
             meetingDate, meetingRoom, notes);
            bool success = false;
            MailMessage emailmessage = new MailMessage();

            emailmessage.Body = emailbody;
            emailmessage.Subject = MessageTemplateTypeEnum.Preliminary_Meeting_AcceptReject_Email.ToStringValue();
            success = adapter.SendPrelimMeetingScheduledEmail(emailmessage);


            Assert.IsNotNull(emailbody);
        }
        [Ignore]
        [TestMethod]
        public void CreateMeetingScheduledMessageBodySuccess()
        {
            IEmailAdapter adapter = new EmailAdapter();
            string projectId = "JBA-PRELIM-05-05";
            string projectName = "JBA-PRELIM-05-05";
            string projectAddress = "";
            DateTime meetingDate = DateTime.Parse("05/25/2021 8:00AM");
            string meetingRoom = "Pineville";

            string emailbody = adapter.CreateMeetingScheduledMessageBody(projectId, projectName, projectAddress,
             meetingDate, meetingRoom, MeetingTypeEnum.Project_Challenges, "", "", "");
            bool success = false;
            MailMessage emailmessage = new MailMessage();

            emailmessage.Body = emailbody;
            emailmessage.Subject = MessageTemplateTypeEnum.Meeting_AcceptReject_email.ToStringValue();
            success = adapter.SendPrelimMeetingScheduledEmail(emailmessage);


            Assert.IsNotNull(emailbody);

        }
        [Ignore]
        [TestMethod]
        public void CreateAccelaFailureEmailSendSuccess()
        {

            EmailAdapter adapter = new EmailAdapter();

            List<AccelaFailure> accelaFailures = new List<AccelaFailure>();
            accelaFailures.Add(new AccelaFailure
            {
                AccelaEnvironment = "SUPP",
                FailureType = "",
                Message = "This is a MS Graph test",
                ProjectNumber = "",
                RecordId = "fakerecordid",
                TimeStamp = DateTime.UtcNow.ToString()
            });
            accelaFailures.Add(new AccelaFailure
            {
                AccelaEnvironment = "SUPP",
                FailureType = "",
                Message = "This is a MS Graph test",
                ProjectNumber = "",
                RecordId = "fakerecordid",
                TimeStamp = DateTime.UtcNow.ToString()
            });

            bool success = adapter.SendAccelaIntegrationFailure(accelaFailures);

            Assert.IsTrue(success);
        }

        //[TestMethod]
        public void CreateSchedulingLeadTimeReportEmailSendSuccess()
        {

            EmailAdapter adapter = new EmailAdapter();

            bool success = adapter.SendSchedulingLeadTimeReportDataAvailable(
                "carol.lindsay@mecklenburgcountync.gov", DateTime.UtcNow);

            Assert.IsTrue(success);
        }
        [Ignore]
        [TestMethod]
        public void SendFunctionAdapterSyncFailureEmailSuccess()
        {
            EmailAdapter adapter = new EmailAdapter();

            var failure = new Failure
            {
                Environment = "ONPREM",
                FailureType = "This is a MS Graph test",
                Message = "FunctionAdapterSyncFailure Unit test 2",
                TimeStamp = DateTime.Now.ToString()
            };

            bool success = adapter.SendFunctionAdapterSyncFailure(failure);

        }
    }
}
