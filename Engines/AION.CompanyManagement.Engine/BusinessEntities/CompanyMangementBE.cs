using AION.Base;
using System;
using System.Runtime.Serialization;

namespace AION.CompanyMangement.Engine.BusinessEntities
{
    [DataContract]
    public class CompanyMangementBE : BaseBE
    {
        //sample props
        #region Properties
        [DataMember]
        public decimal? TaxYear { get; set; }

        [DataMember]
        public string TaxDistrictType { get; set; }

        [DataMember]
        public string RateType { get; set; }

        [DataMember]
        public string EstOrFinal { get; set; }

        [DataMember]
        public DateTime VerifiedDate { get; set; }

        [DataMember]
        public decimal? TaxRate { get; set; }



        #endregion
    }
}
