#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Engine.BusinessEntities
{

	#region BusinessEntitiy - PlanReviewScheduleBE

	[DataContract]
	public class PlanReviewScheduleBE : BaseBE
	{

		#region Properties

		[DataMember]
		public int? PlanReviewScheduleId { get; set; }

		[DataMember]
		public int? ProjectCycleId { get; set; }

		[DataMember]
		public string ProjectScheduleTypDesc { get; set; }

		[DataMember]
		public bool? IsRescheduleInd { get; set; }

		[DataMember]
		public int? ApptResponseStatusRefId { get; set; }

		[DataMember]
		public int? ApptCancellationRefId { get; set; }

		[DataMember]
		public bool? VirtualMeetingInd { get; set; }

		[DataMember]
		public DateTime? Proposed1Dt { get; set; }

		[DataMember]
		public DateTime? Proposed2Dt { get; set; }

		[DataMember]
		public DateTime? Proposed3Dt { get; set; }

		[DataMember]
		public DateTime? CancelAfterDt { get; set; }

		[DataMember]
		public int? MeetingRoomRefId { get; set; }

		#endregion

	}

	#endregion

}