#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Engine.BusinessEntities
{

	#region BusinessEntitiy - AppoinmentReccuranceRefBE

	[DataContract]
	public class AppoinmentReccuranceRefBE : BaseBE
	{

		#region Properties

		[DataMember]
		public int? AppoinmentReccuranceID { get; set; }

		[DataMember]
		public string ReccuranceWeek { get; set; }

		[DataMember]
		public string ReccuranceDay { get; set; }

		[DataMember]
		public int? EnumMappingValNbr { get; set; }

		[DataMember]
		public bool? IsActive { get; set; }

		#endregion

	}

	#endregion

}