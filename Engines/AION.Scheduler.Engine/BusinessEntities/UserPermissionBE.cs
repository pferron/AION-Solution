#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Engine.BusinessEntities
{

	#region BusinessEntitiy - UserPermissionBE

	[DataContract]
	public class UserPermissionBE : BaseBE
	{

		#region Properties

		[DataMember]
		public int? UserId { get; set; }

		[DataMember]
		public int? PermissionId { get; set; }

		[DataMember]
		public int? UserPermissionId { get; set; }

		#endregion

	}

	#endregion

}