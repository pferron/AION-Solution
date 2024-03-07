using AION.Estimator.Engine.BusinessEntities;
using System.Collections.Generic;
using System.Data;

namespace AION.Estimator.Engine.BusinessObjects
{
    public interface IDataContextAverageEstimationHoursFactorBO
    {
        int Create(AverageEstimationHoursFactorBE averageEstimationHoursFactorBE);
        int Delete(int id);
        AverageEstimationHoursFactorBE GetById(int id);
        DataSet GetDataSet(int id);
        List<AverageEstimationHoursFactorBE> GetList(int id);
        int Update(AverageEstimationHoursFactorBE averageEstimationHoursFactorBE);
        AverageEstimationHoursFactorBE GetByOccupancyTypConstructionTyp(string occupancytyp, string constructiontyp);
        int SetRowActive(int occupancytyp, string constructiontyp, bool active, string wrkId);
    }
}
