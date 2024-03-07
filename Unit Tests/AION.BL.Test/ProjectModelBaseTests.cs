using AION.BL.BusinessObjects;
using AION.Manager.AccelaBusinessObjects;
using Meck.Shared.MeckDataMapping;
using Meck.Shared.PosseToAccela;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AION.BL.Test
{
    [TestClass]
    public class ProjectModelBaseTests
    {
        private AccelaProjectModel _AccelaProjectModel;

        [TestInitialize]
        public void TestInitialize()
        {
            _AccelaProjectModel = new AccelaProjectModel();

            AccelaProjectDisplayInfo accelaProjectDisplayInfo = new AccelaProjectDisplayInfo();
            accelaProjectDisplayInfo.ProjectName = "test";
            accelaProjectDisplayInfo.ProjectAddress = "test";
            accelaProjectDisplayInfo.ProjectNumber = "test";
            accelaProjectDisplayInfo.ProjectName = "test";
            accelaProjectDisplayInfo.RecordId = "test";
            accelaProjectDisplayInfo.BuildingCodeVersion = "test";
            accelaProjectDisplayInfo.TypeOfWork = "test";
            accelaProjectDisplayInfo.TypeOfConstruction = "test";
            accelaProjectDisplayInfo.Occupancy = "test";
            accelaProjectDisplayInfo.PrimaryOccupancy = "test";
            accelaProjectDisplayInfo.SecondaryOccupancy = "test";
            accelaProjectDisplayInfo.SquareFootage = "test";
            accelaProjectDisplayInfo.NumofSheets = "test";
            accelaProjectDisplayInfo.SealHolders = "test";
            accelaProjectDisplayInfo.Designers = "test";
            accelaProjectDisplayInfo.FloorList = new List<string>() { "test" };
            accelaProjectDisplayInfo.ScopeOfWorkOverall = "test";
            accelaProjectDisplayInfo.ScopeOfWorkMechanical = "test";
            accelaProjectDisplayInfo.ScopeOfWorkElectrical = "test";
            accelaProjectDisplayInfo.ScopeOfWorkCivil = "test";
            accelaProjectDisplayInfo.ZoningOfSite = "test";
            accelaProjectDisplayInfo.ChangeOfUse = "test";
            accelaProjectDisplayInfo.IsConditionalPermitApproval = "test";
            accelaProjectDisplayInfo.PreviousBusinessType = "test";
            accelaProjectDisplayInfo.CityOfC = "test";
            accelaProjectDisplayInfo.ProposedBusinessType = "test";
            accelaProjectDisplayInfo.CodeSummary = "test";
            accelaProjectDisplayInfo.Contacts = new List<contactDetail>() { new contactDetail() { FirstName = "test" } };
            accelaProjectDisplayInfo.BackflowApplictnDet = "test";
            accelaProjectDisplayInfo.WaterSewerDetails = "test";
            accelaProjectDisplayInfo.HealthDeptDetails = "test";
            accelaProjectDisplayInfo.DayCare = "test";
            accelaProjectDisplayInfo.ProposedOutdoorUndergroundPiping = "test";
            accelaProjectDisplayInfo.ProposedFireSprinklerPiping = "test";
            accelaProjectDisplayInfo.IsInstallingCMUDBackflowPreventer = "test";
            accelaProjectDisplayInfo.ExtendingPublicWaterSewer = "test";
            accelaProjectDisplayInfo.GradeModificationWaterSewerEasement = "test";
            accelaProjectDisplayInfo.ProposedEncroachmentWaterSewerEasement = "test";
            accelaProjectDisplayInfo.ParcelNumber = "test";
            accelaProjectDisplayInfo.IsAffordableHousing = "test";
            accelaProjectDisplayInfo.ExactAddress = "test";
            accelaProjectDisplayInfo.DeliveryMethod = "test";
            accelaProjectDisplayInfo.IsBIM = "test";
            accelaProjectDisplayInfo.BIMDesignDiscipline = "test";
            accelaProjectDisplayInfo.NumOfAttendees = "test";
            accelaProjectDisplayInfo.PreviousPreliminaryReview = "test";
            accelaProjectDisplayInfo.ProjectNumberPrevPrelimReview = "test";
            accelaProjectDisplayInfo.IsSameReviewTeam = "test";
            accelaProjectDisplayInfo.PropertyOwnerName = "test";
            accelaProjectDisplayInfo.PropertyOwnerAddress = "test";
            accelaProjectDisplayInfo.PropertyOwnerEmail = "test";
            accelaProjectDisplayInfo.PropertyOwnerPhone = "test";
            accelaProjectDisplayInfo.PropertyOwnerAutoEmail = "test";
            accelaProjectDisplayInfo.PropertyManagerName = "test";
            accelaProjectDisplayInfo.PropertyManagerPhone = "test";
            accelaProjectDisplayInfo.PropertyManagerEmail = "test";
            accelaProjectDisplayInfo.PropertyManagerEmail2 = "test";
            accelaProjectDisplayInfo.ArchDesContactName = "test";
            accelaProjectDisplayInfo.ArchDesContactPhone = "test";
            accelaProjectDisplayInfo.PropertyOwnerPhone = "test";
            accelaProjectDisplayInfo.PropertyOwnerPhone = "test";
            accelaProjectDisplayInfo.ArchDesContactEmail = "test";
            accelaProjectDisplayInfo.ArchDesAutoEmail = "test";
            accelaProjectDisplayInfo.IsArchDrawingsSealed = "test";
            accelaProjectDisplayInfo.ArchDesLicenseNum = "test";
            accelaProjectDisplayInfo.ArchDesLicenseBoard = "test";
            accelaProjectDisplayInfo.IsArchDesEmployee = "test";
            accelaProjectDisplayInfo.PermitNumber = "test";
            accelaProjectDisplayInfo.TotalFee = 0;
            accelaProjectDisplayInfo.GateDate = "test";

            _AccelaProjectModel.DisplayOnlyInformation = accelaProjectDisplayInfo;
        }

        [TestMethod]
        public void TestGenerateDisplayOnlyInformation()
        {
            ProjectEstimationModelBO bo = new ProjectEstimationModelBO();
            AccelaProjectDisplayInfo actual = new AccelaProjectDisplayInfoBO().GenerateDisplayOnlyInformation(_AccelaProjectModel);

            string expectedTypeOfWork = "test";
            string expectedPermitNumber = "test";
            string expectedScopeOfWorkOverall = "test";

            Assert.AreEqual(expectedTypeOfWork, actual.TypeOfWork);
            Assert.AreEqual(expectedPermitNumber, actual.PermitNumber);
            Assert.AreEqual(expectedScopeOfWorkOverall, actual.ScopeOfWorkOverall);
        }

        [TestMethod]
        public void SetProjectLevelReturnsExpectedValueWhenCriteriaIsNull()
        {
            ProjectEstimationModelBO bo = new ProjectEstimationModelBO();
            PropertyTypeEnums propertyType = PropertyTypeEnums.Commercial;
            int? sqrFootage = null;
            bool? isHighRise = null;
            int? numberOfStories = null;
            string occupancyType = "Business";

            string expectedLevel = "3";

            string actualLevel = bo.SetProjectLevel(propertyType, sqrFootage, isHighRise, numberOfStories, occupancyType);
            Assert.AreEqual(expectedLevel, actualLevel);
        }
    }
}
