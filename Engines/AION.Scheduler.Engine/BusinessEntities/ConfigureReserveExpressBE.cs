#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Engine.BusinessEntities
{

	#region BusinessEntitiy - ConfigureReserveExpressBE

	[DataContract]
	public class ConfigureReserveExpressBE : BaseBE
	{

		#region Properties

		[DataMember]
		public int? ConfigureReserveExpressId { get; set; }

		[DataMember]
		public string ReserveExpressDay { get; set; }

		[DataMember]
		public DateTime? StartDate { get; set; }

		[DataMember]
		public DateTime? EndDate { get; set; }

		[DataMember]
		public bool? ActiveInd { get; set; }

		#endregion

	}

	#endregion

}