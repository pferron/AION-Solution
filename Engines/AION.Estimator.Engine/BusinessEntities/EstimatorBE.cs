using AION.Base;
using System;
using System.Runtime.Serialization;


namespace AION.Estimator.Engine.BusinessEntities
{
    [DataContract]
    public class EstimatorBE : BaseBE
    {
        //sample props
        #region Properties

        [DataMember]
        public decimal? EstimatorId { get; set; }

        [DataMember]
        public decimal? ParcelId { get; set; }

        [DataMember]
        public decimal? ParentPropertyId { get; set; }

        [DataMember]
        public string BusinessName { get; set; }

        [DataMember]
        public string PropertyAddress { get; set; }

        [DataMember]
        public decimal? Aassessment { get; set; }

        [DataMember]
        public decimal? TaxYear { get; set; }

        [DataMember]
        public DateTime? AssessedDate { get; set; }

        [DataMember]
        public string TownTaxJurisdiction { get; set; }

        [DataMember]
        public decimal? AbstractPK { get; set; }


        #endregion

    }
}
