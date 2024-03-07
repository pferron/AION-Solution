#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Engine.BusinessEntities
{

	#region BusinessEntitiy - ProjectCycleBE

	[DataContract]
	public class ProjectCycleBE : BaseBE
	{

		#region Properties

		[DataMember]
		public int? ProjectCycleId { get; set; }

		[DataMember]
		public int? ProjectId { get; set; }

		[DataMember]
		public bool? CurrentCycleInd { get; set; }

		[DataMember]
		public bool? FutureCycleInd { get; set; }

		[DataMember]
		public int? CycleNbr { get; set; }

		[DataMember]
		public DateTime? PlansReadyOnDt { get; set; }

		[DataMember]
		public bool? IsCompleteInd { get; set; }

		[DataMember]
		public DateTime? GateDt { get; set; }

		[DataMember]
		public DateTime? ScheduleAfterDt { get; set; }

		[DataMember]
		public int? ResponderUserId { get; set; }

		[DataMember]
		public bool? IsAprvInd { get; set; }

		[DataMember]
		public DateTime? ResponseDt { get; set; }

		[DataMember]
		public bool? IncrementOnPlansReceivedInd { get; set; }

		#endregion
	}

	#endregion

}