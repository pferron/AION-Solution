using AION.BL;
using AION.BL.Adapters;
using AION.Email.Engine.BusinessObjects;
using AION.Manager.Models;
using Meck.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection;

namespace AION.Manager.Adapters
{
    public class ProjectManagerEmailAdapter : BaseManagerAdapter, IProjectManagerEmailAdapter
    {
        private Appointment _Appointment;

        private AppointmentEmail _AppointmentEmail;

        public ProjectManagerEmailAdapter(Appointment appointment)
        {
            _Appointment = appointment;
            _AppointmentEmail = GenerateAppointmentEmail();
        }

        public bool CreateAppointmentEmail()
        {
            try
            {
                if (_Appointment.ProjectEstimation == null
                    || String.IsNullOrWhiteSpace(_Appointment.ProjectEstimation.PMEmail)
                    || !_Appointment.ProjectEstimation.PMEmail.Contains("@"))
                    return false;

                EmailAdapter emailAdapter = new EmailAdapter();
                EmailMessageBO emailMessageBO = new EmailMessageBO();

                MailMessage mailMessage = emailAdapter.GetMailMessage();
                MailMessage cancelMessage = emailAdapter.GetMailMessage();

                mailMessage.To.Add(new MailAddress(_Appointment.ProjectEstimation.PMEmail));
                cancelMessage.To.Add(new MailAddress(_Appointment.ProjectEstimation.PMEmail));

                mailMessage.Subject = _AppointmentEmail.ScheduleSubject;
                mailMessage.Body = _AppointmentEmail.ScheduleMessage;

                if (_Appointment.IsCancellation || (_Appointment.IsReschedule && _Appointment.IsSubmit))
                {
                    cancelMessage.Subject = _AppointmentEmail.CancelSubject;

                    cancelMessage.Body = _AppointmentEmail.CancelMessage;

                    emailAdapter.SendEmailMessage(cancelMessage);
                }

                if (!_Appointment.IsCancellation && _Appointment.IsSubmit)
                {
                    //room not required
                    string meetingRoomName = _Appointment.MeetingRoom == null ? "" : _Appointment.MeetingRoom.MeetingRoomName;
                    emailAdapter.SendEmailMessage(mailMessage);
                    //save this notification
                    if (_Appointment.ProjectID != null)
                    {
                        List<int> userids = _Appointment.AttendeeDetails.Where(x => x.UserId > 0).Select(x => x.UserId).ToList();
                        List<string> pmEmails = new List<string>();
                        pmEmails.Add(_Appointment.ProjectEstimation.PMEmail);
                        SendProjectNotification sendProjectNotification = new SendProjectNotification
                        {
                            ProjectId = _Appointment.ProjectID.Value,
                            MailMessage = mailMessage,
                            SendDate = DateTime.Now,
                            WrkId = 1,
                            EmailNotif = _AppointmentEmail.EmailNotifType,
                            UserIds = userids,
                            EmailTxts = pmEmails
                        };
                        int notifId = emailAdapter.SaveNotificationEmail(sendProjectNotification);
                        if (notifId > 0)
                        {
                            //save the email list
                            sendProjectNotification.ProjectNotificationEmailId = notifId;
                            emailAdapter.SaveNotificationEmailSendList(sendProjectNotification);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in CreateAppointmentEmail - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }

            return true;
        }

        private AppointmentEmail GenerateAppointmentEmail()
        {
            AppointmentEmail appointmentData = new AppointmentEmail(_Appointment);
            return appointmentData;
        }
    }

    public interface IProjectManagerEmailAdapter
    {
        bool CreateAppointmentEmail();
    }
}