using AION.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AION.BL
{
    public class AppointmentCancellationReason : ModelBase
    {
        #region Properties

        public int? ApptCancellationRefId { get; set; }

        public string ApptCancellationDesc { get; set; }

        public int? EnumMappingValNbr { get; set; }

        public bool? ActiveInd { get; set; }

        public AppointmentCancellationEnum ApptCancellationEnum { get; set; }
        #endregion
    }
}