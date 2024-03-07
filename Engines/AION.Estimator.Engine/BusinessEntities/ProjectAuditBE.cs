#region Using

using AION.Base;
using System;
using System.Runtime.Serialization;

#endregion

namespace AION.Estimator.Engine.BusinessEntities
{

    #region BusinessEntitiy - ProjectAuditBE

    [DataContract]
    public class ProjectAuditBE : BaseBE
    {

        #region Properties

        [DataMember]
        public int ProjectAuditId { get; set; }

        [DataMember]
        public int ProjectId { get; set; }

        [DataMember]
        public string AuditActionDetailsTxt { get; set; }

        [DataMember]
        public string AuditUserId { get; set; }

        [DataMember]
        public DateTime? AuditDt { get; set; }

        [DataMember]
        public int AuditActionRefId { get; set; }

        [DataMember]
        public int? ProjectCycleId { get; set; }

        [DataMember]
        public int? CycleNbr { get; set; }
        #endregion

    }

    #endregion

}