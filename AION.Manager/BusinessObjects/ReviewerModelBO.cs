using AION.BL.Models;

namespace AION.BL.BusinessObjects
{
    public class ReviewerModelBO : UserIdentityModelBO, IReviewerBO
    {

        public new Reviewer GetInstance(int id)
        {
            Reviewer ret = new Reviewer();
            base.InjectBaseObjects(id, ret);
            //ret.AssignedProjectsHours
            //ret.AssignedProjectCount
            return ret;
        }

        public new Reviewer GetInstance(string external_Ref_info, ExternalSystemEnum externalSystemEnum)
        {
            Reviewer ret = new Reviewer();
            base.InjectBaseObjects(external_Ref_info, externalSystemEnum, ret);
            return ret;
        }

        public Reviewer ConvertUserIdentityToReviewer(UserIdentity userIdentity)
        {
            Reviewer reviewer = new Reviewer();
            reviewer = new Reviewer
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
                UserName = userIdentity.UserName
            };
            return reviewer;
        }
    }

    public interface IReviewerBO
    {
        Reviewer GetInstance(int id);
        Reviewer GetInstance(string external_Ref_info, ExternalSystemEnum externalSystemEnum);
    }
}
