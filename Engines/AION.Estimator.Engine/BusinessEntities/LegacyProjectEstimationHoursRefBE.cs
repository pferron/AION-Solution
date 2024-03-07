#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Estimator.Engine.BusinessEntities
{

	#region BusinessEntitiy - LegacyProjectEstimationHoursRefBE

	[DataContract]
	public class LegacyProjectEstimationHoursRefBE : BaseBE
	{

		#region Properties

		[DataMember]
		public int? LegacyProjectEstimationHoursRefId { get; set; }

		[DataMember]
		public int? OccupancyTypRefId { get; set; }

		[DataMember]
		public string ConstrTypTxt { get; set; }

		[DataMember]
		public decimal? TotalProjectsCnt { get; set; }

		[DataMember]
		public decimal? BuildHoursNbr { get; set; }

		[DataMember]
		public decimal? ElectHoursNbr { get; set; }

		[DataMember]
		public decimal? MechHoursNbr { get; set; }

		[DataMember]
		public decimal? PlumbHoursNbr { get; set; }

		[DataMember]
		public decimal? TotalSquareFootageCnt { get; set; }

		[DataMember]
		public decimal? TotalConstrCostAmt { get; set; }

		[DataMember]
		public decimal? TotalSheetsCnt { get; set; }

		#endregion

	}

	#endregion

}