using AION.Estimator.Engine.BusinessEntities;
using System.Collections.Generic;
using System.Data;

namespace AION.Estimator.Engine.BusinessObjects
{
    public interface IDataContextAutoEstimationRefBO
    {
        int Create(AutoEstimationRefBE autoEstimationRefBE);
        int Delete(int id);
        AutoEstimationRefBE GetById(int id);
        DataSet GetDataSet(int id);
        List<AutoEstimationRefBE> GetList(int id);
        AutoEstimationRefBE GetActive();
        int Update(AutoEstimationRefBE autoEstimationRefBE);
    }
}
