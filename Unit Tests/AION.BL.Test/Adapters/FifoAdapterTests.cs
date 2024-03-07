using AION.BL.Adapters;
using AION.BL.BusinessObjects;
using AION.Engine.BusinessObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AION.BL.Test.Adapters
{
    [TestClass]
    public class FifoAdapterTests
    {
        //[TestMethod]
        public void GetLastAssignedCityZoningReviewerTestWhenZeroIdIsPassed()
        {
            UserIdentity cityZoningUser = new UserIdentityModelBO().GetInstance(0);
            Assert.IsNotNull(cityZoningUser);
        }

        //[TestMethod]
        //public void TestFifoOptimizationEngine()
        //{
        //    var success = new FunctionAdapter().OptimizeFIFOAssignments();
        //    Assert.IsTrue(success);
        //}
    }
}
