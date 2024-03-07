#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Engine.BusinessEntities
{

	#region BusinessEntitiy - ReserveExpressPlanReviewerBE

	[DataContract]
	public class ReserveExpressPlanReviewerBE : BaseBE
	{

		#region Properties

		[DataMember]
		public int? ReserveExpressPlanReviewerId { get; set; }

		[DataMember]
		public int? BusinessRefId { get; set; }

		[DataMember]
		public int? PlanReviewerId { get; set; }

		[DataMember]
		public int? RotationNbr { get; set; }

		public string FirstName { get; set; }
		public string LastName { get; set; }

		#endregion

	}

	#endregion

}