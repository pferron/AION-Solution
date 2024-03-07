using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AION.Manager.Engines.Scheduling
{
    public class AllocationValues
    {
        public decimal Hours { get; set; }
        public DateTime? ScheduleStart { get; set; }
        public DateTime? ScheduleEnd { get; set; }
        public bool Success { get; set; }
    }
}