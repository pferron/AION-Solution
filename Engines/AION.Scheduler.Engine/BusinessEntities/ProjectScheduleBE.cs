#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Engine.BusinessEntities
{

	#region BusinessEntitiy - ProjectScheduleBE

	[DataContract]
	public class ProjectScheduleBE : BaseBE
	{

		#region Properties

		[DataMember]
		public int? ProjectScheduleID { get; set; }

		[DataMember]
		public string ProjectScheduleTypeRef { get; set; }

		[DataMember]
		public int? AppoinmentID { get; set; }

        [DataMember]
        public DateTime? RecurringApptDt { get; set; }

        #endregion

    }

    #endregion

}