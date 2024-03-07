using AION.Estimator.Engine.BusinessEntities;
using System.Collections.Generic;

namespace AION.Estimator.Engine.BusinessObjects
{
    public interface IDataContextLegacyProjectDataBO
    {
        List<LegacyProjectEstimationHoursRefBE> GetList();
    }
}
