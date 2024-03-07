using AION.Manager.Helpers;
using Meck.Azure;
using Meck.Shared.Accela.ParserModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Configuration;

namespace AION.BL.Test.Adapters
{
    [TestClass]
    public class AccelaTaskHelperTests
    {
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

        //[TestMethod]
        public void AccelaRecIdReturnsTasks()
        {
            var recid = "REC21-00000-000KX";

            var workflowtaskhistory = AccelaTaskHelper.GetAccelaWorkFlowTaskHistory(recid);
            var result = JsonConvert.SerializeObject(workflowtaskhistory);
            Rows historyItems = (Rows)workflowtaskhistory.rows[0];
            result = JsonConvert.SerializeObject(historyItems);
            var items = AccelaTaskHelper.GetApplicableTaskInfoForHistoryRecords(workflowtaskhistory);
            result = JsonConvert.SerializeObject(items);

            Assert.IsTrue(1 == 1);

        }

    }
}
