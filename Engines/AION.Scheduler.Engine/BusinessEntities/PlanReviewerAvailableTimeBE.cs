#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Engine.BusinessEntities
{

	#region BusinessEntitiy - PlanReviewerAvailableTimeBE

	[DataContract]
	public class PlanReviewerAvailableTimeBE : BaseBE
	{

		#region Properties

		[DataMember]
		public int? PlanReviewerAvailableTimeID { get; set; }

		[DataMember]
		public DateTime? AvailableStartTime { get; set; }

		[DataMember]
		public DateTime? AvailableEndTime { get; set; }

		[DataMember]
		public string ProjectTypeDesc { get; set; }

		[DataMember]
		public int? ProjectTypeRefID { get; set; }

		#endregion

	}

	#endregion

}