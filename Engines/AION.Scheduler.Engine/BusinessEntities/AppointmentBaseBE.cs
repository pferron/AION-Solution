using AION.Base;
using System;
using System.Runtime.Serialization;

namespace AION.Engine.BusinessEntities
{
    public abstract class AppointmentBaseBE : BaseBE
    {
        #region properties

        [DataMember]
        public int? ApptCancellationRefId { get; set; }

        [DataMember]
        public DateTime? CancelAfterDt { get; set; }

        #endregion
    }
}