using AION.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AION.Engine.BusinessEntities
{
    [DataContract]

    public class ReserveExpressPlanReviewerSearchResultBE : BaseBE
    {
        #region Properties

        [DataMember]
        public int? ReserveExpressReservationId { get; set; }

        [DataMember]
        public DateTime? ReserveExpressDt { get; set; }

        [DataMember]
        public int? ProjectScheduleId { get; set; }

        [DataMember]
        public DateTime? RecurringApptDt { get; set; }

        [DataMember]
        public string AppoinmentName { get; set; }

        [DataMember]
        public DateTime? AppointmentStartTime { get; set; }

        [DataMember]
        public DateTime? AppointmentEndTime { get; set; }

        [DataMember]
        public int? MeetingRoomRefId { get; set; }

        #endregion
    }
}
