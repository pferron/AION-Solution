using AION.Manager.Adapters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace AION.BL.Test.Adapters
{
    [TestClass]
    public class AdminAdapterTests
    {
        [TestMethod]
        public void GetUsersByFilterModeGetCountyFireReturnIsNotNull()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            AdminAdapter adminAdapter = new AdminAdapter();
            List<UserIdentity> o = adminAdapter.GetUsersByFilterModeUserManagement("", DepartmentNameEnums.Fire_Cornelius.ToString());
            List<UserIdentity> r = adminAdapter.GetUsersByFilterModeUserManagement("", SystemRoleEnum.Plan_Reviewer.ToString());
            UserIdentity countyfire = new UserIdentity();
            List<UserIdentity> q = o
                .Join(r,
                county => county.ID,
                reviewer => reviewer.ID,
                (county, reviewer) => county)
                .ToList();
            Assert.IsNotNull(o);
        }
        [TestMethod]
        public void GetUsersByFilterModeBlankReturnIsNotNull()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            AdminAdapter adminAdapter = new AdminAdapter();
            List<UserIdentity> o = adminAdapter.GetUsersByFilterModeUserManagement("", "");
            Assert.IsNotNull(o);
        }

        [TestMethod()]
        public void GetUserPermissionListTest()
        {
            AdminAdapter adminAdapter = new AdminAdapter();
            adminAdapter.GetUserPermissionList(0);

        }
    }
}
