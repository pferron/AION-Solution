using AION.BL;
using System;

namespace AION.Scheduler.Engine.BusinessEntities
{
    public class TimeSlot
    {
        public TimeAllocationType AllocationType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int UserID { get; set; }
        public int UserScheduleID { get; set; }
        public int ProjectScheduleID { get; set; }
        public int ProjectID { get; set; }
        public string ProjectScheduleTypeName { get; set; }
        public DepartmentNameEnums DepartmentName { get; set; }
        public string ProjectCategory { get; set; }
        public TimeSpan TotalTimeOfDay { get; set; }
        public TimeSpan TotalTimeOfProject { get; set; }
    }

}
