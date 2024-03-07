#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Engine.BusinessEntities
{

	#region BusinessEntitiy - CatalogRefBE

	[DataContract]
	public class CatalogRefBE : BaseBE
	{

		#region Properties

		[DataMember]
		public int? ID { get; set; }

		[DataMember]
		public string Value { get; set; }

		[DataMember]
		public string Key { get; set; }

		[DataMember]
		public string SubKey { get; set; }

		[DataMember]
		public string CatalogGroupRefID { get; set; }

		#endregion

	}

	#endregion

}