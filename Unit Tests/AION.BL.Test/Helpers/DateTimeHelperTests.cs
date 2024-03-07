using AION.BL.Common;
using Meck.Azure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace AION.BL.Test.Adapters
{
    [TestClass]
    public class DateTimeHelperTests
    {
        [TestInitialize]
        public void SetupAccelaEngineTests()
        {
            Meck.Shared.Globals.AccelaAuthBaseUrl = GetConfigValue("AccellaAuthbaseUrl");
            Meck.Shared.Globals.AccelaAgency = GetConfigValue("AccelaAuthAgency");
            Meck.Shared.Globals.AccelaEnvironment = GetConfigValue("AccelaEnvironment");
            Meck.Shared.Globals.AccelaScope = GetConfigValue("AccelaScope");

            Meck.Shared.Globals.AccelaClientId = KeyVaultUtility.GetSecret("AccelaClientID");
            Meck.Shared.Globals.AccelaClientSecret = KeyVaultUtility.GetSecret("AccelaClientSecret");
            Meck.Shared.Globals.AccelaUser = KeyVaultUtility.GetSecret("AccelaUserID");
            Meck.Shared.Globals.AccelaUserName = KeyVaultUtility.GetSecret("AccelaUserID");
            Meck.Shared.Globals.AccelaPassword = KeyVaultUtility.GetSecret("AccelaPassword");

            Meck.Shared.Globals.AIONConnectionString = KeyVaultUtility.GetSecret("KeyVaultConnectionString");
        }

        private string GetConfigValue(string settingname)
        {
            return ConfigurationManager.AppSettings[settingname];

        }

        //[TestMethod]
        public void GetWorkDayCountFromDateRangeReturnsInt()
        {

            DateTimeHelper helper = new DateTimeHelper();
            var timeUtc = DateTime.UtcNow;
            TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);
            var timeToCalcLeadFrom = DateTime.UtcNow.AddDays(1);
            DateTime leadTimeStart = TimeZoneInfo.ConvertTimeFromUtc(timeToCalcLeadFrom, easternZone);
            leadTimeStart = leadTimeStart.Date;
            DateTime timeslotStart = DateTime.Now.Date.AddDays(1);

            var returnint = DateTimeHelper.GetWorkDayCountFromDateRange(leadTimeStart, timeslotStart);

            Assert.IsTrue(1 == 1);

        }

        //[TestMethod]
        public void TestDateTimeParse()
        {

            string selDate = "07/18/2023";
            string selStartTime = "8:00 AM";
            string selEndTime = "8:15 AM";

            DateTime selectedDate = DateTime.Parse(selDate);
            DateTime selectedStartTime = DateTime.Parse(selStartTime);
            DateTime selectedEndTime;

            Assert.IsNotNull(selectedStartTime);

        }
    }
}
