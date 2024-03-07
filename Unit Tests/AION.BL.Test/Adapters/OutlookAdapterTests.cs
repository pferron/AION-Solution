using AION.BL.Adapters;
using AION.BL.Models;
using AION.Engine.BusinessEntities;
using AION.Manager.Adapters;
using AION.Manager.Models;
using AION.Manager.Models.Outlook;
using AIONEstimator.Engine.BusinessObjects;
using Microsoft.Graph.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AION.BL.Test.Adapters
{
    [TestClass]
    public class OutlookAdapterTests
    {
        [TestMethod]
        public void TestGetUserPrincipalName()
        {
            OutlookAdapter outlookAdapter = new OutlookAdapter();

            string emailAddress = "permitplanreview@mecklenburgcountync.gov";
            string expectedUPN = "permitplanreview@mecklenburgcountync.gov";

            string actualUPN = outlookAdapter.GetUserPrincipalNameFromEmailAddress(emailAddress);
            Assert.AreEqual(expectedUPN, actualUPN);
        }

        [TestMethod]
        public void TestGetUserDefaultCalendarExpectCalendarString()
        {
            OutlookAdapter outlookAdapter = new OutlookAdapter();

            string userPrincipalName = "Janessa.Allen@mecklenburgcountync.gov";
            string expectedCalendar = "AQMkADYyZDEzN2U4LWI2OWItNGRjYS1hOTRlLWVhN2YzODUxMzViZgBGAAADqcJGYK1ZAU_XvfvtMTY2APEHAHtMXieaT3FHh9Ghtciq1XcAAAIBBgAAAHtMXieaT3FHh9Ghtciq1XcAAAJzPgAAAA==";

            string actualCalendar = outlookAdapter.GetUserDefaultCalendar(userPrincipalName);
            Assert.AreEqual(expectedCalendar, actualCalendar);
        }

        //[TestMethod]
        public void TestGetUserDefaultCalendarExpectEmptyString()
        {
            OutlookAdapter outlookAdapter = new OutlookAdapter();

            string userPrincipalName = "test@outlook.com";
            string expectedCalendar = "";

            string actualCalendar = outlookAdapter.GetUserDefaultCalendar(userPrincipalName);
            Assert.AreEqual(expectedCalendar, actualCalendar);
        }

        [TestMethod]
        [Ignore]
        public void TestDeleteCityUserCalendar()
        {
            string emailAddress = "janessa.allen@mecklenburgcountync.gov";

            OutlookAdapter outlookAdapter = new OutlookAdapter();

            CalendarCollectionResponse calendars = outlookAdapter.GetCalendarForCityUser(emailAddress);

            foreach (Calendar calendar in calendars.Value)
            {
                if (calendar.Name == emailAddress)
                {
                    var response = outlookAdapter.DeleteCityUserCalendar(calendar.Id);
                }
            }
        }

        [TestMethod]
        [Ignore]
        public void TestCreateAppointmentObject()
        {
            OutlookAdapter outlookAdapter = new OutlookAdapter();

            int expectedUserOutlookDetailsCount = 2;
            string subject = "Test Subject";
            string body = "Test Message";
            string meetingRoom = "Meeting Room";
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now.AddHours(1);

            string actualJsonObject = outlookAdapter.CreateAppointmentObject(subject,
                    body,
                    new List<UserOutlookDetail>() {
                        new UserOutlookDetail()
                        {
                            UserPrincipalName = "Janessa.Allen@mecklenburgcountync.gov",
                            CalendarId = "AQMkADYyZDEzN2U4LWI2OWItNGRjYS1hOTRlLWVhN2YzODUxMzViZgBGAAADqcJGYK1ZAU_XvfvtMTY2APEHAHtMXieaT3FHh9Ghtciq1XcAAAIBBgAAAHtMXieaT3FHh9Ghtciq1XcAAAJzPgAAAA=="
                        },
                        new UserOutlookDetail()
                        {
                                                        UserPrincipalName = "MintHill@mecktech.onmicrosoft.com",
                            CalendarId = "AAMkADQ3MmRkYjhmLWU3ZTMtNDc4Ni1iYmE0LTFhM2UxMGZkZWYyMABGAAAAAABY9ZawjAPrQpaP5V0rEQxjBwCMHo5CSX37Q65DSBFBbScYAAAAAAEGAACMHo5CSX37Q65DSBFBbScYAAUou6N3AAA="
                        }
                    },
                    4444,
                    meetingRoom,
                    start,
                    end,
                    false);


            Assert.IsNotNull(actualJsonObject);

            OutlookCalendarAppointment appointment = JsonConvert.DeserializeObject<OutlookCalendarAppointment>(actualJsonObject);

            Assert.AreEqual(expectedUserOutlookDetailsCount, appointment.UserOutlookDetails.Count);
        }

        [TestMethod]
        [Ignore]
        public void TestDeleteOutlookAppointmentByTransactionId()
        {
            OutlookAdapter outlookAdapter = new OutlookAdapter();

            string userPrincipalName = "permitplanreview@mecklenburgcountync.gov";
            string calendarId = "AAMkADE3NWRmMzEzLTU4YzUtNGI1My04MmY3LWU5MjBlMGJhNmU2YQBGAAAAAADCCsCVydAAQIR5J8HDUmaIBwAnYasg_LiCTJkZ7eAvk8AhAAAAAAEGAAAnYasg_LiCTJkZ7eAvk8AhAAD-VqtaAAA=";
            string transactionId = "4444";

            DateTime start = DateTime.Parse("5/10/2021");
            DateTime end = start.AddDays(1);

            Event eventResult = outlookAdapter.SearchForEventForUserCalendar(userPrincipalName, calendarId, transactionId, start, end);

            bool isSuccess = outlookAdapter.DeleteEventForUser(
                        userPrincipalName, calendarId, eventResult.Id);

            Assert.IsTrue(isSuccess);
        }

        [TestMethod]
        [Ignore]
        public void TestSearchEventForUserByTransactionId()
        {
            OutlookAdapter outlookAdapter = new OutlookAdapter();

            string userPrincipalName = "permitplanreview@mecklenburgcountync.gov";
            string calendarId = "AAMkADE3NWRmMzEzLTU4YzUtNGI1My04MmY3LWU5MjBlMGJhNmU2YQBGAAAAAADCCsCVydAAQIR5J8HDUmaIBwAnYasg_LiCTJkZ7eAvk8AhAAAAAAEGAAAnYasg_LiCTJkZ7eAvk8AhAAD-VqtaAAA=";
            string transactionId = "8182";

            Event eventResult = outlookAdapter.SearchForEventForUserCalendar(userPrincipalName, calendarId, transactionId);

            Assert.IsNotNull(eventResult);
        }

        [TestMethod]
        [Ignore]
        public void TestAddEventForUser()
        {
            OutlookAdapter outlookAdapter = new OutlookAdapter();

            string userPrincipalName = "permitplanreview@mecklenburgcountync.gov";
            string calendarId = "AAMkADE3NWRmMzEzLTU4YzUtNGI1My04MmY3LWU5MjBlMGJhNmU2YQBGAAAAAADCCsCVydAAQIR5J8HDUmaIBwAnYasg_LiCTJkZ7eAvk8AhAAAAAAEGAAAnYasg_LiCTJkZ7eAvk8AhAAD-VqtaAAA=";

            CalendarEvent evt = new CalendarEvent();

            evt.Subject = "Test Subject";
            evt.TransactionId = "4444";
            evt.Body = new Body();
            evt.Body.ContentType = BodyType.Html.ToString();
            evt.Body.Content = "Test Message";
            evt.Start = new CalendarEventDateTimeZone();
            evt.Start.TimeZone = "Eastern Standard Time";
            evt.Start.DateTime = DateTime.Now.ToString();
            evt.End = new CalendarEventDateTimeZone();
            evt.End.TimeZone = "Eastern Standard Time";
            evt.End.DateTime = DateTime.Now.AddHours(1).ToString();
            evt.IsAllDay = false;

            evt.Location = new Manager.Models.Outlook.Location();
            evt.Location.DisplayName = "Meeting Room";

            evt.ShowAs = FreeBusyStatus.Busy.ToString();
            evt.ResponseRequested = false;

            List<UserOutlookDetail> userOutlookDetails = new List<UserOutlookDetail>();
            userOutlookDetails.Add(new UserOutlookDetail()
            {
                UserPrincipalName = userPrincipalName,
                CalendarId = calendarId
            });

            OutlookCalendarAppointment appointment = new OutlookCalendarAppointment()
            {
                Event = evt,
                UserOutlookDetails = userOutlookDetails
            };

            string eventId = outlookAdapter.AddEventForUser(appointment, userPrincipalName, calendarId);

            Assert.IsNotNull(eventId);
        }

        [TestMethod]
        [Ignore]
        public void TestRemoveUserOutlookCalendarAppointment()
        {
            OutlookAdapter outlookAdapter = new OutlookAdapter();

            string userPrincipalName = "HalMarshall@mecktech.onmicrosoft.com";
            string calendarId = outlookAdapter.GetUserDefaultCalendar(userPrincipalName);
            string transactionId = "3850261"; //5816 5802

            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now.AddMonths(1);

            Event evt = outlookAdapter.SearchForEventForUserCalendar(
                userPrincipalName, calendarId, transactionId, start, end);

            bool success = false;

            if (evt != null)
            {
                success = outlookAdapter.DeleteEventForUser(
                    "HalMarshall@mecktech.onmicrosoft.com", calendarId, evt.Id);
            }

            Assert.IsNotNull(success);
        }

        [TestMethod]
        [Ignore]
        public void TestRemoveMeetingRoomOutlookCalendarAppointments()
        {
            OutlookAdapter outlookAdapter = new OutlookAdapter();

            // get all rooms
            MeetingRoomAdapter meetingRoomAdapter = new MeetingRoomAdapter();
            List<MeetingRoom> meetingRooms = meetingRoomAdapter.GetMeetingRooms(true);

            // get future project schedules by PR, FMA, EXP, EMA, NPA
            ProjectScheduleBO projectScheduleBO = new ProjectScheduleBO();
            List<ProjectScheduleBE> projectSchedules = projectScheduleBO.GetList();

            List<ProjectScheduleBE> schedulesToProcess = projectSchedules
                .Where(x => (x.ProjectScheduleTypeRef == "FMA"
                    || x.ProjectScheduleTypeRef == "EMA"
                    || x.ProjectScheduleTypeRef == "EXP"
                    || x.ProjectScheduleTypeRef == "PMA"
                    || x.ProjectScheduleTypeRef == "NPA")
                    && x.RecurringApptDt.HasValue && x.RecurringApptDt >= DateTime.Now)
                .ToList();

            foreach (ProjectScheduleBE schedule in schedulesToProcess)
            {
                foreach (MeetingRoom meetingRoom in meetingRooms)
                {
                    Event evt = outlookAdapter.SearchForEventForUserCalendar(
                        meetingRoom.UserPrincipalName,
                        meetingRoom.CalendarId,
                        schedule.ProjectScheduleID.ToString(),
                        schedule.RecurringApptDt.Value,
                        schedule.RecurringApptDt.Value);

                    bool success = false;

                    if (evt != null)
                    {
                        success = outlookAdapter.DeleteEventForUser(
                            meetingRoom.UserPrincipalName, meetingRoom.CalendarId, evt.Id);
                    }
                }
            }

            Assert.AreEqual(1, 1);
        }

        [TestMethod]
        [Ignore]
        public void TestGetAllMeetingRoomEventsAndRemoveThoseWithNonNullTransactionId()
        {
            OutlookAdapter outlookAdapter = new OutlookAdapter();

            MeetingRoomAdapter meetingRoomAdapter = new MeetingRoomAdapter();
            List<MeetingRoom> meetingRooms = meetingRoomAdapter.GetMeetingRooms(true);

            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now.AddYears(1);

            foreach (var meetingRoom in meetingRooms)
            {
                EventCollectionResponse events =
                    outlookAdapter.GetEventsForUserCalendarByDateRange(meetingRoom.UserPrincipalName, meetingRoom.CalendarId, start, end);

                List<Event> eventResults = events.Value.Where(x => x.TransactionId != null && x.TransactionId != "").ToList();

                foreach (var eventResult in eventResults)
                {
                    bool success = outlookAdapter.DeleteEventForUser(
                            meetingRoom.UserPrincipalName, meetingRoom.CalendarId, eventResult.Id);
                }
            }
        }



        public void SearchForEventForUserCalendarAndDeleteEventTest()
        {
            //  this searches for the project schedule ids and gets the events for each user and attempts to delete them.
            //each appointment table => project schedule id => user schedules => Outlook Adapter SearchForEventForUserCalendar
            OutlookAdapter outlookAdapter = new OutlookAdapter();
            UserAdapter userAdapter = new UserAdapter();

            // get future project schedules by PR, FMA, EXP, EMA, NPA
            ProjectScheduleBO projectScheduleBO = new ProjectScheduleBO();
            List<ProjectScheduleBE> projectSchedules = projectScheduleBO.GetList();

            List<ProjectScheduleBE> schedulesToProcess = projectSchedules
                .Where(x => (x.ProjectScheduleTypeRef == "FMA"
                    || x.ProjectScheduleTypeRef == "EMA"
                    || x.ProjectScheduleTypeRef == "PMA")
                    && x.RecurringApptDt.HasValue && x.RecurringApptDt >= DateTime.Now)
                .ToList();

            foreach (ProjectScheduleBE schedule in schedulesToProcess)
            {

                UserScheduleBO userScheduleBO = new UserScheduleBO();
                List<UserScheduleBE> userSchedules = userScheduleBO.GetListByScheduleID(schedule.ProjectScheduleID.Value);

                var ct = userSchedules.Count();

                if (ct > 0)
                {
                    var u = userAdapter.GetUserIdentityByID(userSchedules[0].UserID.Value);
                    Event evt = outlookAdapter.SearchForEventForUserCalendar(
                                    u.UserPrincipalName,
                                    u.CalendarId,
                                    schedule.ProjectScheduleID.ToString(),
                                    schedule.RecurringApptDt.Value,
                                    schedule.RecurringApptDt.Value);

                    if (evt != null)
                    {
                        bool eventDeleted = outlookAdapter.DeleteEventForUser(u.UserPrincipalName, u.CalendarId, evt.Id);
                    }

                }

            }


            Assert.IsNotNull(projectSchedules);

        }
    }
}
