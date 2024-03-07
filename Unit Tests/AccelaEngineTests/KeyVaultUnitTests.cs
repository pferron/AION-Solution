using System;
using Meck.Azure;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace AccelaEngineTests
{
    [TestClass]
    public class KeyVaultUnitTests
    {
        [TestMethod]
        public  void TestMethod1()
        {
            var result = KeyVaultUtility.GetSecret("AccelaUserId");


            Assert.IsTrue(result != null);





        }
    }
}
