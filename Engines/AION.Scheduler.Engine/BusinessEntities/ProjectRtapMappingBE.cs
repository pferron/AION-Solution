#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Engine.BusinessEntities
{

	#region BusinessEntitiy - ProjectRtapMappingBE

	[DataContract]
	public class ProjectRtapMappingBE : BaseBE
	{
		public ProjectRtapMappingBE()
        {
			CreatedByWkrId = "1";
			UpdatedByWkrId = "1";
        }

		#region Properties

		[DataMember]
		public int? ProjectRtapMappingId { get; set; }

		[DataMember]
		public int? ProjectId { get; set; }

		[DataMember]
		public int? OriginalProjectId { get; set; }

		[DataMember]
		public string OriginalProjectNumber { get; set; }

		#endregion

	}

	#endregion

}