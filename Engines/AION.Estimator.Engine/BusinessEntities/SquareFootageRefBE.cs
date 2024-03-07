#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Estimator.Engine.BusinessObjects
{

	#region BusinessEntitiy - SquareFootageRefBE

	[DataContract]
	public class SquareFootageRefBE : BaseBE
	{

		#region Properties

		[DataMember]
		public int? SquareFootageRefID { get; set; }

		[DataMember]
		public string SquareFootageDesc { get; set; }

		[DataMember]
		public string SquareFootageValue { get; set; }

		[DataMember]
		public int? EnumMappingVal { get; set; }

		#endregion

	}

	#endregion

}