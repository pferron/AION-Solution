#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Engine.BusinessEntities
{

	#region BusinessEntitiy - UserScheduleBE

	[DataContract]
	public class UserScheduleExBE : UserScheduleBE
	{

		#region Properties

		[DataMember]
		public string ProjectScheduleTypeName { get; set; }

		[DataMember]
		public int? ProjectID { get; set; }


		[DataMember]
		public string ProjectCategory { get; set; }

		#endregion

	}

	#endregion

}