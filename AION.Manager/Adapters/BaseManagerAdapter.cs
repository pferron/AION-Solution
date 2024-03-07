using AION.Base;
using AION.BL.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AION.Manager.Adapters
{
    public class BaseManagerAdapter : BaseAdapter
    {
        private List<DateTime> _holidayList;

        public List<DateTime> HolidayList
        {
            get
            {
                if (_holidayList == null)
                    _holidayList = GetHolidays();
                return _holidayList;
            }
        }

        protected List<DateTime> GetHolidays()
        {
            HolidayConfigAdapter hd = new HolidayConfigAdapter();
            return hd.GetHolidayConfigDates();
        }

        protected bool IsDateHolidayOrWeekEnd(DateTime date)
        {
            return HolidayList.Any(x => x.Date == date.Date)
               || date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }

        protected DateTime PrevWorkingDay(DateTime date)
        {
            DateTime ret = date;
            do
            {
                ret = ret.AddDays(-1);
            }
            while (IsDateHolidayOrWeekEnd(ret));

            return ret;
        }

        protected DateTime NextWorkingDay(DateTime date)
        {
            DateTime ret = date;
            do
            {
                ret = ret.AddDays(1);
            }
            while (IsDateHolidayOrWeekEnd(ret));

            return ret;
        }
    }
}