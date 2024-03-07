#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Engine.BusinessEntities
{

	#region BusinessEntitiy - NonProjectAppoinmentBE

	[DataContract]
	public class NonProjectAppoinmentBE : BaseBE
	{

		#region Properties

		[DataMember]
		public int? NonProjectAppointmentID { get; set; }

		[DataMember]
		public string AppoinmentName { get; set; }

		[DataMember]
		public bool? IsAllPlanReviewers { get; set; }

		[DataMember]
		public bool? IsAllDay { get; set; }

		[DataMember]
		public DateTime? AppointmentFrom { get; set; }

		[DataMember]
		public DateTime? AppointmentTo { get; set; }

		[DataMember]
		public int? NPATypeRefID { get; set; }

		[DataMember]
		public int? MeetingRoomRefID { get; set; }

		[DataMember]
		public int? AppoinmentRecurrenceRefID { get; set; }

		[DataMember]
		public bool? IsAllBuild { get; set; }

		[DataMember]
		public bool? IsAllElectric { get; set; }

		[DataMember]
		public bool? IsAllMech { get; set; }

		[DataMember]
		public bool? IsAllPlumb { get; set; }

		[DataMember]
		public bool? IsAllZoning { get; set; }

		[DataMember]
		public bool? IsAllFire { get; set; }

		[DataMember]
		public bool? IsAllBackFlow { get; set; }

		[DataMember]
		public bool? IsAllEhsFood { get; set; }

		[DataMember]
		public bool? IsAllEhsPool { get; set; }

		[DataMember]
		public bool? IsAllEhsLodge { get; set; }

		[DataMember]
		public bool? IsAllEhsDayCare { get; set; }

		#endregion

	}

	#endregion

}