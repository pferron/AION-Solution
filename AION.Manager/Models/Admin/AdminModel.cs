using AION.BL;
using AION.BL.Models;
using System.Collections.Generic;

namespace AION.Manager.Models
{
    public class AdminModel
    {
        public List<HolidayConfig> HolidayConfigList { get; set; } = new List<HolidayConfig>();
        public List<SystemRole> SystemRolesWithPermissions { get; set; } = new List<SystemRole>();
        public List<Permission> PermissionsList { get; set; } = new List<Permission>();
        public List<PlanReviewerAvailableHour> PlanReviewerAvailableHours { get; set; } = new List<PlanReviewerAvailableHour>();
        public List<PlanReviewerAvailableTime> PlanReviewerAvailableTimes { get; set; } = new List<PlanReviewerAvailableTime>();
        public List<CatalogItem> CatalogItems { get; set; } = new List<CatalogItem>();
        public List<Reviewer> AllReviewers { get; set; } = new List<Reviewer>();
        public List<NpaType> NpaTypes { get; set; } = new List<NpaType>();
        public List<MessageTemplateType> MessageTemplateTypes { get; set; } = new List<MessageTemplateType>();
        public List<MessageTemplateDataElement> MessageTemplateDataElements { get; set; } = new List<MessageTemplateDataElement>();
        public List<DefaultEstimationHour> DefaultEstimationHours { get; set; } = new List<DefaultEstimationHour>();
        public List<ProjectType> ProjectTypeList { get; set; } = new List<ProjectType>();
    }
}