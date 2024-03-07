using AION.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.Models;
using AIONEstimator.Engine.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AION.BL.BusinessObjects
{
	public class PlanReviewScheduleModelBO : ModelBaseModelBO
	{
		public PlanReviewSchedule GetInstance(int ID)
		{
			PlanReviewSchedule ret = new PlanReviewSchedule();
			PlanReviewScheduleBO bo = new PlanReviewScheduleBO();
			PlanReviewScheduleBE be = bo.GetById(ID);
			if (be == null || be.PlanReviewScheduleId == 0)
				return new PlanReviewSchedule();
			return ConvertBEToModel(be);
		}

		public PlanReviewSchedule ConvertBEToModel(PlanReviewScheduleBE be)
		{
			var easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

			PlanReviewSchedule ret = new PlanReviewSchedule();
			InjectBaseObjects(ret, be.PlanReviewScheduleId.Value, be.CreatedDate.Value, be.UpdatedDate.Value, be.CreatedByWkrId, be.UpdatedByWkrId);
			ret.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc((DateTime)ret.CreatedDate, easternZone);
			ret.UpdatedDate = TimeZoneInfo.ConvertTimeFromUtc((DateTime)ret.UpdatedDate, easternZone);
			ret.CreatedUser = new UserIdentityModelBO().GetInstance(int.Parse(be.CreatedByWkrId));
			ret.ID = be.PlanReviewScheduleId.Value;
			ret.PlanReviewScheduleId = be.PlanReviewScheduleId.Value;
			ret.ApptCancellationRefId = be.ApptCancellationRefId;
			ret.ApptResponseStatusRefId = be.ApptResponseStatusRefId;
			ret.IsRescheduleInd = be.IsRescheduleInd;
			ret.ProjectCycleId = be.ProjectCycleId;
			ret.ProjectScheduleTypDesc = be.ProjectScheduleTypDesc;
			ret.CancelAfterDt = be.CancelAfterDt;
			ret.VirtualMeetingInd = be.VirtualMeetingInd;
			ret.Proposed1Dt = be.Proposed1Dt;
			ret.Proposed2Dt = be.Proposed2Dt;
			ret.Proposed3Dt = be.Proposed3Dt;
			ret.MeetingRoomRefId = be.MeetingRoomRefId;

			return ret;
		}

		public List<ExpressSearchResult> GetExpressSearchResults(DateTime fromDate, DateTime toDate)
        {
			PlanReviewScheduleBO bo = new PlanReviewScheduleBO();
			List<PlanReviewScheduleBE> planReviewSchedules = bo.GetListByDates(fromDate, toDate, "EMA");

			PlanReviewScheduleDetailBO detailBO = new PlanReviewScheduleDetailBO();
			List<PlanReviewScheduleDetailBE> scheduleDetails = new List<PlanReviewScheduleDetailBE>();

			ProjectCycleBO cycleBO = new ProjectCycleBO();
			ProjectCycleBE cycleBE = new ProjectCycleBE();

			ProjectBO projectBO = new ProjectBO();
			ProjectBE projectBE = new ProjectBE();

			ProjectScheduleBO psBO = new ProjectScheduleBO();
			ProjectScheduleBE psBE = new ProjectScheduleBE();

			ProjectScheduleModelBO projectScheduleModelBO = new ProjectScheduleModelBO();
			List<ProjectSchedule> projectSchedules = new List<ProjectSchedule>();

			List<ExpressSearchResult> results = new List<ExpressSearchResult>();

			foreach (PlanReviewScheduleBE planReviewSchedule in planReviewSchedules)
            {
				List<AttendeeInfo> attendees = new List<AttendeeInfo>();

				cycleBE = cycleBO.GetById(planReviewSchedule.ProjectCycleId.Value);

				projectBE = projectBO.GetById(cycleBE.ProjectId.Value);

				List<ProjectScheduleBE> projectScheduleBEs = psBO.GetByApptId(planReviewSchedule.PlanReviewScheduleId.Value, "EMA");
				foreach (ProjectScheduleBE be in projectScheduleBEs)
                {
					projectSchedules.Add(projectScheduleModelBO.ConvertBEToModel(be));
                }

				scheduleDetails = detailBO.GetListByPlanReviewScheduleId(planReviewSchedule.PlanReviewScheduleId.Value);
				DateTime start = scheduleDetails.FirstOrDefault().StartDt.Value;
				DateTime end = scheduleDetails.FirstOrDefault().EndDt.Value;

				foreach (PlanReviewScheduleDetailBE detail in scheduleDetails)
                {
					if (detail.AssignedPlanReviewerId.HasValue && detail.AssignedPlanReviewerId.Value > 0)
					{
						UserIdentity user = new UserIdentityModelBO().GetInstance(detail.AssignedPlanReviewerId.Value);

						attendees.Add(new AttendeeInfo()
						{
							AttendeeId = detail.AssignedPlanReviewerId.Value,
							BusinessRefId = detail.BusinessRefId.Value,
							DeptNameEnumId = detail.BusinessRefId.Value,
							FirstName = user.FirstName,
							LastName = user.LastName
						});
					}
                }

				DateTime? expressDate = start;
				string expressDateString = expressDate.HasValue ? expressDate?.ToString("MM/dd/yyyy") : "";
				string day = expressDate.HasValue ? expressDate?.DayOfWeek.ToString() : "";

				TimeSpan startTime = start.TimeOfDay;
				TimeSpan endTime = end.TimeOfDay;

				var distinctAttendees = attendees.GroupBy(x => new { x.AttendeeId, x.BusinessRefId }).Select(y => y.First());

				ExpressSearchResult result = new ExpressSearchResult()
				{
					ExpressId = planReviewSchedule.PlanReviewScheduleId,
					ExpressDate = expressDateString,
					Day = day,
					MeetingRoomRefId = planReviewSchedule.MeetingRoomRefId,
					MeetingRoomName = planReviewSchedule.MeetingRoomRefId != null ? new MeetingRoomBO().GetById(planReviewSchedule.MeetingRoomRefId.Value).MeetingRoomName : string.Empty,
					Attendees = distinctAttendees.ToList(),
					StartTime = startTime,
					EndTime = endTime,
					Time = $"{start.ToString("HH:mm")} - {end.ToString("HH:mm")}",
					ProjectId = projectBE.SrcSystemValTxt,
					ProjectScheduleID = projectScheduleBEs.FirstOrDefault().ProjectScheduleID.Value,
					RecIdTxt = projectBE.RecIdTxt,
					Schedules = projectSchedules
				};

				results.Add(result);
            }

			return results;
		}
	}
}