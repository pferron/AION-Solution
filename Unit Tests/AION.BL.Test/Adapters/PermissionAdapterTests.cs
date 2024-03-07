using AION.BL.Adapters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AION.BL.Test.Adapters
{
    [TestClass]
    public class PermissionAdapterTests
    {
        private PermissionAdapter _permissionadapter;
        [TestInitialize]
        public void Initialize()
        {
            _permissionadapter = new PermissionAdapter();
        }
        [TestMethod]
        public void InsertNewRolePermissions()
        {

        }
    }
}
