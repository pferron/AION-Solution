using AIONEstimator.Engine.BusinessObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AION.BL.Test.Models
{
    [TestClass]
    public class PlanReviewerAvailableTimeBOTests
    {
        [TestMethod]
        public void GetPlanReviewerAvailableHours()
        {


            var result = new PlanReviewerAvailableHoursBO().GetAllPlanReviewerAvailableHours();
            Assert.IsTrue(result != null);

        }

    }
}
