using AION.BL;
using AION.BL.Models;
using System;
using System.Collections.Generic;

namespace AION.Manager.Models
{
    public class ScheduleBusinessRelationship : ModelBase
    {
		public int? ScheduleBusinessRelationshipId { get; set; }

		public int? BusinessRefId { get; set; }

		public int? ProjectId { get; set; }

		public decimal? ReReviewHours { get; set; }

		public decimal? ProposedHours { get; set; }

		public int? ProposedPlanReviewerId { get; set; }

		public int? Cycle { get; set; }
	}
}