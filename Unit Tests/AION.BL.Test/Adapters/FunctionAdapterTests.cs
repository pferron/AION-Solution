using AION.Accela.Engine.BusinessObjects;
using AION.Accela.Engine.Models;
using AION.AIONDB.Engine.BusinessObjects;
using AION.BL.Adapters;
using AION.BL.Models;
using Meck.Azure;
using Meck.Shared.Accela;
using Meck.Shared.MeckDataMapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace AION.BL.Test.Adapters
{
    [TestClass]
    public class FunctionAdapterTests
    {
        IFunctionAdapter _adapter = new FunctionAdapter();

        [TestInitialize]
        public void SetupAccelaConfiguration()
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
        public void TestMethod1()
        {
            var result = KeyVaultUtility.GetSecret("AccelaUserId");

            AccelaAuthorizationBO mAuth = new AccelaAuthorizationBO();

            AccelaParmsDetailBE mParams = mAuth.LoadupParmsAndAzureKeyVaultData();

            Assert.IsTrue(result != null);

            Console.WriteLine("Config Params Agency: {0}", mParams.Agency);
            Console.WriteLine("Config Params Environment: {0}", mParams.Environment);
            Console.WriteLine("KeyVault AccelaUserID: {0}", mParams.UserName);
            Console.WriteLine("KeyValue Password: {0}", mParams.password);
            Console.WriteLine("KeyValue ClientId: {0}", mParams.ClientId);
            Console.WriteLine("KeyVault ClientSecret: {0}", mParams.ClientSecret);
        }

        [TestMethod]
        [Ignore]
        public void CancelExpressReservations() // TESTED
        {
            bool isSuccess = _adapter.CancelReserveExpressReservation();
            Assert.IsTrue(isSuccess);
        }

        [TestMethod]
        [Ignore]
        public void CancelFacilitatorMeetings()  // TESTED
        {
            bool isSuccess = _adapter.CancelFacilitatorMeetingAppointment();
            Assert.IsTrue(isSuccess);
        }

        [TestMethod]
        [Ignore]
        public void CancelFacilitatorMeetingById()
        {
            bool isSuccess = _adapter.CancelFacilitatorMeetingAppointmentById(1379);
            Assert.IsTrue(isSuccess);
        }

        [TestMethod]
        //[Ignore]
        public void CancelMeetingSavedUserSchedules()  // TESTED
        {
            bool isSuccess = _adapter.CancelMeetingSavedUserSchedules();
            Assert.IsTrue(isSuccess);
        }

        [TestMethod]
        [Ignore]
        public void CancelPrelimMeetings()  // TESTED
        {
            bool isSuccess = _adapter.CancelPrelimMeeting();
            Assert.IsTrue(isSuccess);
        }

        [TestMethod]
        [Ignore]
        public void CancelScheduledExpressPlanReview() // TESTED
        {
            bool isSuccess = _adapter.CancelScheduledExpressPlanReview();
            Assert.IsTrue(isSuccess);
        }

        [TestMethod]
        [Ignore]
        public void TestProcessCalendarEventQueueRecords()
        {
            _adapter.ProcessCalendarEventQueueRecords(false, "TEST");
            Assert.IsTrue(1 == 1);
        }

        [TestMethod]
        [Ignore]
        public void AddEligibleUsersToExistingNPAsSuccess()
        {
            bool isSuccess = _adapter.AddEligibleUsersToExistingNPAs(true);
            Assert.IsTrue(isSuccess);
        }

        //[TestMethod]
        public void TestProjectSync()
        {
            bool isSuccess = new FunctionAdapter().SyncAccelaAION();
            Assert.IsTrue(isSuccess);
        }

        //[TestMethod]
        public void TestMeetingSync()
        {
            bool isSuccess = new FunctionAdapter().SyncAccelaAION();
            Assert.IsTrue(isSuccess);
        }

        //[TestMethod]
        public void TestMeetingModelCreationDuringSync()
        {
            FunctionAdapter adapter = new FunctionAdapter();
            AIONQueueRecordBE be = new AIONInsertDataBO().TaskGetSpecificAIONQueueRecord(1778);
            AccelaAIONMeetingModel meetingModel = adapter.ProcessMeetingModel(be);

            Assert.IsNotNull(meetingModel);
        }

        //[TestMethod]
        public void TestGetMeetingRecordsToProcessDuringSync()
        {
            FunctionAdapter adapter = new FunctionAdapter();
            List<AIONQueueRecordBE> queueRecords = new List<AIONQueueRecordBE>();
            queueRecords.Add(new AIONInsertDataBO().TaskGetSpecificAIONQueueRecord(1736));
            List<AIONQueueRecordBE> meetingRecords = adapter.GetMeetingRecordsToProcess(queueRecords);

            Assert.AreEqual(1, 1);
        }

        //[TestMethod]
        public void TestFifoOptimization()
        {
            bool success;

            FunctionAdapter adapter = new FunctionAdapter();
            List<int> projectIds = adapter.GetFIFOProjectIdsToBeOptimized();

            foreach (int projectId in projectIds)
            {
                success = adapter.OptimizeFIFOProject(projectId);
            }

            Assert.AreEqual(1, 1);
        }

        //[TestMethod]
        //public void ValidateSyncAccelaAIONTest()
        //{
        //    ProjectParms parms = new ProjectParms
        //    {
        //        ProjectId = "REC23-00000-00063",
        //        RecIdTxt = "REC23-00000-00063"
        //    };
        //    FunctionAdapter adapter = new FunctionAdapter();
        //    var accelaprojectmodel = new EstimationAccelaAdapter().GetProjectDetailsLoad(parms);
        //    var result = JsonConvert.SerializeObject(accelaprojectmodel);
        //    var projectestimation = new EstimationCRUDAdapter().GetProjectDetailsForEstimation(accelaprojectmodel);
        //    var y = adapter.ValidateSyncAccelaAION(projectestimation);


        //    Assert.IsTrue(y);

        //}


    }
}
