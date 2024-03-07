using AION.Base;
using System;
using System.Runtime.Serialization;

namespace AION.Scheduler.Engine.BusinessEntities
{
    [DataContract]
    public class MeetingBE : BaseBE
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
        public bool RTAP { get; set; } //Y/N

        [DataMember]
        public bool MinutesUploaded { get; set; } //Y/N

        [DataMember]
        public string Facilitator { get; set; }

        [DataMember]
        public string TeamScore { get; set; }//Accela


        [DataMember]
        public string PMName { get; set; }//Accela



        [DataMember]
        public string PMPhone { get; set; }//Accela


        [DataMember]
        public string PMEmail { get; set; }//Accela


        [DataMember]
        public string BuildingCodeVersion { get; set; } //Accela

        [DataMember]
        public int? MeetingRoomRefId { get; set; }

        [DataMember]
        public int? ProjectScheduleId { get; set; }

        [DataMember]
        public int? ApptResponseStatusRefId { get; set; }

        [DataMember]
        public DateTime? DateTimeFrom { get; set; }

        [DataMember]
        public DateTime? DateTimeTo { get; set; }

        /// <summary>
        /// NPA's have a name
        /// </summary>
        [DataMember]
        public string MeetingName { get; set; }

        [DataMember]
        public int AppointmentId { get; set; }


        #region properties added for views
        //LES-4028
        [DataMember]
        public string Attendees { get; set; }

        #endregion properties added for views
    }
}
