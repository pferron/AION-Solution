#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Engine.BusinessEntities
{

	#region BusinessEntitiy - UserScheduleStageBE

	[DataContract]
	public class UserScheduleStageBE : BaseBE
	{

		#region Properties

		[DataMember]
		public int? UserScheduleStageIdentifer { get; set; }

		[DataMember]
		public DateTime? StartDate { get; set; }

		[DataMember]
		public DateTime? EndDate { get; set; }

		[DataMember]
		public int? UserID { get; set; }

		[DataMember]
		public int? BusinessRefID { get; set; }

		[DataMember]
		public int? ProjectID { get; set; }

		#endregion

	}

	#endregion

}