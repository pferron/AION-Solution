using AION.Estimator.Engine.BusinessEntities;
using AION.Manager.Models.Estimation;
using System.Collections.Generic;

namespace AION.Manager.Adapters
{
    public interface ICalcAvgEstimationFactorAdapter
    {
        List<AveragedData> GetLegacyAveragedData();

        List<AveragedData> GetAveragedData();

        List<Factor> GetFactors(List<AveragedData> averagedData, AutoEstimationRefBE config);

        bool SaveFactors(List<Factor> factorData);

        bool ProcessData();
    }
}
