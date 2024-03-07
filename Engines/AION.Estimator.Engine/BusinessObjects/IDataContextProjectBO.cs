using AION.Estimator.Engine.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AION.Estimator.Engine.BusinessObjects
{
    public interface IDataContextProjectBO
    {
        List<ProjectBE> GetMMFProjectsComplete(DateTime startdate, DateTime enddate);
    }
}
