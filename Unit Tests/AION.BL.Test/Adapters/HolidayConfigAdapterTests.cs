using AION.BL.Adapters;
using AION.BL.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AION.BL.Test.Adapters
{
    [TestClass]
    public class HolidayConfigAdapterTests
    {
        HolidayConfigAdapter _holidayAdapter;
        List<HolidayConfig> _configuredHolidays;

        [TestInitialize]
        public void Initialize()
        {
            _configuredHolidays = SetUpConfiguredHolidays();
            _holidayAdapter = new HolidayConfigAdapter();
        }

        private List<HolidayConfig> SetUpConfiguredHolidays()
        {
            var holidays = new List<HolidayConfig>();

            holidays.Add(new HolidayConfig() { HolidayDate = new DateTime(2020, 1, 20) });
            holidays.Add(new HolidayConfig() { HolidayDate = new DateTime(2020, 2, 15) });
            holidays.Add(new HolidayConfig() { HolidayDate = new DateTime(2020, 6, 29) });
            holidays.Add(new HolidayConfig() { HolidayDate = new DateTime(2020, 8, 17) });
            holidays.Add(new HolidayConfig() { HolidayDate = new DateTime(2020, 11, 15) });

            return holidays;
        }

        [TestMethod]
        public void GetConfiguredHolidayDates_DateExists()
        {
            var date = new DateTime(2020, 6, 29);
            var holidays = _holidayAdapter.GetConfiguredHolidayDates(_configuredHolidays);

            bool exists = holidays.Any(d => d.Month == date.Month && d.Day == date.Day);

            Assert.IsTrue(exists);
        }

        [TestMethod]
        public void GetConfiguredHolidayDates_DateDoesNotExist()
        {
            var date = new DateTime(2020, 7, 30);
            var holidays = _holidayAdapter.GetConfiguredHolidayDates(_configuredHolidays);

            bool exists = holidays.Any(d => d.Month == date.Month && d.Day == date.Day);

            Assert.IsFalse(exists);
        }
    }
}
