using AION.Estimator.Engine.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AION.Estimator.Engine.BusinessObjects
{
    public interface IDataContextProjectBusinessRelationshipBO
    {
        int Create(ProjectBusinessRelationshipBE projectBusinessRelationshipBE);
        int Delete(int id);
        ProjectBusinessRelationshipBE GetById(int id);
        DataSet GetDataSet(int id);
        List<ProjectBusinessRelationshipBE> GetList(int id);
        int Update(ProjectBusinessRelationshipBE projectBusinessRelationshipBE);
        List<ProjectBusinessRelationshipBE> GetListByProjectId(int id);
    }
}
