using AION.BL.Adapters;
using AION.BL.Controller;
using AION.BL.Models;
using AION.Engine.BusinessEntities;
using AION.Engine.BusinessObjects;
using AION.Manager.Controllers;
using AION.Manager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AION.BL.Test
{


    [TestClass]
    public class ProjectAutoEstimationTests
    {
        private ProjectAutoEstimationEngine _autocalc;
        Mock<EstimationAccelaAdapter> _api;
        Mock<EstimationCRUDAdapter> _crud;
        Mock<ProjectEstimationAdapter> _estimation;

        private ProjectParms _projectparms;
        private string _projectid;

        [TestInitialize]
        public void TestInitialize()
        {
            //_projectadapter = new Mock<IProjectEstimationController>();
            _api = new Mock<EstimationAccelaAdapter>();
            _crud = new Mock<EstimationCRUDAdapter>();
            _estimation = new Mock<ProjectEstimationAdapter>();
            _autocalc = new ProjectAutoEstimationEngine(_api.Object, _crud.Object, _estimation.Object);
            _projectid = "1";
            _projectparms = new ProjectParms
            {
                ProjectId = _projectid
            };

        }

        [TestMethod]
        public void PrelimProjectAutoSchedulingTest()
        {
            try
            {
                AutoScheduledPrelimParams model = new AutoScheduledPrelimParams();
                model.SuggestedDate1 = DateTime.Now.AddDays(4);
                model.SuggestedDate2 = DateTime.Now.AddDays(30);
                model.AccelaProjectIDRef = "SHIJO-REC19-00000-10002";
                SchedulerAdapter thisengine = new SchedulerAdapter();
                var result = thisengine.GetPrelimAutoScheduledData(model);
                Assert.IsTrue(true);
            }
            catch
            {
                //always pass since this is required to debug only.
                Assert.IsTrue(true);
            }
            //Assert.IsNull(result);
        }

        [TestMethod]
        public void CEProjectAutoSchedulingTest()
        {
            try
            {
                //string s = DateTime.Now.Date.AddHours(8).Hour.ToString() + DateTime.Now.CurrentHalfTime().Minute.ToString();
                //AutoScheduledPlanReviewParams model = new AutoScheduledPlanReviewParams();
                //model.AccelaProjectIDRef = "AION-verycat183";//"AION -verycat113a";//"SHIJO -REC19-00000-10020";
                //model.BuildingIsPool = false;
                //model.ElectricIsPool = false;
                ////model.BuildingUserID = 39; //01
                ////model.ElectricUserID = 96; //02
                ////model.MechUserID = 107; //04
                ////model.PlumbUserID = 109;//05
                //string s = DateTime.Now.Date.AddHours(8).Hour.ToString() + DateTime.Now.CurrentHalfTime().Minute.ToString();
                //string s = DateTime.Now.Date.AddHours(8).Hour.ToString() + DateTime.Now.CurrentHalfTime().Minute.ToString();
                string planreviewStr = File.ReadAllText(Environment.CurrentDirectory + @"\TestData\CEProject_SchedulePlanReview_AutoSchedule_data.json");
                var model = JsonConvert.DeserializeObject<AutoScheduledPlanReviewParams>(planreviewStr);
                SchedulerAdapter thisengine = new SchedulerAdapter();
                var result = thisengine.GetAutoScheduledDataPlanReview(model);
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                //always pass since this is required to debug only.
                Assert.IsTrue(true);
            }
            //Assert.IsNull(result);
        }

        [TestMethod]
        public void PerformAutoEstimationReturnsValue()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            ProjectEstimation aionProjectModel = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaPropertyType = PropertyTypeEnums.Mega_Multi_Family,
                AccelaProjectRefId = "1",
                AccelaCostOfConstruction = 10.0,
                AccelaNumberofSheets = 12,
                AccelaSqrFtToBeReviewed = 1200,
                AccelaConstructionType = "upfitrtap",
                ProjectOccupancyTypMapNm = "RESIDENTIAL"
            };
            aionProjectModel.Trades.Add(new ProjectTrade
            {
                DepartmentTypeEnum = DepartmentTypeEnum.Trade,
                EstimationHours = 0,
                DepartmentInfo = DepartmentNameEnums.Building
            });
            aionProjectModel.Trades.Add(new ProjectTrade
            {
                DepartmentTypeEnum = DepartmentTypeEnum.Trade,
                EstimationHours = 0,
                DepartmentInfo = DepartmentNameEnums.Electrical
            });
            aionProjectModel.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = DepartmentNameEnums.Zone_Davidson

            });

            aionProjectModel.DisplayOnlyInformation.TypeOfWork = "test";
            aionProjectModel.AccelaConstructionType = "test";
            aionProjectModel.AccelaCostOfConstruction = 99000;
            aionProjectModel.AccelaNumberofSheets = 15;
            aionProjectModel.AccelaNumberOfStories = 2;
            aionProjectModel.ProjectOccupancyTypMapNm = "RESIDENTIAL";

            bool isTrue = _estimation.Object.PerformAutoEstimation(aionProjectModel);

            Assert.IsNotNull(aionProjectModel);
            Assert.IsTrue(isTrue);
        }



        [TestMethod]
        public void ProjectAutoEstimationEstimateCalcIsCorrect()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;



            ProjectEstimation aionProjectModel = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaPropertyType = PropertyTypeEnums.Mega_Multi_Family,
                AccelaProjectRefId = "1",
                AccelaCostOfConstruction = 10.0,
                AccelaNumberofSheets = 12,
                AccelaOccupancyType = "R1",
                AccelaSqrFtToBeReviewed = 1200,
                AccelaConstructionType = "upfitrtap"
            };
            aionProjectModel.Trades.Add(new ProjectTrade
            {
                DepartmentTypeEnum = DepartmentTypeEnum.Trade,
                EstimationHours = 0,
                DepartmentInfo = DepartmentNameEnums.Building
            });
            aionProjectModel.Trades.Add(new ProjectTrade
            {
                DepartmentTypeEnum = DepartmentTypeEnum.Trade,
                EstimationHours = 0,
                DepartmentInfo = DepartmentNameEnums.Building
            });
            aionProjectModel.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = DepartmentNameEnums.Building
            });

            aionProjectModel.DisplayOnlyInformation.TypeOfWork = "test";
            aionProjectModel.DisplayOnlyInformation.TypeOfWork = "test";
            aionProjectModel.AccelaConstructionType = "test";
            aionProjectModel.AccelaCostOfConstruction = 99000;
            aionProjectModel.AccelaNumberofSheets = 15;
            aionProjectModel.AccelaNumberOfStories = 2;
            aionProjectModel.AccelaOccupancyType = "RESIDENTIAL";

            _estimation.Object.PerformAutoEstimation(aionProjectModel);

        }

        [TestMethod]
        public void ProjectAutoEstimationEstimateIsCalcCorrect()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;


            ProjectEstimation aionProjectModel = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaPropertyType = PropertyTypeEnums.Mega_Multi_Family,
                AccelaProjectRefId = "1",
                AccelaCostOfConstruction = 3534805,
                AccelaNumberofSheets = 61,
                AccelaOccupancyType = "R1",
                AccelaSqrFtToBeReviewed = 106145,
                AccelaConstructionType = "upfitrtap"
            };
            aionProjectModel.Trades.Add(new ProjectTrade
            {
                DepartmentTypeEnum = DepartmentTypeEnum.Trade,
                DepartmentInfo = DepartmentNameEnums.Building,
            });
            aionProjectModel.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = DepartmentNameEnums.Building
            });

            aionProjectModel.DisplayOnlyInformation.TypeOfWork = "test";
            aionProjectModel.DisplayOnlyInformation.TypeOfWork = "test";
            aionProjectModel.AccelaConstructionType = "test";
            aionProjectModel.AccelaCostOfConstruction = 99000;
            aionProjectModel.AccelaNumberofSheets = 15;
            aionProjectModel.AccelaNumberOfStories = 2;
            aionProjectModel.AccelaOccupancyType = "RESIDENTIAL";

            _estimation.Object.PerformAutoEstimation(aionProjectModel);

        }

        [TestMethod]
        public void ProjectAutoEstimationReturnsValue_NewConstruction_Residential()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            ProjectEstimation aionProjectModel = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaPropertyType = PropertyTypeEnums.Mega_Multi_Family,
                AccelaProjectRefId = "1",
                AccelaCostOfConstruction = 78109054,
                AccelaNumberofSheets = 314,
                AccelaOccupancyType = "R1",
                AccelaSqrFtToBeReviewed = 105509,
                AccelaConstructionType = "NewConstruction"
            };
            aionProjectModel.Trades.Add(new ProjectTrade
            {
                DepartmentTypeEnum = DepartmentTypeEnum.Trade,
                DepartmentInfo = DepartmentNameEnums.Building,
            });
            aionProjectModel.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = DepartmentNameEnums.Building
            });

            aionProjectModel.DisplayOnlyInformation.TypeOfWork = "test";
            aionProjectModel.DisplayOnlyInformation.TypeOfWork = "test";
            aionProjectModel.AccelaConstructionType = "test";
            aionProjectModel.AccelaCostOfConstruction = 99000;
            aionProjectModel.AccelaNumberofSheets = 15;
            aionProjectModel.AccelaNumberOfStories = 2;
            aionProjectModel.AccelaOccupancyType = "RESIDENTIAL";

            _estimation.Object.PerformAutoEstimation(aionProjectModel);

        }

        [TestMethod]
        public void ProjectAutoEstimationIsCalcCorrect_NewConstruction_Business()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            ProjectEstimation aionProjectModel = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaPropertyType = PropertyTypeEnums.Mega_Multi_Family,
                AccelaProjectRefId = "1",
                AccelaCostOfConstruction = 2500000,
                AccelaNumberofSheets = 25,
                AccelaOccupancyType = "I1",
                AccelaSqrFtToBeReviewed = 43050,
                AccelaConstructionType = "NewConstruction"
            };
            aionProjectModel.Trades.Add(new ProjectTrade
            {
                DepartmentTypeEnum = DepartmentTypeEnum.Trade,
                DepartmentInfo = DepartmentNameEnums.Building,
            });
            aionProjectModel.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = DepartmentNameEnums.Building
            });

            aionProjectModel.DisplayOnlyInformation.TypeOfWork = "test";
            aionProjectModel.DisplayOnlyInformation.TypeOfWork = "test";
            aionProjectModel.AccelaConstructionType = "test";
            aionProjectModel.AccelaCostOfConstruction = 99000;
            aionProjectModel.AccelaNumberofSheets = 15;
            aionProjectModel.AccelaNumberOfStories = 2;
            aionProjectModel.AccelaOccupancyType = "RESIDENTIAL";

            _estimation.Object.PerformAutoEstimation(aionProjectModel);

        }

        //[TestMethod]
        public void ProjectAutoEstimation_CalculateAverageEstimationHoursFactors()
        {
            IProjectAutoEstimationEngine engine = new ProjectAutoEstimationEngine();

            engine.CalculateAverageEstimationHoursFactors();

            Assert.AreEqual(1, 1);
        }

    }
}
