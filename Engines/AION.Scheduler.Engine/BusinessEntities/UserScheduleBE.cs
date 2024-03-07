#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Engine.BusinessEntities
{

	#region BusinessEntitiy - UserScheduleBE

	[DataContract]
	public class UserScheduleBE : BaseBE
	{

		#region Properties

		[DataMember]
		public int? UserScheduleID { get; set; }

		[DataMember]
		public DateTime? StartDateTime { get; set; }

		[DataMember]
		public DateTime? EndDateTime { get; set; }

		[DataMember]
		public int? ProjectScheduleID { get; set; }

		[DataMember]
		public int? BusinessRefID { get; set; }

		[DataMember]
		public int? UserID { get; set; }

		#endregion

	}

	#endregion

}