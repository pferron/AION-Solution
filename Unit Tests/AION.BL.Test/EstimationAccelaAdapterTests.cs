using AION.BL.Adapters;
using AION.BL.Models;
using Meck.Shared.MeckDataMapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace AION.BL.Test
{

    [TestClass]
    public class EstimationAccelaAdapterTests
    {
        Mock<EstimationAccelaAdapter> _accela;

        private ProjectParms _projectparms;
        private string _projectid;
        private List<AgencyInfo> _incomingagencies;
        private List<TradeInfo> _incomingtrades;

        [TestInitialize]
        public void TestInitialize()
        {
            Mock<EstimationAccelaAdapter> mock = _accela = new Mock<EstimationAccelaAdapter>();
            _projectid = "MECKLENBURG-REC19-00000-10002";
            _projectparms = new ProjectParms
            {
                ProjectId = _projectid
            };
            _incomingagencies = new List<AgencyInfo>();
            _incomingtrades = new List<TradeInfo>();

        }

        //TODO: this call needs to be changed to call GetProjectDetailsLoad method
        [Ignore]
        [TestMethod]
        public void GetProjectDetailsReturnsObject()
        {
            //AccelaProjectModel ret1 = _accela.Object.GetProjectDetails(_projectparms);
            //Assert.IsNotNull(ret1);
        }
        [TestMethod]
        public void GetAllFacilitatorsReturnsObject()
        {
            //List<Facilitator> facilitators = _accela.Object.GetAllFacilitators();
            //Assert.IsNotNull(facilitators);
        }
        [TestMethod]
        public void GetAllReviewersReturnsObject()
        {
            //List<Reviewer> reviewers = _accela.Object.GetAllReviewers();
            //Assert.IsNotNull(reviewers);
        }
        [TestMethod]
        public void GetAllEstimatorsReturnsObject()
        {
            //List<EstimatorUIModel> estimators = _accela.Object.GetAllEstimators();
            //Assert.IsNotNull(estimators);
        }
        [TestMethod]
        public void UpsertUserSystemRolesIsNotNull()
        {
            // JCL 4/13/2020this test now includes parts that need AION.Manager. I do not think this can complete successfully.
            //if (AION.BL.Test.Global.FreezeTesting == true) return;
            // //estimator1_fn
            // _incomingagencies = new List<AgencyInfo>();
            // _incomingagencies.Add(new AgencyInfo
            // {
            //     AccelaDepartmentDivisionRef = "Fire",
            //     AccelaDepartmentRegionRef = "Huntersville"
            // });
            // _incomingtrades = new List<TradeInfo>();
            // _userid = 29;
            //List<SystemRole> roles = _accela.Object.UpsertUserSystemRoles(_incomingagencies, _incomingtrades, _userid);
            //Assert.IsNotNull(roles);
        }
        [TestMethod]
        public void GetProjectsReturnsObject()
        {
            //List<AccelaProjectModel> ret1 = _accela.Object.GetProjects();
            //Assert.IsNotNull(ret1);
        }
        [Ignore]
        [TestMethod]
        public void GetProjectEstimationListReturnsObject()
        {
            List<ProjectEstimation> ret1 = new EstimationAccelaAdapter().GetProjectEstimationList();
            Assert.IsNotNull(ret1);
        }
    }
}
