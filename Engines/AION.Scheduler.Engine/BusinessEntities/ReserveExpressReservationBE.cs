#region Using

using System;
using System.Runtime.Serialization;
using AION.Base;

#endregion

namespace AION.Engine.BusinessEntities
{

    #region BusinessEntitiy - ReserveExpressReservationBE

    [DataContract]
    public class ReserveExpressReservationBE : AppointmentBaseBE
    {

        #region Properties

        [DataMember]
        public int ReserveExpressReservationId { get; set; }

        [DataMember]
        public DateTime ReserveExpressDt { get; set; }

        [DataMember]
        public DateTime StartTime { get; set; }

        [DataMember]
        public DateTime EndTime { get; set; }

        [DataMember]
        public int? MeetingRoomRefId { get; set; }
        [DataMember]
        public DateTime RequestedDate1 { get; set; }
        [DataMember]
        public DateTime RequestedDate2 { get; set; }
        [DataMember]
        public DateTime RequestedDate3 { get; set; }
        [DataMember]
        public int? ApptResponseStatusRefId { get; set; }

        #endregion

    }

    #endregion

}