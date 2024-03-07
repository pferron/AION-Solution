using AION.Manager.Adapters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AION.BL.Test.Adapters
{
    [TestClass]
    public class DashboardAdapterTests
    {
        [Ignore]
        [TestMethod]
        public void GetProjectsReturnsSuccess()
        {
            IDashboardAdapter dashboardAdapter = new DashboardAdapter();
            var list = dashboardAdapter.GetEstimationDashboardProjectList(userid: 0);
            Assert.IsNotNull(list);
        }
    }
}
