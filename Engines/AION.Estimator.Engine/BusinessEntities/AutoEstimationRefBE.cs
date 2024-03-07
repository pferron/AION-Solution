#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Estimator.Engine.BusinessEntities
{

	#region BusinessEntitiy - AutoEstimationRefBE

	[DataContract]
	public class AutoEstimationRefBE : BaseBE
	{

		#region Properties

		[DataMember]
		public bool? ActiveInd { get; set; }

		[DataMember]
		public DateTime? ActiveDate { get; set; }

		[DataMember]
		public int? AutoEstimationRefId { get; set; }

		[DataMember]
		public int? Months { get; set; }

		[DataMember]
		public decimal? WeightSqftNbr { get; set; }

		[DataMember]
		public decimal? WeightCocNbr { get; set; }

		[DataMember]
		public decimal? WeightSheetsNbr { get; set; }

		#endregion

	}

	#endregion

}