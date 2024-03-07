#region Using

using System;
using System.Runtime.Serialization;

#endregion

namespace AION.Engine.BusinessEntities
{

    #region BusinessEntitiy - PreliminaryMeetingAppointmentBE

    [DataContract]
    public class PreliminaryMeetingAppointmentBE : AppointmentBaseBE
    {

        #region Properties

        [DataMember]
        public int? PreliminaryMeetingApptID { get; set; }

        [DataMember]
        public DateTime? FromDT { get; set; }

        [DataMember]
        public DateTime? ToDT { get; set; }

        [DataMember]
        public int? MeetingRoomRefID { get; set; }

        [DataMember]
        public int? ApptResponseStatusRefId { get; set; }

        [DataMember]
        public int? ProjectID { get; set; }

        [DataMember]
        public DateTime? AppendixAgendaDueDt { get; set; }

        public DateTime? RequestedDate1 { get; set; }

        public DateTime? RequestedDate2 { get; set; }

        public DateTime? RequestedDate3 { get; set; }

        public int ApptResponseStatusEnumId { get; set; }

        [DataMember]
        public bool? IsReschedule { get; set; }

        #endregion

    }


    [DataContract]
    public class CustmrMeetingsBE : AppointmentBaseBE
    {
        [DataMember]
        public DateTime? MeetingDate { get; set; }

        [DataMember]
        public DateTime? MeetingTime { get; set; }

        [DataMember]
        public int? MeetingType { get; set; }

        [DataMember]
        public int? ProjectType { get; set; }

        [DataMember]
        public string ProjectExternalRefID { get; set; }

        [DataMember]
        public string ProjectName { get; set; }

        [DataMember]
        public int ProjectStatus { get; set; }

        [DataMember]
        public int ProjectID { get; set; }


        [DataMember]
        public int UserID { get; set; }

        [DataMember]
        public DateTime? AppendixAgendaDue { get; set; }

        [DataMember]
        public DateTime? MinutesDue { get; set; }

        [DataMember]
        public int? ApptResponseStatusRefId { get; set; }

        [DataMember]
        public string RecIdTxt { get; set; }
    }

    [DataContract]
    public class InternalMeetingsBE : CustmrMeetingsBE
    {

        [DataMember]
        public bool? IsProjectRTAP { get; set; } //Y/N

        [DataMember]
        public bool MinutesUploaded { get; set; } //Y/N

        [DataMember]
        public string TeamGradeTxt { get; set; }//Accela

        [DataMember]
        public string BuildingCodeVersion { get; set; } //Accela

        [DataMember]
        public int? MeetingRoomRefId { get; set; }

        [DataMember]
        public int? ProjectScheduleId { get; set; }

        [DataMember]
        public int? FacilitatorId { get; internal set; }

        [DataMember]
        public int? ProjectManagerId { get; internal set; }
    }


    #endregion

}