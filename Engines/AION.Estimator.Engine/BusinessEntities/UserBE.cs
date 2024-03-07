#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Estimator.Engine.BusinessEntities
{

	#region BusinessEntitiy - UserBE

	[DataContract]
	public class UserBE : BaseBE
	{

		#region Properties

		[DataMember]
		public int? UserID { get; set; }

		[DataMember]
		public string FirstNm { get; set; }

		[DataMember]
		public string LastNm { get; set; }

		[DataMember]
		public int? ExternalSystemRefId { get; set; }

		[DataMember]
		public string SrcSystemValueTxt { get; set; }

		[DataMember]
		public bool? IsActive { get; set; }

		[DataMember]
		public string UiSettings { get; set; }

		[DataMember]
		public bool? IsExpressSched { get; set; }

		[DataMember]
		public string UserName { get; set; }

		[DataMember]
		public string ADName { get; set; }

		[DataMember]
		public string Phone { get; set; }

		[DataMember]
		public string Email { get; set; }

		[DataMember]
		public string Notes { get; set; }

		[DataMember]
		public bool? IsSchedulable { get; set; }

		[DataMember]
		public decimal? PlanReviewOverrideHours { get; set; }

		[DataMember]
		public string HoursEstimated { get; set; }

		[DataMember]
		public int? Jurisdiction { get; set; }

		[DataMember]
		public string SchedulableLevel { get; set; }

		[DataMember]
		public bool? IsPrelimMeetingAllowed { get; set; }

		[DataMember]
		public string UserPrincipalName { get; set; }

		[DataMember]
		public string CalendarId { get; set; }

		[DataMember]
		public bool? IsCity { get; set; }

		#endregion

	}

	#endregion

}