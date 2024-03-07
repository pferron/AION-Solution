#region Using

using AION.Base;
using System.Runtime.Serialization;

#endregion

namespace AION.Estimator.Engine.BusinessEntities
{

    #region BusinessEntitiy - ExcludedPlanReviewersBE

    [DataContract]
    public class ExcludedPlanReviewersBE : BaseBE
    {

        #region Properties

        [DataMember]
        public int? ExcludedPlanReviewerId { get; set; }

        [DataMember]
        public int? PlanReviewerId { get; set; }

        [DataMember]
        public int? ProjectBusinessRelationshipId { get; set; }

        [DataMember]
        public int? BusinessRefId { get; set; }

        #endregion

    }

    #endregion

}