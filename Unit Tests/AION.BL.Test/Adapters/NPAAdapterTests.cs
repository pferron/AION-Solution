using AION.BL.Adapters;
using AION.BL.Common;
using AION.BL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AION.BL.Test.Adapters
{
    [TestClass()]
    public class NPAAdapterTests
    {
        INPAAdapter _adapter;
        NonProjectAppointment _npa;

        [TestInitialize]
        public void InitNPAAdapterTests()
        {
            _npa = new NonProjectAppointment();
            _adapter = new NPAAdapter(_npa);

        }

        [TestMethod()]
        public void DeleteNPATest()
        {
            //Assert.Fail();
        }

        [TestMethod()]
        public void GetNPAListTest()
        {
            //Assert.Fail();
        }

        [TestMethod()]
        public void InsertNewNPAAttendeesTest()
        {
            //Assert.Fail();
        }

        [TestMethod()]
        public void RemoveNewNPAAttendeesTest()
        {
            //Assert.Fail();
        }

        [TestMethod()]
        public void SearchNPAsTest()
        {
            //Assert.Fail();
        }

        [TestMethod()]
        public void UpsertNPATest()
        {
            //Assert.Fail();
            _npa = new NonProjectAppointment
            {
                AppoinmentName = "unit test appt",
                AppoinmentRecurrenceRefID = 5,
                AppointmentFrom = DateTime.Now,
                AppointmentTo = DateTime.Now,
                IsAllEhsDayCare = true,
                MeetingRoomRefId = 0,
                NPATypeRefID = 0
            };

        }


        [TestMethod()]
        public void GetAllNpaTypesTest()
        {
            //Assert.Fail();
        }

        [TestMethod()]
        public void InsertNpaTypeTest()
        {
            //Assert.Fail();
        }

        [TestMethod()]
        public void MakeNpaTypeActiveTest()
        {
            //Assert.Fail();
        }

        [TestMethod()]
        public void MakeNpaTypeInActiveTest()
        {
            //Assert.Fail();
        }

        [TestMethod()]
        public void Getdates_GetFirstOccuringWeekDayFromMonthTest()
        {
            Assert.IsTrue(DateTime.Parse("4/4/2020 12:00:00 AM") ==
                DateTimeHelper.GetFirstOccuringWeekDayFromMonth(DateTime.Parse("4/3/2020 12:00:00 AM"), DayOfWeek.Saturday, RecurrenceEnum.First));
            Assert.IsTrue(DateTime.Parse("4/16/2020 12:00:00 AM") ==
                DateTimeHelper.GetFirstOccuringWeekDayFromMonth(DateTime.Parse("4/3/2020 12:00:00 AM"), DayOfWeek.Thursday, RecurrenceEnum.Third));
            Assert.IsTrue(DateTime.Parse("4/30/2020 12:00:00 AM") ==
                DateTimeHelper.GetFirstOccuringWeekDayFromMonth(DateTime.Parse("4/3/2020 12:00:00 AM"), DayOfWeek.Thursday, RecurrenceEnum.Last));
            Assert.IsTrue(DateTime.Parse("4/20/2020 12:00:00 AM") ==
                DateTimeHelper.GetFirstOccuringWeekDayFromMonth(DateTime.Parse("4/3/2020 12:00:00 AM"), DayOfWeek.Monday, RecurrenceEnum.Third));
        }


        [TestMethod()]
        public void Getdates_GetFirstWeekDaysOfYearTest()
        {
            DayOfWeek dayofweek = DayOfWeek.Sunday;
            List<ScheduleTime> items = new List<ScheduleTime>();
            items = DateTimeHelper.GenerateRecurringDaysList(DateTime.Parse("4/3/2018 2:15:00 PM"), DateTime.Parse("5/1/2030 2:45:00 PM"), RecurrenceEnum.Yearly, dayofweek);
            Assert.IsTrue(items.Count == 13);
        }

        [TestMethod()]
        public void Getdates_GetFirstWeekDaysOfMonthWithMondayTest()
        {
            List<ScheduleTime> items = new List<ScheduleTime>();
            items = DateTimeHelper.GenerateRecurringDaysList(DateTime.Parse("4/6/2020 3:15:00 PM"), DateTime.Parse("6/12/2020 4:00:00 PM"), RecurrenceEnum.First, DayOfWeek.Monday);
            Assert.IsTrue(items.Count == 3);
            items = DateTimeHelper.GenerateRecurringDaysList(DateTime.Parse("4/6/2020 3:15:00 PM"), DateTime.Parse("4/12/2020 4:00:00 PM"), RecurrenceEnum.First, DayOfWeek.Monday);
            Assert.IsTrue(items.Count == 1);
            items = DateTimeHelper.GenerateRecurringDaysList(DateTime.Parse("4/6/2020 3:15:00 PM"), DateTime.Parse("5/2/2020 4:00:00 PM"), RecurrenceEnum.First, DayOfWeek.Monday);
            Assert.IsTrue(items.Count == 1);
        }

        [TestMethod()]
        public void Getdates_GetFirstWeekDaysOfMonthTest()
        {
            DayOfWeek dayofweek = DayOfWeek.Sunday;
            List<ScheduleTime> items = new List<ScheduleTime>();
            for (int j = 0; j < 7; j++)
            {
                //tests only from First,Sec,Thirds,Forth and Last
                for (int i = 1; i <= 5; i++)
                {
                    items = DateTimeHelper.GenerateRecurringDaysList(DateTime.Parse("4/3/2018 2:15:00 PM"), DateTime.Parse("5/1/2030 2:45:00 PM"), (RecurrenceEnum)i, dayofweek);
                    foreach (var item in items)
                    {
                        Assert.IsTrue(item.StartDate.DayOfWeek == dayofweek);
                        bool t = IsWeekNumberCorrect(item.StartDate, i, dayofweek);
                        if (t == false)
                            t = false;
                        Assert.IsTrue(t == true);
                    }
                }
                dayofweek = (DayOfWeek)((int)dayofweek) + 1;
            }
        }

        bool IsWeekNumberCorrect(DateTime date, int weekno, DayOfWeek dayofweek)
        {
            DateTime firstMonthDay = new DateTime(date.Year, date.Month, 1);
            DateTime LastMonthday = new DateTime(date.AddMonths(1).Year, date.AddMonths(1).Month, 1).AddDays(-1);
            DateTime current = firstMonthDay;

            int week = 0;
            //if (current.DayOfWeek == dayofweek)
            //    week++;
            do
            {
                if (current.DayOfWeek == dayofweek)
                    week++;
                if (date.Day == current.Day)
                    break;
                current = current.AddDays(1);
            }
            while (current.Day <= LastMonthday.Day);
            if (weekno <= 4)
                return weekno == week;
            else
                return week >= 4;
        }

        [TestMethod()]
        public void Getdates_GetWeekDaysOfWeeklyTest()
        {
            DayOfWeek dayofweek = DayOfWeek.Sunday;
            List<ScheduleTime> items = new List<ScheduleTime>();
            for (int j = 0; j < 7; j++)
            {
                items = DateTimeHelper.GenerateRecurringDaysList(DateTime.Parse("4/3/2020 2:15:00 PM"), DateTime.Parse("5/1/2022 2:45:00 PM"), RecurrenceEnum.Weekly, dayofweek);
                foreach (var item in items)
                {
                    Assert.IsTrue(item.StartDate.DayOfWeek == dayofweek);
                }
                dayofweek = (DayOfWeek)((int)dayofweek) + 1;
            }
        }

        [TestMethod()]
        public void Getdates_GetWeekDayOnceTest()
        {
            var items = DateTimeHelper.GenerateRecurringDaysList(DateTime.Parse("4/4/2020 2:15:00 PM"), DateTime.Parse("5/2/2020 2:45:00 PM"), RecurrenceEnum.Once, DayOfWeek.Saturday);
            Assert.IsNotNull(items);
        }

        //[TestMethod]
        public void TimeAllocationTypeListTest()
        {
            string item = "this test ( NPA - Personal Time )";
            TimeAllocationType timeAllocationType = Helper.TimeAllocationTypes.Where(x => item.Contains(x.ToStringValue())).FirstOrDefault();

            Assert.IsNotNull(timeAllocationType);
        }
    }

    static class DateTimeExtensions
    {
        static GregorianCalendar _gc = new GregorianCalendar();
        public static int GetWeekOfMonth(this DateTime time)
        {
            DateTime first = new DateTime(time.Year, time.Month, 1);
            int ret = time.GetWeekOfYear() - first.GetWeekOfYear() + 1;
            return ret;
        }

        static int GetWeekOfYear(this DateTime time)
        {
            return _gc.GetWeekOfYear(time, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        }
    }
}