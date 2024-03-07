using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AION.BL
{
    public class ProjectSchedule
    {
        public int ProjectScheduleID { get; set; }

        public string ProjectScheduleTypeRef { get; set; }

        public int AppointmentID { get; set; }

        public DateTime? RecurringApptDt { get; set; }
    }
}
