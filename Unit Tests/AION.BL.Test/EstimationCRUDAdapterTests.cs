using AION.BL.Adapters;
using AION.BL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AION.BL.Tests
{
    [TestClass]
    public class EstimationCRUDAdapterTests
    {

        //[Ignore]
        //[TestMethod]
        public void GetProjectDetailsForEstimationByAccelaIdReturnsObject()
        {

            var project = new EstimationCRUDAdapter().GetProjectDetailsByProjectId(7313);

            Assert.IsInstanceOfType(project, typeof(ProjectEstimation));

        }
    }
}
