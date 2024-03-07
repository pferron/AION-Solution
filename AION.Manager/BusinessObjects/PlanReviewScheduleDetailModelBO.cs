using AION.Engine.BusinessEntities;
using AIONEstimator.Engine.BusinessObjects;
using System;

namespace AION.BL.BusinessObjects
{
    public class PlanReviewScheduleDetailModelBO : ModelBaseModelBO
	{
		public PlanReviewScheduleDetail GetInstance(int ID)
		{
			PlanReviewScheduleDetail ret = new PlanReviewScheduleDetail();
			PlanReviewScheduleDetailBO bo = new PlanReviewScheduleDetailBO();
			PlanReviewScheduleDetailBE be = bo.GetById(ID);
			if (be == null || be.PlanReviewScheduleDetailId == 0)
				return new PlanReviewScheduleDetail();
			return ConvertBEToModel(be);
		}

		public PlanReviewScheduleDetail ConvertBEToModel(PlanReviewScheduleDetailBE be)
		{
			var easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

			PlanReviewScheduleDetail ret = new PlanReviewScheduleDetail();
			InjectBaseObjects(ret, be.PlanReviewScheduleDetailId.Value, be.CreatedDate.Value, be.UpdatedDate.Value, be.CreatedByWkrId, be.UpdatedByWkrId);
			ret.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc((DateTime)ret.CreatedDate, easternZone);
			ret.UpdatedDate = TimeZoneInfo.ConvertTimeFromUtc((DateTime)ret.UpdatedDate, easternZone);
			ret.CreatedUser = new UserIdentityModelBO().GetInstance(int.Parse(be.CreatedByWkrId));
			ret.ID = be.PlanReviewScheduleDetailId.Value;
			ret.PlanReviewScheduleDetailId = be.PlanReviewScheduleDetailId.Value;
			ret.AssignedHoursNbr = be.AssignedHoursNbr;
			ret.AssignedPlanReviewerId = be.AssignedPlanReviewerId;
			ret.BusinessRefId = be.BusinessRefId;
			ret.EndDt = be.EndDt;
			ret.ManualAssignmentInd = be.ManualAssignmentInd;
			ret.PlanReviewScheduleId = be.PlanReviewScheduleId;
			ret.PoolRequestInd = be.PoolRequestInd;
			ret.SameBuildContrInd = be.SameBuildContrInd;
			ret.StartDt = be.StartDt;

			return ret;
		}
	}
}