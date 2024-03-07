using AION.BL.Models;
using AION.Engine.BusinessEntities;
using AION.Manager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AION.BL.Test.Models
{
    [TestClass]
    public class ProjectCycleProcessorTests
    {
        private ProjectCycleBE _CurrentCycle;
        private ProjectCycleBE _FutureCycle;
        private List<ProjectCycleBE> _ProjectCyclesNoFutureCycle = new List<ProjectCycleBE>();
        private List<ProjectCycleBE> _ProjectCyclesWithFutureCycle = new List<ProjectCycleBE>();

        private ProjectEstimation _Project;

        private ProjectCycleProcessor _ProjectCycleProcessor;

        [TestInitialize]
        public void TestInitialize()
        {
            _Project = new ProjectEstimation();
            _ProjectCycleProcessor = new ProjectCycleProcessor(_Project);

            _CurrentCycle = new ProjectCycleBE()
            {
                CurrentCycleInd = true,
                CycleNbr = 1,
                FutureCycleInd = false,
                PlansReadyOnDt = DateTime.Now.AddDays(3)
            };

            _FutureCycle = new ProjectCycleBE()
            {
                CurrentCycleInd = false,
                CycleNbr = 2,
                FutureCycleInd = true,
                PlansReadyOnDt = DateTime.Now.AddDays(10)
            };

            _ProjectCyclesNoFutureCycle.Add(_CurrentCycle);

            _ProjectCyclesWithFutureCycle.Add(_CurrentCycle);
            _ProjectCyclesWithFutureCycle.Add(_FutureCycle);
        }

        [TestMethod]
        public void TestExistingCycleIsNullWhenNoProjectCyclesExist()
        {
            _ProjectCycleProcessor.ProjectCycles = new List<ProjectCycleBE>();

            _ProjectCycleProcessor.GetCyclesForProject();

            Assert.AreEqual(0, _ProjectCycleProcessor.ProjectCycles.Count);
            Assert.IsNull(_ProjectCycleProcessor.CurrentCycleBE);
        }
    }
}
