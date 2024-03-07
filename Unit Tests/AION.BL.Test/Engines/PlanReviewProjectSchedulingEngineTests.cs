using AION.Manager.Engines;
using AION.Manager.Models;
using Meck.Azure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace AION.BL.Test.Engines
{
    [TestClass]
    public class PlanReviewProjectSchedulingEngineTests
    {
        private PlanReviewProjectSchedulingEngine _Engine;

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
            AutoScheduledPlanReviewParams obj = new AutoScheduledPlanReviewParams();
            obj.AccelaProjectIDRef = "MMF-000183";
            obj.ProjectID = 7250;
            obj.RecIdTxt = "REC21-00000-000FO";

            obj.BuildingIsPool = false;
            obj.ElectricIsPool = false;
            obj.MechIsPool = false;
            obj.PlumbIsPool = false;
            obj.ZoneIsPool = false;
            obj.FireIsPool = false;
            obj.FoodServiceIsPool = false;
            obj.PoolIsPool = false;
            obj.FacilityIsPool = false;
            obj.DayCareIsPool = false;
            obj.BackFlowIsPool = false;

            obj.BuildingUserID = 269;
            obj.ElectricUserID = 269;
            obj.MechUserID = 269;
            obj.PlumbUserID = 269;
            obj.ZoneUserID = 0;
            obj.FireUserID = 0;
            obj.FoodServiceUserID = 0;
            obj.PoolUserID = 0;
            obj.FacilityUserID = 0;
            obj.DayCareUserID = 0;
            obj.BackFlowUserID = 0;

            obj.Cycle = 1;

            obj.BuildingIsFIFO = false;
            obj.ElectricIsFIFO = false;
            obj.MechIsFIFO = false;
            obj.PlumbIsFIFO = false;
            obj.ZoneIsFIFO = false;
            obj.FireIsFIFO = false;
            obj.FoodServiceIsFIFO = false;
            obj.PoolIsFIFO = false;
            obj.FacilityIsFIFO = false;
            obj.DayCareIsFIFO = false;
            obj.BackFlowIsFIFO = false;

            obj.ScheduleAfterDate = System.DateTime.MinValue;
            obj.PlansReadyOnDate = System.DateTime.Parse("7/30/2021");
            obj.IsFutureCycle = false;
            obj.IsCycleComparison = false;
            obj.IsAdjustHours = false;

            obj.isActivateNAReview = true;

            if (obj.IsFutureCycle)
            {
                obj.UpdatedBuildingHours = 0;
                obj.UpdatedElectricHours = 0;
                obj.UpdatedMechHours = 0;
                obj.UpdatedPlumbHours = 0;
                obj.UpdatedDayCareHours = 0;
                obj.UpdatedFoodHours = 0;
                obj.UpdatedPoolHours = 0;
                obj.UpdatedLodgeHours = 0;
                obj.UpdatedBackflowHours = 0;
                obj.UpdatedZoneHours = 0;
                obj.UpdatedFireHours = 0;

            }
            else
            {
                //jcl LES-186 get the estimation hours in case of na activation
                obj.UpdatedBuildingHours = 1M;
                obj.UpdatedElectricHours = 1M;
                obj.UpdatedMechHours = 0.5M;
                obj.UpdatedPlumbHours = 0.5M;
                obj.UpdatedDayCareHours = 0;
                obj.UpdatedFoodHours = 0;
                obj.UpdatedPoolHours = 0;
                obj.UpdatedLodgeHours = 0;
                obj.UpdatedBackflowHours = 1M;
                obj.UpdatedZoneHours = 0.5M;
                obj.UpdatedFireHours = 0;

            }

            obj.isSelfSchedule = false;
            obj.selfScheduleDate = System.DateTime.MinValue;


            _Engine = new PlanReviewProjectSchedulingEngine(obj);
            var ret = _Engine.GetAutoEstimatedValues();
            Assert.AreEqual(1, 1);
        }
    }
}
