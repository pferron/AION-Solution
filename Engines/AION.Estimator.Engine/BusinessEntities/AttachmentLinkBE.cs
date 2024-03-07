#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Estimator.Engine.BusinessEntities
{

	#region BusinessEntitiy - AttachmentLinkBE

	[DataContract]
	public class AttachmentLinkBE : BaseBE
	{

		#region Properties

		[DataMember]
		public int? AttachmentLinkId { get; set; }

		[DataMember]
		public string LinkText { get; set; }

		[DataMember]
		public int? NotesId { get; set; }

		[DataMember]
		public string TagCreatedIdTxt { get; set; }

		[DataMember]
		public DateTime? TagCreatedDt { get; set; }

		[DataMember]
		public DateTime? TagUpdatedDt { get; set; }

		[DataMember]
		public string TagUpdatedIdTxt { get; set; }

		[DataMember]
		public string AttachmentTypeCd { get; set; }

		#endregion

	}

	#endregion

}