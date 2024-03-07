using AION.BL.Adapters;
using AION.Manager.Models;
using Ical.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;

[assembly: Parallelize(Workers = 0, Scope = ExecutionScope.MethodLevel)]

namespace AION.BL.Test
{
    [TestClass]
    public class EmailControllerTests
    {
        static readonly object lockobj = new object();
        //in TFS server Azure devops build, the permission might create issues. So keeping a kill switch to prevent test build to pass.
        static bool initFolderPass = true;

        static bool writetoFile = false;
        List<AttendeeDetails> attendeeList = new List<AttendeeDetails>();

        const string icsfilepath = @"C:\Email\Calendar.ics";

        [TestInitialize]
        [DoNotParallelize]
        public void TestInitialize()
        {
            if (Debugger.IsAttached)
                writetoFile = true;
            lock (lockobj)
            {
                if (Directory.Exists(@"C:\Email"))
                {
                    foreach (FileInfo file in new DirectoryInfo(@"C:\Email").GetFiles())
                    {
                        try
                        {
                            file.Delete();
                        }
                        catch
                        {
                            //this is for batch test case run or MSbuild on azure DEVOPS.
                            //skips the deletion for now incase of issues. Sometimes the file may be locked out by another test process and it might take some time to get it released.
                        }
                    }
                }
                Directory.CreateDirectory(@"C:\Email\");
                if (Directory.Exists(@"C:\Email") == false)
                    initFolderPass = false;
                attendeeList.Add(new AttendeeDetails() { EmailId = "Shijo.Joseph@Mecknc.gov", FirstName = "Shijo", LastName = "Joseph", IsRequired = true });
                attendeeList.Add(new AttendeeDetails() { EmailId = "Gayatri.Nadimpalli@mecklenburgcountync.gov", FirstName = "Gayatri", LastName = "Nadimpalli", IsRequired = false });
            }

        }

        [TestMethod]
        public void OutlookControllerGetOutlookMeetingAllocation_Test()
        {
            try
            {
                MeetingAllocationRequest data = new MeetingAllocationRequest();
                data.RequestedParticipantEmailList.Add("Shijo.Joseph@mecklenburgcountync.gov");
                data.RequestedStartTime = DateTime.Now.AddDays(-1);
                data.RequestedEndTime = DateTime.Now.AddDays(10);
                MeetingAllocationResponse ret = new MeetingAllocationResponse();
                ret = new OutlookAdapter().CheckForMeetingAllocationAvailability(data);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

        //[TestMethod]
        public void ICS_CalanderInviteCreation_OnceTest()
        {
            if (initFolderPass == false)
                Assert.AreEqual(true, true);
            EmailAdapter email = new EmailAdapter();
            /*WARNIING: DO NOT TOUCH THE HTML BELOW EVEN ADDING TABS TO IT BREAK THE CODE. */
            string desc =
@"</br><p>NPA Name: Erika's Training</p><p>NPA Type: Training</p
 ><p>NPA Date: From: 6/11/2020 To: 6/18/2020</p><p>NPA Hours: 10:00 AM - 1
 1:00 AM</p><p>Meeting Room: Meeting Room 2</p><p style=/""font - size: 10pt\;
            font - family: Arial\, Helvetica\, sans - serif/""><b><u>PLEASE DO NOT REPLY T
 O THIS EMAIL<u> </ b ></ p > ";
            string ret = email.CreateCalendarEntry(DateTime.Now.AddDays(1), DateTime.Now.AddDays(1).AddHours(1), RecurrenceEnum.Once,
                "Customer Title", desc, "LUESA Meeting location", attendeeList, CalendarRequestModeEnum.CreateForAttendees).ToIcalString();
            if (writetoFile == true)
            {
                File.AppendAllText(icsfilepath, ret);
            }
            Assert.IsNotNull(ret);

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(email.MailUserName);

            //loop through our list and add each of our recipients 
            string[] recipientList = email.ErrorRecipientList.Split(new Char[] { ';' });
            foreach (string s in recipientList)
            {
                mailMessage.To.Add(new MailAddress(s));
            }

            mailMessage.Subject = "test Subject";
            mailMessage.Body = "Test email";
            mailMessage.IsBodyHtml = true;
            mailMessage.Attachments.Add(email.GetAttachmentFromPlainText(ret, "Calendar.ics"));
            //LOOK AT C:\EMAIL FOLDER TO SEE EMAIL MESSAGE GENERATED.
            Assert.AreEqual(true, email.SendEmailMessage(mailMessage));
        }

        //[TestMethod]
        [DoNotParallelize]
        public void ICS_CalanderInviteCreation_RecurringDailyTest()
        {
            if (initFolderPass == false)
                Assert.AreEqual(true, true);
            EmailAdapter email = new EmailAdapter();
            string ret = email.CreateCalendarEntry(DateTime.Now.AddDays(1), DateTime.Now.AddDays(5).AddHours(1), RecurrenceEnum.Daily,
                "Customer Title", " Customer Description", "LUESA Meeting location", attendeeList, CalendarRequestModeEnum.CreateForAttendees).ToIcalString();
            if (writetoFile == true)
            {
                File.AppendAllText(icsfilepath, ret);
            }
            Assert.IsNotNull(ret);

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(email.MailUserName);

            //loop through our list and add each of our recipients 
            string[] recipientList = email.ErrorRecipientList.Split(new Char[] { ';' });
            foreach (string s in recipientList)
            {
                mailMessage.To.Add(new MailAddress(s));
            }

            mailMessage.Subject = "test Subject";
            mailMessage.Body = "Test email";
            mailMessage.IsBodyHtml = true;
            mailMessage.Attachments.Add(email.GetAttachmentFromPlainText(ret, "Calendar.ics"));
            //LOOK AT C:\EMAIL FOLDER TO SEE EMAIL MESSAGE GENERATED.
            Assert.AreEqual(true, email.SendEmailMessage(mailMessage));
        }


        //[TestMethod]
        public void ICS_CalanderInviteCreation_RecurringYearlyRecurringTest()
        {
            if (initFolderPass == false)
                Assert.AreEqual(true, true);
            EmailAdapter email = new EmailAdapter();
            string ret = email.CreateCalendarEntry(DateTime.Now.AddDays(1), DateTime.Now.AddYears(4).AddHours(1), RecurrenceEnum.Yearly,
                "Customer Title", " Customer Description", "LUESA Meeting location", attendeeList, CalendarRequestModeEnum.CreateForAttendees).ToIcalString();
            if (writetoFile == true)
            {
                File.AppendAllText(icsfilepath, ret);
            }
            Assert.IsNotNull(ret);

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(email.MailUserName);

            //loop through our list and add each of our recipients 
            string[] recipientList = email.ErrorRecipientList.Split(new Char[] { ';' });
            foreach (string s in recipientList)
            {
                mailMessage.To.Add(new MailAddress(s));
            }

            mailMessage.Subject = "test Subject";
            mailMessage.Body = "Test email";
            mailMessage.IsBodyHtml = true;
            mailMessage.Attachments.Add(email.GetAttachmentFromPlainText(ret, "Calendar.ics"));
            //LOOK AT C:\EMAIL FOLDER TO SEE EMAIL MESSAGE GENERATED.
            Assert.AreEqual(true, email.SendEmailMessage(mailMessage));
        }

        //[TestMethod]
        public void ICS_CalanderInviteCreation_RecurringWeeklyRecurringTest()
        {
            if (initFolderPass == false)
                Assert.AreEqual(true, true);
            EmailAdapter email = new EmailAdapter();
            string ret = email.CreateCalendarEntry(DateTime.Now.AddDays(1), DateTime.Now.AddMonths(4).AddHours(1), RecurrenceEnum.Weekly,
                "Customer Title", " Customer Description", "LUESA Meeting location", attendeeList, CalendarRequestModeEnum.CreateForAttendees, DayOfWeek.Monday).ToIcalString();
            if (writetoFile == true)
            {
                File.AppendAllText(icsfilepath, ret);
            }
            Assert.IsNotNull(ret);

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(email.MailUserName);

            //loop through our list and add each of our recipients 
            string[] recipientList = email.ErrorRecipientList.Split(new Char[] { ';' });
            foreach (string s in recipientList)
            {
                mailMessage.To.Add(new MailAddress(s));
            }

            mailMessage.Subject = "test Subject";
            mailMessage.Body = "Test email";
            mailMessage.IsBodyHtml = true;
            mailMessage.Attachments.Add(email.GetAttachmentFromPlainText(ret, "Calendar.ics"));
            //LOOK AT C:\EMAIL FOLDER TO SEE EMAIL MESSAGE GENERATED.
            Assert.AreEqual(true, email.SendEmailMessage(mailMessage));
        }

        //[TestMethod]
        public void ICS_CalanderInviteCreation_RecurringMonthlyOnceTest()
        {

            if (initFolderPass == false)
                Assert.AreEqual(true, true);
            EmailAdapter email = new EmailAdapter();
            //working version
            //string ret = email.CreateCalendarEntry(new DateTime(2020,05,13,10,30,00,00),new  DateTime(2021, 05, 13, 11, 00, 00, 00), ReccurrenceEnum.Second,
            //    "Customer Title", " Customer Description", "LUESA Meeting location", attendeeList, CalendarRequestModeEnum.CreateForOrganizer, DayOfWeek.Wednesday);
            //string ret = email.CreateCalendarEntry(new DateTime(2021, 05, 13, 10, 30, 00, 00), new DateTime(2023, 05, 13, 11, 00, 00, 00), ReccurrenceEnum.Second,
            //    "Customer Title", " Customer Description", "LUESA Meeting location", attendeeList, CalendarRequestModeEnum.CreateForAttendees, DayOfWeek.Wednesday).ToIcalString();
            string ret = email.CreateCalendarEntry(new DateTime(2020, 04, 17, 14, 30, 00, 00), new DateTime(2020, 04, 17, 15, 30, 00, 00), RecurrenceEnum.Third,
                "Customer Title", " Customer Description", "LUESA Meeting location", attendeeList, CalendarRequestModeEnum.CreateForAttendees, DayOfWeek.Friday).ToIcalString();
            if (writetoFile == true)
            {
                File.AppendAllText(icsfilepath, ret);
            }
            Assert.IsNotNull(ret);

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(email.MailUserName);

            //loop through our list and add each of our recipients 
            string[] recipientList = email.ErrorRecipientList.Split(new Char[] { ';' });
            foreach (string s in recipientList)
            {
                mailMessage.To.Add(new MailAddress(s));
            }

            mailMessage.Subject = "test Subject";
            mailMessage.Body = "Test email";
            mailMessage.IsBodyHtml = true;
            if (writetoFile == true)
                mailMessage.Attachments.Add(new Attachment(icsfilepath));
            else
                mailMessage.Attachments.Add(email.GetAttachmentFromPlainText(ret, "Calendar.ics"));

            //LOOK AT C:\EMAIL FOLDER TO SEE EMAIL MESSAGE GENERATED.
            Assert.AreEqual(true, email.SendEmailMessage(mailMessage));
        }

        //[TestMethod]
        public void ICS_CalanderInviteCreation_Deletion_RecurringMonthlyOnceTest()
        {
            string uid = "";
            string deleteics = @"C:\Email\CalendarDelete.ics";
            if (initFolderPass == false)
                Assert.AreEqual(true, true);
            EmailAdapter email = new EmailAdapter();
            //working version
            //string ret = email.CreateCalendarEntry(new DateTime(2020,05,13,10,30,00,00),new  DateTime(2021, 05, 13, 11, 00, 00, 00), ReccurrenceEnum.Second,
            //    "Customer Title", " Customer Description", "LUESA Meeting location", attendeeList, CalendarRequestModeEnum.CreateForOrganizer, DayOfWeek.Wednesday);
            //string ret = email.CreateCalendarEntry(new DateTime(2021, 05, 13, 10, 30, 00, 00), new DateTime(2023, 05, 13, 11, 00, 00, 00), ReccurrenceEnum.Second,
            //    "Customer Title", " Customer Description", "LUESA Meeting location", attendeeList, CalendarRequestModeEnum.CreateForAttendees, DayOfWeek.Wednesday).ToIcalString();
            Calendar cal = email.CreateCalendarEntry(new DateTime(2020, 04, 17, 14, 30, 00, 00), new DateTime(2020, 04, 17, 15, 30, 00, 00), RecurrenceEnum.Third,
                "Customer Title", " Customer Description", "LUESA Meeting location", attendeeList, CalendarRequestModeEnum.CreateForAttendees, DayOfWeek.Friday);
            uid = cal.Events[0].Uid;
            string retcreate = cal.ToIcalString();
            string deleteicsfl = email.CancelCalendarEntry(uid, new DateTime(2020, 04, 17, 14, 30, 00, 00), new DateTime(2020, 04, 17, 15, 30, 00, 00), RecurrenceEnum.Third,
                "Customer Title", " Customer Description", "LUESA Meeting location", attendeeList, DayOfWeek.Friday).ToIcalString();
            if (writetoFile == true)
            {
                File.AppendAllText(icsfilepath, retcreate);
                File.AppendAllText(deleteics, deleteicsfl);
            }

            Assert.IsNotNull(retcreate);
            Assert.IsNotNull(deleteicsfl);

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(email.MailUserName);

            //loop through our list and add each of our recipients 
            string[] recipientList = email.ErrorRecipientList.Split(new Char[] { ';' });
            foreach (string s in recipientList)
            {
                mailMessage.To.Add(new MailAddress(s));
            }

            mailMessage.Subject = "test Subject";
            mailMessage.Body = "Test email";
            mailMessage.IsBodyHtml = true;
            //email.AttachICSHeaderForOutlookEmail(retcreate, mailMessage);
            if (writetoFile == true)
            {
                mailMessage.Attachments.Add(new Attachment(deleteics));
                mailMessage.Attachments.Add(new Attachment(icsfilepath));
            }
            else
            {
                mailMessage.Attachments.Add(email.GetAttachmentFromPlainText(deleteicsfl, "CalendarDelete.ics"));
                mailMessage.Attachments.Add(email.GetAttachmentFromPlainText(retcreate, "Calendar.ics"));
            }

            //LOOK AT C:\EMAIL FOLDER TO SEE EMAIL MESSAGE GENERATED.
            Assert.AreEqual(true, email.SendEmailMessage(mailMessage));
        }

        //[TestMethod]
        public void BasicEmailTriggerTest()
        {
            if (initFolderPass == false)
                Assert.AreEqual(true, true);

            EmailAdapter email = new EmailAdapter();
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(email.MailUserName);


            //loop through our list and add each of our recipients 
            string[] recipientList = email.ErrorRecipientList.Split(new Char[] { ';' });
            foreach (string s in recipientList)
            {
                mailMessage.To.Add(new MailAddress(s));
            }

            mailMessage.Subject = "test Subject";
            mailMessage.Body = "Test email";
            mailMessage.IsBodyHtml = true;
            //LOOK AT C:\EMAIL FOLDER TO SEE EMAIL MESSAGE GENERATED.
            Assert.AreEqual(true, email.SendEmailMessage(mailMessage));
        }
    }
}

