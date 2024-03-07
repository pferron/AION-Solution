#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Estimator.Engine.BusinessEntities
{

	#region BusinessEntitiy - AuditActionRefBE

	[DataContract]
	public class AuditActionRefBE : BaseBE
	{

		#region Properties

		[DataMember]
		public int? AuditActionRefId { get; set; }

		[DataMember]
		public string AuditActionName { get; set; }

		[DataMember]
		public string AuditActionDesc { get; set; }

		#endregion

	}

	#endregion

}