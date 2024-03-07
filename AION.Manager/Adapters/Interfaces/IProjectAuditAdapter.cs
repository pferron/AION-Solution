using AION.Manager.Models;
using System.Collections.Generic;

namespace AION.Manager.Adapters
{
    public interface IProjectAuditAdapter
    {
        List<ProjectAudit> GetProjectAudits(int projectid);
    }
}
