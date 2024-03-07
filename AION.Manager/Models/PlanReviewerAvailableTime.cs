using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AION.BL.Models
{
    public class PlanReviewerAvailableTime : ModelBase
    {
		public DateTime AvailableStartTime { get; set; }

		public DateTime AvailableEndTime { get; set; }

		public string ProjectTypeDesc { get; set; }

		public PropertyTypeEnums ProjectTypeRefID { get; set; }
	}
}