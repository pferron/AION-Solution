#region Using

using AION.Base;
using System;
using System.Runtime.Serialization;

#endregion

namespace AION.Engine.BusinessEntities
{
    #region BusinessEntitiy - CustomerScheduledMeetingBE

    [DataContract]
    public class CustomerScheduledMeetingBE : BaseBE
    {
        #region Properties

        public int MeetingId { get; set; }

        [DataMember]
        public int? MeetingType { get; set; }

        [DataMember]
        public DateTime FromDt { get; set; }

        [DataMember]
        public DateTime ToDt { get; set; }

        [DataMember]
        public DateTime? AppBAgendaDue { get; set; }

        [DataMember]
        public DateTime? ResponseDue { get; set; }

        [DataMember]
        public int ApptResponseStatusRefId { get; set; }

        [DataMember]
        public int? ApptCancellationRefId { get; set; }

        #endregion

        #region properties added for views
        //LES-4029
        [DataMember]
        public string Attendees { get; set; }

        #endregion properties added for views
    }

    #endregion
}
