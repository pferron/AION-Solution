#region Using

using AION.Base;
using System;
using System.Runtime.Serialization;

#endregion

namespace AION.Engine.BusinessEntities
{

    #region BusinessEntitiy - PlanReviewScheduleDetailBE

    [DataContract]
    public class PlanReviewScheduleDetailBE : BaseBE
    {

        #region Properties

        [DataMember]
        public int? PlanReviewScheduleDetailId { get; set; }

        [DataMember]
        public int? PlanReviewScheduleId { get; set; }

        [DataMember]
        public int? BusinessRefId { get; set; }

        [DataMember]
        public DateTime? StartDt { get; set; }

        [DataMember]
        public DateTime? EndDt { get; set; }

        [DataMember]
        public bool? PoolRequestInd { get; set; }

        [DataMember]
        public bool? SameBuildContrInd { get; set; }

        [DataMember]
        public bool? ManualAssignmentInd { get; set; }

        [DataMember]
        public decimal? AssignedHoursNbr { get; set; }

        [DataMember]
        public int? AssignedPlanReviewerId { get; set; }

        #endregion

        #region properties added for views
        [DataMember]
        public string AssignedReviewerLastName { get; set; }

        [DataMember]

        public string AssignedReviewerFirstName { get; set; }

        #endregion properties added for views
    }

    #endregion

}