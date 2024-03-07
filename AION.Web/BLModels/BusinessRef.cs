using AION.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AION.Web.BLModels
{
    public class BusinessRef : ModelBase
    {
   		public string BusinessName { get; set; }

		public int? BusinessRefId { get; set; }

		public string BusinessShortDesc { get; set; }

		public int? BusinessTypeRefId { get; set; }

		public int? DisionRefId { get; set; }

		public int? EnumMappingNumber { get; set; }

		public int? ExternalSystemRefId { get; set; }

		public int? RegionRefId { get; set; }

		public string SourceSystemValueText { get; set; }
	}
}