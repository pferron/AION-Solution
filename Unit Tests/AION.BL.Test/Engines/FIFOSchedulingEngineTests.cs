using AION.Manager.Engines;
using AION.Manager.Models;
using Meck.Azure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AION.BL.Test.Engines
{
    [TestClass]
    public class FIFOSchedulingEngineTests
    {
        private FIFOSchedulingEngine _Engine;

        [TestInitialize]
        public void SetupAccelaEngineTests()
        {
            Meck.Shared.Globals.AccelaAuthBaseUrl = GetConfigValue("AccellaAuthbaseUrl");
            Meck.Shared.Globals.AccelaAgency = GetConfigValue("AccelaAuthAgency");
            Meck.Shared.Globals.AccelaEnvironment = GetConfigValue("AccelaEnvironment");
            Meck.Shared.Globals.AccelaScope = GetConfigValue("AccelaScope");

            Meck.Shared.Globals.AccelaClientId = KeyVaultUtility.GetSecret("AccelaClientID");
            Meck.Shared.Globals.AccelaClientSecret = KeyVaultUtility.GetSecret("AccelaClientSecret");
            Meck.Shared.Globals.AccelaUser = KeyVaultUtility.GetSecret("AccelaUserID");
            Meck.Shared.Globals.AccelaUserName = KeyVaultUtility.GetSecret("AccelaUserID");
            Meck.Shared.Globals.AccelaPassword = KeyVaultUtility.GetSecret("AccelaPassword");
            Meck.Shared.Globals.AIONConnectionString = KeyVaultUtility.GetSecret("KeyVaultConnectionString");
        }

        private string GetConfigValue(string settingname)
        {
            return ConfigurationManager.AppSettings[settingname];

        }

        [TestMethod]
        [Ignore]
        public void TestSchedulingEngine()
        {
            FIFOEngineParams parms = new FIFOEngineParams();
            parms.AccelaProjectIDRef = "SCP-000419";
            parms.RecIdTxt = "REC24-00000-00009";
            parms.Cycle = 2;
            _Engine = new FIFOSchedulingEngine(parms);
            Assert.AreEqual(1, 1);
        }

        [TestMethod]
        [Ignore]
        public void GetOtherEligibleFifoReviewers()
        {
            FIFOEngineParams parms = new FIFOEngineParams();
            parms.AccelaProjectIDRef = "JANESSA_LES_677-D";
            parms.RecIdTxt = "JANESSA-REC-ID-TXT-02";
            _Engine = new FIFOSchedulingEngine(parms);
            _Engine.GetOtherEligibleFifoReviewers();
            Assert.AreEqual(1, 1);
        }

        [TestMethod]
        [Ignore]
        public void GetAutoScheduledValues()
        {
            FIFOEngineParams parms = new FIFOEngineParams();
            parms.AccelaProjectIDRef = "JANESSA_LES_677-R";
            parms.RecIdTxt = "JANESSA-REC-ID-TXT-03";
            _Engine = new FIFOSchedulingEngine(parms);
            Assert.AreEqual(1, 1);
        }

        [TestMethod]
        public void GetAdjustedListForCityZoningLastAssignedUser()
        {
            UserIdentity lastAssigned = new UserIdentity() { LastName = "Smith", FirstName = "Joe", ID = 4 };

            List<AutoSchedulableReviewer> reviewersForSearch = new List<AutoSchedulableReviewer>()
            {
                new AutoSchedulableReviewer() { UserIdentity = new UserIdentity() { LastName = "Allen", FirstName = "Janessa", ID = 1 } },
                new AutoSchedulableReviewer() { UserIdentity = new UserIdentity() { LastName = "Kaufman", FirstName = "Andy", ID = 5 } },
                new AutoSchedulableReviewer() { UserIdentity = new UserIdentity() { LastName = "Mayer", FirstName = "John" , ID = 8} },
                new AutoSchedulableReviewer() { UserIdentity = new UserIdentity() { LastName = "Snyder", FirstName = "William", ID = 7 } },
                new AutoSchedulableReviewer() { UserIdentity = new UserIdentity() { LastName = "Taubman", FirstName = "Franklin", ID = 3 } }
            };

            FIFOSchedulingEngine engine = new FIFOSchedulingEngine();
            engine.LastAssignedCityZoningReviewer = lastAssigned;

            List<AutoSchedulableReviewer> adjustedList = engine.AdjustReviewerListOrderByLastReviewerAssigned(reviewersForSearch);

            AutoSchedulableReviewer nextReviewer = reviewersForSearch.FirstOrDefault(x => x.UserIdentity.ID == 7);

            int expectedIndex = 0;
            int actualIndex = adjustedList.IndexOf(nextReviewer);

            Assert.AreEqual(expectedIndex, actualIndex);
        }
    }
}
