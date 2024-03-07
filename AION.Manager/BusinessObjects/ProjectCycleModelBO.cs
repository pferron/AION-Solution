using AION.Engine.BusinessEntities;
using AIONEstimator.Engine.BusinessObjects;
using System;

namespace AION.BL.BusinessObjects
{
    public class ProjectCycleModelBO : ModelBaseModelBO
    {
        public ProjectCycle GetInstance(int ID)
        {
			ProjectCycle ret = new ProjectCycle();
            ProjectCycleBO bo = new ProjectCycleBO();
			ProjectCycleBE be = bo.GetById(ID);
            if (be == null || be.ProjectCycleId == 0)
                return new ProjectCycle();
            return ConvertBEToModel(be);
        }

		public ProjectCycle ConvertBEToModel(ProjectCycleBE be)
		{
			var easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

			ProjectCycle ret = new ProjectCycle();
			InjectBaseObjects(ret, be.ProjectCycleId.Value, be.CreatedDate.Value, be.UpdatedDate.Value, be.CreatedByWkrId, be.UpdatedByWkrId);
			ret.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc((DateTime)ret.CreatedDate, easternZone);
			ret.UpdatedDate = TimeZoneInfo.ConvertTimeFromUtc((DateTime)ret.UpdatedDate, easternZone);
			ret.CreatedUser = new UserIdentityModelBO().GetInstance(int.Parse(be.CreatedByWkrId));
			ret.ID = be.ProjectCycleId.Value;
			ret.ProjectCycleId = be.ProjectCycleId.Value;
			ret.ProjectId = be.ProjectId;
			ret.CurrentCycleInd = be.CurrentCycleInd.HasValue ? be.CurrentCycleInd.Value : false;
			ret.FutureCycleInd = be.FutureCycleInd.HasValue ? be.FutureCycleInd.Value : false;
			ret.CycleNbr = be.CycleNbr.HasValue ? be.CycleNbr.Value : 1;
			ret.PlansReadyOnDt = be.PlansReadyOnDt;
			ret.IsCompleteInd = be.IsCompleteInd.HasValue ? be.IsCompleteInd.Value : false;
			ret.GateDt = be.GateDt;
			ret.ScheduleAfterDt = be.ScheduleAfterDt;
			ret.ResponderUserId = be.ResponderUserId;
			ret.IsAprvInd = be.IsAprvInd;
			ret.ResponseDt = be.ResponseDt;

			return ret;
		}
	}
}