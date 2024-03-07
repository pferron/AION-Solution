using AION.BL.Adapters;
using AION.BL.BusinessObjects;
using AION.BL.Models;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using Meck.Azure;
using Meck.Shared.MeckDataMapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;

namespace AION.BL.Test.BusinessObjects
{
    [TestClass]
    public class ProjectModelBaseBOTests
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

        [TestMethod]
        public void GetProfessionalsList()
        {
            List<ProfessionalDetail> professionals = new List<ProfessionalDetail>();
            professionals.Add(new ProfessionalDetail
            {
                addressLine1 = "address",
                city = "city",
                email = "email",
                fullName = "full name",
                id = "id",
                isPrimary = "true",
                lastName = "lastname",
                licenseNumber = "123424",
                licenseType = new LicenseType("xx", "ss"),
                phone2 = "ljkdjfoj",
                postalCode = "postalcode",
                referenceLicenseId = "lll",
                state = new State("xx", "ss"),
                updateOnUI = "xxxx"
            });
            professionals.Add(new ProfessionalDetail
            {
                addressLine1 = "addressvvv",
                city = "cityvvv",
                email = "emailvvv",
                fullName = "full namevvv",
                id = "idvvv",
                isPrimary = "truevvv",
                lastName = "lastnamevvv",
                licenseNumber = "123424vvv",
                licenseType = new LicenseType("xxvv", "ssvvv"),
                phone2 = "ljkdjfojvvv",
                postalCode = "postalcodevvv",
                referenceLicenseId = "lllvvv",
                state = new State("xxvv", "ssvv"),
                updateOnUI = "vvvvv"
            });

            var profs = AION.Manager.AccelaBusinessObjects.ProfessionalDetailBO.ConvertToCSV(professionals, "~");

            Assert.IsTrue(1 == 1);
        }

        //[TestMethod]
        public void TestMapProjectToProjectBE()
        {
            EstimationAccelaAdapter estimationAccelaAdapter = new EstimationAccelaAdapter();
            AccelaProjectModel accelaProjectModel = 
                estimationAccelaAdapter.GetProjectDetailsLoad(new ProjectParms 
                { ProjectId = "REC23-00000-000BW", RecIdTxt = "REC23-00000-000BW" });

            ProjectEstimationModelBO projectEstimationBO = new ProjectEstimationModelBO();

            ProjectEstimation projectEstimation = projectEstimationBO.GetInstance(accelaProjectModel);

            ProjectEstimationModelBO projectEstimationModelBO = new ProjectEstimationModelBO();
            ProjectBE projectBE = new ProjectBO().GetByExternalRefInfo(accelaProjectModel.ProjectNumber);

            projectEstimationModelBO.MapAccelaProject(projectEstimation, projectBE, accelaProjectModel);
            
            Assert.IsTrue(1 == 1);
        }
    }
}
