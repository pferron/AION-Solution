using AION.Base;
using System;
using System.Runtime.Serialization;

namespace AION.Estimator.Engine.BusinessEntities
{
    [DataContract]
    public class HolidayConfigBE
    {
        [DataMember]
        public int HolidayConfigId { get; set; }

        [DataMember]
        public string HolidayNm { get; set; }

        [DataMember]
        public DateTime HolidayDate { get; set; }

        [DataMember]
        public bool HolidayAnnualRecurInd { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

    }
}
