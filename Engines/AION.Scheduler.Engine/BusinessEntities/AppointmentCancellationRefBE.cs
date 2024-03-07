#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Engine.BusinessEntities
{

	#region BusinessEntitiy - AppointmentCancellationRefBE

	[DataContract]
	public class AppointmentCancellationRefBE : BaseBE
	{

		#region Properties

		[DataMember]
		public int? ApptCancellationRefId { get; set; }

		[DataMember]
		public string CancellationDesc { get; set; }

		[DataMember]
		public int? EnumMappingValNbr { get; set; }

		[DataMember]
		public bool? ActiveInd { get; set; }

		#endregion

	}

	#endregion

}