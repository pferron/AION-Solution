using AION.BL.BusinessObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AION.BL.Test
{
    [TestClass]
    public class ExternalSystemModelBOTests
    {
        Mock<ExternalSystemModelBO> _externalsystemmodelbo;
        [TestInitialize]
        public void TestInitialize()
        {
            _externalsystemmodelbo = new Mock<ExternalSystemModelBO>();
        }
        [TestMethod]
        public void BaseListIsNotNull()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            var x = _externalsystemmodelbo.Object.BaseList;
            Assert.IsNotNull(x);
        }
    }
}
