#region Using

using AION.Base;
using System.Runtime.Serialization;

#endregion

namespace AION.Engine.BusinessEntities
{

    #region BusinessEntitiy - PlanReviewerAvailableHoursBE

    [DataContract]
    public class PlanReviewerAvailableHoursBE : BaseBE
    {

        #region Properties

        [DataMember]
        public int? ID { get; set; }

        [DataMember]
        public decimal? AvailableHours { get; set; }

        [DataMember]
        public int? EnumMappingValNbr { get; set; }

        [DataMember]
        public string PlanReviewTypeCd { get; set; }

        [DataMember]
        public int ProjectTypeRefId { get; set; }

        #endregion

    }

    #endregion

}