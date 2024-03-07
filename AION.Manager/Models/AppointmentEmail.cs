using AION.BL.Adapters;
using AION.BL.BusinessObjects;
using AION.BL.Models;
using AION.Email.Engine.BusinessObjects;
using AIONEstimator.Engine.BusinessObjects;
using System;

namespace AION.BL
{
    public class AppointmentEmail
    {
        public string ScheduleSubject { get; set; }
        public string ScheduleMessage { get; set; }
        public string CancelSubject { get; set; }
        public string CancelMessage { get; set; }
        public EmailNotifType EmailNotifType { get;set; }

        private RecurrenceEnum _Recurrence;
        private DayOfWeek? _DayOfWeek;

        public AppointmentEmail() { }

        public AppointmentEmail(Appointment appointment)
        {
            string _accelaProjectRefId = string.Empty;
            string _projectName = string.Empty;
            string _projectAddress = string.Empty;
            string _tradeName = string.Empty;
            string _teamScore = string.Empty;
            string _apptDateTime = string.Empty;
            string _apptDateTimePrev = string.Empty;

            string _facilitatorName = string.Empty;
            string _facilitatorPhone = string.Empty;
            string _facilitatorEmail = string.Empty;

            if (appointment.ScheduleTimes.Count > 0)
            {
                appointment.StartDate = appointment.ScheduleTimes[0].StartDate;
                appointment.EndDate = appointment.ScheduleTimes[0].EndDate;
            }
            else
            {
                appointment.StartDate = appointment.StartDate;
                appointment.EndDate = appointment.EndDate;
            }

            _apptDateTime = appointment.StartDate.Value.ToShortDateString() + " " + appointment.StartDate.Value.ToShortTimeString() + " - " + appointment.EndDate.Value.ToShortTimeString();

            if (appointment.PrevStartDate.HasValue && appointment.PrevEndDate.HasValue)
            {
                _apptDateTimePrev = appointment.PrevStartDate.Value.ToShortDateString() + " " + appointment.PrevStartDate.Value.ToShortTimeString() + " - " + appointment.PrevEndDate.Value.ToShortTimeString();
            }
            else
            {
                _apptDateTimePrev = _apptDateTime;
            }

            if (appointment.ProjectEstimation != null)
            {
                int facilitatorId = appointment.ProjectEstimation.AssignedFacilitator.HasValue ? appointment.ProjectEstimation.AssignedFacilitator.Value : 0;
                Facilitator facilitator = new FacilitatorModelBO().GetInstance(facilitatorId);

                _accelaProjectRefId = appointment.ProjectEstimation.AccelaProjectRefId;
                _projectName = appointment.ProjectEstimation.ProjectName;
                _projectAddress = appointment.ProjectEstimation.DisplayOnlyInformation.ProjectAddress;
                _facilitatorName = $"{facilitator.FirstName} {facilitator.LastName}";
                _facilitatorPhone = facilitator.Phone;
                _facilitatorEmail = facilitator.Email;
            }

            string _meetingRoom = (appointment.MeetingRoom != null) ? appointment.MeetingRoom.MeetingRoomName : string.Empty;
            string _meetingType = (appointment.MeetingTypeEnum > 0) ? appointment.MeetingTypeEnum.ToStringValue() : string.Empty;
            string _notesHtml = string.Empty;

            EmailAdapter emailAdapter = new EmailAdapter();
            EmailMessageBO emailMessageBO = new EmailMessageBO();

            UserIdentity identity;
            if (appointment.UpdatedUser != null)
            {
                identity = new UserIdentityModelBO().GetInstance(appointment.UpdatedUser.ID);
            }
            else
            {
                identity = new UserIdentityModelBO().GetInstance(1);
            }

            if (appointment.GetType() == typeof(FacilitatorMeetingAppointment))
            {
                ScheduleSubject = "Facilitator Meeting Tentatively Scheduled for Project #" + _accelaProjectRefId + ": " + _projectName + " Trade: " + string.Empty + " Team Score: " +
                    string.Empty + " (Manually scheduled by " + identity.UserName + " on " + DateTime.Now.ToShortDateString() + ":" + DateTime.Now.ToShortTimeString() + ")";

                ScheduleMessage = emailAdapter.CreateMeetingScheduledMessageBody(
                   _accelaProjectRefId, _projectName, _projectAddress,
                   appointment.StartDate.Value, _meetingRoom, appointment.MeetingTypeEnum,
                   _facilitatorName, _facilitatorPhone, _facilitatorEmail);

                CancelSubject = string.Format("Facilitator Meeting Tentatively Scheduled Date Cancelled for Project # {0} ({1})", identity.UserName, _projectName);
                CancelMessage = emailMessageBO.CancelMeetingScheduledMessageBody(
                    _accelaProjectRefId, _projectName, _projectAddress,
                    _apptDateTimePrev, _meetingRoom, _meetingType,
                    _facilitatorName, _facilitatorPhone, _facilitatorEmail);

                EmailNotifType = EmailNotifType.Meeting_Tentative_Scheduled;
            }

            if (appointment.GetType() == typeof(NonProjectAppointment))
            {
                NonProjectAppointment npa = ((NonProjectAppointment)appointment);
                _Recurrence = new RecurrenceEnum().CreateInstance(npa.AppointmentRecurrence);
                _DayOfWeek = new DayOfWeek().CreateInstance(npa.AppointmentRecurrence);

                string name = npa.AppoinmentName;
                string npatype = new NpaTypeRefBO().GetById(npa.NPATypeRefID.Value).AppointmentTypeName;
                string npaDate = "From: " + npa.AppointmentFrom.Value.ToShortDateString() + " To: " + npa.AppointmentTo.Value.ToShortDateString();
                string npaHours = npa.AppointmentFrom.Value.ToShortTimeString() + " - " + npa.AppointmentTo.Value.ToShortTimeString();

                ScheduleSubject = ((NonProjectAppointment)appointment).AppoinmentName;
                ScheduleMessage = new EmailMessageBO().CreateNPAMessageBody(name, npatype, npaDate, npaHours, _meetingRoom);

                EmailNotifType = EmailNotifType.Meeting_Tentative_Scheduled;
            }

            if (appointment.GetType() == typeof(PreliminaryMeetingAppointment))
            {
                ScheduleSubject = "Preliminary Project #" + _accelaProjectRefId + ": " + _projectName + " Trade: " + _tradeName + " Team Score: " +
                    _teamScore + " (Manually scheduled by " + identity.UserName + " on " + DateTime.Now.ToShortDateString() + ":" + DateTime.Now.ToShortTimeString() + ")";

                ScheduleMessage = emailAdapter.CreatePrelimMeetingScheduledEmailBody(
                    _accelaProjectRefId, _projectName, _projectAddress, appointment.StartDate.Value,
                    _meetingRoom, _notesHtml, _facilitatorName, _facilitatorPhone, _facilitatorEmail, appointment.ProjectEstimation);

                CancelSubject = string.Format("Preliminary Meeting Tentatively Scheduled Date Cancelled for Project # {0} ({1})", identity.UserName, _projectName);
                CancelMessage = emailMessageBO.CreatePMACancelledMessageBody(_accelaProjectRefId, _projectAddress, _apptDateTimePrev, _facilitatorName, _facilitatorPhone, _facilitatorEmail);

                EmailNotifType = EmailNotifType.Preliminary_Tentative_Scheduled;
            }

            if (appointment.GetType() == typeof(ReserveExpressReservation))
            {
                ScheduleSubject = "Reserve Express Reservation";
                ScheduleMessage = emailMessageBO.CreateReserveExpressReservationMessageBody();

                _Recurrence = RecurrenceEnum.Once;

                EmailNotifType = EmailNotifType.Express_Tentative_Scheduled;
            }

            if (appointment.GetType() == typeof(ExpressMeetingAppointment))
            {
                ExpressMeetingAppointment ema = ((ExpressMeetingAppointment)appointment);

                ScheduleSubject = "Express Review Tentatively Scheduled for Project #" + _accelaProjectRefId + " (" + _projectName + ")";
                ScheduleMessage = emailAdapter.CreateExpressScheduledMessageBody(
                    _accelaProjectRefId,
                    _projectName,
                    _projectAddress,
                    appointment.StartDate.Value,
                    _meetingRoom,
                    _facilitatorName,
                    _facilitatorPhone,
                    _facilitatorEmail
                    );

                CancelSubject = string.Format("Review tentatively scheduled date cancelled for Project # {0} ({1})", _accelaProjectRefId, _projectName);
                CancelMessage = emailMessageBO.CancelPlanReviewScheduledMessageBody(
                    _accelaProjectRefId,
                    _projectName,
                    _projectAddress,
                    appointment.PrevStartDate.ToString(),
                    _facilitatorName,
                    _facilitatorPhone,
                    _facilitatorEmail
                    );

                _Recurrence = RecurrenceEnum.Once;

                EmailNotifType = EmailNotifType.Express_Tentative_Scheduled;
            }
        }
    }
}