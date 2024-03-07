#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Estimator.Engine.BusinessEntities
{

	#region BusinessEntitiy - NotesTypeRefBE

	[DataContract]
	public class NotesTypeRefBE : BaseBE
	{

		#region Properties

		[DataMember]
		public int? NotesTypeRefId { get; set; }

		[DataMember]
		public string NotesTypeRefName { get; set; }

		[DataMember]
		public int? ExternalSystemRefId { get; set; }

		[DataMember]
		public string SrcSystemValTxt { get; set; }

		[DataMember]
		public int? EnumMapping { get; set; }

		#endregion

	}

	#endregion

}