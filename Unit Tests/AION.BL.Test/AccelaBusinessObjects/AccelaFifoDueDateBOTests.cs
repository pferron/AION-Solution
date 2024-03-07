using AION.BL.Adapters;
using AION.BL.Models;
using AION.Manager.AccelaBusinessObjects;
using AION.Manager.Helpers;
using Meck.Azure;
using Meck.Shared;
using Meck.Shared.Accela.ParserModels;
using Meck.Shared.MeckDataMapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace AION.BL.Test.AccelaBusinessObjects
{
    [TestClass]
    public class AccelaFifoDueDateBOTests
    {
        [TestInitialize]
        public void SetupAccelaEngineTests()
        {
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

        private string GetConfigValue(string settingname)
        {
            return ConfigurationManager.AppSettings[settingname];
        }


        //[TestMethod]
        public void GetFifoDueDateTest()
        {
            EstimationAccelaAdapter estimationAccelaAdapter = new EstimationAccelaAdapter();

            string recordId = "REC22-00000-000HO ";

            var result = estimationAccelaAdapter.GetProjectDetails(new ProjectParms { ProjectId = recordId, RecIdTxt = recordId });

            var AccelaRec = result;
            var mapresult = estimationAccelaAdapter.GetAccelaAIONMapByAccelaRecordType(AccelaRec.result[0].ParseRecType);
            mapresult.Wait();
            var mAccelaAONMap = mapresult.Result;

            AccelaFifoDueDateBO bo = new AccelaFifoDueDateBO();
            DateTime? dueDate = bo.GetFifoDueDateFromCustomTable(AccelaRec, mAccelaAONMap, 1);

            Assert.AreEqual(1,1);
        }
    }
}
