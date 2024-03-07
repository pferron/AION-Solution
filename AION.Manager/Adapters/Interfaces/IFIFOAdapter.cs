using AION.BL.Models;
using AION.Engine.BusinessEntities;
using AION.Manager.Models;
using System.Collections.Generic;

namespace AION.Manager.Adapters
{
    public interface IFIFOAdapter
    {
        bool ProcessFIFOScheduling(FIFOEngineParams model);
        bool UpsertFIFO(PlanReview item);
        bool InsertFIFOAttendees(
            List<AttendeeInfo> attendeeIds,
            int scheduleId,
            int WkrId,
            PlanReviewScheduleDetailBE scheduleDetail,
            int projectId,
            int? projectScheduleId = null);
    }
}