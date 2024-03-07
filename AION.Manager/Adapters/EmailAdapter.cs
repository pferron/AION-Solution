using AION.BL.BusinessObjects;
using AION.BL.Common;
using AION.Email.Engine.BusinessObjects;
using AION.Engine.BusinessEntities;
using AION.Engine.BusinessObjects;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.Adapters;
using AION.Manager.BusinessObjects;
using AION.Manager.Common;
using AION.Manager.Engines;
using AION.Manager.Models;
using AIONEstimator.Engine.BusinessObjects;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using Meck.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Text;

namespace AION.BL.Adapters
{
    public class EmailAdapter : BaseManagerAdapter, IEmailAdapter
    {

        /*REF: ICAL references.*/
        //https://github.com/rianjs/ical.net/wiki // https://github.com/rianjs/ical.net
        //https://www.codeproject.com/Articles/17980/Adding-iCalendar-Support-to-Your-Program-Part-1
        //https://rbalajiprasad.blogspot.com/2012/11/mvc-c-create-ical-calendar-ics-feed.html
        //https://code.daypilot.org/68025/asp-net-event-calendar-export-to-icalendar
        //https://stackoverflow.com/questions/461889/sending-outlook-meeting-requests-without-outlook
        //http://www.ddaysoftware.com/Pages/Projects/DDay.iCal/
        //https://www.nylas.com/blog/calendar-events-rrules/
        //https://tools.ietf.org/html/rfc2445#section-4.8.5.4
        //https://github.com/rianjs/ical.net/wiki/Working-with-recurring-elements
        //https://stackoverflow.com/questions/461889/sending-outlook-meeting-requests-without-outlook
        /*ISSUES */
        // 1)   Organizer cannot create an event to him self. So Ical.Net cannot create an event to himself if created with Method = REQUEST. 
        // 1.1) Ref: https://stackoverflow.com/questions/15084236/icalendar-does-not-create-an-event-for-organizer

        public string MailUserName { get; set; }

        public string ErrorRecipientList { get; set; }

        public bool IsRunningFromAzure { get; set; }

        public string MandatoryEmailRecipientList { get; set; }

        public string AccelaEnvironment { get; set; }
        public EmailAdapter()
        {
            IsRunningFromAzure = bool.Parse(ConfigurationManager.AppSettings["IsRunningFromAzure"]);

            ErrorRecipientList = IsRunningFromAzure ? ConfigurationManager.AppSettings["AzureRecipientList"].ToString() : ConfigurationManager.AppSettings["LocalRecipientList"].ToString();
            MailUserName = IsRunningFromAzure ? ConfigurationManager.AppSettings["AzureMailFromUsername"].ToString() : ConfigurationManager.AppSettings["LocalMailUserName"].ToString();

            MandatoryEmailRecipientList = GetMandatoryEmailRecipientList();
            AccelaEnvironment = ConfigurationManager.AppSettings["AccelaEnvironment"].ToString();

        }
        string GetMandatoryEmailRecipientList()
        {
            return new CatalogRefBO().GetByExternalRef("EMAIL").Where(x => x.Key == "EMAILENGINE.MANDATORYLIST").FirstOrDefault().Value;

        }
        /// <summary>
        /// Cancels a Calendar object based on UID and parameters passed. This can be converted to text and then attached as .ics file to a mail to make the appointment.
        /// <list type="number">
        /// <para>You can use extension method ToIcalString() to convert the return value to string.</para>
        /// </list>
        /// </summary>
        /// <param name="originalCalendarUid">Original UID used by calendar when created.</param>
        /// <param name="startdate">Start date with start Time for the appointment to be blocked.</param>
        /// <param name="enddate">End date with start Time for the appointment to be blocked.</param>
        /// <param name="reccurrenceMode">Specifies the mode of Recurrance like Once, Weekly, Monthly etc</param>
        /// <param name="title">Title used in meeting invite. This will appear as mail subject upon opening the invite.</param>
        /// <param name="description">Main content. This could be anything including text or html.</param>
        /// <param name="location">Location for the meeting.</param>
        /// <param name="attendeeDetails">List of Attendees</param>
        /// <param name="day">Required only incase of weekly and monthly appointments. Otherwise throws exception.</param>
        /// <param name="excludedWeekDays"></param>
        /// <returns>instance of calendar.  You can use extension method ToIcalString() to convert the return value to string.</returns>
        public Calendar CancelCalendarEntry(string originalCalendarUid, DateTime startdate, DateTime enddate, RecurrenceEnum reccurrenceMode, string title, string description, string location,
            List<AttendeeDetails> attendeeDetails, DayOfWeek? day = null, List<DayOfWeek> excludedWeekDays = null)
        {
            Calendar ret = CreateCalendarEntry(startdate, enddate, reccurrenceMode, title, description, location, attendeeDetails, CalendarRequestModeEnum.CancelAppointment, day, excludedWeekDays);
            ret.Events[0].Uid = originalCalendarUid;
            //assume there is no changes done to event since creation, which is set to 0. every change in cal it is supposed to increment by 1. As of now requirement is only for add and delete. any changes made in between, user need to delete the event manually.
            ret.Events[0].Sequence = 1;
            return ret;
        }

        /// <summary>
        /// Creates a Calendar object based on paramaters passed. This can be converted to text and then attached as .ics file to a mail to make the appointment.
        /// <list type="number">
        /// <para>1) You can use extension method ToIcalString() to convert the return value to string.</para>
        /// <para>2) Incase you want to update/delete the event in future you need to save the Calendar.UID and provide that as part of CancelCalendarEntry.</para>
        /// </list>
        /// </summary>
        /// <param name="startdate">Start date with start Time for the appointment to be blocked.</param>
        /// <param name="enddate">End date with start Time for the appointment to be blocked.</param>
        /// <param name="reccurrenceMode">Specifies the mode of Recurrance like Once, Weekly, Monthly etc</param>
        /// <param name="title">Title used in meeting invite. This will appear as mail subject upon opening the invite.</param>
        /// <param name="description">Main content. This could be anything including text or html.</param>
        /// <param name="location">Location for the meeting.</param>
        /// <param name="attendeeDetails">List of Attendees</param>
        /// <param name="requestMode">specify mutiple modes of creation like Create new appointment,Delete an exsiting appointment etc.</param>
        /// <param name="day">Required only incase of weekly and monthly appoinments.othewise throws exception.</param>
        /// <param name="excludedWeekDays"></param>
        /// <returns>instance of calendar.  You can use extension method ToIcalString() to convert the return value to string.</returns>
        public Calendar CreateCalendarEntry(DateTime startdate, DateTime enddate, RecurrenceEnum reccurrenceMode, string title, string description, string location,
            List<AttendeeDetails> attendeeDetails, CalendarRequestModeEnum requestMode = CalendarRequestModeEnum.CreateForAttendees, DayOfWeek? day = null, List<DayOfWeek> excludedWeekDays = null)
        {
            try
            {
                if (startdate.AddMinutes(1) >= enddate)
                    throw new ArgumentException("Error in calendar date range. Start must be less than end time");
                if ((reccurrenceMode == RecurrenceEnum.Once || reccurrenceMode == RecurrenceEnum.Daily || reccurrenceMode == RecurrenceEnum.Yearly) && day != null)
                    throw new ArgumentException("Error in arguments. day is required only on Weekly and Monthly modes!");

                DateTime start = new DateTime(startdate.Year, startdate.Month, startdate.Day, startdate.Hour, startdate.Minute, startdate.Second, startdate.Millisecond);
                DateTime end = new DateTime(enddate.Year, enddate.Month, enddate.Day, enddate.Hour, enddate.Minute, enddate.Second, enddate.Millisecond);
                List<ScheduleTime> recurrdays = new List<ScheduleTime>();
                Calendar ical = new Calendar();
                ical.ProductId = "-//Google Inc//Google Calendar 70.9054//EN";// making it to match google calendar. It appears outlook got integration done properly for google standard. So match everyting to that.
                ical.Scale = "GREGORIAN";
                ical.TimeZones.Add(CreateESTVTimeZone());
                if (requestMode == CalendarRequestModeEnum.CreateForAttendees)
                    ical.Method = "REQUEST"; //This mode need to be used to all other attendees except organizer. This creates an invite with accept reject button options in outlook.
                else if (requestMode == CalendarRequestModeEnum.CreateForOrganizer)
                    ical.Method = "PUBLISH"; //This mode is exclusive for event organizer so that he can add the event to his calendar. Not recommended since the user need to open and click send update to make this work.
                else
                    ical.Method = "CANCEL"; //This mode along with //evnt.Status = "CANCELLED"; need to be used incase of cancellation.
                string TZID = "Eastern Standard Time";

                RecurrencePattern rrule;
                Alarm reminder;
                TimeSpan duration;
                DateTime endtime;
                // Create the event, and add it to the iCalendar
                CalendarEvent evnt = ical.Create<CalendarEvent>();
                evnt.AddProperty(new CalendarProperty("X-ALT-DESC;FMTTYPE=text/html", description));
                evnt.Description = description;
                evnt.Summary = title;
                //"1913d514-696e-4237-bc3c-c1d073eacced" or 63c7he40hqqnvmmkus57720uug@google.com . will need to be replaced by original UID incase of edited or delted before converting to string
                evnt.Uid = Guid.NewGuid().ToString();
                evnt.Location = location;
                //Ref: https://www.kanzaki.com/docs/ical/sequence.html
                evnt.Sequence = 0;
                evnt.AddProperty("X-MICROSOFT-CDO-OWNERAPPTID", "-1195236389");
                evnt.Transparency = "OPAQUE";
                if (requestMode == CalendarRequestModeEnum.CancelAppointment)
                    evnt.Status = "CANCELLED";
                else
                    evnt.Status = "CONFIRMED";
                foreach (var item in attendeeDetails)
                {
                    var attendee = new Attendee("mailto:" + item.EmailId);
                    if (string.IsNullOrEmpty(item.FirstName) == false && string.IsNullOrEmpty(item.LastName) == false)
                        attendee.CommonName = item.LastName + ", " + item.FirstName;
                    else
                        attendee.CommonName = item.EmailId;
                    if (item.IsRequired == false)
                        attendee.Role = "OPT-PARTICIPANT";
                    else
                        attendee.Role = "REQ-PARTICIPANT";
                    attendee.ParticipationStatus = "NEEDS-ACTION";
                    attendee.Rsvp = true;
                    attendee.Parameters.Add(new CalendarParameter("X-NUM-GUESTS", "0"));
                    attendee.Type = "INDIVIDUAL";
                    evnt.Attendees.Add(attendee);
                }

                evnt.Organizer = new Organizer(MailUserName);

                if (DateTime.Now.AddMinutes(15) < start)
                {
                    // Create a reminder 15m before the event
                    reminder = new Alarm();
                    reminder.Action = AlarmAction.Display;
                    reminder.Trigger = new Trigger(new TimeSpan(0, -15, 0));
                    reminder.Description = "Reminder";
                    evnt.Alarms.Add(reminder);
                }

                switch (reccurrenceMode)
                {
                    case RecurrenceEnum.Once:
                        //adding start and end of event.
                        evnt.Start = new CalDateTime(start);
                        evnt.End = new CalDateTime(end);
                        break;

                    // This is monthly once on specified first instance of weekday
                    case RecurrenceEnum.First:
                    case RecurrenceEnum.Second:
                    case RecurrenceEnum.Third:
                    case RecurrenceEnum.Fourth:
                    case RecurrenceEnum.Last:
                        if (day.HasValue == false)
                            throw new Exception("Required value missing. Need day of the week!.");
                        //adding start and end of event.
                        duration = end - start;
                        //incase of recurring the ics rule requires the start date to be on the same weekday. Othewise the outlook calendar wil not accept the format.
                        //So moving the startdate and enddate to next avilable weekday as per 'day' parameter.
                        DateTime tmpstart = DateTimeHelper.GetFirstOccuringWeekDayFromMonth(start, day.Value, reccurrenceMode);
                        //if tmpstart date is before start date given then move start date to next month
                        start = tmpstart >= start ? tmpstart : DateTimeHelper.GetFirstOccuringWeekDayFromMonth(start.AddMonths(1), day.Value, reccurrenceMode);
                        //if tmpend date is after end date given then move start date to previous month
                        DateTime tmpend = DateTimeHelper.GetFirstOccuringWeekDayFromMonth(end, day.Value, reccurrenceMode);
                        end = tmpend <= end ? tmpend : DateTimeHelper.GetFirstOccuringWeekDayFromMonth(end.AddMonths(1), day.Value, reccurrenceMode);
                        endtime = start.AddHours(duration.Hours).AddMinutes(duration.Minutes).AddSeconds(duration.Seconds);
                        evnt.Start = new CalDateTime(start, TZID);
                        evnt.End = new CalDateTime(endtime, TZID);
                        recurrdays = DateTimeHelper.GenerateRecurringDaysList(start, end, reccurrenceMode, day);
                        rrule = new RecurrencePattern(FrequencyType.Monthly, 1);
                        rrule.Count = recurrdays.Count;
                        FrequencyOccurrence freq = reccurrenceMode == RecurrenceEnum.Last ? FrequencyOccurrence.Last : (FrequencyOccurrence)((int)reccurrenceMode);
                        rrule.ByDay.Add(new WeekDay(day.Value, freq));
                        evnt.RecurrenceRules = new List<RecurrencePattern> { rrule };
                        break;
                    case RecurrenceEnum.Weekly:
                        if (day.HasValue == false)
                            throw new Exception("Required value missing. Need day of the week!.");
                        //adding start and end of event.
                        duration = end - start;
                        //incase of recurring the ics rule requires the start date to be on the same weekday. Othewise the outlook calendar wil not accept the format.
                        //So moving the startdate and enddate to next avilable weekday as per 'day' parameter.
                        start = DateTimeHelper.GetOnOrAfterWeekday(start, day.Value);
                        end = DateTimeHelper.GetOnOrBeforeWeekday(end, day.Value);
                        endtime = start.AddHours(duration.Hours).AddMinutes(duration.Minutes).AddSeconds(duration.Seconds);
                        evnt.Start = new CalDateTime(start, TZID);
                        evnt.End = new CalDateTime(endtime, TZID);
                        rrule = new RecurrencePattern(FrequencyType.Weekly, 1);
                        rrule.ByDay.Add(new WeekDay(day.Value));
                        rrule.Until = end;
                        evnt.RecurrenceRules = new List<RecurrencePattern> { rrule };
                        break;
                    case RecurrenceEnum.Yearly://working good
                        //adding start and end of event.
                        duration = end - start;
                        endtime = start.AddHours(duration.Hours).AddMinutes(duration.Minutes).AddSeconds(duration.Seconds);
                        evnt.Start = new CalDateTime(start, TZID);
                        evnt.End = new CalDateTime(new DateTime(evnt.Start.Year, evnt.Start.Month, endtime.Day, endtime.Hour, endtime.Minute, endtime.Second), TZID);
                        //add recurring for eery year on starting date.
                        rrule = new RecurrencePattern(FrequencyType.Yearly, 1);
                        rrule.Until = end;
                        evnt.RecurrenceRules = new List<RecurrencePattern> { rrule };
                        break;
                    case RecurrenceEnum.Daily://working good
                        //adding start and end of event.
                        duration = end - start;
                        endtime = start.AddHours(duration.Hours).AddMinutes(duration.Minutes).AddSeconds(duration.Seconds);
                        evnt.Start = new CalDateTime(start, TZID);
                        evnt.End = new CalDateTime(new DateTime(evnt.Start.Year, evnt.Start.Month, endtime.Day, endtime.Hour, endtime.Minute, endtime.Second), TZID);
                        //add recurring event paramaters.
                        recurrdays = DateTimeHelper.GenerateRecurringDaysList(start, end, reccurrenceMode);
                        //Repeat daily for n days
                        rrule = new RecurrencePattern(FrequencyType.Daily);
                        rrule.Count = recurrdays.Count;
                        evnt.RecurrenceRules = new List<RecurrencePattern> { rrule };
                        break;
                }
                return ical;
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                return new Calendar();
            }
        }

        VTimeZone CreateESTVTimeZone()
        {
            VTimeZone ret = new VTimeZone("America/New_York");
            ret.AddProperty("X-LIC-LOCATION", "America/New_York");

            Todo daylight = new Todo();
            daylight.Name = Components.Daylight;
            daylight.DtStart = new CalDateTime(1970, 03, 08, 02, 00, 00);
            daylight.AddProperty("TZOFFSETFROM", "-0500");
            daylight.AddProperty("TZOFFSETTO", "-0400");
            daylight.AddProperty("TZNAME", "EDT");
            RecurrencePattern dlrp = new RecurrencePattern(FrequencyType.Yearly);
            dlrp.ByMonth.Add(3);
            dlrp.ByDay.Add(new WeekDay(DayOfWeek.Sunday, 2));
            daylight.RecurrenceRules.Add(dlrp);
            ret.Children.Add(daylight);

            Todo standard = new Todo();
            standard.Name = Components.Standard;
            standard.DtStart = new CalDateTime(1970, 11, 01, 02, 00, 00);
            standard.AddProperty("TZOFFSETFROM", "-0400");
            standard.AddProperty("TZOFFSETTO", "-0500");
            standard.AddProperty("TZNAME", "EST");
            RecurrencePattern strp = new RecurrencePattern(FrequencyType.Yearly);
            strp.ByMonth.Add(11);
            strp.ByDay.Add(new WeekDay(DayOfWeek.Sunday, 1));
            standard.RecurrenceRules.Add(strp);
            ret.Children.Add(standard);

            return ret;
        }

        public System.Net.Mail.Attachment GetAttachmentFromPlainText(string content, string fileName)
        {
            System.IO.MemoryStream m = new System.IO.MemoryStream(Encoding.ASCII.GetBytes(content));
            System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(m, fileName, "text/plain");
            return attach;
        }

        public bool SendPendingNotificationEmail(SendPendingEmailModel sendPendingEmailModel)
        {
            bool isSuccessful = false;
            try
            {
                bool success = false;
                MailMessage emailmessage = GetMailMessage();
                List<MailAddress> mailAddresses = GetEmailsFromUserIdList(sendPendingEmailModel.SendUserIds);
                emailmessage.Subject = "Project Estimation - " + sendPendingEmailModel.ProjectName + " set as " + sendPendingEmailModel.ProjectStatusDesc;

                foreach (MailAddress mailAddress in mailAddresses)
                {
                    emailmessage.To.Add(mailAddress);
                }

                MessageTemplateEngine messageTemplateEngine = new MessageTemplateEngine();
                //required fields
                messageTemplateEngine.ProjectId = sendPendingEmailModel.ProjectId;
                messageTemplateEngine.ProjectScheduleTypDesc = sendPendingEmailModel.IsPreliminaryMeeting ? "PMA" : sendPendingEmailModel.IsExpress ? "EMA" : "PR";
                //end required fields
                messageTemplateEngine.ProjectName = sendPendingEmailModel.ProjectName;
                messageTemplateEngine.PendingEstimationReason = sendPendingEmailModel.ProjectStatusDesc;
                messageTemplateEngine.PendingEstimationNotes = sendPendingEmailModel.PendingCommentsToCustomer;
                messageTemplateEngine.EstimatorName = sendPendingEmailModel.Usernamepublic;
                var easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                var timestamp = TimeZoneInfo.ConvertTimeFromUtc(sendPendingEmailModel.Timestamp, easternZone);
                messageTemplateEngine.Timestamp = timestamp.ToString();
                messageTemplateEngine.ProjectNumber = sendPendingEmailModel.AccelaProjectId;
                //switch types
                if (sendPendingEmailModel.IsPreliminaryMeeting)
                {
                    messageTemplateEngine.MessageTemplateTypeEnum = MessageTemplateTypeEnum.Pending_Preliminary_Estimation;

                }
                else
                {
                    messageTemplateEngine.MessageTemplateTypeEnum = MessageTemplateTypeEnum.Pending_Estimation;

                }
                string emailbody = messageTemplateEngine.BuildMessage();

                emailmessage.Body = emailbody;

                emailmessage.From = new MailAddress(MailUserName);

                string[] recipientList = MandatoryEmailRecipientList.Split(new Char[] { ';' });
                foreach (string s in recipientList)
                {
                    if (!String.IsNullOrWhiteSpace(s))
                        emailmessage.To.Add(new MailAddress(s));
                }

                isSuccessful = SendEmailMessage(emailmessage);

                //save this notification
                SendProjectNotification sendProjectNotification = new SendProjectNotification
                {
                    AccelaProjectId = sendPendingEmailModel.AccelaProjectId,
                    MailMessage = emailmessage,
                    SendDate = sendPendingEmailModel.Timestamp,
                    WrkId = sendPendingEmailModel.WrkId,
                    EmailNotif = BL.EmailNotifType.Pending_Estimation,
                    UserIds = sendPendingEmailModel.SendUserIds,
                    EmailTxts = new List<string>()
                };
                //get the default emails this goes to and add to emailtxt list
                foreach (string s in recipientList)
                {
                    if (!String.IsNullOrWhiteSpace(s))
                        sendProjectNotification.EmailTxts.Add(s);
                }
                int notifId = SaveNotificationEmail(sendProjectNotification);
                if (notifId > 0)
                {
                    sendProjectNotification.ProjectNotificationEmailId = notifId;
                    success = SaveNotificationEmailSendList(sendProjectNotification);
                }
            }

            catch (Exception ex)
            {
                string errorMessage = "EmailAdapter.SendPendingNotificationEmail Error:  " + ex.Message + "-" + DateTime.Now;
                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                isSuccessful = false;
            }

            return isSuccessful;
        }

        public bool SendNAEmail(MailMessage mailMessage)
        {
            bool isSuccessful = false;

            try
            {
                mailMessage.From = new MailAddress(MailUserName);

                //loop through our list and add each of our recipients 
                string[] recipientList = MandatoryEmailRecipientList.Split(new Char[] { ';' });
                foreach (string s in recipientList)
                {
                    if (!String.IsNullOrWhiteSpace(s))
                        mailMessage.To.Add(new MailAddress(s));
                }

                mailMessage.IsBodyHtml = true;

                isSuccessful = SendEmailMessage(mailMessage);
            }

            catch (Exception ex)
            {
                string errorMessage = "Email.Utility.SendNAEmail Error:  " + ex.Message + "-" + DateTime.Now;
                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                isSuccessful = false;

            }

            return isSuccessful;
        }

        public string CreateNAEmailMessageBody(string projectId, string projectName, string projectAddress)
        {
            try
            {
                string baseurl = "";
                //get the baseurl from catalog
                baseurl = new CatalogRefBO().GetByExternalRef("EMAIL").Where(x => x.Key == "EMAILENGINE.BASEURL").FirstOrDefault().Value;
                return new EmailMessageBO().CreateNAEmailMessageBody(projectId, projectName, projectAddress, baseurl);

            }
            catch (Exception ex)
            {

                string errorMessage = "Error Sending Email - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }

        public bool SendEmailMessage(System.Net.Mail.MailMessage mailMessage)
        {
            try
            {
                string mandatoryListOnly = new CatalogRefBO().GetByExternalRef("EMAIL").Where(x => x.Key == "EMAILENGINE.MANDATORYLIST.ACTIVE").FirstOrDefault().Value;
                if (mandatoryListOnly.Equals("Y", StringComparison.CurrentCultureIgnoreCase))
                {
                    //replace send to with mandatory list emails
                    mailMessage.To.Clear();
                    string[] recipientList = MandatoryEmailRecipientList.Split(new Char[] { ';' });
                    foreach (string s in recipientList)
                    {
                        if (!String.IsNullOrWhiteSpace(s))
                            mailMessage.To.Add(new MailAddress(s));
                    }

                }
                return new SendEmailBO().SendEmailMessage(mailMessage);
            }
            catch (Exception ex)
            {
                string errorMessage = $"Error SendingEmail - Message: {ex.Message} From: {mailMessage.From.ToString()} To: {mailMessage.To.ToString()} InnerException: {ex.InnerException}";

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }

        public MailMessage GetMailMessage()
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(MailUserName);

            string[] recipientList = MandatoryEmailRecipientList.Split(new Char[] { ';' });


            foreach (string s in recipientList)
            {
                if (!String.IsNullOrWhiteSpace(s))
                    mailMessage.To.Add(new MailAddress(s));
            }

            mailMessage.IsBodyHtml = true;
            return mailMessage;
        }

        public bool SendApptCancellation(RecurrenceEnum recurrence, DateTime startdate, DateTime enddate, List<AttendeeDetails> attendees,
            string title, string description, string location, DayOfWeek? dayofweek = null)
        {
            try
            {
                bool success = false;

                string errorMessage = string.Empty;

                errorMessage = "Log SendApptCancellation";

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                MailMessage mailMessage = GenerateMailMessage(attendees, title, description);

                Calendar cal = CreateCalendarEntry(startdate, enddate, recurrence, title, description, location, attendees,
                    CalendarRequestModeEnum.CreateForAttendees, dayofweek);

                if (cal.Events[0] == null)
                {
                    return success;
                }

                string uid = cal.Events[0].Uid;
                string retcreate = cal.ToIcalString();
                string serializedCalendar = CancelCalendarEntry(uid, startdate, enddate, recurrence, title, description, location,
                    attendees, dayofweek).ToIcalString();

                ContentType ctHtml = new ContentType("text/html");
                ctHtml.Parameters.Add("method", "REQUEST");
                AlternateView avHtml = AlternateView.CreateAlternateViewFromString(description, ctHtml);
                mailMessage.AlternateViews.Add(avHtml);

                var contype = new System.Net.Mime.ContentType("text/calendar");
                contype.Parameters.Add("method", "REQUEST");
                contype.Parameters.Add("name", "CalendarDelete.ics");
                AlternateView avCal = AlternateView.CreateAlternateViewFromString(serializedCalendar, contype);
                mailMessage.AlternateViews.Add(avCal);

                success = SendEmailMessage(mailMessage);

                errorMessage = "Log SendApptCancellation - success = " + success.ToString();

                var logging2 = Logger.LogMessageAsync(Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                if (IsRunningFromAzure)
                {
                    errorMessage = "Log SendApptCancellation - IsRunningFromAzure = true";
                }
                else
                {
                    string deleteics = @"C:\Email\CalendarDelete.ics";
                    File.AppendAllText(deleteics, serializedCalendar);
                }

                return success;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error SendApptCancellation - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }

        public bool SendAppt(RecurrenceEnum recurrence, DateTime startdate, DateTime enddate, List<AttendeeDetails> attendees,
            string title, string description, string location, DayOfWeek? dayofweek = null)
        {
            try
            {
                bool success = false;

                MailMessage mailMessage = GenerateMailMessage(attendees, title, description);

                string serializedCalendar = CreateCalendarEntry(startdate, enddate, recurrence, title, description, location, attendees,
                    CalendarRequestModeEnum.CreateForAttendees, dayofweek).ToIcalString();

                ContentType ctHtml = new ContentType("text/html");
                ctHtml.Parameters.Add("method", "REQUEST");
                AlternateView avHtml = AlternateView.CreateAlternateViewFromString(description, ctHtml);
                mailMessage.AlternateViews.Add(avHtml);

                ContentType ctCalendar = new ContentType("text/calendar");
                ctCalendar.Parameters.Add("method", "REQUEST");
                AlternateView avCal = AlternateView.CreateAlternateViewFromString(serializedCalendar, ctCalendar);
                mailMessage.AlternateViews.Add(avCal);

                if (IsRunningFromAzure)
                {
                    string errorMessage = "Log SendAppt - IsRunningFromAzure = true";
                    var logging = Logger.LogMessageAsync(Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), errorMessage,
                        string.Empty, string.Empty, string.Empty);
                }
                else
                {
                    string icsfilepath = @"C:\Email\Calendar.ics";
                    File.AppendAllText(icsfilepath, serializedCalendar);
                }

                success = SendEmailMessage(mailMessage);

                return success;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error SendAppt - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }

        private MailMessage GenerateMailMessage(List<AttendeeDetails> attendees, string title, string description)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(MailUserName);

            foreach (var item in attendees)
            {
                if (!String.IsNullOrWhiteSpace(item.EmailId) && item.EmailId.Contains("@"))
                    mailMessage.To.Add(new MailAddress(item.EmailId));
            }
            mailMessage.Subject = title;
            mailMessage.Body = description;

            return mailMessage;
        }

        public bool SendPrelimMeetingScheduledEmail(MailMessage mailMessage)
        {
            bool isSuccessful = false;
            try
            {
                mailMessage.From = new MailAddress(MailUserName);

                string[] recipientList = MandatoryEmailRecipientList.Split(new Char[] { ';' });
                foreach (string s in recipientList)
                {
                    if (!String.IsNullOrWhiteSpace(s))
                        mailMessage.To.Add(new MailAddress(s));
                }

                mailMessage.IsBodyHtml = true;
                isSuccessful = SendEmailMessage(mailMessage);

            }

            catch (Exception ex)
            {
                string errorMessage = "EmailAdapter.SendPrelimMeetingScheduledEmail Error:  " + ex.Message + "-" + DateTime.Now;
                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                isSuccessful = false;
            }

            return isSuccessful;
        }

        public string CreatePrelimMeetingScheduledEmailBody(string projectId, string projectName, string projectAddress,
            DateTime meetingDate, string meetingRoom, string notes, string projectcoordname = "", string projectcoordphone = ""
            , string projectcoordemail = "", Models.ProjectEstimation projectEstimation = null)
        {
            try
            {
                //Prelim accept/reject email
                MessageTemplateEngine mte = new MessageTemplateEngine();
                EmailAdapter emailAdapter = new EmailAdapter();
                mte.ProjectNumber = projectId;
                mte.ProjectName = projectName;
                mte.ProjectAddress = projectAddress;
                mte.MeetingDate = meetingDate.ToShortDateString();
                mte.MeetingTime = meetingDate.ToShortTimeString();
                mte.MeetingRoom = meetingRoom;
                mte.FacilitatorEmail = projectcoordemail;
                mte.FacilitatorName = projectcoordname;
                mte.FacilitatorPhone = projectcoordphone;
                mte.ProjectScheduleTypDesc = "PMA";
                mte.MeetingTypRefId = null;
                mte.Notes = notes;
                mte.Project = projectEstimation;
                mte.MessageTemplateTypeEnum = MessageTemplateTypeEnum.Preliminary_Meeting_AcceptReject_Email;

                return mte.BuildMessage();

            }
            catch (Exception ex)
            {

                string errorMessage = "Error Sending Email - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }

        public string CancelPrelimMeetingScheduledEmailBody(string projectId, string projectName, string projectAddress, DateTime meetingDate)
        {
            try
            {
                return new EmailMessageBO().CancelPrelimMeetingScheduledMessageBody(projectId, projectName, projectAddress, meetingDate);

            }
            catch (Exception ex)
            {

                string errorMessage = "Error Sending Email - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }

        public int SaveNotificationEmail(SendProjectNotification item)
        {
            int id = 0;
            try
            {

                if (!string.IsNullOrWhiteSpace(item.AccelaProjectId) && item.ProjectId == 0)
                {
                    //get the project id
                    ProjectEstimationAdapter projectEstimationAdapter = new ProjectEstimationAdapter();
                    item.ProjectId = projectEstimationAdapter.GetAIONProjectId(item.AccelaProjectId);
                }
                if (item.ProjectId == 0)
                {
                    string accelaid = string.IsNullOrWhiteSpace(item.AccelaProjectId) ? "AccelaProjectId is blank" : item.AccelaProjectId;
                    string errorMessage = "Error SaveNotificationEmail - Project ID is required - AccelaProjectId: " + accelaid;
                    var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                        string.Empty, string.Empty, string.Empty);
                    throw new Exception(errorMessage);
                }
                ProjectEmailNotificationBO bo = new ProjectEmailNotificationBO();
                ProjectEmailNotificationBE be = new ProjectEmailNotificationBE
                {
                    ProjectId = item.ProjectId,
                    EmailSubjectText = item.MailMessage.Subject.ToString(),
                    EmailBodyTxt = item.MailMessage.Body.ToString(),
                    SenderUserId = item.WrkId,
                    EmailSentDt = item.SendDate,
                    EmailTypeDesc = item.EmailNotif.ToString(),
                    UserId = item.WrkId.ToString()
                };
                id = bo.Create(be);

            }
            catch (Exception ex)
            {
                string errorMessage = "Error SaveNotificationEmail - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
            return id;
        }

        public bool SaveNotificationEmailSendList(SendProjectNotification item)
        {
            bool success = false;
            try
            {
                NotificationEmailListBO bo = new NotificationEmailListBO();
                NotificationEmailListBE be = new NotificationEmailListBE();
                int newid = 0;
                if (item.UserIds != null)
                    foreach (int id in item.UserIds)
                    {
                        be = new NotificationEmailListBE
                        {
                            SendUserId = id,
                            ProjectEmailNotificationId = item.ProjectNotificationEmailId,
                            UserId = item.WrkId.ToString()
                        };
                        newid = bo.Create(be);

                    }
                if (item.EmailTxts != null)
                    foreach (string email in item.EmailTxts)
                    {
                        be = new NotificationEmailListBE
                        {
                            EmailAddressTxt = email,
                            ProjectEmailNotificationId = item.ProjectNotificationEmailId,
                            UserId = item.WrkId.ToString()
                        };
                        newid = bo.Create(be);
                    }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error SaveNotificationEmailSendList - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
            return success;
        }

        public List<ProjectNotificationEmail> GetProjectNotificationEmails(int projectId)
        {
            List<ProjectNotificationEmail> emails = new List<ProjectNotificationEmail>();

            try
            {
                ProjectEmailNotificationBO bo = new ProjectEmailNotificationBO();
                NotificationEmailListBO emailListBO = new NotificationEmailListBO();
                List<ProjectEmailNotificationBE> emaillist = bo.GetList(projectId);
                foreach (ProjectEmailNotificationBE be in emaillist)
                {
                    List<NotificationEmailListBE> emailListBEs = emailListBO.GetList(be.ProjectEmailNotificationId.Value);
                    ProjectNotificationEmail projectNotificationEmail = new ProjectNotificationEmail
                    {
                        EmailBodyTxt = be.EmailBodyTxt,
                        EmailSentDt = be.EmailSentDt,
                        EmailSubjectText = be.EmailSubjectText,
                        EmailTypeDesc = be.EmailTypeDesc,
                        ProjectEmailNotificationId = be.ProjectEmailNotificationId,
                        ProjectId = be.ProjectId,
                        SenderUserId = be.SenderUserId,
                        NotificationEmailList = new List<NotificationEmailList>()
                    };
                    //map the notificationlist
                    foreach (NotificationEmailListBE email in emailListBEs)
                    {
                        UserIdentity userBE = new UserIdentity();
                        if (email.SendUserId.HasValue && email.SendUserId.Value > 0)
                        {
                            userBE = new UserIdentityModelBO().GetInstance(email.SendUserId.Value);
                        }

                        projectNotificationEmail.NotificationEmailList.Add(new NotificationEmailList
                        {
                            EmailAddressTxt = email.EmailAddressTxt,
                            NotificationEmailListId = email.NotificationEmailListId,
                            ProjectEmailNotificationId = email.ProjectEmailNotificationId,
                            SendUserId = email.SendUserId,
                            FirstName = userBE.FirstName,
                            LastName = userBE.LastName
                        });

                    }
                    emails.Add(projectNotificationEmail);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error GetProjectNotificationEmails - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
            return emails;
        }

        public bool ResendProjectNotification(SendProjectNotification resendProjectNotification)
        {
            bool success = false;
            try
            {
                //get the email
                //get the emails
                ProjectEmailNotificationBO emailNotifBO = new ProjectEmailNotificationBO();
                NotificationEmailListBO notifListBO = new NotificationEmailListBO();
                UserIdentityModelBO userBO = new UserIdentityModelBO();
                ProjectEmailNotificationBE emailNotif = emailNotifBO.GetById(resendProjectNotification.ProjectNotificationEmailId);
                MailMessage mailMessage = GetMailMessage();
                mailMessage.Body = emailNotif.EmailBodyTxt;
                mailMessage.Subject = emailNotif.EmailSubjectText;
                if (resendProjectNotification.SendToAll)
                {
                    List<NotificationEmailListBE> emailList = notifListBO.GetList(resendProjectNotification.ProjectNotificationEmailId);
                    foreach (NotificationEmailListBE email in emailList)
                    {
                        if (email.SendUserId != null && email.SendUserId != 0)
                        {
                            UserIdentity user = userBO.GetInstance(email.SendUserId.Value);
                            if (!string.IsNullOrWhiteSpace(user.Email))
                                mailMessage.To.Add(new MailAddress(user.Email));
                        }

                        if (!string.IsNullOrWhiteSpace(email.EmailAddressTxt))
                        {
                            mailMessage.To.Add(new MailAddress(email.EmailAddressTxt));
                        }
                    }
                }
                else
                {
                    //use the lists in the object
                    foreach (int id in resendProjectNotification.UserIds)
                    {
                        UserIdentity user = userBO.GetInstance(id);
                        if (!string.IsNullOrWhiteSpace(user.Email))
                            mailMessage.To.Add(new MailAddress(user.Email));
                    }

                    foreach (string email in resendProjectNotification.EmailTxts)
                    {
                        if (!string.IsNullOrWhiteSpace(email) && RegexUtilities.IsValidEmail(email))
                        {
                            mailMessage.To.Add(new MailAddress(email));
                        }
                    }
                }
                //send the notification
                success = SendEmailMessage(mailMessage);

            }
            catch (Exception ex)
            {
                string errorMessage = "Error ResendProjectNotification - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            return success;
        }

        public List<MailAddress> GetEmailsFromUserIdList(List<int> userids)
        {
            try
            {

                List<MailAddress> mailAddresses = new List<MailAddress>();
                foreach (int userid in userids)
                {
                    UserIdentity user = new UserIdentityModelBO().GetInstance(userid);
                    if (user != null && user.ID > 0 && !string.IsNullOrWhiteSpace(user.Email))
                    {
                        mailAddresses.Add(new MailAddress(user.Email));
                    }
                }
                return mailAddresses;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error GetEmailsFromUserIdList - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }

        public bool SendNewProjectManagerToAdmin(string pmname, string pmemail, string accelaprojectnum)
        {
            bool isSuccessful = false;
            try
            {
                string emailBody = string.Empty;

                //get all the users with role ITS Support Group
                int systemroleid = new SystemRoleModelBO().BaseList.Where(x => x.SrcSystemValTxt == "ITS_Support_Group").FirstOrDefault().ID;
                List<UserBE> userBEs = new UserBO().GetListBySystemRoleID(systemroleid);

                //get the template
                //get the active template by template type id
                MessageTemplateBE messageTemplateBE = new MessageTemplateBO().GetActiveByTypeId((int)MessageTemplateTypeEnum.New_Project_Manager_Email, DateTime.Now);
                string messageTemplate = messageTemplateBE.TemplateText;

                Dictionary<string, string> dataElements = new Dictionary<string, string>();
                dataElements.Add("[[PROJECT NUMBER]]", accelaprojectnum);
                dataElements.Add("[[PROJECT MANAGER EMAIL]]", pmemail);
                dataElements.Add("[[PROJECT MANAGER NAME]]", pmname);

                //Use StringBuilder here because it uses less memory
                StringBuilder html = new StringBuilder();

                html.Append(messageTemplate);

                foreach (KeyValuePair<string, string> dataelement in dataElements)
                {
                    //replace with the value in the correct property
                    html.Replace(dataelement.Key, dataelement.Value);

                }

                emailBody += html.ToString();

                //init mail message
                MailMessage mailMessage = GetMailMessage();
                mailMessage.Subject = "New Project Manager - Aion";
                mailMessage.Body = emailBody;

                foreach (UserBE s in userBEs)
                {
                    if (!String.IsNullOrWhiteSpace(s.SrcSystemValueTxt))
                        mailMessage.To.Add(new MailAddress(s.SrcSystemValueTxt));
                }

                isSuccessful = SendEmailMessage(mailMessage);

            }

            catch (Exception ex)
            {
                string errorMessage = "EmailAdapter.SendNewProjectManagerToAdmin Error:  " + ex.Message + "-" + DateTime.Now;
                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                isSuccessful = false;
            }

            return isSuccessful;
        }
        public bool SendUpdateProjectManagerToAdmin(string pmname, string pmemail, string accelaprojectnum)
        {
            bool isSuccessful = false;
            try
            {
                string emailBody = string.Empty;

                //get all the users with role ITS Support Group
                int systemroleid = new SystemRoleModelBO().BaseList.Where(x => x.SrcSystemValTxt == "ITS_Support_Group").FirstOrDefault().ID;
                List<UserBE> userBEs = new UserBO().GetListBySystemRoleID(systemroleid);

                //get the template
                //get the active template by template type id
                MessageTemplateBE messageTemplateBE = new MessageTemplateBO().GetActiveByTypeId((int)MessageTemplateTypeEnum.Change_project_Manager_email, DateTime.Now);
                string messageTemplate = messageTemplateBE.TemplateText;

                Dictionary<string, string> dataElements = new Dictionary<string, string>();
                dataElements.Add("[[PROJECT NUMBER]]", accelaprojectnum);
                dataElements.Add("[[PROJECT MANAGER EMAIL]]", pmemail);
                dataElements.Add("[[PROJECT MANAGER NAME]]", pmname);

                //Use StringBuilder here because it uses less memory
                StringBuilder html = new StringBuilder();

                html.Append(messageTemplate);

                foreach (KeyValuePair<string, string> dataelement in dataElements)
                {
                    //replace with the value in the correct property
                    html.Replace(dataelement.Key, dataelement.Value);

                }

                emailBody += html.ToString();
                //init mail message
                MailMessage mailMessage = GetMailMessage();
                mailMessage.Subject = "Changed Project Manager - Aion";
                mailMessage.Body = emailBody;

                foreach (UserBE s in userBEs)
                {
                    if (!String.IsNullOrWhiteSpace(s.SrcSystemValueTxt))
                        mailMessage.To.Add(new MailAddress(s.SrcSystemValueTxt));
                }

                isSuccessful = SendEmailMessage(mailMessage);

            }

            catch (Exception ex)
            {
                string errorMessage = "EmailAdapter.SendNewProjectManagerToAdmin Error:  " + ex.Message + "-" + DateTime.Now;
                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                isSuccessful = false;
            }

            return isSuccessful;
        }
        public string CreateExpressScheduledMessageBody(string projectnumber, string projectname, string projectaddress, DateTime meetingDateTime,
          string meetingroom, string projectcoordname, string projectcoordphone, string projectcoordemail)
        {
            string emailbody = string.Empty;
            try
            {
                MessageTemplateEngine mte = new MessageTemplateEngine();
                mte.ProjectNumber = projectnumber;
                mte.MessageTemplateTypeEnum = MessageTemplateTypeEnum.Express_Tentative_Scheduled_Email;
                mte.ProjectScheduleTypDesc = "EMA";
                mte.ProjectName = projectname;
                mte.ProjectAddress = projectaddress;
                mte.MeetingDate = meetingDateTime.ToShortDateString();
                mte.MeetingTime = meetingDateTime.ToShortTimeString();
                mte.MeetingRoom = meetingroom;
                mte.FacilitatorEmail = projectcoordemail;
                mte.FacilitatorName = projectcoordname;
                mte.FacilitatorPhone = projectcoordphone;
                mte.MeetingTypRefId = null;
                mte.Project = new EstimationCRUDAdapter().GetProjectDetailsByProjectSrcSourceTxt(projectnumber);
                emailbody = mte.BuildMessage();

            }
            catch (Exception ex)
            {
                string errorMessage = "EmailAdapter.CreateExpressScheduledMessageBody Error:  " + ex.Message + "-" + DateTime.Now;
                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
            }
            return emailbody;
        }

        public string CreateMeetingScheduledMessageBody(string projectnumber, string projectname, string projectaddress, DateTime meetingDateTime,
          string meetingroom, MeetingTypeEnum meetingtype, string projectcoordname, string projectcoordphone, string projectcoordemail)
        {
            string emailbody = string.Empty;
            try
            {
                MessageTemplateEngine mte = new MessageTemplateEngine();
                mte.ProjectNumber = projectnumber;
                mte.MessageTemplateTypeEnum = MessageTemplateTypeEnum.Meeting_AcceptReject_email;
                mte.ProjectScheduleTypDesc = "FMA";
                mte.ProjectName = projectname;
                mte.ProjectAddress = projectaddress;
                mte.MeetingDate = meetingDateTime.ToShortDateString();
                mte.MeetingTime = meetingDateTime.ToShortTimeString();
                mte.MeetingRoom = meetingroom;
                mte.MeetingName = meetingtype.ToStringValue();
                mte.FacilitatorEmail = projectcoordemail;
                mte.FacilitatorName = projectcoordname;
                mte.FacilitatorPhone = projectcoordphone;
                mte.MeetingTypeEnum = meetingtype;
                mte.MeetingTypRefId = null;

                emailbody = mte.BuildMessage();

            }
            catch (Exception ex)
            {
                string errorMessage = "EmailAdapter.CreateMeetingScheduledMessageBody Error:  " + ex.Message + "-" + DateTime.Now;
                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
            }
            return emailbody;

        }

        public bool SendAccelaIntegrationFailure(List<AccelaFailure> accelaFailures)
        {
            try
            {
                if (SyncFailureNotificationsSentInPastHour() > 0) { return true; }

                TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

                string emailBody = "";
                //get the email addresses from catalog
                //get the template
                //get the active template by template type id
                MessageTemplateBE messageTemplateBE = new MessageTemplateBO().GetActiveByTypeId((int)MessageTemplateTypeEnum.Accela_Integration_Failure, DateTime.Now);
                string messageTemplate = messageTemplateBE.TemplateText;
                //for each failure, build the block from the template
                foreach (AccelaFailure failure in accelaFailures)
                {
                    DateTime failureTimeStamp = string.IsNullOrWhiteSpace(failure.TimeStamp) ? DateTime.UtcNow : DateTime.Parse(failure.TimeStamp);

                    Dictionary<string, string> dataElements = new Dictionary<string, string>();
                    dataElements.Add("[[FAILURE TYPE]]", failure.FailureType);
                    dataElements.Add("[[ACCELA ENVIRONMENT]]", failure.AccelaEnvironment);
                    dataElements.Add("[[RECORD ID]]", failure.RecordId);
                    dataElements.Add("[[PROJECT NUMBER]]", failure.ProjectNumber);
                    dataElements.Add("[[TIMESTAMP]]", TimeZoneInfo.ConvertTimeFromUtc(failureTimeStamp, easternZone).ToString());
                    dataElements.Add("[[ERROR MESSAGE]]", failure.Message);
                    //Use StringBuilder here because it uses less memory
                    StringBuilder html = new StringBuilder();

                    html.Append(messageTemplate);

                    foreach (KeyValuePair<string, string> dataelement in dataElements)
                    {
                        //replace with the value in the correct property
                        html.Replace(dataelement.Key, dataelement.Value);

                    }

                    emailBody += html.ToString();
                }
                //emails
                string emails = new CatalogRefBO().GetByExternalRef("EMAIL").Where(x => x.Key == "EMAILENGINE.ACCELAFAILURE").FirstOrDefault().Value;
                //construct the base email
                MailMessage mailMessage = GetMailMessage();
                mailMessage.Subject = "Accela to AION Integration Failure - Environment: " + AccelaEnvironment;
                //add addresses to mailto
                foreach (string email in emails.Split(';'))
                {
                    if (!String.IsNullOrWhiteSpace(email))
                        mailMessage.To.Add(email);
                }
                //add template to body
                mailMessage.Body = emailBody;
                //send email
                bool success = SendEmailMessage(mailMessage);
                if (success)
                {
                    SyncFailureNotificationBE syncFailureBE = new SyncFailureNotificationBE()
                    {
                        LastFailureNotificationDt = DateTime.Now
                    };

                    SyncFailureNotificationBO syncFailureBO = new SyncFailureNotificationBO();
                    syncFailureBO.Create(syncFailureBE);
                }

                return success;
            }
            catch (Exception ex)
            {

                string errorMessage = "EmailAdapter.SendAccelaIntegrationFailure Error:  " + ex.Message + "-" + DateTime.Now;
                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
            }

            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="csvEmails">string separated by ; </param>
        /// <param name="readyDt"></param>
        /// <returns></returns>
        public bool SendSchedulingLeadTimeReportDataAvailable(string csvEmails, DateTime readyDt)
        {
            try
            {
                TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc(readyDt, easternZone);

                string emailBody = "";
                //get the email addresses from catalog
                //get the template
                //get the active template by template type id
                MessageTemplateBE messageTemplateBE = new MessageTemplateBO().GetActiveByTypeId((int)MessageTemplateTypeEnum.Scheduling_LeadTimeReport_Data_Available, DateTime.Now);
                string messageTemplate = messageTemplateBE.TemplateText;
                //for each failure, build the block from the template

                Dictionary<string, string> dataElements = new Dictionary<string, string>();
                dataElements.Add("[[MEETING DATE]]", easternTime.Date.ToShortDateString());
                dataElements.Add("[[MEETING TIME]]", easternTime.ToShortTimeString());
                //Use StringBuilder here because it uses less memory
                StringBuilder html = new StringBuilder();

                html.Append(messageTemplate);

                foreach (KeyValuePair<string, string> dataelement in dataElements)
                {
                    //replace with the value in the correct property
                    html.Replace(dataelement.Key, dataelement.Value);

                }

                emailBody += html.ToString();

                //emails
                string emails = csvEmails;
                //construct the base email
                MailMessage mailMessage = GetMailMessage();
                mailMessage.Subject = MessageTemplateTypeEnum.Scheduling_LeadTimeReport_Data_Available.ToStringValue();
                //add addresses to mailto
                foreach (string email in emails.Split(';'))
                {
                    if (!String.IsNullOrWhiteSpace(email))
                        mailMessage.To.Add(email);
                }
                //add template to body
                mailMessage.Body = emailBody;
                //send email
                return SendEmailMessage(mailMessage);

            }
            catch (Exception ex)
            {

                string errorMessage = "EmailAdapter.SendSchedulingLeadTimeReportDataAvailable Error:  " + ex.Message + "-" + DateTime.Now;
                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
            }

            return true;
        }
        public bool SendFunctionAdapterSyncFailure(Failure failure)
        {
            try
            {
                if (SyncFailureNotificationsSentInPastHour() > 0) { return true; }

                string emailBody = "";
                //get the email addresses from catalog
                //get the template
                //get the active template by template type id
                MessageTemplateBE messageTemplateBE = new MessageTemplateBO().GetActiveByTypeId((int)MessageTemplateTypeEnum.Function_Adapter_Sync_Failure, DateTime.Now);
                string messageTemplate = messageTemplateBE.TemplateText;
                //for each failure, build the block from the template

                Dictionary<string, string> dataElements = new Dictionary<string, string>();
                dataElements.Add("[[FAILURE TYPE]]", failure.FailureType);
                dataElements.Add("[[ENVIRONMENT]]", failure.Environment);
                dataElements.Add("[[TIMESTAMP]]", failure.TimeStamp);
                dataElements.Add("[[ERROR MESSAGE]]", failure.Message);
                //Use StringBuilder here because it uses less memory
                StringBuilder html = new StringBuilder();

                html.Append(messageTemplate);

                foreach (KeyValuePair<string, string> dataelement in dataElements)
                {
                    //replace with the value in the correct property
                    html.Replace(dataelement.Key, dataelement.Value);

                }

                emailBody += html.ToString();

                string emails = new CatalogRefBO().GetByExternalRef("EMAIL").Where(x => x.Key == "EMAILENGINE.FAILURE").FirstOrDefault().Value;
                //construct the base email
                MailMessage mailMessage = GetMailMessage();
                mailMessage.Subject = "AION Function Adapter Sync Failure - Environment: " + AccelaEnvironment;
                //add addresses to mailto
                foreach (string email in emails.Split(';'))
                {
                    if (!String.IsNullOrWhiteSpace(email))
                        mailMessage.To.Add(email);
                }
                //add template to body
                mailMessage.Body = emailBody;
                //send email
                bool success = SendEmailMessage(mailMessage);
                if (success)
                {
                    DateTime readyDt = DateTime.UtcNow;
                    TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                    DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc(readyDt, easternZone);

                    SyncFailureNotificationBE syncFailureBE = new SyncFailureNotificationBE()
                    {

                        LastFailureNotificationDt = easternTime
                    };

                    SyncFailureNotificationBO syncFailureBO = new SyncFailureNotificationBO();
                    syncFailureBO.Create(syncFailureBE);
                }

                return success;

            }
            catch (Exception ex)
            {

                string errorMessage = "EmailAdapter.SendAccelaIntegrationFailure Error:  " + ex.Message + "-" + DateTime.Now;
                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
            }

            return true;
        }

        private int SyncFailureNotificationsSentInPastHour()
        {
            SyncFailureNotificationBO syncFailureBO = new SyncFailureNotificationBO();
            List<SyncFailureNotificationBE> syncFailureNotificationBEs = syncFailureBO.GetList();

            return syncFailureNotificationBEs.Count();
        }
    }

    public interface IEmailAdapter
    {
        /// <summary>
        /// Use Email engine to send email message
        /// </summary>
        /// <param name="mailMessage"></param>
        /// <returns></returns>
        bool SendEmailMessage(MailMessage mailMessage);
        /// <summary>
        /// Use Email Engine to send PEnding notification
        /// </summary>
        /// <param name="mailMessage"></param>
        /// <returns></returns>
        bool SendPendingNotificationEmail(SendPendingEmailModel sendPendingEmailModel);

        /// <summary>
        /// Use Email Engine to send NA email notification
        /// </summary>
        /// <param name="mailMessage"></param>
        /// <returns></returns>
        bool SendNAEmail(MailMessage mailMessage);

        /// <summary>
        /// Create Mail Message for NA Notification
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="projectName"></param>
        /// <param name="projectAddress"></param>
        /// <returns></returns>
        string CreateNAEmailMessageBody(string projectId, string projectName, string projectAddress);
        /// <summary>
        /// Create and Send Appt Cancellation
        /// </summary>
        /// <param name="recurrence"></param>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="attendees"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="location"></param>
        /// <param name="dayofweek"></param>
        /// <returns></returns>
        bool SendApptCancellation(RecurrenceEnum recurrence, DateTime startdate, DateTime enddate, List<AttendeeDetails> attendees,
            string title, string description, string location, DayOfWeek? dayofweek = null);
        /// <summary>
        /// Create and Send Appt
        /// </summary>
        /// <param name="recurrence"></param>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="attendees"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="location"></param>
        /// <param name="dayofweek"></param>
        /// <returns></returns>
        bool SendAppt(RecurrenceEnum recurrence, DateTime startdate, DateTime enddate, List<AttendeeDetails> attendees,
            string title, string description, string location, DayOfWeek? dayofweek = null);

        string CreatePrelimMeetingScheduledEmailBody(string projectId, string projectName, string projectAddress,
            DateTime meetingDate, string meetingRoom, string notes, string projectcoordname = "", string projectcoordphone = ""
            , string projectcoordemail = "", Models.ProjectEstimation projectEstimation = null);

        string CancelPrelimMeetingScheduledEmailBody(string projectId, string projectName, string projectAddress, DateTime meetingDate);
        bool SendPrelimMeetingScheduledEmail(MailMessage mailMessage);

        /// <summary>
        /// Save a notification email
        /// </summary>
        /// <param name="item"></param>
        /// <returns>db id of item</returns>
        int SaveNotificationEmail(SendProjectNotification item);

        /// <summary>
        /// Save list of emails/users for a notification
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool SaveNotificationEmailSendList(SendProjectNotification item);

        /// <summary>
        /// Get the list of notifications by AION Project Id
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        List<ProjectNotificationEmail> GetProjectNotificationEmails(int projectId);

        /// <summary>
        /// resend Notifications
        /// </summary>
        /// <param name="projectNotificationEmailId"></param>
        /// <param name="userids"></param>
        /// <param name="emailtxts"></param>
        /// <param name="wrkid"></param>
        /// <returns></returns>
        bool ResendProjectNotification(SendProjectNotification resendProjectNotification);

        /// <summary>
        /// Gets default mail message with default emails from the mandatory recipient list and From value.
        /// </summary>
        /// <returns></returns>
        MailMessage GetMailMessage();

        /// <summary>
        /// Get a MailAddress list from a list of user ids
        /// </summary>
        /// <param name="userids"></param>
        /// <returns></returns>
        List<MailAddress> GetEmailsFromUserIdList(List<int> userids);

        /// <summary>
        /// Send the email to Project Manager Admins for a new project manager
        /// </summary>
        /// <param name="pmname"></param>
        /// <param name="pmemail"></param>
        /// <param name="pmphone"></param>
        /// <param name="accelaprojectnum"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        bool SendNewProjectManagerToAdmin(string pmname, string pmemail, string accelaprojectnum);
        string CreateMeetingScheduledMessageBody(string projectnumber, string projectname, string projectaddress, DateTime meetingDateTime,
          string meetingroom, MeetingTypeEnum meetingtype, string projectcoordname, string projectcoordphone, string projectcoordemail);
    }



    public enum CalendarRequestModeEnum
    {
        CreateForOrganizer = 0,
        CreateForAttendees = 1,
        CancelAppointment = 2
    }

    public static class Extensions
    {
        public static string ToIcalString(this Calendar iCal)
        {
            return new CalendarSerializer().SerializeToString(iCal);
        }
    }
}
