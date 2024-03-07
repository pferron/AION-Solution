#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Engine.BusinessEntities
{

	#region BusinessEntitiy - CatalogGroupRefBE

	[DataContract]
	public class CatalogGroupRefBE : BaseBE
	{

		#region Properties

		[DataMember]
		public int? ID { get; set; }

		[DataMember]
		public string CatalogGroupName { get; set; }

		#endregion

	}

	#endregion

}