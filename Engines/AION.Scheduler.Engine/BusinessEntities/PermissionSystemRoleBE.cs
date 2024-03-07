#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Engine.BusinessEntities
{

	#region BusinessEntitiy - PermissionSystemRoleBE

	[DataContract]
	public class PermissionSystemRoleBE : BaseBE
	{

		#region Properties

		[DataMember]
		public int? PermissionId { get; set; }

		[DataMember]
		public int? SystemRoleId { get; set; }

		[DataMember]
		public int? PermissionSystemRoleId { get; set; }

		#endregion

	}

	#endregion

}