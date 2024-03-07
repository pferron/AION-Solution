using AION.Manager.Models;
using AION.Web.BusinessEntities;
using System.Collections.Generic;

namespace AION.BL
{
    public class UserIdentity : ModelBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string SrcSystemValueText { get; set; }

        public int ExternalSystemID { get; set; }

        public List<SystemRole> DesignatedRoles { get; set; }
        public List<Department> DesignatedDepartments { get; set; }
        public bool IsActive { get; set; }
        public string UiSetting { get; set; }

        public bool IsValueCreated { get; set; }

        /// <summary>
        /// Indicates if user can be scheduled for Express plan reviews
        /// </summary>
        public bool IsExpressSched { get; set; }

        public string UserName { get; set; }

        public string ADName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Notes { get; set; }

        public bool IsSchedulable { get; set; }

        public decimal PlanReviewOverrideHours { get; set; }

        public string HoursEstimated { get; set; }

        public int Jurisdiction { get; set; }

        public string SchedulableLevel { get; set; }

        public bool IsPrelimMeetingAllowed { get; set; }

        public string UserPrincipalName { get; set; }

        public string CalendarId { get; set; }

        public bool IsCity { get; set; }
        public PermissionMapping PermissionMapping { get; set; }
    }


}
