#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Engine.BusinessEntities
{

	#region BusinessEntitiy - CalendarEventBE

	[DataContract]
	public class CalendarEventBE : BaseBE
	{

		#region Properties

		[DataMember]
		public int? CalendarEventQueueId { get; set; }

		[DataMember]
		public string JsonObjectTxt { get; set; }

		[DataMember]
		public string ActionDesc { get; set; }

		[DataMember]
		public bool? ProcessedInd { get; set; }

		[DataMember]
		public DateTime? ProcessedDate { get; set; }

		[DataMember]
		public bool? InProcessInd { get; set; }

		[DataMember]
		public DateTime? InProcessDate { get; set; }

		[DataMember]
		public int? RetryCount { get; set; }

		#endregion

	}

	#endregion

}