using System;
using System.Runtime.Serialization;
using AION.Base;

namespace AION.Engine.BusinessEntities
{
    #region BusinessEntity - CustomerExpressMeetingAppointmentBE

    [DataContract]
    public class CustomerExpressMeetingAppointmentBE:BaseBE
    {
        #region Properties

        public int ExpressMeetingAppointmentId { get; set; }

        #endregion
    }

    #endregion
}
