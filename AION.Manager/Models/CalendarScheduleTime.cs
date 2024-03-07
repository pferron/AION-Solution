using AION.BL.Common;

namespace AION.BL
{
    public class CalendarScheduleTime
    {
        public int? ProjectScheduleId { get; set; }
        public int? HolidayConfigId { get; set; }
        public ScheduleTime ScheduleTime { get; set; }
    }
}