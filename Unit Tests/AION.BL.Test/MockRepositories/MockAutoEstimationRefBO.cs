using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;

namespace AION.BL.Test.MockRepositories
{
    public class MockAutoEstimationRefBO : IDataContextAutoEstimationRefBO
    {
        public int Create(AutoEstimationRefBE entity)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public AutoEstimationRefBE GetById(int id)
        {
            throw new NotImplementedException();
        }

        public DataSet GetDataSet(int id)
        {
            throw new NotImplementedException();
        }

        public List<AutoEstimationRefBE> GetList(int id)
        {
            throw new NotImplementedException();
        }

        public AutoEstimationRefBE GetActive()
        {
            return new AutoEstimationRefBE()
            {
                ActiveDate= DateTime.Now,
                ActiveInd = true,
                AutoEstimationRefId = 1,
                Months = 24,
                WeightSqftNbr = Convert.ToDecimal(.80),
                WeightCocNbr = Convert.ToDecimal(.10),
                WeightSheetsNbr = Convert.ToDecimal(.10)
            };
        }

        public int Update(AutoEstimationRefBE entity)
        {
            throw new NotImplementedException();
        }
    }
}
