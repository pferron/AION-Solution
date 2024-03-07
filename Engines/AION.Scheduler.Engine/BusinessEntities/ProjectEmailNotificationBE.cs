#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Engine.BusinessEntities
{

	#region BusinessEntitiy - ProjectEmailNotificationBE

	[DataContract]
	public class ProjectEmailNotificationBE : BaseBE
	{

		#region Properties

		[DataMember]
		public int? ProjectEmailNotificationId { get; set; }

		[DataMember]
		public int? ProjectId { get; set; }

		[DataMember]
		public string EmailTypeDesc { get; set; }

		[DataMember]
		public string EmailSubjectText { get; set; }

		[DataMember]
		public string EmailBodyTxt { get; set; }

		[DataMember]
		public DateTime? EmailSentDt { get; set; }

		[DataMember]
		public int? SenderUserId { get; set; }

		#endregion

	}

	#endregion

}