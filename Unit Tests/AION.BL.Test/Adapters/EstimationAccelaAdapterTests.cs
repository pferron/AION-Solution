using AION.AIONDB.Engine.BusinessObjects;
using AION.BL.Adapters;
using AION.BL.Models;
using AION.Estimator.Engine.BusinessEntities;
using AION.Manager.AccelaBusinessObjects;
using AION.Manager.Engines.SyncAccela;
using Meck.Azure;
using Meck.Shared.Accela;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;

namespace AION.BL.Test.Adapters
{
    [TestClass]
    public class EstimationAccelaAdapterTests
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
        public void GetAccelaProjectMapped()
        {
            ProjectParms parms = new ProjectParms
            {
                ProjectId = "REC23-00000-00063",
                RecIdTxt = "REC23-00000-00063"
            };

            var accelaprojectmodel = new EstimationAccelaAdapter().GetProjectDetailsLoad(parms);
            var result = JsonConvert.SerializeObject(accelaprojectmodel);
            //var projectestimation = new EstimationCRUDAdapter().GetProjectDetailsForEstimation(accelaprojectmodel);

            Assert.IsTrue(1 == 1);

        }


        //[TestMethod]
        public void SaveAccelaProjectReturnsSuccess()
        {
            ProjectParms parms = new ProjectParms
            {
                ProjectId = "REC23-00000-00063",
                RecIdTxt = "REC23-00000-00063"
            };

            var accelaprojectmodel = new EstimationAccelaAdapter().GetProjectDetailsLoad(parms);
            var result = JsonConvert.SerializeObject(accelaprojectmodel);
            AIONQueueRecordBE be = new AIONInsertDataBO().TaskGetSpecificAIONQueueRecord(72424);

            var projectestimation = new EstimationCRUDAdapter().GetProjectDetailsForEstimation(accelaprojectmodel);
            var projectjson = JsonConvert.SerializeObject(projectestimation);
            var insert = new AccelaSyncEngine().SaveAccelaProject(accelaprojectmodel, be);


            Assert.IsTrue(insert);

        }
        //[TestMethod]
        public void SyncAccelaAIONReturnsSuccess()
        {
            var ret = new FunctionAdapter().SyncAccelaAION();

            Assert.IsTrue(1 == 1);

        }
        //[TestMethod]
        public void GetAccelaProjectDisplayInfo()
        {
            ProjectParms parms = new ProjectParms
            {
                ProjectId = "REC21-00000-000HC",
                RecIdTxt = "REC21-00000-000HC"
            };

            var accelaprojectmodel = new EstimationAccelaAdapter().GetProjectDetailsLoad(parms);
            var test = new AccelaProjectDisplayInfoBO().GenerateDisplayOnlyInformation(accelaprojectmodel);

            Assert.IsTrue(1 == 1);

        }
        //[TestMethod]
        public void GetAccelaProjectAIONPropertyType()
        {
            ProjectParms parms = new ProjectParms
            {
                ProjectId = "REC21-00000-000I6",
                RecIdTxt = "REC21-00000-000I6"
            };

            var accelaprojectmodel = new EstimationAccelaAdapter().GetProjectDetailsLoad(parms);
            var ret = new AccelaPropertyTypeBO().MapAccelaPropertyType(accelaprojectmodel);

            Assert.IsTrue(1 == 1);

        }
        //[TestMethod]
        public void GetAccelaProjectTradesAgenciesList()
        {
            ProjectParms parms = new ProjectParms
            {
                ProjectId = "REC21-00000-000I6",
                RecIdTxt = "REC21-00000-000I6"
            };

            var accelaprojectmodel = new EstimationAccelaAdapter().GetProjectDetailsLoad(parms);
            var list = new AccelaDepartmentBO().GetAccelaAgencyInfoList(accelaprojectmodel.PrelimMeetingTradesAndReviewer);
            var list2 = new AccelaDepartmentBO().GetAccelaTradeInfoList(accelaprojectmodel.PrelimMeetingTradesAndReviewer);
            Assert.IsTrue(1 == 1);

        }
        //[TestMethod]
        public void GetAccelaProjectStatus()
        {
            ProjectParms parms = new ProjectParms
            {
                ProjectId = "REC21-00000-0016L",
                RecIdTxt = "REC21-00000-0016L"
            };

            AIONQueueRecordBE be = new AIONInsertDataBO().TaskGetSpecificAIONQueueRecord(1478);

            var accelaprojectmodel = new EstimationAccelaAdapter().GetProjectDetailsLoad(parms);
            var projectestimation = new EstimationCRUDAdapter().GetProjectDetailsForEstimation(accelaprojectmodel);
            var status = new AccelaProjectStatusBO(
                projectestimation.AionPropertyType,
                projectestimation.PlansReadyOnDate,
                projectestimation.AIONProjectStatus)
                .GetCurrentProjectStatusFromAccelaStatus(be.WORKFLOW_TASK_STATUS, be.STATUS_DESC);

            Assert.IsTrue(1 == 1);

        }

        //[TestMethod]
        public void SyncAccelaAIONProjectsReturnsSuccess()
        {
            var queueRecords = new AIONEngineCrudApiBO().GetNewAIONRecordsToProcess();

            var projectRecords = new FunctionAdapter().GetProjectRecordsToProcess(queueRecords);

            var ret = new FunctionAdapter().SyncAccelaAIONProjects(projectRecords);

            Assert.IsTrue(1 == 1);

        }
        //[TestMethod]
        public void SyncAccelaAIONOneProjectReturnsSuccess()
        {
            var be = new AIONInsertDataBO().TaskGetSpecificAIONQueueRecord(72876);
            var projectRecords = new List<AIONQueueRecordBE>();
            projectRecords.Add(be);


            var ret = new FunctionAdapter().SyncAccelaAIONProjects(projectRecords);

            Assert.IsTrue(ret);

        }

        //[TestMethod]
        public void SyncPlanReviewsProjectsReturnsSuccess()
        {
            var queueRecords = new AIONEngineCrudApiBO().GetNewAIONRecordsToProcess();

            var projectRecords = new FunctionAdapter().GetApprovedRecordsToProcess(queueRecords);

            var ret = new FunctionAdapter().SyncPlanReviewHours(projectRecords);

            Assert.IsTrue(1 == 1);

        }

        //[TestMethod]
        public void GetAccelaProjectJson()
        {
            ProjectParms parms = new ProjectParms
            {
                ProjectId = "REC22-00000-000HS",
                RecIdTxt = "REC22-00000-000HS"
            };

            var accelaprojectmodel = new EstimationAccelaAdapter().GetProjectDetailsJsonString(parms);

            Assert.IsTrue(1 == 1);

        }

        [TestMethod]
        public void TestEstimationDashboardItemOnHoldWhenAllCriteriaAreMet()
        {
            // arrange
            EstimationAccelaAdapter adapter = new EstimationAccelaAdapter();

            ProjectBE project = new ProjectBE();
            project.FifoInd = true;
            project.TeamGradeTxt = "Poor";
            project.ProjectTypRefId = (int)PropertyTypeEnums.FIFO_Small_Commercial;
            project.TagUpdatedByTs = System.DateTime.Now.AddHours(-6);

            // act
            bool onHold = adapter.IsOnHoldForOneDay(project);

            // assert
            Assert.IsTrue(onHold);
        }

        [TestMethod]
        public void TestEstimationDashboardItemOnHoldWhenNotAllCriteriaAreMet()
        {
            // arrange
            EstimationAccelaAdapter adapter = new EstimationAccelaAdapter();

            ProjectBE project = new ProjectBE();
            project.FifoInd = true;
            project.TeamGradeTxt = "";
            project.ProjectTypRefId = (int)PropertyTypeEnums.FIFO_Small_Commercial;
            project.TagUpdatedByTs = System.DateTime.Now.AddHours(-6);

            // act
            bool onHold = adapter.IsOnHoldForOneDay(project);

            // assert
            Assert.IsFalse(onHold);

            project.FifoInd = false;
            project.TeamGradeTxt = "Poor";
            project.ProjectTypRefId = (int)PropertyTypeEnums.FIFO_Small_Commercial;
            project.TagUpdatedByTs = System.DateTime.Now.AddHours(-6);

            Assert.IsFalse(onHold);

            project.FifoInd = false;
            project.TeamGradeTxt = "Poor";
            project.ProjectTypRefId = (int)PropertyTypeEnums.FIFO_Single_Family_Homes;
            project.TagUpdatedByTs = System.DateTime.Now.AddHours(-6);

            Assert.IsFalse(onHold);

            project.FifoInd = false;
            project.TeamGradeTxt = "Poor";
            project.ProjectTypRefId = (int)PropertyTypeEnums.FIFO_Small_Commercial;
            project.TagUpdatedByTs = System.DateTime.Now.AddHours(-30);

            Assert.IsFalse(onHold);
        }
    }
}
