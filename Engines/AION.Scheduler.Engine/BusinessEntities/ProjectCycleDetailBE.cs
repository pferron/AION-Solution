#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Engine.BusinessEntities
{

	#region BusinessEntitiy - ProjectCycleDetailBE

	[DataContract]
	public class ProjectCycleDetailBE : BaseBE
	{

		#region Properties

		[DataMember]
		public int? ProjectCycleDetailId { get; set; }

		[DataMember]
		public int? ProjectCycleId { get; set; }

		[DataMember]
		public int? BusinessRefId { get; set; }

		[DataMember]
		public decimal? RereviewHoursNbr { get; set; }

		#endregion

	}

	#endregion

}