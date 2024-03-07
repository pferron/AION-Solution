using AION.BL;
using AION.BL.Models;
using System;

namespace AION.Manager.Models
{
    public class ProjectAudit : ModelBase
    {
        #region Properties
        public AuditActionRef AuditActionRef { get; set; }

        public int ProjectAuditId { get; set; }

        public int ProjectId { get; set; }

        public string AuditActionDetailsTxt { get; set; }

        public string AuditUserId { get; set; }

        public DateTime AuditDt { get; set; }
        public int AuditActionRefId { get; set; }

        public UserIdentity AuditUser { get; set; }
        public AuditActionEnum AuditActionEnum { get; set; }

        public int? ProjectCycleId { get; set; }

        public int? CycleNbr { get; set; }
        #endregion

    }
}
