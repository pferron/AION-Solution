using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AION.BL.Adapters;
using AION.BL.Common;

namespace AION.BL.Test
{
    [TestClass]
    public class BLLoggingTest
    { 
        [TestMethod]
        public async Task BLLoggingWrapperAsyncTest()
        {
           Console.WriteLine(" starting Logging Test");
                LoggingWrapper mLoggingControl = new LoggingWrapper();
          
                Console.WriteLine("Async Logging Test");

          var result1  =  await mLoggingControl.BLLogMessageAsync(MethodBase.GetCurrentMethod(), "Unit TestError Message Async", null);

          Assert.IsTrue(result1);

            Console.WriteLine(" non async Logging Test");
          var result2 =  mLoggingControl.BLLogMessage(MethodBase.GetCurrentMethod(), "Unit TestError Message NonAsync", null);

            Assert.IsTrue(result2);

            Console.WriteLine(" Logging test completed"); 
            
        }

        [TestMethod]
        public async Task BLStaticLoggingWrapperAsyncTest()
        {
            Console.WriteLine(" starting Logging Test");
            LoggingWrapper mLoggingControl = new LoggingWrapper();
            Console.WriteLine("static Async Logging Test");
            
            var result1 = await mLoggingControl.BLLogMessageAsync(MethodBase.GetCurrentMethod(), "Unit TestError Message static Async", null);
            Assert.IsTrue(result1); 

            Console.WriteLine(" static non async starting Logging Test");
           var result2 =  mLoggingControl.BLLogMessage(MethodBase.GetCurrentMethod(), "Unit TestError Message static NonAsync", null);

             Assert.IsTrue(result2);
            
            Console.WriteLine(" Logging test completed");

        }
    }
}
