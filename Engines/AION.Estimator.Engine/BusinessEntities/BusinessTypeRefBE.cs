#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Estimator.Engine.BusinessEntities
{

	#region BusinessEntitiy - BusinessTypeRefBE

	[DataContract]
	public class BusinessTypeRefBE : BaseBE
	{

		#region Properties

		[DataMember]
		public int? BusinessTypeRefId { get; set; }

		[DataMember]
		public string BusinessRefTypeShortDesc { get; set; }

		[DataMember]
		public string BusinessRefDisplayName { get; set; }

		[DataMember]
		public int? ExternalSystemRefId { get; set; }

		[DataMember]
		public string BusinessRef_SrcSystemValueText { get; set; }

		[DataMember]
		public int? BusinessRef_EnumMappingValNbr { get; set; }

		#endregion

	}

	#endregion

}