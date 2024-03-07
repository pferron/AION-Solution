using AION.BL.BusinessObjects;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.Models;
using Meck.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AION.Manager.Adapters
{
    public class ProjectAuditAdapter : BaseManagerAdapter, IProjectAuditAdapter
    {
        public List<ProjectAudit> GetProjectAudits(int projectid)
        {
            try
            {
                ProjectAuditBO projectAuditBO = new ProjectAuditBO();
                List<ProjectAuditBE> audits = projectAuditBO.GetList(projectid);
                return audits.Select(x => ConvertProjectAuditBEToProjectAudit(x)).ToList();

            }
            catch (Exception ex)
            {
                string errorMessage = "Error in GetProjectAudits - projectid: " + projectid.ToString() + " - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw (ex);
            }

        }
        private ProjectAudit ConvertProjectAuditBEToProjectAudit(ProjectAuditBE projectAuditBE)
        {
            int userid = 0;
            bool isuserid = int.TryParse(projectAuditBE.AuditUserId, out userid);
            var easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

            return new ProjectAudit
            {
                AuditActionDetailsTxt = projectAuditBE.AuditActionDetailsTxt,
                AuditActionRefId = projectAuditBE.AuditActionRefId,
                AuditDt = TimeZoneInfo.ConvertTimeFromUtc((DateTime)projectAuditBE.AuditDt.Value, easternZone),
                AuditUserId = projectAuditBE.AuditUserId,
                ID = projectAuditBE.AuditActionRefId,
                ProjectAuditId = projectAuditBE.ProjectAuditId,
                ProjectId = projectAuditBE.ProjectId,
                AuditUser = isuserid ? new UserIdentityModelBO().GetInstance(userid) : new BL.UserIdentity(),
                CycleNbr = projectAuditBE.CycleNbr,
                ProjectCycleId = projectAuditBE.ProjectCycleId
            };
        }
    }
}