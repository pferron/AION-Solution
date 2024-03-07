using AION.Engine.BusinessEntities;
using AION.Manager.Adapters;
using System;
using System.Collections.Generic;

namespace AION.BL.Test.MockRepositories
{
    public class MockProjectCycleBO : IDataContextProjectCycleBO
    {
        public List<ProjectCycleBE> GetListByProject(int projectId)
        {
            throw new NotImplementedException();
        }

        public int Update(ProjectCycleBE projectCycleBE)
        {
            throw new NotImplementedException();
        }
    }
}
