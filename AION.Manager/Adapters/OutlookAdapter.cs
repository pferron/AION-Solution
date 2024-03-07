using AION.Base.MSGraph;
using AION.BL.Models;
using AION.Engine.BusinessObjects;
using AION.Manager.Adapters;
using AION.Manager.Models;
using AION.Manager.Models.Outlook;
using Microsoft.Graph.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace AION.BL.Adapters
{
    public class OutlookAdapter
    {
        // Load configuration settings from PrivateSettings.config
        private static string appId = ConfigurationManager.AppSettings["ida:ClientId"];
        private static string appSecret = ConfigurationManager.AppSettings["ida:Password"];
        private static string tenantid = ConfigurationManager.AppSettings["ida:TenantId"];
        private static string applicationUserGuid = ConfigurationManager.AppSettings["ida:ApplicationUserId"];
        private static string MandatoryEmailRecipientList = GetMandatoryEmailRecipientList();
        private static string PrincipalEmailAddress = "permitplanreview@mecklenburgcountync.gov";

        static string GetMandatoryEmailRecipientList()
        {
            return new CatalogRefBO().GetByExternalRef("EMAIL").Where(x => x.Key == "EMAILENGINE.MANDATORYLIST").FirstOrDefault().Value;

        }

        public MeetingAllocationResponse CheckForMeetingAllocationAvailability(MeetingAllocationRequest data)
        {
            var estTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            MeetingAllocationResponse ret = new MeetingAllocationResponse();
            GetScheduleResultSetRoot apiResult = GetMeetingAllocationAvailabilityFromOutlookGraph(data);
            foreach (var item in data.RequestedParticipantEmailList)
            {
                var participantdata = apiResult.value.Where(x => (x != null ? x.scheduleId.ToLower() == item.ToLower() : false)).FirstOrDefault();
                if (participantdata != null && participantdata.scheduleItems != null && participantdata.scheduleItems.Count > 0)
                {
                    ret.AllocatedParticipantList.Add(item);
                    foreach (var subitem in participantdata.scheduleItems)
                    {
                        MeetingAllocations m = new MeetingAllocations();
                        m.StartTime = m.EndTime = TimeZoneInfo.ConvertTime(subitem.start.dateTime, TimeZoneInfo.FindSystemTimeZoneById(subitem.start.timeZone), estTimeZone);
                        m.EndTime = TimeZoneInfo.ConvertTime(subitem.end.dateTime, TimeZoneInfo.FindSystemTimeZoneById(subitem.end.timeZone), estTimeZone);
                        m.TimeZone = estTimeZone.StandardName;
                        m.Subject = subitem.subject;
                        m.LocationName = subitem.location;
                        m.ParticipantEmail = item;
                        ret.AllocatedMeetings.Add(m);
                    }
                }
                //else do not add since that person is not allocated to any meetings at this time.
            }
            return ret;
        }

        public List<MeetingRoom> GetAllAvailableMeetingRoomsForDateRange(DateTime selDate, DateTime selStartTime, DateTime selEndTime, string meetingType)
        {
            // SchedulePreliminaryMeetingViewModel vm = new SchedulePreliminaryMeetingViewModel();
            IMeetingRoomAdapter meetingRoomAdapter = new MeetingRoomAdapter();
            List<MeetingRoom> rooms = meetingRoomAdapter.GetMeetingRooms(true, meetingType);
            MeetingAllocationRequest data = new MeetingAllocationRequest();
            List<MeetingRoom> MeetingRoomList = new List<MeetingRoom>();
            foreach (var item in rooms)
            {
                data.RequestedParticipantEmailList.Add(item.MeetingRoomEmail);
            }
            data.RequestedStartTime = selDate.Date.AddHours(selStartTime.Hour).AddMinutes(selStartTime.Minute);
            data.RequestedEndTime = selDate.Date.AddHours(selEndTime.Hour).AddMinutes(selEndTime.Minute);
            var ret = CheckForMeetingAllocationAvailability(data);
            if (ret != null && ret.AllocatedParticipantList != null && ret.AllocatedParticipantList.Count > 0)
            {
                //vm.MeetingRoomList = new List<MeetingRoom>();
                foreach (var room in rooms)  // all meeting rooms
                {
                    bool allocated = false;
                    if (ret.AllocatedParticipantList.Any(x => x.ToUpper() == room.MeetingRoomEmail.ToUpper()) == false) //allocatedPList =Matthews,Marshall,Hoffman,Rue-Woods
                    {
                        MeetingRoomList.Add(room); //MintHill,PineVille,Huntersville,Granite,Dogwood,Cardinal
                        continue;
                    }
                    var alc = ret.AllocatedMeetings.Where(x => x.ParticipantEmail == room.MeetingRoomEmail);
                    foreach (var item in alc)
                    {
                        // If meeting allocation shows that the start of new meeting is same as end of allocated meeting then it 
                        // means it can be allocated upto that time since there is no allocation after that point.
                        if (data.RequestedStartTime == item.EndTime)
                            continue;
                        // If meeting allocation shows that the end of new meeting is same as start of allocated meeting then it 
                        // means it can be allocated upto that time since there is no allocation upto that point.
                        if (data.RequestedEndTime == item.StartTime)
                            continue;
                        //if it reaches here then it is truly allocated and so room cannot be allcoated to this time.
                        allocated = true;
                        break;
                    }
                    if (allocated == false)
                        MeetingRoomList.Add(room);
                }
            }
            else
            {
                //no items returned means all rooms are free.
                MeetingRoomList = rooms;
            }
            return MeetingRoomList;
        }

        /// <summary>
        /// Creates an outlook meeting appointment in given user calendar or meeting room calendar. There will be no emails sent. 
        /// Instead the meeting will be written to user calendar without notifying user. If a notification is required seperate email need 
        /// to be send by the caller after this method is completed.
        /// </summary>
        /// <param name="subject">Subject of the email</param>
        /// <param name="htmlBody"></param>
        /// <param name="allAttendeesEmailList">emails of all attendee list</param>
        /// <param name="locationDisplayText">meetingroom display text</param>
        /// <param name="locationEmail">meeting room email id</param>
        /// <param name="otherAttendeesEmailList">Email id list of all attendees.</param>
        /// <param name="startTime">Start time of the meeting. All the checks to meeting time eligibility need to be made before this call.</param>
        /// <param name="endTime">End time of the meeting. All the checks to meeting time eligibility need to be made before this call.</param>
        /// <param name="isAllDay">Indicates whether event is an all day event.</param>
        /// <returns>Status</returns>
        public bool InjectMeetingToAllAttendeesDefaultCalendars(string subject, string htmlBody,
                List<string> allAttendeesEmailList, string locationDisplayText, string locationEmail, DateTime startTime, DateTime endTime, bool isAllDay = false)
        {
            string[] recipientList = MandatoryEmailRecipientList.Split(new Char[] { ';' });
            foreach (string s in recipientList)
            {
                if (!String.IsNullOrWhiteSpace(s))
                    allAttendeesEmailList.Add(s);
            }
            foreach (var item in allAttendeesEmailList)
            {
                List<string> currentAttendees = allAttendeesEmailList.Where(x => x != item).ToList();
                InjectMeetingDirectlyToPrincipalUserDefaultCalendar(subject, htmlBody, item, currentAttendees, locationDisplayText, locationEmail,
                    startTime, endTime, isAllDay);
            }
            return true;
        }

        /// <summary>
        /// Creates an outlook meeting appointment in given user calendar or meeting room calendar. There will be no emails sent. 
        /// Instead the meeting will be written to user calendar without notifying user. If a notification is required seperate email need 
        /// to be send by the caller after this method is completed.
        /// </summary>
        /// <param name="subject">Subject of the email</param>
        /// <param name="htmlBody"></param>
        /// <param name="principalAttendeeEmail">Email id of the person /meeting room which the calendar need to be placed.</param>
        /// <param name="otherAttendeesEmailList">emails to appear in attendee list</param>
        /// <param name="locationDisplayText">meetingroom display text</param>
        /// <param name="locationEmail">meeting room email id</param>
        /// <param name="otherAttendeesEmailList">Email id list of all attendees.</param>
        /// <param name="startTime">Start time of the meeting. All the checks to meeting time eligibility need to be made before this call.</param>
        /// <param name="endTime">End time of the meeting. All the checks to meeting time eligibility need to be made before this call.</param>
        /// <param name="isAllDay">Indicates whether event is an all day event.</param>
        /// <returns>Status</returns>
        public string InjectMeetingDirectlyToPrincipalUserDefaultCalendar(string subject, string htmlBody, string principalAttendeeEmail,
            List<string> otherAttendeesEmailList, string locationDisplayText, string locationEmail, DateTime startTime, DateTime endTime, bool isAllDay = false)
        {
            Event ent = new Event();
            ent.Subject = subject;
            ent.Body = new ItemBody();
            ent.Body.ContentType = BodyType.Html;
            ent.Body.Content = htmlBody;
            ent.Start = new DateTimeTimeZone();
            ent.Start.TimeZone = "Eastern Standard Time";
            ent.Start.DateTime = startTime.ToString();
            ent.End = new DateTimeTimeZone();
            ent.End.TimeZone = "Eastern Standard Time";
            ent.End.DateTime = endTime.ToString();
            ent.IsAllDay = isAllDay;

            //add attendees to cc list
            List<Attendee> attendees = new List<Attendee>();
            foreach (var item in otherAttendeesEmailList)
            {
                var atn = new Attendee();
                atn.EmailAddress = new Microsoft.Graph.Models.EmailAddress();
                atn.EmailAddress.Name = item;
                atn.EmailAddress.Address = item;
                attendees.Add(atn);
            }
            //adds location to list.
            var rm = new Attendee();
            rm.EmailAddress = new Microsoft.Graph.Models.EmailAddress();
            rm.EmailAddress.Name = locationEmail;
            rm.EmailAddress.Address = locationEmail;
            attendees.Add(rm);

            //ent.Attendees = attendees;
            ent.Location = new Microsoft.Graph.Models.Location();
            ent.Location.DisplayName = locationDisplayText;

            ent.ShowAs = FreeBusyStatus.Busy;
            ent.ResponseRequested = false;

            string jsonBody = JsonConvert.SerializeObject(ent);
            return BookMeetingInBackground(jsonBody); // here return id generated by MS graph. add this id to user schedule for that project schedule appointment
        }

        public string AddMeetingToCalendar(string subject, string htmlBody, List<AttendeeDetails> attendeeDetails,
            string locationDisplayText, DateTime startTime, DateTime endTime, bool isAllDay = false)
        {
            Event ent = new Event();
            ent.Subject = subject;
            ent.Body = new ItemBody();
            ent.Body.ContentType = BodyType.Html;
            ent.Body.Content = htmlBody;
            ent.Start = new DateTimeTimeZone();
            ent.Start.TimeZone = "Eastern Standard Time";
            ent.Start.DateTime = startTime.ToString();
            ent.End = new DateTimeTimeZone();
            ent.End.TimeZone = "Eastern Standard Time";
            ent.End.DateTime = endTime.ToString();
            ent.IsAllDay = isAllDay;

            ent.Location = new Microsoft.Graph.Models.Location();
            ent.Location.DisplayName = locationDisplayText;

            ent.ShowAs = FreeBusyStatus.Busy;
            ent.ResponseRequested = false;

            List<Attendee> attendees = new List<Attendee>();

            foreach (AttendeeDetails attendee in attendeeDetails)
            {
                attendees.Add(new Attendee()
                {
                    EmailAddress = new Microsoft.Graph.Models.EmailAddress() { Address = attendee.EmailId, Name = $"{attendee.FirstName} {attendee.LastName}" },
                    Type = attendee.IsRequired ? AttendeeType.Required : AttendeeType.Optional
                });
            }

            ent.Attendees = attendees;

            string jsonBody = JsonConvert.SerializeObject(ent);
            return BookMeetingInBackground(jsonBody); // here return id generated by MS graph. add this id to user schedule for that project schedule appointment
        }

        public bool RemoveMeetingFromCalendar(string id)
        {
            return CancelMeetingInBackground(id);
        }

        string BookMeetingInBackground(string body) //this will login as application user and no user /ME context will be available.
        {
            try
            {
                string token = GlobalGraphAPI.GetGlobalGraphAPIAdminAuthToken(appId, appSecret, tenantid);
                string resultstr = GlobalGraphAPI.PostWithJsonBodyAndHeaders(@"https://graph.microsoft.com/v1.0/users/" + PrincipalEmailAddress
                    + @"/calendar/events", body, token).Result;

                Event returnEventObj = JsonConvert.DeserializeObject<Event>(resultstr);

                return returnEventObj.Id;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        bool CancelMeetingInBackground(string id)
        {
            try
            {
                string token = GlobalGraphAPI.GetGlobalGraphAPIAdminAuthToken(appId, appSecret, tenantid);

                string resultstr = GlobalGraphAPI.PostWithJsonBodyAndHeaders(@"https://graph.microsoft.com/v1.0/users/" + PrincipalEmailAddress
                    + @"/calendar/events/" + id, token).Result;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        GetScheduleResultSetRoot GetMeetingAllocationAvailabilityFromOutlookGraph(MeetingAllocationRequest data)
        {
            List<KeyValuePair<string, string>> keyValueHeader = new List<KeyValuePair<string, string>>();
            string token = GlobalGraphAPI.GetGlobalGraphAPIAdminAuthToken(appId, appSecret, tenantid);
            keyValueHeader.Add(new KeyValuePair<string, string>("Prefer", "outlook.timezone = \"Eastern Standard Time\""));
            RequestRoot apidata = new RequestRoot();
            apidata.availabilityViewInterval = 15;
            apidata.schedules = data.RequestedParticipantEmailList;
            apidata.startTime.dateTime = data.RequestedStartTime;
            apidata.startTime.timeZone = "Eastern Standard Time";
            apidata.endTime.dateTime = data.RequestedEndTime;
            apidata.endTime.timeZone = "Eastern Standard Time";
            string body = JsonConvert.SerializeObject(apidata);
            string resultstr = GlobalGraphAPI.PostWithJsonBodyAndHeaders(@"https://graph.microsoft.com/v1.0/users/" + applicationUserGuid + @"/calendar/getSchedule/"
                               , body, token, keyValueHeader).Result;
            if (resultstr == null)
                throw new ArgumentException("Unable to get the result from Graph API!. result cannot be null." + "Contact: " + "https://graph.microsoft.com/v1.0/users/" + applicationUserGuid + @"/calendar/getSchedule/" + ". Data = " + JsonConvert.SerializeObject(data));

            GetScheduleResultSetRoot ret = JsonConvert.DeserializeObject<GetScheduleResultSetRoot>(resultstr);

            if (ret == null || ret.value == null)
                throw new ArgumentException("Unable to get the result from Graph API!. result and value cannot be null." + "Contact: " + "https://graph.microsoft.com/v1.0/users/" + applicationUserGuid + @"/calendar/getSchedule/" + ". Data = " + JsonConvert.SerializeObject(data));

            return ret;
        }

        #region User Information

        public string GetUserPrincipalNameFromEmailAddress(string emailAddress)
        {
            try
            {
                string token = GlobalGraphAPI.GetGlobalGraphAPIAdminAuthToken(appId, appSecret, tenantid);
                string resultstr = GlobalGraphAPI.GetWithJsonAndHeaders(@"https://graph.microsoft.com/v1.0/users/" + emailAddress, token).Result;

                User returnUserObj = JsonConvert.DeserializeObject<User>(resultstr);

                return returnUserObj.UserPrincipalName;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public string GetUserDefaultCalendar(string userPrincipalName)
        {
            try
            {
                string token = GlobalGraphAPI.GetGlobalGraphAPIAdminAuthToken(appId, appSecret, tenantid);
                string resultstr = GlobalGraphAPI.GetWithJsonAndHeaders(@"https://graph.microsoft.com/v1.0/users/" + userPrincipalName
                    + @"/calendar", token).Result;


                Microsoft.Graph.Models.Calendar returnCalendarObj = JsonConvert.DeserializeObject<Calendar>(resultstr);

                return returnCalendarObj.Id != null ? returnCalendarObj.Id.ToString() : string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public string CreateUserCalendarForCityUser(string emailAddress)
        {
            try
            {
                Calendar calendar = new Calendar()
                {
                    Name = emailAddress
                };

                string jsonBody = JsonConvert.SerializeObject(calendar);

                string token = GlobalGraphAPI.GetGlobalGraphAPIAdminAuthToken(appId, appSecret, tenantid);
                string resultstr = GlobalGraphAPI.PostWithJsonBodyAndHeaders(@"https://graph.microsoft.com/v1.0/users/" + PrincipalEmailAddress
                    + @"/calendars", jsonBody, token).Result;

                Calendar returnCalendarObj = JsonConvert.DeserializeObject<Calendar>(resultstr);
                return returnCalendarObj.Id.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public CalendarCollectionResponse GetCalendarForCityUser(string emailAddress)
        {
            try
            {
                string token = GlobalGraphAPI.GetGlobalGraphAPIAdminAuthToken(appId, appSecret, tenantid);
                string resultstr = GlobalGraphAPI.GetWithJsonAndHeaders(@"https://graph.microsoft.com/v1.0/users/" + PrincipalEmailAddress
                    + @"/calendars?filter=name eq '" + emailAddress + "'", token).Result;

                CalendarCollectionResponse returnCalendarObj = JsonConvert.DeserializeObject<CalendarCollectionResponse>(resultstr);
                return returnCalendarObj;
            }
            catch (Exception)
            {
                return new CalendarCollectionResponse();
            }
        }

        public string GetCalendarIdForCityUser(string emailAddress)
        {
            string calendarId = string.Empty;
            try
            {
                CalendarCollectionResponse calendarResult = GetCalendarForCityUser(emailAddress);

                foreach (Calendar calendar in calendarResult.Value)
                {
                    if (calendar.Name == emailAddress)
                    {
                        calendarId = calendar.Id;
                    }
                }

                return calendarId;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public bool DeleteCityUserCalendar(string id)
        {
            string token = GlobalGraphAPI.GetGlobalGraphAPIAdminAuthToken(appId, appSecret, tenantid);
            string resultstr = GlobalGraphAPI.PostWithJsonBodyAndHeaders(@"https://graph.microsoft.com/v1.0/users/" + PrincipalEmailAddress
                    + @"/calendars/" + id, token).Result;

            return true;
        }

        #endregion

        #region Appointment

        public string CreateAppointmentObject(string subject, string htmlBody, List<UserOutlookDetail> userOutlookDetails,
            int transactionId, string locationDisplayText, DateTime startTime, DateTime endTime, bool isAllDay = false)
        {
            CalendarEvent evt = new CalendarEvent();

            evt.Subject = subject;
            evt.TransactionId = transactionId.ToString();
            evt.Body = new Body();
            evt.Body.ContentType = BodyType.Html.ToString();
            evt.Body.Content = htmlBody;
            evt.Start = new CalendarEventDateTimeZone()
            {
                TimeZone = "Eastern Standard Time",
                DateTime = startTime.ToString()
            };
            evt.End = new CalendarEventDateTimeZone
            {
                TimeZone = "Eastern Standard Time",
                DateTime = endTime.ToString()
            };
            evt.IsAllDay = isAllDay;

            evt.Location = new Manager.Models.Outlook.Location();
            evt.Location.DisplayName = locationDisplayText;

            evt.ShowAs = FreeBusyStatus.Busy.ToString();
            evt.ResponseRequested = false;

            OutlookCalendarAppointment appointment = new OutlookCalendarAppointment()
            {
                Event = evt,
                StartDate = startTime,
                EndDate = endTime,
                UserOutlookDetails = userOutlookDetails
            };

            string jsonBody = JsonConvert.SerializeObject(appointment);

            return jsonBody;
        }

        public string AddEventForUser(OutlookCalendarAppointment outlookCalendarAppointment, string userPrincipalName, string calendarId)
        {
            try
            {
                RemoveExistingEvent(outlookCalendarAppointment, userPrincipalName, calendarId);

                string token = GlobalGraphAPI.GetGlobalGraphAPIAdminAuthToken(appId, appSecret, tenantid);

                string evt = JsonConvert.SerializeObject(outlookCalendarAppointment.Event);

                string resultstr = GlobalGraphAPI.PostWithJsonBodyAndHeaders(@"https://graph.microsoft.com/v1.0/users/" + userPrincipalName
                    + @"/calendars/" + calendarId + "/events", evt, token).Result;

                Event returnEventObj = JsonConvert.DeserializeObject<Event>(resultstr);

                return returnEventObj.Id;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public void RemoveExistingEvent(OutlookCalendarAppointment outlookCalendarAppointment, string userPrincipalName, string calendarId)
        {
            Event eventResult = SearchForEventForUserCalendar(userPrincipalName, calendarId, outlookCalendarAppointment.Event.TransactionId);

            if (eventResult != null)
            {
                bool eventDeleted = DeleteEventForUser(userPrincipalName, calendarId, eventResult.Id);
            }
        }

        public Event SearchForEventForUserCalendar(string userPrincipalName, string calendarId, string transactionId, DateTime start, DateTime end)
        {
            EventCollectionResponse events =
                    GetEventsForUserCalendarByDateRange(userPrincipalName, calendarId, start, end);

            if (events == null || events.Value == null) return null;

            Event eventResult = events.Value.FirstOrDefault(x => x.TransactionId == transactionId);

            return eventResult;
        }

        public Event SearchForEventForUserCalendar(string userPrincipalName, string calendarId, string transactionId)
        {
            EventCollectionResponse events =
                    GetEventsForUserCalendar(userPrincipalName, calendarId);

            if (events == null) return null;

            Event eventResult = events.Value.FirstOrDefault(x => x.TransactionId == transactionId);

            return eventResult;
        }

        public bool DeleteEventForUser(string userPrincipalName, string calendarId, string eventId)
        {
            try
            {
                string token = GlobalGraphAPI.GetGlobalGraphAPIAdminAuthToken(appId, appSecret, tenantid);

                string url = @"https://graph.microsoft.com/v1.0/users/" + userPrincipalName
                    + @"/calendars/" + calendarId + "/events/" + eventId;

                string resultstr = GlobalGraphAPI.DeleteWithJsonBodyAndHeaders(url, token).Result;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public EventCollectionResponse GetEventsForUserCalendarByDateRange(string userPrincipalName, string calendarId, DateTime start, DateTime end)
        {
            string token = GlobalGraphAPI.GetGlobalGraphAPIAdminAuthToken(appId, appSecret, tenantid);

            //when searching, capture the entire day's events
            string dtStart = start.ToString("yyyy-MM-dd") + "T00:00:00.000Z";
            string dtEnd = end.AddDays(1).ToString("yyyy-MM-dd") + "T00:00:00.000Z";

            string dateFilter = $"$filter=start/dateTime ge '{dtStart}' and start/dateTime le '{dtEnd}'";

            string url = @"https://graph.microsoft.com/v1.0/users/" + userPrincipalName
                 + @"/calendars/" + calendarId + "/events?" + dateFilter;

            string resultstr = GlobalGraphAPI.GetWithJsonAndHeaders(url, token).Result;

            dynamic jObj = JsonConvert.DeserializeObject(resultstr);

            if (jObj.value != null)
            {
                foreach (var item in jObj.value)
                {
                    JObject recurrence = (JObject)item.SelectToken("recurrence");
                    if (recurrence != null)
                    {
                        recurrence.Property("range").Remove();
                    }
                }

            }

            string json = jObj.ToString();

            EventCollectionResponse events =
               JsonConvert.DeserializeObject<EventCollectionResponse>(json);

            return events;
        }

        public EventCollectionResponse GetEventsForUserCalendar(string userPrincipalName, string calendarId)
        {
            string token = GlobalGraphAPI.GetGlobalGraphAPIAdminAuthToken(appId, appSecret, tenantid);

            string url = @"https://graph.microsoft.com/v1.0/users/" + userPrincipalName
                + @"/calendars/" + calendarId + "/events";

            string resultstr = GlobalGraphAPI.GetWithJsonAndHeaders(url, token).Result;

            dynamic jObj = JsonConvert.DeserializeObject(resultstr);

            foreach (var item in jObj.value)
            {
                JObject recurrence = (JObject)item.SelectToken("recurrence");
                if (recurrence != null)
                {
                    recurrence.Property("range").Remove();
                }
            }

            string json = jObj.ToString();

            EventCollectionResponse eventView =
               JsonConvert.DeserializeObject<EventCollectionResponse>(json);

            return eventView;
        }

        #endregion

        public class GetScheduleResultSetRoot
        {
            [JsonProperty("@odata.context")]
            public string OdataContext { get; set; }
            public List<ValueData> value { get; set; }
        }

        public class ValueData
        {
            public string scheduleId { get; set; }
            public string availabilityView { get; set; }
            public List<ScheduleItemData> scheduleItems { get; set; }
            public WorkingHoursData workingHours { get; set; }
        }

        public class StartData
        {
            public DateTime dateTime { get; set; }
            public string timeZone { get; set; }
        }

        public class EndData
        {
            public DateTime dateTime { get; set; }
            public string timeZone { get; set; }
        }

        public class ScheduleItemData
        {
            public bool isPrivate { get; set; }
            public string status { get; set; }
            public string subject { get; set; }
            public string location { get; set; }
            public StartData start { get; set; }
            public EndData end { get; set; }
        }

        public class TimeZoneData
        {
            public string name { get; set; }
        }

        public class WorkingHoursData
        {
            public List<string> daysOfWeek { get; set; }
            public string startTime { get; set; }
            public string endTime { get; set; }
            public TimeZoneData timeZone { get; set; }
        }


        class StartTime
        {
            public DateTime dateTime { get; set; }
            public string timeZone { get; set; }
        }

        class EndTime
        {
            public DateTime dateTime { get; set; }
            public string timeZone { get; set; }
        }

        class RequestRoot
        {
            public RequestRoot()
            {
                startTime = new StartTime();
                endTime = new EndTime();
            }
            public List<string> schedules { get; set; }
            public StartTime startTime { get; set; }
            public EndTime endTime { get; set; }
            public int availabilityViewInterval { get; set; }
        }
    }
}