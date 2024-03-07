using AION.BL.Adapters;
using AION.BL.BusinessObjects;
using AION.BL.Models;
using AION.Engine.BusinessEntities;
using AION.Manager.Adapters;
using AION.Manager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace AION.BL.Test.Adapters
{
    [TestClass]
    public class CalendarEventAdapterTests
    {
        [TestMethod]
        [Ignore]
        public void TestGetCalendarEvents()
        {
            ICalendarEventAdapter calendarEventAdapter = new CalendarEventAdapter();

            List<CalendarEventBE> calendarEvents = calendarEventAdapter.GetCalendarEvents(false);

            Assert.AreEqual(14, calendarEvents.Count());
        }
    }
}
