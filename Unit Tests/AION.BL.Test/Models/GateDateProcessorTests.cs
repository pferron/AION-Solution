using AION.Manager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AION.BL.Test.Models
{
    [TestClass]
    public class GateDateProcessorTests
    {
        [TestMethod]
        public void ValidateThatProcessorCalculatesCorrectDateWhenStartIsASunday()
        {
            var gateDateConfig = 2;
            DateTime startDateTime = new DateTime(2021, 11, 14);

            var processor = new GateDateProcessor(gateDateConfig, startDateTime);

            DateTime expectedGateDate = DateTime.Now.Date;

            DateTime actualGateDate = processor.CalculateGateDate();

            Assert.AreEqual(expectedGateDate, actualGateDate);
        }

        [TestMethod]
        public void ValidateThatProcessorCalculatesCorrectDateWhenStartIsAMonday()
        {
            var gateDateConfig = 2;
            DateTime startDateTime = new DateTime(2021, 11, 15);

            var processor = new GateDateProcessor(gateDateConfig, startDateTime);

            DateTime expectedGateDate = new DateTime(2021, 11, 12);

            if (expectedGateDate < DateTime.Now.Date) // upon a reschedule, do not issue a gate date that is prior to current date 
            {
                expectedGateDate = DateTime.Now.Date;
            }

            DateTime actualGateDate = processor.CalculateGateDate();

            Assert.AreEqual(expectedGateDate, actualGateDate);
        }

        [TestMethod]
        public void ValidateThatProcessorCalculatesCorrectDateWhenStartIsAWednesday()
        {
            var gateDateConfig = 2;
            DateTime startDateTime = new DateTime(2021, 11, 17);

            var processor = new GateDateProcessor(gateDateConfig, startDateTime);

            DateTime expectedGateDate = new DateTime(2021, 11, 15);

            if (expectedGateDate < DateTime.Now.Date) // upon a reschedule, do not issue a gate date that is prior to current date 
            {
                expectedGateDate = DateTime.Now.Date;
            }

            DateTime actualGateDate = processor.CalculateGateDate();

            Assert.AreEqual(expectedGateDate, actualGateDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
                "GateDateConfig must be a positive number.")]
        public void InvalidGateDateConfigThrowsError()
        {
            var gateDateConfig = -2;
            DateTime startDateTime = new DateTime(2020, 11, 15);

            var processor = new GateDateProcessor(gateDateConfig, startDateTime);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
                "StartDateTime must be a valid date.")]
        public void InvalidStartDateTimeThrowsError()
        {
            var gateDateConfig = -2;
            DateTime startDateTime = DateTime.MinValue;

            var processor = new GateDateProcessor(gateDateConfig, startDateTime);
        }
    }
}
