#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Estimator.Engine.BusinessObjects
{

	#region BusinessEntitiy - OccupancySqrFootageUsrMapBE

	[DataContract]
	public class OccupancySqrFootageUsrMapBE : BaseBE
	{

		#region Properties

		[DataMember]
		public int? UserID { get; set; }

		[DataMember]
		public int? OccupancyTypeRefID { get; set; }

		[DataMember]
		public int? SquareFootageRefID { get; set; }

		[DataMember]
		public int? OccupancySqrFootageUsrMapID { get; set; }

		#endregion

	}

	#endregion

}