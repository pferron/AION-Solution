#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Estimator.Engine.BusinessEntities
{

	#region BusinessEntitiy - PersonBE

	[DataContract]
	public class PersonBE : BaseBE
	{

		#region Properties

		[DataMember]
		public int? PersonId { get; set; }

		[DataMember]
		public string LastName { get; set; }

		[DataMember]
		public string MiddleName { get; set; }

		[DataMember]
		public string FirstName { get; set; }

		[DataMember]
		public int? GenderRefId { get; set; }

		[DataMember]
		public DateTime? Birthdate { get; set; }

		[DataMember]
		public int? Age { get; set; }

		[DataMember]
		public int? EthnicityRefId { get; set; }

		[DataMember]
		public string RaceOtherDescription { get; set; }

		[DataMember]
		public int? RaceRefId { get; set; }

		#endregion

	}

	#endregion

}