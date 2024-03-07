#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Estimator.Engine.BusinessEntities
{

	#region BusinessEntitiy - ExternalSystemRefBE

	[DataContract]
	public class ExternalSystemRefBE : BaseBE
	{

		#region Properties

		[DataMember]
		public int? ExternalSystemRefId { get; set; }

		[DataMember]
		public string ExternalSystemName { get; set; }

		[DataMember]
		public string ExternalSystemDesc { get; set; }

		[DataMember]
		public string AddlInformationText { get; set; }

		[DataMember]
		public int? EnumMappingValNbr { get; set; }

		#endregion

	}

	#endregion

}