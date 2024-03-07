using System.Collections.Generic;

namespace AION.BL.Models.Base
{

    public abstract class ProjectDepartment : ModelBase
    {
        /// <summary>
        /// AION Accela project status
        /// </summary>
        public ProjectStatus ProjectStatus { get; set; }

        /// <summary>
        /// Calculated field stored in AION
        /// </summary>
        public decimal? EstimationHours { get; set; }

        /// <summary>
        /// Project object loaded from AION and Accela
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// Agency, Trade, Other from Accela
        /// </summary>
        public DepartmentTypeEnum DepartmentTypeEnum { get; set; }

        /// <summary>
        ///  Agency, Trade, Other from Accela
        /// </summary>
        public DepartmentNameEnums DepartmentInfo { get; set; }


        /// <summary>
        /// Assigned Plan Reviewer from Accela
        /// </summary>
        public UserIdentity AssignedPlanReviewer { get; set; }

        /// <summary>
        /// Primary Plan Reviewer from Accela
        /// </summary>
        public UserIdentity PrimaryPlanReviewer { get; set; }

        /// <summary>
        /// Secondary Plan Reviewer from Accela
        /// </summary>
        public UserIdentity SecondaryPlanReviewer { get; set; }

        /// <summary>
        /// Proposed Plan Reviewer from Accela
        /// </summary>
        public UserIdentity ProposedPlanReviewer { get; set; }

        /// <summary>
        /// Excluded Plan Reviewers list from Accela
        /// </summary>
        public List<int> ExcludedPlanReviewers { get; set; }

        /// <summary>
        /// Stores value regarding the specific department is set explicitly applicable from UI. This is only set from UI and not anywhere else.
        /// </summary>
        private bool _EstimationNotApplicable;

        public bool EstimationNotApplicable
        {
            get
            {
                return _EstimationNotApplicable;
            }
            set
            {
                _EstimationNotApplicable = value;
            }
        }


        //public bool EstimationNotApplicable { get; set; }


        /// <summary>
        /// Blank, P, L, C, CR etc from constant Meck.Shared.ProjectDisplayStatus
        /// </summary>
        public string DepartmentStatus { get; set; }
        public ProjectStatus DepartmentStatusRef { get; set; }
        public DepartmentDivisionEnum DepartmentDivision { get; set; }
        public bool IsDeptRequested { get; set; }

        /// <summary>
        /// Audit Action for insert project audit processing
        /// </summary>
        public AuditActionEnum AuditAction { get; set; }
    }
}
