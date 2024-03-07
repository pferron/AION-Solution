#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Estimator.Engine.BusinessEntities
{

	#region BusinessEntitiy - UserProjectTypeRefBE

	[DataContract]
	public class UserProjectTypeRefBE : BaseBE
	{

		#region Properties

		[DataMember]
		public int? UserProjectTypeCrossRefID { get; set; }

		[DataMember]
		public int? ProjectTypeRefID { get; set; }

		[DataMember]
		public int? UserID { get; set; }

		#endregion

	}

	#endregion

}