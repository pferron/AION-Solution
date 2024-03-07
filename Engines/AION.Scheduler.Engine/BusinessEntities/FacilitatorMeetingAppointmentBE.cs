#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Engine.BusinessEntities
{

	#region BusinessEntitiy - FacilitatorMeetingAppointmentBE

	[DataContract]
	public class FacilitatorMeetingAppointmentBE : AppointmentBaseBE
	{

		#region Properties

		[DataMember]
		public int? FacilitatorMeetingApptId { get; set; }

		[DataMember]
		public int? ProjectId { get; set; }

		[DataMember]
		public int? MeetingRoomRefId { get; set; }

		[DataMember]
		public int? ApptResponseStatusRefId { get; set; }

		[DataMember]
		public DateTime? FromDt { get; set; }

		[DataMember]
		public DateTime? ToDt { get; set; }

		[DataMember]
		public bool? VirtualMeetingInd { get; set; }

		[DataMember]
		public int? MeetingTypRefId { get; set; }

		public DateTime? RequestedDate1 { get; set; }

		public DateTime? RequestedDate2 { get; set; }

		public DateTime? RequestedDate3 { get; set; }

		public int? ExternalAttendeesCnt { get; set; }

		#endregion

	}

	#endregion

}