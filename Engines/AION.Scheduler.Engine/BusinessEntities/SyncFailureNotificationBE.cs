#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Engine.BusinessEntities
{

	#region BusinessEntitiy - SyncFailureNotificationBE

	[DataContract]
	public class SyncFailureNotificationBE : BaseBE
	{

		#region Properties

		[DataMember]
		public DateTime? LastFailureNotificationDt { get; set; }

		#endregion

	}

	#endregion

}