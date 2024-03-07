using AION.Manager.Engines;
using AION.Manager.Models;
using AION.Scheduler.Engine.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AION.BL.Test.Engines
{
    [TestClass]
    public class ExpressProjectSchedulingEngineTests
    {
        private ExpressProjectSchedulingEngine _Engine;
        List<TimeSlot> _TimeSlots = new List<TimeSlot>();

        [TestInitialize]
        public void TestInitialize()
        {
            AutoScheduledExpressParams parms = new AutoScheduledExpressParams();
            parms.AccelaProjectIDRef = "COS-000285";
            parms.Cycle = 1;
            _Engine = new ExpressProjectSchedulingEngine(parms);

            _TimeSlots.Add(new TimeSlot()
            {
                AllocationType = TimeAllocationType.Project_Express_Blocked,
                DepartmentName = DepartmentNameEnums.Building,
                EndTime = new DateTime(2022, 1, 26, 16, 0, 0),
                StartTime = new DateTime(2022, 1, 26, 9, 0, 0),
                TotalTimeOfDay = new TimeSpan(7, 0, 0)
            });

            _TimeSlots.Add(new TimeSlot()
            {
                AllocationType = TimeAllocationType.Project_Express_Reserved,
                DepartmentName = DepartmentNameEnums.Building,
                EndTime = new DateTime(2022, 1, 26, 11, 0, 0),
                StartTime = new DateTime(2022, 1, 26, 9, 0, 0),
                TotalTimeOfDay = new TimeSpan(2, 0, 0)
            });

            _TimeSlots.Add(new TimeSlot()
            {
                AllocationType = TimeAllocationType.Project_Express_Reserved,
                DepartmentName = DepartmentNameEnums.Building,
                EndTime = new DateTime(2022, 1, 26, 14, 0, 0),
                StartTime = new DateTime(2022, 1, 26, 13, 0, 0),
                TotalTimeOfDay = new TimeSpan(1, 0, 0)
            });
        }

        //[TestMethod]
        public void TestAdjustReservedTimeSlots()
        {
            List<TimeSlot> adjustedTimeSlots = _Engine.AdjustReservedTimeSlots(_TimeSlots);

            TimeSlot reservedTimeSlot = adjustedTimeSlots.FirstOrDefault(x => x.AllocationType == TimeAllocationType.Project_Express_Blocked);

            DateTime expectedStart = new DateTime(2022, 1, 26, 11, 0, 0);
            DateTime expectedEnd = new DateTime(2022, 1, 26, 13, 0, 0);

            Assert.AreEqual(expectedStart, reservedTimeSlot.StartTime);
            Assert.AreEqual(expectedEnd, reservedTimeSlot.EndTime);
        }

        //[TestMethod]
        public void TestGetTotalHoursForCapacity()
        {
            decimal totalCapacity = _Engine.GetTotalHoursForCapacityExpress(_TimeSlots);
            Assert.AreEqual(totalCapacity, 3);
        }

        //[TestMethod]
        public void TestRequestFiveReservationdateWithRange()
        {
            DateTime start = DateTime.Now;
            DateTime end = start.AddDays(30);
            ExpressProjectSchedulingEngine enginetest = new ExpressProjectSchedulingEngine(start, end, "COS-000388");

            Assert.AreEqual(5, ExpressProjectSchedulingEngine.AvailableDateForRequestExpress.Count());
        }

    }
}
