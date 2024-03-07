using AION.AIONDB.Engine.BusinessObjects;
using AION.BL.Adapters;
using AION.BL.BusinessObjects;
using AION.BL.Models;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.Adapters;
using AION.Manager.Engines.SyncAccela;
using Meck.Azure;
using Meck.Shared.Accela;
using Meck.Shared.MeckDataMapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Linq;

namespace AION.BL.Test.Engines
{
    [TestClass]
    public class AccelaSyncEngineTests
    {
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

        //[TestMethod]
        public void TestSaveAccelaProject()
        {
            AccelaSyncEngine engine = new AccelaSyncEngine();

            AIONQueueRecordBE be = new AIONInsertDataBO().TaskGetSpecificAIONQueueRecord(1778);

            AccelaProjectModel project = new EstimationAccelaAdapter()
                .GetProjectDetailsLoad(new ProjectParms { ProjectId = be.REC_ID_NUM, RecIdTxt = be.REC_ID_NUM });

            bool success = engine.SaveAccelaProject(project, be);
        }

        //[TestMethod]
        public void SetProjectToAbortPackage()
        {
            //if (projectStatus == ProjectStatusEnum.Abort_Package)
            //{
            //    projectBO.AbortProject(aionProjectModel);

            //    aionProjectModel.PlansReadyOnDate = null;

            //    //update project cycle plans ready on date to null
            //    PlanReviewAdapter planReviewAdapter = new PlanReviewAdapter();
            //    ProjectCycle projectCycle = planReviewAdapter.GetProjectCyclesByProjectId(aionProjectModel.ID).FirstOrDefault(x => x.CurrentCycleInd == true);
            //    projectCycle.PlansReadyOnDt = null;
            //    planReviewAdapter.UpdateProjectCycle(projectCycle);
            //}
            ProjectEstimation project = new ProjectEstimation
            {
                ID = 7881,
                UpdatedUser = new UserIdentity { ID = 112 },
                AIONProjectStatus = new ProjectStatusModelBO().GetInstance(ProjectStatusEnum.Abort_Package),
                PlansReadyOnDate = null
            };
            ProjectBE projectBE = new ProjectBE
            {
                ProjectId = 7881,
                UpdatedByWkrId = "112",
                ProjectStatusRefId = project.AIONProjectStatus.ID,
                UpdatedDate = System.DateTime.Parse("2022-01-03 17:01:16.860")
            };
            new ProjectEstimationModelBO().AbortProject(project);
            PlanReviewAdapter planReviewAdapter = new PlanReviewAdapter();
            ProjectCycle projectCycle = planReviewAdapter.GetProjectCyclesByProjectId(project.ID).FirstOrDefault(x => x.CurrentCycleInd == true);
            projectCycle.PlansReadyOnDt = null;
            planReviewAdapter.UpdateProjectCycle(projectCycle);
            new ProjectBO().UpdateProjectStatus(projectBE);

        }
    }
}
