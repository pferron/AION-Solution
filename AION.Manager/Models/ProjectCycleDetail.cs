using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AION.BL
{
    public class ProjectCycleDetail : ModelBase
    {
		public int? ProjectCycleDetailId { get; set; }

		public int? ProjectCycleId { get; set; }

		public int? BusinessRefId { get; set; }

		public decimal? RereviewHoursNbr { get; set; }
	}
}