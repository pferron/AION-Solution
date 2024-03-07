#region Using

using AION.Base;
using System;
using System.Runtime.Serialization;

#endregion

namespace AION.Engine.BusinessEntities
{

    #region BusinessEntitiy - PlanReviewScheduleBE

    [DataContract]
    public class PlanReviewScheduleBEOLD : BaseBE
    {

        #region Properties

        [DataMember]
        public int? PlanReviewScheduleId { get; set; }
        [DataMember]
        public int? ProjectId { get; set; }
        [DataMember]
        public int? BusinessRefId { get; set; }
        [DataMember]
        public DateTime? StartDt { get; set; }
        [DataMember]
        public DateTime? EndDt { get; set; }
        [DataMember]
        public bool? PoolRequestInd { get; set; }
        [DataMember]
        public bool? FifoRequestInd { get; set; }
        [DataMember]
        public int? ApptResponseStatusRefId { get; set; }
        [DataMember]
        public int? PlanReviewProjectDetailsId { get; set; }
        [DataMember]
        public int Cycle { get; set; }
        [DataMember]
        public DateTime? ProdDate { get; set; }
        [DataMember]
        public bool RequestExpressNextCycle { get; set; }
        [DataMember]
        public bool IsFutureCycle { get; set; }
        [DataMember]
        public DateTime? ScheduleAfterDate { get; set; }
        [DataMember]
        public bool IsReschedule { get; set; }
        [DataMember]
        public DateTime? GateDate { get; set; }
        [DataMember]
        public bool IsCurrentCycle { get; set; }
        [DataMember]
        public decimal? ReReviewHours { get; set; }
        [DataMember]
        public decimal? ProposedHours { get; set; }
        [DataMember]
        public int? ProposedPlanReviewerId { get; set; }
        [DataMember]
        public int? ProjectCycleId { get; set; }

        #endregion

    }

    #endregion

}