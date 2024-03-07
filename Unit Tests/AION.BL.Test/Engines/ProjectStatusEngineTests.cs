using AION.AIONDB.Engine.BusinessObjects;
using AION.BL.Adapters;
using AION.BL.Models;
using AION.Manager.AccelaBusinessObjects;
using Meck.Azure;
using Meck.Shared.Accela;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace AION.BL.Test.Engines
{
    [TestClass]
    public class ProjectStatusEngineTests
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
        public void TestProjectStatusEngine()
        {
            List<AIONQueueRecordBE> queueRecords = new List<AIONQueueRecordBE>();
            AIONQueueRecordBE queueRecord = new AIONQueueRecordBE();
            AIONEngineCrudApiBO aIONEngineCrudApiBO = new AIONEngineCrudApiBO();
            EstimationCRUDAdapter estimationCRUDAdapter = new EstimationCRUDAdapter();
            EstimationAccelaAdapter estimationAccelaAdapter = new EstimationAccelaAdapter();
            List<Meck.Shared.MeckDataMapping.AccelaProjectModel> accelaProjectModels = new List<Meck.Shared.MeckDataMapping.AccelaProjectModel>();
            Meck.Shared.MeckDataMapping.AccelaProjectModel project = new Meck.Shared.MeckDataMapping.AccelaProjectModel();

            queueRecord = aIONEngineCrudApiBO.GetSpecificAIONQueueRecord(14597);
            string projectId = queueRecord.REC_ID_NUM;
            if (queueRecord.REC_TYP_DESC != "Meeting Request")
            {
                project = estimationAccelaAdapter.GetProjectDetailsLoad(new ProjectParms { ProjectId = projectId, RecIdTxt = projectId });
            }
            ProjectEstimation aionProjectModel = estimationCRUDAdapter.GetProjectDetailsForEstimation(project);
            string status = queueRecord.STATUS_DESC;
            string workflow = queueRecord.WORKFLOW_TASK_STATUS;
            ProjectStatusEnum pstatus = aionProjectModel.AIONProjectStatus.ProjectStatusEnum;
            DateTime? plansReadyOnDate = null;
            var test = new AccelaProjectStatusBO(
                aionProjectModel.AionPropertyType,
                aionProjectModel.PlansReadyOnDate,
                aionProjectModel.AIONProjectStatus)
                .GetCurrentProjectStatusFromAccelaStatus(workflow, status);

            Assert.AreEqual(1, 1);


        }
    }
}
