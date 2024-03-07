using AION.BL.Models;
using AION.Engine.BusinessEntities;
using AION.Manager.Models;
using System;
using System.Collections.Generic;

namespace AION.BL.Adapters
{
    public interface IExpressAdapter
    {
        ExpressModel GetExpressModel();
        int Upsert();
        bool ExpressReservations();
        List<ReserveExpressPlanReviewerBE> GetPlanReviewerList();
        List<ExpressSearchResult> GetReservationByDate(DateTime fromdate, DateTime todate);
        List<ExpressSearchResult> GetScheduledByDate(DateTime fromdate, DateTime todate);
        bool CheckIfDateIsHoliday(DateTime date);
        bool InsertUserSchedule(int projectScheduleId, ReserveExpressReservationBE reservation, ReserveExpressPlanReviewerBE planReviewerBE);
        bool UpdateAttendeeList(List<AttendeeInfo> attendeeIds, int expressId, int wkrId, int projectScheduleId, bool isSchedule, bool processInsertRemoveOnly);
        bool CancelReservations(List<ApptAttendeesManagerModel> cancellations);
        bool UpdateReservation(List<ReserveExpressReservation> reservations);
        List<ReserveExpressReservation> GetExpressReservationList();
        bool InsertExpressAttendees(List<AttendeeInfo> attendeeIds, int reserveId, int wkrId);
        List<ReserveExpressPlanReviewer> GetReserveExpressPlanReviewerListAll();
    }
}