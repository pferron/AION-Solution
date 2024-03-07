#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Estimator.Engine.BusinessEntities
{

	#region BusinessEntitiy - BusinessRefBE

	[DataContract]
	public class BusinessRefBE : BaseBE
	{

		#region Properties

		[DataMember]
		public string BusinessName { get; set; }

		[DataMember]
		public int? BusinessRefId { get; set; }

		[DataMember]
		public string BusinessShortDesc { get; set; }

		[DataMember]
		public int? BusinessTypeRefId { get; set; }

		[DataMember]
		public int? DisionRefId { get; set; }

		[DataMember]
		public int? EnumMappingNumber { get; set; }

		[DataMember]
		public int? ExternalSystemRefId { get; set; }

		[DataMember]
		public int? RegionRefId { get; set; }

		[DataMember]
		public string SourceSystemValueText { get; set; }

		#endregion

	}

	#endregion

}