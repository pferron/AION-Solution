using AION.Manager.BusinessObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace AION.BL.Test.BusinessObjects
{
    [TestClass]
    public class SystemRoleModelBOTests
    {
        [TestMethod]
        public void GetITSupportReturnsRole()
        {
            var systemroleid = new SystemRoleModelBO().BaseList.Where(x => x.SrcSystemValTxt == "ITS_Support_Group").FirstOrDefault().ID;
            Assert.IsTrue(systemroleid > 0);

        }


    }
}
