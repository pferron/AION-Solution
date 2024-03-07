#region Using

using AION.Base;
using System;
using System.Runtime.Serialization;

#endregion

namespace AION.Estimator.Engine.BusinessEntities
{

    #region BusinessEntitiy - AverageEstimationHoursFactorBE

    [DataContract]
    public class AverageEstimationHoursFactorBE : BaseBE
    {

        #region Properties

        [DataMember]
        public int? AverageEstimationHoursFactorId { get; set; }

        [DataMember]
        public int? OccupancyTypRefId { get; set; }

        [DataMember]
        public string ConstructionType { get; set; }

        [DataMember]
        public decimal? BuildingSqftFactor { get; set; }

        [DataMember]
        public decimal? ElectricalSqftFactor { get; set; }

        [DataMember]
        public decimal? MechanicalSqftFactor { get; set; }

        [DataMember]
        public decimal? PlumbingSqftFactor { get; set; }

        [DataMember]
        public decimal? BuildingCocFactor { get; set; }

        [DataMember]
        public decimal? ElectricalCocFactor { get; set; }

        [DataMember]
        public decimal? MechanicalCocFactor { get; set; }

        [DataMember]
        public decimal? PlumbingCocFactor { get; set; }

        [DataMember]
        public decimal? BuildingSheetsFactor { get; set; }

        [DataMember]
        public decimal? ElectricalSheetsFactor { get; set; }

        [DataMember]
        public decimal? MechanicalSheetsFactor { get; set; }

        [DataMember]
        public decimal? PlumbingSheetsFactor { get; set; }

        [DataMember]
        public bool? ActiveInd { get; set; }

        [DataMember]
        public DateTime? ActiveDate { get; set; }

        #endregion

    }

    #endregion

}