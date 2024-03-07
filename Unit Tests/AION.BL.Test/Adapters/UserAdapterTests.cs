using AION.BL.Adapters;
using AION.BL.Models;
using Meck.Azure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace AION.BL.Test.Adapters
{
    [TestClass()]
    public class UserAdapterTests
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

        [TestMethod()]
        public void GetUserIdentityByIDTest()
        {
            int id = 65;
            UserAdapter ad = new UserAdapter();
            var i = ad.GetUserIdentityByID(id);
            Assert.IsTrue(i.ID > 0);
        }

        [TestMethod]
        public void GetAllReviewersReturnsObj()
        {
            UserAdapter adapter = new UserAdapter();
            var items = adapter.GetAllReviewers();
            Assert.IsNotNull(items);
        }

        [TestMethod]
        public void GetAllReviewersExpressSchedulableReturnsObj()
        {
            UserAdapter adapter = new UserAdapter();
            var items = adapter.GetAllReviewers(isExpressSched: true);
            Assert.IsNotNull(items);
        }

        //[TestMethod]
        public void GetAllReviewersIsSchedulableReturnsObj()
        {
            UserAdapter adapter = new UserAdapter();
            var items = adapter.GetAllReviewers(IsSchedulable: true);
            Assert.IsNotNull(items);
        }

        [TestMethod]
        public void GetAllEstimatorsReturnsObj()
        {
            UserAdapter adapter = new UserAdapter();
            var items = adapter.GetAllEstimators();
            Assert.IsNotNull(items);
        }
        [TestMethod]
        public void GetAllFacilitatorsReturnsObj()
        {
            UserAdapter adapter = new UserAdapter();
            var items = adapter.GetAllFacilitators();
            Assert.IsNotNull(items);
        }

        //[TestMethod]
        public void UpsertProjectManagerReturnsInt()
        {

            var project = new ProjectEstimation
            {
                PMEmail = "jeanine.lindsayjeanine@testingprojectmanager.com",
                PMName = "JeanineJeanine LindsayLindsay",
                PMFirstName = "JeanineJeanine",
                PMLastName = "LindsayLindsay",
                PMPhone = "1234567890"
            };

            var returnid = new UserAdapter().UpsertProjectManager(project);

            Assert.IsTrue(returnid > 0);

        }
        //[TestMethod]
        //public void SaveUserJurisdictionSuccess()
        //{
        //    IUserAdapter adapter = new UserAdapter();
        //    List<string> csv = new List<string>();
        //    csv.Add("1");
        //    csv.Add("8");
        //    var success = adapter.SaveUserJurisdiction(
        //        new Manager.Models.User.UserJurisdictionSaveModel
        //        {
        //            UserId = 112,
        //            JurisdictionList = csv,
        //            WrkId = "112"
        //        });

        //    Assert.IsTrue(success);
        //}

        //[TestMethod]
        //public void GetJurisdictionListByUserSuccess()
        //{
        //    IUserAdapter adapter = new UserAdapter();
        //    var items = adapter.GetJurisdictionListByUser(1);
        //    Assert.IsTrue(1 == 1);
        //}

        //[TestMethod]
        //public void GetUsersByPropertyTypeBusRefIDSuccess()
        //{
        //    IUserAdapter adapter = new UserAdapter();
        //    var items = adapter.GetReviewers((int)PropertyTypeEnums.NA, (int)DepartmentDivisionEnum.Fire);
        //    Assert.IsNotNull(items);
        //}

        //[TestMethod]
        public void GetAllDepartmentsByUserIdWSOI()
        {
            UserAdapter userAdapter = new UserAdapter();

            var items = userAdapter.GetAllDepartmentsByUserIdWSOI(112);

            Assert.IsNotNull(items);

        }
    }
}