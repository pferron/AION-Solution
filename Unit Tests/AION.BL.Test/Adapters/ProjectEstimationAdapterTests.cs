using AION.BL.Adapters;
using AION.Estimator.Engine.BusinessEntities;
using Meck.Azure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AION.BL.Test.Adapters
{
    [TestClass]
    public class ProjectEstimationAdapterTests
    {
        AverageEstimationHoursFactorBE _averageEstimationHoursFactorBE;
        ProjectEstimationAdapter _projectEstimationAdapter;
        string _occupancyType;
        string _typeOfWork;
        List<ProjectTrade> _trades;
        double _coc;
        int _sheets;
        int _sqFt;

        [TestInitialize]
        public void Initialize()
        {
            _averageEstimationHoursFactorBE = new AverageEstimationHoursFactorBE();
            _projectEstimationAdapter = new ProjectEstimationAdapter();
            _occupancyType = string.Empty;
            _typeOfWork = string.Empty;
            _trades = new List<ProjectTrade>();
        }
        [TestMethod]
        [Ignore]
        public void GetAverageEstimationHoursFactorsReturnsObj()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _typeOfWork = "New Construction";
            _occupancyType = "A2";
            decimal? bsqftfactor = 0.000131436802M;
            decimal? esqftfactor = 0.000117032769M;
            decimal? msqftfactor = 0.000041411595M;
            decimal? psqftfactor = 0.000037810587M;
            decimal? bcocfactor = 0.000000079451M;
            decimal? ecocfactor = 0.000000070744M;
            decimal? mcocfactor = 0.000000025033M;
            decimal? pcocfactor = 0.000000022856M;
            decimal? bshtsfactor = 0.009240506329M;
            decimal? eshtsfactor = 0.008227848101M;
            decimal? mshtsfactor = 0.002911392405M;
            decimal? pshtsfactor = 0.002658227848M;

            _averageEstimationHoursFactorBE = _projectEstimationAdapter.GetAverageEstimationHoursFactors(_occupancyType, _typeOfWork);
            Assert.IsTrue(bsqftfactor == _averageEstimationHoursFactorBE.BuildingSqftFactor);
            Assert.IsTrue(esqftfactor == _averageEstimationHoursFactorBE.ElectricalSqftFactor);
            Assert.IsTrue(msqftfactor == _averageEstimationHoursFactorBE.MechanicalSqftFactor);
            Assert.IsTrue(psqftfactor == _averageEstimationHoursFactorBE.PlumbingSqftFactor);
            Assert.IsTrue(bcocfactor == _averageEstimationHoursFactorBE.BuildingCocFactor);
            Assert.IsTrue(ecocfactor == _averageEstimationHoursFactorBE.ElectricalCocFactor);
            Assert.IsTrue(mcocfactor == _averageEstimationHoursFactorBE.MechanicalCocFactor);
            Assert.IsTrue(pcocfactor == _averageEstimationHoursFactorBE.PlumbingCocFactor);
            Assert.IsTrue(bshtsfactor == _averageEstimationHoursFactorBE.BuildingSheetsFactor);
            Assert.IsTrue(eshtsfactor == _averageEstimationHoursFactorBE.ElectricalSheetsFactor);
            Assert.IsTrue(mshtsfactor == _averageEstimationHoursFactorBE.MechanicalSheetsFactor);
            Assert.IsTrue(pshtsfactor == _averageEstimationHoursFactorBE.PlumbingSheetsFactor);

        }
        [TestMethod]
        [Ignore]
        public void EstimatorToolReturnsCorrectCalculations()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _typeOfWork = "New Construction";
            _occupancyType = "BUSINESS";
            _coc = 2500000.00000000;
            _sheets = 25;
            _sqFt = 43050;
            _trades = new List<ProjectTrade>();
            ProjectTrade building = new ProjectTrade();
            ProjectTrade electrical = new ProjectTrade();
            ProjectTrade mechanical = new ProjectTrade();
            ProjectTrade plumbing = new ProjectTrade();
            decimal? buildingestimate = 2.0M;
            decimal? electricalestimate = 1.8M;
            decimal? mechanicalestimate = 0.6M;
            decimal? plumbingestimate = 0.6M;

            _trades.Add(new ProjectTrade
            {
                DepartmentTypeEnum = DepartmentTypeEnum.Trade,
                DepartmentInfo = DepartmentNameEnums.Building
            });
            _trades.Add(new ProjectTrade
            {
                DepartmentTypeEnum = DepartmentTypeEnum.Trade,
                DepartmentInfo = DepartmentNameEnums.Electrical
            });
            _trades.Add(new ProjectTrade
            {
                DepartmentTypeEnum = DepartmentTypeEnum.Trade,
                DepartmentInfo = DepartmentNameEnums.Mechanical
            });
            _trades.Add(new ProjectTrade
            {
                DepartmentTypeEnum = DepartmentTypeEnum.Trade,
                DepartmentInfo = DepartmentNameEnums.Plumbing
            });

            var o = _projectEstimationAdapter.EstimatorTool(_trades, _coc, _sheets, _sqFt, _occupancyType, _typeOfWork);
            building = _trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Building).FirstOrDefault();
            electrical = _trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Electrical).FirstOrDefault();
            mechanical = _trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Mechanical).FirstOrDefault();
            plumbing = _trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Plumbing).FirstOrDefault();

            Assert.IsTrue(Math.Round((decimal)building.EstimationHours, 1, MidpointRounding.AwayFromZero) == buildingestimate);
            Assert.IsTrue(Math.Round((decimal)electrical.EstimationHours, 1, MidpointRounding.AwayFromZero) == electricalestimate);
            Assert.IsTrue(Math.Round((decimal)mechanical.EstimationHours, 1, MidpointRounding.AwayFromZero) == mechanicalestimate);
            Assert.IsTrue(Math.Round((decimal)plumbing.EstimationHours, 1, MidpointRounding.AwayFromZero) == plumbingestimate);
        }

        //[TestMethod]
        public void TestIfAllDepartmentEstimationsComplete()
        {
            bool isComplete = _projectEstimationAdapter.CheckIfAllDepartmentsComplete(8045);
            Assert.IsTrue(isComplete);
        }

        [TestMethod]
        public void MeckAzureKeyVaultTest()
        {
            var connectionstring = KeyVaultUtility.GetSecret("KeyVaultConnectionString");
            Assert.IsNotNull(connectionstring);
        }
    }
}
