using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AION.BL.Test.Models.Base
{
    [TestClass()]
    public class PropertyTypeEnumsExtensionsTests
    {
        [TestMethod()]
        public void AppointmentRecurrenceRefEnumCreateInstanceTest()
        {
            var result = new AppointmentRecurrenceRefEnum().CreateInstance("Friday", "Weekly");
            Assert.IsTrue(result == AppointmentRecurrenceRefEnum.Weekly_Friday);
        }
    }
}