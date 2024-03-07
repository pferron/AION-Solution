using AION.Manager.BusinessObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AION.BL.Test
{
    [TestClass]
    public class SystemRoleModelBOTests
    {
        private Mock<SystemRoleModelBO> _systemrolemodelbo;

        [TestInitialize]
        public void TestInitialize()
        {
            _systemrolemodelbo = new Mock<SystemRoleModelBO>();
        }
        [TestMethod]
        public void BaseListIsNotNull()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            var x = _systemrolemodelbo.Object.BaseList;
            Assert.IsNotNull(x);
        }
    }
}
