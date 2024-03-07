using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AION.BL.Test.MockRepositories
{
    public class MockAverageEstimationHoursFactorBO : IDataContextAverageEstimationHoursFactorBO
    {
        public List<AverageEstimationHoursFactorBE> EstimationHoursFactors { get; set; } =
            new List<AverageEstimationHoursFactorBE>() {
            new AverageEstimationHoursFactorBE() { OccupancyTypRefId = 1, ConstructionType = "UPFITRTAP" },
            new AverageEstimationHoursFactorBE() { OccupancyTypRefId = 1, ConstructionType = "NEWCONSTRUCTION" },
            new AverageEstimationHoursFactorBE() { OccupancyTypRefId = 2, ConstructionType = "UPFITRTAP" },
            new AverageEstimationHoursFactorBE() { OccupancyTypRefId = 2, ConstructionType = "NEWCONSTRUCTION" },
            new AverageEstimationHoursFactorBE() { OccupancyTypRefId = 3, ConstructionType = "UPFITRTAP" },
            new AverageEstimationHoursFactorBE() { OccupancyTypRefId = 3, ConstructionType = "NEWCONSTRUCTION" },
            new AverageEstimationHoursFactorBE() { OccupancyTypRefId = 4, ConstructionType = "UPFITRTAP" },
            new AverageEstimationHoursFactorBE() { OccupancyTypRefId = 4, ConstructionType = "NEWCONSTRUCTION" },
            new AverageEstimationHoursFactorBE() { OccupancyTypRefId = 5, ConstructionType = "UPFITRTAP" },
            new AverageEstimationHoursFactorBE() { OccupancyTypRefId = 5, ConstructionType = "NEWCONSTRUCTION" },
            new AverageEstimationHoursFactorBE() { OccupancyTypRefId = 6, ConstructionType = "UPFITRTAP" },
            new AverageEstimationHoursFactorBE() { OccupancyTypRefId = 6, ConstructionType = "NEWCONSTRUCTION" },
            new AverageEstimationHoursFactorBE() { OccupancyTypRefId = 7, ConstructionType = "UPFITRTAP" },
            new AverageEstimationHoursFactorBE() { OccupancyTypRefId = 7, ConstructionType = "NEWCONSTRUCTION" },
            new AverageEstimationHoursFactorBE() { OccupancyTypRefId = 8, ConstructionType = "UPFITRTAP" },
            new AverageEstimationHoursFactorBE() { OccupancyTypRefId = 8, ConstructionType = "NEWCONSTRUCTION" },
            new AverageEstimationHoursFactorBE() { OccupancyTypRefId = 10, ConstructionType = "UPFITRTAP" },
            new AverageEstimationHoursFactorBE() { OccupancyTypRefId = 10, ConstructionType = "NEWCONSTRUCTION" }
            };

        public int Create(AverageEstimationHoursFactorBE averageEstimationHoursFactorBE)
        {
            EstimationHoursFactors.Add(averageEstimationHoursFactorBE);

            return EstimationHoursFactors.Count();
        }

        public int Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public AverageEstimationHoursFactorBE GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public AverageEstimationHoursFactorBE GetByOccupancyTypConstructionTyp(string occupancytyp, string constructiontyp)
        {
            throw new System.NotImplementedException();
        }

        public DataSet GetDataSet(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<AverageEstimationHoursFactorBE> GetList(int id)
        {
            throw new System.NotImplementedException();
        }

        public int SetRowActive(int occupancytyp, string constructiontyp, bool active, string wrkId)
        {
            var rowToUpdate = EstimationHoursFactors
                .FirstOrDefault(x => x.OccupancyTypRefId == occupancytyp && x.ConstructionType == constructiontyp);

            if (rowToUpdate != null)
            {
                rowToUpdate.ActiveInd = active;
            }

            return 1;
        }

        public int Update(AverageEstimationHoursFactorBE averageEstimationHoursFactorBE)
        {
            throw new System.NotImplementedException();
        }
    }
}
