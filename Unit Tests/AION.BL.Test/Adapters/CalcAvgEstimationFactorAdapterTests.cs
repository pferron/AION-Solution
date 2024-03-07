using AION.BL.Models;
using AION.BL.Test.MockRepositories;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.Adapters;
using AION.Manager.Models;
using AION.Manager.Models.Estimation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AION.BL.Test.Adapters
{
	[TestClass]
    public class CalcAvgEstimationFactorAdapterTests
    {
        CalcAvgEstimationFactorAdapter _adapter;

        MockLegacyProjectEstimationHoursRefBO mockLegacyProjectData = new MockLegacyProjectEstimationHoursRefBO();
		LegacyProjectEstimationHoursRefBO legacyProjectData = new LegacyProjectEstimationHoursRefBO();
        MockAutoEstimationRefBO mockRepoAutoEstimationRef = new MockAutoEstimationRefBO();
        MockProjectBusinessRelationshipBO mockProjectBusinessRelationship = new MockProjectBusinessRelationshipBO();
        MockAverageEstimationHoursFactorBO mockAverageEstimationHoursFactorBO = new MockAverageEstimationHoursFactorBO();
        MockProjectBO mockProjectBO = new MockProjectBO();

		ProjectEstimation _project = new ProjectEstimation();

		[TestInitialize]
        public void TestInitialize()
        {
            _adapter = new CalcAvgEstimationFactorAdapter(
				mockLegacyProjectData,
                mockRepoAutoEstimationRef, 
                mockProjectBusinessRelationship,
                mockAverageEstimationHoursFactorBO,
				mockProjectBO);
        }

        [TestMethod]
        public void TestIfUsingLegacyData()
        {
            DateTime goLiveDate = DateTime.Parse("01/15/2023");

            bool useLegacyData = _adapter.DetermineIfUsingLegacyData(goLiveDate);

            bool expectedValue = true;

            Assert.AreEqual(expectedValue, useLegacyData);

            goLiveDate = DateTime.Parse("01/15/2021");

			useLegacyData = _adapter.DetermineIfUsingLegacyData(goLiveDate);

            expectedValue = false;

            Assert.AreEqual(expectedValue, useLegacyData);
        }

        [TestMethod]
        public void TestGetEstimationConfigurationParameters()
        {
            var config = _adapter.GetEstimationConfigurationParameters();

            Assert.IsNotNull(config);
        }

        [TestMethod]
        public void TestGetMMFProjectsCompletedCount()
        {
            List<ProjectBE> completedProjects = _adapter.GetTotalCompletedMMFProjects();
            int expectedProjectCount = 6;
            int actualProjectCount = completedProjects.Count;
            Assert.AreEqual(expectedProjectCount, actualProjectCount);
        }

        [TestMethod]
        public void TestGetMMFProjectsCompletedSummary()
        {
            List<MMFHoursGroupingFlattened> flattenedProjects = _adapter.GetMMFProjectsCompletedSummary();

            //// upfit, factory industrial, building hours
            decimal expectedBuildingHours = 20;

            decimal actualBuildingHours = flattenedProjects.FirstOrDefault(x => x.OccupancyTypeId == 10
                && x.ConstructionTypeShortDesc == "UPFITRTAP").BuildingHours;

            Assert.AreEqual(expectedBuildingHours, actualBuildingHours);
        }

         [TestMethod]
        public void TestMMFProjectGroupings()
        {
            List<ProjectBE> projects = _adapter.GetTotalCompletedMMFProjects();

            List<MMFHoursGroupingFlattened> grouped = _adapter.GroupMMFCompletedProjects(projects);

            decimal expectedTotalProjects = 2;
            decimal actualTotalProjects = grouped.FirstOrDefault(x => x.OccupancyTypeId == 10 && x.ConstructionTypeShortDesc == "UPFITRTAP").TotalProjects;

            Assert.AreEqual(expectedTotalProjects, actualTotalProjects);
        }

        [TestMethod]
        public void TestGetLegacyAveragedData()
        {
			List<AveragedData> averagedData = _adapter.GetLegacyAveragedData();

            int expectedRowCount = 18;
            int actualRowCount = averagedData.Count;

            Assert.AreEqual(expectedRowCount, actualRowCount);
        }

        [TestMethod]
        public void TestGetAveragedData()
        {
            List<AveragedData> averagedData = _adapter.GetAveragedData();

            int expectedRowCount = 4;
            int actualRowCount = averagedData.Count;

            Assert.AreEqual(expectedRowCount, actualRowCount);
        }

        [TestMethod]
        public void TestDetermineStartDateDuringLegacyPeriod()
        {
            DateTime goLiveDate = new DateTime(2023, 01, 15);

			DateTime expectedStartDate = goLiveDate;
            
            DateTime actualStartDate = _adapter.DetermineStartDate(goLiveDate).Date;

            Assert.AreEqual(expectedStartDate, actualStartDate);
        }

		[TestMethod]
		public void TestDetermineStartDateAfterLegacyPeriod()
		{
			DateTime goLiveDate = new DateTime(2021, 01, 15);

			DateTime expectedStartDate = DateTime.Now.AddDays(-1).AddMonths(-24).Date;

			DateTime actualStartDate = _adapter.DetermineStartDate(goLiveDate).Date;

			Assert.AreEqual(expectedStartDate, actualStartDate);
		}

        [TestMethod]
        public void TestLegacyAveragedDataCalculations()
        {
			List<AveragedData> averagedData = _adapter.GetLegacyAveragedData();

			decimal expectedBuildingHours = 8.3m;
			decimal expectedElectricHours = 6.8m;
			decimal expectedMechanicalHours = 3.1m;
			decimal expectedPlumbingHours = 3.1m;
			decimal expectedSquareFeet = 423759.1m;
			decimal expectedCostOfConstruction = 44842736.6m;
			decimal expectedSheets = 173.9m;

			decimal actualBuilding = RoundDecimal(averagedData[1].AvgBuildingHours, 1);
			decimal actualElectric = RoundDecimal(averagedData[1].AvgElectricHours, 1);
			decimal actualMechanical = RoundDecimal(averagedData[1].AvgMechanicalHours, 1);
			decimal actualPlumbing = RoundDecimal(averagedData[1].AvgPlumbingHours, 1);
			decimal actualSquareFeet = RoundDecimal(averagedData[1].AvgSquareFeet, 1);
			decimal actualCostOfConstruction = RoundDecimal(averagedData[1].AvgCostOfConstruction, 1);
			decimal actualSheets = RoundDecimal(averagedData[1].AvgSheets, 1);

			Assert.AreEqual(expectedBuildingHours, actualBuilding);
            Assert.AreEqual(expectedElectricHours, actualElectric);
            Assert.AreEqual(expectedMechanicalHours, actualMechanical);
            Assert.AreEqual(expectedPlumbingHours, actualPlumbing);
            Assert.AreEqual(expectedSquareFeet, actualSquareFeet);
            Assert.AreEqual(expectedCostOfConstruction, actualCostOfConstruction);
            Assert.AreEqual(expectedSheets, actualSheets);
        }

        [TestMethod]
        public void TestGetFactors()
        {
            var config = _adapter.GetEstimationConfigurationParameters();

			List<AveragedData> averagedData = _adapter.GetLegacyAveragedData();

			AveragedData secondResult = averagedData[1];

			List<Factor> factors = _adapter.GetFactors(averagedData, config);

			decimal expectedBldgCOC = (secondResult.AvgBuildingHours / secondResult.AvgCostOfConstruction) * config.WeightCocNbr.Value;
			decimal expectedElectricCOC = (secondResult.AvgElectricHours / secondResult.AvgCostOfConstruction) * config.WeightCocNbr.Value;
			decimal expectedMechancialCOC = (secondResult.AvgMechanicalHours / secondResult.AvgCostOfConstruction) * config.WeightCocNbr.Value;
			decimal expectedPlumbingCOC = (secondResult.AvgPlumbingHours / secondResult.AvgCostOfConstruction) * config.WeightCocNbr.Value;

			decimal expectedBldgSheets = (secondResult.AvgBuildingHours / secondResult.AvgSheets) * config.WeightSheetsNbr.Value;
			decimal expectedElectricSheets = (secondResult.AvgElectricHours / secondResult.AvgSheets) * config.WeightSheetsNbr.Value;
			decimal expectedMechanicalSheets = (secondResult.AvgMechanicalHours / secondResult.AvgSheets) * config.WeightSheetsNbr.Value;
			decimal expectedPlumbingSheets = (secondResult.AvgPlumbingHours / secondResult.AvgSheets) * config.WeightSheetsNbr.Value;

			decimal expectedBldgSqFeet = (secondResult.AvgBuildingHours / secondResult.AvgSquareFeet) * config.WeightSqftNbr.Value;
			decimal expectedElectricSqFeet = (secondResult.AvgElectricHours / secondResult.AvgSquareFeet) * config.WeightSqftNbr.Value;
			decimal expectedMechanicalSqFeet = (secondResult.AvgMechanicalHours / secondResult.AvgSquareFeet) * config.WeightSqftNbr.Value;
			decimal expectedPlumbingSqFeet = (secondResult.AvgPlumbingHours / secondResult.AvgSquareFeet) * config.WeightSqftNbr.Value;

			Assert.AreEqual(RoundDecimal(expectedBldgCOC, 7), factors[1].BldgCOC);
			Assert.AreEqual(RoundDecimal(expectedElectricCOC, 7), factors[1].ElecCOC);
			Assert.AreEqual(RoundDecimal(expectedMechancialCOC, 7), factors[1].MechCOC);
			Assert.AreEqual(RoundDecimal(expectedPlumbingCOC, 7), factors[1].PlmgCOC);

			Assert.AreEqual(RoundDecimal(expectedBldgSheets, 7), factors[1].BldgSheets);
			Assert.AreEqual(RoundDecimal(expectedElectricSheets, 7), factors[1].ElecSheets);
			Assert.AreEqual(RoundDecimal(expectedMechanicalSheets, 7), factors[1].MechSheets);
			Assert.AreEqual(RoundDecimal(expectedPlumbingSheets, 7), factors[1].PlmgSheets);

			Assert.AreEqual(RoundDecimal(expectedBldgSqFeet, 7), factors[1].BldgSqFeet);
			Assert.AreEqual(RoundDecimal(expectedElectricSqFeet, 7), factors[1].ElecSqFeet);
			Assert.AreEqual(RoundDecimal(expectedMechanicalSqFeet, 7), factors[1].MechSqFeet);
			Assert.AreEqual(RoundDecimal(expectedPlumbingSqFeet, 7), factors[1].PlmgSqFeet);

			int expectedCount = 18;
			int actualCount = factors.Count;

			Assert.AreEqual(expectedCount, actualCount);
		}

        [TestMethod]
        public void TestSaveFactors()
        {
			var config = _adapter.GetEstimationConfigurationParameters();

			List<AveragedData> averagedData = _adapter.GetLegacyAveragedData();

			List<Factor> factors = _adapter.GetFactors(averagedData, config);

			_adapter.SaveFactors(factors);

			int expectedActiveRows = 18;
			int actualActiveRows = mockAverageEstimationHoursFactorBO.EstimationHoursFactors.Where(x => x.ActiveInd == true).Count();

			Assert.AreEqual(expectedActiveRows, actualActiveRows);
		}

		#region Private Methods

		private decimal RoundDecimal(decimal amountToRound, int decimalPlaces)
		{
			return Math.Round(amountToRound, decimalPlaces, MidpointRounding.AwayFromZero);
		}
		#endregion
	}
}
