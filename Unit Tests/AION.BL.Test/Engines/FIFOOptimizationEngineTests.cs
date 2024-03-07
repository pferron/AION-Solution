using AION.BL.Common;
using AION.BL.Models;
using AION.Manager.Engines.Scheduling;
using AION.Manager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AION.BL.Test.Engines
{
    [TestClass]
    public class FIFOOptimizationEngineTests
    {
        private FIFOOptimizationEngine _Engine;
        private FIFOEngineParams _OptimizationParams;

        [TestInitialize]
        [TestMethod]
        public void TestInitialize()
        {
            ProjectEstimation currentProject = new ProjectEstimation()
            {
                AccelaPropertyType = PropertyTypeEnums.FIFO_Small_Commercial,
                FifoDueDt = DateTimeHelper.DetermineWorkDateAfterDateSpecified(DateTime.Now.AddDays(1), 7),
                ID = 12345,
                Trades = new List<ProjectTrade>()
                {
                    new ProjectTrade() { DepartmentInfo = DepartmentNameEnums.Building, ExcludedPlanReviewers = new List<int>() },
                }
            };

            PlanReview fifoSchedule = new PlanReview()
            {
                Cycle = 1,
                IsSameBuildingContractor = false,
                IsManualAssignment = false,
                ProjectId = 12345,
                ApptResponseStatusEnum = AppointmentResponseStatusEnum.Scheduled,
                BuildStartDate = DateTime.Parse("7/09/2021 10:00 AM"),
                BuildEndDate = DateTime.Parse("7/09/2021 12:00 PM"),
                HoursBuilding = 2,
                BuildAssignedReviewerId = 269,
                EarliestDate = DateTime.Parse("7/09/2021 10:00 AM"),
                BuildPlanReviewScheduleId = 222,
            };

            _OptimizationParams = new FIFOEngineParams()
            {
                CurrentProject = currentProject,
                PlanReview = fifoSchedule,
                AccelaWorkflowTaskStatus = "Not started"
            };
        }

        [TestMethod]
        [Ignore]
        public void TestSchedulingEngine()
        {
            _Engine = new FIFOOptimizationEngine(_OptimizationParams);

            Assert.AreEqual(1, 1);
        }
    }
}
