using AION.BL;
using AION.BL.Adapters;
using AION.Manager.Models;
using System.Collections.Generic;

namespace AION.Manager.Adapters
{
    public interface IEMAAdapter
    {
        bool UpsertEMA(PlanReview planReview);
        bool SendNotifications();
        ExpressMeetingAppointment ConvertPlanReviewToEMA(PlanReview planReview);
        void SendAppointmentNotifications(List<AttendeeDetails> attendeeDetails, bool isCancellation);
    }
}