using AccelaEngineTests.UnitTestData;
using AION.Accela.Engine;
using AION.Accela.WebApi;
using AION.Accela.WebApi.Controllers;
using AION.AIONDB.Engine;
using AION.AIONDB.Engine.BusinessObjects;
using Meck.Azure;
using Meck.Shared.Accela;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows.Forms;
using AION.Accela.Engine.BusinessObjects;
using Newtonsoft.Json;

// using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace AccelaEngineTests
{

    [TestClass]
    public class AccelaApiTests : System.Web.HttpApplication
    {
        private TestData mUTData = new TestData();

        //  data In Unit Test Build
        public List<PlanReviewHistory> mPlanRviewHistoryTest = new List<PlanReviewHistory>();
        public List<RecordNotification> mPrepRecordNotificationTest = new List<RecordNotification>();

        [TestInitialize]
         [Ignore]
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

        [TestMethod]
       [Ignore]
        public void AccelaWebApiPingTest()
        {
            string mIHttpActionResult = string.Empty;

            //  CancellationToken CanToken = new CancellationToken();

            DateTime moqresult = DateTime.Now;

            PingController mPingController = new PingController();

            var result = mPingController.PingTest();

            Assert.IsFalse(result == null);

            Console.WriteLine("Test Completed");

        }



        [TestMethod]
        [Ignore]
        public void InsertNewRecordTest()
        {


            IAIONDBEngine mAIONDBEngine = new AIONEngineCrudApiBO();

            foreach (var mRecNotify in mUTData.mRecordNotificationTest)
            {
                //  var recjson = JsonConvert.SerializeObject(mRecNotify);
                var result = mAIONDBEngine.InsertNewAIONRecord(mRecNotify);

                var newRecResult = result;

                Console.WriteLine(JsonConvert.SerializeObject(newRecResult));

                //  AIONRecordQueueResponse newRecordDetail = JsonConvert.DeserializeObject<AIONRecordQueueResponse>(newRecResult);

                Assert.IsTrue(newRecResult.errors.Length == 0);
            }

            mUTData.ClearDBTable(true, false);
        }

        [TestMethod]
        [Ignore]
        public void InsertNewRecordFailTest()
        {
            IAIONDBEngine mAIONDBEngine = new AIONEngineCrudApiBO();

            RecordNotification mRecordNortification = new RecordNotification();

            //   mRecordNortification.EstimatedRereviewHours = null;
            mRecordNortification.WorkFlowStepId = string.Empty;
            mRecordNortification.WorkFlowTaskName = "Similuated send to Aion";
            mRecordNortification.WorkFlowStatus = "SendToAION";
            mRecordNortification.recordID = "Unit_Test-00" + 000 + "-birds".Length;
            mRecordNortification.recordtype = "";
            mRecordNortification.status = "";
            mRecordNortification.statusdescription = "should be replaced";

            var result = mAIONDBEngine.InsertNewAIONRecord(mRecordNortification);
            var newRecResult = result;

            Console.WriteLine(newRecResult);

            Assert.IsTrue(newRecResult.errors.Length == 0);

        }


        [TestMethod]
        [Ignore]
        public void InsertNewPlanReviewHistoryTest()
        {
            TestData mUTData = new TestData();

            IAIONDBEngine mAIONDBEngine = new AIONEngineCrudApiBO();

            foreach (PlanReviewHistory mRecNotify in mUTData.mPlaneReviewHistoryTest)
            {
                //  var recjson = JsonConvert.SerializeObject(mRecNotify);
                var resulttask = mAIONDBEngine.InsertNewAIONPlanReviewHistoryRecord(mRecNotify);
                var mAIONPlanHistoryResponse = resulttask;

                Console.WriteLine(JsonConvert.SerializeObject(mAIONPlanHistoryResponse));

                Assert.IsTrue(mAIONPlanHistoryResponse.errors == string.Empty);
            }

            mUTData.ClearDBTable(false, true, false);
        }


        // AE DataInsert
        // then  Select
        // and Delete  by record Id
        /// <summary>
        /// 
        /// </summary>

        [TestMethod]
        [Ignore]
        public void InsertAEDataTest()
        {
            IAIONDBEngine mAIONDBEngine = new AIONEngineCrudApiBO();

            AccelaAIONAEData _mAccelaAIONAEData = new AccelaAIONAEData()
            {
                ACCELA_AE_DATA_ID = 0,
                SYSTEM_USER_NM = "UnitTestUser",
                REC_ID_NUM = "Unit_Test_1",
                PLAN_REVIEW_TYP_DESC = "Unit TestDetail Type",
                CYCLE_NBR = 1,
                LICENSE_TYP_DESC = "Test License Desc",
                PROJECT_SCORE_DESC = "Project score",
                PROJECT_CREATED_DTTM = DateTime.Now,
                PASS_FAIL_IND = true,
                FAILURE_CAUSE_TXT = " test Fail;ure cause",
                FAILURE_REASON_TXT = " Test Failure Reason",
                WKR_ID_CREATED_TXT = "UnitTesTuser",
                CREATED_DTTM = DateTime.Now,
                WKR_ID_UPDATED_TXT = "iupdateuser",
                UPDATED_DTTM = DateTime.Now
            };
            var result = mAIONDBEngine.InsertNewAEData(_mAccelaAIONAEData);
            // result.Wait();

            var response = result;

            Console.WriteLine(JsonConvert.SerializeObject(response));

            Assert.IsTrue(response.errors == string.Empty);




            //var getResult = mAIONDBEngine.GetAEDataByRecordId(_mAccelaAIONAEData.REC_ID_NUM);
            //getResult.Wait();

            //var getResponse = getResult.Result;

            // Assert.IsTrue(result.Result );


            var deleteResult =
                Task.Run(() => mAIONDBEngine.DeleteAERecordsByRecordId(response.CurrentRecord.REC_ID_NUM));
            deleteResult.Wait();

            var deleteResponse = deleteResult.Result;

            Assert.IsTrue(deleteResponse);

            mUTData.ClearDBTable(false, false, true);

        }

      

    }
    
}
