#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Engine.BusinessEntities
{

	#region BusinessEntitiy - FacilitatorMeetingApptDetailBE

	[DataContract]
	public class FacilitatorMeetingApptDetailBE : BaseBE
	{

		#region Properties

		[DataMember]
		public int? FacilitatorMeetingApptDetailId { get; set; }

		[DataMember]
		public int? FacilitatorMeetingApptId { get; set; }

		[DataMember]
		public int? BusinessRefId { get; set; }

		[DataMember]
		public int? AssignedPlanReviewerId { get; set; }

		#endregion

	}

	#endregion

}