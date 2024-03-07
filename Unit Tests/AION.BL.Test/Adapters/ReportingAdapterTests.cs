using AION.Manager.Adapters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AION.BL.Test.Adapters
{
    [TestClass]
    public class ReportingAdapterTests
    {
        //[TestMethod]
        public void GetSchedulingLeadTimeReportReturnsTrue()
        {
            var adapter = new ReportingAdapter();
            var success = adapter.GenerateSchedulingLeadTimeData(39);

            Assert.IsTrue(success);
        }

    }
}