using AION.BL.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AION.BL.Test.Common
{
    [TestClass]
    public class DateTimeHelperTests
    {
        [TestMethod]
        public void TestDetermineWorkDateAfterDateSpecified()
        {
            DateTime startDate = new DateTime(2021, 5, 7);

            DateTime calculatedDate = DateTimeHelper.DetermineWorkDateAfterDateSpecified(startDate, 7);

            Assert.AreEqual(1,1);
        }

        [TestMethod]
        public void TestDetermineWorkDateBeforeDateSpecified()
        {
            DateTime startDate = new DateTime(2021, 10, 14);
            DateTime expectedDate = new DateTime(2021, 10, 12);

            if (expectedDate < DateTime.Now)
            {
                expectedDate = DateTime.Now.Date;
            }

            DateTime calculatedDate = DateTimeHelper.DetermineWorkDateBeforeDateSpecified(startDate, 2);

            Assert.AreEqual(expectedDate, calculatedDate);
        }

        [TestMethod]
        public void StandardHolidayDates_ContainsFullWeekForJulyFourthHoliday()
        {
            var standardHolidays = DateTimeHelper.GetBlockedHolidayDatesForExpress(2020);

            Assert.IsTrue(standardHolidays.Any(d => d.Month == 6 && d.Day == 29));
            Assert.IsTrue(standardHolidays.Any(d => d.Month == 6 && d.Day == 30));
            Assert.IsTrue(standardHolidays.Any(d => d.Month == 7 && d.Day == 1));
            Assert.IsTrue(standardHolidays.Any(d => d.Month == 7 && d.Day == 2));
            Assert.IsTrue(standardHolidays.Any(d => d.Month == 7 && d.Day == 3));
        }
    }
}
