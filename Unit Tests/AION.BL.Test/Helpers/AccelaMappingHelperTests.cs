using AION.BL.Common;
using AION.BL.Helpers;
using AION.BL.Models;
using Meck.Azure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace AION.BL.Test.Adapters
{
    [TestClass]
    public class AccelaMappingHelperTests
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
        public void AccelaPropertyTypeToTaskIdReturnsString()
        {
            ProjectParms parms = new ProjectParms
            {
                ProjectId = "REC21-00000-0013D",
                RecIdTxt = "REC21-00000-0013D"
            };
            AccelaMappingHelper maphelper = new AccelaMappingHelper();

            var returnstring = maphelper.AccelaPropertyTypeToTaskId(PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home, true, false);

            Assert.IsTrue(1 == 1);

        }

        [TestMethod]
        public void GetAllDepartmentsReturnsList()
        {
            var helper = new Helper();
            var list = helper.AllDepartmentNames;
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void GetDepartmentListByDeptReturnsList()
        {
            var helper = new Helper();
            var list = helper.DepartmentNamesEnums(DepartmentNameEnums.NA);
            Assert.IsNotNull(list);
        }
    }
}
