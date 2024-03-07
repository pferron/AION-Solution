using AION.BL.Models;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using System;
using System.Collections.Generic;

namespace AION.BL.BusinessObjects
{
    public class FacilitatorModelBO : UserIdentityModelBO, IFacilitatorBO
    {
        public new Facilitator GetInstance(int id)
        {
            Facilitator ret = new Facilitator();
            base.InjectBaseObjects(id, ret);
            //ret.AssignedProjectsHours
            //ret.AssignedProjectCount
            //ret.MappedDepartmentTypeIds
            return ret;
        }

        public new Facilitator GetInstance(string external_Ref_info, ExternalSystemEnum externalSystemEnum)
        {
            Facilitator ret = new Facilitator();
            base.InjectBaseObjects(external_Ref_info, externalSystemEnum, ret);
            return ret;
        }

        public List<Facilitator> GetFacilitatorWorkloadSummary(DateTime startdate, DateTime enddate)
        {
            List<Facilitator> ret = new List<Facilitator>();
            FacilitatorBO bo = new FacilitatorBO();
            //add 1 day to end date to include that day
            List<FacilitatorBE> be = bo.GetFacilitatorworkloadSummary(startdate.Date, enddate.Date.AddDays(1));
            if (be == null || be.Count == 0)
                return new List<Facilitator>();
            foreach (var item in be)
            {
                Facilitator facilitator = new Facilitator();
                facilitator.FirstName = (item.UserID == -1) ? "Unassigned" : item.FirstNm;
                facilitator.LastName = item.LastNm;
                facilitator.AssignedProjectsHours = item.AssignedProjectsHours;
                ret.Add(facilitator);
            }
            return ret;

        }
        public Facilitator ConvertUserIdentityToFacilitator(UserIdentity userIdentity)
        {
            Facilitator facilitator = new Facilitator();
            facilitator = new Facilitator
            {
                ADName = userIdentity.ADName,
                CreatedDate = userIdentity.CreatedDate,
                DesignatedDepartments = userIdentity.DesignatedDepartments,
                DesignatedRoles = userIdentity.DesignatedRoles,
                CreatedUser = userIdentity.CreatedUser,
                Email = userIdentity.Email,
                ExternalSystemID = userIdentity.ExternalSystemID,
                FirstName = userIdentity.FirstName,
                HoursEstimated = userIdentity.HoursEstimated,
                ID = userIdentity.ID,
                IsActive = userIdentity.IsActive,
                IsExpressSched = userIdentity.IsExpressSched,
                IsModelUpdated = userIdentity.IsModelUpdated,
                IsPrelimMeetingAllowed = userIdentity.IsPrelimMeetingAllowed,
                IsSchedulable = userIdentity.IsSchedulable,
                IsValueCreated = userIdentity.IsValueCreated,
                Jurisdiction = userIdentity.Jurisdiction,
                LastName = userIdentity.LastName,
                Notes = userIdentity.Notes,
                Phone = userIdentity.Phone,
                PlanReviewOverrideHours = userIdentity.PlanReviewOverrideHours,
                SchedulableLevel = userIdentity.SchedulableLevel,
                SrcSystemValueText = userIdentity.SrcSystemValueText,
                UiSetting = userIdentity.UiSetting,
                UpdatedDate = userIdentity.UpdatedDate,
                UpdatedUser = userIdentity.UpdatedUser,
                UserName = userIdentity.UserName,
                UserPrincipalName = userIdentity.UserPrincipalName,
                CalendarId = userIdentity.CalendarId
            };
            return facilitator;
        }

    }

    public interface IFacilitatorBO
    {
        Facilitator GetInstance(int id);
        Facilitator GetInstance(string external_Ref_info, ExternalSystemEnum externalSystemEnum);

        List<Facilitator> GetFacilitatorWorkloadSummary(DateTime startdate, DateTime enddate);
    }
}
