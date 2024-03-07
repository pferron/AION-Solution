using AION.Base;
using System;
using System.Runtime.Serialization;


namespace AION.Security.Engine.BusinessEntities
{
    [DataContract]
    public class SecurityBE : BaseBE
    {

        //sample props
        #region Properties
        [DataMember]
        public decimal? ParcelID { get; set; }

        [DataMember]
        public decimal? ParentPropertyID { get; set; }

        [DataMember]
        public string BusinessName { get; set; }

        [DataMember]
        public string PropertyAddress { get; set; }

        [DataMember]
        public decimal? Aassessment { get; set; }

        [DataMember]
        public decimal? TaxYear { get; set; }

        [DataMember]
        public DateTime AssessedDate { get; set; }

        [DataMember]
        public string TownTaxJurisdiction { get; set; }

        [DataMember]
        public decimal? AbstractPK { get; set; }


        #endregion

    }
}
