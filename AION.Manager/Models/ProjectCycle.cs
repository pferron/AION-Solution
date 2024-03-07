using System;
using System.Collections.Generic;

namespace AION.BL
{
    public class ProjectCycle : ModelBase
	{
        public ProjectCycle()
        {
            IsResponderCustomer = false;
        }

		public int? ProjectCycleId { get; set; }

		public int? ProjectId { get; set; }

		public bool? CurrentCycleInd { get; set; }

		public bool? FutureCycleInd { get; set; }

		public int? CycleNbr { get; set; }

		public DateTime? PlansReadyOnDt { get; set; }

		public bool? IsCompleteInd { get; set; }

		public DateTime? GateDt { get; set; }

		public DateTime? ScheduleAfterDt { get; set; }

		public int? ResponderUserId { get; set; }

        public bool IsResponderCustomer { get; set; }

        public string AuditText { get; set; }

		public bool? IsAprvInd { get; set; }

		public DateTime? ResponseDt { get; set; }

        public string IsApprovedText
        {
            get
            {
                string ret = "";
                if (IsAprvInd.HasValue)
                {
                    ret = IsAprvInd.Value == true ? "Accepted" : "Rejected";
                }
                else
                {
                    ret = "";
                }
                return ret;
            }
        }

        public bool HasEMAPlanReview { get; set; } = false;

        public List<ProjectCycleDetail> ProjectCycleDetails { get; set; }
    }
}