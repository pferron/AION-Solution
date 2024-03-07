using AION.Engine.BusinessEntities;
using AIONEstimator.Engine.BusinessObjects;
using System;

namespace AION.BL.BusinessObjects
{
    public class ProjectCycleDetailModelBO : ModelBaseModelBO
    {
        public ProjectCycleDetail GetInstance(int ID)
        {
			ProjectCycleDetail ret = new ProjectCycleDetail();
            ProjectCycleDetailBO bo = new ProjectCycleDetailBO();
			ProjectCycleDetailBE be = bo.GetById(ID);
            if (be == null || be.ProjectCycleId == 0)
                return new ProjectCycleDetail();
            return ConvertBEToModel(be);
        }

		public ProjectCycleDetail ConvertBEToModel(ProjectCycleDetailBE be)
		{
			var easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

			ProjectCycleDetail ret = new ProjectCycleDetail();
			InjectBaseObjects(ret, be.ProjectCycleId.Value, be.CreatedDate.Value, be.UpdatedDate.Value, be.CreatedByWkrId, be.UpdatedByWkrId);
			ret.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc((DateTime)ret.CreatedDate, easternZone);
			ret.UpdatedDate = TimeZoneInfo.ConvertTimeFromUtc((DateTime)ret.UpdatedDate, easternZone);
			
			ret.CreatedUser = new UserIdentityModelBO().GetInstance(int.Parse(be.CreatedByWkrId));
			ret.ID = be.ProjectCycleDetailId.Value;
			ret.ProjectCycleDetailId = be.ProjectCycleDetailId.Value;
			ret.ProjectCycleId = be.ProjectCycleId;
			ret.BusinessRefId = be.BusinessRefId;
			ret.RereviewHoursNbr = be.RereviewHoursNbr;

			return ret;
		}
	}
}