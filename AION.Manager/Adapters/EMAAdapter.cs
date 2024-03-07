using AION.BL;
using AION.BL.Adapters;
using AION.BL.BusinessObjects;
using AION.Manager.Models;
using Meck.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AION.Manager.Adapters
{
    public class EMAAdapter : AppointmentAdapter, IAppointmentAdapter, IEMAAdapter
    {
        private ExpressMeetingAppointment _ema;
        private ProjectCycleSummary _ProjectCycleSummary;

        public EMAAdapter() { }

        public EMAAdapter(ExpressMeetingAppointment ema)
        {
            _ema = ema;

            SetData();
        }

        public bool UpsertEMA(PlanReview planReview)
        {
            bool success = false;

            try
            {
                IPlanReviewAdapter planReviewAdapter = new PlanReviewAdapter();

                success = planReviewAdapter.UpsertPlanReview(planReview);

                _ProjectCycleSummary = planReviewAdapter.GetProjectCycleSummary(planReview.ProjectId);

                ConvertPlanReviewToEMA(_ProjectCycleSummary.PlanReviewCurrent);

                SetData();

                ManageAppointment();
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in EMAAdapter UpsertEMA - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            return success;
        }

        public new void SendAppointmentNotifications(List<AttendeeDetails> attendeeDetails, bool isCancellation)
        {
            base.SendAppointmentNotifications(attendeeDetails, isCancellation);
        }

        public bool SendNotifications()
        {
            return base.SendCalendarNotifications();
        }
        public ExpressMeetingAppointment ConvertPlanReviewToEMA(PlanReview planReview)
        {
            List<AttendeeInfo> attendees = planReview.AssignedReviewers;

            if (planReview.NewAttendees != null && planReview.NewAttendees.Count > 0)
            {
                attendees.AddRange(planReview.NewAttendees);
            }

            _ema = new ExpressMeetingAppointment()
            {
                ApptResponseStatusEnum = planReview.ApptResponseStatusEnum,
                ApptResponseStatusRefId = planReview.ApptResponseStatusRefId,
                AssignedFacilitator = planReview.AssignedFacilitator,
                AssignedReviewers = planReview.AssignedReviewers,
                NewAttendees = planReview.AssignedReviewers,
                CreatedDate = planReview.CreatedDate,
                CreatedUser = planReview.CreatedUser,
                Cycle = planReview.Cycle,
                EndDate = planReview.EndDate,
                ExpressMeetingApptID = planReview.PlanReviewScheduleId,
                FromDT = planReview.StartDate,
                ID = planReview.PlanReviewScheduleId.Value,
                IsReschedule = planReview.IsReschedule,
                IsCancellation = planReview.ApptResponseStatusEnum == AppointmentResponseStatusEnum.Cancelled ? true : false,
                MeetingRoom = planReview.MeetingRoom,
                MeetingRoomRefId = planReview.MeetingRoomRefId,
                VirtualMeetingInd = planReview.VirtualMeetingInd,
                MeetingTypeEnum = MeetingTypeEnum.Express,
                MeetingTypeRefId = (int)MeetingTypeEnum.Express,
                ProjectID = planReview.ProjectId,
                StartDate = planReview.StartDate,
                ToDT = planReview.EndDate,
                UpdatedUser = planReview.UpdatedUser,
                UpdatedDate = planReview.UpdatedDate,
                UserId = planReview.UpdatedUser.ID.ToString()
            };

            return _ema;
        }


        #region Private Methods

        private void SetData()
        {
            if (_ema.ApptResponseStatusEnum > 0)
            {
                AppointmentResponseStatus appointmentResponseStatus = new AppointmentResponseStatusModelBO().GetInstance(_ema.ApptResponseStatusEnum);
                _ema.ApptResponseStatusRefId = appointmentResponseStatus.ApptResponseStatusRefId;
            }

            _Appointment = _ema;

            SetAppointmentData();
        }

        private bool ManageAppointment()
        {
            if (_Appointment.IsReschedule && _Appointment.IsSubmit)
            {
                //get previous scheduled dates
                PlanReview previousPlanReview = _ProjectCycleSummary.PlanReviewPrevious;

                if (previousPlanReview != null & previousPlanReview.ID > 0)
                {
                    //set previous dates for cancellation email
                    _Appointment.PrevStartDate = previousPlanReview.StartDate;
                    _Appointment.PrevEndDate = previousPlanReview.EndDate;
                }
            }

            SendAppointmentNotifications(_Appointment.IsCancellation);

            return true;
        }

        #endregion Private Methods
    }
}