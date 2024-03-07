using AION.Accela.Engine;
using AION.Accela.Engine.BusinessObjects;
using AION.BL.Models;
using AION.BL.Test.MockRepositories;
using AION.Manager.Adapters;
using Meck.Azure;
using Meck.Shared;
using Microsoft.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace AION.BL.Test.Adapters
{
    [TestClass]
    public class CloseProjectAdapterTests
    {
        CloseProjectAdapter _adapter;
        MockProjectBusinessRelationshipBO mockProjectBusinessRelationship = new MockProjectBusinessRelationshipBO();
        IAccelaEngine mockAccelaEngine = new MockAccelaEngine();
        MockProjectCycleBO mockProjectCycleBO = new MockProjectCycleBO();

        [TestInitialize]
        public void TestInitialize()
        {
            _adapter = new CloseProjectAdapter(
                mockAccelaEngine,
                mockProjectCycleBO,
                mockProjectBusinessRelationship);


            Meck.Shared.Globals.AccelaAuthBaseUrl = GetConfigValue("AccellaAuthbaseUrl");
            Meck.Shared.Globals.AccelaAgency = GetConfigValue("AccelaAuthAgency");
            Meck.Shared.Globals.AccelaEnvironment = GetConfigValue("AccelaEnvironment");

            Meck.Shared.Globals.AccelaScope = GetConfigValue("AccelaScope");
            Meck.Shared.Globals.AccelaClientSecret = KeyVaultUtility.GetSecret("AccelaClientSecret");
            Meck.Shared.Globals.AccelaUser = KeyVaultUtility.GetSecret("AccelaUserID");
            Meck.Shared.Globals.AccelaUserName = KeyVaultUtility.GetSecret("AccelaUserID");
            Meck.Shared.Globals.AccelaPassword = KeyVaultUtility.GetSecret("AccelaPassword");
            Meck.Shared.Globals.AccelaClientId = KeyVaultUtility.GetSecret("AccelaClientID");

            Meck.Shared.Globals.AIONConnectionString = KeyVaultUtility.GetSecret("KeyVaultConnectionString");
        }

        [TestMethod]
        public void UpdateProjectActualHoursTest()
        {
            CloseProjectAdapter closeProjectAdapter = 
                new CloseProjectAdapter(mockAccelaEngine, mockProjectCycleBO, mockProjectBusinessRelationship);

            List<TaskReview> taskReviews = GetTaskReviews();

            bool success = _adapter.UpdateProjectActualHours(mockProjectBusinessRelationship.ProjectTrades, taskReviews);

            decimal expectedBusinessActualHours = 4;
            decimal expectedEngineeringActualHours = 3;
            decimal expectedMechanicalActualHours = 2;
            decimal expectedPlumbingActualHours = 1;

            Assert.AreEqual(expectedBusinessActualHours, mockProjectBusinessRelationship.ProjectTrades.FirstOrDefault(x => x.BusinessRefId == 1).ActualHoursNbr);
            Assert.AreEqual(expectedEngineeringActualHours, mockProjectBusinessRelationship.ProjectTrades.FirstOrDefault(x => x.BusinessRefId == 2).ActualHoursNbr);
            Assert.AreEqual(expectedMechanicalActualHours, mockProjectBusinessRelationship.ProjectTrades.FirstOrDefault(x => x.BusinessRefId == 3).ActualHoursNbr);
            Assert.AreEqual(expectedPlumbingActualHours, mockProjectBusinessRelationship.ProjectTrades.FirstOrDefault(x => x.BusinessRefId == 4).ActualHoursNbr);
        }

        private List<TaskReview> GetTaskReviews()
        {
            return new List<TaskReview>()
            {
                 new TaskReview() { Department = "Commercial Building", HoursSpent = "4", IsCompleted = "Y"},
                 new TaskReview() { Department = "Commercial Electrical", HoursSpent = "3", IsCompleted = "Y"},
                 new TaskReview() { Department = "Commercial Mechanical", HoursSpent = "2", IsCompleted = "Y"},
                 new TaskReview() { Department = "Commercial Plumbing", HoursSpent = "1", IsCompleted = "Y"}
            };
        }

        private string GetConfigValue(string settingname)
        {
            return ConfigurationManager.AppSettings[settingname];
        }
    }
}
