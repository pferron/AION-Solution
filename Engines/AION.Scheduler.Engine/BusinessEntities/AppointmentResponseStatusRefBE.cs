#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Engine.BusinessEntities
{

	#region BusinessEntitiy - AppointmentResponseStatusRefBE

	[DataContract]
	public class AppointmentResponseStatusRefBE : BaseBE
	{

		#region Properties

		[DataMember]
		public int? ApptResponseStatusRefId { get; set; }

		[DataMember]
		public string ApptResponseStatusDesc { get; set; }

		[DataMember]
		public int? EnumMappingValNbr { get; set; }

		[DataMember]
		public bool? ActiveInd { get; set; }

		#endregion

	}

	#endregion

}