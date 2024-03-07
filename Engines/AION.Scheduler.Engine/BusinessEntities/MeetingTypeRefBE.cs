#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Engine.BusinessEntities
{

	#region BusinessEntitiy - MeetingTypeRefBE

	[DataContract]
	public class MeetingTypeRefBE : BaseBE
	{

		#region Properties

		[DataMember]
		public int? MeetingTypRefId { get; set; }

		[DataMember]
		public string MeetingTypDesc { get; set; }

		[DataMember]
		public int? EnumMappingValNbr { get; set; }

		[DataMember]
		public bool? ActiveInd { get; set; }

		#endregion

	}

	#endregion

}