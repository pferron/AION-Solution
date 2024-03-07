namespace AION.BL.Models
{
    public class EstimatorUIModel : UserIdentity
    {
        public double AssignedProjectsHours { get; set; }
        public int AssignedProjectCount { get; set; }

        public EstimatorUIModel ConvertUserIdentityToEstimator(UserIdentity userIdentity)
        {
            EstimatorUIModel estimator = new EstimatorUIModel();
            estimator = new EstimatorUIModel
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
            return estimator;
        }
    }
}
