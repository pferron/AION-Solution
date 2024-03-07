using System;

namespace AION.BL
{
    public class PlanReviewScheduleDetail : ModelBase
	{
		public int? PlanReviewScheduleDetailId { get; set; }

		public int? PlanReviewScheduleId { get; set; }

		public int? BusinessRefId { get; set; }

		public DateTime? StartDt { get; set; }

		public DateTime? EndDt { get; set; }

		public bool? PoolRequestInd { get; set; }

		public bool? SameBuildContrInd { get; set; }

		public bool? ManualAssignmentInd { get; set; }

		public decimal? AssignedHoursNbr { get; set; }

		public int? AssignedPlanReviewerId { get; set; }
	}
}