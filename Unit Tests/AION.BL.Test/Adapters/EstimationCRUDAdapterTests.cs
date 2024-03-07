using AION.BL.Adapters;
using AION.BL.Models;
using Meck.Shared;
using Meck.Shared.MeckDataMapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace AION.BL.Test.Adapters
{
    [TestClass()]
    public class EstimationCRUDAdapterTests
    {

        private Mock<EstimationCRUDAdapter> _estimationcrud;

        [TestInitialize]
        public void TestInitialize()
        {

            _estimationcrud = new Mock<EstimationCRUDAdapter>();
        }
        [Ignore]
        [TestMethod()]
        public void SaveProjectAutoAssignmentDetailsTest()
        {
            //if (AION.BL.Test.Global.FreezeTesting == true) return;
            //Meck.Shared.AccelaProjectModel ret1 = new Meck.Shared.AccelaProjectModel();
            //ret1.ProjectIDRef = "00000345";
            //ret1.PropertyTypeRef = PropertyTypeExternalRef.Mega_Multi_Family;
            //ret1.ProjectTradesList = new List<Meck.Shared.TradeInfo>();
            //ret1.ProjectAgencyList = new List<Meck.Shared.AgencyInfo>();
            //ret1.ReviewTypeRef = ReviewTypeEnumExternalRef.Express;
            //ret1.CostOfConstruction = 10.0;
            //ret1.NumberofSheets = 12;
            //ret1.OccupancyType = "R1";
            //ret1.ConstructionType = "upfitrtap";

            //ret1.ProjectTradesList.Add(new TradeInfo
            //{
            //    AccelaDepartmentDivisionRef = "Building",
            //    AccelaDepartmentRegionRef = null
            //});
            //ret1.ProjectTradesList.Add(new TradeInfo
            //{
            //    AccelaDepartmentDivisionRef = "Electrical",
            //    AccelaDepartmentRegionRef = null
            //});
            //ret1.ProjectAgencyList.Add(new AgencyInfo
            //{
            //    AccelaDepartmentDivisionRef = "Zoning",
            //    AccelaDepartmentRegionRef = "Davidson"

            //});
            //ret1.ProjectStatusCodeRef = "Auto_Estimation_Pending";
            //ret1.DisplayOnlyInformation = new AccelaProjectDisplayInfo();
            //ProjectEstimation aionProjectModel = _estimationcrud.Object.GetProjectDetailsForEstimation(ret1);
            //aionProjectModel.ProjectName = "unit test 1";
            //aionProjectModel.AccelaProjectCreatedDate = DateTime.Today;
            //aionProjectModel.AccelaProjectLastUpdatedDate = DateTime.Today;
            //aionProjectModel.CreatedUser = new UserIdentity() { ID = 1 };
            //aionProjectModel.UpdatedUser = new UserIdentity() { ID = 1 };

            ////change the estimated hours
            //aionProjectModel.Trades[0].EstimationHours = 7.0M;
            //aionProjectModel.Trades[1].EstimationHours = 5.0M;
            //aionProjectModel.PMEmail = "SaveProjectAutoAssignmentDetails@accela.com";
            //aionProjectModel.PMName = "SaveProjectAutoAssignmentDetails Test";
            //aionProjectModel.PMPhone = "Test";

            //aionProjectModel.AIONProjectStatus = new ProjectStatus();
            //aionProjectModel.AIONProjectStatus.ProjectStatusEnum = ProjectStatusEnum.Auto_Estimation_Pending;

            //bool ret4 = _estimationcrud.Object.SaveProjectEstimationDetails(aionProjectModel);

            //Assert.IsTrue(ret4);

            //Assert.IsNotNull(aionProjectModel);

        }
        [Ignore]
        [TestMethod()]
        public void CreateNewProject()
        {
            //if (AION.BL.Test.Global.FreezeTesting == true) return;
            //Meck.Shared.AccelaProjectModel ret1 = new Meck.Shared.AccelaProjectModel();
            //ret1.ProjectIDRef = "0000077666NO";
            //ret1.PropertyTypeRef = PropertyTypeExternalRef.FIFO_Small_Commercial;
            //ret1.ProjectTradesList = new List<Meck.Shared.TradeInfo>();
            //ret1.ProjectAgencyList = new List<Meck.Shared.AgencyInfo>();
            //ret1.ReviewTypeRef = ReviewTypeEnumExternalRef.Express;
            //ret1.CostOfConstruction = 100000.0;
            //ret1.NumberofSheets = 12;
            //ret1.OccupancyType = "R1";
            //ret1.SquareFootageToBeReviewed = 1200;
            //ret1.SquareFootageOfOverallBuilding = 1200;
            //ret1.ConstructionType = "upfitrtap";
            //ret1.ProjectRegionExternalRef = "City Of Charlotte";
            //ret1.ProjectStatusCodeRef = "Auto Estimation Pending";
            //ret1.DisplayOnlyInformation = new Meck.Shared.AccelaProjectDisplayInfo();
            //ProjectEstimation aionProjectModel = _estimationcrud.Object.GetProjectDetailsForEstimation(ret1);
            //aionProjectModel.ProjectName = "Jeanine SCFIFO No city of charlotte zoning";
            //aionProjectModel.AccelaProjectCreatedDate = DateTime.Today;
            //aionProjectModel.AccelaProjectLastUpdatedDate = DateTime.Today;
            //aionProjectModel.CreatedUser = new UserIdentity() { ID = 1 };
            //aionProjectModel.UpdatedUser = new UserIdentity() { ID = 1 };

            //ret1.ProjectManagerName = "project manager name";
            //ret1.projectManagerPhone = "project manager phone";
            //ret1.projectManagerEmail = "ProjectManagerTest@accela.com";
            //bool ret4 = _estimationcrud.Object.SaveProjectEstimationDetails(aionProjectModel);

            //Assert.IsTrue(ret4);
        }
    }
}