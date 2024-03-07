using AION.Engine.BusinessEntities;
using System.Collections.Generic;

namespace AION.Manager.Adapters
{
    public interface IDataContextProjectCycleBO
    {
        List<ProjectCycleBE> GetListByProject(int projectId);
        int Update(ProjectCycleBE projectCycleBE);
    }
}
