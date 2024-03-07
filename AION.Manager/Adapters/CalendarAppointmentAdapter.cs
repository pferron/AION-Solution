using AION.BL;
using AION.BL.Adapters;
using AION.BL.BusinessObjects;
using AION.BL.Common;
using AION.BL.Models;
using AION.Email.Engine.BusinessObjects;
using AION.Engine.BusinessEntities;
using AION.Engine.BusinessObjects;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.Models;
using AIONEstimator.Engine.BusinessObjects;
using Meck.Logging;
using Microsoft.Graph.Models;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AION.Manager.Adapters
{
    public class CalendarAppointmentAdapter : BaseManagerAdapter, ICalendarAppointmentAdapter
    {
        private readonly int _NumberToBatch = 10;

        private List<AttendeeDetails> _Attendees;
        private List<AttendeeDetails> _RemainingAttendees = new List<AttendeeDetails>();
        private Appointment _Appointment;
        private MeetingRoom _MeetingRoom;
        private ProjectSchedule _ProjectSchedule;
        private HolidayConfig _HolidayConfig;
        private ProjectEstimation _ProjectEstimation;
        private List<ScheduleTime> _ScheduleTimes;
        private AppointmentRecurrenceRefEnum _AppointmentRecurrence;
        private System.DayOfWeek? _DayOfWeek;

        private AppointmentEmail _AppointmentEmail;

        public CalendarAppointmentAdapter(Appointment appointment)
        {
            _Attendees = UpdateAttendeesWithUserIdentities(appointment.AttendeeDetails);

            _Appointment = appointment;
            _HolidayConfig = appointment.HolidayConfig;
            _ProjectSchedule = appointment.ProjectSchedule;
            _ScheduleTimes = appointment.ScheduleTimes;
            _ProjectEstimation = appointment.ProjectEstimation;

            if (_HolidayConfig != null)
            {
                _AppointmentEmail = GenerateHolidaySubjectAndMessage();
                SetHolidayRecurrence();
            }
            else
            {
                if (_ProjectSchedule != null)
                {
                    _AppointmentEmail = GenerateAppointmentSubjectAndMessage();
                    SetAppointmentRecurrence();
                    GetRemainingScheduledAttendees();
                }
            }

            SetMeetingRoom();

            if (_MeetingRoom != null)
            {
                AddMeetingRoomToListOfAttendees();
            }
        }

        public IEnumerable<IEnumerable<AttendeeDetails>> CreateAttendeeBatches(int numberToBatch)
        {
            return _Attendees.Batch(numberToBatch);
        }

        public bool ProcessAppointments()
        {
            bool success = false;
            if (_Attendees.Count == 0)
            {
                return true;
            }

            if (_ScheduleTimes != null)
            {
                string calendarEngine = new CatalogRefBO().GetByExternalRef("EMAIL").Where(x => x.Key == "CALENDARENGINE").FirstOrDefault().Value;
                if (calendarEngine.Equals("ON", StringComparison.CurrentCultureIgnoreCase))
                {
                    success = GenerateCalendarEventQueueRecord();

                }
                else
                {
                    //get the mandatory list, check if not blank, add attendees as the mandatory list
                    string emaillist = new CatalogRefBO().GetByExternalRef("EMAIL").Where(x => x.Key == "CALENDARENGINE.MANDATORYLIST").FirstOrDefault().Value;
                    if (string.IsNullOrWhiteSpace(emaillist))
                    {
                        success = true;

                    }
                    else
                    {
                        //set attendees to mandatory list
                        _Attendees = new List<AttendeeDetails>();
                        string[] recipientList = emaillist.Split(new Char[] { ';' });
                        foreach (string useremail in recipientList)
                        {
                            if (!String.IsNullOrWhiteSpace(useremail))
                            {
                                //for each email, get the user and add the attendee details
                                UserBE userBE = new UserBO().GetByExternalRefInfo(useremail, (int)ExternalSystemEnum.Accela);
                                AION.BL.UserIdentity identity = new UserIdentityModelBO().GetInstance(userBE.UserID.Value);

                                _Attendees.Add(new AttendeeDetails
                                {
                                    EmailId = identity.SrcSystemValueText,
                                    IsRequired = true,
                                    FirstName = identity.FirstName,
                                    LastName = identity.LastName,
                                    UserId = identity.ID,
                                    UserPrincipalName = identity.UserPrincipalName,
                                    CalendarId = identity.CalendarId

                                });

                            }
                        }

                        success = GenerateCalendarEventQueueRecord();
                    }
                }

            }

            return success;
        }

        private List<UserOutlookDetail> GenerateOutlookDetails(IEnumerable<AttendeeDetails> attendeeDetails)
        {
            List<UserOutlookDetail> userOutlookDetails = new List<UserOutlookDetail>();

            foreach (AttendeeDetails attendee in attendeeDetails)
            {
                if (!string.IsNullOrEmpty(attendee.UserPrincipalName) && !string.IsNullOrEmpty(attendee.CalendarId))
                {
                    userOutlookDetails.Add(new UserOutlookDetail()
                    {
                        UserPrincipalName = attendee.UserPrincipalName,
                        CalendarId = attendee.CalendarId,
                        IsProcessed = false
                    });
                }
            }

            return userOutlookDetails;
        }

        private void GetRemainingScheduledAttendees()
        {
            List<UserScheduleBE> userSchedules = new UserScheduleBO().GetListByScheduleID(_ProjectSchedule.ProjectScheduleID);
            foreach (UserScheduleBE userScheduleBE in userSchedules)
            {
                AION.BL.UserIdentity identity = new UserIdentityModelBO().GetInstance(userScheduleBE.UserID.Value);

                _RemainingAttendees.Add(new AttendeeDetails()
                {
                    EmailId = identity.SrcSystemValueText,
                    FirstName = identity.FirstName,
                    LastName = identity.LastName,
                    IsRequired = true,
                    UserPrincipalName = identity.UserPrincipalName,
                    CalendarId = identity.CalendarId
                });
            }
        }

        private void SetMeetingRoom()
        {
            if (_Appointment != null && _Appointment.MeetingRoom != null)
            {
                if (_Appointment.MeetingRoom.MeetingRoomRefID.Value > 0)
                {
                    _MeetingRoom = new MeetingRoomBO().GetById(_Appointment.MeetingRoom.MeetingRoomRefID.Value);
                }
            }
        }

        private void SetAppointmentRecurrence()
        {
            if (_Appointment.GetType() == typeof(NonProjectAppointment))
            {
                NonProjectAppointment npa = (NonProjectAppointment)_Appointment;

                if (npa.AppoinmentRecurrenceRefID.HasValue)
                {
                    var appointmentRecurrenceRefBE = new AppoinmentReccuranceRefBO().GetById(npa.AppoinmentRecurrenceRefID.Value);
                    _AppointmentRecurrence = (AppointmentRecurrenceRefEnum)appointmentRecurrenceRefBE.EnumMappingValNbr;
                    _DayOfWeek = new System.DayOfWeek().CreateInstance(_AppointmentRecurrence);
                }
            }
            else
            {
                _AppointmentRecurrence = AppointmentRecurrenceRefEnum.NA;
                _DayOfWeek = null;
            }
        }

        private void SetHolidayRecurrence()
        {
            _AppointmentRecurrence = _HolidayConfig.HolidayAnnualRecurInd ? AppointmentRecurrenceRefEnum.Yearly : AppointmentRecurrenceRefEnum.Once;
        }

        private void AdjustMeetingRoomAttendance(string actionDescription, int transactionId, DateTime start, DateTime end)
        {
            OutlookAdapter outlookAdapter = new OutlookAdapter();

            List<MeetingRoom> meetingRooms = new MeetingRoomBO().GetListOfRooms(true);

            List<Event> events = new List<Event>();

            bool isAlreadyScheduled = false;

            foreach (MeetingRoom meetingRoom in meetingRooms)
            {
                Event evt = outlookAdapter.SearchForEventForUserCalendar(
                    meetingRoom.UserPrincipalName,
                    meetingRoom.CalendarId,
                    transactionId.ToString(),
                    start, end);

                if (evt != null)
                {
                    events.Add(evt);
                }
            }

            if (events.Count() > 0) // a room is already scheduled
            {
                if (_MeetingRoom != null)
                {
                    isAlreadyScheduled = events.Any(x => x.Organizer.EmailAddress.Address.Contains(_MeetingRoom.UserPrincipalName) ||
                        x.Organizer.EmailAddress.Address.Contains(_MeetingRoom.MeetingRoomEmail));
                }
            }

            switch (actionDescription)
            {
                case "Create":
                    if (!isAlreadyScheduled && _MeetingRoom != null)
                    {
                        AddMeetingRoomToListOfAttendees();
                    }
                    break;

                case "Delete":
                    if (isAlreadyScheduled)
                    {
                        if (!_RemainingAttendees.Any() && _MeetingRoom != null)
                        {
                            AddMeetingRoomToListOfAttendees();
                        }
                    }
                    break;
            }
        }

        private void AddMeetingRoomToListOfAttendees()
        {
            //if (ConfigurationManager.AppSettings["Environment"].ToUpper() == "PRD")
            //{
            _Attendees.Add(new AttendeeDetails()
            {
                EmailId = _MeetingRoom.MeetingRoomEmail,
                FirstName = string.Empty,
                LastName = string.Empty,
                IsRequired = true,
                UserPrincipalName = _MeetingRoom.UserPrincipalName,
                CalendarId = _MeetingRoom.CalendarId
            });
            //}
        }

        public bool GenerateCalendarEventQueueRecord()
        {
            bool success = false;
            try
            {
                OutlookAdapter outlookAdapter = new OutlookAdapter();

                ScheduleTime scheduleTime = new ScheduleTime();

                int transactionId = 0;

                if (_ProjectSchedule != null)
                {
                    scheduleTime = _ScheduleTimes.Where(x => x.StartDate.Date == ((DateTime)_ProjectSchedule.RecurringApptDt).Date).FirstOrDefault();

                    if (scheduleTime.StartDate == DateTime.MinValue)
                    {
                        scheduleTime = _ScheduleTimes.First();
                    }

                    transactionId = _ProjectSchedule.ProjectScheduleID;
                }
                else
                {
                    if (_HolidayConfig != null)
                    {
                        scheduleTime = _ScheduleTimes.Where(x => x.StartDate.Date == _HolidayConfig.HolidayDate.Date).FirstOrDefault();
                        transactionId = _HolidayConfig.HolidayConfigId;
                    }
                }

                string actionDescription = CalendarAppointmentAction.Create.ToString();
                if (_Appointment.IsCancellation)
                {
                    actionDescription = CalendarAppointmentAction.Delete.ToString();
                }

                bool isAllDay = false;

                // find the schedule time that matches the project schedule

                DateTime startDate = scheduleTime.StartDate;
                DateTime endDate = scheduleTime.EndDate;

                if (_MeetingRoom != null)
                {
                    AdjustMeetingRoomAttendance(actionDescription, transactionId, startDate, endDate);
                }

                var batchedAttendees = CreateAttendeeBatches(_NumberToBatch);

                List<UserOutlookDetail> userOutlookDetails = new List<UserOutlookDetail>();

                foreach (var batch in batchedAttendees)
                {
                    userOutlookDetails = GenerateOutlookDetails(batch);

                    string jsonObject = outlookAdapter.CreateAppointmentObject(
                    _AppointmentEmail.ScheduleSubject,
                    _AppointmentEmail.ScheduleMessage,
                    userOutlookDetails,
                    transactionId,
                    (_MeetingRoom != null) ? _MeetingRoom.MeetingRoomName : string.Empty,
                    startDate,
                    endDate,
                    isAllDay);

                    CalendarEventBE calendarEvent = new CalendarEventBE()
                    {
                        JsonObjectTxt = jsonObject,
                        ActionDesc = actionDescription,
                        ProcessedInd = false,
                        ProcessedDate = null,
                        InProcessInd = false,
                        InProcessDate = null,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        UserId = _Appointment.UserId,
                        RetryCount = 0
                    };

                    success = AddCalendarEventToQueue(calendarEvent);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in GenerateCalendarEventQueueRecord CalendarAppointmentAdapter - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }

            return success;
        }

        #region Private Methods

        private bool AddCalendarEventToQueue(CalendarEventBE calendarEventBE)
        {
            bool success = false;
            try
            {
                ICalendarEventAdapter calendarEventQueueAdapter = new CalendarEventAdapter();
                calendarEventQueueAdapter.AddCalendarEvent(calendarEventBE);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in AddCalendarEventToQueue CalendarAppointmentAdapter - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
            return success;
        }

        private List<AttendeeDetails> UpdateAttendeesWithUserIdentities(List<AttendeeDetails> attendeeDetails)
        {
            List<AttendeeDetails> attendees = new List<AttendeeDetails>();

            UserBO bo = new UserBO();

            foreach (AttendeeDetails attendee in attendeeDetails)
            {
                UserBE userBE = bo.GetByEmail(attendee.EmailId);

                if (userBE.UserID.HasValue)
                {
                    AttendeeDetails newAttendee = attendee;
                    newAttendee.UserId = userBE.UserID.Value;
                    newAttendee.UserPrincipalName = userBE.UserPrincipalName;
                    newAttendee.CalendarId = userBE.CalendarId;
                    attendees.Add(newAttendee);
                }
            }

            return attendees;
        }

        private AppointmentEmail GenerateHolidaySubjectAndMessage()
        {
            AppointmentEmail appointmentData = new AppointmentEmail();

            appointmentData.ScheduleSubject = _HolidayConfig.HolidayNm;

            appointmentData.ScheduleMessage = new EmailMessageBO().CreateHolidayConfigMessageBody();

            appointmentData.CancelSubject = _HolidayConfig.HolidayNm;
            appointmentData.CancelMessage = new EmailMessageBO().CreateHolidayConfigMessageBody();

            return appointmentData;
        }

        private AppointmentEmail GenerateAppointmentSubjectAndMessage()
        {
            AppointmentEmail appointmentData = new AppointmentEmail(_Appointment);
            return appointmentData;
        }

        #endregion
    }

    public interface ICalendarAppointmentAdapter
    {
        bool ProcessAppointments();
        bool GenerateCalendarEventQueueRecord();
    }
}