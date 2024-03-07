using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AION.Manager.Models
{
    public class ProjectRtapMapping
    {
		public int? ProjectRtapMappingId { get; set; }

		public int? ProjectId { get; set; }

		public int? OriginalProjectId { get; set; }

		public string OriginalProjectNumber { get; set; }
	}
}