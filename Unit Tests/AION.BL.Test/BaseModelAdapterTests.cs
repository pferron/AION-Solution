using AION.BL.Adapters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AION.BL.Test
{
    [TestClass]
    public class BaseModelAdapterTests
    {
        private BaseModelAdapter _adapter;
        private ProjectParms _parms;
        [TestInitialize]
        public void TestInitialize()
        {
            _adapter = new BaseModelAdapter();
            _parms = new ProjectParms();
        }
        [TestMethod]
        public void GetAgenciesReturnsObject()
        {
            var result = _adapter.GetAllAgencies(_parms);

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetTradesReturnsObject()
        {
            var result = _adapter.GetAllTrades(_parms);

            Assert.IsNotNull(result);
        }
    }
}
