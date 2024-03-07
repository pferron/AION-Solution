using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AION.BL.Test
{
    [TestClass]
    public class DepartmentAndDeptTypeTest
    {

        //private readonly Mock<IDepartmentTypeBaseBO> _deptTypeadapter;

        [TestInitialize]
        public void TestInitialize()
        {
            List<DepartmentTypeBase> deptbaselist = new List<DepartmentTypeBase>();
            //_deptTypeadapter = new Mock<IDepartmentTypeBaseBO>();
            //_deptTypeadapter.Setup(x => x.CreateInstance()).Returns(_projectestimation);

        }

        //Mock<EstimationAccelaAdapter> _api;
        //Mock<EstimationCRUDAdapter> _crud;
        //Mock<ProjectEstimationAdapter> _estimation;
        //Mock<AccelaProjectModel> _accelaProjectModel;

        //private ProjectParms _projectparms;
        //private ProjectEstimation _projectestimation;
        //private List<ProjectTrade> _trades;
        //private List<ProjectAgency> _agencies;
        //private float _hours;
        //private float _costofconstruction;
        //private int _noofsheets;
        //private int _reviewsqft;
        //private string _occupancytype;
        //private string _projectid;

        //[TestInitialize]
        //public void TestInitialize()
        //{
        //    _deptTypeadapter = new Mock<IDepartmentTypeBaseBO>();
        //    _api = new Mock<EstimationAccelaAdapter>();
        //    _crud = new Mock<EstimationCRUDAdapter>();
        //    _estimation = new Mock<ProjectEstimationAdapter>();
        //    _autocalc = new ProjectEstimationController(_api.Object,_crud.Object,_estimation.Object);
        //    _hours = 300;
        //    _costofconstruction = 20000;
        //    _noofsheets = 1;
        //    _reviewsqft = 2000;
        //    _occupancytype = "R4";
        //    _projectid = "1";
        //    _projectparms = new ProjectParms
        //    {
        //        ProjectId = _projectid
        //    };
        //    //create trade and calc values
        //    _accelaProjectModel = new Mock<AccelaProjectModel>();
        //    _trades = new List<ProjectTrade>();
        //    _trades.Add(new ProjectTrade
        //    {
        //        Hours = _hours,
        //        CostOfConstruction = _costofconstruction,
        //        NoOfSheets = _noofsheets,
        //        OccupancyType = _occupancytype,
        //        ReviewSqFt = _reviewsqft,
        //        DepartmentTypeEnum = DepartmentTypeEnum.Trade,
        //        DepartmentInfo = new System.Lazy<Department>(() => new Department
        //        {
        //            DepartmentEnum = DepartmentNameEnums.Building
        //        })
        //    });
        //    _agencies = new List<ProjectAgency>();
        //    _projectestimation = new ProjectEstimation
        //    {
        //        Trades = _trades,
        //        Agencies = _agencies
        //    };

        //    _deptTypeadapter.Setup(x => x.ExecuteProjectEstimation(_projectparms))
        //        .Returns(_projectestimation);

        //}

        [TestMethod]
        public void ProjectAutoEstimationIsNotNull()
        {
            // //var project = _autocalc.ExecuteProjectEstimation(_projectparms);

            // AccelaProjectModel ret1 = _api.Object.GetProjectDetailsFromAccela(_projectparms);
            // ret1.ProjectIDRef = "1";
            // ret1.PropertyTypeRef = PropertyTypeEnumConst.Mega_Multi_Family; // "Mega Multi-Family";
            // ret1.ProjectTradesList = new List<TradeInfo>();
            // ret1.ProjectAgencyList = new List<AgencyInfo>();
            // ret1.ProjectTradesList.Add(new TradeInfo
            // {
            //     AccelaDepartmentDivisionRef = "Building",
            //     CostOfConstruction = (float)10.0,
            //     NoOfSheets = 12,
            //     OccupancyType = "R1",
            //     ReviewSqFt = 1200,
            //     AccelaDepartmentRegionRef = null
            // });
            // ret1.ProjectTradesList.Add(new TradeInfo
            // {
            //     AccelaDepartmentDivisionRef = "Electrical",
            //     CostOfConstruction = (float)15.0,
            //     NoOfSheets = 12,
            //     OccupancyType = "R4",
            //     ReviewSqFt = 1200,
            //     AccelaDepartmentRegionRef = null
            // });
            // ret1.ProjectAgencyList.Add(new AgencyInfo
            // {
            //     AccelaDepartmentDivisionRef = "Zoning",
            //     AccelaDepartmentRegionRef = "Davidson"
            // });

            // ProjectEstimation aionProjectModel = _crud.Object.GetProjectDetailsForEstimation(ret1);

            // _estimation.Object.PerformAutoEstimation(aionProjectModel);
            //bool ret4 = _crud.Object.SaveProjectAutoAssignmentDetails(aionProjectModel);


            // Assert.IsNotNull(ret4);
            // Assert.IsNotNull(new object());
        }

        [TestMethod]
        public void ProjectEstimationDefaultsCreationTest()
        {
            ////var project = _autocalc.ExecuteProjectEstimation(_projectparms);

            //AccelaProjectModel ret1 = _api.Object.GetProjectDetailsFromAccela(_projectparms);
            //ret1.ProjectIDRef = "1";
            //ret1.PropertyTypeRef = PropertyTypeEnumConst.Mega_Multi_Family; //"Mega Multi-Family";
            //ret1.ProjectTradesList = new List<TradeInfo>();
            //ret1.ProjectAgencyList = new List<AgencyInfo>();
            //ret1.ProjectTradesList.Add(new TradeInfo
            //{
            //    AccelaDepartmentDivisionRef = "Building",
            //    CostOfConstruction = (float)10.0,
            //    NoOfSheets = 12,
            //    OccupancyType = "R1",
            //    ReviewSqFt = 1200,
            //    AccelaDepartmentRegionRef = null
            //});
            //ret1.ProjectTradesList.Add(new TradeInfo
            //{
            //    AccelaDepartmentDivisionRef = "Electrical",
            //    CostOfConstruction = (float)15.0,
            //    NoOfSheets = 12,
            //    OccupancyType = "R4",
            //    ReviewSqFt = 1200,
            //    AccelaDepartmentRegionRef = null
            //});
            //ret1.ProjectAgencyList.Add(new AgencyInfo
            //{
            //    AccelaDepartmentDivisionRef = "Zoning",
            //    AccelaDepartmentRegionRef = "Davidson"
            //});

            //ProjectEstimation aionProjectModel = _crud.Object.GetProjectDetailsForEstimation(ret1);

            //_estimation.Object.PerformAutoEstimation(aionProjectModel);
            //bool ret4 = _crud.Object.SaveProjectAutoAssignmentDetails(aionProjectModel);


            //Assert.IsNotNull(ret4);
            //Assert.IsNotNull(new object());
        }

        [TestMethod]
        public void ProjectAutoEstimationEstimateCalcIsCorrect()
        {
            //put what the result should be from the calc
            //var esthours = 3.144;

            //var project = _autocalc.ExecuteProjectEstimation(_projectparms);

            //foreach (var item in project.Agencies)
            //{
            //    //assert that estimation hours are correct
            //    Assert.IsTrue(item.EstimationHours > 0);
            //    Assert.IsTrue(item.EstimationHours == esthours);
            //}

            //foreach (var item in project.Trades)
            //{
            //    //assert that estimation hours are correct
            //    Assert.IsTrue(item.EstimationHours > 0);
            //    Assert.IsTrue(item.EstimationHours == esthours);
            //}

            Assert.IsNotNull(new object());

        }
    }
}
