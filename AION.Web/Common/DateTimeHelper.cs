using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AION.BL.Common
{
    public class DateTimeHelper
    {

        /// <summary>
        /// Get First week day of each month from start to end date.
        /// </summary>
        /// <param name="start">Start date of the range</param>
        /// <param name="end">End date of the range</param>
        /// <param name="day">Weekday enum instance</param>
        /// <param name="requiredWeekofMonth">1 = First week, 2 = Second week, 3 = Third week, 4 = Forth week, 5 = Last week/ 5th or 6th week.</param>
        /// <returns></returns>
        public static List<ScheduleTime> GenerateRecurringDaysList(DateTime start, DateTime end, RecurrenceEnum requiredWeekofMonth, DayOfWeek? day = null)
        {
            DateTime startTime = new DateTime(start.Year, start.Month, start.Day,start.Hour,start.Minute,00);
            DateTime endTime = new DateTime(end.Year, end.Month, end.Day, end.Hour, end.Minute, 00);
            List<ScheduleTime> ret = new List<ScheduleTime>();
            if (requiredWeekofMonth == RecurrenceEnum.Once)
            {
                ret.Add(MergeWithTime(start,startTime,endTime));
                return ret;
            }
            else if (requiredWeekofMonth == RecurrenceEnum.Weekly)
            {
                ret = GetWeekDayListFromDateRange(start, end, day.Value);
                return ret;
            }
            else if (requiredWeekofMonth == RecurrenceEnum.Yearly)
            {
                ret = GetYearlyDates(start, end);
                return ret;
            }
            else if (requiredWeekofMonth == RecurrenceEnum.Daily)
            {
                ret = GetWeekDayListFromDateRange(start, end,DayOfWeek.Monday);
                ret.AddRange(GetWeekDayListFromDateRange(start, end, DayOfWeek.Tuesday));
                ret.AddRange(GetWeekDayListFromDateRange(start, end, DayOfWeek.Wednesday));
                ret.AddRange(GetWeekDayListFromDateRange(start, end, DayOfWeek.Thursday));
                ret.AddRange(GetWeekDayListFromDateRange(start, end, DayOfWeek.Friday));
                return ret;
            }
            else if (requiredWeekofMonth == RecurrenceEnum.First || requiredWeekofMonth == RecurrenceEnum.Second
                || requiredWeekofMonth == RecurrenceEnum.Third || requiredWeekofMonth == RecurrenceEnum.Fourth
                || requiredWeekofMonth == RecurrenceEnum.Last)
            {
                if (day.HasValue == false)
                    throw new Exception("Required value missing. Need day of the week!.");
                DateTime startdate = new DateTime(start.Year, start.Month, start.Day);
                DateTime current = GetFirstOccuringWeekDayFromMonth(startdate, day.Value, requiredWeekofMonth);
                // If requiredWeekofMonth of weekday is before start date then skip that month.
                // This is required only for the first month. So doing it before loop.
                if (current < startdate)
                {
                    //skips to next month 1st
                    DateTime nextMonthDayOne = new DateTime(current.AddMonths(1).Year, current.AddMonths(1).Month, 1);
                    //now since current is past start, check it is past the end date. If yes then return and there will be no dates in list...
                    if (nextMonthDayOne > end)
                        return ret;
                    else //moves to next month 1st.
                        current = nextMonthDayOne;
                }
                else
                {
                    //this is the first date in range.
                    ret.Add(MergeWithTime(current, startTime, endTime));
                    //skips to next month 1st
                    DateTime nextMonthDayOne = new DateTime(current.AddMonths(1).Year, current.AddMonths(1).Month, 1);
                    // check it is past the end date. If yes then return and there will only date in list...
                    if (nextMonthDayOne > end)
                        return ret;
                    else //moves to next month 1st.
                        current = nextMonthDayOne;
                }
                //start going through each month.
                do
                {
                    current = GetFirstOccuringWeekDayFromMonth(current, day.Value, requiredWeekofMonth);
                    //if current is past end date, then return at this point.
                    if (current > end)
                        return ret;
                    else
                        ret.Add(MergeWithTime(current, startTime, endTime));
                    //skips to next month 1st
                    current = new DateTime(current.AddMonths(1).Year, current.AddMonths(1).Month, 1);
                } while (current <= end);
                return ret;
            }
            return ret;
        }

        public static List<ScheduleTime> GetReccuringDates(DateTime startDate, DateTime endDate, AppointmentRecurrenceRefEnum recurrenceRefEnum)
        {
            List<ScheduleTime> ret = new List<ScheduleTime>();
            switch (recurrenceRefEnum)
            {
                case AppointmentRecurrenceRefEnum.NA:
                case AppointmentRecurrenceRefEnum.Once:
                    return DateTimeHelper.GenerateRecurringDaysList(startDate, endDate, RecurrenceEnum.Once);
                case AppointmentRecurrenceRefEnum.Daily:
                    return DateTimeHelper.GenerateRecurringDaysList(startDate, endDate, RecurrenceEnum.Daily);
                case AppointmentRecurrenceRefEnum.Yearly:
                    return DateTimeHelper.GenerateRecurringDaysList(startDate, endDate, RecurrenceEnum.Yearly);
                case AppointmentRecurrenceRefEnum.First_Monday:
                    return DateTimeHelper.GenerateRecurringDaysList(startDate, endDate, RecurrenceEnum.First, DayOfWeek.Monday);
                case AppointmentRecurrenceRefEnum.First_Tuesday:
                    return DateTimeHelper.GenerateRecurringDaysList(startDate, endDate, RecurrenceEnum.First, DayOfWeek.Tuesday);
                case AppointmentRecurrenceRefEnum.First_Wednesday:
                    return DateTimeHelper.GenerateRecurringDaysList(startDate, endDate, RecurrenceEnum.First, DayOfWeek.Wednesday);
                case AppointmentRecurrenceRefEnum.First_Thursday:
                    return DateTimeHelper.GenerateRecurringDaysList(startDate, endDate, RecurrenceEnum.First, DayOfWeek.Thursday);
                case AppointmentRecurrenceRefEnum.First_Friday:
                    return DateTimeHelper.GenerateRecurringDaysList(startDate, endDate, RecurrenceEnum.First, DayOfWeek.Friday);
                case AppointmentRecurrenceRefEnum.Second_Monday:
                    return DateTimeHelper.GenerateRecurringDaysList(startDate, endDate, RecurrenceEnum.Second, DayOfWeek.Monday);
                case AppointmentRecurrenceRefEnum.Second_Tuesday:
                    return DateTimeHelper.GenerateRecurringDaysList(startDate, endDate, RecurrenceEnum.Second, DayOfWeek.Tuesday);
                case AppointmentRecurrenceRefEnum.Second_Wednesday:
                    return DateTimeHelper.GenerateRecurringDaysList(startDate, endDate, RecurrenceEnum.Second, DayOfWeek.Wednesday);
                case AppointmentRecurrenceRefEnum.Second_Thursday:
                    return DateTimeHelper.GenerateRecurringDaysList(startDate, endDate, RecurrenceEnum.Second, DayOfWeek.Thursday);
                case AppointmentRecurrenceRefEnum.Second_Friday:
                    return DateTimeHelper.GenerateRecurringDaysList(startDate, endDate, RecurrenceEnum.Second, DayOfWeek.Friday);
                case AppointmentRecurrenceRefEnum.Third_Monday:
                    return DateTimeHelper.GenerateRecurringDaysList(startDate, endDate, RecurrenceEnum.Third, DayOfWeek.Monday);
                case AppointmentRecurrenceRefEnum.Third_Tuesday:
                    return DateTimeHelper.GenerateRecurringDaysList(startDate, endDate, RecurrenceEnum.Third, DayOfWeek.Tuesday);
                case AppointmentRecurrenceRefEnum.Third_Wednesday:
                    return DateTimeHelper.GenerateRecurringDaysList(startDate, endDate, RecurrenceEnum.Third, DayOfWeek.Wednesday);
                case AppointmentRecurrenceRefEnum.Third_Thursday:
                    return DateTimeHelper.GenerateRecurringDaysList(startDate, endDate, RecurrenceEnum.Third, DayOfWeek.Thursday);
                case AppointmentRecurrenceRefEnum.Third_Friday:
                    return DateTimeHelper.GenerateRecurringDaysList(startDate, endDate, RecurrenceEnum.Third, DayOfWeek.Friday);
                case AppointmentRecurrenceRefEnum.Fourth_Monday:
                    return DateTimeHelper.GenerateRecurringDaysList(startDate, endDate, RecurrenceEnum.Fourth, DayOfWeek.Monday);
                case AppointmentRecurrenceRefEnum.Fourth_Tuesday:
                    return DateTimeHelper.GenerateRecurringDaysList(startDate, endDate, RecurrenceEnum.Fourth, DayOfWeek.Tuesday);
                case AppointmentRecurrenceRefEnum.Fourth_Wednesday:
                    return DateTimeHelper.GenerateRecurringDaysList(startDate, endDate, RecurrenceEnum.Fourth, DayOfWeek.Wednesday);
                case AppointmentRecurrenceRefEnum.Fourth_Thursday:
                    return DateTimeHelper.GenerateRecurringDaysList(startDate, endDate, RecurrenceEnum.Fourth, DayOfWeek.Thursday);
                case AppointmentRecurrenceRefEnum.Fourth_Friday:
                    return DateTimeHelper.GenerateRecurringDaysList(startDate, endDate, RecurrenceEnum.Fourth, DayOfWeek.Friday);
                case AppointmentRecurrenceRefEnum.Last_Monday:
                    return DateTimeHelper.GenerateRecurringDaysList(startDate, endDate, RecurrenceEnum.Last, DayOfWeek.Monday);
                case AppointmentRecurrenceRefEnum.Last_Tuesday:
                    return DateTimeHelper.GenerateRecurringDaysList(startDate, endDate, RecurrenceEnum.Last, DayOfWeek.Tuesday);
                case AppointmentRecurrenceRefEnum.Last_Wednesday:
                    return DateTimeHelper.GenerateRecurringDaysList(startDate, endDate, RecurrenceEnum.Last, DayOfWeek.Wednesday);
                case AppointmentRecurrenceRefEnum.Last_Thursday:
                    return DateTimeHelper.GenerateRecurringDaysList(startDate, endDate, RecurrenceEnum.Last, DayOfWeek.Thursday);
                case AppointmentRecurrenceRefEnum.Last_Friday:
                    return DateTimeHelper.GenerateRecurringDaysList(startDate, endDate, RecurrenceEnum.Last, DayOfWeek.Friday);
                case AppointmentRecurrenceRefEnum.Weekly_Monday:
                    return DateTimeHelper.GenerateRecurringDaysList(startDate, endDate, RecurrenceEnum.Weekly, DayOfWeek.Monday);
                case AppointmentRecurrenceRefEnum.Weekly_Tuesday:
                    return DateTimeHelper.GenerateRecurringDaysList(startDate, endDate, RecurrenceEnum.Weekly, DayOfWeek.Tuesday);
                case AppointmentRecurrenceRefEnum.Weekly_Wednesday:
                    return DateTimeHelper.GenerateRecurringDaysList(startDate, endDate, RecurrenceEnum.Weekly, DayOfWeek.Wednesday);
                case AppointmentRecurrenceRefEnum.Weekly_Thursday:
                    return DateTimeHelper.GenerateRecurringDaysList(startDate, endDate, RecurrenceEnum.Weekly, DayOfWeek.Thursday);
                case AppointmentRecurrenceRefEnum.Weekly_Friday:
                    return DateTimeHelper.GenerateRecurringDaysList(startDate, endDate, RecurrenceEnum.Weekly, DayOfWeek.Friday);
                default:
                    break;
            }
            return ret;
        }

        static ScheduleTime MergeWithTime(DateTime basedate,DateTime startTime,DateTime endtime)
        {
            DateTime retstart = new DateTime(basedate.Year, basedate.Month, basedate.Day, startTime.Hour, startTime.Minute, 00);
            DateTime retend = new DateTime(basedate.Year, basedate.Month, basedate.Day, endtime.Hour, endtime.Minute, 00);
            return new ScheduleTime(retstart, retend);
        }

        static List<ScheduleTime> GetYearlyDates(DateTime start, DateTime end)
        {
            DateTime startTime = new DateTime(start.Year, start.Month, start.Day, start.Hour, start.Minute, 00);
            DateTime endTime = new DateTime(end.Year, end.Month, end.Day, end.Hour, end.Minute, 00);
            List<ScheduleTime> ret = new List<ScheduleTime>();
            //the dates are inclusive on start date but exclusive on end date. only year time will be picked from end date.
            DateTime current = new DateTime(start.Year, start.Month, start.Day);
            //setting end date as same as start date but year as end date so that system will not create more appoinemt years.
            DateTime enddate = new DateTime(end.Year, start.Month, start.Day); 
            ret.Add(MergeWithTime(current, startTime, endTime));
            do
            {
                current = new DateTime(current.AddYears(1).Year, current.AddYears(1).Month, current.AddYears(1).Day);
                ret.Add(MergeWithTime(current, startTime, endTime));
            } while (current < enddate);
            return ret;
        }

        static DateTime LastWeekDayOfTheMonth(DateTime date, DayOfWeek dayOfWeek)
        {
            var day = new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1);
            while (day.DayOfWeek != dayOfWeek)
            {
                day = day.AddDays(-1);
            }
            return day;
        }

        /// <summary>
        /// Returns the same day if day matches the date else returns the next date which matches the weekday passed.
        /// </summary>
        /// <param name="startdate">startdate</param>
        /// <param name="day">weekday</param>
        /// <returns>Returns the same day if day matches the date else returns the next date which matches the weekday passed.</returns>
        public static DateTime GetOnOrAfterWeekday(DateTime startdate, DayOfWeek day)
        {
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysToAdd = ((int)day - (int)startdate.DayOfWeek + 7) % 7;
            return startdate.AddDays(daysToAdd);
        }

        /// <summary>
        /// Returns the same day if day matches the date else returns the last date which matches the weekday passed.
        /// </summary>
        /// <param name="startdate">startdate</param>
        /// <param name="day">weekday</param>
        /// <returns>Returns the same day if day matches the date else returns the last date which matches the weekday passed.</returns>
        public static DateTime GetOnOrBeforeWeekday(DateTime startdate, DayOfWeek day)
        {
            if (startdate.DayOfWeek == day)
                return startdate;
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysback = ((int)day - (int)startdate.DayOfWeek + 7) % 7;
            return startdate.AddDays((daysback - 7));
        }

        static List<ScheduleTime> GetWeekDayListFromDateRange(DateTime start, DateTime end, DayOfWeek day)
        {
            DateTime startTime = new DateTime(start.Year, start.Month, start.Day, start.Hour, start.Minute, 00);
            DateTime endTime = new DateTime(end.Year, end.Month, end.Day, end.Hour, end.Minute, 00);
            List<ScheduleTime> ret = new List<ScheduleTime>();
            DateTime current = start;
            do
            {
                while (current.DayOfWeek != day && current <= end)
                    current = current.AddDays(1);
                if (current.DayOfWeek == day && current <= end)
                    ret.Add(MergeWithTime(current, startTime, endTime));
                //skips to next week 1st
                DateTime nextWeekday = current.AddDays(7);
                if (nextWeekday > end)
                    current = current.AddDays(1);
                else
                    current = nextWeekday;
            } while (current <= end);
            return ret;
        }

        /// <summary>
        /// Calculates and returns the date from specifed month based on weekday and week number passed in. This optionaly excludes the hour and mniute and seconds based on orignial date passed.
        /// </summary>
        /// <param name="month">any date of the month as base referece</param>
        /// <param name="dayOfWeek">day of week Mon/Tues/Wed/Thu/Fri etc..</param>
        /// <param name="requiredWeek">!st wqeek = 1,2nd week =2,3,4,5,last etc</param>
        /// <param name="excludeHoursOnReturn">Optionaly excludes the hour and mniute and seconds based on orignial date passed.</param>
        /// <returns></returns>
        public static DateTime GetFirstOccuringWeekDayFromMonth(DateTime month, DayOfWeek dayOfWeek, RecurrenceEnum requiredWeek,bool excludeHoursOnReturn = false)
        {
            if (requiredWeek == RecurrenceEnum.First || requiredWeek == RecurrenceEnum.Second
                || requiredWeek == RecurrenceEnum.Third || requiredWeek == RecurrenceEnum.Fourth || requiredWeek == RecurrenceEnum.Last)
            {
                DateTime current;
                int requiredweekno = (int)requiredWeek;
                int foundDayInstanceCount = 0;
                if (requiredWeek == RecurrenceEnum.Last)
                {
                    DateTime lastDayofMonth = new DateTime(month.AddMonths(1).Year, month.AddMonths(1).Month, 1).AddDays(-1);
                    DateTime firstDayofMonth = new DateTime(month.Year, month.Month, 1);
                    current = lastDayofMonth;
                    //skip until required day is reached.
                    while (current.DayOfWeek != dayOfWeek && current >= firstDayofMonth)
                        current = current.AddDays(-1);
                    if (excludeHoursOnReturn == true)
                        return current;
                    else
                        return current.AddHours(month.Hour).AddMinutes(month.Minute).AddSeconds(month.Second).AddMilliseconds(month.Millisecond);
                }
                else
                {
                    DateTime firstDayofMonth = new DateTime(month.Year, month.Month, 1);
                    DateTime lastDayofMonth = new DateTime(month.AddMonths(1).Year, month.AddMonths(1).Month, 1).AddDays(-1);
                    current = firstDayofMonth;
                    if (current.DayOfWeek == dayOfWeek)
                        foundDayInstanceCount++;
                    //skip until required week is reached.
                    while (foundDayInstanceCount < requiredweekno && current <= lastDayofMonth)
                    {
                        current = current.AddDays(1);
                        if (current.DayOfWeek == dayOfWeek)
                            foundDayInstanceCount++;
                    }
                    //skip until required day is reached.
                    while (current.DayOfWeek != dayOfWeek && current <= lastDayofMonth)
                        current = current.AddDays(1);
                    if (excludeHoursOnReturn == true)
                        return current;
                    else
                        return current.AddHours(month.Hour).AddMinutes(month.Minute).AddSeconds(month.Second).AddMilliseconds(month.Millisecond);
                }
            }
            else
                throw new Exception("Exception, wrong week recurrence specified!");
        }

        static GregorianCalendar _gc = new GregorianCalendar();
        public static int GetWeekOfMonth(DateTime time)
        {
            DateTime first = new DateTime(time.Year, time.Month, 1);
            int ret = GetWeekOfYear(time) - GetWeekOfYear(first) + 1;
            return ret;
        }

        static int GetWeekOfYear(DateTime time)
        {
            return _gc.GetWeekOfYear(time, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        }

        public static List<DateTime> GetAllDayOfWeekPerMonth(int month, int year, DayOfWeek dayOfWeek)
        {
            var date = new DateTime(year, month, 1);

            if (date.DayOfWeek != dayOfWeek)
            {
                int daysUntilDayOfWeek = ((int)dayOfWeek - (int)date.DayOfWeek + 7) % 7;
                date = date.AddDays(daysUntilDayOfWeek);
            }

            List<DateTime> days = new List<DateTime>();

            while (date.Month == month)
            {
                days.Add(date);
                date = date.AddDays(7);
            }

            return days;
        }

        public static List<DateTime> GetHolidays(int year)
        {
            var holidays = new List<DateTime>();

            // New Years
            DateTime newYearsDay = DateTimeHelper.AdjustForWeekendHoliday(new DateTime(year, 1, 1));
            var newYearsWeek = GetAllWorkDaysForHolidayWeekByDate(newYearsDay);
            holidays.AddRange(newYearsWeek);

            // Independence Day
            DateTime independenceDay = DateTimeHelper.AdjustForWeekendHoliday(new DateTime(year, 7, 4));
            var independenceDayWeek = GetAllWorkDaysForHolidayWeekByDate(independenceDay);
            holidays.AddRange(independenceDayWeek);

            // Thanksgiving Day -- 4th Thursday in November 
            var thanksgiving = (from day in Enumerable.Range(1, 30)
                                where new DateTime(year, 11, day).DayOfWeek == DayOfWeek.Thursday
                                select day).ElementAt(3);
            DateTime thanksgivingDay = new DateTime(year, 11, thanksgiving);
            for (int i = 1; i <= 3; i++)
            {
                var dateToAdd = thanksgivingDay.AddDays(-i);
                holidays.Add(dateToAdd);
            }

            // Christmas Day 
            DateTime christmasDay = DateTimeHelper.AdjustForWeekendHoliday(new DateTime(year, 12, 25));
            var christmasDayWeek = GetAllWorkDaysForHolidayWeekByDate(christmasDay);
            holidays.AddRange(christmasDayWeek);

            return holidays;
        }

        public static List<DateTime> GetAllWorkDaysForHolidayWeekByDate(DateTime date)
        {
            DateTime startOfWeek = DateTimeHelper.StartOfWeek(date, DayOfWeek.Monday);
            DateTime[] allWeekDays = Enumerable.Range(0, 5).Select(d => startOfWeek.AddDays(d)).ToArray();
            return allWeekDays.ToList();
        }

        public static DateTime AdjustForWeekendHoliday(DateTime holiday)
        {
            if (holiday.DayOfWeek == DayOfWeek.Saturday)
            {
                return holiday.AddDays(-1);
            }
            else if (holiday.DayOfWeek == DayOfWeek.Sunday)
            {
                return holiday.AddDays(1);
            }
            else
            {
                return holiday;
            }
        }

        public static DateTime StartOfWeek(DateTime date, DayOfWeek startOfWeek)
        {
            int diff = (7 + (date.DayOfWeek - startOfWeek)) % 7;
            return date.AddDays(-1 * diff).Date;
        }
    }

    public struct ScheduleTime
    {
        public ScheduleTime(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
