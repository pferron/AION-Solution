#region Using

using AION.Base;
using System.Runtime.Serialization;

#endregion

namespace AION.Estimator.Engine.BusinessEntities
{

    #region BusinessEntitiy - ProjectBusinessRelationshipBE

    [DataContract]
    public class ProjectBusinessRelationshipBE : BaseBE
    {

        #region Properties

        [DataMember]
        public int? ProjectBusinessRelationshipId { get; set; }

        [DataMember]
        public decimal? EstimationHoursNbr { get; set; }

        [DataMember]
        public int? BusinessRefId { get; set; }

        [DataMember]
        public int? ProjectId { get; set; }

        [DataMember]
        public int? AssignedPlanReviewerId { get; set; }

        [DataMember]
        public int? ProposedPlanReviewerId { get; set; }

        [DataMember]
        public int? SecondaryPlanReviewerId { get; set; }

        [DataMember]
        public int? PrimaryPlanReviewerId { get; set; }
        /// <summary>
        /// Stores value regarding the specific department is set explicitly applicable from UI. This is only set from UI and not anywhere else.
        /// </summary>
        [DataMember]
        public bool IsEstimationNotApplicable { get; set; }
        [DataMember]
        public string ProjectBusinessRelationshipStatusDesc { get; set; }
        [DataMember]
        public int StatusRefId { get; set; }
        #endregion
        [DataMember]
        public bool IsDeptRequested { get; set; }
        [DataMember]
        public decimal? ActualHoursNbr { get; set; }
    }

    #endregion

}